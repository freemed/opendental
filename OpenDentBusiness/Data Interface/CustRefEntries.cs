using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CustRefEntries{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all CustRefEntries.</summary>
		private static List<CustRefEntry> listt;

		///<summary>A list of all CustRefEntries.</summary>
		public static List<CustRefEntry> Listt{
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
			string command="SELECT * FROM custrefentry ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="CustRefEntry";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CustRefEntryCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<CustRefEntry> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CustRefEntry>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM custrefentry WHERE PatNum = "+POut.Long(patNum);
			return Crud.CustRefEntryCrud.SelectMany(command);
		}

		///<summary>Gets one CustRefEntry from the db.</summary>
		public static CustRefEntry GetOne(long custRefEntryNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CustRefEntry>(MethodBase.GetCurrentMethod(),custRefEntryNum);
			}
			return Crud.CustRefEntryCrud.SelectOne(custRefEntryNum);
		}

		///<summary></summary>
		public static long Insert(CustRefEntry custRefEntry){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				custRefEntry.CustRefEntryNum=Meth.GetLong(MethodBase.GetCurrentMethod(),custRefEntry);
				return custRefEntry.CustRefEntryNum;
			}
			return Crud.CustRefEntryCrud.Insert(custRefEntry);
		}

		///<summary></summary>
		public static void Update(CustRefEntry custRefEntry){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custRefEntry);
				return;
			}
			Crud.CustRefEntryCrud.Update(custRefEntry);
		}

		///<summary></summary>
		public static void Delete(long custRefEntryNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),custRefEntryNum);
				return;
			}
			string command= "DELETE FROM custrefentry WHERE CustRefEntryNum = "+POut.Long(custRefEntryNum);
			Db.NonQ(command);
		}
		*/



	}
}