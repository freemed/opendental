using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace OpenDentBusiness {
	public partial class ConvertDatabases {
		public static System.Version LatestVersion=new Version("14.1.0.0");//This value must be changed when a new conversion is to be triggered.

		///<summary>Oracle compatible: 07/11/2013</summary>
		private static void To13_2_1() {
			if(FromVersion<new Version("13.2.1.0")) {
				string command;
				//Add TaskEdit permission to everyone------------------------------------------------------
				command="SELECT DISTINCT UserGroupNum FROM grouppermission";
				DataTable table=Db.GetTable(command);
				long groupNum;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					for(int i=0;i<table.Rows.Count;i++) {
						groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
						command="INSERT INTO grouppermission (UserGroupNum,PermType) "
							+"VALUES("+POut.Long(groupNum)+",66)";//POut.Int((int)Permissions.TaskEdit)
						Db.NonQ(command);
					}
				}
				else {//oracle
					for(int i=0;i<table.Rows.Count;i++) {
						groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
						command="INSERT INTO grouppermission (GroupPermNum,NewerDays,UserGroupNum,PermType) "
							+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,"+POut.Long(groupNum)+",66)";//POut.Int((int)Permissions.TaskEdit)
						Db.NonQ(command);
					}
				}
				//add WikiListSetup permissions for users that have security admin------------------------------------------------------
				command="SELECT UserGroupNum FROM grouppermission WHERE PermType=24";//POut.Int((int)Permissions.SecurityAdmin)
				table=Db.GetTable(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					for(int i=0;i<table.Rows.Count;i++) {
						groupNum=PIn.Long(table.Rows[i][0].ToString());
						command="INSERT INTO grouppermission (NewerDate,UserGroupNum,PermType) "
						+"VALUES('0001-01-01',"+POut.Long(groupNum)+",67)";//POut.Int((int)Permissions.WikiListSetup);
						Db.NonQ32(command);
					}
				}
				else {//oracle
					for(int i=0;i<table.Rows.Count;i++) {
						groupNum=PIn.Long(table.Rows[i][0].ToString());
						command="INSERT INTO grouppermission (GroupPermNum,NewerDays,NewerDate,UserGroupNum,PermType) "
						+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),0,TO_DATE('0001-01-01','YYYY-MM-DD'),"+POut.Long(groupNum)+",67)";//POut.Int((int)Permissions.WikiListSetup)
						Db.NonQ32(command);
					}
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('PatientPortalURL','')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'PatientPortalURL','')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE procedurelog ADD BillingNote varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE procedurelog ADD BillingNote varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE procedurelog ADD RepeatChargeNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE procedurelog ADD INDEX (RepeatChargeNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE procedurelog ADD RepeatChargeNum number(20)";
					Db.NonQ(command);
					command="UPDATE procedurelog SET RepeatChargeNum = 0 WHERE RepeatChargeNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE procedurelog MODIFY RepeatChargeNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX procedurelog_RepeatChargeNum ON procedurelog (RepeatChargeNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS reseller";
					Db.NonQ(command);
					command=@"CREATE TABLE reseller (
						ResellerNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						UserName varchar(255) NOT NULL,
						ResellerPassword varchar(255) NOT NULL,
						INDEX(PatNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE reseller'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE reseller (
						ResellerNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						UserName varchar2(255),
						ResellerPassword varchar2(255),
						CONSTRAINT reseller_ResellerNum PRIMARY KEY (ResellerNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX reseller_PatNum ON reseller (PatNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS resellerservice";
					Db.NonQ(command);
					command=@"CREATE TABLE resellerservice (
						ResellerServiceNum bigint NOT NULL auto_increment PRIMARY KEY,
						ResellerNum bigint NOT NULL,
						CodeNum bigint NOT NULL,
						Fee double NOT NULL,
						INDEX(ResellerNum),
						INDEX(CodeNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE resellerservice'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE resellerservice (
						ResellerServiceNum number(20) NOT NULL,
						ResellerNum number(20) NOT NULL,
						CodeNum number(20) NOT NULL,
						Fee number(38,8) NOT NULL,
						CONSTRAINT resellerservice_ResellerServic PRIMARY KEY (ResellerServiceNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX resellerservice_ResellerNum ON resellerservice (ResellerNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX resellerservice_CodeNum ON resellerservice (CodeNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE registrationkey ADD IsResellerCustomer tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE registrationkey ADD IsResellerCustomer number(3)";
					Db.NonQ(command);
					command="UPDATE registrationkey SET IsResellerCustomer = 0 WHERE IsResellerCustomer IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE registrationkey MODIFY IsResellerCustomer NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE repeatcharge ADD CopyNoteToProc tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE repeatcharge ADD CopyNoteToProc number(3)";
					Db.NonQ(command);
					command="UPDATE repeatcharge SET CopyNoteToProc = 0 WHERE CopyNoteToProc IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE repeatcharge MODIFY CopyNoteToProc NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS xchargetransaction";
					Db.NonQ(command);
					command=@"CREATE TABLE xchargetransaction (
						XChargeTransactionNum bigint NOT NULL auto_increment PRIMARY KEY,
						TransType varchar(255) NOT NULL,
						Amount double NOT NULL,
						CCEntry varchar(255) NOT NULL,
						PatNum bigint NOT NULL,
						Result varchar(255) NOT NULL,
						ClerkID varchar(255) NOT NULL,
						ResultCode varchar(255) NOT NULL,
						Expiration varchar(255) NOT NULL,
						CCType varchar(255) NOT NULL,
						CreditCardNum varchar(255) NOT NULL,
						BatchNum varchar(255) NOT NULL,
						ItemNum varchar(255) NOT NULL,
						ApprCode varchar(255) NOT NULL,
						TransactionDateTime datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						INDEX(PatNum),
						INDEX(TransactionDateTime)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE xchargetransaction'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE xchargetransaction (
						XChargeTransactionNum number(20) NOT NULL,
						TransType varchar2(255),
						Amount number(38,8) NOT NULL,
						CCEntry varchar2(255),
						PatNum number(20) NOT NULL,
						Result varchar2(255),
						ClerkID varchar2(255),
						ResultCode varchar2(255),
						Expiration varchar2(255),
						CCType varchar2(255),
						CreditCardNum varchar2(255),
						BatchNum varchar2(255),
						ItemNum varchar2(255),
						ApprCode varchar2(255),
						TransactionDateTime date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						CONSTRAINT xchargetransaction_XChargeTran PRIMARY KEY (XChargeTransactionNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX xchargetransaction_PatNum ON xchargetransaction (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX xchargetransaction_Transaction ON xchargetransaction (TransactionDateTime)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('CanadaODAMemberNumber','')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'CanadaODAMemberNumber','')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('CanadaODAMemberPass','')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'CanadaODAMemberPass','')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE patient ADD SmokingSnoMed varchar(32) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE patient ADD SmokingSnoMed varchar2(32)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE repeatcharge ADD CreatesClaim tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE repeatcharge ADD CreatesClaim number(3)";
					Db.NonQ(command);
					command="UPDATE repeatcharge SET CreatesClaim = 0 WHERE CreatesClaim IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE repeatcharge MODIFY CreatesClaim NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE repeatcharge ADD IsEnabled tinyint NOT NULL";
					Db.NonQ(command);
					command="UPDATE repeatcharge SET IsEnabled = 1";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE repeatcharge ADD IsEnabled number(3)";
					Db.NonQ(command);
					//command="UPDATE repeatcharge SET IsEnabled = 0 WHERE IsEnabled IS NULL";
					command="UPDATE repeatcharge SET IsEnabled = 1 WHERE IsEnabled IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE repeatcharge MODIFY IsEnabled NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE rxalert ADD IsHighSignificance tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE rxalert ADD IsHighSignificance number(3)";
					Db.NonQ(command);
					command="UPDATE rxalert SET IsHighSignificance = 0 WHERE IsHighSignificance IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE rxalert MODIFY IsHighSignificance NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('EhrRxAlertHighSeverity','0')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'EhrRxAlertHighSeverity','0')";
					Db.NonQ(command);
				}
				//Oracle compatible
				command="UPDATE patient SET SmokingSnoMed='449868002' WHERE SmokeStatus=5";//CurrentEveryDay
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='428041000124106' WHERE SmokeStatus=4";//CurrentSomeDay
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='8517006' WHERE SmokeStatus=3";//FormerSmoker
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='266919005' WHERE SmokeStatus=2";//NeverSmoked
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='77176002' WHERE SmokeStatus=1";//SmokerUnknownCurrent
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='266927001' WHERE SmokeStatus=0";//UnknownIfEver
				Db.NonQ(command);
				command="ALTER TABLE patient DROP COLUMN SmokeStatus";
				Db.NonQ(command);
				//Add ICD9Code to DiseaseDef and update eduresource and disease to use DiseaseDefNum instead of ICD9Num----------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE diseasedef ADD ICD9Code varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE diseasedef ADD ICD9Code varchar2(255)";
					Db.NonQ(command);
				}
				//command="SELECT DISTINCT Description,ICD9Code,icd9.ICD9Num "
				//  +"FROM icd9,eduresource,disease,reminderrule "
				//  +"WHERE icd9.ICD9Num=eduresource.ICD9Num "
				//  +"OR icd9.ICD9Num=disease.ICD9Num "
				//  +"OR (ReminderCriterion=6 AND icd9.ICD9Num=CriterionFK)";//6=ICD9
				//table=Db.GetTable(command);
				List<string> listDescription=new List<string>();
				List<string> listICD9Code=new List<string>();
				List<long> listICD9Num=new List<long>();
				command="SELECT DISTINCT icd9.Description,icd9.ICD9Code,icd9.ICD9Num "
					+"FROM icd9,eduresource "
					+"WHERE icd9.ICD9Num=eduresource.ICD9Num";
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					listDescription.Add(PIn.String(table.Rows[i]["Description"].ToString()));
					listICD9Code.Add(PIn.String(table.Rows[i]["ICD9Code"].ToString()));
					listICD9Num.Add(PIn.Long(table.Rows[i]["ICD9Num"].ToString()));
				}
				command="SELECT DISTINCT Description,ICD9Code,icd9.ICD9Num "
					+"FROM icd9,disease "
					+"WHERE icd9.ICD9Num=disease.ICD9Num ";//6=ICD9
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					if(listICD9Num.Contains(PIn.Long(table.Rows[i]["ICD9Num"].ToString()))) {
						continue;
					}
					listDescription.Add(PIn.String(table.Rows[i]["Description"].ToString()));
					listICD9Code.Add(PIn.String(table.Rows[i]["ICD9Code"].ToString()));
					listICD9Num.Add(PIn.Long(table.Rows[i]["ICD9Num"].ToString()));
				}
				command="SELECT DISTINCT Description,ICD9Code,icd9.ICD9Num "
					+"FROM icd9,reminderrule "
					+"WHERE (ReminderCriterion=6 AND icd9.ICD9Num=CriterionFK)";//6=ICD9
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					if(listICD9Num.Contains(PIn.Long(table.Rows[i]["ICD9Num"].ToString()))) {
						continue;
					}
					listDescription.Add(PIn.String(table.Rows[i]["Description"].ToString()));
					listICD9Code.Add(PIn.String(table.Rows[i]["ICD9Code"].ToString()));
					listICD9Num.Add(PIn.Long(table.Rows[i]["ICD9Num"].ToString()));
				}
				command="SELECT MAX(ItemOrder) FROM diseasedef";
				int itemOrderCur=PIn.Int(Db.GetScalar(command));
				//for(int i=0;i<table.Rows.Count;i++) {
				//  itemOrderCur++;
				//  if(DataConnection.DBtype==DatabaseType.MySql) {
				//    command="INSERT INTO diseasedef(DiseaseName,ItemOrder,ICD9Code) VALUES('"
				//      +POut.String(table.Rows[i]["Description"].ToString())+"',"+POut.Int(itemOrderCur)+",'"+POut.String(table.Rows[i]["ICD9Code"].ToString())+"')";
				//  }
				//  else {//oracle
				//    command="INSERT INTO diseasedef(DiseaseDefNum,DiseaseName,ItemOrder,ICD9Code) VALUES((SELECT MAX(DiseaseDefNum)+1 FROM diseasedef),'"
				//      +POut.String(table.Rows[i]["Description"].ToString())+"',"+POut.Int(itemOrderCur)+",'"+POut.String(table.Rows[i]["ICD9Code"].ToString())+"')";
				//  }
				//  long defNum=Db.NonQ(command,true);
				//  command="UPDATE eduresource SET DiseaseDefNum="+POut.Long(defNum)+" WHERE ICD9Num="+table.Rows[i]["ICD9Num"].ToString();
				//  Db.NonQ(command);
				//  command="UPDATE disease SET DiseaseDefNum="+POut.Long(defNum)+" WHERE ICD9Num="+table.Rows[i]["ICD9Num"].ToString();
				//  Db.NonQ(command);
				//  command="UPDATE reminderrule SET CriterionFK="+POut.Long(defNum)+" WHERE CriterionFK="+table.Rows[i]["ICD9Num"].ToString()+" AND ReminderCriterion=6";
				//  Db.NonQ(command);
				//}
				for(int i=0;i<listICD9Num.Count;i++) {
					itemOrderCur++;
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="INSERT INTO diseasedef(DiseaseName,ItemOrder,ICD9Code) VALUES('"
							+POut.String(listDescription[i])+"',"+POut.Int(itemOrderCur)+",'"+POut.String(listICD9Code[i])+"')";
					}
					else {//oracle
						command="INSERT INTO diseasedef(DiseaseDefNum,DiseaseName,ItemOrder,ICD9Code) VALUES((SELECT MAX(DiseaseDefNum)+1 FROM diseasedef),'"
							+POut.String(listDescription[i])+"',"+POut.Int(itemOrderCur)+",'"+POut.String(listICD9Code[i])+"')";
					}
					long defNum=Db.NonQ(command,true);
					command="UPDATE eduresource SET DiseaseDefNum="+POut.Long(defNum)+" WHERE ICD9Num="+POut.Long(listICD9Num[i]);
					Db.NonQ(command);
					command="UPDATE disease SET DiseaseDefNum="+POut.Long(defNum)+" WHERE ICD9Num="+POut.Long(listICD9Num[i]);
					Db.NonQ(command);
					command="UPDATE reminderrule SET CriterionFK="+POut.Long(defNum)+" WHERE CriterionFK="+POut.Long(listICD9Num[i])+" AND ReminderCriterion=6";
					Db.NonQ(command);
				}
				command="ALTER TABLE eduresource DROP COLUMN ICD9Num";
				Db.NonQ(command);
				command="ALTER TABLE disease DROP COLUMN ICD9Num";
				Db.NonQ(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE diseasedef ADD SnomedCode varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE diseasedef ADD SnomedCode varchar2(255)";
					Db.NonQ(command);
				}
				//Update reminderrule.ReminderCriterion - set ICD9 (6) to Problem (0)------------------------------------------------------------------------------------
				command="UPDATE reminderrule SET ReminderCriterion=0 WHERE ReminderCriterion=6";
				Db.NonQ(command);
				//Update patientrace-------------------------------------------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('LanguagesIndicateNone','Declined to Specify')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'LanguagesIndicateNone','Declined to Specify')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS patientrace";
					Db.NonQ(command);
					command=@"CREATE TABLE patientrace (
						PatientRaceNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						Race tinyint NOT NULL,
						INDEX(PatNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE patientrace'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE patientrace (
						PatientRaceNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						Race number(3) NOT NULL,
						CONSTRAINT patientrace_PatientRaceNum PRIMARY KEY (PatientRaceNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX patientrace_PatNum ON patientrace (PatNum)";
					Db.NonQ(command);
				}
				//Create Custom Language "DeclinedToSpecify" ----------------------------------------------------------------------------------------------------------
				command="SELECT ValueString FROM preference WHERE PrefName = 'LanguagesUsedByPatients'";
				string valueString=Db.GetScalar(command);
				command="UPDATE preference SET ValueString='"+(POut.String(valueString)+",Declined to Specify").Trim(',')+"'"//trim ,(comma) off
					+" WHERE PrefName='LanguagesUsedByPatients'";
				Db.NonQ(command);
				//update Race and Ethnicity for EHR.---------------------------------------------------------------------------------------------------------------------
				//Get a list of patients that have a race set.
				command="SELECT PatNum, Race FROM patient WHERE Race!=0";
				table=Db.GetTable(command);
				string maxPkStr="1";//Used for Orcale.  Oracle has to insert the first row manually setting the PK to 1.
				for(int i=0;i<table.Rows.Count;i++) {
					string patNum=table.Rows[i]["PatNum"].ToString();
					switch(PIn.Int(table.Rows[i]["Race"].ToString())) {//PatientRaceOld
						case 0://PatientRaceOld.Unknown
							//Do nothing.  No entry means "Unknown", the old default.
							continue;
						case 1://PatientRaceOld.Multiracial
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",7)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",7)";
							}
							break;
						case 2://PatientRaceOld.HispanicLatino
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",9)";
								Db.NonQ(command);
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",6)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",9)";
								Db.NonQ(command);
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ((SELECT MAX(PatientRaceNum+1) FROM patientrace),"+patNum+",6)";
							}
							break;
						case 3://PatientRaceOld.AfricanAmerican
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",1)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",1)";
							}
							break;
						case 4://PatientRaceOld.White
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",9)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",9)";
							}
							break;
						case 5://PatientRaceOld.HawaiiOrPacIsland
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",5)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",5)";
							}
							break;
						case 6://PatientRaceOld.AmericanIndian
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",2)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",2)";
							}
							break;
						case 7://PatientRaceOld.Asian
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",3)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",3)";
							}
							break;
						case 8://PatientRaceOld.Other
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",8)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",8)";
							}
							break;
						case 9://PatientRaceOld.Aboriginal
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",0)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",0)";
							}
							break;
						case 10://PatientRaceOld.BlackHispanic
							if(DataConnection.DBtype==DatabaseType.MySql) {
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",1)";
								Db.NonQ(command);
								command="INSERT INTO patientrace (PatNum,Race) VALUES ("+patNum+",6)";
							}
							else {//oracle
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ("+maxPkStr+","+patNum+",1)";
								Db.NonQ(command);
								command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race) VALUES ((SELECT MAX(PatientRaceNum+1) FROM patientrace),"+patNum+",6)";
							}
							break;
						default:
							//should never happen, useful for debugging.
							continue;
					}//end switch
					Db.NonQ(command);
					if(DataConnection.DBtype==DatabaseType.Oracle && maxPkStr=="1") {
						//At least one row has been entered.  Set the pk string to the auto-increment SQL for Oracle.
						maxPkStr="(SELECT MAX(PatientRaceNum+1) FROM patientrace)";
					}
				}
				//Apex clearinghouse.
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command=@"INSERT INTO clearinghouse(Description,ExportPath,Payors,Eformat,ISA05,SenderTin,ISA07,ISA08,ISA15,Password,ResponsePath,CommBridge,ClientProgram,
						LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03,ISA02,ISA04,ISA16,SeparatorData,SeparatorSegment) 
						VALUES ('Apex','"+POut.String(@"C:\ONETOUCH\")+"','','5','ZZ','870578776','ZZ','99999','P','','','0','',0,0,'','Apex','8008409152','99999','','','','','')";
					Db.NonQ(command);
				}
				else {//oracle
					command=@"INSERT INTO clearinghouse(ClearinghouseNum,Description,ExportPath,Payors,Eformat,ISA05,SenderTin,ISA07,ISA08,ISA15,Password,ResponsePath,CommBridge,ClientProgram,
						LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03,ISA02,ISA04,ISA16,SeparatorData,SeparatorSegment) 
						VALUES ((SELECT MAX(ClearinghouseNum+1) FROM clearinghouse),'Apex','"+POut.String(@"C:\ONETOUCH\")+"','','5','ZZ','870578776','ZZ','99999','P','','','0','',0,0,'','Apex','8008409152','99999','','','','','')";
					Db.NonQ(command);
				}
				//Insert Guru Bridge
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
				    +") VALUES("
				    +"'Guru', "
				    +"'Guru from guru.waziweb.com', "
				    +"'0', "
				    +"'',"
				    +"'', "
				    +"'')";
					long programNum=Db.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"'"+POut.Long(programNum)+"', "
				    +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
				    +"'0')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"'"+POut.Long(programNum)+"', "
				    +"'Guru image path', "
				    +"'C:\')";
					Db.NonQ(command);
					command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
				    +"VALUES ("
				    +"'"+POut.Long(programNum)+"', "
				    +"'2', "//ToolBarsAvail.ChartModule
				    +"'Guru')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
				    +") VALUES("
				    +"(SELECT MAX(ProgramNum)+1 FROM program),"
				    +"'Guru', "
				    +"'Guru from guru.waziweb.com/', "
				    +"'0', "
				    +"'',"
				    +"'', "
				    +"'')";
					long programNum=Db.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"(SELECT MAX(ProgramPropertyNum+1) FROM programproperty),"
				    +"'"+POut.Long(programNum)+"', "
				    +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
				    +"'0')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"(SELECT MAX(ProgramPropertyNum+1) FROM programproperty),"
				    +"'"+POut.Long(programNum)+"', "
				    +"'Guru image path', "
				    +"'C:\')";
					Db.NonQ(command);
					command="INSERT INTO toolbutitem (ToolButItemNum,ProgramNum,ToolBar,ButtonText) "
				    +"VALUES ("
				    +"(SELECT MAX(ToolButItemNum)+1 FROM toolbutitem),"
				    +"'"+POut.Long(programNum)+"', "
				    +"'2', "//ToolBarsAvail.ChartModule
				    +"'Guru')";
					Db.NonQ(command);
				}//end Guru bridge
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('NewCropPartnerName','')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'NewCropPartnerName','')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailaddress ADD SMTPserverIncoming varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE emailaddress ADD SMTPserverIncoming varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailaddress ADD ServerPortIncoming int NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE emailaddress ADD ServerPortIncoming number(11)";
					Db.NonQ(command);
					command="UPDATE emailaddress SET ServerPortIncoming = 0 WHERE ServerPortIncoming IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE emailaddress MODIFY ServerPortIncoming NOT NULL";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.2.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_2_2();
		}

		///<summary>Oracle compatible: 07/11/2013</summary>
		private static void To13_2_2() {
			if(FromVersion<new Version("13.2.2.0")) {
				string command;
				//Convert languages in the LanguagesUsedByPatients preference from ISO639-1 to ISO639-2 for languages which are not custom.
				command="SELECT ValueString FROM preference WHERE PrefName='LanguagesUsedByPatients'";
				string strLanguageList=PIn.String(Db.GetScalar(command));
				if(strLanguageList!="") {
					StringBuilder sb=new StringBuilder();
					string[] lanstring=strLanguageList.Split(',');
					for(int i=0;i<lanstring.Length;i++) {
						if(lanstring[i]=="") {
							continue;
						}
						if(sb.Length>0) {
							sb.Append(",");
						}
						try {
							sb.Append(CultureInfo.GetCultureInfo(lanstring[i]).ThreeLetterISOLanguageName);
						}
						catch {//custom language
							sb.Append(lanstring[i]);
						}
					}
					command="UPDATE preference SET ValueString='"+POut.String(sb.ToString())+"' WHERE PrefName='LanguagesUsedByPatients'";
					Db.NonQ(command);
				}
				//Convert languages in the patient.Langauge column from ISO639-1 to ISO639-2 for languages which are not custom.
				command="SELECT PatNum,Language FROM patient WHERE Language<>'' AND Language IS NOT NULL";
				DataTable tablePatLanguages=Db.GetTable(command);
				for(int i=0;i<tablePatLanguages.Rows.Count;i++) {
					string lang=PIn.String(tablePatLanguages.Rows[i]["Language"].ToString());
					try {
						lang=CultureInfo.GetCultureInfo(lang).ThreeLetterISOLanguageName;
						long patNum=PIn.Long(tablePatLanguages.Rows[i]["PatNum"].ToString());
						command="UPDATE patient SET Language='"+POut.String(lang)+"' WHERE PatNum="+POut.Long(patNum);
						Db.NonQ(command);
					}
					catch {//Custom language
						//Do not modify.
					}
				}
				command="UPDATE preference SET ValueString = '13.2.2.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_2_4();
		}

		private static void To13_2_4() {
			if(FromVersion<new Version("13.2.4.0")) {
				//This fixes a bug in the conversion script above at lines 324 and 328
				string command;
				command="SELECT DiseaseDefNum,DiseaseName,ICD9Code,SnomedCode FROM diseasedef ORDER BY DiseaseDefNum ASC";
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {//compare each row (i)
					for(int j=i+1;j<table.Rows.Count;j++) {//with every row below it
						if(PIn.String(table.Rows[i]["DiseaseName"].ToString())!=PIn.String(table.Rows[j]["DiseaseName"].ToString())
							|| PIn.String(table.Rows[i]["ICD9Code"].ToString())!=PIn.String(table.Rows[j]["ICD9Code"].ToString())
							|| PIn.String(table.Rows[i]["SnomedCode"].ToString())!=PIn.String(table.Rows[j]["SnomedCode"].ToString())) 
						{
							continue;
						}
						//row i and row j are "identical".  Because DiseaseDefNum is ascending, we want to keep row j, not row i.
						//Always use POut when entering data into the database. Jordan ok'd omitting it here for readability. Do not use this as an example.
						//The queries below will probably not make any changes.  Just if they used this part of the program heavily after the 
						command="UPDATE eduresource SET DiseaseDefNum="+table.Rows[j]["DiseaseDefNum"].ToString()+" WHERE DiseaseDefNum="+table.Rows[i]["DiseaseDefNum"].ToString();
						Db.NonQ(command);
						command="UPDATE disease SET DiseaseDefNum="+table.Rows[j]["DiseaseDefNum"].ToString()+" WHERE DiseaseDefNum="+table.Rows[i]["DiseaseDefNum"].ToString();
						Db.NonQ(command);
						command="UPDATE reminderrule SET CriterionFK="+table.Rows[j]["DiseaseDefNum"].ToString()+" WHERE CriterionFK="+table.Rows[i]["DiseaseDefNum"].ToString()+" AND ReminderCriterion=6";
						Db.NonQ(command);
						command="DELETE FROM diseasedef WHERE DiseaseDefNum="+table.Rows[i]["DiseaseDefNum"].ToString();
						Db.NonQ(command);
					}
				}
				command="UPDATE preference SET ValueString = '13.2.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_2_7();
		}

		private static void To13_2_7() {
			if(FromVersion<new Version("13.2.7.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE patient ADD Country varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE patient ADD Country varchar2(255)";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.2.7.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_2_16();
		}

		private static void To13_2_16() {
			if(FromVersion<new Version("13.2.16.0")) {
				string command;
				//Get the 1500 claim form primary key. The unique ID is OD9.
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="SELECT ClaimFormNum FROM claimform WHERE UniqueID='OD9' LIMIT 1";
				}
				else {//oracle doesn't have LIMIT
					command="SELECT * FROM (SELECT ClaimFormNum FROM claimform WHERE UniqueID='OD9') WHERE RowNum<=1";
				}
				DataTable tableClaimFormNum=Db.GetTable(command);
				if(tableClaimFormNum.Rows.Count>0) {//The claim form should exist, but might not if foreign.
					long claimFormNum=PIn.Long(Db.GetScalar(command));
					//Change the form facility address from the pay to address to the treating address.  The pay to address still shows under the billing section of the 1500 claim form.
					command="UPDATE claimformitem SET FieldName='TreatingDentistAddress' WHERE claimformnum="+POut.Long(claimFormNum)+" AND FieldName='PayToDentistAddress' AND XPos<400";
					Db.NonQ(command);
					command="UPDATE claimformitem SET FieldName='TreatingDentistCity' WHERE claimformnum="+POut.Long(claimFormNum)+" AND FieldName='PayToDentistCity' AND XPos<470";
					Db.NonQ(command);
					command="UPDATE claimformitem SET FieldName='TreatingDentistST' WHERE claimformnum="+POut.Long(claimFormNum)+" AND FieldName='PayToDentistST' AND XPos<500";
					Db.NonQ(command);
					command="UPDATE claimformitem SET FieldName='TreatingDentistZip' WHERE claimformnum="+POut.Long(claimFormNum)+" AND FieldName='PayToDentistZip' AND XPos<520";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.2.16.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_2_22();
		}

		private static void To13_2_22() {
			if(FromVersion<new Version("13.2.22.0")) {
				string command;
				//Moving codes to the Obsolete category that were deleted in CDT 2014.
				if(CultureInfo.CurrentCulture.Name.EndsWith("US")) {//United States
					//Move depricated codes to the Obsolete procedure code category.
					//Make sure the procedure code category exists before moving the procedure codes.
					string procCatDescript="Obsolete";
					long defNum=0;
					command="SELECT DefNum FROM definition WHERE Category=11 AND ItemName='"+POut.String(procCatDescript)+"'";//11 is DefCat.ProcCodeCats
					DataTable dtDef=Db.GetTable(command);
					if(dtDef.Rows.Count==0) { //The procedure code category does not exist, add it
						if(DataConnection.DBtype==DatabaseType.MySql) {
							command="INSERT INTO definition (Category,ItemName,ItemOrder) "
									+"VALUES (11"+",'"+POut.String(procCatDescript)+"',"+POut.Long(DefC.Long[11].Length)+")";//11 is DefCat.ProcCodeCats
						}
						else {//oracle
							command="INSERT INTO definition (DefNum,Category,ItemName,ItemOrder) "
									+"VALUES ((SELECT MAX(DefNum)+1 FROM definition),11,'"+POut.String(procCatDescript)+"',"+POut.Long(DefC.Long[11].Length)+")";//11 is DefCat.ProcCodeCats
						}
						defNum=Db.NonQ(command,true);
					}
					else { //The procedure code category already exists, get the existing defnum
						defNum=PIn.Long(dtDef.Rows[0]["DefNum"].ToString());
					}
					string[] cdtCodesDeleted=new string[] {
						"D0363","D3354","D5860","D5861"
					};
					for(int i=0;i<cdtCodesDeleted.Length;i++) {
						string procCode=cdtCodesDeleted[i];
						command="SELECT CodeNum FROM procedurecode WHERE ProcCode='"+POut.String(procCode)+"'";
						DataTable dtProcCode=Db.GetTable(command);
						if(dtProcCode.Rows.Count==0) { //The procedure code does not exist in this database.
							continue;//Do not try to move it.
						}
						long codeNum=PIn.Long(dtProcCode.Rows[0]["CodeNum"].ToString());
						command="UPDATE procedurecode SET ProcCat="+POut.Long(defNum)+" WHERE CodeNum="+POut.Long(codeNum);
						Db.NonQ(command);
					}
				}//end United States update
				command="UPDATE preference SET ValueString = '13.2.22.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_2_27();
		}

		private static void To13_2_27() {
			if(FromVersion<new Version("13.2.27.0")) {
				string command;
				//Insert DentalStudio Bridge
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
				    +") VALUES("
				    +"'DentalStudio', "
				    +"'DentalStudio from www.villasm.com', "
				    +"'0', "
				    +"'"+POut.String(@"C:\Program Files (x86)\DentalStudioPlus\AutoStartup.exe")+"',"
				    +"'', "
				    +"'')";
					long programNum=Db.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"'"+POut.Long(programNum)+"', "
				    +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
				    +"'0')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"'"+POut.Long(programNum)+"', "
				    +"'UserName (clear to use OD username)', "
				    +"'Admin')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"'"+POut.Long(programNum)+"', "
				    +"'UserPassword', "
				    +"'12345678')";
					Db.NonQ(command);
					command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
				    +"VALUES ("
				    +"'"+POut.Long(programNum)+"', "
				    +"'"+POut.Int(((int)ToolBarsAvail.ChartModule))+"', "
				    +"'DentalStudio')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
				    +") VALUES("
				    +"(SELECT MAX(ProgramNum)+1 FROM program),"
				    +"'DentalStudio', "
				    +"'DentalStudio from www.villasm.com', "
				    +"'0', "
				    +"'"+POut.String(@"C:\Program Files (x86)\DentalStudioPlus\AutoStartup.exe")+"',"
				    +"'', "
				    +"'')";
					long programNum=Db.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"(SELECT MAX(ProgramPropertyNum+1) FROM programproperty),"
				    +"'"+POut.Long(programNum)+"', "
				    +"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
				    +"'0')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"(SELECT MAX(ProgramPropertyNum+1) FROM programproperty),"
				    +"'"+POut.Long(programNum)+"', "
				    +"'UserName (clear to use OD username)', "
				    +"'Admin')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramPropertyNum,ProgramNum,PropertyDesc,PropertyValue"
				    +") VALUES("
				    +"(SELECT MAX(ProgramPropertyNum+1) FROM programproperty),"
				    +"'"+POut.Long(programNum)+"', "
				    +"'UserPassword', "
				    +"'12345678')";
					Db.NonQ(command);
					command="INSERT INTO toolbutitem (ToolButItemNum,ProgramNum,ToolBar,ButtonText) "
				    +"VALUES ("
				    +"(SELECT MAX(ToolButItemNum)+1 FROM toolbutitem),"
				    +"'"+POut.Long(programNum)+"', "
				    +"'"+POut.Int(((int)ToolBarsAvail.ChartModule))+"', "
				    +"'DentalStudio')";
					Db.NonQ(command);
				}//end DentalStudio bridge
				command="UPDATE preference SET ValueString = '13.2.27.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_3_1();
		}

		private static void To13_3_1() {
			if(FromVersion<new Version("13.3.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('EmailInboxComputerName','')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'EmailInboxComputerName','')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('EmailInboxCheckInterval','5')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'EmailInboxCheckInterval','5')";
					Db.NonQ(command);
				}
				//Add Family Health table for EHR A.13 (Family Health History)
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS familyhealth";
					Db.NonQ(command);
					command=@"CREATE TABLE familyhealth (
						FamilyHealthNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						Relationship tinyint NOT NULL,
						DiseaseDefNum bigint NOT NULL,
						PersonName varchar(255) NOT NULL,
						INDEX(PatNum),
						INDEX(DiseaseDefNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE familyhealth'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE familyhealth (
						FamilyHealthNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						Relationship number(3) NOT NULL,
						DiseaseDefNum number(20) NOT NULL,
						PersonName varchar2(255),
						CONSTRAINT familyhealth_FamilyHealthNum PRIMARY KEY (FamilyHealthNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX familyhealth_PatNum ON familyhealth (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX familyhealth_DiseaseDefNum ON familyhealth (DiseaseDefNum)";
					Db.NonQ(command);
				}
				//Add securityloghash table for EHR D.2
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS securityloghash";
					Db.NonQ(command);
					command=@"CREATE TABLE securityloghash (
						SecurityLogHashNum bigint NOT NULL auto_increment PRIMARY KEY,
						SecurityLogNum bigint NOT NULL,
						LogHash varchar(255) NOT NULL,
						INDEX(SecurityLogNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE securityloghash'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE securityloghash (
						SecurityLogHashNum number(20) NOT NULL,
						SecurityLogNum number(20) NOT NULL,
						LogHash varchar2(255),
						CONSTRAINT securityloghash_SecurityLogHas PRIMARY KEY (SecurityLogHashNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX securityloghash_SecurityLogNum ON securityloghash (SecurityLogNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailmessage CHANGE BodyText BodyText LONGTEXT NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					//Changing a column's datatype from VARCHAR2 to clob yields the following error in oracle:  ORA-22858: invalid alteration of datatype
					//The easiest way to get change the datatype from VARCHAR2 to clob is to create a temp column then rename it.
					command="ALTER TABLE emailmessage ADD (BodyTextClob clob NOT NULL)";
					Db.NonQ(command);
					command="UPDATE emailmessage SET BodyTextClob=BodyText";
					Db.NonQ(command);
					command="ALTER TABLE emailmessage DROP COLUMN BodyText";
					Db.NonQ(command);
					command="ALTER TABLE emailmessage RENAME COLUMN BodyTextClob TO BodyText";
					Db.NonQ(command);
				}
				//Electronic Dental Services (EDS) clearinghouse.
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command=@"INSERT INTO clearinghouse(Description,ExportPath,Payors,Eformat,ISA05,SenderTin,ISA07,ISA08,ISA15,Password,ResponsePath,CommBridge,
					ClientProgram,LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03,ISA02,ISA04,ISA16,SeparatorData,SeparatorSegment) 
					VALUES ('Electronic Dental Services','"+POut.String(@"C:\EDS\Claims\In\")+"','','1','ZZ','','ZZ','EDS','P','','','0','"+POut.String(@"C:\Program Files\EDS\edsbridge.exe")+"',0,0,'','','','EDS','','','','','')";
					Db.NonQ(command);
				}
				else {//oracle
					command=@"INSERT INTO clearinghouse (ClearinghouseNum,Description,ExportPath,Payors,Eformat,ISA05,SenderTin,ISA07,ISA08,ISA15,Password,ResponsePath,CommBridge,ClientProgram,
					LastBatchNumber,ModemPort,LoginID,SenderName,SenderTelephone,GS03,ISA02,ISA04,ISA16,SeparatorData,SeparatorSegment) 
					VALUES ((SELECT MAX(ClearinghouseNum+1) FROM clearinghouse),'Electronic Dental Services','"+POut.String(@"C:\EDS\Claims\In\")+"','','1','ZZ','','ZZ','EDS','P','','','0','"+POut.String(@"C:\Program Files\EDS\edsbridge.exe")+"',0,0,'','','','EDS','','','','','')";
					Db.NonQ(command);
				}
				//codesystem
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS codesystem";
					Db.NonQ(command);
					command=@"CREATE TABLE codesystem (
						CodeSystemNum bigint NOT NULL auto_increment PRIMARY KEY,
						CodeSystemName varchar(255) NOT NULL,
						VersionCur varchar(255) NOT NULL,
						VersionAvail varchar(255) NOT NULL,
						HL7OID varchar(255) NOT NULL,
						Note varchar(255) NOT NULL,
						INDEX(CodeSystemName)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE codesystem'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE codesystem (
						CodeSystemNum number(20) NOT NULL,
						CodeSystemName varchar2(255),
						VersionCur varchar2(255),
						VersionAvail varchar2(255),
						HL7OID varchar2(255),
						Note varchar2(255),
						CONSTRAINT codesystem_CodeSystemNum PRIMARY KEY (CodeSystemNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX codesystem_CodeSystemName ON codesystem (CodeSystemName)";
					Db.NonQ(command);
				}
				//No need for mysql/oracle split
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID,VersionAvail,VersionCur,Note) VALUES (1,'AdministrativeSex','2.16.840.1.113883.18.2','HL7v2.5','HL7v2.5','')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (2,'CDCREC','2.16.840.1.113883.6.238')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (3,'CDT','2.16.840.1.113883.6.13')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (4,'CPT','2.16.840.1.113883.6.12')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (5,'CVX','2.16.840.1.113883.12.292')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (6,'HCPCS','2.16.840.1.113883.6.285')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (7,'ICD10CM','2.16.840.1.113883.6.90')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (8,'ICD9CM','2.16.840.1.113883.6.103')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (9,'LOINC','2.16.840.1.113883.6.1')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (10,'RXNORM','2.16.840.1.113883.6.88')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (11,'SNOMEDCT','2.16.840.1.113883.6.96')";
				Db.NonQ(command);
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (12,'SOP','2.16.840.1.113883.3.221.5')";
				Db.NonQ(command);
#region Create Code Systems Tables
				//CDCREC (CDC Race and Ethnicity)-------------------------------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS cdcrec";
					Db.NonQ(command);
					command=@"CREATE TABLE cdcrec (
						CdcrecNum bigint NOT NULL auto_increment PRIMARY KEY,
						CdcrecCode varchar(255) NOT NULL,
						HeirarchicalCode varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						INDEX(CdcrecCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE cdcrec'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE cdcrec (
						CdcrecNum number(20) NOT NULL,
						CdcrecCode varchar2(255),
						HeirarchicalCode varchar2(255),
						Description varchar2(255),
						CONSTRAINT cdcrec_CdcrecNum PRIMARY KEY (CdcrecNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX cdcrec_CdcrecCode ON cdcrec (CdcrecCode)";
					Db.NonQ(command);
				}
				//CDT ----------------------------------------------------------------------------------------------------------------------------------------------------
				//Not neccesary, stored in ProcCode table
				//CPT (Current Procedure Terminology)---------------------------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS cpt";
					Db.NonQ(command);
					command=@"CREATE TABLE cpt (
						CptNum bigint NOT NULL auto_increment PRIMARY KEY,
						CptCode varchar(255) NOT NULL,
						Description varchar(4000) NOT NULL,
						INDEX(CptCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE cpt'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE cpt (
						CptNum number(20) NOT NULL,
						CptCode varchar2(255),
						Description varchar2(4000),
						CONSTRAINT cpt_CptNum PRIMARY KEY (CptNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX cpt_CptCode ON cpt (CptCode)";
					Db.NonQ(command);
				}
				//CVX (Vaccine Administered)------------------------------------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS cvx";
					Db.NonQ(command);
					command=@"CREATE TABLE cvx (
						CvxNum bigint NOT NULL auto_increment PRIMARY KEY,
						CvxCode varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						IsActive varchar(255) NOT NULL,
						INDEX(CvxCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE cvx'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE cvx (
						CvxNum number(20) NOT NULL,
						CvxCode varchar2(255),
						Description varchar2(255),
						IsActive varchar2(255),
						CONSTRAINT cvx_CvxNum PRIMARY KEY (CvxNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX cvx_CvxCode ON cvx (CvxCode)";
					Db.NonQ(command);
				}
				//HCPCS (Healhtcare Common Procedure Coding System)-------------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS hcpcs";
					Db.NonQ(command);
					command=@"CREATE TABLE hcpcs (
						HcpcsNum bigint NOT NULL auto_increment PRIMARY KEY,
						HcpcsCode varchar(255) NOT NULL,
						DescriptionShort varchar(255) NOT NULL,
						INDEX(HcpcsCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE hcpcs'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE hcpcs (
						HcpcsNum number(20) NOT NULL,
						HcpcsCode varchar2(255),
						DescriptionShort varchar2(255),
						CONSTRAINT hcpcs_HcpcsNum PRIMARY KEY (HcpcsNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX hcpcs_HcpcsCode ON hcpcs (HcpcsCode)";
					Db.NonQ(command);
				}
				//ICD10CM International Classification of Diseases, 10th Revision, Clinical Modification------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS icd10";
					Db.NonQ(command);
					command=@"CREATE TABLE icd10 (
						Icd10Num bigint NOT NULL auto_increment PRIMARY KEY,
						Icd10Code varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						IsCode varchar(255) NOT NULL,
						INDEX(Icd10Code)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE icd10'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE icd10 (
						Icd10Num number(20) NOT NULL,
						Icd10Code varchar2(255),
						Description varchar2(255),
						IsCode varchar2(255),
						CONSTRAINT icd10_Icd10Num PRIMARY KEY (Icd10Num)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX icd10_Icd10Code ON icd10 (Icd10Code)";
					Db.NonQ(command);
				}
				//ICD9CM International Classification of Diseases, 9th Revision, Clinical Modification--------------------------------------------------------------------
				//Already Exists.
				//LOINC (Logical Observation Identifier Names and Codes)--------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS loinc";
					Db.NonQ(command);
					command=@"CREATE TABLE loinc (
						LoincNum bigint NOT NULL auto_increment PRIMARY KEY,
						LoincCode varchar(255) NOT NULL,
						Component varchar(255) NOT NULL,
						PropertyObserved varchar(255) NOT NULL,
						TimeAspct varchar(255) NOT NULL,
						SystemMeasured varchar(255) NOT NULL,
						ScaleType varchar(255) NOT NULL,
						MethodType varchar(255) NOT NULL,
						StatusOfCode varchar(255) NOT NULL,
						NameShort varchar(255) NOT NULL,
						ClassType varchar(255) NOT NULL,
						UnitsRequired tinyint NOT NULL,
						OrderObs varchar(255) NOT NULL,
						HL7FieldSubfieldID varchar(255) NOT NULL,
						ExternalCopyrightNotice text NOT NULL,
						NameLongCommon varchar(255) NOT NULL,
						UnitsUCUM varchar(255) NOT NULL,
						RankCommonTests int NOT NULL,
						RankCommonOrders int NOT NULL,
						INDEX(LoincCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE loinc'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE loinc (
						LoincNum number(20) NOT NULL,
						LoincCode varchar2(255),
						Component varchar2(255),
						PropertyObserved varchar2(255),
						TimeAspct varchar2(255),
						SystemMeasured varchar2(255),
						ScaleType varchar2(255),
						MethodType varchar2(255),
						StatusOfCode varchar2(255),
						NameShort varchar2(255),
						ClassType varchar2(255) NOT NULL,
						UnitsRequired number(3) NOT NULL,
						OrderObs varchar2(255),
						HL7FieldSubfieldID varchar2(255),
						ExternalCopyrightNotice varchar2(4000),
						NameLongCommon varchar2(255),
						UnitsUCUM varchar2(255),
						RankCommonTests number(11) NOT NULL,
						RankCommonOrders number(11) NOT NULL,
						CONSTRAINT loinc_LoincNum PRIMARY KEY (LoincNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX loinc_LoincCode ON loinc (LoincCode)";
					Db.NonQ(command);
				}
				//RXNORM--------------------------------------------------------------------------------------------------------------------------------------------------
				//Already Exists.
				//SNOMEDCT (Systematic Nomencalture of Medicine Clinical Terms)-------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS snomed";
					Db.NonQ(command);
					command=@"CREATE TABLE snomed (
						SnomedNum bigint NOT NULL auto_increment PRIMARY KEY,
						SnomedCode varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						INDEX(SnomedCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE snomed'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE snomed (
						SnomedNum number(20) NOT NULL,
						SnomedCode varchar2(255),
						Description varchar2(255),
						CONSTRAINT snomed_SnomedNum PRIMARY KEY (SnomedNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX snomed_SnomedCode ON snomed (SnomedCode)";
					Db.NonQ(command);
				}
				//SOP (Source of Payment Typology)------------------------------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS sop";
					Db.NonQ(command);
					command=@"CREATE TABLE sop (
						SopNum bigint NOT NULL auto_increment PRIMARY KEY,
						SopCode varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						INDEX(SopCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE sop'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE sop (
						SopNum number(20) NOT NULL,
						SopCode varchar2(255),
						Description varchar2(255),
						CONSTRAINT sop_SopNum PRIMARY KEY (SopNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX sop_SopCode ON sop (SopCode)";
					Db.NonQ(command);
				}
#endregion
				//Rename emailaddress.SMTPserverIncoming to emailaddress.Pop3ServerIncoming, but leave data type alone. CRUD generator cannot write this query. See pattern for convert database.
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailaddress CHANGE SMTPserverIncoming Pop3ServerIncoming varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE emailaddress RENAME COLUMN SMTPserverIncoming TO Pop3ServerIncoming";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE medicationpat ADD MedDescript varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE medicationpat ADD MedDescript varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE medicationpat ADD RxCui bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE medicationpat ADD INDEX (RxCui)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE medicationpat ADD RxCui number(20)";
					Db.NonQ(command);
					command="UPDATE medicationpat SET RxCui = 0 WHERE RxCui IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE medicationpat MODIFY RxCui NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX medicationpat_RxCui ON medicationpat (RxCui)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="UPDATE medicationpat,medication SET medicationpat.RxCui=medication.RxCui WHERE medicationpat.MedicationNum=medication.MedicationNum";
					Db.NonQ(command);
				}
				else {//oracle
					command="UPDATE medicationpat SET medicationpat.RxCui=(SELECT medication.RxCui FROM medication WHERE medication.MedicationNum=medicationpat.MedicationNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE medicationpat ADD NewCropGuid varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE medicationpat ADD NewCropGuid varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE medicationpat ADD IsCpoe tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE medicationpat ADD IsCpoe number(3)";
					Db.NonQ(command);
					command="UPDATE medicationpat SET IsCpoe = 0 WHERE IsCpoe IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE medicationpat MODIFY IsCpoe NOT NULL";
					Db.NonQ(command);
				}
				//oracle compatible
				command="UPDATE medicationpat SET IsCpoe=1 "
						+"WHERE PatNote!='' AND DateStart > "+POut.Date((new DateTime(1880,1,1)));
				Db.NonQ(command);
				//Add additional EHR Measures to DB
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO ehrmeasure(MeasureType,Numerator,Denominator) VALUES(16,-1,-1)";
					Db.NonQ(command);
					command="INSERT INTO ehrmeasure(MeasureType,Numerator,Denominator) VALUES(17,-1,-1)";
					Db.NonQ(command);
					command="INSERT INTO ehrmeasure(MeasureType,Numerator,Denominator) VALUES(18,-1,-1)";
					Db.NonQ(command);
					command="INSERT INTO ehrmeasure(MeasureType,Numerator,Denominator) VALUES(19,-1,-1)";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO ehrmeasure(EhrMeasureNum,MeasureType,Numerator,Denominator) VALUES((SELECT MAX(EhrMeasureNum)+1 FROM ehrmeasure),16,-1,-1)";
					Db.NonQ(command);
					command="INSERT INTO ehrmeasure(EhrMeasureNum,MeasureType,Numerator,Denominator) VALUES((SELECT MAX(EhrMeasureNum)+1 FROM ehrmeasure),17,-1,-1)";
					Db.NonQ(command);
					command="INSERT INTO ehrmeasure(EhrMeasureNum,MeasureType,Numerator,Denominator) VALUES((SELECT MAX(EhrMeasureNum)+1 FROM ehrmeasure),18,-1,-1)";
					Db.NonQ(command);
					command="INSERT INTO ehrmeasure(EhrMeasureNum,MeasureType,Numerator,Denominator) VALUES((SELECT MAX(EhrMeasureNum)+1 FROM ehrmeasure),19,-1,-1)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('MeaningfulUseTwo','0')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'MeaningfulUseTwo','0')";
					Db.NonQ(command);
				}
				//Time Card Overhaul for differential pay----------------------------------------------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE clockevent ADD Rate2Hours time NOT NULL";
					Db.NonQ(command);
					command="UPDATE clockevent SET rate2hours='-01:00:00'";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE clockevent ADD Rate2Hours varchar2(255)";
					Db.NonQ(command);
					command="UPDATE clockevent SET rate2hours='-01:00:00'";
					Db.NonQ(command);
				}				
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE clockevent ADD Rate2Auto time NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE clockevent ADD Rate2Auto varchar2(255)";
					Db.NonQ(command);
				}
				command="ALTER TABLE timecardrule DROP COLUMN AmtDiff";
				Db.NonQ(command);
				command="ALTER TABLE clockevent DROP COLUMN AmountBonus";
				Db.NonQ(command);
				command="ALTER TABLE clockevent DROP COLUMN AmountBonusAuto";
				Db.NonQ(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehramendment";
					Db.NonQ(command);
					command=@"CREATE TABLE ehramendment (
						EhrAmendmentNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						IsAccepted tinyint NOT NULL,
						Description text NOT NULL,
						Source tinyint NOT NULL,
						SourceName text NOT NULL,
						FileName varchar(255) NOT NULL,
						RawBase64 longtext NOT NULL,
						DateTRequest datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						DateTAcceptDeny datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						DateTAppend datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						INDEX(PatNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehramendment'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehramendment (
						EhrAmendmentNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						IsAccepted number(3) NOT NULL,
						Description varchar2(2000),
						Source number(3) NOT NULL,
						SourceName varchar2(2000),
						FileName varchar2(255),
						RawBase64 clob,
						DateTRequest date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						DateTAcceptDeny date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						DateTAppend date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						CONSTRAINT ehramendment_EhrAmendmentNum PRIMARY KEY (EhrAmendmentNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehramendment_PatNum ON ehramendment (PatNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE popup ADD UserNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE popup ADD INDEX (UserNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE popup ADD UserNum number(20)";
					Db.NonQ(command);
					command="UPDATE popup SET UserNum = 0 WHERE UserNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE popup MODIFY UserNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX popup_UserNum ON popup (UserNum)";
					Db.NonQ(command);
				} 
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE popup ADD DateTimeEntry datetime NOT NULL";
					Db.NonQ(command);
					command="UPDATE popup SET DateTimeEntry='0001-01-01 00:00:00'";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE popup ADD DateTimeEntry date";
					Db.NonQ(command);
					command="UPDATE popup SET DateTimeEntry = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateTimeEntry IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE popup MODIFY DateTimeEntry NOT NULL";
					Db.NonQ(command);
				} 
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE popup ADD IsArchived tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE popup ADD IsArchived number(3)";
					Db.NonQ(command);
					command="UPDATE popup SET IsArchived = 0 WHERE IsArchived IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE popup MODIFY IsArchived NOT NULL";
					Db.NonQ(command);
				} 
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE popup ADD PopupNumArchive bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE popup ADD INDEX (PopupNumArchive)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE popup ADD PopupNumArchive number(20)";
					Db.NonQ(command);
					command="UPDATE popup SET PopupNumArchive = 0 WHERE PopupNumArchive IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE popup MODIFY PopupNumArchive NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX popup_PopupNumArchive ON popup (PopupNumArchive)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE patientrace ADD CdcrecCode varchar(255) NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE patientrace ADD INDEX (CdcrecCode)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE patientrace ADD CdcrecCode varchar2(255)";
					Db.NonQ(command);
					command="UPDATE patientrace SET CdcrecCode = '' WHERE CdcrecCode IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE patientrace MODIFY CdcrecCode NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX patientrace_CdcrecCode ON patientrace (CdcrecCode)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vitalsign ADD WeightCode varchar(255) NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE vitalsign ADD INDEX (WeightCode)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vitalsign ADD WeightCode varchar2(255)";
					Db.NonQ(command);
					command=@"CREATE INDEX vitalsign_WeightCode ON vitalsign (WeightCode)";
					Db.NonQ(command);
				}
				//Add indexes for code systems------------------------------------------------------------------------------------------------------
				try {
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="ALTER TABLE diseasedef ADD INDEX (Icd9Code)";
						Db.NonQ(command);
						command="ALTER TABLE diseasedef ADD INDEX (SnomedCode)";
						Db.NonQ(command);
					}
					else {//oracle
						command=@"CREATE INDEX diseasedef_Icd9Code ON diseasedef (Icd9Code)";
						Db.NonQ(command);
						command=@"CREATE INDEX diseasedef_SnomedCode ON diseasedef (SnomedCode)";
						Db.NonQ(command);
					}
				}
				catch(Exception ex) { }//Only an index. (Exception ex) required to catch thrown exception
				try {
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="ALTER TABLE icd9 ADD INDEX (Icd9Code)";
						Db.NonQ(command);
					}
					else {//oracle
						command=@"CREATE INDEX icd9_Icd9Code ON icd9 (Icd9Code)";
						Db.NonQ(command);
					}
				}
				catch(Exception ex) { }//Only an index. (Exception ex) required to catch thrown exception
				try {
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="ALTER TABLE rxnorm ADD INDEX (RxCui)";
						Db.NonQ(command);
					}
					else {//oracle
						command=@"CREATE INDEX rxnorm_RxCui ON rxnorm (RxCui)";
						Db.NonQ(command);
					}
				}	
				catch(Exception ex) { }//Only an index. (Exception ex) required to catch thrown exception
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE insplan ADD SopCode varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE insplan ADD SopCode varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS payortype";
					Db.NonQ(command);
					command=@"CREATE TABLE payortype (
						PayorTypeNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						DateStart date NOT NULL DEFAULT '0001-01-01',
						SopCode varchar(255) NOT NULL,
						Note text NOT NULL,
						INDEX(PatNum),
						INDEX(SopCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE payortype'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE payortype (
						PayorTypeNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						DateStart date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						SopCode varchar2(255),
						Note varchar2(2000),
						CONSTRAINT payortype_PayorTypeNum PRIMARY KEY (PayorTypeNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX payortype_PatNum ON payortype (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX payortype_SopCode ON payortype (SopCode)";
					Db.NonQ(command);
				}
				//oracle compatible
				command="UPDATE patientrace SET CdcrecCode='2054-5' WHERE Race=1";//AfricanAmerican
				Db.NonQ(command);
				command="UPDATE patientrace SET CdcrecCode='1002-5' WHERE Race=2";//AmericanIndian
				Db.NonQ(command);
				command="UPDATE patientrace SET CdcrecCode='2028-9' WHERE Race=3";//Asian
				Db.NonQ(command);
				command="UPDATE patientrace SET CdcrecCode='2076-8' WHERE Race=5";//HawaiiOrPacIsland
				Db.NonQ(command);
				command="UPDATE patientrace SET CdcrecCode='2135-2' WHERE Race=6";//Hispanic
				Db.NonQ(command);
				command="UPDATE patientrace SET CdcrecCode='2131-1' WHERE Race=8";//Other
				Db.NonQ(command);
				command="UPDATE patientrace SET CdcrecCode='2106-3' WHERE Race=9";//White
				Db.NonQ(command);
				command="UPDATE patientrace SET CdcrecCode='2186-5' WHERE Race=10";//NotHispanic
				Db.NonQ(command);
				//oracle compatible
				//We will insert another patientrace row specifying 'NotHispanic' if there is not a Hispanic entry or a DeclinedToSpecify entry but there is at least one other patientrace entry.  The absence of ethnicity was assumed NotHispanic in the past, now we are going to explicitly store that value.  enum=10, CdcrecCode='2186-5'
				command="SELECT DISTINCT PatNum FROM patientrace WHERE PatNum NOT IN(SELECT PatNum FROM patientrace WHERE Race IN(4,6))";//4=DeclinedToSpecify,6=Hispanic
				DataTable table=Db.GetTable(command);
				long patNum=0;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					for(int i=0;i<table.Rows.Count;i++) {
						patNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
						command="INSERT INTO patientrace (PatNum,Race,CdcrecCode) VALUES("+POut.Long(patNum)+",10,'2186-5')";
						Db.NonQ(command);
					}
				}
				else {//oracle
					for(int i=0;i<table.Rows.Count;i++) {
						patNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
						command="INSERT INTO patientrace (PatientRaceNum,PatNum,Race,CdcrecCode) "
							+"VALUES((SELECT MAX(PatientRaceNum)+1 FROM patientrace),"+POut.Long(patNum)+",10,'2186-5')";
						Db.NonQ(command);
					}
				}
				//intervention
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS intervention";
					Db.NonQ(command);
					command=@"CREATE TABLE intervention (
						InterventionNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						ProvNum bigint NOT NULL,
						CodeValue varchar(30) NOT NULL,
						CodeSystem varchar(30) NOT NULL,
						Note text NOT NULL,
						DateTimeEntry date NOT NULL DEFAULT '0001-01-01',
						CodeSet tinyint NOT NULL,
						INDEX(PatNum),
						INDEX(ProvNum),
						INDEX(CodeValue)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE intervention'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE intervention (
						InterventionNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						ProvNum number(20) NOT NULL,
						CodeValue varchar2(30),
						CodeSystem varchar2(30),
						Note varchar2(4000),
						CodeSet number(3) NOT NULL,
						DateTimeEntry date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						CONSTRAINT intervention_InterventionNum PRIMARY KEY (InterventionNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX intervention_PatNum ON intervention (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX intervention_ProvNum ON intervention (ProvNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX intervention_CodeValue ON intervention (CodeValue)";
					Db.NonQ(command);
				}
				//ehrnotperformed
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrnotperformed";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrnotperformed (
						EhrNotPerformedNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						ProvNum bigint NOT NULL,
						CodeValue varchar(30) NOT NULL,
						CodeSystem varchar(30) NOT NULL,
						CodeValueReason varchar(30) NOT NULL,
						CodeSystemReason varchar(30) NOT NULL,
						Note text NOT NULL,
						DateEntry date NOT NULL DEFAULT '0001-01-01',
						INDEX(PatNum),
						INDEX(ProvNum),
						INDEX(CodeValue),
						INDEX(CodeValueReason)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrnotperformed'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrnotperformed (
						EhrNotPerformedNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						ProvNum number(20) NOT NULL,
						CodeValue varchar2(30),
						CodeSystem varchar2(30),
						CodeValueReason varchar2(30),
						CodeSystemReason varchar2(30),
						Note varchar2(4000),
						DateEntry date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						CONSTRAINT ehrnotperformed_EhrNotPerforme PRIMARY KEY (EhrNotPerformedNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrnotperformed_PatNum ON ehrnotperformed (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrnotperformed_ProvNum ON ehrnotperformed (ProvNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrnotperformed_CodeValue ON ehrnotperformed (CodeValue)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrnotperformed_CodeValueReaso ON ehrnotperformed (CodeValueReason)";
					Db.NonQ(command);
				}
				//encounter
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS encounter";
					Db.NonQ(command);
					command=@"CREATE TABLE encounter (
						EncounterNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						ProvNum bigint NOT NULL,
						CodeValue varchar(30) NOT NULL,
						CodeSystem varchar(30) NOT NULL,
						Note text NOT NULL,
						DateEncounter date NOT NULL DEFAULT '0001-01-01',
						INDEX(PatNum),
						INDEX(ProvNum),
						INDEX(CodeValue)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE encounter'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE encounter (
						EncounterNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						ProvNum number(20) NOT NULL,
						CodeValue varchar2(30),
						CodeSystem varchar2(30),
						Note varchar2(4000),
						DateEncounter date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						CONSTRAINT encounter_EncounterNum PRIMARY KEY (EncounterNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX encounter_PatNum ON encounter (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX encounter_ProvNum ON encounter (ProvNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX encounter_CodeValue ON encounter (CodeValue)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('NistTimeServerUrl','nist-time-server.eoni.com')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'NistTimeServerUrl','nist-time-server.eoni.com')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrsummaryccd ADD EmailAttachNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE ehrsummaryccd ADD INDEX (EmailAttachNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE ehrsummaryccd ADD EmailAttachNum number(20)";
					Db.NonQ(command);
					command="UPDATE ehrsummaryccd SET EmailAttachNum = 0 WHERE EmailAttachNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE ehrsummaryccd MODIFY EmailAttachNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrsummaryccd_EmailAttachNum ON ehrsummaryccd (EmailAttachNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vitalsign ADD HeightExamCode varchar(30) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vitalsign ADD HeightExamCode varchar2(30)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vitalsign ADD WeightExamCode varchar(30) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vitalsign ADD WeightExamCode varchar2(30)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vitalsign ADD BMIExamCode varchar(30) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vitalsign ADD BMIExamCode varchar2(30)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vitalsign ADD EhrNotPerformedNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE vitalsign ADD INDEX (EhrNotPerformedNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vitalsign ADD EhrNotPerformedNum number(20)";
					Db.NonQ(command);
					command="UPDATE vitalsign SET EhrNotPerformedNum = 0 WHERE EhrNotPerformedNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vitalsign MODIFY EhrNotPerformedNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX vitalsign_EhrNotPerformedNum ON vitalsign (EhrNotPerformedNum)";
					Db.NonQ(command);
				}
				//Add exam codes to vital sign rows currently in the db using the most generic code from each set
				command="UPDATE vitalsign SET HeightExamCode='8302-2' WHERE Height!=0";//8302-2 is "Body height"
				Db.NonQ(command);
				command="UPDATE vitalsign SET WeightExamCode='29463-7' WHERE Weight!=0";//29463-7 is "Body weight"
				Db.NonQ(command);
				command="UPDATE vitalsign SET BMIExamCode='59574-4' WHERE Height!=0 AND Weight!=0";//59574-4 is "Body mass index (BMI) [Percentile]"
				Db.NonQ(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					//Update over/underweight code, only 1 LOINC code for overweight and 1 for underweight
					//Based on age before the start of the measurement period, which is age before any birthday in the year of DateTaken.  Different range for 18-64 and 65+.  Under 18 not classified under/over
					command="UPDATE vitalsign,patient SET WeightCode='238131007'/*Overweight*/ "
						+"WHERE patient.PatNum=vitalsign.PatNum AND Height!=0 AND Weight!=0 "
						+"AND Birthdate>'1880-01-01' AND ("
						+"(YEAR(DateTaken)-YEAR(Birthdate)-1>=65 AND (Weight*703)/(Height*Height)>=30) "
						+"OR "
						+"(YEAR(DateTaken)-YEAR(Birthdate)-1 BETWEEN 18 AND 64 AND (Weight*703)/(Height*Height)>=25))";
					Db.NonQ(command);
					command="UPDATE vitalsign,patient	SET WeightCode='248342006'/*Underweight*/ "
						+"WHERE patient.PatNum=vitalsign.PatNum	AND Height!=0 AND Weight!=0 "
						+"AND Birthdate>'1880-01-01' AND ("
						+"(YEAR(DateTaken)-YEAR(patient.Birthdate)-1>=65 AND (Weight*703)/(Height*Height)<23) "
						+"OR "
						+"(YEAR(DateTaken)-YEAR(patient.Birthdate)-1 BETWEEN 18 AND 64 AND (Weight*703)/(Height*Height)<18.5))";
					Db.NonQ(command);
				}
				else {//oracle
					//EHR not oracle compatible so the vital sign WeightCode will not be used, only for ehr reporting
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vitalsign ADD PregDiseaseNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE vitalsign ADD INDEX (PregDiseaseNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vitalsign ADD PregDiseaseNum number(20)";
					Db.NonQ(command);
					command="UPDATE vitalsign SET PregDiseaseNum = 0 WHERE PregDiseaseNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vitalsign MODIFY PregDiseaseNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX vitalsign_PregDiseaseNum ON vitalsign (PregDiseaseNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('CQMDefaultEncounterCodeValue','none')";//we cannot preset this to a SNOMEDCT code since the customer may not be US
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'CQMDefaultEncounterCodeValue','none')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('CQMDefaultEncounterCodeSystem','')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'CQMDefaultEncounterCodeSystem','')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('PregnancyDefaultCodeValue','none')";//we cannot preset this to a SNOMEDCT code since the customer may not be US
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'PregnancyDefaultCodeValue','none')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('PregnancyDefaultCodeSystem','')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'PregnancyDefaultCodeSystem','')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE diseasedef ADD Icd10Code varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE diseasedef ADD Icd10Code varchar2(255)";
					Db.NonQ(command);
				}
				//Add indexes for code systems------------------------------------------------------------------------------------------------------
				try {
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="ALTER TABLE diseasedef ADD INDEX (Icd10Code)";
						Db.NonQ(command);
					}
					else {//oracle
						command=@"CREATE INDEX diseasedef_Icd10Code ON diseasedef (Icd10Code)";
						Db.NonQ(command);
					}
				}
				catch(Exception ex) { }//Only an index. (Exception ex) required to catch thrown exception
				//Add indexes to speed up payroll------------------------------------------------------------------------------------------------------
				try {
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="ALTER TABLE clockevent ADD INDEX (TimeDisplayed1)";
						Db.NonQ(command);
					}
					else {//oracle
						command=@"CREATE INDEX clockevent_TimeDisplayed1 ON clockevent (TimeDisplayed1)";
						Db.NonQ(command);
					}
				}
				catch(Exception ex) { }//Only an index. (Exception ex) required to catch thrown exception
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName) VALUES('ADPCompanyCode')";//No default value.
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'ADPCompanyCode')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE employee ADD PayrollID varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE employee ADD PayrollID varchar2(255)";
					Db.NonQ(command);
				}
				//Add indexes to speed up customer management window------------------------------------------------------------------------------------
				try {
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="ALTER TABLE registrationkey ADD INDEX (PatNum)";
						Db.NonQ(command);
					}
					else {//oracle
						command=@"CREATE INDEX registrationkey_PatNum ON clockevent (PatNum)";
						Db.NonQ(command);
					}
				}
				catch(Exception ex) { }//Only an index. (Exception ex) required to catch thrown exception
				try {
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="ALTER TABLE repeatcharge ADD INDEX (PatNum)";
						Db.NonQ(command);
					}
					else {//oracle
						command=@"CREATE INDEX repeatcharge_PatNum ON clockevent (PatNum)";
						Db.NonQ(command);
					}
				}
				catch(Exception ex) { }//Only an index. (Exception ex) required to catch thrown exception
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE allergydef CHANGE Snomed SnomedType tinyint";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE allergydef RENAME COLUMN Snomed TO SnomedType";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE allergydef ADD SnomedAllergyTo varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE allergydef ADD SnomedAllergyTo varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE allergy ADD SnomedReaction varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE allergy ADD SnomedReaction varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE dunning ADD EmailSubject varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE dunning ADD EmailSubject varchar2(255)";
					Db.NonQ(command);
				} 
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE dunning ADD EmailBody mediumtext NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE dunning ADD EmailBody clob";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE statement ADD EmailSubject varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE statement ADD EmailSubject varchar2(255)";
					Db.NonQ(command);
				} 
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE statement ADD EmailBody mediumtext NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE statement ADD EmailBody clob";
					Db.NonQ(command);
				}				
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS maparea";
					Db.NonQ(command);
					command=@"CREATE TABLE maparea (
						MapAreaNum bigint NOT NULL auto_increment PRIMARY KEY,
						Extension int NOT NULL,
						XPos double NOT NULL,
						YPos double NOT NULL,
						Width double NOT NULL,
						Height double NOT NULL,
						Description varchar(255) NOT NULL,
						ItemType tinyint NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE maparea'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE maparea (
						MapAreaNum number(20) NOT NULL,
						Extension number(11) NOT NULL,
						XPos number(38,8) NOT NULL,
						YPos number(38,8) NOT NULL,
						Width number(38,8) NOT NULL,
						Height number(38,8) NOT NULL,
						Description varchar2(255),
						ItemType number(3) NOT NULL,
						CONSTRAINT maparea_MapAreaNum PRIMARY KEY (MapAreaNum)
						)";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.3.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_3_4();
		}

		private static void To13_3_4() {
			if(FromVersion<new Version("13.3.4.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailmessage ADD RecipientAddress varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE emailmessage ADD RecipientAddress varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS emailmessageuid";
					Db.NonQ(command);
					command=@"CREATE TABLE emailmessageuid (
						EmailMessageUidNum bigint NOT NULL auto_increment PRIMARY KEY,
						Uid text NOT NULL,
						RecipientAddress varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE emailmessageuid'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE emailmessageuid (
						EmailMessageUidNum number(20) NOT NULL,
						""Uid"" varchar2(4000),
						RecipientAddress varchar2(255),
						CONSTRAINT emailmessageuid_EmailMessageUi PRIMARY KEY (EmailMessageUidNum)
						)";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.3.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_3_5();
		}

		private static void To13_3_5() {
			if(FromVersion<new Version("13.3.5.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE erxlog ADD ProvNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE erxlog ADD INDEX (ProvNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE erxlog ADD ProvNum number(20)";
					Db.NonQ(command);
					command="UPDATE erxlog SET ProvNum = 0 WHERE ProvNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE erxlog MODIFY ProvNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX erxlog_ProvNum ON erxlog (ProvNum)";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.3.5.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_3_6();
		}

		///<summary>Oracle compatible: 12/26/2013</summary>
		private static void To13_3_6() {
			if(FromVersion<new Version("13.3.6.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailmessage ADD RawEmailIn longtext NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE emailmessage ADD RawEmailIn clob";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.3.6.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To13_3_7();
		}

		private static void To13_3_7() {
			if(FromVersion<new Version("13.3.7.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailmessageuid CHANGE Uid MsgId text";
					Db.NonQ(command);
				}
				else {//oracle
					command=@"ALTER TABLE emailmessageuid RENAME COLUMN ""Uid"" TO MsgId";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '13.3.7.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To14_1_0();
		}

		private static void To14_1_0() {
			if(FromVersion<new Version("14.1.0.0")) {
				string command;
				//Added permission EhrShowCDS.     No one has this permission by default.  This is more like a user level preference than a permission.
				//Added permission EhrInfoButton.  No one has this permission by default.  This is more like a user level preference than a permission.
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrtrigger";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrtrigger (
						EhrTriggerNum bigint NOT NULL auto_increment PRIMARY KEY,
						Description varchar(255) NOT NULL,
						ProblemSnomedList text NOT NULL,
						ProblemIcd9List text NOT NULL,
						ProblemIcd10List text NOT NULL,
						ProblemDefNumList text NOT NULL,
						MedicationNumList text NOT NULL,
						RxCuiList text NOT NULL,
						CvxList text NOT NULL,
						AllergyDefNumList text NOT NULL,
						DemographicsList text NOT NULL,
						LabLoincList text NOT NULL,
						VitalLoincList text NOT NULL,
						Instructions text NOT NULL,
						Bibliography text NOT NULL,
						Cardinality tinyint NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrtrigger'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrtrigger (
						EhrTriggerNum number(20) NOT NULL,
						Description varchar2(255),
						ProblemSnomedList varchar2(4000),
						ProblemIcd9List varchar2(4000),
						ProblemIcd10List varchar2(4000),
						ProblemDefNumList varchar2(4000),
						MedicationNumList varchar2(4000),
						RxCuiList varchar2(4000),
						CvxList varchar2(4000),
						AllergyDefNumList varchar2(4000),
						DemographicsList varchar2(4000),
						LabLoincList varchar2(4000),
						VitalLoincList varchar2(4000),
						Instructions varchar2(4000),
						Bibliography varchar2(4000),
						Cardinality number(3) NOT NULL,
						CONSTRAINT ehrtrigger_EhrTriggerNum PRIMARY KEY (EhrTriggerNum)
						)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE provider ADD EmailAddressNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE provider ADD INDEX (EmailAddressNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE provider ADD EmailAddressNum number(20)";
					Db.NonQ(command);
					command="UPDATE provider SET EmailAddressNum = 0 WHERE EmailAddressNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE provider MODIFY EmailAddressNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX provider_EmailAddressNum ON provider (EmailAddressNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrmeasureevent ADD CodeValueEvent varchar(30) NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE ehrmeasureevent ADD INDEX (CodeValueEvent)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE ehrmeasureevent ADD CodeValueEvent varchar2(30)";
					Db.NonQ(command);
					command="CREATE INDEX ehrmeasureevent_CodeValueEvent ON ehrmeasureevent (CodeValueEvent)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrmeasureevent ADD CodeSystemEvent varchar(30) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE ehrmeasureevent ADD CodeSystemEvent varchar2(30)";
					Db.NonQ(command);
				}
				//oracle compatible
				command="UPDATE ehrmeasureevent SET CodeValueEvent='11366-2' WHERE EventType=8";//Set all TobaccoUseAssessed ehrmeasureevents to code for 'History of tobacco use Narrative'
				Db.NonQ(command);
				command="UPDATE ehrmeasureevent SET CodeSystemEvent='LOINC' WHERE EventType=8";//All TobaccoUseAssessed codes are LOINC codes
				Db.NonQ(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrmeasureevent ADD CodeValueResult varchar(30) NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE ehrmeasureevent ADD INDEX (CodeValueResult)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE ehrmeasureevent ADD CodeValueResult varchar2(30)";
					Db.NonQ(command);
					command="CREATE INDEX ehrmeasureevent_CodeValueResult ON ehrmeasureevent (CodeValueResult)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrmeasureevent ADD CodeSystemResult varchar(30) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE ehrmeasureevent ADD CodeSystemResult varchar2(30)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE intervention CHANGE DateTimeEntry DateEntry date NOT NULL DEFAULT '0001-01-01'";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE intervention CHANGE DateTimeEntry DateEntry date";
					Db.NonQ(command);
					command="UPDATE intervention SET DateEntry = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateEntry IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE intervention MODIFY DateEntry NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrsummaryccd MODIFY ContentSummary longtext NOT NULL";
					Db.NonQ(command);
				}
				//oracle ContentSummary data type is already clob which can handle up to 4GB of data.
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE disease ADD SnomedProblemType varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE disease ADD SnomedProblemType varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE disease ADD FunctionStatus tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE disease ADD FunctionStatus number(3)";
					Db.NonQ(command);
					command="UPDATE disease SET FunctionStatus = 0 WHERE FunctionStatus IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE disease MODIFY FunctionStatus NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrcareplan";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrcareplan (
						EhrCarePlanNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						SnomedEducation varchar(255) NOT NULL,
						Instructions varchar(255) NOT NULL,
						INDEX(PatNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrcareplan'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrcareplan (
						EhrCarePlanNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						SnomedEducation varchar2(255),
						Instructions varchar2(255),
						CONSTRAINT ehrcareplan_EhrCarePlanNum PRIMARY KEY (EhrCarePlanNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrcareplan_PatNum ON ehrcareplan (PatNum)";
					Db.NonQ(command);
				}
				//Add UCUM table
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ucum";
					Db.NonQ(command);
					command=@"CREATE TABLE ucum (
						UcumNum bigint NOT NULL auto_increment PRIMARY KEY,
						UcumCode varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						IsInUse tinyint NOT NULL,
						INDEX(UcumCode)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ucum'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ucum (
						UcumNum number(20) NOT NULL,
						UcumCode varchar2(255),
						Description varchar2(255),
						IsInUse number(3) NOT NULL,
						CONSTRAINT ucum_UcumNum PRIMARY KEY (UcumNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ucum_UcumCode ON ucum (UcumCode)";
					Db.NonQ(command);
				}
				//Add UCUM to Code System Importer
				command=@"INSERT INTO codesystem (CodeSystemNum,CodeSystemName,HL7OID) VALUES (13,'UCUM','2.16.840.1.113883.6.8')";
				Db.NonQ(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrcareplan ADD DatePlanned date NOT NULL DEFAULT '0001-01-01'";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE ehrcareplan ADD DatePlanned date";
					Db.NonQ(command);
					command="UPDATE ehrcareplan SET DatePlanned = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DatePlanned IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE ehrcareplan MODIFY DatePlanned NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('PatientPortalNotifyBody','Please go to this link and login using your credentials. [URL]')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'PatientPortalNotifyBody','Please go to this link and login using your credentials. [URL]')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference(PrefName,ValueString) VALUES('PatientPortalNotifySubject','You have a secure message waiting for you')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO preference(PrefNum,PrefName,ValueString) VALUES((SELECT MAX(PrefNum)+1 FROM preference),'PatientPortalNotifySubject','You have a secure message waiting for you')";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailmessage ADD ProvNumWebMail bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE emailmessage ADD INDEX (ProvNumWebMail)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE emailmessage ADD ProvNumWebMail number(20)";
					Db.NonQ(command);
					command="UPDATE emailmessage SET ProvNumWebMail = 0 WHERE ProvNumWebMail IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE emailmessage MODIFY ProvNumWebMail NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX emailmessage_ProvNumWebMail ON emailmessage (ProvNumWebMail)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE emailmessage ADD PatNumSubj bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE emailmessage ADD INDEX (PatNumSubj)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE emailmessage ADD PatNumSubj number(20)";
					Db.NonQ(command);
					command="UPDATE emailmessage SET PatNumSubj = 0 WHERE PatNumSubj IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE emailmessage MODIFY PatNumSubj NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX emailmessage_PatNumSubj ON emailmessage (PatNumSubj)";
					Db.NonQ(command);
				}
				//Added Table cdspermission
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS cdspermission";
					Db.NonQ(command);
					command=@"CREATE TABLE cdspermission (
						CDSPermissionNum bigint NOT NULL auto_increment PRIMARY KEY,
						UserNum bigint NOT NULL,
						SetupCDS tinyint NOT NULL,
						ShowCDS tinyint NOT NULL,
						AccessBibliography tinyint NOT NULL,
						ProblemCDS tinyint NOT NULL,
						MedicationCDS tinyint NOT NULL,
						AllergyCDS tinyint NOT NULL,
						DemographicCDS tinyint NOT NULL,
						LabTestCDS tinyint NOT NULL,
						VitalCDS tinyint NOT NULL,
						INDEX(UserNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE cdspermission'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE cdspermission (
						CDSPermissionNum number(20) NOT NULL,
						UserNum number(20) NOT NULL,
						SetupCDS number(3) NOT NULL,
						ShowCDS number(3) NOT NULL,
						AccessBibliography number(3) NOT NULL,
						ProblemCDS number(3) NOT NULL,
						MedicationCDS number(3) NOT NULL,
						AllergyCDS number(3) NOT NULL,
						DemographicCDS number(3) NOT NULL,
						LabTestCDS number(3) NOT NULL,
						VitalCDS number(3) NOT NULL,
						CONSTRAINT cdspermission_CDSPermissionNum PRIMARY KEY (CDSPermissionNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX cdspermission_UserNum ON cdspermission (UserNum)";
					Db.NonQ(command);
				}
				#region EHR Lab framework (never going to be used)
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlab";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlab (
						EhrLabNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						EhrLabMessageNum bigint NOT NULL,
						OrderControlCode varchar(255) NOT NULL,
						PlacerOrderNum varchar(255) NOT NULL,
						PlacerOrderNamespace varchar(255) NOT NULL,
						PlacerOrderUniversalID varchar(255) NOT NULL,
						PlacerOrderUniversalIDType varchar(255) NOT NULL,
						FillerOrderNum varchar(255) NOT NULL,
						FillerOrderNamespace varchar(255) NOT NULL,
						FillerOrderUniversalID varchar(255) NOT NULL,
						FillerOrderUniversalIDType varchar(255) NOT NULL,
						PlacerGroupNum varchar(255) NOT NULL,
						PlacerGroupNamespace varchar(255) NOT NULL,
						PlacerGroupUniversalID varchar(255) NOT NULL,
						PlacerGroupUniversalIDType varchar(255) NOT NULL,
						OrderingProviderID varchar(255) NOT NULL,
						OrderingProviderLName varchar(255) NOT NULL,
						OrderingProviderFName varchar(255) NOT NULL,
						OrderingProviderMiddleNames varchar(255) NOT NULL,
						OrderingProviderSuffix varchar(255) NOT NULL,
						OrderingProviderPrefix varchar(255) NOT NULL,
						OrderingProviderAssigningAuthorityNamespaceID varchar(255) NOT NULL,
						OrderingProviderAssigningAuthorityUniversalID varchar(255) NOT NULL,
						OrderingProviderAssigningAuthorityIDType varchar(255) NOT NULL,
						OrderingProviderNameTypeCode varchar(255) NOT NULL,
						OrderingProviderIdentifierTypeCode varchar(255) NOT NULL,
						SetIdOBR bigint NOT NULL,
						UsiID varchar(255) NOT NULL,
						UsiText varchar(255) NOT NULL,
						UsiCodeSystemName varchar(255) NOT NULL,
						UsiIDAlt varchar(255) NOT NULL,
						UsiTextAlt varchar(255) NOT NULL,
						UsiCodeSystemNameAlt varchar(255) NOT NULL,
						UsiTextOriginal varchar(255) NOT NULL,
						ObservationDateTimeStart varchar(255) NOT NULL,
						ObservationDateTimeEnd varchar(255) NOT NULL,
						SpecimenActionCode varchar(255) NOT NULL,
						ResultDateTime varchar(255) NOT NULL,
						ResultStatus varchar(255) NOT NULL,
						ParentObservationID varchar(255) NOT NULL,
						ParentObservationText varchar(255) NOT NULL,
						ParentObservationCodeSystemName varchar(255) NOT NULL,
						ParentObservationIDAlt varchar(255) NOT NULL,
						ParentObservationTextAlt varchar(255) NOT NULL,
						ParentObservationCodeSystemNameAlt varchar(255) NOT NULL,
						ParentObservationTextOriginal varchar(255) NOT NULL,
						ParentObservationSubID varchar(255) NOT NULL,
						ParentPlacerOrderNum varchar(255) NOT NULL,
						ParentPlacerOrderNamespace varchar(255) NOT NULL,
						ParentPlacerOrderUniversalID varchar(255) NOT NULL,
						ParentPlacerOrderUniversalIDType varchar(255) NOT NULL,
						ParentFillerOrderNum varchar(255) NOT NULL,
						ParentFillerOrderNamespace varchar(255) NOT NULL,
						ParentFillerOrderUniversalID varchar(255) NOT NULL,
						ParentFillerOrderUniversalIDType varchar(255) NOT NULL,
						ListEhrLabResultsHandlingF tinyint NOT NULL,
						ListEhrLabResultsHandlingN tinyint NOT NULL,
						TQ1SetId bigint NOT NULL,
						TQ1DateTimeStart varchar(255) NOT NULL,
						TQ1DateTimeEnd varchar(255) NOT NULL,
						INDEX(PatNum),
						INDEX(EhrLabMessageNum),
						INDEX(SetIdOBR),
						INDEX(TQ1SetId)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlab'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlab (
						EhrLabNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						EhrLabMessageNum number(20) NOT NULL,
						OrderControlCode varchar2(255),
						PlacerOrderNum varchar2(255),
						PlacerOrderNamespace varchar2(255),
						PlacerOrderUniversalID varchar2(255),
						PlacerOrderUniversalIDType varchar2(255),
						FillerOrderNum varchar2(255),
						FillerOrderNamespace varchar2(255),
						FillerOrderUniversalID varchar2(255),
						FillerOrderUniversalIDType varchar2(255),
						PlacerGroupNum varchar2(255),
						PlacerGroupNamespace varchar2(255),
						PlacerGroupUniversalID varchar2(255),
						PlacerGroupUniversalIDType varchar2(255),
						OrderingProviderID varchar2(255),
						OrderingProviderLName varchar2(255),
						OrderingProviderFName varchar2(255),
						OrderingProviderMiddleNames varchar2(255),
						OrderingProviderSuffix varchar2(255),
						OrderingProviderPrefix varchar2(255),
						OrderingProviderAssigningAuthorityNamespaceID varchar2(255),
						OrderingProviderAssigningAuthorityUniversalID varchar2(255),
						OrderingProviderAssigningAuthorityIDType varchar2(255),
						OrderingProviderNameTypeCode varchar2(255),
						OrderingProviderIdentifierTypeCode varchar2(255),
						SetIdOBR number(20) NOT NULL,
						UsiID varchar2(255),
						UsiText varchar2(255),
						UsiCodeSystemName varchar2(255),
						UsiIDAlt varchar2(255),
						UsiTextAlt varchar2(255),
						UsiCodeSystemNameAlt varchar2(255),
						UsiTextOriginal varchar2(255),
						ObservationDateTimeStart varchar2(255),
						ObservationDateTimeEnd varchar2(255),
						SpecimenActionCode varchar2(255),
						ResultDateTime varchar2(255),
						ResultStatus varchar2(255),
						ParentObservationID varchar2(255),
						ParentObservationText varchar2(255),
						ParentObservationCodeSystemName varchar2(255),
						ParentObservationIDAlt varchar2(255),
						ParentObservationTextAlt varchar2(255),
						ParentObservationCodeSystemNameAlt varchar2(255),
						ParentObservationTextOriginal varchar2(255),
						ParentObservationSubID varchar2(255),
						ParentPlacerOrderNum varchar2(255),
						ParentPlacerOrderNamespace varchar2(255),
						ParentPlacerOrderUniversalID varchar2(255),
						ParentPlacerOrderUniversalIDType varchar2(255),
						ParentFillerOrderNum varchar2(255),
						ParentFillerOrderNamespace varchar2(255),
						ParentFillerOrderUniversalID varchar2(255),
						ParentFillerOrderUniversalIDType varchar2(255),
						ListEhrLabResultsHandlingF number(3) NOT NULL,
						ListEhrLabResultsHandlingN number(3) NOT NULL,
						TQ1SetId number(20) NOT NULL,
						TQ1DateTimeStart varchar2(255),
						TQ1DateTimeEnd varchar2(255),
						CONSTRAINT ehrlab_EhrLabNum PRIMARY KEY (EhrLabNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlab_PatNum ON ehrlab (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlab_EhrLabMessageNum ON ehrlab (EhrLabMessageNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlab_SetIdOBR ON ehrlab (SetIdOBR)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlab_TQ1SetId ON ehrlab (TQ1SetId)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabresult";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabresult (
						EhrLabResultNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabNum bigint NOT NULL,
						SetIdOBX bigint NOT NULL,
						ValueType varchar(255) NOT NULL,
						ObservationIdentifierID varchar(255) NOT NULL,
						ObservationIdentifierText varchar(255) NOT NULL,
						ObservationIdentifierCodeSystemName varchar(255) NOT NULL,
						ObservationIdentifierIDAlt varchar(255) NOT NULL,
						ObservationIdentifierTextAlt varchar(255) NOT NULL,
						ObservationIdentifierCodeSystemNameAlt varchar(255) NOT NULL,
						ObservationIdentifierTextOriginal varchar(255) NOT NULL,
						ObservationIdentifierSub varchar(255) NOT NULL,
						ObservationValueCodedElementID varchar(255) NOT NULL,
						ObservationValueCodedElementText varchar(255) NOT NULL,
						ObservationValueCodedElementCodeSystemName varchar(255) NOT NULL,
						ObservationValueCodedElementIDAlt varchar(255) NOT NULL,
						ObservationValueCodedElementTextAlt varchar(255) NOT NULL,
						ObservationValueCodedElementCodeSystemNameAlt varchar(255) NOT NULL,
						ObservationValueCodedElementTextOriginal varchar(255) NOT NULL,
						ObservationValueDateTime varchar(255) NOT NULL,
						ObservationValueTime time NOT NULL DEFAULT '00:00:00',
						ObservationValueComparator varchar(255) NOT NULL,
						ObservationValueNumber1 double NOT NULL,
						ObservationValueSeparatorOrSuffix varchar(255) NOT NULL,
						ObservationValueNumber2 double NOT NULL,
						ObservationValueNumeric double NOT NULL,
						ObservationValueText varchar(255) NOT NULL,
						UnitsID varchar(255) NOT NULL,
						UnitsText varchar(255) NOT NULL,
						UnitsCodeSystemName varchar(255) NOT NULL,
						UnitsIDAlt varchar(255) NOT NULL,
						UnitsTextAlt varchar(255) NOT NULL,
						UnitsCodeSystemNameAlt varchar(255) NOT NULL,
						UnitsTextOriginal varchar(255) NOT NULL,
						referenceRange varchar(255) NOT NULL,
						AbnormalFlags varchar(255) NOT NULL,
						ObservationResultStatus varchar(255) NOT NULL,
						ObservationDateTime varchar(255) NOT NULL,
						AnalysisDateTime varchar(255) NOT NULL,
						PerformingOrganizationName varchar(255) NOT NULL,
						PerformingOrganizationNameAssigningAuthorityNamespaceId varchar(255) NOT NULL,
						PerformingOrganizationNameAssigningAuthorityUniversalId varchar(255) NOT NULL,
						PerformingOrganizationNameAssigningAuthorityUniversalIdType varchar(255) NOT NULL,
						PerformingOrganizationIdentifierTypeCode varchar(255) NOT NULL,
						PerformingOrganizationIdentifier varchar(255) NOT NULL,
						PerformingOrganizationAddressStreet varchar(255) NOT NULL,
						PerformingOrganizationAddressOtherDesignation varchar(255) NOT NULL,
						PerformingOrganizationAddressCity varchar(255) NOT NULL,
						PerformingOrganizationAddressStateOrProvince varchar(255) NOT NULL,
						PerformingOrganizationAddressZipOrPostalCode varchar(255) NOT NULL,
						PerformingOrganizationAddressCountryCode varchar(255) NOT NULL,
						PerformingOrganizationAddressAddressType varchar(255) NOT NULL,
						PerformingOrganizationAddressCountyOrParishCode varchar(255) NOT NULL,
						MedicalDirectorID varchar(255) NOT NULL,
						MedicalDirectorLName varchar(255) NOT NULL,
						MedicalDirectorFName varchar(255) NOT NULL,
						MedicalDirectorMiddleNames varchar(255) NOT NULL,
						MedicalDirectorSuffix varchar(255) NOT NULL,
						MedicalDirectorPrefix varchar(255) NOT NULL,
						MedicalDirectorAssigningAuthorityNamespaceID varchar(255) NOT NULL,
						MedicalDirectorAssigningAuthorityUniversalID varchar(255) NOT NULL,
						MedicalDirectorAssigningAuthorityIDType varchar(255) NOT NULL,
						MedicalDirectorNameTypeCode varchar(255) NOT NULL,
						MedicalDirectorIdentifierTypeCode varchar(255) NOT NULL,
						INDEX(EhrLabNum),
						INDEX(SetIdOBX)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabresult'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabresult (
						EhrLabResultNum number(20) NOT NULL,
						EhrLabNum number(20) NOT NULL,
						SetIdOBX number(20) NOT NULL,
						ValueType varchar2(255),
						ObservationIdentifierID varchar2(255),
						ObservationIdentifierText varchar2(255),
						ObservationIdentifierCodeSystemName varchar2(255),
						ObservationIdentifierIDAlt varchar2(255),
						ObservationIdentifierTextAlt varchar2(255),
						ObservationIdentifierCodeSystemNameAlt varchar2(255),
						ObservationIdentifierTextOriginal varchar2(255),
						ObservationIdentifierSub varchar2(255),
						ObservationValueCodedElementID varchar2(255),
						ObservationValueCodedElementText varchar2(255),
						ObservationValueCodedElementCodeSystemName varchar2(255),
						ObservationValueCodedElementIDAlt varchar2(255),
						ObservationValueCodedElementTextAlt varchar2(255),
						ObservationValueCodedElementCodeSystemNameAlt varchar2(255),
						ObservationValueCodedElementTextOriginal varchar2(255),
						ObservationValueDateTime varchar2(255),
						ObservationValueTime date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						ObservationValueComparator varchar2(255),
						ObservationValueNumber1 number(38,8) NOT NULL,
						ObservationValueSeparatorOrSuffix varchar2(255),
						ObservationValueNumber2 number(38,8) NOT NULL,
						ObservationValueNumeric number(38,8) NOT NULL,
						ObservationValueText varchar2(255),
						UnitsID varchar2(255),
						UnitsText varchar2(255),
						UnitsCodeSystemName varchar2(255),
						UnitsIDAlt varchar2(255),
						UnitsTextAlt varchar2(255),
						UnitsCodeSystemNameAlt varchar2(255),
						UnitsTextOriginal varchar2(255),
						referenceRange varchar2(255),
						AbnormalFlags varchar2(255),
						ObservationResultStatus varchar2(255),
						ObservationDateTime varchar2(255),
						AnalysisDateTime varchar2(255),
						PerformingOrganizationName varchar2(255),
						PerformingOrganizationNameAssigningAuthorityNamespaceId varchar2(255),
						PerformingOrganizationNameAssigningAuthorityUniversalId varchar2(255),
						PerformingOrganizationNameAssigningAuthorityUniversalIdType varchar2(255),
						PerformingOrganizationIdentifierTypeCode varchar2(255),
						PerformingOrganizationIdentifier varchar2(255),
						PerformingOrganizationAddressStreet varchar2(255),
						PerformingOrganizationAddressOtherDesignation varchar2(255),
						PerformingOrganizationAddressCity varchar2(255),
						PerformingOrganizationAddressStateOrProvince varchar2(255),
						PerformingOrganizationAddressZipOrPostalCode varchar2(255),
						PerformingOrganizationAddressCountryCode varchar2(255),
						PerformingOrganizationAddressAddressType varchar2(255),
						PerformingOrganizationAddressCountyOrParishCode varchar2(255),
						MedicalDirectorID varchar2(255),
						MedicalDirectorLName varchar2(255),
						MedicalDirectorFName varchar2(255),
						MedicalDirectorMiddleNames varchar2(255),
						MedicalDirectorSuffix varchar2(255),
						MedicalDirectorPrefix varchar2(255),
						MedicalDirectorAssigningAuthorityNamespaceID varchar2(255),
						MedicalDirectorAssigningAuthorityUniversalID varchar2(255),
						MedicalDirectorAssigningAuthorityIDType varchar2(255),
						MedicalDirectorNameTypeCode varchar2(255),
						MedicalDirectorIdentifierTypeCode varchar2(255),
						CONSTRAINT ehrlabresult_EhrLabResultNum PRIMARY KEY (EhrLabResultNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabresult_EhrLabNum ON ehrlabresult (EhrLabNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabresult_SetIdOBX ON ehrlabresult (SetIdOBX)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlab";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlab (
						EhrLabNum bigint NOT NULL auto_increment PRIMARY KEY,
						PatNum bigint NOT NULL,
						OrderControlCode varchar(255) NOT NULL,
						PlacerOrderNum varchar(255) NOT NULL,
						PlacerOrderNamespace varchar(255) NOT NULL,
						PlacerOrderUniversalID varchar(255) NOT NULL,
						PlacerOrderUniversalIDType varchar(255) NOT NULL,
						FillerOrderNum varchar(255) NOT NULL,
						FillerOrderNamespace varchar(255) NOT NULL,
						FillerOrderUniversalID varchar(255) NOT NULL,
						FillerOrderUniversalIDType varchar(255) NOT NULL,
						PlacerGroupNum varchar(255) NOT NULL,
						PlacerGroupNamespace varchar(255) NOT NULL,
						PlacerGroupUniversalID varchar(255) NOT NULL,
						PlacerGroupUniversalIDType varchar(255) NOT NULL,
						OrderingProviderID varchar(255) NOT NULL,
						OrderingProviderLName varchar(255) NOT NULL,
						OrderingProviderFName varchar(255) NOT NULL,
						OrderingProviderMiddleNames varchar(255) NOT NULL,
						OrderingProviderSuffix varchar(255) NOT NULL,
						OrderingProviderPrefix varchar(255) NOT NULL,
						OrderingProviderAssigningAuthorityNamespaceID varchar(255) NOT NULL,
						OrderingProviderAssigningAuthorityUniversalID varchar(255) NOT NULL,
						OrderingProviderAssigningAuthorityIDType varchar(255) NOT NULL,
						OrderingProviderNameTypeCode varchar(255) NOT NULL,
						OrderingProviderIdentifierTypeCode varchar(255) NOT NULL,
						SetIdOBR bigint NOT NULL,
						UsiID varchar(255) NOT NULL,
						UsiText varchar(255) NOT NULL,
						UsiCodeSystemName varchar(255) NOT NULL,
						UsiIDAlt varchar(255) NOT NULL,
						UsiTextAlt varchar(255) NOT NULL,
						UsiCodeSystemNameAlt varchar(255) NOT NULL,
						UsiTextOriginal varchar(255) NOT NULL,
						ObservationDateTimeStart varchar(255) NOT NULL,
						ObservationDateTimeEnd varchar(255) NOT NULL,
						SpecimenActionCode varchar(255) NOT NULL,
						ResultDateTime varchar(255) NOT NULL,
						ResultStatus varchar(255) NOT NULL,
						ParentObservationID varchar(255) NOT NULL,
						ParentObservationText varchar(255) NOT NULL,
						ParentObservationCodeSystemName varchar(255) NOT NULL,
						ParentObservationIDAlt varchar(255) NOT NULL,
						ParentObservationTextAlt varchar(255) NOT NULL,
						ParentObservationCodeSystemNameAlt varchar(255) NOT NULL,
						ParentObservationTextOriginal varchar(255) NOT NULL,
						ParentObservationSubID varchar(255) NOT NULL,
						ParentPlacerOrderNum varchar(255) NOT NULL,
						ParentPlacerOrderNamespace varchar(255) NOT NULL,
						ParentPlacerOrderUniversalID varchar(255) NOT NULL,
						ParentPlacerOrderUniversalIDType varchar(255) NOT NULL,
						ParentFillerOrderNum varchar(255) NOT NULL,
						ParentFillerOrderNamespace varchar(255) NOT NULL,
						ParentFillerOrderUniversalID varchar(255) NOT NULL,
						ParentFillerOrderUniversalIDType varchar(255) NOT NULL,
						ListEhrLabResultsHandlingF tinyint NOT NULL,
						ListEhrLabResultsHandlingN tinyint NOT NULL,
						TQ1SetId bigint NOT NULL,
						TQ1DateTimeStart varchar(255) NOT NULL,
						TQ1DateTimeEnd varchar(255) NOT NULL,
						INDEX(PatNum),
						INDEX(SetIdOBR),
						INDEX(TQ1SetId)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlab'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlab (
						EhrLabNum number(20) NOT NULL,
						PatNum number(20) NOT NULL,
						OrderControlCode varchar2(255),
						PlacerOrderNum varchar2(255),
						PlacerOrderNamespace varchar2(255),
						PlacerOrderUniversalID varchar2(255),
						PlacerOrderUniversalIDType varchar2(255),
						FillerOrderNum varchar2(255),
						FillerOrderNamespace varchar2(255),
						FillerOrderUniversalID varchar2(255),
						FillerOrderUniversalIDType varchar2(255),
						PlacerGroupNum varchar2(255),
						PlacerGroupNamespace varchar2(255),
						PlacerGroupUniversalID varchar2(255),
						PlacerGroupUniversalIDType varchar2(255),
						OrderingProviderID varchar2(255),
						OrderingProviderLName varchar2(255),
						OrderingProviderFName varchar2(255),
						OrderingProviderMiddleNames varchar2(255),
						OrderingProviderSuffix varchar2(255),
						OrderingProviderPrefix varchar2(255),
						OrderingProviderAssigningAuthorityNamespaceID varchar2(255),
						OrderingProviderAssigningAuthorityUniversalID varchar2(255),
						OrderingProviderAssigningAuthorityIDType varchar2(255),
						OrderingProviderNameTypeCode varchar2(255),
						OrderingProviderIdentifierTypeCode varchar2(255),
						SetIdOBR number(20) NOT NULL,
						UsiID varchar2(255),
						UsiText varchar2(255),
						UsiCodeSystemName varchar2(255),
						UsiIDAlt varchar2(255),
						UsiTextAlt varchar2(255),
						UsiCodeSystemNameAlt varchar2(255),
						UsiTextOriginal varchar2(255),
						ObservationDateTimeStart varchar2(255),
						ObservationDateTimeEnd varchar2(255),
						SpecimenActionCode varchar2(255),
						ResultDateTime varchar2(255),
						ResultStatus varchar2(255),
						ParentObservationID varchar2(255),
						ParentObservationText varchar2(255),
						ParentObservationCodeSystemName varchar2(255),
						ParentObservationIDAlt varchar2(255),
						ParentObservationTextAlt varchar2(255),
						ParentObservationCodeSystemNameAlt varchar2(255),
						ParentObservationTextOriginal varchar2(255),
						ParentObservationSubID varchar2(255),
						ParentPlacerOrderNum varchar2(255),
						ParentPlacerOrderNamespace varchar2(255),
						ParentPlacerOrderUniversalID varchar2(255),
						ParentPlacerOrderUniversalIDType varchar2(255),
						ParentFillerOrderNum varchar2(255),
						ParentFillerOrderNamespace varchar2(255),
						ParentFillerOrderUniversalID varchar2(255),
						ParentFillerOrderUniversalIDType varchar2(255),
						ListEhrLabResultsHandlingF number(3) NOT NULL,
						ListEhrLabResultsHandlingN number(3) NOT NULL,
						TQ1SetId number(20) NOT NULL,
						TQ1DateTimeStart varchar2(255),
						TQ1DateTimeEnd varchar2(255),
						CONSTRAINT ehrlab_EhrLabNum PRIMARY KEY (EhrLabNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlab_PatNum ON ehrlab (PatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlab_SetIdOBR ON ehrlab (SetIdOBR)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlab_TQ1SetId ON ehrlab (TQ1SetId)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabclinicalinfo";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabclinicalinfo (
						EhrLabClinicalInfoNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabNum bigint NOT NULL,
						ClinicalInfoID varchar(255) NOT NULL,
						ClinicalInfoText varchar(255) NOT NULL,
						ClinicalInfoCodeSystemName varchar(255) NOT NULL,
						ClinicalInfoIDAlt varchar(255) NOT NULL,
						ClinicalInfoTextAlt varchar(255) NOT NULL,
						ClinicalInfoCodeSystemNameAlt varchar(255) NOT NULL,
						ClinicalInfoTextOriginal varchar(255) NOT NULL,
						INDEX(EhrLabNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabclinicalinfo'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabclinicalinfo (
						EhrLabClinicalInfoNum number(20) NOT NULL,
						EhrLabNum number(20) NOT NULL,
						ClinicalInfoID varchar2(255),
						ClinicalInfoText varchar2(255),
						ClinicalInfoCodeSystemName varchar2(255),
						ClinicalInfoIDAlt varchar2(255),
						ClinicalInfoTextAlt varchar2(255),
						ClinicalInfoCodeSystemNameAlt varchar2(255),
						ClinicalInfoTextOriginal varchar2(255),
						CONSTRAINT ehrlabclinicalinfo_EhrLabClini PRIMARY KEY (EhrLabClinicalInfoNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabclinicalinfo_EhrLabNum ON ehrlabclinicalinfo (EhrLabNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabnote";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabnote (
						EhrLabNoteNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabNum bigint NOT NULL,
						EhrLabResultNum bigint NOT NULL,
						Comments text NOT NULL,
						INDEX(EhrLabNum),
						INDEX(EhrLabResultNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabnote'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabnote (
						EhrLabNoteNum number(20) NOT NULL,
						EhrLabNum number(20) NOT NULL,
						EhrLabResultNum number(20) NOT NULL,
						Comments clob,
						CONSTRAINT ehrlabnote_EhrLabNoteNum PRIMARY KEY (EhrLabNoteNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabnote_EhrLabNum ON ehrlabnote (EhrLabNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabnote_EhrLabResultNum ON ehrlabnote (EhrLabResultNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabresult";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabresult (
						EhrLabResultNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabNum bigint NOT NULL,
						SetIdOBX bigint NOT NULL,
						ValueType varchar(255) NOT NULL,
						ObservationIdentifierID varchar(255) NOT NULL,
						ObservationIdentifierText varchar(255) NOT NULL,
						ObservationIdentifierCodeSystemName varchar(255) NOT NULL,
						ObservationIdentifierIDAlt varchar(255) NOT NULL,
						ObservationIdentifierTextAlt varchar(255) NOT NULL,
						ObservationIdentifierCodeSystemNameAlt varchar(255) NOT NULL,
						ObservationIdentifierTextOriginal varchar(255) NOT NULL,
						ObservationIdentifierSub varchar(255) NOT NULL,
						ObservationValueCodedElementID varchar(255) NOT NULL,
						ObservationValueCodedElementText varchar(255) NOT NULL,
						ObservationValueCodedElementCodeSystemName varchar(255) NOT NULL,
						ObservationValueCodedElementIDAlt varchar(255) NOT NULL,
						ObservationValueCodedElementTextAlt varchar(255) NOT NULL,
						ObservationValueCodedElementCodeSystemNameAlt varchar(255) NOT NULL,
						ObservationValueCodedElementTextOriginal varchar(255) NOT NULL,
						ObservationValueDateTime varchar(255) NOT NULL,
						ObservationValueTime time NOT NULL DEFAULT '00:00:00',
						ObservationValueComparator varchar(255) NOT NULL,
						ObservationValueNumber1 double NOT NULL,
						ObservationValueSeparatorOrSuffix varchar(255) NOT NULL,
						ObservationValueNumber2 double NOT NULL,
						ObservationValueNumeric double NOT NULL,
						ObservationValueText varchar(255) NOT NULL,
						UnitsID varchar(255) NOT NULL,
						UnitsText varchar(255) NOT NULL,
						UnitsCodeSystemName varchar(255) NOT NULL,
						UnitsIDAlt varchar(255) NOT NULL,
						UnitsTextAlt varchar(255) NOT NULL,
						UnitsCodeSystemNameAlt varchar(255) NOT NULL,
						UnitsTextOriginal varchar(255) NOT NULL,
						referenceRange varchar(255) NOT NULL,
						AbnormalFlags varchar(255) NOT NULL,
						ObservationResultStatus varchar(255) NOT NULL,
						ObservationDateTime varchar(255) NOT NULL,
						AnalysisDateTime varchar(255) NOT NULL,
						PerformingOrganizationName varchar(255) NOT NULL,
						PerformingOrganizationNameAssigningAuthorityNamespaceId varchar(255) NOT NULL,
						PerformingOrganizationNameAssigningAuthorityUniversalId varchar(255) NOT NULL,
						PerformingOrganizationNameAssigningAuthorityUniversalIdType varchar(255) NOT NULL,
						PerformingOrganizationIdentifierTypeCode varchar(255) NOT NULL,
						PerformingOrganizationIdentifier varchar(255) NOT NULL,
						PerformingOrganizationAddressStreet varchar(255) NOT NULL,
						PerformingOrganizationAddressOtherDesignation varchar(255) NOT NULL,
						PerformingOrganizationAddressCity varchar(255) NOT NULL,
						PerformingOrganizationAddressStateOrProvince varchar(255) NOT NULL,
						PerformingOrganizationAddressZipOrPostalCode varchar(255) NOT NULL,
						PerformingOrganizationAddressCountryCode varchar(255) NOT NULL,
						PerformingOrganizationAddressAddressType varchar(255) NOT NULL,
						PerformingOrganizationAddressCountyOrParishCode varchar(255) NOT NULL,
						MedicalDirectorID varchar(255) NOT NULL,
						MedicalDirectorLName varchar(255) NOT NULL,
						MedicalDirectorFName varchar(255) NOT NULL,
						MedicalDirectorMiddleNames varchar(255) NOT NULL,
						MedicalDirectorSuffix varchar(255) NOT NULL,
						MedicalDirectorPrefix varchar(255) NOT NULL,
						MedicalDirectorAssigningAuthorityNamespaceID varchar(255) NOT NULL,
						MedicalDirectorAssigningAuthorityUniversalID varchar(255) NOT NULL,
						MedicalDirectorAssigningAuthorityIDType varchar(255) NOT NULL,
						MedicalDirectorNameTypeCode varchar(255) NOT NULL,
						MedicalDirectorIdentifierTypeCode varchar(255) NOT NULL,
						INDEX(EhrLabNum),
						INDEX(SetIdOBX)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabresult'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabresult (
						EhrLabResultNum number(20) NOT NULL,
						EhrLabNum number(20) NOT NULL,
						SetIdOBX number(20) NOT NULL,
						ValueType varchar2(255),
						ObservationIdentifierID varchar2(255),
						ObservationIdentifierText varchar2(255),
						ObservationIdentifierCodeSystemName varchar2(255),
						ObservationIdentifierIDAlt varchar2(255),
						ObservationIdentifierTextAlt varchar2(255),
						ObservationIdentifierCodeSystemNameAlt varchar2(255),
						ObservationIdentifierTextOriginal varchar2(255),
						ObservationIdentifierSub varchar2(255),
						ObservationValueCodedElementID varchar2(255),
						ObservationValueCodedElementText varchar2(255),
						ObservationValueCodedElementCodeSystemName varchar2(255),
						ObservationValueCodedElementIDAlt varchar2(255),
						ObservationValueCodedElementTextAlt varchar2(255),
						ObservationValueCodedElementCodeSystemNameAlt varchar2(255),
						ObservationValueCodedElementTextOriginal varchar2(255),
						ObservationValueDateTime varchar2(255),
						ObservationValueTime date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						ObservationValueComparator varchar2(255),
						ObservationValueNumber1 number(38,8) NOT NULL,
						ObservationValueSeparatorOrSuffix varchar2(255),
						ObservationValueNumber2 number(38,8) NOT NULL,
						ObservationValueNumeric number(38,8) NOT NULL,
						ObservationValueText varchar2(255),
						UnitsID varchar2(255),
						UnitsText varchar2(255),
						UnitsCodeSystemName varchar2(255),
						UnitsIDAlt varchar2(255),
						UnitsTextAlt varchar2(255),
						UnitsCodeSystemNameAlt varchar2(255),
						UnitsTextOriginal varchar2(255),
						referenceRange varchar2(255),
						AbnormalFlags varchar2(255),
						ObservationResultStatus varchar2(255),
						ObservationDateTime varchar2(255),
						AnalysisDateTime varchar2(255),
						PerformingOrganizationName varchar2(255),
						PerformingOrganizationNameAssigningAuthorityNamespaceId varchar2(255),
						PerformingOrganizationNameAssigningAuthorityUniversalId varchar2(255),
						PerformingOrganizationNameAssigningAuthorityUniversalIdType varchar2(255),
						PerformingOrganizationIdentifierTypeCode varchar2(255),
						PerformingOrganizationIdentifier varchar2(255),
						PerformingOrganizationAddressStreet varchar2(255),
						PerformingOrganizationAddressOtherDesignation varchar2(255),
						PerformingOrganizationAddressCity varchar2(255),
						PerformingOrganizationAddressStateOrProvince varchar2(255),
						PerformingOrganizationAddressZipOrPostalCode varchar2(255),
						PerformingOrganizationAddressCountryCode varchar2(255),
						PerformingOrganizationAddressAddressType varchar2(255),
						PerformingOrganizationAddressCountyOrParishCode varchar2(255),
						MedicalDirectorID varchar2(255),
						MedicalDirectorLName varchar2(255),
						MedicalDirectorFName varchar2(255),
						MedicalDirectorMiddleNames varchar2(255),
						MedicalDirectorSuffix varchar2(255),
						MedicalDirectorPrefix varchar2(255),
						MedicalDirectorAssigningAuthorityNamespaceID varchar2(255),
						MedicalDirectorAssigningAuthorityUniversalID varchar2(255),
						MedicalDirectorAssigningAuthorityIDType varchar2(255),
						MedicalDirectorNameTypeCode varchar2(255),
						MedicalDirectorIdentifierTypeCode varchar2(255),
						CONSTRAINT ehrlabresult_EhrLabResultNum PRIMARY KEY (EhrLabResultNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabresult_EhrLabNum ON ehrlabresult (EhrLabNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabresult_SetIdOBX ON ehrlabresult (SetIdOBX)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabresultscopyto";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabresultscopyto (
						EhrLabResultsCopyToNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabNum bigint NOT NULL,
						CopyToID varchar(255) NOT NULL,
						CopyToLName varchar(255) NOT NULL,
						CopyToFName varchar(255) NOT NULL,
						CopyToMiddleNames varchar(255) NOT NULL,
						CopyToSuffix varchar(255) NOT NULL,
						CopyToPrefix varchar(255) NOT NULL,
						CopyToAssigningAuthorityNamespaceID varchar(255) NOT NULL,
						CopyToAssigningAuthorityUniversalID varchar(255) NOT NULL,
						CopyToAssigningAuthorityIDType varchar(255) NOT NULL,
						CopyToNameTypeCode varchar(255) NOT NULL,
						CopyToIdentifierTypeCode varchar(255) NOT NULL,
						INDEX(EhrLabNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabresultscopyto'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabresultscopyto (
						EhrLabResultsCopyToNum number(20) NOT NULL,
						EhrLabNum number(20) NOT NULL,
						CopyToID varchar2(255),
						CopyToLName varchar2(255),
						CopyToFName varchar2(255),
						CopyToMiddleNames varchar2(255),
						CopyToSuffix varchar2(255),
						CopyToPrefix varchar2(255),
						CopyToAssigningAuthorityNamespaceID varchar2(255),
						CopyToAssigningAuthorityUniversalID varchar2(255),
						CopyToAssigningAuthorityIDType varchar2(255),
						CopyToNameTypeCode varchar2(255),
						CopyToIdentifierTypeCode varchar2(255),
						CONSTRAINT ehrlabresultscopyto_EhrLabResu PRIMARY KEY (EhrLabResultsCopyToNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabresultscopyto_EhrLabNum ON ehrlabresultscopyto (EhrLabNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabspecimen";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabspecimen (
						EhrLabSpecimenNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabNum bigint NOT NULL,
						SetIdSPM bigint NOT NULL,
						SpecimenTypeID varchar(255) NOT NULL,
						SpecimenTypeText varchar(255) NOT NULL,
						SpecimenTypeCodeSystemName varchar(255) NOT NULL,
						SpecimenTypeIDAlt varchar(255) NOT NULL,
						SpecimenTypeTextAlt varchar(255) NOT NULL,
						SpecimenTypeCodeSystemNameAlt varchar(255) NOT NULL,
						SpecimenTypeTextOriginal varchar(255) NOT NULL,
						CollectionDateTimeStart varchar(255) NOT NULL,
						CollectionDateTimeEnd varchar(255) NOT NULL,
						INDEX(EhrLabNum),
						INDEX(SetIdSPM)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabspecimen'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabspecimen (
						EhrLabSpecimenNum number(20) NOT NULL,
						EhrLabNum number(20) NOT NULL,
						SetIdSPM number(20) NOT NULL,
						SpecimenTypeID varchar2(255),
						SpecimenTypeText varchar2(255),
						SpecimenTypeCodeSystemName varchar2(255),
						SpecimenTypeIDAlt varchar2(255),
						SpecimenTypeTextAlt varchar2(255),
						SpecimenTypeCodeSystemNameAlt varchar2(255),
						SpecimenTypeTextOriginal varchar2(255),
						CollectionDateTimeStart varchar2(255),
						CollectionDateTimeEnd varchar2(255),
						CONSTRAINT ehrlabspecimen_EhrLabSpecimenN PRIMARY KEY (EhrLabSpecimenNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabspecimen_EhrLabNum ON ehrlabspecimen (EhrLabNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabspecimen_SetIdSPM ON ehrlabspecimen (SetIdSPM)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabspecimencondition";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabspecimencondition (
						EhrLabSpecimenConditionNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabSpecimenNum bigint NOT NULL,
						SpecimenConditionID varchar(255) NOT NULL,
						SpecimenConditionText varchar(255) NOT NULL,
						SpecimenConditionCodeSystemName varchar(255) NOT NULL,
						SpecimenConditionIDAlt varchar(255) NOT NULL,
						SpecimenConditionTextAlt varchar(255) NOT NULL,
						SpecimenConditionCodeSystemNameAlt varchar(255) NOT NULL,
						SpecimenConditionTextOriginal varchar(255) NOT NULL,
						INDEX(EhrLabSpecimenNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabspecimencondition'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabspecimencondition (
						EhrLabSpecimenConditionNum number(20) NOT NULL,
						EhrLabSpecimenNum number(20) NOT NULL,
						SpecimenConditionID varchar2(255),
						SpecimenConditionText varchar2(255),
						SpecimenConditionCodeSystemName varchar2(255),
						SpecimenConditionIDAlt varchar2(255),
						SpecimenConditionTextAlt varchar2(255),
						SpecimenConditionCodeSystemNameAlt varchar2(255),
						SpecimenConditionTextOriginal varchar2(255),
						CONSTRAINT ehrlabspecimencondition_EhrLab PRIMARY KEY (EhrLabSpecimenConditionNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabspecimencondition_EhrLab ON ehrlabspecimencondition (EhrLabSpecimenNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrlabspecimenrejectreason";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabspecimenrejectreason (
						EhrLabSpecimenRejectReasonNum bigint NOT NULL auto_increment PRIMARY KEY,
						EhrLabSpecimenNum bigint NOT NULL,
						SpecimenRejectReasonID varchar(255) NOT NULL,
						SpecimenRejectReasonText varchar(255) NOT NULL,
						SpecimenRejectReasonCodeSystemName varchar(255) NOT NULL,
						SpecimenRejectReasonIDAlt varchar(255) NOT NULL,
						SpecimenRejectReasonTextAlt varchar(255) NOT NULL,
						SpecimenRejectReasonCodeSystemNameAlt varchar(255) NOT NULL,
						SpecimenRejectReasonTextOriginal varchar(255) NOT NULL,
						INDEX(EhrLabSpecimenNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrlabspecimenrejectreason'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrlabspecimenrejectreason (
						EhrLabSpecimenRejectReasonNum number(20) NOT NULL,
						EhrLabSpecimenNum number(20) NOT NULL,
						SpecimenRejectReasonID varchar2(255),
						SpecimenRejectReasonText varchar2(255),
						SpecimenRejectReasonCodeSystemName varchar2(255),
						SpecimenRejectReasonIDAlt varchar2(255),
						SpecimenRejectReasonTextAlt varchar2(255),
						SpecimenRejectReasonCodeSystemNameAlt varchar2(255),
						SpecimenRejectReasonTextOriginal varchar2(255),
						CONSTRAINT ehrlabspecimenrejectreason_Ehr PRIMARY KEY (EhrLabSpecimenRejectReasonNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrlabspecimenrejectreason_Ehr ON ehrlabspecimenrejectreason (EhrLabSpecimenNum)";
					Db.NonQ(command);
				}
				#endregion
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE guardian ADD IsGuardian tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE guardian ADD IsGuardian number(3)";
					Db.NonQ(command);
					command="UPDATE guardian SET IsGuardian = 0 WHERE IsGuardian IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE guardian MODIFY IsGuardian NOT NULL";
					Db.NonQ(command);
				}
				command="UPDATE guardian SET IsGuardian=1";//Works for both MySQL and Oracle.
				Db.NonQ(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE allergydef ADD UniiCode varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE allergydef ADD UniiCode varchar2(255)";
					Db.NonQ(command);
				}
				//Oracle compatible.
				command="ALTER TABLE allergydef DROP COLUMN SnomedAllergyTo";
				Db.NonQ(command);
				//OID External
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS oidexternal";
					Db.NonQ(command);
					command=@"CREATE TABLE oidexternal (
						OIDExternalNum bigint NOT NULL auto_increment PRIMARY KEY,
						IDType varchar(255) NOT NULL,
						IDInternal bigint NOT NULL,
						IDExternal varchar(255) NOT NULL,
						rootExternal varchar(255) NOT NULL,
						INDEX(IDType,IDInternal),
						INDEX(rootExternal(62),IDExternal(62))
						) DEFAULT CHARSET=utf8";//Index is 1000/8=125/n where n is the number of columns to be indexed together. In this case the result is 62.5=62
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE oidexternal'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE oidexternal (
						OIDExternalNum number(20) NOT NULL,
						IDType varchar2(255),
						IDInternal number(20),
						IDExternal varchar2(255),
						rootExternal varchar2(255),
						CONSTRAINT oidexternal_OIDExternalNum PRIMARY KEY (OIDExternalNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX oidexternal_type_ID ON oidexternal (IDType, IDInternal)";
					Db.NonQ(command);
					command=@"CREATE INDEX oidexternal_root_extension ON oidexternal (rootExternal, IDExternal)";
					Db.NonQ(command);
				}
				//OID Internal
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS oidinternal";
					Db.NonQ(command);
					command=@"CREATE TABLE oidinternal (
						OIDInternalNum bigint NOT NULL auto_increment PRIMARY KEY,
						IDType varchar(255) NOT NULL,
						IDRoot varchar(255) NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE oidinternal'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE oidinternal (
						OIDInternalNum number(20) NOT NULL,
						IDType varchar2(255),
						IDRoot varchar2(255),
						CONSTRAINT oidinternal_OIDInternalNum PRIMARY KEY (OIDInternalNum)
						)";
					Db.NonQ(command);
				}				
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD FilledCity varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD FilledCity varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD FilledST varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD FilledST varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD CompletionStatus tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD CompletionStatus number(3)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET CompletionStatus = 0 WHERE CompletionStatus IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY CompletionStatus NOT NULL";
					Db.NonQ(command);
				}
				//MySQL and Oracle
				command="UPDATE vaccinepat SET CompletionStatus=CASE WHEN NotGiven=1 THEN 2 ELSE 0 END";//If was NotGiven then CompletionStatus=NotAdministered, otherwise CompletionStatus=Complete.
				Db.NonQ(command);
				//MySQL and Oracle
				command="ALTER TABLE vaccinepat DROP COLUMN NotGiven";
				Db.NonQ(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD AdministrationNoteCode tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD AdministrationNoteCode number(3)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET AdministrationNoteCode = 0 WHERE AdministrationNoteCode IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY AdministrationNoteCode NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD UserNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat ADD INDEX (UserNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD UserNum number(20)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET UserNum = 0 WHERE UserNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY UserNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX vaccinepat_UserNum ON vaccinepat (UserNum)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD ProvNumOrdering bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat ADD INDEX (ProvNumOrdering)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD ProvNumOrdering number(20)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET ProvNumOrdering = 0 WHERE ProvNumOrdering IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY ProvNumOrdering NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX vaccinepat_ProvNumOrdering ON vaccinepat (ProvNumOrdering)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD ProvNumAdminister bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat ADD INDEX (ProvNumAdminister)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD ProvNumAdminister number(20)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET ProvNumAdminister = 0 WHERE ProvNumAdminister IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY ProvNumAdminister NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX vaccinepat_ProvNumAdminister ON vaccinepat (ProvNumAdminister)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD DateExpire date NOT NULL DEFAULT '0001-01-01'";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD DateExpire date";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET DateExpire = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateExpire IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY DateExpire NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD RefusalReason tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD RefusalReason number(3)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET RefusalReason = 0 WHERE RefusalReason IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY RefusalReason NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD ActionCode tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD ActionCode number(3)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET ActionCode = 0 WHERE ActionCode IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY ActionCode NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD AdministrationRoute tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD AdministrationRoute number(3)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET AdministrationRoute = 0 WHERE AdministrationRoute IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY AdministrationRoute NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vaccinepat ADD AdministrationSite tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vaccinepat ADD AdministrationSite number(3)";
					Db.NonQ(command);
					command="UPDATE vaccinepat SET AdministrationSite = 0 WHERE AdministrationSite IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vaccinepat MODIFY AdministrationSite NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS vaccineobs";
					Db.NonQ(command);
					command=@"CREATE TABLE vaccineobs (
						VaccineObsNum bigint NOT NULL auto_increment PRIMARY KEY,
						VaccinePatNum bigint NOT NULL,
						ValType tinyint NOT NULL,
						IdentifyingCode tinyint NOT NULL,
						ValReported varchar(255) NOT NULL,
						ValCodeSystem tinyint NOT NULL,
						VaccineObsNumGroup bigint NOT NULL,
						ValUnit varchar(255) NOT NULL,
						DateObs date NOT NULL DEFAULT '0001-01-01',
						MethodCode varchar(255) NOT NULL,
						INDEX(VaccinePatNum),
						INDEX(VaccineObsNumGroup)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE vaccineobs'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE vaccineobs (
						VaccineObsNum number(20) NOT NULL,
						VaccinePatNum number(20) NOT NULL,
						ValType number(3) NOT NULL,
						IdentifyingCode number(3) NOT NULL,
						ValReported varchar2(255),
						ValCodeSystem number(3) NOT NULL,
						VaccineObsNumGroup number(20) NOT NULL,
						ValUnit varchar2(255),
						DateObs date DEFAULT TO_DATE('0001-01-01','YYYY-MM-DD') NOT NULL,
						MethodCode varchar2(255),
						CONSTRAINT vaccineobs_VaccineObsNum PRIMARY KEY (VaccineObsNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX vaccineobs_VaccinePatNum ON vaccineobs (VaccinePatNum)";
					Db.NonQ(command);
					command=@"CREATE INDEX vaccineobs_VaccineObsNumGroup ON vaccineobs (VaccineObsNumGroup)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE ehrmeasureevent ADD FKey bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE ehrmeasureevent ADD INDEX (FKey)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE ehrmeasureevent ADD FKey number(20)";
					Db.NonQ(command);
					command="UPDATE ehrmeasureevent SET FKey = 0 WHERE FKey IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE ehrmeasureevent MODIFY FKey NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrmeasureevent_FKey ON ehrmeasureevent (FKey)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE vitalsign ADD BMIPercentile int NOT NULL";
					Db.NonQ(command);
					command="UPDATE vitalsign SET BMIPercentile=-1";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE vitalsign ADD BMIPercentile number(11)";
					Db.NonQ(command);
					command="UPDATE vitalsign SET BMIPercentile = -1 WHERE BMIPercentile IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE vitalsign MODIFY BMIPercentile NOT NULL";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE procedurelog ADD SnomedBodySite varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE procedurelog ADD SnomedBodySite varchar2(255)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehrpatient";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrpatient (
						PatNum bigint NOT NULL PRIMARY KEY,
						MotherMaidenFname varchar(255) NOT NULL,
						MotherMaidenLname varchar(255) NOT NULL,
						VacShareOk tinyint NOT NULL
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehrpatient'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehrpatient (
						PatNum number(20) NOT NULL,
						MotherMaidenFname varchar2(255),
						MotherMaidenLname varchar2(255),
						VacShareOk number(3) NOT NULL,
						CONSTRAINT ehrpatient_PatNum PRIMARY KEY (PatNum)
						)";
					Db.NonQ(command);
				}
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS ehraptobs";
					Db.NonQ(command);
					command=@"CREATE TABLE ehraptobs (
						EhrAptObsNum bigint NOT NULL auto_increment PRIMARY KEY,
						AptNum bigint NOT NULL,
						ValType tinyint NOT NULL,
						LoincCode varchar(255) NOT NULL,
						ValReported varchar(255) NOT NULL,
						ValUnit varchar(255) NOT NULL,
						ValCodeSystem varchar(255) NOT NULL,
						INDEX(AptNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle
					command="BEGIN EXECUTE IMMEDIATE 'DROP TABLE ehraptobs'; EXCEPTION WHEN OTHERS THEN NULL; END;";
					Db.NonQ(command);
					command=@"CREATE TABLE ehraptobs (
						EhrAptObsNum number(20) NOT NULL,
						AptNum number(20) NOT NULL,
						ValType number(3) NOT NULL,
						LoincCode varchar2(255),
						ValReported varchar2(255),
						ValUnit varchar2(255),
						ValCodeSystem varchar2(255),
						CONSTRAINT ehraptobs_EhrAptObsNum PRIMARY KEY (EhrAptObsNum)
						)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehraptobs_AptNum ON ehraptobs (AptNum)";
					Db.NonQ(command);
				}


				command="UPDATE preference SET ValueString = '14.1.0.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			//To14_2_0();
		}


		





	}
}



