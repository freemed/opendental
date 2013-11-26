using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EhrCarePlans{
		//If this table type will exist as cached data, uncomment the CachePattern region below and edit.
		/*
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all EhrCarePlans.</summary>
		private static List<EhrCarePlan> listt;

		///<summary>A list of all EhrCarePlans.</summary>
		public static List<EhrCarePlan> Listt{
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
			string command="SELECT * FROM ehrcareplan ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="EhrCarePlan";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.EhrCarePlanCrud.TableToList(table);
		}
		#endregion
		*/
		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<EhrCarePlan> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EhrCarePlan>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM ehrcareplan WHERE PatNum = "+POut.Long(patNum);
			return Crud.EhrCarePlanCrud.SelectMany(command);
		}

		///<summary>Gets one EhrCarePlan from the db.</summary>
		public static EhrCarePlan GetOne(long ehrCarePlanNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<EhrCarePlan>(MethodBase.GetCurrentMethod(),ehrCarePlanNum);
			}
			return Crud.EhrCarePlanCrud.SelectOne(ehrCarePlanNum);
		}

		///<summary></summary>
		public static long Insert(EhrCarePlan ehrCarePlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				ehrCarePlan.EhrCarePlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),ehrCarePlan);
				return ehrCarePlan.EhrCarePlanNum;
			}
			return Crud.EhrCarePlanCrud.Insert(ehrCarePlan);
		}

		///<summary></summary>
		public static void Update(EhrCarePlan ehrCarePlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrCarePlan);
				return;
			}
			Crud.EhrCarePlanCrud.Update(ehrCarePlan);
		}

		///<summary></summary>
		public static void Delete(long ehrCarePlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),ehrCarePlanNum);
				return;
			}
			string command= "DELETE FROM ehrcareplan WHERE EhrCarePlanNum = "+POut.Long(ehrCarePlanNum);
			Db.NonQ(command);
		}
		*/



	}
}