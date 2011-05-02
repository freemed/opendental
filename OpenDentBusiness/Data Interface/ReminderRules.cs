using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ReminderRules{

		///<summary></summary>
		public static long Insert(ReminderRule reminderRule) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				reminderRule.ReminderRuleNum=Meth.GetLong(MethodBase.GetCurrentMethod(),reminderRule);
				return reminderRule.ReminderRuleNum;
			}
			return Crud.ReminderRuleCrud.Insert(reminderRule);
		}

		///<summary></summary>
		public static void Update(ReminderRule reminderRule) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
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

		///<summary></summary>
		public static List<ReminderRule> SelectAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ReminderRule>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM reminderrule";
			return Crud.ReminderRuleCrud.SelectMany(command);
		}

		public static List<ReminderRule> SelectForPatient(Patient Pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ReminderRule>>(MethodBase.GetCurrentMethod());
			}
			string command;
			command = "";
			return Crud.ReminderRuleCrud.SelectMany(command);
		}


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


		*/



	}
}