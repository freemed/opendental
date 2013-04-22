using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ResellerServices{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ResellerServices.</summary>
		private static List<ResellerService> listt;

		///<summary>A list of all ResellerServices.</summary>
		public static List<ResellerService> Listt{
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
			string command="SELECT * FROM resellerservice ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ResellerService";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ResellerServiceCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ResellerService> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ResellerService>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM resellerservice WHERE PatNum = "+POut.Long(patNum);
			return Crud.ResellerServiceCrud.SelectMany(command);
		}

		///<summary>Gets one ResellerService from the db.</summary>
		public static ResellerService GetOne(long resellerServiceNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ResellerService>(MethodBase.GetCurrentMethod(),resellerServiceNum);
			}
			return Crud.ResellerServiceCrud.SelectOne(resellerServiceNum);
		}

		///<summary></summary>
		public static long Insert(ResellerService resellerService){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				resellerService.ResellerServiceNum=Meth.GetLong(MethodBase.GetCurrentMethod(),resellerService);
				return resellerService.ResellerServiceNum;
			}
			return Crud.ResellerServiceCrud.Insert(resellerService);
		}

		///<summary></summary>
		public static void Update(ResellerService resellerService){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),resellerService);
				return;
			}
			Crud.ResellerServiceCrud.Update(resellerService);
		}

		///<summary></summary>
		public static void Delete(long resellerServiceNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),resellerServiceNum);
				return;
			}
			string command= "DELETE FROM resellerservice WHERE ResellerServiceNum = "+POut.Long(resellerServiceNum);
			Db.NonQ(command);
		}
		*/



	}
}