using System;
using System.Collections;
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
		private System.Version LatestVersion=new Version("6.0.0.0");//This value must be changed when a new conversion is to be triggered.
		
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
			To6_0_0();
		}

		private void To6_0_0() {
			if(FromVersion<new Version("6.0.0.0")) {
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










				} 
				else {//oracle
					
				}
				command="UPDATE preference SET ValueString = '6.0.0.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			//To6_0_?();
		}

		/*For 6.0:
		 * ALTER TABLE schedule ADD INDEX (EmployeeNum)
ALTER TABLE schedule ADD INDEX (ProvNum)
ALTER TABLE schedule ADD INDEX (SchedDate)*/


	}

}