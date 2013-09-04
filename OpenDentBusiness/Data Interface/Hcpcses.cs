using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Hcpcses{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Hcpcses.</summary>
		private static List<Hcpcs> listt;

		///<summary>A list of all Hcpcses.</summary>
		public static List<Hcpcs> Listt{
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
			string command="SELECT * FROM hcpcs ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Hcpcs";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.HcpcsCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(Hcpcs hcpcs){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				hcpcs.HcpcsNum=Meth.GetLong(MethodBase.GetCurrentMethod(),hcpcs);
				return hcpcs.HcpcsNum;
			}
			return Crud.HcpcsCrud.Insert(hcpcs);
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT HcpcsCode FROM hcpcs";
			DataTable table=DataCore.GetTable(command);
			foreach(DataRow row in table.Rows) {
				retVal.Add(row.ItemArray[0].ToString());
			}
			return retVal;
		}


		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Hcpcs> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Hcpcs>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM hcpcs WHERE PatNum = "+POut.Long(patNum);
			return Crud.HcpcsCrud.SelectMany(command);
		}

		///<summary>Gets one Hcpcs from the db.</summary>
		public static Hcpcs GetOne(long hcpcsNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Hcpcs>(MethodBase.GetCurrentMethod(),hcpcsNum);
			}
			return Crud.HcpcsCrud.SelectOne(hcpcsNum);
		}

		///<summary></summary>
		public static void Update(Hcpcs hcpcs){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hcpcs);
				return;
			}
			Crud.HcpcsCrud.Update(hcpcs);
		}

		///<summary></summary>
		public static void Delete(long hcpcsNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),hcpcsNum);
				return;
			}
			string command= "DELETE FROM hcpcs WHERE HcpcsNum = "+POut.Long(hcpcsNum);
			Db.NonQ(command);
		}
		*/



	}
}