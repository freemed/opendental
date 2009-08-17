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
		public static System.Version LatestVersion=new Version("6.8.0.0");//This value must be changed when a new conversion is to be triggered.

		private static void To6_2_9() {
			if(FromVersion<new Version("6.2.9.0")) {
				string command="ALTER TABLE fee CHANGE FeeSched FeeSched int NOT NULL";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.2.9.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_3_1();
		}

		private static void To6_3_1() {
			if(FromVersion<new Version("6.3.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncPath','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncLastFileNumber','0','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('MobileSyncDateTimeLastRun','0001-01-01','')";
					Db.NonQ(command);
					//I had originally deleted these.  But decided instead to just comment them as obsolete because I think it caused a bug in our upgrade.
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName = 'LettersIncludeReturnAddress'";
					Db.NonQ(command);
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName ='StationaryImage'";
					Db.NonQ(command);
					command="UPDATE preference SET Comments = 'Obsolete' WHERE PrefName ='StationaryDocument'";
					Db.NonQ(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.3.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_3_3();
		}

		private static void To6_3_3() {
			if(FromVersion<new Version("6.3.3.0")) {
				string command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('CoPay_FeeSchedule_BlankLikeZero','1','1 to treat blank entries like zero copay.  0 to make patient responsible on blank entries.')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.3.3.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_3_4();
		}

		private static void To6_3_4() {
			if(FromVersion<new Version("6.3.4.0")) {
				string command="ALTER TABLE sheetfielddef CHANGE FieldValue FieldValue text NOT NULL";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.3.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_4_1();
		}

		private static void To6_4_1() {
			if(FromVersion<new Version("6.4.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="UPDATE preference SET Comments = '-1 indicates min for all dates' WHERE PrefName = 'RecallDaysPast'";
					Db.NonQ(command);
					command="UPDATE preference SET Comments = '-1 indicates max for all dates' WHERE PrefName = 'RecallDaysFuture'";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('RecallShowIfDaysFirstReminder','-1','-1 indicates do not show')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName, ValueString,Comments) VALUES ('RecallShowIfDaysSecondReminder','-1','-1 indicates do not show')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage','You are due for your regular dental check-up on ?DueDate  Please call our office today to schedule an appointment.','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg','You are due for your regular dental check-up.  [FamilyList]  Please call our office today to schedule an appointment.','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject2','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage2','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardMessage2','','')";
					Db.NonQ(command);
					string prefVal;
					DataTable table;
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPostcardMessage'";
					table=Db.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?DueDate","[DueDate]");
					command="UPDATE preference SET ValueString='"+POut.PString(prefVal)+"' WHERE PrefName='RecallPostcardMessage'";
					Db.NonQ(command);
					command="SELECT ValueString FROM preference WHERE PrefName='RecallPostcardFamMsg'";
					table=Db.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?FamilyList","[FamilyList]");
					command="UPDATE preference SET ValueString='"+POut.PString(prefVal)+"' WHERE PrefName='RecallPostcardFamMsg'";
					Db.NonQ(command);
					command="SELECT ValueString FROM preference WHERE PrefName='ConfirmPostcardMessage'";
					table=Db.GetTable(command);
					prefVal=table.Rows[0][0].ToString().Replace("?date","[date]");
					prefVal=prefVal.Replace("?time","[time]");
					command="UPDATE preference SET ValueString='"+POut.PString(prefVal)+"' WHERE PrefName='ConfirmPostcardMessage'";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailSubject3','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailMessage3','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardMessage3','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg2','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallEmailFamMsg3','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardFamMsg2','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('RecallPostcardFamMsg3','','')";
					Db.NonQ(command);
					command="ALTER TABLE autonote CHANGE ControlsToInc MainText text";
					Db.NonQ(command);
					command="UPDATE autonote SET MainText = ''";
					Db.NonQ(command);
					command="UPDATE autonotecontrol SET ControlType='Text' WHERE ControlType='MultiLineTextBox'";
					Db.NonQ(command);
					command="UPDATE autonotecontrol SET ControlType='OneResponse' WHERE ControlType='ComboBox'";
					Db.NonQ(command);
					command="UPDATE autonotecontrol SET ControlType='Text' WHERE ControlType='TextBox'";
					Db.NonQ(command);
					command="UPDATE autonotecontrol SET ControlOptions=MultiLineText WHERE MultiLineText != ''";
					Db.NonQ(command);
					command="ALTER TABLE autonotecontrol DROP PrefaceText";
					Db.NonQ(command);
					command="ALTER TABLE autonotecontrol DROP MultiLineText";
					Db.NonQ(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.4.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
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
					command="UPDATE autonotecontrol SET ControlOptions='"+POut.PString(newVal)
						+"' WHERE AutoNoteControlNum="+table.Rows[i]["AutoNoteControlNum"].ToString();
					Db.NonQ(command);
				}
				command="UPDATE preference SET ValueString = '6.4.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_5_1();
		}

		private static void To6_5_1() {
			if(FromVersion<new Version("6.5.1.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('ShowFeatureMedicalInsurance','0','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingUseElectronic','0','Set to 1 to used e-billing.')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectVendorId','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectVendorPMSCode','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectCreditCardChoices','V,MC','Choices of V,MC,D,A comma delimited.')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectClientAcctNumber','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectUserName','','')";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingElectPassword','','')";
					Db.NonQ(command);
					command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES(12,22,'Status Condition','',-8978432,0)";
					Db.NonQ(command);
					command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES(22,16,'Condition','',-5169880,0)";
					Db.NonQ(command);
					command="INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES(22,17,'Condition (light)','',-1678747,0)";
					Db.NonQ(command);
					command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('BillingIgnoreInPerson','0','Set to 1 to ignore walkout statements.')";
					Db.NonQ(command);
					//eClinicalWorks Bridge---------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'eClinicalWorks', "
						+"'eClinicalWorks from www.eclinicalworks.com', "
						+"'0', "
						+"'', "
						+"'', "
						+"'')";
					int programNum=Db.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'HL7FolderIn', "
						+"'')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'HL7FolderOut', "
						+"'')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+POut.PInt(programNum)+"', "
						+"'DefaultUserGroup', "
						+"'')";
					Db.NonQ(command);
					command = "ALTER TABLE anesthmedsgiven ADD AnesthMedNum int NOT NULL";
					Db.NonQ(command);
					command = "ALTER TABLE provider ADD AnesthProvType int NOT NULL";
					Db.NonQ(command);
					command="DROP TABLE IF EXISTS hl7msg";
					Db.NonQ(command);
					command=@"CREATE TABLE hl7msg (
						HL7MsgNum int NOT NULL auto_increment,
						HL7Status int NOT NULL,
						MsgText text,
						AptNum int NOT NULL,
						PRIMARY KEY (HL7MsgNum),
						INDEX (AptNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				else {//oracle

				}
				command="UPDATE preference SET ValueString = '6.5.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
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
						programNum=PIn.PInt(table.Rows[0]["ProgramNum"].ToString());
						command="UPDATE program SET Path='"+POut.PString(@"C:\XDRClient\Bin\XDR.exe")+"' WHERE ProgramNum="+POut.PInt(programNum);
						Db.NonQ(command);
						command="UPDATE programproperty SET PropertyValue='"+POut.PString(@"C:\XDRClient\Bin\infofile.txt")+"' "
							+"WHERE ProgramNum="+POut.PInt(programNum)+" "
							+"AND PropertyDesc='InfoFile path'";
						Db.NonQ(command);
						command="UPDATE toolbutitem SET ToolBar=7 "//The toolbar at the top that is common to all modules.
							+"WHERE ProgramNum="+POut.PInt(programNum);
						Db.NonQ(command);
					}
					//iCat Bridge---------------------------------------------------------------------------
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'iCat', "
						+"'iCat from www.imagingsciences.com', "
						+"'0', "
						+"'"+POut.PString(@"C:\Program Files\ISIP\iCATVision\Vision.exe")+"', "
						+"'', "
						+"'')";
					programNum=Db.NonQ(command,true);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
						+"'0')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Acquisition computer name', "
						+"'')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'XML output file path', "
						+"'"+POut.PString(@"C:\iCat\Out\pm.xml")+"')";
					Db.NonQ(command);
					command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
						+") VALUES("
						+"'"+programNum.ToString()+"', "
						+"'Return folder path', "
						+"'"+POut.PString(@"C:\iCat\Return")+"')";
					Db.NonQ(command);
					command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
						+"VALUES ("
						+"'"+POut.PInt(programNum)+"', "
						+"'"+POut.PInt((int)ToolBarsAvail.ChartModule)+"', "
						+"'iCat')";
					Db.NonQ(command);
					//end of iCat Bridge
					string[] commands = new string[]{
						"ALTER TABLE anesthvsdata ADD MessageID varchar(50)",
						"ALTER TABLE anesthvsdata ADD HL7Message longtext"
					};
					Db.NonQ(commands);
					command="ALTER TABLE computer DROP PrinterName";
					Db.NonQ(command);
					command="ALTER TABLE computer ADD LastHeartBeat datetime NOT NULL default '0001-01-01'";
					Db.NonQ(command);
					command="ALTER TABLE registrationkey ADD UsesServerVersion tinyint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE registrationkey ADD IsFreeVersion tinyint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE registrationkey ADD IsOnlyForTesting tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle

				}				
				command="UPDATE preference SET ValueString = '6.6.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_6_2();
		}

		private static void To6_6_2() {
			if(FromVersion<new Version("6.6.2.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('WebServiceServerName','','Blank if not using web service.')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.6.2.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_6_3();
		}

		private static void To6_6_3() {
			if(FromVersion<new Version("6.6.3.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateInProgressOnComputerName','','Will be blank if update is complete.  If in the middle of an update, the named workstation is the only one allowed to startup OD.')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.6.3.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_6_8();
		}

		private static void To6_6_8() {
			if(FromVersion<new Version("6.6.8.0")) {
				string command;
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateMultipleDatabases','','Comma delimited')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.6.8.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_6_16();
		}

		private static void To6_6_16() {
			if(FromVersion<new Version("6.6.16.0")) {
				string command;
				command="SELECT ProgramNum FROM program WHERE ProgName='MediaDent'";
				int programNum=PIn.PInt(Db.GetScalar(command));
				command="DELETE FROM programproperty WHERE ProgramNum="+POut.PInt(programNum)
					+" AND PropertyDesc='Image Folder'";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.PInt(programNum)+"', "
					+"'"+POut.PString("Text file path")+"', "
					+"'"+POut.PString(@"C:\MediadentInfo.txt")+"')";
				Db.NonQ(command);
				command="UPDATE program SET Note='Text file path needs to be the same on all computers.' WHERE ProgramNum="+POut.PInt(programNum);
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.6.16.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_6_19();
		}

		private static void To6_6_19() {
			if(FromVersion<new Version("6.6.19.0")) {
				string command;
				command="UPDATE employee SET LName='O' WHERE LName='' AND FName=''";
				Db.NonQ(command);
				command="UPDATE schedule SET SchedType=1 WHERE ProvNum != 0 AND SchedType != 1";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.6.19.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_7_1();
		}

		private static void To6_7_1() {
			if(FromVersion<new Version("6.7.1.0")) {
				string command;
				command="ALTER TABLE document ADD DateTStamp TimeStamp";
				Db.NonQ(command);
				command="UPDATE document SET DateTStamp=NOW()";
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('StatementShowNotes','0','Payments and adjustments.')";
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('StatementShowProcBreakdown','0','')";
				Db.NonQ(command);
				command="ALTER TABLE etrans ADD EtransMessageTextNum INT NOT NULL";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS etransmessagetext";
				Db.NonQ(command);
				command=@"CREATE TABLE etransmessagetext (
					EtransMessageTextNum int NOT NULL auto_increment,
					MessageText text NOT NULL,
					PRIMARY KEY (EtransMessageTextNum),
					INDEX(MessageText(255))
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				command="ALTER TABLE etrans ADD INDEX(MessageText(255))";
				Db.NonQ(command);
				command="INSERT INTO etransmessagetext (MessageText) "
					+"SELECT DISTINCT MessageText FROM etrans "
					+"WHERE etrans.MessageText != ''";
				Db.NonQ(command);
				command="UPDATE etrans,etransmessagetext "
					+"SET etrans.EtransMessageTextNum=etransmessagetext.EtransMessageTextNum "
					+"WHERE etrans.MessageText=etransmessagetext.MessageText";
				Db.NonQ(command);
				command="ALTER TABLE etrans DROP MessageText";
				Db.NonQ(command);
				command="ALTER TABLE etransmessagetext DROP INDEX MessageText";
				Db.NonQ(command);
				command="ALTER TABLE etrans ADD AckEtransNum INT NOT NULL";
				Db.NonQ(command);
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
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateWebProxyAddress','','')";
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateWebProxyUserName','','')";
				Db.NonQ(command);
				command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('UpdateWebProxyPassword','','')";
				Db.NonQ(command);
				command="ALTER TABLE etrans ADD PlanNum INT NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE etrans ADD INDEX (PlanNum)";
				Db.NonQ(command);
				//Added new enum value of 0=None to CoverageLevel.
				command="UPDATE benefit SET CoverageLevel=CoverageLevel+1 WHERE BenefitType=2 OR BenefitType=5";//Deductible, Limitations
				Db.NonQ(command);
				command="ALTER TABLE benefit CHANGE Percent Percent tinyint NOT NULL";//So that we can store -1.
				Db.NonQ(command);
				command="UPDATE benefit SET Percent=-1 WHERE BenefitType != 1";//set Percent empty where not CoInsurance
				Db.NonQ(command);
				command="ALTER TABLE benefit DROP OldCode";
				Db.NonQ(command);
				//set MonetaryAmt empty when ActiveCoverage,CoInsurance,Exclusion
				command="UPDATE benefit SET MonetaryAmt=-1 WHERE BenefitType=0 OR BenefitType=1 OR BenefitType=4";
				Db.NonQ(command);
				//set MonetaryAmt empty when Limitation and a quantity is entered
				command="UPDATE benefit SET MonetaryAmt=-1 WHERE BenefitType=5 AND Quantity != 0";
				Db.NonQ(command);
				if(CultureInfo.CurrentCulture.Name=="en-US") {
					command="UPDATE covcat SET CovOrder=CovOrder+1 WHERE CovOrder > 1";
					Db.NonQ(command);
					command="INSERT INTO covcat (Description,DefaultPercent,CovOrder,IsHidden,EbenefitCat) VALUES('X-Ray',100,2,0,13)";
					int covCatNum=Db.NonQ(command,true);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.PInt(covCatNum)+",'D0200','D0399')";
					Db.NonQ(command);
					command="SELECT MAX(CovOrder) FROM covcat";
					int covOrder=PIn.PInt(Db.GetScalar(command));
					command="INSERT INTO covcat (Description,DefaultPercent,CovOrder,IsHidden,EbenefitCat) VALUES('Adjunctive',-1,"
						+POut.PInt(covOrder+1)+",0,14)";//adjunctive
					covCatNum=Db.NonQ(command,true);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.PInt(covCatNum)+",'D9000','D9999')";
					Db.NonQ(command);
					command="SELECT CovCatNum FROM covcat WHERE EbenefitCat=1";//general
					covCatNum=Db.NonQ(command,true);
					command="DELETE FROM covspan WHERE CovCatNum="+POut.PInt(covCatNum);
					Db.NonQ(command);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.PInt(covCatNum)+",'D0000','D7999')";
					Db.NonQ(command);
					command="INSERT INTO covspan (CovCatNum,FromCode,ToCode) VALUES("+POut.PInt(covCatNum)+",'D9000','D9999')";
					Db.NonQ(command);
				}
				command="ALTER TABLE claimproc ADD DedEst double NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD DedEstOverride double NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD InsEstTotal double NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD InsEstTotalOverride double NOT NULL";
				Db.NonQ(command);
				command="UPDATE claimproc SET DedApplied=-1 WHERE ClaimNum=0";//if not attached to a claim, clear this value
				Db.NonQ(command);
				command="UPDATE claimproc SET DedEstOverride=-1";
				Db.NonQ(command);
				command="UPDATE claimproc SET InsEstTotal=BaseEst";
				Db.NonQ(command);
				command="UPDATE claimproc SET InsEstTotalOverride=OverrideInsEst";
				Db.NonQ(command);
				command="ALTER TABLE claimproc DROP OverrideInsEst";
				Db.NonQ(command);
				command="ALTER TABLE claimproc DROP DedBeforePerc";
				Db.NonQ(command);
				command="ALTER TABLE claimproc DROP OverAnnualMax";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD PaidOtherInsOverride double NOT NULL";
				Db.NonQ(command);
				command="UPDATE claimproc SET PaidOtherInsOverride=PaidOtherIns";
				Db.NonQ(command);
				command="ALTER TABLE claimproc ADD EstimateNote varchar(255) NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE insplan ADD MonthRenew tinyint NOT NULL";
				Db.NonQ(command);
				command="SELECT insplan.PlanNum,MONTH(DateEffective) "
					+"FROM insplan,benefit "
					+"WHERE insplan.PlanNum=benefit.PlanNum "
					+"AND benefit.TimePeriod=1 "
					+"GROUP BY insplan.PlanNum";//service year
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE insplan SET MonthRenew="+table.Rows[i][1].ToString()
						+" WHERE PlanNum="+table.Rows[i][0].ToString();
					Db.NonQ(command);
				}
				command="ALTER TABLE appointment ADD InsPlan1 INT NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE appointment ADD INDEX (InsPlan1)";
				Db.NonQ(command);
				command="ALTER TABLE appointment ADD InsPlan2 INT NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE appointment ADD INDEX (InsPlan2)";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS insfilingcode";
				Db.NonQ(command);
				command=@"CREATE TABLE insfilingcode (
					InsFilingCodeNum INT AUTO_INCREMENT,
					Descript VARCHAR(255),
					EclaimCode VARCHAR(100),
					ItemOrder INT,
					PRIMARY KEY(InsFilingCodeNum),
					INDEX(ItemOrder)
					)";
				Db.NonQ(command);
				command="DROP TABLE IF EXISTS insfilingcodesubtype";
				Db.NonQ(command);
				command=@"CREATE TABLE insfilingcodesubtype (
					InsFilingCodeSubtypeNum INT AUTO_INCREMENT,
					InsFilingCodeNum INT,
					Descript VARCHAR(255),
					INDEX(InsFilingCodeNum),
					PRIMARY KEY(InsFilingCodeSubtypeNum)
					)";
				Db.NonQ(command);
				command="ALTER TABLE insplan ADD FilingCodeSubtype INT NOT NULL";
				Db.NonQ(command);
				command="ALTER TABLE insplan ADD INDEX (FilingCodeSubtype)";
				Db.NonQ(command);
				//eCW bridge enhancements
				command="SELECT ProgramNum FROM program WHERE ProgName='eClinicalWorks'";
				int programNum=PIn.PInt(Db.GetScalar(command));
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.PInt(programNum)+"', "
					+"'ShowImagesModule', "
					+"'0')";
				Db.NonQ(command);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+POut.PInt(programNum)+"', "
					+"'IsStandalone', "
					+"'0')";//starts out as false
				Db.NonQ(command);
				command="UPDATE insplan SET FilingCode = FilingCode+1";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(1,'Commercial_Insurance','CI',0)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(2,'SelfPay','09',1)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(3,'OtherNonFed','11',2)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(4,'PPO','12',3)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(5,'POS','13',4)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(6,'EPO','14',5)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(7,'Indemnity','15',6)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(8,'HMO_MedicareRisk','16',7)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(9,'DMO','17',8)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(10,'BCBS','BL',9)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(11,'Champus','CH',10)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(12,'Disability','DS',11)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(13,'FEP','FI',12)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(14,'HMO','HM',13)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(15,'LiabilityMedical','LM',14)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(16,'MedicarePartB','MB',15)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(17,'Medicaid','MC',16)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(18,'ManagedCare_NonHMO','MH',17)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(19,'OtherFederalProgram','OF',18)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(20,'SelfAdministered','SA',19)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(21,'Veterans','VA',20)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(22,'WorkersComp','WC',21)";
				Db.NonQ(command);
				command="INSERT INTO insfilingcode VALUES(23,'MutuallyDefined','ZZ',22)";
				Db.NonQ(command);
				//Fixes bug here instead of db maint
				//Duplicated in version 6.6
				command="UPDATE employee SET LName='O' WHERE LName='' AND FName=''";
				Db.NonQ(command);
				command="UPDATE schedule SET SchedType=1 WHERE ProvNum != 0 AND SchedType != 1";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.7.1.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_7_3();
		}

		private static void To6_7_3() {
			if(FromVersion<new Version("6.7.3.0")) {
				string command=@"UPDATE claimform,claimformitem SET claimformitem.FieldName='IsGroupHealthPlan'
					WHERE claimformitem.FieldName='IsStandardClaim' AND claimform.ClaimFormNum=claimformitem.ClaimFormNum
					AND claimform.UniqueID='OD9'";//1500
				Db.NonQ(command);
				command=@"UPDATE claimform,claimformitem SET claimformitem.XPos='97'
					WHERE claimformitem.XPos='30' AND claimform.ClaimFormNum=claimformitem.ClaimFormNum
					AND claimform.UniqueID='OD9'";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.7.3.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_7_4();
		}

		private static void To6_7_4() {
			if(FromVersion<new Version("6.7.4.0")) {
				string command="DELETE FROM medicationpat WHERE EXISTS(SELECT * FROM patient WHERE medicationpat.PatNum=patient.PatNum AND patient.PatStatus=4)";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.7.4.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_7_5();
		}

		private static void To6_7_5() {
			if(FromVersion<new Version("6.7.5.0")) {
				string command="INSERT INTO preference (PrefName,ValueString,Comments) VALUES ('ClaimsValidateACN','0','If set to 1, then any claim with a groupName containing ADDP will require an ACN number in the claim remarks.')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.7.5.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
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
					+"'"+POut.PString(@"C:\cdm\cdm\cdmx\cdmx.exe")+"', "
					+"'', "
					+"'')";
				int programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+POut.PInt(programNum)+"', "
					+"'"+POut.PInt((int)ToolBarsAvail.ChartModule)+"', "
					+"'Camsight')";
				Db.NonQ(command);
				//CliniView Bridge---------------------------------------------------------------------------
				command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
					+") VALUES("
					+"'CliniView', "
					+"'CliniView', "
					+"'0', "
					+"'"+POut.PString(@"C:\Program Files\CliniView\CliniView.exe")+"', "
					+"'', "
					+"'')";
				programNum=Db.NonQ(command,true);
				command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
					+") VALUES("
					+"'"+programNum.ToString()+"', "
					+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
					+"'0')";
				Db.NonQ(command);
				command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
					+"VALUES ("
					+"'"+POut.PInt(programNum)+"', "
					+"'"+POut.PInt((int)ToolBarsAvail.ChartModule)+"', "
					+"'CliniView')";
				Db.NonQ(command);
				command="UPDATE preference SET ValueString = '6.7.12.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			To6_8_0();
		}

		private static void To6_8_0() {
			if(FromVersion<new Version("6.8.0.0")) {
				string command;









				
				command="UPDATE preference SET ValueString = '6.8.0.0' WHERE PrefName = 'DataBaseVersion'";
				Db.NonQ(command);
			}
			//To6_7_1();
		}

	}
}
