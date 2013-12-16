using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrLabResults{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrLabResults.</summary>
		private static List<EhrLabResult> listt;

		///<summary>A list of all EhrLabResults.</summary>
		public static List<EhrLabResult> Listt{
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
			string command="SELECT * FROM ehrlabresult ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrLabResult";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrLabResultCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrLabResult> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrLabResult>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrlabresult WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrLabResultCrud.SelectMany(command);
		}

		///<summary>Gets one EhrLabResult from the db.</summary>
		public static EhrLabResult GetOne(long ehrLabResultNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrLabResult>(MethodBase.GetCurrentMethod(),ehrLabResultNum);
			}
			return Crud.EhrLabResultCrud.SelectOne(ehrLabResultNum);
		}

		///<summary></summary>
		public static long Insert(EhrLabResult ehrLabResult){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrLabResult.EhrLabResultNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrLabResult);
				return ehrLabResult.EhrLabResultNum;
			}
			return Crud.EhrLabResultCrud.Insert(ehrLabResult);
		}

		///<summary></summary>
		public static void Update(EhrLabResult ehrLabResult){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabResult);
				return;
			}
			Crud.EhrLabResultCrud.Update(ehrLabResult);
		}

		///<summary></summary>
		public static void Delete(long ehrLabResultNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabResultNum);
				return;
			}
			string command= "DELETE FROM ehrlabresult WHERE EhrLabResultNum = "+POut.Long(ehrLabResultNum);
			Db.NonQ(command);
		}
		*/



	}
}