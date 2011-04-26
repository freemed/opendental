using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ReminderRules{
		#region CachePattern
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all ReminderRules.</summary>
		private static List<ReminderRule> listt;

		///<summary>A list of all ReminderRules.</summary>
		public static List<ReminderRule> Listt{
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
			string command="SELECT * FROM reminderrule ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ReminderRule";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.ReminderRuleCrud.TableToList(table);
		}
		#endregion

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<ReminderRule> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ReminderRule>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM reminderrule WHERE PatNum = "+POut.Long(patNum);
			return Crud.ReminderRuleCrud.SelectMany(command);
		}

		///<summary>Gets one ReminderRule from the db.</summary>
		public static ReminderRule GetOne(long reminderRuleNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<ReminderRule>(MethodBase.GetCurrentMethod(),reminderRuleNum);
			}
			return Crud.ReminderRuleCrud.SelectOne(reminderRuleNum);
		}

		///<summary></summary>
		public static long Insert(ReminderRule reminderRule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				reminderRule.ReminderRuleNum=Meth.GetLong(MethodBase.GetCurrentMethod(),reminderRule);
				return reminderRule.ReminderRuleNum;
			}
			return Crud.ReminderRuleCrud.Insert(reminderRule);
		}

		///<summary></summary>
		public static void Update(ReminderRule reminderRule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reminderRule);
				return;
			}
			Crud.ReminderRuleCrud.Update(reminderRule);
		}

		///<summary></summary>
		public static void Delete(long reminderRuleNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),reminderRuleNum);
				return;
			}
			string command= "DELETE FROM reminderrule WHERE ReminderRuleNum = "+POut.Long(reminderRuleNum);
			Db.NonQ(command);
		}
		*/



	}
}