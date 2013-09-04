using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary>Cache pattern only used for updates.</summary>
	public class Cdcrecs{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Cdcrecs.</summary>
		private static List<Cdcrec> listt;

		///<summary>A list of all Cdcrecs.</summary>
		public static List<Cdcrec> Listt{
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
			string command="SELECT * FROM cdcrec ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Cdcrec";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CdcrecCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(Cdcrec cdcrec){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				cdcrec.CdcrecNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cdcrec);
				return cdcrec.CdcrecNum;
			}
			return Crud.CdcrecCrud.Insert(cdcrec);
		}

		///<summary></summary>
		public static void Update(Cdcrec cdcrec){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cdcrec);
				return;
			}
			Crud.CdcrecCrud.Update(cdcrec);
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT CdcRecCode FROM cdcrec";
			DataTable table=DataCore.GetTable(command);
			foreach(DataRow row in table.Rows) {
				retVal.Add(row.ItemArray[0].ToString());
			}
			return retVal;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Cdcrec> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Cdcrec>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM cdcrec WHERE PatNum = "+POut.Long(patNum);
			return Crud.CdcrecCrud.SelectMany(command);
		}

		///<summary>Gets one Cdcrec from the db.</summary>
		public static Cdcrec GetOne(long cdcrecNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Cdcrec>(MethodBase.GetCurrentMethod(),cdcrecNum);
			}
			return Crud.CdcrecCrud.SelectOne(cdcrecNum);
		}

		///<summary></summary>
		public static void Delete(long cdcrecNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cdcrecNum);
				return;
			}
			string command= "DELETE FROM cdcrec WHERE CdcrecNum = "+POut.Long(cdcrecNum);
			Db.NonQ(command);
		}
		*/



	}
}