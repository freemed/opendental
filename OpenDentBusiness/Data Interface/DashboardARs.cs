using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class DashboardARs{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all DashboardARs.</summary>
		private static List<DashboardAR> listt;

		///<summary>A list of all DashboardARs.</summary>
		public static List<DashboardAR> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM dashboardar ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="DashboardAR";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.DashboardARCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<DashboardAR> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<DashboardAR>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM dashboardar WHERE PatNum = "+POut.Long(patNum);
			return Crud.DashboardARCrud.SelectMany(command);
		}

		///<summary>Gets one DashboardAR from the db.</summary>
		public static DashboardAR GetOne(long dashboardARNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<DashboardAR>(MethodBase.GetCurrentMethod(),dashboardARNum);
			}
			return Crud.DashboardARCrud.SelectOne(dashboardARNum);
		}

		///<summary></summary>
		public static long Insert(DashboardAR dashboardAR){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				dashboardAR.DashboardARNum=Meth.GetLong(MethodBase.GetCurrentMethod(),dashboardAR);
				return dashboardAR.DashboardARNum;
			}
			return Crud.DashboardARCrud.Insert(dashboardAR);
		}

		///<summary></summary>
		public static void Update(DashboardAR dashboardAR){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dashboardAR);
				return;
			}
			Crud.DashboardARCrud.Update(dashboardAR);
		}

		///<summary></summary>
		public static void Delete(long dashboardARNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),dashboardARNum);
				return;
			}
			string command= "DELETE FROM dashboardar WHERE DashboardARNum = "+POut.Long(dashboardARNum);
			Db.NonQ(command);
		}
		*/



	}
}