using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LabResults{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all LabResults.</summary>
		private static List<LabResult> listt;

		///<summary>A list of all LabResults.</summary>
		public static List<LabResult> Listt{
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
			string command="SELECT * FROM labresult ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="LabResult";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.LabResultCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<LabResult> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LabResult>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM labresult WHERE PatNum = "+POut.Long(patNum);
			return Crud.LabResultCrud.SelectMany(command);
		}

		///<summary>Gets one LabResult from the db.</summary>
		public static LabResult GetOne(long labResultNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<LabResult>(MethodBase.GetCurrentMethod(),labResultNum);
			}
			return Crud.LabResultCrud.SelectOne(labResultNum);
		}

		///<summary></summary>
		public static long Insert(LabResult labResult){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				labResult.LabResultNum=Meth.GetLong(MethodBase.GetCurrentMethod(),labResult);
				return labResult.LabResultNum;
			}
			return Crud.LabResultCrud.Insert(labResult);
		}

		///<summary></summary>
		public static void Update(LabResult labResult){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labResult);
				return;
			}
			Crud.LabResultCrud.Update(labResult);
		}

		///<summary></summary>
		public static void Delete(long labResultNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),labResultNum);
				return;
			}
			string command= "DELETE FROM labresult WHERE LabResultNum = "+POut.Long(labResultNum);
			Db.NonQ(command);
		}
		*/



	}
}