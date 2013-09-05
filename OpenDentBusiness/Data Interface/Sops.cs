using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Sops{
		//If this table type will exist as cached data, uncomment the CachePattern region below.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all Sops.</summary>
		private static List<Sop> listt;

		///<summary>A list of all Sops.</summary>
		public static List<Sop> Listt{
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
			string command="SELECT * FROM sop ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Sop";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.SopCrud.TableToList(table);
		}
		#endregion
		*/

		///<summary></summary>
		public static long Insert(Sop sop){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				sop.SopNum=Meth.GetLong(MethodBase.GetCurrentMethod(),sop);
				return sop.SopNum;
			}
			return Crud.SopCrud.Insert(sop);
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT SopCode FROM Sop";
			DataTable table=DataCore.GetTable(command);
			foreach(DataRow row in table.Rows) {
				retVal.Add(row.ItemArray[0].ToString());
			}
			return retVal;
		}

		///<summary></summary>
		public static void TruncateAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="TRUNCATE TABLE sop";//Oracle compatible
			DataCore.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<Sop> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Sop>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM sop WHERE PatNum = "+POut.Long(patNum);
			return Crud.SopCrud.SelectMany(command);
		}

		///<summary>Gets one Sop from the db.</summary>
		public static Sop GetOne(long sopNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<Sop>(MethodBase.GetCurrentMethod(),sopNum);
			}
			return Crud.SopCrud.SelectOne(sopNum);
		}

		///<summary></summary>
		public static void Update(Sop sop){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sop);
				return;
			}
			Crud.SopCrud.Update(sop);
		}

		///<summary></summary>
		public static void Delete(long sopNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),sopNum);
				return;
			}
			string command= "DELETE FROM sop WHERE SopNum = "+POut.Long(sopNum);
			Db.NonQ(command);
		}
		*/



	}
}