using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Ucums{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		//#region CachePattern
		////This region can be eliminated if this is not a table type with cached data.
		////If leaving this region in place, be sure to add RefreshCache and FillCache 
		////to the Cache.cs file with all the other Cache types.

		/////<summary>A list of all UCUMs.</summary>
		//private static List<Ucum> listt;

		/////<summary>A list of all UCUMs.</summary>
		//public static List<Ucum> Listt{
		//	get {
		//		if(listt==null) {
		//			RefreshCache();
		//		}
		//		return listt;
		//	}
		//	set {
		//		listt=value;
		//	}
		//}

		/////<summary></summary>
		//public static DataTable RefreshCache(){
		//	//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
		//	string command="SELECT * FROM ucum ORDER BY ItemOrder";//stub query probably needs to be changed
		//	DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
		//	table.TableName="UCUM";
		//	FillCache(table);
		//	return table;
		//}

		/////<summary></summary>
		//public static void FillCache(DataTable table){
		//	//No need to check RemotingRole; no call to db.
		//	listt=Crud.UcumCrud.TableToList(table);
		//}
		//#endregion

		///<summary></summary>
		public static long Insert(Ucum ucum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ucum.UcumNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ucum);
				return ucum.UcumNum;
			}
			return Crud.UcumCrud.Insert(ucum);
		}

		///<summary>Returns a list of just the codes for use in update or insert logic.</summary>
		public static List<string> GetAllCodes() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			string command="SELECT UcumCode FROM ucum";
			DataTable table=DataCore.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				retVal.Add(table.Rows[i].ItemArray[0].ToString());
			}
			return retVal;
		}
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<UCUM> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<UCUM>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ucum WHERE PatNum = "+POut.Long(patNum);
			return Crud.UCUMCrud.SelectMany(command);
		}

		///<summary>Gets one UCUM from the db.</summary>
		public static UCUM GetOne(long uCUMNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<UCUM>(MethodBase.GetCurrentMethod(),uCUMNum);
			}
			return Crud.UCUMCrud.SelectOne(uCUMNum);
		}

		///<summary></summary>
		public static void Update(UCUM uCUM){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),uCUM);
				return;
			}
			Crud.UCUMCrud.Update(uCUM);
		}

		///<summary></summary>
		public static void Delete(long uCUMNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),uCUMNum);
				return;
			}
			string command= "DELETE FROM ucum WHERE UCUMNum = "+POut.Long(uCUMNum);
			Db.NonQ(command);
		}
		*/



	}
}