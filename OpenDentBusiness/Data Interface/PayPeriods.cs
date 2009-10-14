using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PayPeriods {
		///<summary>A list of all payperiods.</summary>
		private static PayPeriod[] list;

		public static PayPeriod[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * from payperiod ORDER BY DateStart";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="PayPeriod";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new PayPeriod[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new PayPeriod();
				List[i].PayPeriodNum=PIn.PLong(table.Rows[i][0].ToString());
				List[i].DateStart=PIn.PDate(table.Rows[i][1].ToString());
				List[i].DateStop=PIn.PDate(table.Rows[i][2].ToString());
				List[i].DatePaycheck=PIn.PDate(table.Rows[i][3].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pp.PayPeriodNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pp);
				return pp.PayPeriodNum;
			}
			if(PrefC.RandomKeys) {
				pp.PayPeriodNum=ReplicationServers.GetKey("payperiod","PayPeriodNum");
			}
			string command="INSERT INTO payperiod (";
			if(PrefC.RandomKeys) {
				command+="PayPeriodNum,";
			}
			command+="DateStart,DateStop,DatePaycheck) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(pp.PayPeriodNum)+"', ";
			}
			command+=
				 POut.PDate  (pp.DateStart)+", "
				+POut.PDate  (pp.DateStop)+", "
				+POut.PDate  (pp.DatePaycheck)+")";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				pp.PayPeriodNum=Db.NonQ(command,true);
			}
			return pp.PayPeriodNum;
		}

		///<summary></summary>
		public static void Update(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pp);
				return;
			}
			string command= "UPDATE payperiod SET "
				+"DateStart = "    +POut.PDate  (pp.DateStart)+" "
				+",DateStop = "    +POut.PDate  (pp.DateStop)+" "
				+",DatePaycheck = "+POut.PDate  (pp.DatePaycheck)+" "
				+"WHERE PayPeriodNum = '"+POut.PLong(pp.PayPeriodNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pp);
				return;
			}
			string command= "DELETE FROM payperiod WHERE PayPeriodNum = "+POut.PLong(pp.PayPeriodNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int GetForDate(DateTime date){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<List.Length;i++){
				if(date.Date >= List[i].DateStart.Date && date.Date <= List[i].DateStop.Date){
					return i;
				}
			}
			return List.Length-1;//if we can't find a match, just return the last index
		}
		




	}

	
}




