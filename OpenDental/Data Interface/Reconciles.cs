using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>The two lists get refreshed the first time they are needed rather than at startup.</summary>
	public class Reconciles {

		///<summary></summary>
		public static Reconcile[] GetList(int accountNum) {
			string command="SELECT * FROM reconcile WHERE AccountNum="+POut.PInt(accountNum)
				+" ORDER BY DateReconcile";
			return RefreshAndFill(command);
		}

		///<summary>Gets one reconcile directly from the database.  Program will crash if reconcile not found.</summary>
		public static Reconcile GetOne(int reconcileNum) {
			string command="SELECT * FROM reconcile WHERE ReconcileNum="+POut.PInt(reconcileNum);
			return RefreshAndFill(command)[0];
		}

		private static Reconcile[] RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			Reconcile[] List=new Reconcile[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new Reconcile();
				List[i].ReconcileNum = PIn.PInt(table.Rows[i][0].ToString());
				List[i].AccountNum   = PIn.PInt(table.Rows[i][1].ToString());
				List[i].StartingBal  = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].EndingBal    = PIn.PDouble(table.Rows[i][3].ToString());
				List[i].DateReconcile= PIn.PDate(table.Rows[i][4].ToString());
				List[i].IsLocked     = PIn.PBool(table.Rows[i][5].ToString());
			}
			return List;
		}	

		///<summary></summary>
		public static void Insert(Reconcile reconcile) {
			if(PrefB.RandomKeys) {
				reconcile.ReconcileNum=MiscData.GetKey("reconcile","ReconcileNum");
			}
			string command="INSERT INTO reconcile (";
			if(PrefB.RandomKeys) {
				command+="ReconcileNum,";
			}
			command+="AccountNum,StartingBal,EndingBal,DateReconcile,IsLocked) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(reconcile.ReconcileNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (reconcile.AccountNum)+"', "
				+"'"+POut.PDouble(reconcile.StartingBal)+"', "
				+"'"+POut.PDouble(reconcile.EndingBal)+"', "
				+POut.PDate  (reconcile.DateReconcile)+", "
				+"'"+POut.PBool  (reconcile.IsLocked)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				reconcile.ReconcileNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Reconcile reconcile) {
			string command= "UPDATE reconcile SET "
				+"AccountNum = '"    +POut.PInt   (reconcile.AccountNum)+"' "
				+",StartingBal= '"   +POut.PDouble(reconcile.StartingBal)+"' "
				+",EndingBal = '"    +POut.PDouble(reconcile.EndingBal)+"' "
				+",DateReconcile = "+POut.PDate  (reconcile.DateReconcile)+" "
				+",IsLocked = '"     +POut.PBool  (reconcile.IsLocked)+"' "
				+"WHERE ReconcileNum = '"+POut.PInt(reconcile.ReconcileNum)+"'";
			General.NonQ(command);
		}

		///<summary>Throws exception if Reconcile is in use.</summary>
		public static void Delete(Reconcile reconcile) {
			//check to see if any journal entries are attached to this Reconcile
			string command="SELECT COUNT(*) FROM journalentry WHERE ReconcileNum="+POut.PInt(reconcile.ReconcileNum);
			if(General.GetCount(command)!="0"){
				throw new ApplicationException(Lan.g("FormReconcileEdit",
					"Not allowed to delete a Reconcile with existing journal entries."));
			}
			command="DELETE FROM reconcile WHERE ReconcileNum = "+POut.PInt(reconcile.ReconcileNum);
			General.NonQ(command);
		}

	
	

		

	}

	
}




