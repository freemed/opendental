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
		private System.Version LatestVersion=new Version("6.2.0.0");//This value must be changed when a new conversion is to be triggered.

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
			To6_2_0();
		}

		private void To6_2_0() {
			if(FromVersion<new Version("6.2.0.0")) {
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
						AnestheticMedNum int(3) NOT NULL auto_increment,
						AdjPos int(4),
						AdjNeg int(4),
						Provider char(4),
						Notes text NOT NULL,
						TimeStamp datetime,
						PRIMARY KEY (AnestheticMedNum),
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
						,"ALTER table anesthmedsinventory CHANGE AnestheticMed AnesthMedName char(30) NOT NULL"
						,"ALTER table anesthmedsinventory CHANGE QtyOnHand QtyOnHand int default '0' NOT NULL"
						,"ALTER table anesthmedsinventory ADD DEASchedule char(3)"
						,"ALTER table anesthmedsintake DROP DEASchedule"
						,"ALTER table anesthmedsintake CHANGE AnestheticMed AnesthMedName char(32) NOT NULL"
						,"ALTER table anesthmedsgiven CHANGE AnesthMed AnesthMedName char(32) NOT NULL"
						,"ALTER table anestheticdata CHANGE Anesthetist Anesthetist char(32)"
						,"ALTER table anestheticdata CHANGE Surgeon Surgeon char(32)"						
						,"ALTER TABLE anestheticdata CHANGE Asst Asst char(32)"
						,"ALTER TABLE anestheticdata CHANGE Circulator Circulator char(32)"
						,"ALTER table anestheticdata CHANGE IVFVol IVFVol float (5) NOT NULL"
						,"ALTER table anestheticdata CHANGE PatHgt PatHgt smallint (3) NOT NULL"
						,"ALTER table anestheticdata ADD PatHgtIn tinyint (1) NOT NULL"
						,"ALTER table anestheticdata ADD PatHgtCm tinyint (1) NOT NULL"
						
					};
					General.NonQ(commands);
					command="ALTER TABLE schedule ADD INDEX (EmployeeNum)";
					General.NonQ(command);
					command="ALTER TABLE schedule ADD INDEX (ProvNum)";
					General.NonQ(command);
					command="ALTER TABLE schedule ADD INDEX (SchedDate)";
					General.NonQ(command);






				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.2.0.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			//To6_2_?();
		}

		






	}
}
