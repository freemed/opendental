using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Resellers{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Resellers.</summary>
		private static List<Reseller> listt;

		///<summary>A list of all Resellers.</summary>
		public static List<Reseller> Listt{
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
			string command="SELECT * FROM reseller ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Reseller";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ResellerCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Reseller> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Reseller>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM reseller WHERE PatNum = "+POut.Long(patNum);
			return Crud.ResellerCrud.SelectMany(command);
		}

		///<summary>Gets one Reseller from the db.</summary>
		public static Reseller GetOne(long resellerNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Reseller>(MethodBase.GetCurrentMethod(),resellerNum);
			}
			return Crud.ResellerCrud.SelectOne(resellerNum);
		}

		///<summary></summary>
		public static long Insert(Reseller reseller){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				reseller.ResellerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),reseller);
				return reseller.ResellerNum;
			}
			return Crud.ResellerCrud.Insert(reseller);
		}

		///<summary></summary>
		public static void Update(Reseller reseller){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reseller);
				return;
			}
			Crud.ResellerCrud.Update(reseller);
		}

		///<summary></summary>
		public static void Delete(long resellerNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),resellerNum);
				return;
			}
			string command= "DELETE FROM reseller WHERE ResellerNum = "+POut.Long(resellerNum);
			Db.NonQ(command);
		}
		*/



	}
}