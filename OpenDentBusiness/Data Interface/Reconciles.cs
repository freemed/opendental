using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary>The two lists get refreshed the first time they are needed rather than at startup.</summary>
	public class Reconciles {

		///<summary></summary>
		public static Reconcile[] GetList(long accountNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Reconcile[]>(MethodBase.GetCurrentMethod(),accountNum);
			}
			string command="SELECT * FROM reconcile WHERE AccountNum="+POut.Long(accountNum)
				+" ORDER BY DateReconcile";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary>Gets one reconcile directly from the database.  Program will crash if reconcile not found.</summary>
		public static Reconcile GetOne(long reconcileNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Reconcile>(MethodBase.GetCurrentMethod(),reconcileNum);
			}
			string command="SELECT * FROM reconcile WHERE ReconcileNum="+POut.Long(reconcileNum);
			return RefreshAndFill(Db.GetTable(command))[0];
		}

		private static Reconcile[] RefreshAndFill(DataTable table) {
			//No need to check RemotingRole; no call to db.
			Reconcile[] List=new Reconcile[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new Reconcile();
				List[i].ReconcileNum = PIn.Long(table.Rows[i][0].ToString());
				List[i].AccountNum   = PIn.Long(table.Rows[i][1].ToString());
				List[i].StartingBal  = PIn.Double(table.Rows[i][2].ToString());
				List[i].EndingBal    = PIn.Double(table.Rows[i][3].ToString());
				List[i].DateReconcile= PIn.Date(table.Rows[i][4].ToString());
				List[i].IsLocked     = PIn.Bool(table.Rows[i][5].ToString());
			}
			return List;
		}	

		///<summary></summary>
		public static long Insert(Reconcile reconcile) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				reconcile.ReconcileNum=Meth.GetLong(MethodBase.GetCurrentMethod(),reconcile);
				return reconcile.ReconcileNum;
			}
			if(PrefC.RandomKeys) {
				reconcile.ReconcileNum=ReplicationServers.GetKey("reconcile","ReconcileNum");
			}
			string command="INSERT INTO reconcile (";
			if(PrefC.RandomKeys) {
				command+="ReconcileNum,";
			}
			command+="AccountNum,StartingBal,EndingBal,DateReconcile,IsLocked) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(reconcile.ReconcileNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (reconcile.AccountNum)+"', "
				+"'"+POut.Double(reconcile.StartingBal)+"', "
				+"'"+POut.Double(reconcile.EndingBal)+"', "
				+POut.Date  (reconcile.DateReconcile)+", "
				+"'"+POut.Bool  (reconcile.IsLocked)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				reconcile.ReconcileNum=Db.NonQ(command,true);
			}
			return reconcile.ReconcileNum;
		}

		///<summary></summary>
		public static void Update(Reconcile reconcile) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reconcile);
				return;
			}
			string command= "UPDATE reconcile SET "
				+"AccountNum = '"    +POut.Long   (reconcile.AccountNum)+"' "
				+",StartingBal= '"   +POut.Double(reconcile.StartingBal)+"' "
				+",EndingBal = '"    +POut.Double(reconcile.EndingBal)+"' "
				+",DateReconcile = "+POut.Date  (reconcile.DateReconcile)+" "
				+",IsLocked = '"     +POut.Bool  (reconcile.IsLocked)+"' "
				+"WHERE ReconcileNum = '"+POut.Long(reconcile.ReconcileNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Throws exception if Reconcile is in use.</summary>
		public static void Delete(Reconcile reconcile) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reconcile);
				return;
			}
			//check to see if any journal entries are attached to this Reconcile
			string command="SELECT COUNT(*) FROM journalentry WHERE ReconcileNum="+POut.Long(reconcile.ReconcileNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("FormReconcileEdit",
					"Not allowed to delete a Reconcile with existing journal entries."));
			}
			command="DELETE FROM reconcile WHERE ReconcileNum = "+POut.Long(reconcile.ReconcileNum);
			Db.NonQ(command);
		}

	
	

		

	}

	
}




