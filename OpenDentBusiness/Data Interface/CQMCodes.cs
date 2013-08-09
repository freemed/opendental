using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CQMCodes{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all CQMCodes.</summary>
		private static List<CQMCode> listt;

		///<summary>A list of all CQMCodes.</summary>
		public static List<CQMCode> Listt{
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
			string command="SELECT * FROM cqmcode ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="CQMCode";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.CQMCodeCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<CQMCode> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CQMCode>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM cqmcode WHERE PatNum = "+POut.Long(patNum);
			return Crud.CQMCodeCrud.SelectMany(command);
		}

		///<summary>Gets one CQMCode from the db.</summary>
		public static CQMCode GetOne(long cQMCodeNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CQMCode>(MethodBase.GetCurrentMethod(),cQMCodeNum);
			}
			return Crud.CQMCodeCrud.SelectOne(cQMCodeNum);
		}

		///<summary></summary>
		public static long Insert(CQMCode cQMCode){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				cQMCode.CQMCodeNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cQMCode);
				return cQMCode.CQMCodeNum;
			}
			return Crud.CQMCodeCrud.Insert(cQMCode);
		}

		///<summary></summary>
		public static void Update(CQMCode cQMCode){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cQMCode);
				return;
			}
			Crud.CQMCodeCrud.Update(cQMCode);
		}

		///<summary></summary>
		public static void Delete(long cQMCodeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cQMCodeNum);
				return;
			}
			string command= "DELETE FROM cqmcode WHERE CQMCodeNum = "+POut.Long(cQMCodeNum);
			Db.NonQ(command);
		}
		*/



	}
}