using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AutomationConditions{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all AutomationConditions.</summary>
		private static List<AutomationCondition> listt;

		///<summary>A list of all AutomationConditions.</summary>
		public static List<AutomationCondition> Listt{
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
			string command="SELECT * FROM automationcondition";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutomationCondition";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.AutomationConditionCrud.TableToList(table);
		}
		#endregion

		///<summary>Gets one AutomationCondition from the db.</summary>
		public static AutomationCondition GetOne(long automationConditionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<AutomationCondition>(MethodBase.GetCurrentMethod(),automationConditionNum);
			}
			return Crud.AutomationConditionCrud.SelectOne(automationConditionNum);
		}

		///<summary>Gets a list of AutomationConditions from the db by AutomationNum.</summary>
		public static List<AutomationCondition> GetListByAutomationNum(long automationNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AutomationCondition>>(MethodBase.GetCurrentMethod(),automationNum);
			}
			string command="SELECT * FROM automationcondition WHERE AutomationNum = "+POut.Long(automationNum);
			return Crud.AutomationConditionCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(AutomationCondition automationCondition) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				automationCondition.AutomationConditionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),automationCondition);
				return automationCondition.AutomationConditionNum;
			}
			return Crud.AutomationConditionCrud.Insert(automationCondition);
		}

		///<summary></summary>
		public static void Update(AutomationCondition automationCondition) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),automationCondition);
				return;
			}
			Crud.AutomationConditionCrud.Update(automationCondition);
		}

		///<summary></summary>
		public static void Delete(long automationConditionNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),automationConditionNum);
				return;
			}
			string command= "DELETE FROM automationcondition WHERE AutomationConditionNum = "+POut.Long(automationConditionNum);
			Db.NonQ(command);
		}

		///<summary></summary.
		public static void DeleteByAutomationNum(long automationNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),automationNum);
				return;
			}
			string command= "DELETE FROM automationcondition WHERE AutomationNum = "+POut.Long(automationNum);
			Db.NonQ(command);
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<AutomationCondition> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<AutomationCondition>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM automationcondition WHERE PatNum = "+POut.Long(patNum);
			return Crud.AutomationConditionCrud.SelectMany(command);
		}

		
		*/



	}
}