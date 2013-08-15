using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LOINCs{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all LOINCs.</summary>
		private static List<LOINC> listt;

		///<summary>A list of all LOINCs.</summary>
		public static List<LOINC> Listt{
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
			string command="SELECT * FROM loinc ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="LOINC";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.LOINCCrud.TableToList(table);
		}
		#endregion

		///<summary></summary>
		public static long Insert(LOINC lOINC){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				lOINC.LOINCNum=Meth.GetLong(MethodBase.GetCurrentMethod(),lOINC);
				return lOINC.LOINCNum;
			}
			return Crud.LOINCCrud.Insert(lOINC);
		}

		///<summary></summary>
		public static List<LOINC> GetBySearchString(string searchText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LOINC>>(MethodBase.GetCurrentMethod(),searchText);
			}
			string command="SELECT * FROM loinc WHERE LOINCCode LIKE '%"+POut.String(searchText)+"%' OR NameLongCommon LIKE '%"+POut.String(searchText)+"%'";
			return Crud.LOINCCrud.SelectMany(command);
		}

		///<summary>Gets one LOINC from the db based on LoincCode, returns null if not found.</summary>
		public static LOINC GetByCode(string lOINCCode) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<LOINC>(MethodBase.GetCurrentMethod(),lOINCCode);
			}
			string command="SELECT * FROM loinc WHERE LoincCode='"+POut.String(lOINCCode)+"'";
			List<LOINC> retVal=Crud.LOINCCrud.SelectMany(command);
			if(retVal.Count>0) {
				return retVal[0];
			}
			return null;
		}

		///<summary>CAUTION, this empties the entire loinc table. "DELETE FROM loinc"</summary>
		public static void DeleteAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="DELETE FROM loinc";
			Db.NonQ(command);
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="ALTER TABLE loinc AUTO_INCREMENT = 1";//resets the primary key to start counting from 1 again.
				Db.NonQ(command);
			}
			return;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<LOINC> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<LOINC>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM loinc WHERE PatNum = "+POut.Long(patNum);
			return Crud.LOINCCrud.SelectMany(command);
		}

		///<summary>Gets one LOINC from the db.</summary>
		public static LOINC GetOne(long lOINCNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<LOINC>(MethodBase.GetCurrentMethod(),lOINCNum);
			}
			return Crud.LOINCCrud.SelectOne(lOINCNum);
		}

		///<summary></summary>
		public static void Update(LOINC lOINC){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lOINC);
				return;
			}
			Crud.LOINCCrud.Update(lOINC);
		}

		///<summary></summary>
		public static void Delete(long lOINCNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),lOINCNum);
				return;
			}
			string command= "DELETE FROM loinc WHERE LOINCNum = "+POut.Long(lOINCNum);
			Db.NonQ(command);
		}
		*/



	}
}