using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	//The other file was simply getting too big.  It was bogging down VS speed.
	///<summary></summary>
	public partial class ClassConvertDatabase {
		private System.Version LatestVersion=new Version("6.4.0.0");//This value must be changed when a new conversion is to be triggered.

		private void To6_0_2() {
			if(FromVersion<new Version("6.0.2.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypesShowingInList','1,3','Comma-delimited list. FK to recalltype.RecallTypeNum.')";//1=prophy,3=perio
				General.NonQ(command);
				command="UPDATE preference SET ValueString = '6.0.2.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_1_1();
		}

		private void To6_1_1() {
			if(FromVersion<new Version("6.1.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS plannedappt";
					General.NonQ(command);
					command=@"CREATE TABLE plannedappt (
						PlannedApptNum int NOT NULL auto_increment,
						PatNum int NOT NULL,
						AptNum int NOT NULL,
						ItemOrder int NOT NULL,
						PRIMARY KEY (PlannedApptNum),
						INDEX (PatNum),
						INDEX (AptNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="SELECT PatNum,NextAptNum FROM patient WHERE NextAptNum != 0";
					DataTable table=General.GetTable(command);
					for(int i=0;i<table.Rows.Count;i++) {
						command="INSERT INTO plannedappt (PatNum,AptNum,ItemOrder) VALUES("
							+table.Rows[i]["PatNum"].ToString()+","
							+table.Rows[i]["NextAptNum"].ToString()+",0)";
						General.NonQ(command);
					}
					command="ALTER TABLE patient DROP NextAptNum";
					General.NonQ(command);
					//Billing charges------------------------------------------------------------------------------------------
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('BillingChargeOrFinanceIsDefault', 'Finance','Value is a string, either Billing or Finance.')";
					General.NonQ(command);
					command="SELECT Max(ItemOrder) FROM definition WHERE Category=1";
					table=General.GetTable(command);
					int billingchargeItemOrder=PIn.PInt(table.Rows[0][0].ToString())+1;
					command="INSERT INTO definition (category,itemorder,itemname,itemvalue) VALUES("
						+"1, "//category=AdjTypes
						+"'"+POut.PInt(billingchargeItemOrder)+"', "//itemOrder
						+"'Billing Charge', "//itemname
						+"'+')";//itemValue
					int numAdj=General.NonQ(command,true);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('BillingChargeAdjustmentType', "
						+"'"+POut.PInt(numAdj)+"','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('BillingChargeLastRun', '0001-01-01','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('BillingChargeAmount', '2','')";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS scheduleop";
					General.NonQ(command);
					command=@"CREATE TABLE scheduleop (
						ScheduleOpNum int NOT NULL auto_increment,
						ScheduleNum int NOT NULL,
						OperatoryNum int NOT NULL,
						PRIMARY KEY (ScheduleOpNum),
						INDEX (ScheduleNum),
						INDEX (OperatoryNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//conversion to new operatory paradigm for blockouts-------------------------------------------------------------------
					//get all visible ops
					command="SELECT OperatoryNum FROM operatory WHERE IsHidden=0";
					table=General.GetTable(command);
					List<int> visibleOps=new List<int>();
					for(int i=0;i<table.Rows.Count;i++) {
						visibleOps.Add(PIn.PInt(table.Rows[i]["OperatoryNum"].ToString()));
					}
					//convert blockouts with op=0
					command="SELECT ScheduleNum FROM schedule WHERE SchedType=2 "//blockout
						+"AND Op=0";//indicates all ops
					table=General.GetTable(command);
					for(int i=0;i<table.Rows.Count;i++) {
						//for each schedule, we need to insert a separate scheduleop for each visible op
						for(int o=0;o<visibleOps.Count;o++) {
							command="INSERT INTO scheduleop(ScheduleNum,OperatoryNum) VALUES("
								+table.Rows[i]["ScheduleNum"].ToString()+","
								+POut.PInt(visibleOps[o])+")";
							General.NonQ(command);
						}
					}
					//convert blockouts with op>0
					command="SELECT ScheduleNum,Op FROM schedule WHERE SchedType=2 "//blockout
						+"AND Op>0";//indicates one assigned op
					table=General.GetTable(command);
					for(int i=0;i<table.Rows.Count;i++) {
						command="INSERT INTO scheduleop(ScheduleNum,OperatoryNum) VALUES("
							+table.Rows[i]["ScheduleNum"].ToString()+","
							+table.Rows[i]["Op"].ToString()+")";
						General.NonQ(command);
					}
					command="ALTER TABLE schedule DROP Op";
					General.NonQ(command);
					//Fee schedule name conversion-------------------------------------------------------------------------------
					command="DROP TABLE IF EXISTS feesched";
					General.NonQ(command);
					command=@"CREATE TABLE feesched (
						FeeSchedNum int NOT NULL auto_increment,
						Description varchar(255),
						FeeSchedType int NOT NULL,
						ItemOrder int NOT NULL,
						IsHidden tinyint(1) NOT NULL,
						PRIMARY KEY (FeeSchedNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="SELECT DefNum,ItemName,ItemValue,ItemOrder,IsHidden FROM definition WHERE Category=7";//fee schedule names
					table=General.GetTable(command);
					for(int i=0;i<table.Rows.Count;i++) {
						command="INSERT INTO feesched(FeeSchedNum,Description,FeeSchedType,ItemOrder,IsHidden) VALUES("
							+table.Rows[i]["DefNum"].ToString()+","
							+"'"+POut.PString(table.Rows[i]["ItemName"].ToString())+"',";
						if(table.Rows[i]["ItemValue"].ToString()=="A") {
							command+=POut.PInt((int)FeeScheduleType.Allowed)+",";
						}
						else if(table.Rows[i]["ItemValue"].ToString()=="C") {
							command+=POut.PInt((int)FeeScheduleType.CoPay)+",";
						}
						else {
							command+=POut.PInt((int)FeeScheduleType.Normal)+",";
						}
						command+=table.Rows[i]["ItemOrder"].ToString()+","//although this will be reset in the UI
							+table.Rows[i]["IsHidden"].ToString()+")";
						General.NonQ(command);
					}
					command="DELETE FROM definition WHERE Category=7";
					General.NonQ(command);
					//end of fee schedule
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('AllowedFeeSchedsAutomate','0','0 or 1')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('BackupReminderLastDateRun','0001-01-01','')";
					General.NonQ(command);
					//Anesthesia Module Conversions-----------------------------------------------------
					//field to toggle Anesthesia Module on or off. Turned 'off' by default
					command = "INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('EnableAnesthMod', '0','0 or 1, Toggles Anesthesia Module Off and On. Disabled (0) by default')";
					General.NonQ(command);
					//individual unique records of delivered anesthetics
					command="DROP TABLE IF EXISTS anestheticrecord";
					General.NonQ(command);
					command = @"CREATE TABLE anestheticrecord (
						AnestheticRecordNum int(11) NOT NULL auto_increment,
						PatNum int(11) NOT NULL,
						AnestheticDate datetime NOT NULL,
						ProvNum smallint(5) NOT NULL,
						PRIMARY KEY (AnestheticRecordNum),
						INDEX (PatNum),
						INDEX (ProvNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//data recorded for an individual anesthetic on a given date and time
					command="DROP TABLE IF EXISTS anestheticdata";
					General.NonQ(command);
					command = @"CREATE TABLE anestheticdata (
						AnestheticDataNum int(11) NOT NULL auto_increment,
						AnestheticRecordNum int(11) NOT NULL,
						AnesthOpen char(32),
						AnesthClose char(32),
						SurgOpen char(32),
						SurgClose char(32),
						Anesthetist char(32) NOT NULL,
						Surgeon char(32) NOT NULL,						
						Asst char(32) NOT NULL,
						Circulator char(32) NOT NULL,						
						VSMName char(20) NOT NULL,
						VSMSerNum char(20) NOT NULL,					
						ASA char(3) NOT NULL,
						ASA_EModifier char(1) NOT NULL,					
						InhO2 tinyint(1) NOT NULL,
						InhN2O tinyint(1) NOT NULL,						
						O2LMin smallint(1) NOT NULL,
						N2OLMin smallint(1) NOT NULL,						
						RteNasCan tinyint(1) NOT NULL,
						RteNasHood tinyint(1) NOT NULL,
						RteETT tinyint(1) NOT NULL,
						MedRouteIVCath tinyint(1) NOT NULL,
						MedRouteIVButtFly tinyint(1) NOT NULL,
						MedRouteIM tinyint(1) NOT NULL,
						MedRoutePO tinyint(1) NOT NULL,
						MedRouteNasal tinyint(1) NOT NULL,
						MedRouteRectal tinyint(1) NOT NULL,
						IVSite char(16) NOT NULL,
						IVGauge smallint(2) NOT NULL,
						IVSideR smallint(2) NOT NULL,
						IVSideL smallint(2) NOT NULL,
						IVAtt smallint(1) NOT NULL,
						IVF char(8) NOT NULL,
						IVFVol int(5) NOT NULL,
						MonBP tinyint(1) NOT NULL,
						MonSpO2 tinyint(1)NOT NULL,
						MonEtCO2 tinyint(1) NOT NULL,
						MonTemp tinyint(1) NOT NULL,
						MonPrecordial tinyint(1) NOT NULL,
						MonEKG tinyint(1) NOT NULL,
						Notes text NOT NULL,
						PatWgt smallint(3) NOT NULL,
						WgtUnitsLbs tinyint(1) NOT NULL,
						WgtUnitsKgs tinyint(1) NOT NULL,
						PatHgt char(10) NOT NULL,
						EscortName char(32) NOT NULL,
						EscortCellNum char(13) NOT NULL,
						EscortRel char(16) NOT NULL,
						NPOTime char(5) NOT NULL,
						SigIsTopaz tinyint(3) NOT NULL,
						Signature text NOT NULL,
						PRIMARY KEY (AnestheticDataNum),
						INDEX (AnestheticRecordNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//a list of anesthetic medications to be delivered to a patient
					command="DROP TABLE IF EXISTS anesthmedsgiven";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsgiven(
						AnestheticMedNum int(3) NOT NULL auto_increment,
						AnestheticRecordNum int(11) NOT NULL,
						AnesthMed char (20) NOT NULL,
						QtyGiven int(4) NOT NULL,
						QtyWasted int(4) NOT NULL,
						DoseTimeStamp datetime NOT NULL,
						PRIMARY KEY (AnestheticMedNum),
						INDEX (AnestheticMedNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//a list of DEA scheduled anesthetic medications taken into inventory from a Supplier. Qtys are always in milliLiters so inventory count works properly.
					command="DROP TABLE IF EXISTS anesthmedsintake";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsintake(
						AnestheticMedNum int(3) NOT NULL auto_increment,
						IntakeDate datetime NOT NULL,
						AnestheticMed char (20) NOT NULL,
						DEASchedule char(2)NOT NULL,
						Qty int(6) NOT NULL, 
						SupplierIDNum char(11) NOT NULL,
						InvoiceNum char(20) NOT NULL,
						PRIMARY KEY (AnestheticMedNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//fields required to create inventory of anesthetic medications
					command="DROP TABLE IF EXISTS anesthmedsinventory";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsinventory (
						AnestheticMedNum int(3) NOT NULL auto_increment,
						AnestheticMed char(20) NOT NULL,
						AnesthHowSupplied char(20) NOT NULL,
						QtyOnHand int(5) NOT NULL,
						PRIMARY KEY (AnestheticMedNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//fields required to adjust inventory of anesthetic medications
					command="DROP TABLE IF EXISTS anesthmedsinventoryadj";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsinventoryadj (
						AnestheticMedNum int(3) NOT NULL auto_increment,
						AdjPos int(4) NOT NULL,
						AdjNeg int(4)NOT NULL,
						Provider char(4) NOT NULL,
						Notes text NOT NULL,
						TimeStamp datetime NOT NULL,
						PRIMARY KEY (AnestheticMedNum),
						INDEX (AnestheticMedNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//a list of suppliers of anesthetic medications
					command="DROP TABLE IF EXISTS anesthmedsuppliers";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsuppliers (
						SupplierIDNum int(3) NOT NULL auto_increment,
						SupplierName char(32) NOT NULL,
						Addr1 char(32)NOT NULL,
						Addr2 char(32) NOT NULL,
						City char(32) NOT NULL,
						State char(10) NOT NULL,
						Country char(32) NOT NULL,
						Phone char(12) NOT NULL,
						Fax char(12) NOT NULL,
						PhoneExt int(5) NOT NULL,
						Contact char(32) NOT NULL,
						Notes text NOT NULL,
						PRIMARY KEY (SupplierIDNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//keeps the post-anesthesia score and discharge data
					command="DROP TABLE IF EXISTS anesthscore";
					General.NonQ(command);
					command = @"CREATE TABLE anesthscore (
						AnestheticRecordNum int(7) NOT NULL auto_increment,
						QActivity smallint(1) NOT NULL,
						QResp smallint(1) NOT NULL,
						QCirc smallint(1) NOT NULL,
						QConc smallint(1) NOT NULL,
						QColor smallint(1) NOT NULL,
						AnesthScore smallint(2) NOT NULL,
						DischAmb tinyint(1) NOT NULL,
						DischWheelChr tinyint(1) NOT NULL,
						DischAmbulance tinyint(1) NOT NULL,
						DischCondStable tinyint(1) NOT NULL,
						PRIMARY KEY (AnestheticRecordNum),
						INDEX (AnestheticRecordNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//keeps data auto-imported from networkable vital sign monitors
					command="DROP TABLE IF EXISTS anesthvsdata";
					General.NonQ(command);
					command = @"CREATE TABLE anesthvsdata (
						AnestheticRecordNum int(7) NOT NULL auto_increment,
						VSMName char(20) NOT NULL,
						VSMSerNum char(20) NOT NULL,
						BPSys int(3) NOT NULL,
						BPDias int(3) NOT NULL,
						BPMAP int(3) NOT NULL,
						HR int(3) NOT NULL,
						SpO2 int(3) NOT NULL,
						EtCo2 int(3) NOT NULL,
						Temp int(3) NOT NULL,
						PRIMARY KEY (AnestheticRecordNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.1.1.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_1_8();
		}

		private void To6_1_8() {
			if(FromVersion<new Version("6.1.8.0")) {
				string command="UPDATE userod SET IsHidden=0 WHERE IsHidden=1 "
					+"AND EXISTS(SELECT * FROM grouppermission "
					+"WHERE PermType='"+POut.PInt((int)Permissions.SecurityAdmin)+"' "//24
					+"AND grouppermission.UserGroupNum=userod.UserGroupNum)";
				General.NonQ(command);
				command="UPDATE preference SET ValueString = '6.1.8.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_2_1();
		}

		private void To6_2_1() {
			if(FromVersion<new Version("6.2.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command = "DROP TABLE IF EXISTS anesthmedsintake";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsintake(
						AnestheticMedNum int(3) NOT NULL auto_increment,
						IntakeDate datetime NOT NULL,
						AnestheticMed char (20) NOT NULL,
						DEASchedule char(2),
						Qty int(6) NOT NULL, 
						SupplierIDNum char(11) NOT NULL,
						InvoiceNum char(20) NOT NULL,
						PRIMARY KEY (AnestheticMedNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command = "DROP TABLE IF EXISTS anesthmedsinventoryadj";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsinventoryadj (
						AdjustNum int(11) NOT NULL auto_increment,
						AnestheticMedNum int(11) NOT NULL,
						QtyAdj double,
						UserNum int(11) NOT NULL,
						Notes varchar(255),
						TimeStamp datetime NOT NULL,
						PRIMARY KEY (AdjustNum),
						INDEX (AnestheticMedNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS anestheticdata";
					General.NonQ(command);
					command = @"CREATE TABLE anestheticdata (
						AnestheticDataNum int(11) NOT NULL auto_increment,
						AnestheticRecordNum int(11) NOT NULL,
						AnesthOpen char(32),
						AnesthClose char(32),
						SurgOpen char(32),
						SurgClose char(32),
						Anesthetist char(32),
						Surgeon char(32),						
						Asst char(32),
						Circulator char(32),						
						VSMName char(20),
						VSMSerNum char(20),					
						ASA char(3),
						ASA_EModifier char(1),										
						O2LMin smallint(1),
						N2OLMin smallint(1),						
						RteNasCan tinyint(1),
						RteNasHood tinyint(1),
						RteETT tinyint(1),
						MedRouteIVCath tinyint(1),
						MedRouteIVButtFly tinyint(1),
						MedRouteIM tinyint(1),
						MedRoutePO tinyint(1),
						MedRouteNasal tinyint(1),
						MedRouteRectal tinyint(1),
						IVSite char(20),
						IVGauge smallint(2),
						IVSideR smallint(2),
						IVSideL smallint(2),
						IVAtt smallint(1),
						IVF char(20),
						IVFVol float(5),
						MonBP tinyint(1),
						MonSpO2 tinyint(1),
						MonEtCO2 tinyint(1),
						MonTemp tinyint(1),
						MonPrecordial tinyint(1),
						MonEKG tinyint(1),
						Notes text,
						PatWgt smallint(3),
						WgtUnitsLbs tinyint(1),
						WgtUnitsKgs tinyint(1),
						PatHgt smallint(3),
						EscortName char(32),
						EscortCellNum char(13),
						EscortRel char(16),
						NPOTime char(5),
						HgtUnitsIn tinyint (1),
						HgtUnitsCm tinyint (1),
						Signature text,
						SigIsTopaz tinyint unsigned default '0',
						PRIMARY KEY (AnestheticDataNum),
						INDEX (AnestheticRecordNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command = "DROP TABLE IF EXISTS anesthmedsuppliers";
					General.NonQ(command);
					command = @"CREATE TABLE anesthmedsuppliers (
						SupplierIDNum int(3) NOT NULL auto_increment,
						SupplierName varchar(255) NOT NULL,
						Phone char(13),
						PhoneExt char(6),
						Fax char(13),
						Addr1 varchar(48),
						Addr2 char(32),
						City varchar(48),
						State char(20),
						Zip char(10),
						Contact char(32),
						WebSite varchar(48),
						Notes text,
						PRIMARY KEY (SupplierIDNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					string[] commands = new string[]{
						"ALTER table userod ADD AnesthProvType int(2) default '3' NOT NULL"
						,"ALTER table anesthmedsinventory CHANGE AnestheticMedNum AnestheticMedNum int NOT NULL auto_increment"
						,"ALTER table anesthmedsinventory CHANGE AnestheticMed AnesthMedName char(30)"
						,"ALTER table anesthmedsinventory CHANGE QtyOnHand QtyOnHand double default '0'"
						,"ALTER table anesthmedsinventory ADD DEASchedule char(3)"
						,"ALTER table anesthmedsintake DROP DEASchedule"
						,"ALTER table anesthmedsintake CHANGE AnestheticMed AnesthMedName char(32)"
						,"ALTER table anesthmedsgiven CHANGE QtyGiven QtyGiven double"
						,"ALTER table anesthmedsgiven CHANGE QtyWasted QtyWasted double"
						,"ALTER table anesthmedsgiven CHANGE AnesthMed AnesthMedName char(32)"
						,"ALTER table anesthmedsgiven CHANGE DoseTimeStamp DoseTimeStamp char(32)"
						,"ALTER table anesthmedsgiven ADD QtyOnHandOld double"
					};
					General.NonQ(commands);
					command = "DROP TABLE IF EXISTS anesthscore";
					General.NonQ(command);
					command = @"CREATE TABLE anesthscore (
						AnesthScoreNum int(11) NOT NULL auto_increment,
						AnestheticRecordNum int(11),
						QActivity smallint(1),
						QResp smallint(1),
						QCirc smallint(1),
						QConc smallint(1),
						QColor smallint(1),
						AnesthesiaScore smallint(2),
						DischAmb tinyint(1),
						DischWheelChr tinyint(1),
						DischAmbulance tinyint(1),
						DischCondStable tinyint(1),
						DischCondUnStable tinyint(1),
						PRIMARY KEY (AnesthScoreNum),
						INDEX (AnestheticRecordNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//keeps data auto-imported from networkable vital sign monitors
					command = "DROP TABLE IF EXISTS anesthvsdata";
					General.NonQ(command);
					command = @"CREATE TABLE anesthvsdata (
						AnesthVSDataNum int(11) NOT NULL auto_increment,
						AnestheticRecordNum int(11) NOT NULL,
						PatNum int(11),
						VSMName char(20),
						VSMSerNum char(32),
						BPSys smallint(3),
						BPDias smallint(3),
						BPMAP smallint(3),
						HR smallint(3),
						SpO2 smallint(3),
						EtCo2 smallint(3),
						Temp smallint(3),
						VSTimeStamp char(32),
						PRIMARY KEY (AnesthVSDataNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="ALTER TABLE schedule ADD INDEX (EmployeeNum)";
					General.NonQ(command);
					command="ALTER TABLE schedule ADD INDEX (ProvNum)";
					General.NonQ(command);
					command="ALTER TABLE schedule ADD INDEX (SchedDate)";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('SecurityLockDate','0001-01-01','If present, global lock on procedures, payments, insurance payments, and adjustments.  Prevents editing old entries and backdating entries.')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('SecurityLockIncludesAdmin','0','0 or 1.  If 1, administrators are also locked out by date.')";
					General.NonQ(command);
					command="ALTER TABLE patient ADD ResponsParty int NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE patient ADD INDEX (ResponsParty)";
					General.NonQ(command);
					command="ALTER TABLE treatplan ADD ResponsParty int NOT NULL";
					General.NonQ(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.2.1.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_2_2();
		}

		private void To6_2_2() {
			if(FromVersion<new Version("6.2.2.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS phonenumber";
					General.NonQ(command);
					command=@"CREATE TABLE phonenumber (
						PhoneNumberNum int NOT NULL auto_increment,
						PatNum int NOT NULL,
						PhoneNumberVal varchar(255),
						PRIMARY KEY (PhoneNumberNum),
						INDEX (PatNum),
						INDEX (PhoneNumberVal)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
				}
				else{

				}
				command="UPDATE preference SET ValueString = '6.2.2.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_2_9();
		}

		private void To6_2_9() {
			if(FromVersion<new Version("6.2.9.0")) {
				string command="ALTER TABLE fee CHANGE FeeSched FeeSched int NOT NULL";
				General.NonQ(command);
				command="UPDATE preference SET ValueString = '6.2.9.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_3_1();
		}

		private void To6_3_1() {
			if(FromVersion<new Version("6.3.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncPath','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncLastFileNumber','0','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncDateTimeLastRun','0001-01-01','')";
					General.NonQ(command);
					//I had originally deleted these.  But decided instead to just comment them as obsolete because I think it caused a bug in our upgrade.
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName = 'LettersIncludeReturnAddress'";
					General.NonQ(command);
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName ='StationaryImage'";
					General.NonQ(command);
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName ='StationaryDocument'";
					General.NonQ(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.3.1.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_3_3();
		}

		private void To6_3_3() {
			if(FromVersion<new Version("6.3.3.0")) {
				string command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('CoPay_FeeSchedule_BlankLikeZero','1','1 to treat blank entries like zero copay.  0 to make patient responsible on blank entries.')";
				General.NonQ(command);
				command="UPDATE preference SET ValueString = '6.3.3.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_3_4();
		}

		private void To6_3_4() {
			if(FromVersion<new Version("6.3.4.0")) {
				string command="ALTER TABLE sheetfielddef CHANGE FieldValue FieldValue text NOT NULL";
				General.NonQ(command);
				command="UPDATE preference SET ValueString = '6.3.4.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_4_0();
		}

		private void To6_4_0() {
			if(FromVersion<new Version("6.4.0.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="UPDATE preference SET Comments = '-1 indicates min for all dates' WHERE PrefName = 'RecallDaysPast'";
					General.NonQ(command);
					command="UPDATE preference SET Comments = '-1 indicates max for all dates' WHERE PrefName = 'RecallDaysFuture'";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('RecallShowIfDaysFirstReminder','-1','-1 indicates do not show')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('RecallShowIfDaysSecondReminder','-1','-1 indicates do not show')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage','You are due for your regular dental check-up on ?DueDate  Please call our office today to schedule an appointment.','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg','You are due for your regular dental check-up.  [FamilyList]  Please call our office today to schedule an appointment.','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject2','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage2','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardMessage2','','')";
					General.NonQ(command);
					string prefVal;
					DataTable table;
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPostcardMessage'";
					table=General.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?DueDate","[DueDate]");
					command="UPDATE preference SET ValueString='"+POut.PString(prefVal)+"' WHERE PrefName='RecallPostcardMessage'";
					General.NonQ(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPostcardFamMsg'";
					table=General.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?FamilyList","[FamilyList]");
					command="UPDATE preference SET ValueString='"+POut.PString(prefVal)+"' WHERE PrefName='RecallPostcardFamMsg'";
					General.NonQ(command);
					command="SELECT ValueString FROM preference WHERE PrefName='ConfirmPostcardMessage'";
					table=General.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?date","[date]");
					prefVal=prefVal.Replace("?time","[time]");
					command="UPDATE preference SET ValueString='"+POut.PString(prefVal)+"' WHERE PrefName='ConfirmPostcardMessage'";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject3','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage3','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardMessage3','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg2','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg3','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardFamMsg2','','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardFamMsg3','','')";
					General.NonQ(command);
					command="ALTER TABLE autonote CHANGE ControlsToInc MainText text";
					General.NonQ(command);
					command="UPDATE autonote SET MainText = ''";
					General.NonQ(command);
					command="UPDATE autonotecontrol SET ControlType='Text' WHERE ControlType='MultiLineTextBox'";
					General.NonQ(command);
					command="UPDATE autonotecontrol SET ControlType='OneResponse' WHERE ControlType='ComboBox'";
					General.NonQ(command);
					command="UPDATE autonotecontrol SET ControlType='Text' WHERE ControlType='TextBox'";
					General.NonQ(command);
					command="UPDATE autonotecontrol SET ControlOptions=MultiLineText WHERE MultiLineText != ''";
					General.NonQ(command);
					command="ALTER TABLE autonotecontrol DROP PrefaceText";
					General.NonQ(command);
					command="ALTER TABLE autonotecontrol DROP MultiLineText";
					General.NonQ(command);



					
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.4.0.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			//To6_?_0();
		}





	}
}
