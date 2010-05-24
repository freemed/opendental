using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class AppointmentRules {

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM appointmentrule";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AppointmentRule";
			FillCache(table);
			return table;
		}

		private static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			AppointmentRuleC.List=Crud.AppointmentRuleCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(AppointmentRule appointmentRule) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				appointmentRule.AppointmentRuleNum=Meth.GetLong(MethodBase.GetCurrentMethod(),appointmentRule);
				return appointmentRule.AppointmentRuleNum;
			}
			return Crud.AppointmentRuleCrud.Insert(appointmentRule);
		}

		///<summary></summary>
		public static void Update(AppointmentRule appointmentRule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),appointmentRule);
				return;
			}
			Crud.AppointmentRuleCrud.Update(appointmentRule);
		}

		///<summary></summary>
		public static void Delete(AppointmentRule rule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),rule);
				return;
			}
			string command="DELETE FROM appointmentrule" 
				+" WHERE AppointmentRuleNum = "+POut.Long(rule.AppointmentRuleNum);
 			Db.NonQ(command);
		}

		///<summary>Whenever an appointment is scheduled, the procedures which would be double booked are calculated.  In this method, those procedures are checked to see if the double booking should be blocked.  If double booking is indeed blocked, then a separate function will tell the user which category.</summary>
		public static bool IsBlocked(ArrayList codes){
			//No need to check RemotingRole; no call to db.
			for(int j=0;j<codes.Count;j++){
				for(int i=0;i<AppointmentRuleC.List.Length;i++){
					if(!AppointmentRuleC.List[i].IsEnabled){
						continue;
					}
					if(String.Compare((string)codes[j],AppointmentRuleC.List[i].CodeStart) < 0){
						continue;
					}
					if(String.Compare((string)codes[j],AppointmentRuleC.List[i].CodeEnd) > 0) {
						continue;
					}
					return true;
				}
			}
			return false;
		}

		///<summary>Whenever an appointment is blocked from being double booked, this method will tell the user which category.</summary>
		public static string GetBlockedDescription(ArrayList codes){
			//No need to check RemotingRole; no call to db.
			for(int j=0;j<codes.Count;j++) {
				for(int i=0;i<AppointmentRuleC.List.Length;i++) {
					if(!AppointmentRuleC.List[i].IsEnabled) {
						continue;
					}
					if(String.Compare((string)codes[j],AppointmentRuleC.List[i].CodeStart) < 0) {
						continue;
					}
					if(String.Compare((string)codes[j],AppointmentRuleC.List[i].CodeEnd) > 0) {
						continue;
					}
					return AppointmentRuleC.List[i].RuleDesc;
				}
			}
			return "";
		}

		
		

	}
	


}













