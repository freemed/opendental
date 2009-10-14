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
			string command="SELECT * FROM reconcile WHERE AccountNum="+POut.PLong(accountNum)
				+" ORDER BY DateReconcile";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary>Gets one reconcile directly from the database.  Program will crash if reconcile not found.</summary>
		public static Reconcile GetOne(long reconcileNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Reconcile>(MethodBase.GetCurrentMethod(),reconcileNum);
			}
			string command="SELECT * FROM reconcile WHERE ReconcileNum="+POut.PLong(reconcileNum);
			return RefreshAndFill(Db.GetTable(command))[0];
		}

		private static Reconcile[] RefreshAndFill(DataTable table) {
			//No need to check RemotingRole; no call to db.
			Reconcile[] List=new Reconcile[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new Reconcile();
				List[i].ReconcileNum = PIn.PLong(table.Rows[i][0].ToString());
				List[i].AccountNum   = PIn.PLong(table.Rows[i][1].ToString());
				List[i].StartingBal  = PIn.PDouble(table.Rows[i][2].ToString());
				List[i].EndingBal    = PIn.PDouble(table.Rows[i][3].ToString());
				List[i].DateReconcile= PIn.PDate(table.Rows[i][4].ToString());
				List[i].IsLocked     = PIn.PBool(table.Rows[i][5].ToString());
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
				command+="'"+POut.PLong(reconcile.ReconcileNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (reconcile.AccountNum)+"', "
				+"'"+POut.PDouble(reconcile.StartingBal)+"', "
				+"'"+POut.PDouble(reconcile.EndingBal)+"', "
				+POut.PDate  (reconcile.DateReconcile)+", "
				+"'"+POut.PBool  (reconcile.IsLocked)+"')";
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
				+"AccountNum = '"    +POut.PLong   (reconcile.AccountNum)+"' "
				+",StartingBal= '"   +POut.PDouble(reconcile.StartingBal)+"' "
				+",EndingBal = '"    +POut.PDouble(reconcile.EndingBal)+"' "
				+",DateReconcile = "+POut.PDate  (reconcile.DateReconcile)+" "
				+",IsLocked = '"     +POut.PBool  (reconcile.IsLocked)+"' "
				+"WHERE ReconcileNum = '"+POut.PLong(reconcile.ReconcileNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Throws exception if Reconcile is in use.</summary>
		public static void Delete(Reconcile reconcile) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reconcile);
				return;
			}
			//check to see if any journal entries are attached to this Reconcile
			string command="SELECT COUNT(*) FROM journalentry WHERE ReconcileNum="+POut.PLong(reconcile.ReconcileNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("FormReconcileEdit",
					"Not allowed to delete a Reconcile with existing journal entries."));
			}
			command="DELETE FROM reconcile WHERE ReconcileNum = "+POut.PLong(reconcile.ReconcileNum);
			Db.NonQ(command);
		}

	
	

		

	}

	
}




