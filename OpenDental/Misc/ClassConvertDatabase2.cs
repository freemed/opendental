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

namespace OpenDental{
	//The other file was simply getting too big.  It was bogging down VS speed.
	///<summary></summary>
	public partial class ClassConvertDatabase{
		private System.Version LatestVersion=new Version("6.1.0.0");//This value must be changed when a new conversion is to be triggered.
		
		private void To5_9_1() {
			if(FromVersion<new Version("5.9.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DELETE FROM preference WHERE PrefName='RxOrientVert'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxAdjustRight'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxAdjustDown'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxGeneric'";
					General.NonQ(command);
					command="ALTER TABLE rxdef ADD IsControlled tinyint NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE rxpat ADD IsControlled tinyint NOT NULL";
					General.NonQ(command);
					command="UPDATE rxdef SET IsControlled = 1";
					General.NonQ(command);
					//UAppoint Bridge---------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'UAppoint', "
						+"'UAppoint from www.uappoint.com', "
						+"'0', "
						+"'"+POut.PString(@"https://s0.uappoint.com/Sync")+"', "
						+"'', "
						+"'')";
					int programNum=General.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'Username', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'Password', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'WorkstationName', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'IntervalSeconds', "
						+"'15')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'DateTimeLastUploaded', "
						+"'')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'SynchStatus', "
						+"'')";
					General.NonQ(command);
					command="ALTER TABLE patient ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE patient SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE provider ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE provider SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE appointment SET DateTStamp=NOW()";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS deletedobject";
					General.NonQ(command);
					command=@"CREATE TABLE deletedobject (
						DeletedObjectNum int NOT NULL auto_increment,
						ObjectNum int NOT NULL,
						ObjectType int NOT NULL,
						DateTStamp TimeStamp,
						PRIMARY KEY (DeletedObjectNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="ALTER TABLE schedule ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE schedule SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE operatory ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE operatory SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE recall ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE recall SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE procedurecode ADD DateTStamp TimeStamp";
					General.NonQ(command);
					command="UPDATE procedurecode SET DateTStamp=NOW()";
					General.NonQ(command);
					command="ALTER TABLE insplan ADD IsHidden tinyint NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE carrier ADD IsHidden tinyint NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE insplan ADD INDEX (CarrierNum)";
					try {
						General.NonQ(command);
					}
					catch {
					}
					try {
						//this functionality is also duplicated in SheetUtil.GetImagePath, but we try very hard not to use external routines during conversions.
						if(!PrefC.UsingAtoZfolder) {
							throw new ApplicationException("Must be using AtoZ folders.");
						}
						string imagePath=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"SheetImages");
						if(!Directory.Exists(imagePath)) {
							Directory.CreateDirectory(imagePath);
						}
						Properties.Resources.Med_History.Save(ODFileUtils.CombinePaths(imagePath,"Med History.gif"));
						Properties.Resources.Patient_Info.Save(ODFileUtils.CombinePaths(imagePath,"Patient Info.gif"));
					}
					catch{
					}
					command="DELETE FROM preference WHERE PrefName='ShowNotesInAccount'";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AllowSettingProcsComplete','0','')";
					General.NonQ(command);
				} 
				else {//oracle
					
				}
				command="UPDATE preference SET ValueString = '5.9.1.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_0_1();
		}

		private void To6_0_1() {
			if(FromVersion<new Version("6.0.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject','Continuing Care Reminder','')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallStatusMailed','0','FK to definition.DefNum')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallStatusEmailed','0','FK to definition.DefNum')";
					General.NonQ(command);
					command="ALTER TABLE toothinitial ADD DrawingSegment text";
					General.NonQ(command);
					command="ALTER TABLE toothinitial ADD ColorDraw int NOT NULL";
					General.NonQ(command);
					//Dolphin bridge------------------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'Dolphin', "
						+"'Dolphin from dolphinimaging.com', "
						+"'0', "
						+"'"+POut.PString(@"C:\Dolphin\")+"', "
						+"'', "
						+"'The path is to a folder rather than to a specific file.  Filename property refers to the input filename used to transer data.')";
					int programNum=General.NonQ(command,true);//we now have a ProgramNum to work with
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
						+"'0')";
					General.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Filename', "
						+"'"+POut.PString(@"C:\Dolphin\Import\Import.txt")+"')";
					General.NonQ(command);
					command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
						+"VALUES ("
						+"'"+POut.PInt(programNum)+"', "
						+"'"+POut.PInt((int)ToolBarsAvail.ChartModule)+"', "
						+"'Dolphin')";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTimeArrived DateTime NOT NULL";
					General.NonQ(command);
					command="UPDATE appointment SET DateTimeArrived = '0001-01-01'";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTimeSeated DateTime NOT NULL";
					General.NonQ(command);
					command="UPDATE appointment SET DateTimeSeated = '0001-01-01'";
					General.NonQ(command);
					command="ALTER TABLE appointment ADD DateTimeDismissed DateTime NOT NULL";
					General.NonQ(command);
					command="UPDATE appointment SET DateTimeDismissed = '0001-01-01'";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AppointmentTimeArrivedTrigger','0','FK to definition.DefNum, Category ApptConfirmed.  0 indicates no trigger.')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AppointmentTimeSeatedTrigger','0','FK to definition.DefNum, Category ApptConfirmed.  0 indicates no trigger.')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('AppointmentTimeDismissedTrigger','0','FK to definition.DefNum, Category ApptConfirmed.  0 indicates no trigger.')";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('ApptModuleRefreshesEveryMinute','1','Keeps the waiting room indicator times current.')";
					General.NonQ(command);
					//RECALL---------------------------------------------------------------------------------------
					command="ALTER TABLE recall ADD RecallTypeNum int NOT NULL";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS recalltype";
					General.NonQ(command);
					command=@"CREATE TABLE recalltype (
						RecallTypeNum int NOT NULL auto_increment,
						Description varchar(255),
						DefaultInterval int NOT NULL,
						TimePattern varchar(255),
						Procedures varchar(255),
						PRIMARY KEY (RecallTypeNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					command="DROP TABLE IF EXISTS recalltrigger";
					General.NonQ(command);
					command=@"CREATE TABLE recalltrigger (
						RecallTriggerNum int NOT NULL auto_increment,
						RecallTypeNum int NOT NULL,
						CodeNum int NOT NULL,
						PRIMARY KEY (RecallTriggerNum),
						INDEX (CodeNum),
						INDEX (RecallTypeNum)
						) DEFAULT CHARSET=utf8";
					General.NonQ(command);
					//Basic recall-----------------------------------------------------------------------------
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPattern'";
					string timepattern=General.GetCount(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallProcedures'";
					string procs=General.GetCount(command);
					command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
						+"VALUES(1,'Prophy',"
						+"393216,"//six months
						+"'"+timepattern+"',"//always / and X, so no need to parameterize
						+"'"+POut.PString(procs)+"')";
					General.NonQ(command);
					command="SELECT CodeNum FROM procedurecode WHERE SetRecall=1";
					DataTable table=General.GetTable(command);
					for(int i=0;i<table.Rows.Count;i++){
						command="INSERT INTO recalltrigger(RecallTypeNum,CodeNum) VALUES(1,"+table.Rows[i][0].ToString()+")";
						General.NonQ(command);
					}
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypeSpecialProphy','1','FK to recalltype.RecallTypeNum.')";
					General.NonQ(command);
					//Child recall-----------------------------------------------------------------------------
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPatternChild'";
					timepattern=General.GetCount(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallProceduresChild'";
					procs=General.GetCount(command);
					command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
						+"VALUES(2,'Child Prophy',"
						+"0,"
						+"'"+timepattern+"',"//always / and X, so no need to parameterize
						+"'"+POut.PString(procs)+"')";
					General.NonQ(command);
					//no triggers
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypeSpecialChildProphy','2','FK to recalltype.RecallTypeNum.')";
					General.NonQ(command);
					//Perio recall-----------------------------------------------------------------------------
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPatternPerio'";
					timepattern=General.GetCount(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallProceduresPerio'";
					procs=General.GetCount(command);
					command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
						+"VALUES(3,'Perio',"
						+"262144,"//4 months.
						+"'"+timepattern+"',"//always / and X, so no need to parameterize
						+"'"+POut.PString(procs)+"')";
					General.NonQ(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPerioTriggerProcs'";
					string triggerStr=General.GetCount(command);
					List<string> perioCodeNums=new List<string>();
					string codeNum;
					if(triggerStr!=""){
						string[] triggerArray=triggerStr.Split(',');
						for(int i=0;i<triggerArray.Length;i++){
							command="SELECT CodeNum FROM procedurecode WHERE ProcCode='"+POut.PString(triggerArray[i])+"'";
							table=General.GetTable(command);
							if(table.Rows.Count==0){
								continue;
							}
							codeNum=table.Rows[0][0].ToString();
							perioCodeNums.Add(codeNum);
							command="INSERT INTO recalltrigger(RecallTypeNum,CodeNum) VALUES(3,"+codeNum+")";
							General.NonQ(command);
						}
					}
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypeSpecialPerio','3','FK to recalltype.RecallTypeNum.')";
					General.NonQ(command);
					if(CultureInfo.CurrentCulture.Name=="en-US"){
						//4BWX-----------------------------------------------------------------------------
						timepattern="";
						procs="D0274";
						command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
							+"VALUES(4,'4BW',"
							+"16777216,"//one year.
							+"'"+timepattern+"',"
							+"'"+POut.PString(procs)+"')";
						General.NonQ(command);
						command="SELECT CodeNum FROM procedurecode WHERE ProcCode='D0274'";
						table=General.GetTable(command);
						if(table.Rows.Count>0){
							codeNum=table.Rows[0][0].ToString();
							command="INSERT INTO recalltrigger(RecallTypeNum,CodeNum) VALUES(4,"+codeNum+")";
							General.NonQ(command);
						}
						//Pano-----------------------------------------------------------------------------
						timepattern="";
						procs="D0330";
						command="INSERT INTO recalltype(RecallTypeNum,Description,DefaultInterval,TimePattern,Procedures) "
							+"VALUES(5,'Pano',"
							+"83886080,"//5 years.
							+"'"+timepattern+"',"
							+"'"+POut.PString(procs)+"')";
						General.NonQ(command);
						command="SELECT CodeNum FROM procedurecode WHERE ProcCode='D0330'";
						table=General.GetTable(command);
						if(table.Rows.Count>0){
							codeNum=table.Rows[0][0].ToString();
							command="INSERT INTO recalltrigger(RecallTypeNum,CodeNum) VALUES(5,"+codeNum+")";
							General.NonQ(command);
						}
					}
					//Set existing recall objects to new types--------------------------------------------------
					command="UPDATE recall SET RecallTypeNum=1 WHERE RecallTypeNum=0";
					General.NonQ(command);
					for(int i=0;i<perioCodeNums.Count;i++){
						command="UPDATE recall SET RecallTypeNum=3 WHERE EXISTS("
							+"SELECT * FROM procedurelog WHERE procedurelog.PatNum=recall.PatNum "
							+"AND procedurelog.CodeNum="+perioCodeNums[i]+" "
							+"AND procedurelog.ProcStatus=2)";//complete
						General.NonQ(command);
					}
					//an automatic synch would violate the rule of not calling external methods.
					//Recalls.SynchAllPatients();
					MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste("When this conversion is done, you will also need to resynchronize all patient recalls from inside the Setup | Recall Types window.");
					msgbox.TopMost=true;
					msgbox.ShowDialog();
					//Get rid of unused prefs-----------------------------------------------------------------
					command="DELETE FROM preference WHERE PrefName='RecallPattern'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallProcedures'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallPatternChild'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallProceduresChild'";
					General.NonQ(command);
					command="ALTER TABLE procedurecode DROP SetRecall";
					General.NonQ(command);
					command="ALTER TABLE procedurecode DROP RemoveTooth";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallBW'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallFMXPanoProc'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallDisableAutoFilms'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallFMXPanoYrInterval'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallDisablePerioAlt'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallPatternPerio'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallProceduresPerio'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RecallPerioTriggerProcs'";
					General.NonQ(command);
					command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES (21,8,'Family Module Referral','',-2823993,0)";
					General.NonQ(command);
					command="ALTER TABLE payplan ADD CompletedAmt double NOT NULL";
					General.NonQ(command);
					command="UPDATE payplan SET CompletedAmt=(SELECT SUM(Principal) FROM payplancharge WHERE payplan.PayPlanNum=payplancharge.PayPlanNum)";
					General.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('ChartQuickAddHideAmalgam','0','')";
					General.NonQ(command);
				} 
				else {//oracle
					
				}
				command="UPDATE preference SET ValueString = '6.0.1.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_0_2();
		}

		private void To6_0_2() {
			if(FromVersion<new Version("6.0.2.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallTypesShowingInList','1,3','Comma-delimited list. FK to recalltype.RecallTypeNum.')";//1=prophy,3=perio
				General.NonQ(command);
				command="UPDATE preference SET ValueString = '6.0.2.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			To6_1_0();
		}

		private void To6_1_0() {
			if(FromVersion<new Version("6.1.0.0")) {
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
					for(int i=0;i<table.Rows.Count;i++){
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
					for(int i=0;i<table.Rows.Count;i++){
						visibleOps.Add(PIn.PInt(table.Rows[i]["OperatoryNum"].ToString()));
					}
					//convert blockouts with op=0
					command="SELECT ScheduleNum FROM schedule WHERE SchedType=2 "//blockout
						+"AND Op=0";//indicates all ops
					table=General.GetTable(command);
					for(int i=0;i<table.Rows.Count;i++){
						//for each schedule, we need to insert a separate scheduleop for each visible op
						for(int o=0;o<visibleOps.Count;o++){
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
					for(int i=0;i<table.Rows.Count;i++){
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
					for(int i=0;i<table.Rows.Count;i++){
						command="INSERT INTO feesched(FeeSchedNum,Description,FeeSchedType,ItemOrder,IsHidden) VALUES("
							+table.Rows[i]["DefNum"].ToString()+","
							+"'"+POut.PString(table.Rows[i]["ItemName"].ToString())+"',";
						if(table.Rows[i]["ItemValue"].ToString()=="A"){
							command+=POut.PInt((int)FeeScheduleType.Allowed)+",";
						}
						else if(table.Rows[i]["ItemValue"].ToString()=="C"){
							command+=POut.PInt((int)FeeScheduleType.CoPay)+",";
						}
						else{
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
					//this field is used to temporarily elevate security privileges to adminstrator. This will probably never be used.                 
					// command = "ALTER TABLE userod ADD TempAdminPriv tinyint NOT NULL";
					// General.NonQ(command);
                    

				} 
				else {//oracle
					
				}
				command="UPDATE preference SET ValueString = '6.1.0.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			//To6_2_?();
		}

		/*For 6.1:
		 * ALTER TABLE schedule ADD INDEX (EmployeeNum)
ALTER TABLE schedule ADD INDEX (ProvNum)
ALTER TABLE schedule ADD INDEX (SchedDate)*/

	}
}
