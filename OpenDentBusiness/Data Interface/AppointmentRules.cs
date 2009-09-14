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
			AppointmentRuleC.List=new AppointmentRule[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				AppointmentRuleC.List[i]=new AppointmentRule();
				AppointmentRuleC.List[i].AppointmentRuleNum = PIn.PLong(table.Rows[i][0].ToString());
				AppointmentRuleC.List[i].RuleDesc           = PIn.PString(table.Rows[i][1].ToString());
				AppointmentRuleC.List[i].CodeStart          = PIn.PString(table.Rows[i][2].ToString());
				AppointmentRuleC.List[i].CodeEnd            = PIn.PString(table.Rows[i][3].ToString());
				AppointmentRuleC.List[i].IsEnabled          = PIn.PBool(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(AppointmentRule rule) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				rule.AppointmentRuleNum=Meth.GetInt(MethodBase.GetCurrentMethod(),rule);
				return rule.AppointmentRuleNum;
			}
			if(PrefC.RandomKeys) {
				rule.AppointmentRuleNum=ReplicationServers.GetKey("appointmentrule","AppointmentRuleNum");
			}
			string command="INSERT INTO appointmentrule (";
			if(PrefC.RandomKeys) {
				command+="AppointmentRuleNum,";
			}
			command+="RuleDesc,CodeStart,CodeEnd,IsEnabled) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(rule.AppointmentRuleNum)+", ";
			}
			command+=
				 "'"+POut.PString(rule.RuleDesc)+"', "
				+"'"+POut.PString(rule.CodeStart)+"', "
				+"'"+POut.PString(rule.CodeEnd)+"', "
				+"'"+POut.PBool  (rule.IsEnabled)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				rule.AppointmentRuleNum=Db.NonQ(command,true);
			}
			return rule.AppointmentRuleNum;
		}

		///<summary></summary>
		public static void Update(AppointmentRule rule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),rule);
				return;
			}
			string command= "UPDATE appointmentrule SET " 
				+ "RuleDesc = '"      +POut.PString(rule.RuleDesc)+"'"
				+ ",CodeStart = '" +POut.PString(rule.CodeStart)+"'"
				+ ",CodeEnd = '"   +POut.PString(rule.CodeEnd)+"'"
				+ ",IsEnabled = '"    +POut.PBool  (rule.IsEnabled)+"'"
				+" WHERE AppointmentRuleNum = '" +POut.PLong   (rule.AppointmentRuleNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(AppointmentRule rule){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),rule);
				return;
			}
			string command="DELETE FROM appointmentrule" 
				+" WHERE AppointmentRuleNum = "+POut.PLong(rule.AppointmentRuleNum);
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













