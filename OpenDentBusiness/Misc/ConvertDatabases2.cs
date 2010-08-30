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
//using System.Windows.Forms;
//using OpenDentBusiness;
using CodeBase;

namespace OpenDentBusiness {
	//The other file was simply getting too big.  It was bogging down VS speed.
	///<summary></summary>
	public partial class ConvertDatabases {
		public static System.Version LatestVersion=new Version("7.3.0.0");//This value must be changed when a new conversion is to be triggered.

		private static void To6_2_9() {
			if(FromVersion<new Version("6.2.9.0")) {
				string command="ALTER TABLE fee CHANGE FeeSched FeeSched int NOT NULL";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.2.9.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_3_1();
		}

		private static void To6_3_1() {
			if(FromVersion<new Version("6.3.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncPath','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncLastFileNumber','0','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncDateTimeLastRun','0001-01-01','')";
					Db.NonQ32(command);
					//I had originally deleted these.  But decided instead to just comment them as obsolete because I think it caused a bug in our upgrade.
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName = 'LettersIncludeReturnAddress'";
					Db.NonQ32(command);
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName ='StationaryImage'";
					Db.NonQ32(command);
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName ='StationaryDocument'";
					Db.NonQ32(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.3.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_3_3();
		}

		private static void To6_3_3() {
			if(FromVersion<new Version("6.3.3.0")) {
				string command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('CoPay_FeeSchedule_BlankLikeZero','1','1 to treat blank entries like zero copay.  0 to make patient responsible on blank entries.')";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.3.3.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_3_4();
		}

		private static void To6_3_4() {
			if(FromVersion<new Version("6.3.4.0")) {
				string command="ALTER TABLE sheetfielddef CHANGE FieldValue FieldValue text NOT NULL";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.3.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_4_1();
		}

		private static void To6_4_1() {
			if(FromVersion<new Version("6.4.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="UPDATE preference SET Comments = '-1 indicates min for all dates' WHERE PrefName = 'RecallDaysPast'";
					Db.NonQ32(command);
					command="UPDATE preference SET Comments = '-1 indicates max for all dates' WHERE PrefName = 'RecallDaysFuture'";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('RecallShowIfDaysFirstReminder','-1','-1 indicates do not show')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('RecallShowIfDaysSecondReminder','-1','-1 indicates do not show')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage','You are due for your regular dental check-up on ?DueDate  Please call our office today to schedule an appointment.','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg','You are due for your regular dental check-up.  [FamilyList]  Please call our office today to schedule an appointment.','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject2','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage2','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardMessage2','','')";
					Db.NonQ32(command);
					string prefVal;
					DataTable table;
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPostcardMessage'";
					table=Db.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?DueDate","[DueDate]");
					command="UPDATE preference SET ValueString='"+POut.String(prefVal)+"' WHERE PrefName='RecallPostcardMessage'";
					Db.NonQ32(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPostcardFamMsg'";
					table=Db.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?FamilyList","[FamilyList]");
					command="UPDATE preference SET ValueString='"+POut.String(prefVal)+"' WHERE PrefName='RecallPostcardFamMsg'";
					Db.NonQ32(command);
					command="SELECT ValueString FROM preference WHERE PrefName='ConfirmPostcardMessage'";
					table=Db.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?date","[date]");
					prefVal=prefVal.Replace("?time","[time]");
					command="UPDATE preference SET ValueString='"+POut.String(prefVal)+"' WHERE PrefName='ConfirmPostcardMessage'";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject3','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage3','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardMessage3','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg2','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg3','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardFamMsg2','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardFamMsg3','','')";
					Db.NonQ32(command);
					command="ALTER TABLE autonote CHANGE ControlsToInc MainText text";
					Db.NonQ32(command);
					command="UPDATE autonote SET MainText = ''";
					Db.NonQ32(command);
					command="UPDATE autonotecontrol SET ControlType='Text' WHERE ControlType='MultiLineTextBox'";
					Db.NonQ32(command);
					command="UPDATE autonotecontrol SET ControlType='OneResponse' WHERE ControlType='ComboBox'";
					Db.NonQ32(command);
					command="UPDATE autonotecontrol SET ControlType='Text' WHERE ControlType='TextBox'";
					Db.NonQ32(command);
					command="UPDATE autonotecontrol SET ControlOptions=MultiLineText WHERE MultiLineText != ''";
					Db.NonQ32(command);
					command="ALTER TABLE autonotecontrol DROP PrefaceText";
					Db.NonQ32(command);
					command="ALTER TABLE autonotecontrol DROP MultiLineText";
					Db.NonQ32(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.4.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_4_4();
		}

		private static void To6_4_4() {
			if(FromVersion<new Version("6.4.4.0")) {
				string command;
				//Convert comma-delimited autonote controls to carriage-return delimited.
				command="SELECT AutoNoteControlNum,ControlOptions FROM autonotecontrol";
				DataTable table=Db.GetTable(command);
				string newVal;
				for(int i=0;i<table.Rows.Count;i++) {
					newVal=table.Rows[i]["ControlOptions"].ToString();
					newVal=newVal.TrimEnd(',');
					newVal=newVal.Replace(",","\r\n");
					command="UPDATE autonotecontrol SET ControlOptions='"+POut.String(newVal)
						+"' WHERE AutoNoteControlNum="+table.Rows[i]["AutoNoteControlNum"].ToString();
					Db.NonQ32(command);
				}
				command="UPDATE preference SET ValueString = '6.4.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_5_1();
		}

		private static void To6_5_1() {
			if(FromVersion<new Version("6.5.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('ShowFeatureMedicalInsurance','0','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingUseElectronic','0','Set to 1 to used e-billing.')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectVendorId','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectVendorPMSCode','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectCreditCardChoices','V,MC','Choices of V,MC,D,A comma delimited.')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectClientAcctNumber','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectUserName','','')";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectPassword','','')";
					Db.NonQ32(command);
					command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES(12,22,'Status Condition','',-8978432,0)";
					Db.NonQ32(command);
					command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES(22,16,'Condition','',-5169880,0)";
					Db.NonQ32(command);
					command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES(22,17,'Condition (light)','',-1678747,0)";
					Db.NonQ32(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingIgnoreInPerson','0','Set to 1 to ignore walkout statements.')";
					Db.NonQ32(command);
					//eClinicalWorks Bridge---------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'eClinicalWorks', "
						+"'eClinicalWorks from www.eclinicalworks.com', "
						+"'0', "
						+"'', "
						+"'', "
						+"'')";
					int programNum=Db.NonQ32(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.Long(programNum)+"', "
						+"'HL7FolderIn', "
						+"'')";
					Db.NonQ32(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.Long(programNum)+"', "
						+"'HL7FolderOut', "
						+"'')";
					Db.NonQ32(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.Long(programNum)+"', "
						+"'DefaultUserGroup', "
						+"'')";
					Db.NonQ32(command);
					command = "ALTER TABLE anesthmedsgiven ADD AnesthMedNum int NOT NULL";
					Db.NonQ32(command);
					command = "ALTER TABLE provider ADD AnesthProvType int NOT NULL";
					Db.NonQ32(command);
					command="DROP TABLE IF EXISTS hl7msg";
					Db.NonQ32(command);
					command=@"CREATE TABLE hl7msg (
						HL7MsgNum int NOT NULL auto_increment,
						HL7Status int NOT NULL,
						MsgText text,
						AptNum int NOT NULL,
						PRIMARY KEY (HL7MsgNum),
						INDEX (AptNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ32(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.5.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_6_1();
		}

		private static void To6_6_1() {
			if(FromVersion<new Version("6.6.1.0")) {
				string command;
				DataTable table;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					//Change defaults for XDR bridge-------------------------------------------------------------------
					command="SELECT Enabled,ProgramNum FROM program WHERE ProgName='XDR'";
					table=Db.GetTable(command);
					int programNum;
					if(table.Rows.Count>0 && table.Rows[0]["Enabled"].ToString()=="0") {//if XDR not enabled
						//change the defaults
						programNum=PIn.Int(table.Rows[0]["ProgramNum"].ToString());
						command="UPDATE program SET Path='"+POut.String(@"C:\XDRClient\Bin\XDR.exe")+"' WHERE ProgramNum="+POut.Long(programNum);
						Db.NonQ32(command);
						command="UPDATE programproperty SET PropertyValue='"+POut.String(@"C:\XDRClient\Bin\infofile.txt")+"' "
							+"WHERE ProgramNum="+POut.Long(programNum)+" "
							+"AND PropertyDesc='InfoFile path'";
						Db.NonQ32(command);
						command="UPDATE toolbutitem SET ToolBar=7 "//The toolbar at the top that is common to all modules.
							+"WHERE ProgramNum="+POut.Long(programNum);
						Db.NonQ32(command);
					}
					//iCat Bridge---------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'iCat', "
						+"'iCat from www.imagingsciences.com', "
						+"'0', "
						+"'"+POut.String(@"C:\Program Files\ISIP\iCATVision\Vision.exe")+"', "
						+"'', "
						+"'')";
					programNum=Db.NonQ32(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
						+"'0')";
					Db.NonQ32(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Acquisition computer name', "
						+"'')";
					Db.NonQ32(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'XML output file path', "
						+"'"+POut.String(@"C:\iCat\Out\pm.xml")+"')";
					Db.NonQ32(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Return folder path', "
						+"'"+POut.String(@"C:\iCat\Return")+"')";
					Db.NonQ32(command);
					command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
						+"VALUES ("
						+"'"+POut.Long(programNum)+"', "
						+"'"+POut.Long((int)ToolBarsAvail.ChartModule)+"', "
						+"'iCat')";
					Db.NonQ32(command);
					//end of iCat Bridge
					string[] commands = new string[]{
						"ALTER TABLE anesthvsdata ADD MessageID varchar(50)",
						"ALTER TABLE anesthvsdata ADD HL7Message longtext"
					};
					Db.NonQ32(commands);
					command="ALTER TABLE computer DROP PrinterName";
					Db.NonQ32(command);
					command="ALTER TABLE computer ADD LastHeartBeat datetime NOT NULL default '0001-01-01'";
					Db.NonQ32(command);
					command="ALTER TABLE registrationkey ADD UsesServerVersion tinyint NOT NULL";
					Db.NonQ32(command);
					command="ALTER TABLE registrationkey ADD IsFreeVersion tinyint NOT NULL";
					Db.NonQ32(command);
					command="ALTER TABLE registrationkey ADD IsOnlyForTesting tinyint NOT NULL";
					Db.NonQ32(command);
				}
				else {//oracle

				}				
				command="UPDATE preference SET ValueString = '6.6.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_6_2();
		}

		private static void To6_6_2() {
			if(FromVersion<new Version("6.6.2.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('WebServiceServerName','','Blank if not using web service.')";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.6.2.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_6_3();
		}

		private static void To6_6_3() {
			if(FromVersion<new Version("6.6.3.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateInProgressOnComputerName','','Will be blank if update is complete.  If in the middle of an update, the named workstation is the only one allowed to startup OD.')";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.6.3.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_6_8();
		}

		private static void To6_6_8() {
			if(FromVersion<new Version("6.6.8.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateMultipleDatabases','','Comma delimited')";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.6.8.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_6_16();
		}

		private static void To6_6_16() {
			if(FromVersion<new Version("6.6.16.0")) {
				string command;
				command="SELECT ProgramNum FROM program WHERE ProgName='MediaDent'";
				int programNum=PIn.Int(Db.GetScalar(command));
				command="DELETE FROM programproperty WHERE ProgramNum="+POut.Long(programNum)
					+" AND PropertyDesc='Image Folder'";
				Db.NonQ32(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'"+POut.String("Text file path")+"', "
					+"'"+POut.String(@"C:\MediadentInfo.txt")+"')";
				Db.NonQ32(command);
				command="UPDATE program SET Note='Text file path needs to be the same on all computers.' WHERE ProgramNum="+POut.Long(programNum);
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.6.16.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_6_19();
		}

		private static void To6_6_19() {
			if(FromVersion<new Version("6.6.19.0")) {
				string command;
				command="UPDATE employee SET LName='O' WHERE LName='' AND FName=''";
				Db.NonQ32(command);
				command="UPDATE schedule SET SchedType=1 WHERE ProvNum != 0 AND SchedType != 1";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.6.19.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_7_1();
		}

		private static void To6_7_1() {
			if(FromVersion<new Version("6.7.1.0")) {
				string command;
				command="ALTER TABLE document ADD DateTStamp TimeStamp";
				Db.NonQ32(command);
				command="UPDATE document SET DateTStamp=NOW()";
				Db.NonQ32(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('StatementShowNotes','0','Payments and adjustments.')";
				Db.NonQ32(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('StatementShowProcBreakdown','0','')";
				Db.NonQ32(command);
				command="ALTER TABLE etrans ADD EtransMessageTextNum INT NOT NULL";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS etransmessagetext";
				Db.NonQ32(command);
				command=@"CREATE TABLE etransmessagetext (
					EtransMessageTextNum int NOT NULL auto_increment,
					MessageText text NOT NULL,
					PRIMARY KEY (EtransMessageTextNum),
					INDEX(MessageText(255))
					) DEFAULT CHARSET=utf8";
				Db.NonQ32(command);
				command="ALTER TABLE etrans ADD INDEX(MessageText(255))";
				Db.NonQ32(command);
				command="INSERT INTO etransmessagetext (MessageText) "
					+"SELECT DISTINCT MessageText FROM etrans "
					+"WHERE etrans.MessageText != ''";
				Db.NonQ32(command);
				command="UPDATE etrans,etransmessagetext "
					+"SET etrans.EtransMessageTextNum=etransmessagetext.EtransMessageTextNum "
					+"WHERE etrans.MessageText=etransmessagetext.MessageText";
				Db.NonQ32(command);
				command="ALTER TABLE etrans DROP MessageText";
				Db.NonQ32(command);
				command="ALTER TABLE etransmessagetext DROP INDEX MessageText";
				Db.NonQ32(command);
				command="ALTER TABLE etrans ADD AckEtransNum INT NOT NULL";
				Db.NonQ32(command);
				//Fill the AckEtransNum values for existing claims.
				command=@"DROP TABLE IF EXISTS etack;
CREATE TABLE etack (                                             
EtransNum int(11) NOT NULL auto_increment,                      
DateTimeTrans datetime NOT NULL,  
ClearinghouseNum int(11) NOT NULL,                                                               
BatchNumber int(11) NOT NULL,                                                                  
PRIMARY KEY  (`EtransNum`)                               
)  
SELECT * FROM etrans
WHERE Etype=21;
UPDATE etrans etorig, etack
SET etorig.AckEtransNum=etack.EtransNum 
WHERE etorig.EtransNum != etack.EtransNum
AND etorig.BatchNumber=etack.BatchNumber
AND etorig.ClearinghouseNum=etack.ClearinghouseNum
AND etorig.DateTimeTrans > DATE_SUB(etack.DateTimeTrans,INTERVAL 14 DAY)
AND etorig.DateTimeTrans < DATE_ADD(etack.DateTimeTrans,INTERVAL 1 DAY);
DROP TABLE IF EXISTS etAck";
				Db.NonQ32(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateWebProxyAddress','','')";
				Db.NonQ32(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateWebProxyUserName','','')";
				Db.NonQ32(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateWebProxyPassword','','')";
				Db.NonQ32(command);
				command="ALTER TABLE etrans ADD PlanNum INT NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans ADD INDEX (PlanNum)";
				Db.NonQ32(command);
				//Added new enum value of 0=None to CoverageLevel.
				command="UPDATE benefit SET CoverageLevel=CoverageLevel+1 WHERE BenefitType=2 OR BenefitType=5";//Deductible, Limitations
				Db.NonQ32(command);
				command="ALTER TABLE benefit CHANGE Percent Percent tinyint NOT NULL";//So that we can store -1.
				Db.NonQ32(command);
				command="UPDATE benefit SET Percent=-1 WHERE BenefitType != 1";//set Percent empty where not CoInsurance
				Db.NonQ32(command);
				command="ALTER TABLE benefit DROP OldCode";
				Db.NonQ32(command);
				//set MonetaryAmt empty when ActiveCoverage,CoInsurance,Exclusion
				command="UPDATE benefit SET MonetaryAmt=-1 WHERE BenefitType=0 OR BenefitType=1 OR BenefitType=4";
				Db.NonQ32(command);
				//set MonetaryAmt empty when Limitation and a quantity is entered
				command="UPDATE benefit SET MonetaryAmt=-1 WHERE BenefitType=5 AND Quantity != 0";
				Db.NonQ32(command);
				if(CultureInfo.CurrentCulture.Name=="en-US") {
					command="UPDATE covcat SET CovOrder=CovOrder+1 WHERE CovOrder > 1";
					Db.NonQ32(command);
					command="INSERT INTO covcat (Description,DefaultPercent,CovOrder,IsHidden,EbenefitCat) VALUES('X-Ray',100,2,0,13)";
					int covCatNum=Db.NonQ32(command,true);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.Long(covCatNum)+",'D0200','D0399')";
					Db.NonQ32(command);
					command="SELECT MAX(CovOrder) FROM covcat";
					int covOrder=PIn.Int(Db.GetScalar(command));
					command="INSERT INTO covcat (Description,DefaultPercent,CovOrder,IsHidden,EbenefitCat) VALUES('Adjunctive',-1,"
						+POut.Long(covOrder+1)+",0,14)";//adjunctive
					covCatNum=Db.NonQ32(command,true);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.Long(covCatNum)+",'D9000','D9999')";
					Db.NonQ32(command);
					command="SELECT CovCatNum FROM covcat WHERE EbenefitCat=1";//general
					covCatNum=Db.NonQ32(command,true);
					command="DELETE FROM covspan WHERE CovCatNum="+POut.Long(covCatNum);
					Db.NonQ32(command);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.Long(covCatNum)+",'D0000','D7999')";
					Db.NonQ32(command);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.Long(covCatNum)+",'D9000','D9999')";
					Db.NonQ32(command);
				}
				command="ALTER TABLE claimproc ADD DedEst double NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc ADD DedEstOverride double NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc ADD InsEstTotal double NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc ADD InsEstTotalOverride double NOT NULL";
				Db.NonQ32(command);
				command="UPDATE claimproc SET DedApplied=-1 WHERE ClaimNum=0";//if not attached to a claim, clear this value
				Db.NonQ32(command);
				command="UPDATE claimproc SET DedEstOverride=-1";
				Db.NonQ32(command);
				command="UPDATE claimproc SET InsEstTotal=BaseEst";
				Db.NonQ32(command);
				command="UPDATE claimproc SET InsEstTotalOverride=OverrideInsEst";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc DROP OverrideInsEst";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc DROP DedBeforePerc";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc DROP OverAnnualMax";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc ADD PaidOtherInsOverride double NOT NULL";
				Db.NonQ32(command);
				command="UPDATE claimproc SET PaidOtherInsOverride=PaidOtherIns";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc ADD EstimateNote varchar(255) NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan ADD MonthRenew tinyint NOT NULL";
				Db.NonQ32(command);
				command="SELECT insplan.PlanNum,MONTH(DateEffective) "
					+"FROM insplan,benefit "
					+"WHERE insplan.PlanNum=benefit.PlanNum "
					+"AND benefit.TimePeriod=1 "
					+"GROUP BY insplan.PlanNum";//service year
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE insplan SET MonthRenew="+table.Rows[i][1].ToString()
						+" WHERE PlanNum="+table.Rows[i][0].ToString();
					Db.NonQ32(command);
				}
				command="ALTER TABLE appointment ADD InsPlan1 INT NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment ADD INDEX (InsPlan1)";
				Db.NonQ32(command);
				command="ALTER TABLE appointment ADD InsPlan2 INT NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment ADD INDEX (InsPlan2)";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS insfilingcode";
				Db.NonQ32(command);
				command=@"CREATE TABLE insfilingcode (
					InsFilingCodeNum INT AUTO_INCREMENT,
					Descript VARCHAR(255),
					EclaimCode VARCHAR(100),
					ItemOrder INT,
					PRIMARY KEY(InsFilingCodeNum),
					INDEX(ItemOrder)
					)";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS insfilingcodesubtype";
				Db.NonQ32(command);
				command=@"CREATE TABLE insfilingcodesubtype (
					InsFilingCodeSubtypeNum INT AUTO_INCREMENT,
					InsFilingCodeNum INT,
					Descript VARCHAR(255),
					INDEX(InsFilingCodeNum),
					PRIMARY KEY(InsFilingCodeSubtypeNum)
					)";
				Db.NonQ32(command);
				command="ALTER TABLE insplan ADD FilingCodeSubtype INT NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan ADD INDEX (FilingCodeSubtype)";
				Db.NonQ32(command);
				//eCW bridge enhancements
				command="SELECT ProgramNum FROM program WHERE ProgName='eClinicalWorks'";
				int programNum=PIn.Int(Db.GetScalar(command));
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'ShowImagesModule', "
					+"'0')";
				Db.NonQ32(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'IsStandalone', "
					+"'0')";//starts out as false
				Db.NonQ32(command);
				command="UPDATE insplan SET FilingCode = FilingCode+1";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(1,'Commercial_Insurance','CI',0)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(2,'SelfPay','09',1)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(3,'OtherNonFed','11',2)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(4,'PPO','12',3)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(5,'POS','13',4)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(6,'EPO','14',5)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(7,'Indemnity','15',6)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(8,'HMO_MedicareRisk','16',7)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(9,'DMO','17',8)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(10,'BCBS','BL',9)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(11,'Champus','CH',10)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(12,'Disability','DS',11)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(13,'FEP','FI',12)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(14,'HMO','HM',13)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(15,'LiabilityMedical','LM',14)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(16,'MedicarePartB','MB',15)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(17,'Medicaid','MC',16)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(18,'ManagedCare_NonHMO','MH',17)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(19,'OtherFederalProgram','OF',18)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(20,'SelfAdministered','SA',19)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(21,'Veterans','VA',20)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(22,'WorkersComp','WC',21)";
				Db.NonQ32(command);
				command="INSERT INTO insfilingcode VALUES(23,'MutuallyDefined','ZZ',22)";
				Db.NonQ32(command);
				//Fixes bug here instead of db maint
				//Duplicated in version 6.6
				command="UPDATE employee SET LName='O' WHERE LName='' AND FName=''";
				Db.NonQ32(command);
				command="UPDATE schedule SET SchedType=1 WHERE ProvNum != 0 AND SchedType != 1";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.7.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_7_3();
		}

		private static void To6_7_3() {
			if(FromVersion<new Version("6.7.3.0")) {
				string command=@"UPDATE claimform,claimformitem SET claimformitem.FieldName='IsGroupHealthPlan'
					WHERE claimformitem.FieldName='IsStandardClaim' AND claimform.ClaimFormNum=claimformitem.ClaimFormNum
					AND claimform.UniqueID='OD9'";//1500
				Db.NonQ32(command);
				command=@"UPDATE claimform,claimformitem SET claimformitem.XPos='97'
					WHERE claimformitem.XPos='30' AND claimform.ClaimFormNum=claimformitem.ClaimFormNum
					AND claimform.UniqueID='OD9'";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.7.3.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_7_4();
		}

		private static void To6_7_4() {
			if(FromVersion<new Version("6.7.4.0")) {
				string command="DELETE FROM medicationpat WHERE EXISTS(SELECT * FROM patient WHERE medicationpat.PatNum=patient.PatNum AND patient.PatStatus=4)";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.7.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_7_5();
		}

		private static void To6_7_5() {
			if(FromVersion<new Version("6.7.5.0")) {
				string command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('ClaimsValidateACN','0','If set to 1, then any claim with a groupName containing ADDP will require an ACN number in the claim remarks.')";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.7.5.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_7_12();
		}

		private static void To6_7_12() {
			if(FromVersion<new Version("6.7.12.0")) {
				string command;
				//Camsight Bridge---------------------------------------------------------------------------
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'Camsight', "
					+"'Camsight from www.camsight.com', "
					+"'0', "
					+"'"+POut.String(@"C:\cdm\cdm\cdmx\cdmx.exe")+"', "
					+"'', "
					+"'')";
				int programNum=Db.NonQ32(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ32(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+POut.Long(programNum)+"', "
					+"'"+POut.Long((int)ToolBarsAvail.ChartModule)+"', "
					+"'Camsight')";
				Db.NonQ32(command);
				//CliniView Bridge---------------------------------------------------------------------------
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'CliniView', "
					+"'CliniView', "
					+"'0', "
					+"'"+POut.String(@"C:\Program Files\CliniView\CliniView.exe")+"', "
					+"'', "
					+"'')";
				programNum=Db.NonQ32(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ32(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+POut.Long(programNum)+"', "
					+"'"+POut.Long((int)ToolBarsAvail.ChartModule)+"', "
					+"'CliniView')";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.7.12.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_7_15();
		}

		private static void To6_7_15() {
			//duplicated in 6.6.26
			if(FromVersion<new Version("6.7.15.0")) {
				string command;
				command="ALTER TABLE insplan CHANGE FeeSched FeeSched int NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE CopayFeeSched CopayFeeSched int NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE AllowedFeeSched AllowedFeeSched int NOT NULL";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '6.7.15.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_7_22();
		}

		private static void To6_7_22() {
			if(FromVersion<new Version("6.7.22.0")) {
				string command;
				command="UPDATE preference SET ValueString ='http://opendentalsoft.com:1942/WebServiceCustomerUpdates/Service1.asmx' WHERE PrefName='UpdateServerAddress' AND ValueString LIKE '%70.90.133.65%'";
				Db.NonQ(command);
				try {
					command="ALTER TABLE document ADD INDEX (PatNum)";
					Db.NonQ(command);
					command="ALTER TABLE procedurelog ADD INDEX (BillingTypeOne)";
					Db.NonQ(command);
					command="ALTER TABLE procedurelog ADD INDEX (BillingTypeTwo)";
					Db.NonQ(command);
					command="ALTER TABLE securitylog ADD INDEX (PatNum)";
					Db.NonQ(command);
					command="ALTER TABLE toothinitial ADD INDEX (PatNum)";
					Db.NonQ(command);
					command="ALTER TABLE patplan ADD INDEX (PlanNum)";
					Db.NonQ(command);
				}
				catch {
					//in case any of the indices arlready exists.
				}
				command="UPDATE preference SET ValueString = '6.7.22.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_8_1();
		}

		//To6_7_25()//duplicated further down

		private static void To6_8_1() {
			if(FromVersion<new Version("6.8.1.0")) {
				string command;
				//add TreatPlanEdit,ReportProdInc,TimecardDeleteEntry permissions to all groups------------------------------------------------------
				command="SELECT UserGroupNum FROM usergroup";
				DataTable table=Db.GetTable(command);
				int groupNum;
				for(int i=0;i<table.Rows.Count;i++) {
					groupNum=PIn.Int(table.Rows[i][0].ToString());
					command="INSERT INTO grouppermission (NewerDate,UserGroupNum,PermType) "
						+"VALUES('0001-01-01',"+POut.Long(groupNum)+","+POut.Long((int)Permissions.TreatPlanEdit)+")";
					Db.NonQ32(command);
					command="INSERT INTO grouppermission (NewerDate,UserGroupNum,PermType) "
						+"VALUES('0001-01-01',"+POut.Long(groupNum)+","+POut.Long((int)Permissions.ReportProdInc)+")";
					Db.NonQ32(command);
					command="INSERT INTO grouppermission (NewerDate,UserGroupNum,PermType) "
						+"VALUES('0001-01-01',"+POut.Long(groupNum)+","+POut.Long((int)Permissions.TimecardDeleteEntry)+")";
					Db.NonQ32(command);
				}
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingExcludeIfUnsentProcs','0','')";
				Db.NonQ32(command);
				command="SELECT MAX(DefNum) FROM definition";
				int defNum=PIn.Int(Db.GetScalar(command))+1;
				command="SELECT MAX(ItemOrder) FROM definition WHERE Category=18";
				int order=PIn.Int(Db.GetScalar(command))+1;
				command="INSERT INTO definition (DefNum,Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES("
					+POut.Long(defNum)+",18,"+POut.Long(order)+",'Tooth Charts','T',0,0)";
				Db.NonQ32(command);
				command="ALTER TABLE apptview ADD OnlyScheduledProvs tinyint unsigned NOT NULL";
				Db.NonQ32(command);
				//Get rid of some old columns
				command="ALTER TABLE appointment DROP Lab";
				Db.NonQ32(command);
				command="ALTER TABLE appointment DROP InstructorNum";
				Db.NonQ32(command);
				command="ALTER TABLE appointment DROP SchoolClassNum";
				Db.NonQ32(command);
				command="ALTER TABLE appointment DROP SchoolCourseNum";
				Db.NonQ32(command);
				command="ALTER TABLE appointment DROP GradePoint";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS graphicassembly";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS graphicelement";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS graphicpoint";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS graphicshape";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS graphictype";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS proclicense";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS scheddefault";
				Db.NonQ32(command);
				//Change all primary and foreign keys to 64 bit---------------------------------------------------------------
				command="ALTER TABLE account CHANGE AccountNum AccountNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE accountingautopay CHANGE AccountingAutoPayNum AccountingAutoPayNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE accountingautopay CHANGE PayType PayType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE adjustment CHANGE AdjNum AdjNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE adjustment CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE adjustment CHANGE AdjType AdjType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE adjustment CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE adjustment CHANGE ProcNum ProcNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE AptNum AptNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE Confirmed Confirmed bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE Op Op bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE ProvHyg ProvHyg bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE NextAptNum NextAptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE UnschedStatus UnschedStatus bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE Assistant Assistant bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE InsPlan1 InsPlan1 bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointment CHANGE InsPlan2 InsPlan2 bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE appointmentrule CHANGE AppointmentRuleNum AppointmentRuleNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE apptview CHANGE ApptViewNum ApptViewNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE apptviewitem CHANGE ApptViewItemNum ApptViewItemNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE apptviewitem CHANGE ApptViewNum ApptViewNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE apptviewitem CHANGE OpNum OpNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE apptviewitem CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE autocode CHANGE AutoCodeNum AutoCodeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE autocodecond CHANGE AutoCodeCondNum AutoCodeCondNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE autocodecond CHANGE AutoCodeItemNum AutoCodeItemNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE autocodeitem CHANGE AutoCodeItemNum AutoCodeItemNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE autocodeitem CHANGE AutoCodeNum AutoCodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE autocodeitem CHANGE CodeNum CodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE autonote CHANGE AutoNoteNum AutoNoteNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE autonotecontrol CHANGE AutoNoteControlNum AutoNoteControlNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE benefit CHANGE BenefitNum BenefitNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE benefit CHANGE PlanNum PlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE benefit CHANGE PatPlanNum PatPlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE benefit CHANGE CovCatNum CovCatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE benefit CHANGE CodeNum CodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE canadianclaim CHANGE ClaimNum ClaimNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE canadianextract CHANGE CanadianExtractNum CanadianExtractNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE canadianextract CHANGE ClaimNum ClaimNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE canadiannetwork CHANGE CanadianNetworkNum CanadianNetworkNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE carrier CHANGE CarrierNum CarrierNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE carrier CHANGE CanadianNetworkNum CanadianNetworkNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE ClaimNum ClaimNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE PlanNum PlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE ProvTreat ProvTreat bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE ProvBill ProvBill bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE ReferringProv ReferringProv bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE PlanNum2 PlanNum2 bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claim CHANGE ClaimForm ClaimForm bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimattach CHANGE ClaimAttachNum ClaimAttachNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claimattach CHANGE ClaimNum ClaimNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimcondcodelog CHANGE ClaimCondCodeLogNum ClaimCondCodeLogNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claimcondcodelog CHANGE ClaimNum ClaimNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimform CHANGE ClaimFormNum ClaimFormNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claimformitem CHANGE ClaimFormItemNum ClaimFormItemNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claimformitem CHANGE ClaimFormNum ClaimFormNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimpayment CHANGE ClaimPaymentNum ClaimPaymentNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claimpayment CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimpayment CHANGE DepositNum DepositNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc CHANGE ClaimProcNum ClaimProcNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc CHANGE ProcNum ProcNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc CHANGE ClaimNum ClaimNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc CHANGE ClaimPaymentNum ClaimPaymentNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc CHANGE PlanNum PlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE claimvalcodelog CHANGE ClaimValCodeLogNum ClaimValCodeLogNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE claimvalcodelog CHANGE ClaimNum ClaimNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE clearinghouse CHANGE ClearinghouseNum ClearinghouseNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE clinic CHANGE ClinicNum ClinicNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE clinic CHANGE InsBillingProv InsBillingProv bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE clockevent CHANGE ClockEventNum ClockEventNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE clockevent CHANGE EmployeeNum EmployeeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE commlog CHANGE CommlogNum CommlogNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE commlog CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE commlog CHANGE CommType CommType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE commlog CHANGE UserNum UserNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE computer CHANGE ComputerNum ComputerNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE computerpref CHANGE ComputerPrefNum ComputerPrefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE contact CHANGE ContactNum ContactNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE contact CHANGE Category Category bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE covcat CHANGE CovCatNum CovCatNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE covspan CHANGE CovSpanNum CovSpanNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE covspan CHANGE CovCatNum CovCatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE definition CHANGE DefNum DefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE deletedobject CHANGE DeletedObjectNum DeletedObjectNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE deletedobject CHANGE ObjectNum ObjectNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE deposit CHANGE DepositNum DepositNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE disease CHANGE DiseaseNum DiseaseNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE disease CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE disease CHANGE DiseaseDefNum DiseaseDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE diseasedef CHANGE DiseaseDefNum DiseaseDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE displayfield CHANGE DisplayFieldNum DisplayFieldNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE document CHANGE DocNum DocNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE document CHANGE DocCategory DocCategory bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE document CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE document CHANGE MountItemNum MountItemNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE dunning CHANGE DunningNum DunningNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE dunning CHANGE BillingType BillingType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE electid CHANGE ElectIDNum ElectIDNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE emailattach CHANGE EmailAttachNum EmailAttachNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE emailattach CHANGE EmailMessageNum EmailMessageNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE emailmessage CHANGE EmailMessageNum EmailMessageNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE emailmessage CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE emailtemplate CHANGE EmailTemplateNum EmailTemplateNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE employee CHANGE EmployeeNum EmployeeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE employer CHANGE EmployerNum EmployerNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE EtransNum EtransNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE ClearingHouseNum ClearingHouseNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE ClaimNum ClaimNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE CarrierNum CarrierNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE CarrierNum2 CarrierNum2 bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE EtransMessageTextNum EtransMessageTextNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE AckEtransNum AckEtransNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etrans CHANGE PlanNum PlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE etransmessagetext CHANGE EtransMessageTextNum EtransMessageTextNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE fee CHANGE FeeNum FeeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE fee CHANGE FeeSched FeeSched bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE fee CHANGE CodeNum CodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE feesched CHANGE FeeSchedNum FeeSchedNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE files CHANGE DocNum DocNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE formpat CHANGE FormPatNum FormPatNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE formpat CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE grouppermission CHANGE GroupPermNum GroupPermNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE grouppermission CHANGE UserGroupNum UserGroupNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE hl7msg CHANGE HL7MsgNum HL7MsgNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE hl7msg CHANGE AptNum AptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insfilingcode CHANGE InsFilingCodeNum InsFilingCodeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE insfilingcodesubtype CHANGE InsFilingCodeSubTypeNum InsFilingCodeSubTypeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE insfilingcodesubtype CHANGE InsFilingCodeNum InsFilingCodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE PlanNum PlanNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE Subscriber Subscriber bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE FeeSched FeeSched bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE ClaimFormNum ClaimFormNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE CopayFeeSched CopayFeeSched bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE EmployerNum EmployerNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE CarrierNum CarrierNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE AllowedFeeSched AllowedFeeSched bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE FilingCode FilingCode bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE insplan CHANGE FilingCodeSubtype FilingCodeSubtype bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE journalentry CHANGE JournalEntryNum JournalEntryNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE journalentry CHANGE TransactionNum TransactionNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE journalentry CHANGE AccountNum AccountNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE journalentry CHANGE ReconcileNum ReconcileNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE labcase CHANGE LabCaseNum LabCaseNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE labcase CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE labcase CHANGE LaboratoryNum LaboratoryNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE labcase CHANGE AptNum AptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE labcase CHANGE PlannedAptNum PlannedAptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE labcase CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE laboratory CHANGE LaboratoryNum LaboratoryNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE labturnaround CHANGE LabTurnaroundNum LabTurnaroundNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE labturnaround CHANGE LaboratoryNum LaboratoryNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE letter CHANGE LetterNum LetterNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE lettermerge CHANGE LetterMergeNum LetterMergeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE lettermerge CHANGE Category Category bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE lettermergefield CHANGE FieldNum FieldNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE lettermergefield CHANGE LetterMergeNum LetterMergeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE medication CHANGE MedicationNum MedicationNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE medication CHANGE GenericNum GenericNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE medicationpat CHANGE MedicationPatNum MedicationPatNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE medicationpat CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE medicationpat CHANGE MedicationNum MedicationNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE mount CHANGE MountNum MountNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE mount CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE mount CHANGE DocCategory DocCategory bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE mountdef CHANGE MountDefNum MountDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE mountitem CHANGE MountItemNum MountItemNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE mountitem CHANGE MountNum MountNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE mountitemdef CHANGE MountItemDefNum MountItemDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE mountitemdef CHANGE MountDefNum MountDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE operatory CHANGE OperatoryNum OperatoryNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE operatory CHANGE ProvDentist ProvDentist bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE operatory CHANGE ProvHygienist ProvHygienist bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE operatory CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patfield CHANGE PatFieldNum PatFieldNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE patfield CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patfielddef CHANGE PatFieldDefNum PatFieldDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE PatNum PatNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE Guarantor Guarantor bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE PriProv PriProv bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE SecProv SecProv bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE FeeSched FeeSched bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE BillingType BillingType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE EmployerNum EmployerNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE SiteNum SiteNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patient CHANGE ResponsParty ResponsParty bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patientnote CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patplan CHANGE PatPlanNum PatPlanNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE patplan CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE patplan CHANGE PlanNum PlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payment CHANGE PayNum PayNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE payment CHANGE PayType PayType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payment CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payment CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payment CHANGE DepositNum DepositNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payperiod CHANGE PayPeriodNum PayPeriodNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE payplan CHANGE PayPlanNum PayPlanNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE payplan CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payplan CHANGE Guarantor Guarantor bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payplan CHANGE PlanNum PlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payplancharge CHANGE PayPlanChargeNum PayPlanChargeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE payplancharge CHANGE PayPlanNum PayPlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payplancharge CHANGE Guarantor Guarantor bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payplancharge CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE payplancharge CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE paysplit CHANGE SplitNum SplitNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE paysplit CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE paysplit CHANGE PayNum PayNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE paysplit CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE paysplit CHANGE PayPlanNum PayPlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE paysplit CHANGE ProcNum ProcNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE perioexam CHANGE PerioExamNum PerioExamNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE perioexam CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE perioexam CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE periomeasure CHANGE PerioMeasureNum PerioMeasureNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE periomeasure CHANGE PerioExamNum PerioExamNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE pharmacy CHANGE PharmacyNum PharmacyNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE phonenumber CHANGE PhoneNumberNum PhoneNumberNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE phonenumber CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE plannedappt CHANGE PlannedApptNum PlannedApptNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE plannedappt CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE plannedappt CHANGE AptNum AptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE popup CHANGE PopupNum PopupNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE popup CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE printer CHANGE PrinterNum PrinterNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE printer CHANGE ComputerNum ComputerNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procbutton CHANGE ProcButtonNum ProcButtonNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE procbutton CHANGE Category Category bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procbuttonitem CHANGE ProcButtonItemNum ProcButtonItemNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE procbuttonitem CHANGE ProcButtonNum ProcButtonNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procbuttonitem CHANGE AutoCodeNum AutoCodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procbuttonitem CHANGE CodeNum CodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE proccodenote CHANGE ProcCodeNoteNum ProcCodeNoteNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE proccodenote CHANGE CodeNum CodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE proccodenote CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurecode CHANGE CodeNum CodeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE procedurecode CHANGE ProcCat ProcCat bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE ProcNum ProcNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE AptNum AptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE Priority Priority bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE Dx Dx bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE PlannedAptNum PlannedAptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE ProcNumLab ProcNumLab bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE BillingTypeOne BillingTypeOne bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE BillingTypeTwo BillingTypeTwo bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE CodeNum CodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procedurelog CHANGE SiteNum SiteNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procnote CHANGE ProcNoteNum ProcNoteNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE procnote CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procnote CHANGE ProcNum ProcNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE procnote CHANGE UserNum UserNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE proctp CHANGE ProcTPNum ProcTPNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE proctp CHANGE TreatPlanNum TreatPlanNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE proctp CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE proctp CHANGE ProcNumOrig ProcNumOrig bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE proctp CHANGE Priority Priority bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE program CHANGE ProgramNum ProgramNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE programproperty CHANGE ProgramPropertyNum ProgramPropertyNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE programproperty CHANGE ProgramNum ProgramNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE provider CHANGE ProvNum ProvNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE provider CHANGE FeeSched FeeSched bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE provider CHANGE SchoolClassNum SchoolClassNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE provider CHANGE AnesthProvType AnesthProvType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE providerident CHANGE ProviderIdentNum ProviderIdentNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE providerident CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE question CHANGE QuestionNum QuestionNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE question CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE question CHANGE FormPatNum FormPatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE questiondef CHANGE QuestionDefNum QuestionDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE quickpastecat CHANGE QuickPasteCatNum QuickPasteCatNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE quickpastenote CHANGE QuickPasteNoteNum QuickPasteNoteNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE quickpastenote CHANGE QuickPasteCatNum QuickPasteCatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE recall CHANGE RecallNum RecallNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE recall CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE recall CHANGE RecallStatus RecallStatus bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE recall CHANGE RecallTypeNum RecallTypeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE recalltrigger CHANGE RecallTriggerNum RecallTriggerNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE recalltrigger CHANGE RecallTypeNum RecallTypeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE recalltrigger CHANGE CodeNum CodeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE recalltype CHANGE RecallTypeNum RecallTypeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE reconcile CHANGE ReconcileNum ReconcileNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE reconcile CHANGE AccountNum AccountNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE refattach CHANGE RefAttachNum RefAttachNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE refattach CHANGE ReferralNum ReferralNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE refattach CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE referral CHANGE ReferralNum ReferralNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE referral CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE referral CHANGE Slip Slip bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE registrationkey CHANGE RegistrationKeyNum RegistrationKeyNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE registrationkey CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE repeatcharge CHANGE RepeatChargeNum RepeatChargeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE repeatcharge CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqneeded CHANGE ReqNeededNum ReqNeededNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE reqneeded CHANGE SchoolCourseNum SchoolCourseNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqneeded CHANGE SchoolClassNum SchoolClassNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqstudent CHANGE ReqStudentNum ReqStudentNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE reqstudent CHANGE ReqNeededNum ReqNeededNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqstudent CHANGE SchoolCourseNum SchoolCourseNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqstudent CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqstudent CHANGE AptNum AptNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqstudent CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE reqstudent CHANGE InstructorNum InstructorNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE rxalert CHANGE RxAlertNum RxAlertNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE rxalert CHANGE RxDefNum RxDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE rxalert CHANGE DiseaseDefNum DiseaseDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE rxdef CHANGE RxDefNum RxDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE rxpat CHANGE RxNum RxNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE rxpat CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE rxpat CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE rxpat CHANGE PharmacyNum PharmacyNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE schedule CHANGE ScheduleNum ScheduleNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE schedule CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE schedule CHANGE BlockoutType BlockoutType bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE schedule CHANGE EmployeeNum EmployeeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE scheduleop CHANGE ScheduleOpNum ScheduleOpNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE scheduleop CHANGE ScheduleNum ScheduleNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE scheduleop CHANGE OperatoryNum OperatoryNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE schoolclass CHANGE SchoolClassNum SchoolClassNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE schoolcourse CHANGE SchoolCourseNum SchoolCourseNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE screen CHANGE ScreenNum ScreenNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE screen CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE screen CHANGE ScreenGroupNum ScreenGroupNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE screengroup CHANGE ScreenGroupNum ScreenGroupNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE securitylog CHANGE SecurityLogNum SecurityLogNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE securitylog CHANGE UserNum UserNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE securitylog CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sheet CHANGE SheetNum SheetNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE sheet CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sheetdef CHANGE SheetDefNum SheetDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE sheetfield CHANGE SheetFieldNum SheetFieldNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE sheetfield CHANGE SheetNum SheetNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sheetfielddef CHANGE SheetFieldDefNum SheetFieldDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE sheetfielddef CHANGE SheetDefNum SheetDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sigbutdef CHANGE SigButDefNum SigButDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE sigbutdefelement CHANGE ElementNum ElementNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE sigbutdefelement CHANGE SigButDefNum SigButDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sigbutdefelement CHANGE SigElementDefNum SigElementDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sigelement CHANGE SigElementNum SigElementNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE sigelement CHANGE SigElementDefNum SigElementDefNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sigelement CHANGE SignalNum SignalNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE sigelementdef CHANGE SigElementDefNum SigElementDefNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE signal CHANGE SignalNum SignalNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE signal CHANGE TaskNum TaskNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE site CHANGE SiteNum SiteNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE statement CHANGE StatementNum StatementNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE statement CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE statement CHANGE DocNum DocNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE supplier CHANGE SupplierNum SupplierNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE supply CHANGE SupplyNum SupplyNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE supply CHANGE SupplierNum SupplierNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE supply CHANGE Category Category bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE supplyneeded CHANGE SupplyNeededNum SupplyNeededNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE supplyorder CHANGE SupplyOrderNum SupplyOrderNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE supplyorder CHANGE SupplierNum SupplierNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE supplyorderitem CHANGE SupplyOrderItemNum SupplyOrderItemNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE supplyorderitem CHANGE SupplyOrderNum SupplyOrderNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE supplyorderitem CHANGE SupplyNum SupplyNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE task CHANGE TaskNum TaskNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE task CHANGE TaskListNum TaskListNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE task CHANGE KeyNum KeyNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE task CHANGE FromNum FromNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE task CHANGE UserNum UserNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE taskancestor CHANGE TaskAncestorNum TaskAncestorNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE taskancestor CHANGE TaskNum TaskNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE taskancestor CHANGE TaskListNum TaskListNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE tasklist CHANGE TaskListNum TaskListNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE tasklist CHANGE Parent Parent bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE tasklist CHANGE FromNum FromNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE tasksubscription CHANGE TaskSubscriptionNum TaskSubscriptionNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE tasksubscription CHANGE UserNum UserNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE tasksubscription CHANGE TaskListNum TaskListNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE terminalactive CHANGE TerminalActiveNum TerminalActiveNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE terminalactive CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE timeadjust CHANGE TimeAdjustNum TimeAdjustNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE timeadjust CHANGE EmployeeNum EmployeeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE toolbutitem CHANGE ToolButItemNum ToolButItemNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE toolbutitem CHANGE ProgramNum ProgramNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE toothinitial CHANGE ToothInitialNum ToothInitialNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE toothinitial CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE transaction CHANGE TransactionNum TransactionNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE transaction CHANGE UserNum UserNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE transaction CHANGE DepositNum DepositNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE transaction CHANGE PayNum PayNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE treatplan CHANGE TreatPlanNum TreatPlanNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE treatplan CHANGE PatNum PatNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE treatplan CHANGE ResponsParty ResponsParty bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE usergroup CHANGE UserGroupNum UserGroupNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE userod CHANGE UserNum UserNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE userod CHANGE UserGroupNum UserGroupNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE userod CHANGE EmployeeNum EmployeeNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE userod CHANGE ClinicNum ClinicNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE userod CHANGE ProvNum ProvNum bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE userod CHANGE TaskListInBox TaskListInBox bigint NOT NULL";
				Db.NonQ32(command);
				command="ALTER TABLE userquery CHANGE QueryNum QueryNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="ALTER TABLE zipcode CHANGE ZipCodeNum ZipCodeNum bigint NOT NULL auto_increment";
				Db.NonQ32(command);
				command="DROP TABLE IF EXISTS replicationserver";
				Db.NonQ32(command);
				command=@"CREATE TABLE replicationserver (
					ReplicationServerNum bigint NOT NULL auto_increment,
					Descript TEXT NOT NULL,
					ServerId INT unsigned NOT NULL,
					RangeStart BIGINT NOT NULL,
					RangeEnd BIGINT NOT NULL,
					PRIMARY KEY(ReplicationServerNum)
					)";
				Db.NonQ32(command);
				command="ALTER TABLE claimproc ADD WriteOffEst double NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD WriteOffEstOverride double NOT NULL";
				Db.NonQ(command);
				command="UPDATE claimproc SET WriteOffEst = -1";
				Db.NonQ(command);
				command="UPDATE claimproc SET WriteOffEstOverride = -1";
				Db.NonQ(command);
				command="ALTER TABLE paysplit ADD UnearnedType bigint NOT NULL";
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallMaxNumberReminders','-1','')";
				Db.NonQ(command);
				command="ALTER TABLE recall ADD DisableUntilBalance double NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE recall ADD DisableUntilDate date NOT NULL default '0001-01-01'";
				Db.NonQ(command);
				command="ALTER TABLE program ADD PluginDllName varchar(255) NOT NULL";
				Db.NonQ(command);
				command="DELETE FROM preference WHERE PrefName = 'DeductibleBeforePercentAsDefault'";
				Db.NonQ(command);
				//We will not delete this pref just in case it's needed later.  It's not used anywhere right now.
				//command = "DELETE FROM preference WHERE PrefName='EnableAnesthMod'";
				//We will not delete this pref just in case it's needed later.  It's not used anywhere right now.
				//command="DELETE FROM preference WHERE PrefName='ImageStore'";//this option is no longer supported.
				//Db.NonQ(command);
				command="SELECT MAX(ItemOrder) FROM definition WHERE Category=2";
				int itemOrder=PIn.Int(Db.GetScalar(command))+1;//eg 7+1
				//this should end up with an acceptable autoincrement even if using random primary keys.
				command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue) VALUES (2,"+POut.Int(itemOrder)+",'E-mailed','E-mailed')";
				Db.NonQ(command);
				command="SELECT DefNum FROM definition WHERE Category=2 AND ItemOrder="+POut.Int(itemOrder);
				string defNumStr=Db.GetScalar(command);
				command="INSERT INTO preference (PrefName,ValueString) VALUES ('ConfirmStatusEmailed','"+defNumStr+"')";
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString) VALUES ('ConfirmEmailSubject','Appointment Confirmation')";
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString) VALUES ('ConfirmEmailMessage','[NameF], We would like to confirm your appointment on [date] at [time]')";
				Db.NonQ(command);
				command="ALTER TABLE replicationserver ADD AtoZpath varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE replicationserver ADD UpdateBlocked tinyint NOT NULL";
				Db.NonQ(command);
				try {
					command="ALTER TABLE task ADD INDEX (KeyNum)";
					Db.NonQ(command);
				}
				catch { }
				command="UPDATE preference SET ValueString = '6.8.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To6_8_7();
		}

		private static void To6_8_7() {
			//duplicated in 6.7
			if(FromVersion<new Version("6.8.7.0")) {
				string command;
				try {
					command="ALTER TABLE claimpayment ADD INDEX (DepositNum)";
					Db.NonQ(command);
					command="ALTER TABLE payment ADD INDEX (DepositNum)";
					Db.NonQ(command);
				}
				catch {
					//in case any of the indices already exists.
				}
				command="UPDATE preference SET ValueString = '6.8.7.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_8_11();
		}

		private static void To6_8_11() {
			//duplicated in 6.6 and 6.7
			if(FromVersion<new Version("6.8.11.0")) {
				string command;
				//Mediadent version 4 and 5---------------------------------------
				command="SELECT COUNT(*) FROM programproperty WHERE PropertyDesc='MediaDent Version 4 or 5'";
				if(Db.GetScalar(command)=="0") {
					command="SELECT ProgramNum FROM program WHERE ProgName='MediaDent'";
					long programNum=PIn.Long(Db.GetScalar(command));
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.Long(programNum)+"', "
						+"'"+POut.String("MediaDent Version 4 or 5")+"', "
						+"'5')";
					Db.NonQ(command);
					//add back the image folder
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.Long(programNum)+"', "
						+"'"+POut.String("Image Folder")+"', "
						+"'"+POut.String(@"C:\Mediadent\patients\")+"')";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '6.8.11.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_8_12();
		}

		private static void To6_8_12() {
			if(FromVersion<new Version("6.8.12.0")) {
				string command;
				//Ewoo_EZDent bridge-------------------------------------------------------------------------
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'EwooEZDent', "
					+"'EwooEZDent from www.ewoousa.com', "
					+"'0', "
					+"'"+POut.String(@"C:\EasyDent4\Edp4\EasyDent4.exe")+"', "
					+"'', "
					+"'')";
				long programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+POut.Long(programNum)+"', "
					+"'"+POut.Long((int)ToolBarsAvail.ChartModule)+"', "
					+"'EZDent')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.8.12.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_8_24();
		}

		private static void To6_8_24() {
			if(FromVersion<new Version("6.8.24.0")) {
				string command;
				command="SELECT CodeNum FROM procedurecode WHERE ProcCode='D1204'";
				string codeNum1204=Db.GetScalar(command);
				command="SELECT CodeNum FROM procedurecode WHERE ProcCode='D1203'";
				string codeNum1203=Db.GetScalar(command);
				if(codeNum1203!="" && codeNum1204!="") {
					command="UPDATE benefit SET CodeNum="+codeNum1203+" WHERE CodeNum="+codeNum1204;
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '6.8.24.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_9_1();
		}

		private static void To6_9_1() {
			if(FromVersion<new Version("6.9.1.0")) {
				string command;
				//Mountainside Bridge---------------------------------------------------------------------------
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'Mountainside', "
					+"'Mountainside from www.mountainsidesoftware.com', "
					+"'0', "
					+"'', "
					+"'', "
					+"'')";
				Db.NonQ(command);
				//Move the HL7 folders from eCW to the pref table
				command="SELECT PropertyValue FROM programproperty WHERE PropertyDesc='HL7FolderOut'";
				string folder=Db.GetScalar(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('HL7FolderOut','"+POut.String(folder)+"')";
				Db.NonQ(command);
				command="DELETE FROM programproperty WHERE PropertyDesc='HL7FolderOut'";
				Db.NonQ(command);
				command="SELECT PropertyValue FROM programproperty WHERE PropertyDesc='HL7FolderIn'";
				folder=Db.GetScalar(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('HL7FolderIn','"+POut.String(folder)+"')";
				Db.NonQ(command);
				command="DELETE FROM programproperty WHERE PropertyDesc='HL7FolderIn'";
				Db.NonQ(command);
				//Clinic enhancements----------------------------------------------------------------------------------------
				command="ALTER TABLE paysplit ADD ClinicNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE paysplit ADD INDEX (ClinicNum)";
				Db.NonQ(command);
				command="Update payment,paysplit SET paysplit.ClinicNum = payment.ClinicNum WHERE paysplit.PayNum = payment.PayNum";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD ClinicNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD INDEX (ClinicNum)";
				Db.NonQ(command);
				command="Update procedurelog,claimproc SET claimproc.ClinicNum = procedurelog.ClinicNum WHERE claimproc.ProcNum = procedurelog.ProcNum";
				Db.NonQ(command);
				//then, for claimprocs that are total payments and not attached to any proc:
				command="Update claim,claimproc SET claimproc.ClinicNum = claim.ClinicNum WHERE claimproc.ClaimNum = claim.ClaimNum AND claimproc.ProcNum=0";
				Db.NonQ(command);
				command="ALTER TABLE adjustment ADD ClinicNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE adjustment ADD INDEX (ClinicNum)";
				Db.NonQ(command);
				command="ALTER TABLE payplancharge ADD ClinicNum bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE payplancharge ADD INDEX (ClinicNum)";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PerioColorCAL','-16777011')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PerioColorFurcations','-16777216')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PerioColorFurcationsRed','-7667712')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PerioColorGM','-8388480')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PerioColorMGJ','-29696')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PerioColorProbing','-16744448')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PerioColorProbingRed','-65536')";
				Db.NonQ(command);
				command="ALTER TABLE registrationkey ADD VotesAllotted int NOT NULL";
				Db.NonQ(command);
				command="UPDATE registrationkey SET VotesAllotted =100";
				Db.NonQ(command);
				command="ALTER TABLE apptview ADD OnlySchedBeforeTime time NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE apptview ADD OnlySchedAfterTime time NOT NULL";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS automation";
				Db.NonQ(command);
				command=@"CREATE TABLE automation (
					AutomationNum bigint NOT NULL auto_increment,
					Description text NOT NULL,
					Autotrigger tinyint NOT NULL,
					ProcCodes text NOT NULL,
					AutoAction tinyint NOT NULL,
					SheetNum bigint NOT NULL,
					CommType bigint NOT NULL,
					MessageContent text NOT NULL,
					PRIMARY KEY(AutomationNum)
					)";
				Db.NonQ(command);
				command="ALTER TABLE sheet ADD Description varchar(255) NOT NULL";
				Db.NonQ(command);
				//for each sheettype, set descriptions for all sheets of that type.
				for(int i=0;i<Enum.GetNames(typeof(SheetTypeEnum)).Length;i++) {
					command="UPDATE sheet SET Description= '"+POut.String(Enum.GetNames(typeof(SheetTypeEnum))[i])+"' "
						+"WHERE SheetType="+POut.Int(i);
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '6.9.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_9_4();
		}

		private static void To6_9_4() {
			if(FromVersion<new Version("6.9.4.0")) {
				string command;
				command="ALTER TABLE automation CHANGE SheetNum SheetDefNum bigint NOT NULL";
				Db.NonQ(command);
				//Trophy
				command="SELECT ProgramNum FROM program WHERE ProgName='TrophyEnhanced'";
				long programNum=PIn.Long(Db.GetScalar(command));
				if(programNum>0) {
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.Long(programNum)+"', "
						+"'Enter 1 to enable Numbered Mode', "
						+"'0')";
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '6.9.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_9_10();
		}

		private static void To6_9_10() {
			if(FromVersion<new Version("6.9.10.0")) {
				string command;
				command="ALTER TABLE computerpref ADD COLUMN DirectXFormat VARCHAR(255) DEFAULT ''";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.9.10.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_0_1();
		}

		private static void To7_0_1() {
			if(FromVersion<new Version("7.0.1.0")) {
				string command;
				command="INSERT INTO preference(PrefName,ValueString) VALUES('InsDefaultShowUCRonClaims','0')";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS equipment";
				Db.NonQ(command);
				command=@"CREATE TABLE equipment (
					EquipmentNum bigint NOT NULL auto_increment,
					Description text NOT NULL,
					SerialNumber varchar(255),
					ModelYear varchar(2),
					DatePurchased date NOT NULL default '0001-01-01',
					DateSold date NOT NULL default '0001-01-01',
					PurchaseCost double NOT NULL,
					MarketValue double NOT NULL,
					Location text NOT NULL,
					DateEntry date NOT NULL default '0001-01-01',
					PRIMARY KEY(EquipmentNum)
					)";
				Db.NonQ(command);
				command="ALTER TABLE sheet ADD ShowInTerminal tinyint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE sheetfielddef ADD RadioButtonValue varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE sheetfield ADD RadioButtonValue varchar(255) NOT NULL";
				Db.NonQ(command);
				//add a bunch of indexes to the benefit table to make it faster when there are many similar plans
				command="ALTER TABLE benefit ADD INDEX(CovCatNum)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(BenefitType)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(Percent)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(MonetaryAmt)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(TimePeriod)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(QuantityQualifier)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(Quantity)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(CodeNum)";
				Db.NonQ(command);
				command="ALTER TABLE benefit ADD INDEX(CoverageLevel)";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.0.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ32(command);
			}
			To7_1_1();
		}

		private static void To7_1_1() {
			if(FromVersion<new Version("7.1.1.0")) {
				string command;
				try {
					command="ALTER TABLE refattach ADD INDEX (PatNum)";
					Db.NonQ(command);
				}
				catch {}
				command="INSERT INTO preference(PrefName,ValueString) VALUES('UpdateShowMsiButtons','0')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('ReportsPPOwriteoffDefaultToProcDate','0')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('ReportsShowPatNum','0')";
				Db.NonQ(command);
				command="ALTER TABLE userod ADD DefaultHidePopups tinyint NOT NULL";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS taskunread";
				Db.NonQ(command);
				command=@"CREATE TABLE taskunread (
					TaskUnreadNum bigint NOT NULL auto_increment,
					TaskNum bigint NOT NULL,
					UserNum bigint NOT NULL,
					PRIMARY KEY (TaskUnreadNum),
					INDEX(TaskNum),
					INDEX(UserNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				//MercuryDE clearinghouse.
				command=@"INSERT INTO clearinghouse(Description,ExportPath,IsDefault,Payors,Eformat,
Password,ResponsePath,CommBridge,ClientProgram,ISA05,ISA07,
ISA08,
ISA15,
GS03) 
VALUES('MercuryDE','"+POut.String(@"C:\MercuryDE\Temp\")+@"','0','','1','','','11','','ZZ','ZZ',
'204203105',
'P',
'204203105')";
				Db.NonQ(command);
				command="ALTER TABLE clockevent CHANGE TimeEntered TimeEntered1 datetime NOT NULL default '0001-01-01 00:00:00'";
				Db.NonQ(command);
				command="ALTER TABLE clockevent CHANGE TimeDisplayed TimeDisplayed1 datetime NOT NULL default '0001-01-01 00:00:00'";
				Db.NonQ(command);
				command="ALTER TABLE clockevent ADD TimeEntered2 datetime NOT NULL default '0001-01-01 00:00:00'";
				Db.NonQ(command);
				command="ALTER TABLE clockevent ADD TimeDisplayed2 datetime NOT NULL default '0001-01-01 00:00:00'";
				Db.NonQ(command);
				command="SELECT * FROM clockevent WHERE ClockStatus != 2 ORDER BY EmployeeNum,TimeDisplayed1";
				DataTable table=Db.GetTable(command);
				DateTime timeEntered2;
				DateTime timeDisplayed2;
				string note;
				int clockStatus;
				for(int i=0;i<table.Rows.Count;i++){
					if(table.Rows[i]["ClockIn"].ToString()=="0"){//false
						continue;//only stop on clock-in rows, not clock-out.
					}
					if(i==table.Rows.Count-1){//if this is the last row
						break;//because we always need the next clock-out to actually do anything.
					}
					if(table.Rows[i+1]["ClockIn"].ToString()=="1"){//true
						continue;//if the next row is also a clock-in, then we have two clock-ins in a row.  Can't do anything.
					}
					if(table.Rows[i]["EmployeeNum"].ToString()!=table.Rows[i+1]["EmployeeNum"].ToString()){//employeeNums don't match
						continue;
					}
					timeEntered2=PIn.DateT(table.Rows[i+1]["TimeEntered1"].ToString());//The time of the second row
					timeDisplayed2=PIn.DateT(table.Rows[i+1]["TimeDisplayed1"].ToString());
					clockStatus=PIn.Int(table.Rows[i+1]["ClockStatus"].ToString());
					note=PIn.String(table.Rows[i+1]["Note"].ToString());
					command="UPDATE clockevent SET "
						+"TimeEntered2 = "+POut.DateT(timeEntered2)+", "
						+"TimeDisplayed2 = "+POut.DateT(timeDisplayed2)+", "
						+"ClockStatus = "+POut.Int(clockStatus)+", "
						+"Note = CONCAT(Note,'"+POut.String(note)+"') "
						+"WHERE ClockEventNum = "+table.Rows[i]["ClockEventNum"].ToString();
					Db.NonQ(command);
					command="DELETE FROM clockevent WHERE ClockEventNum = "+table.Rows[i+1]["ClockEventNum"].ToString();
					Db.NonQ(command);
				}
				//now, breaks, which are out/in instead of in/out.
				command="SELECT * FROM clockevent WHERE ClockStatus = 2 ORDER BY EmployeeNum,TimeDisplayed1";
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					if(table.Rows[i]["ClockIn"].ToString()=="1"){//true
						continue;//only stop on clock-out rows, not clock-in.
					}
					if(i==table.Rows.Count-1){//if this is the last row
						break;//because we always need the next clock-in to actually do anything.
					}
					if(table.Rows[i+1]["ClockIn"].ToString()=="0"){//false
						continue;//if the next row is also a clock-out, then we have two clock-outs in a row.  Can't do anything.
					}
					if(table.Rows[i]["EmployeeNum"].ToString()!=table.Rows[i+1]["EmployeeNum"].ToString()){//employeeNums don't match
						continue;
					}
					timeEntered2=PIn.DateT(table.Rows[i+1]["TimeEntered1"].ToString());//The time of the second row
					timeDisplayed2=PIn.DateT(table.Rows[i+1]["TimeDisplayed1"].ToString());
					//clockStatus=PIn.Int(table.Rows[i+1]["ClockStatus"].ToString());
					note=PIn.String(table.Rows[i+1]["Note"].ToString());
					command="UPDATE clockevent SET "
						+"TimeEntered2 = "+POut.DateT(timeEntered2)+", "
						+"TimeDisplayed2 = "+POut.DateT(timeDisplayed2)+", "
						//+"ClockStatus = "+POut.Int(clockStatus)+", "
						+"Note = CONCAT(Note,'"+POut.String(note)+"') "
						+"WHERE ClockEventNum = "+table.Rows[i]["ClockEventNum"].ToString();
					Db.NonQ(command);
					command="DELETE FROM clockevent WHERE ClockEventNum = "+table.Rows[i+1]["ClockEventNum"].ToString();
					Db.NonQ(command);
				}
				command="ALTER TABLE clockevent DROP ClockIn";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('PasswordsMustBeStrong','0')";
				Db.NonQ(command);
				command="ALTER TABLE userod ADD PasswordIsStrong tinyint NOT NULL";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('SecurityLockDays','0')";
				Db.NonQ(command);
				command="ALTER TABLE laboratory DROP LabSlip";
				Db.NonQ(command);
				command="ALTER TABLE laboratory ADD Slip bigint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE laboratory ADD Address varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE laboratory ADD City varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE laboratory ADD State varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE laboratory ADD Zip varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE laboratory ADD Email varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE laboratory ADD WirelessPhone varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE procedurelog ADD HideGraphics tinyint NOT NULL";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.1.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_1_2();
		}

		private static void To7_1_2() {
			if(FromVersion<new Version("7.1.2.0")) {
				string command;
				command="ALTER TABLE provider ADD TaxonomyCodeOverride varchar(255) NOT NULL";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.1.2.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_1_16();
		}

		private static void To7_1_16() {
			if(FromVersion<new Version("7.1.16.0")) {
				string command;
				command="ALTER TABLE etransmessagetext CHANGE MessageText MessageText mediumtext NOT NULL";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.1.16.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_1_18();
		}

		private static void To7_1_18() {
			if(FromVersion<new Version("7.1.18.0")) {
				string command;
				command="INSERT INTO preference(PrefName,ValueString) VALUES('ToothChartMoveMenuToRight','0')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.1.18.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_1_24();
		}

		private static void To7_1_24() {
			if(FromVersion<new Version("7.1.24.0")) {
				string command;
				command="UPDATE patient SET Guarantor=PatNum WHERE Guarantor=0;";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.1.24.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_2_1();
		}

		private static void To7_2_1() {
			if(FromVersion<new Version("7.2.1.0")) {
				string command;
				//this column was a varchar holding currency amounts.
				command="ALTER TABLE claimvalcodelog CHANGE ValAmount ValAmount double not null";
				Db.NonQ(command);
				command="ALTER TABLE carrier ADD CanadianEncryptionMethod tinyint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE carrier ADD CanadianTransactionPrefix varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE carrier ADD CanadianSupportedTypes int NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE canadianclaim DROP EligibilityCode";
				Db.NonQ(command);
				command="ALTER TABLE canadianclaim DROP SchoolName";
				Db.NonQ(command);
				command="ALTER TABLE patient ADD CanadianEligibilityCode tinyint NOT NULL";
				Db.NonQ(command);
				command="DROP TABLE canadianextract";
				Db.NonQ(command);
				command="DROP TABLE canadianclaim";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianMaterialsForwarded varchar(10) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianReferralProviderNum varchar(20) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianReferralReason tinyint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianIsInitialLower varchar(5) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianDateInitialLower date NOT NULL default '0001-01-01'";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianMandProsthMaterial tinyint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianIsInitialUpper varchar(5) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianDateInitialUpper date NOT NULL default '0001-01-01'";
				Db.NonQ(command);
				command="ALTER TABLE claim ADD CanadianMaxProsthMaterial tinyint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE carrier DROP IsPMP";
				Db.NonQ(command);
				command="ALTER TABLE insplan ADD CanadianPlanFlag varchar(5) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE procedurelog ADD CanadianTypeCodes varchar(20) NOT NULL";
				Db.NonQ(command);
				command="UPDATE clearinghouse SET ResponsePath='"+POut.String(@"C:\MercuryDE\Reports\")+"' WHERE ResponsePath='' AND Description='MercuryDE' LIMIT 1";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS guardian";
				Db.NonQ(command);
				command=@"CREATE TABLE guardian (
					GuardianNum bigint NOT NULL auto_increment,
					PatNumChild bigint NOT NULL,
					PatNumGuardian bigint NOT NULL,
					Relationship tinyint NOT NULL,
					PRIMARY KEY (GuardianNum),
					INDEX(PatNumChild),
					INDEX(PatNumGuardian)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				command="ALTER TABLE apptviewitem ADD ElementAlignment tinyint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE apptview ADD StackBehavUR tinyint NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE apptview ADD StackBehavLR tinyint NOT NULL";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS apptfield";
				Db.NonQ(command);
				command=@"CREATE TABLE apptfield (
					ApptFieldNum bigint NOT NULL auto_increment,
					AptNum bigint NOT NULL,
					FieldName varchar(255) NOT NULL,
					FieldValue text NOT NULL,
					PRIMARY KEY (ApptFieldNum),
					INDEX(AptNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS apptfielddef";
				Db.NonQ(command);
				command=@"CREATE TABLE apptfielddef (
					ApptFieldDefNum bigint NOT NULL auto_increment,
					FieldName varchar(255) NOT NULL,
					PRIMARY KEY (ApptFieldDefNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				try {
					command="ALTER TABLE patfield ADD INDEX (PatNum)";
					Db.NonQ(command);
				}
				catch {
					//in case the index already exists.
				}
				command="ALTER TABLE labcase ADD LabFee double NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE insplan CHANGE PlanNote PlanNote text NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE insplan CHANGE BenefitNotes BenefitNotes text NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE insplan CHANGE SubscNote SubscNote text NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE apptviewitem ADD ApptFieldDefNum bigint NOT NULL";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.2.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_2_3();
		}

		private static void To7_2_3() {
			if(FromVersion<new Version("7.2.3.0")) {
				string command;
				command="ALTER TABLE apptviewitem ADD PatFieldDefNum bigint NOT NULL";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.2.3.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_2_4();
		}

		private static void To7_2_4() {
			if(FromVersion<new Version("7.2.4.0")) {
				string command;
				command="UPDATE apptview SET StackBehavUR=1";//all horiz
				Db.NonQ(command);
				command="SELECT ApptViewNum FROM apptview";//all of them.
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					command="SELECT COUNT(*) FROM apptviewitem WHERE ApptViewNum="+table.Rows[i]["ApptViewNum"].ToString()
						+" AND ElementDesc='AssistantAbbr'";
					if(Db.GetCount(command)=="0") {
						command="INSERT INTO apptviewitem (ApptViewNum,ElementDesc,ElementOrder,ElementColor,ElementAlignment) VALUES("
							+table.Rows[i]["ApptViewNum"].ToString()+","
							+"'AssistantAbbr',"
							+"0,"
							+"-16777216,"//black
							+"2)";//LR
						Db.NonQ(command);
					}
					command="SELECT COUNT(*) FROM apptviewitem WHERE ApptViewNum="+table.Rows[i]["ApptViewNum"].ToString()
						+" AND ElementDesc='ConfirmedColor'";
					if(Db.GetCount(command)=="0") {
						command="INSERT INTO apptviewitem (ApptViewNum,ElementDesc,ElementOrder,ElementColor,ElementAlignment) VALUES("
							+table.Rows[i]["ApptViewNum"].ToString()+","
							+"'ConfirmedColor',"
							+"0,"
							+"-16777216,"
							+"1)";//UR
						Db.NonQ(command);
					}
					command="SELECT COUNT(*) FROM apptviewitem WHERE ApptViewNum="+table.Rows[i]["ApptViewNum"].ToString()
						+" AND ElementDesc='HasIns[I]'";
					if(Db.GetCount(command)=="0") {
						command="INSERT INTO apptviewitem (ApptViewNum,ElementDesc,ElementOrder,ElementColor,ElementAlignment) VALUES("
							+table.Rows[i]["ApptViewNum"].ToString()+","
							+"'HasIns[I]',"
							+"1,"
							+"-16777216,"
							+"1)";//UR
						Db.NonQ(command);
					}
					command="SELECT COUNT(*) FROM apptviewitem WHERE ApptViewNum="+table.Rows[i]["ApptViewNum"].ToString()
						+" AND ElementDesc='InsToSend[!]'";
					if(Db.GetCount(command)=="0") {
						command="INSERT INTO apptviewitem (ApptViewNum,ElementDesc,ElementOrder,ElementColor,ElementAlignment) VALUES("
							+table.Rows[i]["ApptViewNum"].ToString()+","
							+"'InsToSend[!]',"
							+"2,"
							+"-65536,"//red
							+"1)";//UR
						Db.NonQ(command);
					}
				}
				command="UPDATE preference SET ValueString = '7.2.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_2_7();
		}

		private static void To7_2_7() {
			if(FromVersion<new Version("7.2.7.0")) {
				string command;
				command="UPDATE apptviewitem SET ElementColor=-1 WHERE ElementDesc='"+POut.String("MedOrPremed[+]")+"'";//white
				Db.NonQ(command);
				command="UPDATE apptviewitem SET ElementColor=-1 WHERE ElementDesc='"+POut.String("HasIns[I]")+"'";
				Db.NonQ(command);
				command="UPDATE apptviewitem SET ElementColor=-1 WHERE ElementDesc='"+POut.String("InsToSend[!]")+"'";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.2.7.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_2_12();
		}

		private static void To7_2_12() {
			if(FromVersion<new Version("7.2.12.0")) {
				string command;
				command="INSERT INTO preference(PrefName,ValueString) VALUES('RecallUseEmailIfHasEmailAddress','0')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '7.2.12.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_2_31();
		}

		private static void To7_2_31() {
			if(FromVersion<new Version("7.2.31.0")) {
				string command;
				//add Sopro bridge:
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'Sopro', "
					+"'Sopro by Acteon www.acteongroup.com', "
					+"'0', "
					+"'"+POut.String(@"C:\Program Files\Sopro Imaging\SOPRO Imaging.exe")+"', "
					+"'', "
					+"'')";
				Db.NonQ(command);
				command="SELECT ProgramNum FROM program WHERE ProgName='Sopro' LIMIT 1";
				long programNum=PIn.Long(Db.GetScalar(command));
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.Long(programNum)+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ32(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+POut.Long(programNum)+"', "
					+"'"+POut.Int(((int)ToolBarsAvail.ChartModule))+"', "
					+"'Sopro')";
				Db.NonQ32(command);
				command="UPDATE preference SET ValueString = '7.2.31.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To7_3_0();
		}

		private static void To7_3_0() {
			if(FromVersion<new Version("7.3.0.0")) {
				string command;
				command="ALTER TABLE patient CHANGE SchoolName SchoolName varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE sheet ADD IsWebForm tinyint NOT NULL";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('WebHostSynchServerURL','https://opendentalsoft.com/WebHostSynch/WebHostSynch.aspx')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('WebFormsBorderColor','3896686')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('WebFormsHeading1','PATIENT INFORMATION')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('WebFormsHeading2','"+POut.String("We are pleased to welcome you to our office. Please take a few minutes to fill out this form as completely as you can. If you have any questions we'll be glad to help you.")+"')";
				Db.NonQ(command);
				command="ALTER TABLE appointment ADD TimeAskedToArrive datetime NOT NULL default '0001-01-01 00:00:00'";
				Db.NonQ(command);
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'OrthoPlex', "
					+"'OrthoPlex from Dentsply GAC', "
					+"'0', "
					+"'"+POut.String(@"C:\\Program Files\\GAC\\OrthoPlex v3.20\\OrthoPlex.exe")+"', "
					+"'-E [PatNum]', "
					+"'')";
				Db.NonQ(command);
				command="SELECT ProgramNum FROM program WHERE ProgName='OrthoPlex' LIMIT 1";
				long programNum=PIn.Long(Db.GetScalar(command));
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+POut.Long(programNum)+"', "
					+"'"+POut.Int((int)ToolBarsAvail.ChartModule)+"', "
					+"'OrthoPlex')";
				Db.NonQ(command);
				command="ALTER TABLE patient ADD AskToArriveEarly int NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE appointment ADD ProcsColored text NOT NULL";
				Db.NonQ(command);



				command="UPDATE preference SET ValueString = '7.3.0.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			//To7_4_0();
		}




	}
}


			
		



				
				


