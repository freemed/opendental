using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	public partial class ConvertDatabases {
		public static System.Version LatestVersion=new Version("13.2.0.0");//This value must be changed when a new conversion is to be triggered.

		private static void To13_2_0() {
			if(FromVersion<new Version("13.2.0.0")) {
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
						command="INSERT INTO grouppermission (GroupPermNum,NewerDate,UserGroupNum,PermType) "
						+"VALUES((SELECT MAX(GroupPermNum)+1 FROM grouppermission),'0001-01-01',"+POut.Long(groupNum)+",67)";//POut.Int((int)Permissions.WikiListSetup)
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
				}
				else {//oracle
					command="ALTER TABLE repeatcharge ADD IsEnabled number(3)";
					Db.NonQ(command);
					command="UPDATE repeatcharge SET IsEnabled = 0 WHERE IsEnabled IS NULL";
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
				command="UPDATE patient SET SmokingSnoMed='449868002' WHERE SmokeStatus=5";//+POut.Int((int)SmokingStatus.CurrentEveryDay_Recode1);
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='428041000124106' WHERE SmokeStatus=4";//+POut.Int((int)SmokingStatus.CurrentSomeDay_Recode2);
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='8517006' WHERE SmokeStatus=3";//+POut.Int((int)SmokingStatus.FormerSmoker_Recode3);
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='266919005' WHERE SmokeStatus=2";//+POut.Int((int)SmokingStatus.NeverSmoked_Recode4);
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='77176002' WHERE SmokeStatus=1";//+POut.Int((int)SmokingStatus.SmokerUnknownCurrent_Recode5);
				Db.NonQ(command);
				command="UPDATE patient SET SmokingSnoMed='266927001' WHERE SmokeStatus=0";//+POut.Int((int)SmokingStatus.UnknownIfEver_Recode9);
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
				command="SELECT MAX(ItemOrder) FROM diseasedef";
				int itemOrderCur=PIn.Int(Db.GetScalar(command));
				command="SELECT DISTINCT Description,ICD9Code,icd9.ICD9Num "
					+"FROM icd9,eduresource,disease,reminderrule "
					+"WHERE icd9.ICD9Num=eduresource.ICD9Num "
					+"OR icd9.ICD9Num=disease.ICD9Num "
					+"OR (ReminderCriterion=6 AND icd9.ICD9Num=CriterionFK)";//6=ICD9
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					itemOrderCur++;
					if(DataConnection.DBtype==DatabaseType.MySql) {
						command="INSERT INTO diseasedef(DiseaseName,ItemOrder,ICD9Code) VALUES('"
							+POut.String(table.Rows[i]["Description"].ToString())+"',"+POut.Int(itemOrderCur)+",'"+POut.String(table.Rows[i]["ICD9Code"].ToString())+"')";
						Db.NonQ(command);
					}
					else {//oracle
						command="INSERT INTO diseasedef(DiseaseDefNum,DiseaseName,ItemOrder,ICD9Code) VALUES((SELECT MAX(DiseaseDefNum)+1 FROM diseasedef),'"
							+POut.String(table.Rows[i]["Description"].ToString())+"',"+POut.Int(itemOrderCur)+",'"+POut.String(table.Rows[i]["ICD9Code"].ToString())+"')";
						Db.NonQ(command);
					}
					long defNum=Db.NonQ(command,true);
					command="UPDATE eduresource SET DiseaseDefNum="+POut.Long(defNum)+" WHERE ICD9Num="+table.Rows[i]["ICD9Num"].ToString();
					Db.NonQ(command);
					command="UPDATE disease SET DiseaseDefNum="+POut.Long(defNum)+" WHERE ICD9Num="+table.Rows[i]["ICD9Num"].ToString();
					Db.NonQ(command);
					command="UPDATE reminderrule SET CriterionFK="+POut.Long(defNum)+" WHERE CriterionFK="+table.Rows[i]["ICD9Num"].ToString()+" AND ReminderCriterion=6";
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










				command="UPDATE preference SET ValueString = '13.2.0.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			//To13_3_0();
		}





	}
}







