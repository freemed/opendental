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
					command="ALTER TABLE emailmessage MODIFY (BodyText clob NOT NULL)";
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
					command=@"CREATE INDEX administrativesex_CodeValue ON intervention (CodeValue)";
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
					command=@"CREATE INDEX patientrace_CdcrecCode ON intervention (CdcrecCode)";
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
					command=@"CREATE INDEX ehrnotperformed_CodeValueReason ON ehrnotperformed (CodeValueReason)";
					Db.NonQ(command);
					command=@"CREATE INDEX ehrnotperformed_CodeValueReason ON ehrnotperformed (CodeValueReason)";
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
					command="ALTER TABLE allergydef CHANGE Snomed SnomedType int";
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
						Uid varchar2(4000),
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
						SnomedList varchar(255) NOT NULL,
						Icd9List varchar(255) NOT NULL,
						Icd10List varchar(255) NOT NULL,
						CvxList varchar(255) NOT NULL,
						RxCuiList varchar(255) NOT NULL,
						LoincList varchar(255) NOT NULL,
						DemographicAgeLessThan int NOT NULL,
						DemographicAgeGreaterThan int NOT NULL,
						DemographicGender varchar(255) NOT NULL,
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
						SnomedList varchar2(255),
						Icd9List varchar2(255),
						Icd10List varchar2(255),
						CvxList varchar2(255),
						RxCuiList varchar2(255),
						LoincList varchar2(255),
						DemographicAgeLessThan number(11) NOT NULL,
						DemographicAgeGreaterThan number(11) NOT NULL,
						DemographicGender varchar2(255),
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




				command="UPDATE preference SET ValueString = '14.1.0.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			//To14_2_0();
		}





	}
}





