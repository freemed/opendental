using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrLabClinicalInfos{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrLabClinicalInfos.</summary>
		private static List<EhrLabClinicalInfo> listt;

		///<summary>A list of all EhrLabClinicalInfos.</summary>
		public static List<EhrLabClinicalInfo> Listt{
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
			string command="SELECT * FROM ehrlabclinicalinfo ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrLabClinicalInfo";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrLabClinicalInfoCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrLabClinicalInfo> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrLabClinicalInfo>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrlabclinicalinfo WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrLabClinicalInfoCrud.SelectMany(command);
		}

		///<summary>Gets one EhrLabClinicalInfo from the db.</summary>
		public static EhrLabClinicalInfo GetOne(long ehrLabClinicalInfoNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrLabClinicalInfo>(MethodBase.GetCurrentMethod(),ehrLabClinicalInfoNum);
			}
			return Crud.EhrLabClinicalInfoCrud.SelectOne(ehrLabClinicalInfoNum);
		}

		///<summary></summary>
		public static long Insert(EhrLabClinicalInfo ehrLabClinicalInfo){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrLabClinicalInfo.EhrLabClinicalInfoNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrLabClinicalInfo);
				return ehrLabClinicalInfo.EhrLabClinicalInfoNum;
			}
			return Crud.EhrLabClinicalInfoCrud.Insert(ehrLabClinicalInfo);
		}

		///<summary></summary>
		public static void Update(EhrLabClinicalInfo ehrLabClinicalInfo){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabClinicalInfo);
				return;
			}
			Crud.EhrLabClinicalInfoCrud.Update(ehrLabClinicalInfo);
		}

		///<summary></summary>
		public static void Delete(long ehrLabClinicalInfoNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabClinicalInfoNum);
				return;
			}
			string command= "DELETE FROM ehrlabclinicalinfo WHERE EhrLabClinicalInfoNum = "+POut.Long(ehrLabClinicalInfoNum);
			Db.NonQ(command);
		}
		*/



	}
}