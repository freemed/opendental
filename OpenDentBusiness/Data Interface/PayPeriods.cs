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
			list=Crud.PayPeriodCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pp.PayPeriodNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pp);
				return pp.PayPeriodNum;
			}
			return Crud.PayPeriodCrud.Insert(pp);
		}

		///<summary></summary>
		public static void Update(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pp);
				return;
			}
			Crud.PayPeriodCrud.Update(pp);
		}

		///<summary></summary>
		public static void Delete(PayPeriod pp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pp);
				return;
			}
			string command= "DELETE FROM payperiod WHERE PayPeriodNum = "+POut.Long(pp.PayPeriodNum);
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




