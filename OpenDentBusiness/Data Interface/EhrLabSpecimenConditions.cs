using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrLabSpecimenConditions {

		///<summary></summary>
		public static List<EhrLabSpecimenCondition> GetForLab(long ehrLabNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrLabSpecimenCondition>>(MethodBase.GetCurrentMethod(),ehrLabNum);
			}
			string command="SELECT * FROM ehrlabspecimencondition WHERE EhrLabNum = "+POut.Long(ehrLabNum);
			return Crud.EhrLabSpecimenConditionCrud.SelectMany(command);
		}

		///<summary>Deletes notes for lab results too.</summary>
		public static void DeleteForLab(long ehrLabNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabNum);
				return;
			}
			string command="DELETE FROM ehrlabspecimencondition WHERE EhrLabSpecimenNum = "+POut.Long(ehrLabNum);
			Db.NonQ(command);
		}

		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrLabSpecimenConditions.</summary>
		private static List<EhrLabSpecimenCondition> listt;

		///<summary>A list of all EhrLabSpecimenConditions.</summary>
		public static List<EhrLabSpecimenCondition> Listt{
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
			string command="SELECT * FROM ehrlabspecimencondition ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrLabSpecimenCondition";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrLabSpecimenConditionCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrLabSpecimenCondition> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrLabSpecimenCondition>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrlabspecimencondition WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrLabSpecimenConditionCrud.SelectMany(command);
		}

		///<summary>Gets one EhrLabSpecimenCondition from the db.</summary>
		public static EhrLabSpecimenCondition GetOne(long ehrLabSpecimenConditionNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrLabSpecimenCondition>(MethodBase.GetCurrentMethod(),ehrLabSpecimenConditionNum);
			}
			return Crud.EhrLabSpecimenConditionCrud.SelectOne(ehrLabSpecimenConditionNum);
		}

		///<summary></summary>
		public static long Insert(EhrLabSpecimenCondition ehrLabSpecimenCondition){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrLabSpecimenCondition.EhrLabSpecimenConditionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrLabSpecimenCondition);
				return ehrLabSpecimenCondition.EhrLabSpecimenConditionNum;
			}
			return Crud.EhrLabSpecimenConditionCrud.Insert(ehrLabSpecimenCondition);
		}

		///<summary></summary>
		public static void Update(EhrLabSpecimenCondition ehrLabSpecimenCondition){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabSpecimenCondition);
				return;
			}
			Crud.EhrLabSpecimenConditionCrud.Update(ehrLabSpecimenCondition);
		}

		///<summary></summary>
		public static void Delete(long ehrLabSpecimenConditionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrLabSpecimenConditionNum);
				return;
			}
			string command= "DELETE FROM ehrlabspecimencondition WHERE EhrLabSpecimenConditionNum = "+POut.Long(ehrLabSpecimenConditionNum);
			Db.NonQ(command);
		}
		*/



	}
}