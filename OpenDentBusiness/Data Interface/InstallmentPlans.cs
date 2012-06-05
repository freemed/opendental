using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class InstallmentPlans{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all InstallmentPlans.</summary>
		private static List<InstallmentPlan> listt;

		///<summary>A list of all InstallmentPlans.</summary>
		public static List<InstallmentPlan> Listt{
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
			string command="SELECT * FROM installmentplan ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="InstallmentPlan";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.InstallmentPlanCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<InstallmentPlan> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<InstallmentPlan>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM installmentplan WHERE PatNum = "+POut.Long(patNum);
			return Crud.InstallmentPlanCrud.SelectMany(command);
		}

		///<summary>Gets one InstallmentPlan from the db.</summary>
		public static InstallmentPlan GetOne(long installmentPlanNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<InstallmentPlan>(MethodBase.GetCurrentMethod(),installmentPlanNum);
			}
			return Crud.InstallmentPlanCrud.SelectOne(installmentPlanNum);
		}

		///<summary></summary>
		public static long Insert(InstallmentPlan installmentPlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				installmentPlan.InstallmentPlanNum=Meth.GetLong(MethodBase.GetCurrentMethod(),installmentPlan);
				return installmentPlan.InstallmentPlanNum;
			}
			return Crud.InstallmentPlanCrud.Insert(installmentPlan);
		}

		///<summary></summary>
		public static void Update(InstallmentPlan installmentPlan){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),installmentPlan);
				return;
			}
			Crud.InstallmentPlanCrud.Update(installmentPlan);
		}

		///<summary></summary>
		public static void Delete(long installmentPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),installmentPlanNum);
				return;
			}
			string command= "DELETE FROM installmentplan WHERE InstallmentPlanNum = "+POut.Long(installmentPlanNum);
			Db.NonQ(command);
		}
		*/



	}
}