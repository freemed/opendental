using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;
using CodeBase;

namespace OpenDentBusiness {
	public class DatabaseMaintenance {
		private static DataTable table;
		private static string command;
		private static bool success=false;

		public static bool GetSuccess() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod());
			}
			return DatabaseMaintenance.success; 
		}

		public static string MySQLTables(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			success=true;
			if(DataConnection.DBtype!=DatabaseType.MySql) {
				return "";
			}
			command="DROP TABLE IF EXISTS `signal`";//Signal is keyword for MySQL 5.5.  Was renamed to signalod so drop if exists.
			Db.NonQ(command);
			command="SHOW TABLES";
			table=Db.GetTable(command);
			string[] tableNames=new string[table.Rows.Count];
			int lastRow;
			bool allOK=true;
			for(int i=0;i<table.Rows.Count;i++) {
				tableNames[i]=table.Rows[i][0].ToString();
			}
			for(int i=0;i<tableNames.Length;i++) {
				command="CHECK TABLE "+tableNames[i];
				table=Db.GetTable(command);
				lastRow=table.Rows.Count-1;
				string status=PIn.ByteArray(table.Rows[lastRow][3]);
				if(status!="OK") {
					log+=Lans.g("FormDatabaseMaintenance","Corrupt file found for table")+" "+tableNames[i]+"\r\n";
					allOK=false;
					//Sometimes dangerous because it can remove table rows (it has happened a few times). Repair of tables is necessary frequently though, so left the code here.
					if(!isCheck){
						command="REPAIR TABLE "+tableNames[i];
						DataTable tableResults=Db.GetTable(command);
						//we always log the results of a repair table, regardless of whether user wants to show all.
						log+=Lans.g("FormDatabaseMaintenance","Repair log:")+"\r\n";
						for(int t=0;t<tableResults.Rows.Count;t++) {
							for(int j=0;j<tableResults.Columns.Count;j++) {
								log+=PIn.ByteArray(tableResults.Rows[t][j])+",";
							}
							log+="\r\n";
						}
					}
				}
			}
			if(allOK) {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","No corrupted tables.")+"\r\n";
				}
			}
			else {
				success=false;//no other checks should be done until we can successfully get past this.
				if(!isCheck) {
					log+=Lans.g("FormDatabaseMaintenance","Corrupted files probably fixed.  Look closely at the log.  Also, run again to be sure they were really fixed.")+"\r\n"
						+Lans.g("FormDatabaseMaintenance","Done.");
				}
			}
			return log;
		}

		///<summary>This is currently called whenever mysql is upgraded.  There's also a button in dbm window.  Needs to be made more elegant.</summary>
		public static void RepairAndOptimize() {
			if(DataConnection.DBtype!=DatabaseType.MySql) {
				return;
			}
			command="SHOW TABLES";
			table=Db.GetTable(command);
			string[] tableNames=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				tableNames[i]=table.Rows[i][0].ToString();
			}
			for(int i=0;i<tableNames.Length;i++) {
				command="OPTIMIZE TABLE `"+tableNames[i]+"`";
				Db.NonQ(command);
			}
			for(int i=0;i<tableNames.Length;i++) {
				command="REPAIR TABLE `"+tableNames[i]+"`";
				Db.NonQ(command);
			}
		}

		public static string DatesNoZeros(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";//This check is not valid for Oracle, because each of the following fields are defined as non-null,
				//and 0000-00-00 is not a valid Oracle date.
			}
			if(isCheck){
				string[] commands=new string[]
				{
				  "SELECT COUNT(*) FROM adjustment WHERE AdjDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM adjustment WHERE DateEntry<'1980'"
				  ,"SELECT COUNT(*) FROM adjustment WHERE ProcDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM appointment WHERE AptDateTime LIKE '0000-00-00%'"
				  ,"SELECT COUNT(*) FROM appointment WHERE DateTimeArrived LIKE '0000-00-00%'"
					,"SELECT COUNT(*) FROM appointment WHERE DateTimeAskedToArrive LIKE '0000-00-00%'"
				  ,"SELECT COUNT(*) FROM appointment WHERE DateTimeSeated LIKE '0000-00-00%'"
				  ,"SELECT COUNT(*) FROM appointment WHERE DateTimeDismissed LIKE '0000-00-00%'"
				  ,"SELECT COUNT(*) FROM appointment WHERE DateTStamp='0000-00-00 00:00:00'"
				  ,"SELECT COUNT(*) FROM claim WHERE DateService='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claim WHERE DateSent='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claim WHERE DateReceived='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claim WHERE PriorDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claim WHERE AccidentDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claim WHERE OrthoDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claimpayment WHERE CheckDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claimproc WHERE DateCP='0000-00-00'"
				  ,"SELECT COUNT(*) FROM claimproc WHERE ProcDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM inssub WHERE DateEffective='0000-00-00'"
				  ,"SELECT COUNT(*) FROM inssub WHERE DateTerm='0000-00-00'"
				  ,"SELECT COUNT(*) FROM patient WHERE Birthdate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM patient WHERE DateFirstVisit='0000-00-00'"
				  ,"SELECT COUNT(*) FROM procedurelog WHERE ProcDate LIKE '0000-00-00%'"
				  ,"SELECT COUNT(*) FROM procedurelog WHERE DateOriginalProsth='0000-00-00'"
				  ,"SELECT COUNT(*) FROM procedurelog WHERE DateEntryC='0000-00-00'"
				  ,"SELECT COUNT(*) FROM procedurelog WHERE DateTP='0000-00-00'"
				  ,"SELECT COUNT(*) FROM recall WHERE DateDueCalc='0000-00-00'"
				  ,"SELECT COUNT(*) FROM recall WHERE DateDue='0000-00-00'"
				  ,"SELECT COUNT(*) FROM recall WHERE DatePrevious='0000-00-00'"
					,"SELECT COUNT(*) FROM recall WHERE DateScheduled='0000-00-00'"
				  ,"SELECT COUNT(*) FROM recall WHERE DisableUntilDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM schedule WHERE SchedDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM signalod WHERE DateViewing='0000-00-00'"
				  ,"SELECT COUNT(*) FROM signalod WHERE SigDateTime LIKE '0000-00-00%'"
				  ,"SELECT COUNT(*) FROM signalod WHERE AckTime LIKE '0000-00-00%'"
				};
				int numInvalidDates=0;
				for(int i=0;i<commands.Length;i++) {
					numInvalidDates+=PIn.Int(Db.GetCount(commands[i]));
				}
				if(numInvalidDates>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Dates invalid:")+" "+numInvalidDates+"\r\n";
				}
			}
			else{
				string[] commands=new string[]
				{
				  "UPDATE adjustment SET AdjDate='0001-01-01' WHERE AdjDate='0000-00-00'"
				  ,"UPDATE adjustment SET DateEntry='1980-01-01' WHERE DateEntry<'1980'"
				  ,"UPDATE adjustment SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
				  ,"UPDATE appointment SET AptDateTime='0001-01-01 00:00:00' WHERE AptDateTime LIKE '0000-00-00%'"
				  ,"UPDATE appointment SET DateTimeArrived='0001-01-01 00:00:00' WHERE DateTimeArrived LIKE '0000-00-00%'"
					,"UPDATE appointment SET DateTimeAskedToArrive='0001-01-01 00:00:00' WHERE DateTimeAskedToArrive LIKE '0000-00-00%'"
				  ,"UPDATE appointment SET DateTimeSeated='0001-01-01 00:00:00' WHERE DateTimeSeated LIKE '0000-00-00%'"
				  ,"UPDATE appointment SET DateTimeDismissed='0001-01-01 00:00:00' WHERE DateTimeDismissed LIKE '0000-00-00%'"
				  ,"UPDATE appointment SET DateTStamp='2009-08-24 00:00:00' WHERE DateTStamp='0000-00-00 00:00:00'"
				  ,"UPDATE claim SET DateService='0001-01-01' WHERE DateService='0000-00-00'"
				  ,"UPDATE claim SET DateSent='0001-01-01' WHERE DateSent='0000-00-00'"
				  ,"UPDATE claim SET DateReceived='0001-01-01' WHERE DateReceived='0000-00-00'"
				  ,"UPDATE claim SET PriorDate='0001-01-01' WHERE PriorDate='0000-00-00'"
				  ,"UPDATE claim SET AccidentDate='0001-01-01' WHERE AccidentDate='0000-00-00'"
				  ,"UPDATE claim SET OrthoDate='0001-01-01' WHERE OrthoDate='0000-00-00'"
				  ,"UPDATE claimpayment SET CheckDate='0001-01-01' WHERE CheckDate='0000-00-00'"
				  ,"UPDATE claimproc SET DateCP='0001-01-01' WHERE DateCP='0000-00-00'"
				  ,"UPDATE claimproc SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
				  ,"UPDATE inssub SET DateEffective='0001-01-01' WHERE DateEffective='0000-00-00'"
				  ,"UPDATE inssub SET DateTerm='0001-01-01' WHERE DateTerm='0000-00-00'"
				  ,"UPDATE patient SET Birthdate='0001-01-01' WHERE Birthdate='0000-00-00'"
				  ,"UPDATE patient SET DateFirstVisit='0001-01-01' WHERE DateFirstVisit='0000-00-00'"
				  ,"UPDATE procedurelog SET ProcDate='0001-01-01 00:00:00' WHERE ProcDate LIKE '0000-00-00%'"
				  ,"UPDATE procedurelog SET DateOriginalProsth='0001-01-01' WHERE DateOriginalProsth='0000-00-00'"
				  ,"UPDATE procedurelog SET DateEntryC='0001-01-01' WHERE DateEntryC='0000-00-00'"
				  ,"UPDATE procedurelog SET DateTP='0001-01-01' WHERE DateTP='0000-00-00'"
				  ,"UPDATE recall SET DateDueCalc='0001-01-01' WHERE DateDueCalc='0000-00-00'"
				  ,"UPDATE recall SET DateDue='0001-01-01' WHERE DateDue='0000-00-00'"
				  ,"UPDATE recall SET DatePrevious='0001-01-01' WHERE DatePrevious='0000-00-00'"
					,"UPDATE recall SET DateScheduled='0001-01-01' WHERE DateScheduled='0000-00-00'"
				  ,"UPDATE recall SET DisableUntilDate='0001-01-01' WHERE DisableUntilDate='0000-00-00'"
				  ,"UPDATE schedule SET SchedDate='0001-01-01' WHERE SchedDate='0000-00-00'"
				  ,"UPDATE signalod SET DateViewing='0001-01-01' WHERE DateViewing='0000-00-00'"
				  ,"UPDATE signalod SET SigDateTime='0001-01-01 00:00:00' WHERE SigDateTime LIKE '0000-00-00%'"
				  ,"UPDATE signalod SET AckTime='0001-01-01 00:00:00' WHERE AckTime LIKE '0000-00-00%'"
				};
				long rowsChanged=0;
				for(int i=0;i<commands.Length;i++) {
					rowsChanged+=Db.NonQ(commands[i]);
				}
				if(rowsChanged !=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Dates fixed. Rows changed:")+" "+rowsChanged.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string DecimalValues(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			//This specific fix is no longer needed, since we are using ROUND(EstBalance,2) in the aging calculation now.
			//However, it is still a problem in many columns that we will eventually need to deal with.
			//Maybe add this back when users can control which fixes they make.
			//One problem is the foreign users do not necessarily use 2 decimal places (Kuwait uses 3).
			////Holds columns to be checked. Strings are in pairs in the following order: table-name,column-name
			//string[] decimalCols=new string[] {
			//  "patient","EstBalance"
			//};
			//int decimalPlacessToRoundTo=8;
			//long numberFixed=0;
			//for(int i=0;i<decimalCols.Length;i+=2) {
			//  string tablename=decimalCols[i];
			//  string colname=decimalCols[i+1];
			//  string command="UPDATE "+tablename+" SET "+colname+"=ROUND("+colname+","+decimalPlacessToRoundTo
			//    +") WHERE "+colname+"!=ROUND("+colname+","+decimalPlacessToRoundTo+")";
			//  numberFixed+=Db.NonQ(command);
			//}
			//if(numberFixed>0 || verbose) {
			//  log+=Lans.g("FormDatabaseMaintenance","Decimal values fixed: ")+numberFixed.ToString()+"\r\n";
			//}
			return log;
		}

		//Methods that apply to specific tables----------------------------------------------------------------------------------

		public static string AppointmentsNoPattern(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command=@"SELECT AptNum FROM appointment WHERE Pattern=''";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Appointments found with zero length: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				if(table.Rows.Count>0) {
					//detach all procedures
					if(DataConnection.DBtype==DatabaseType.Oracle) {
						command="UPDATE procedurelog P SET P.AptNum = 0 WHERE (SELECT A.Pattern FROM appointment A WHERE A.AptNum=P.AptNum AND ROWNUM<=1)=''";
					}
					else {
						command="UPDATE procedurelog P, appointment A SET P.AptNum = 0 WHERE P.AptNum = A.AptNum AND A.Pattern = ''";
					}
					Db.NonQ(command);
					if(DataConnection.DBtype==DatabaseType.Oracle) {
						command="UPDATE procedurelog P SET P.PlannedAptNum = 0 WHERE (SELECT A.Pattern FROM appointment A WHERE A.AptNum=P.PlannedAptNum AND ROWNUM<=1)=''";
					}
					else {
						command="UPDATE procedurelog P, appointment A SET P.PlannedAptNum = 0 WHERE P.PlannedAptNum = A.AptNum AND A.Pattern = ''";
					}
					Db.NonQ(command);
					command="DELETE FROM appointment WHERE Pattern = ''";
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Appointments deleted with zero length: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string AppointmentsNoDateOrProcs(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM appointment "
				  +"WHERE AptStatus=1 "//scheduled 
				  +"AND "+DbHelper.Year("AptDateTime")+"<1880 "//scheduled but no date 
				  +"AND NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.AptNum=appointment.AptNum)";//and no procs
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound!=0 || verbose) {
				  log+=Lans.g("FormDatabaseMaintenance","Appointments found with no date and no procs: ")+numFound+"\r\n";
				}
			}
			else{
				command="DELETE FROM appointment "
				  +"WHERE AptStatus=1 "//scheduled 
				  +"AND "+DbHelper.Year("AptDateTime")+"<1880 "//scheduled but no date 
				  +"AND NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.AptNum=appointment.AptNum)";//and no procs
				long numberFixed=Db.NonQ(command);
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Appointments deleted due to no date and no procs: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string AppointmentsNoPatients(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT Count(*) FROM appointment WHERE PatNum NOT IN(SELECT PatNum FROM patient)";
			int count=PIn.Int(Db.GetCount(command));
			if(isCheck){
				if(count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Appointments found abandoned: ")+count.ToString()+"\r\n";
				}
			}
			else{//Fix is safe because we are not deleting data, we are just attaching abandoned appointments to a dummy patient.
				long numfixed=0;
				if(count!=0) {
					Patient dummyPatient=new Patient();
					dummyPatient.FName="MISSING";
					dummyPatient.LName="PATIENT";
					dummyPatient.AddrNote="Appointments with missing patients were assigned to this patient on "+DateTime.Now.ToShortDateString()+" while doing database maintenance.";
					dummyPatient.Birthdate=DateTime.MinValue;
					dummyPatient.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
					dummyPatient.PatStatus=PatientStatus.Archived;
					dummyPatient.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
					long dummyPatNum=Patients.Insert(dummyPatient,false);
					Patient oldDummyPatient=dummyPatient.Copy();
					dummyPatient.Guarantor=dummyPatNum;
					Patients.Update(dummyPatient,oldDummyPatient);
					command="UPDATE appointment SET PatNum="+POut.Long(dummyPatNum)+" WHERE PatNum NOT IN(SELECT PatNum FROM patient)";
					numfixed=Db.NonQ(command);
				}
				if(numfixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Appointments altered due to no patient: ")+count.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string AppointmentPlannedNoPlannedApt(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM appointment WHERE AptStatus=6 AND AptNum NOT IN (SELECT AptNum FROM plannedappt)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Appointments with status set to planned without Planned Appointment: ")+numFound.ToString()+"\r\n";
				}
			}
			else {
				command="SELECT * FROM appointment WHERE AptStatus=6 AND AptNum NOT IN (SELECT AptNum FROM plannedappt)";
				DataTable appts=Db.GetTable(command);
				if(appts.Rows.Count > 0 || verbose){
					PlannedAppt plannedAppt;
					for(int i=0;i<appts.Rows.Count;i++) {
						plannedAppt=new PlannedAppt();
						plannedAppt.PatNum=PIn.Long(appts.Rows[i]["PatNum"].ToString());
						plannedAppt.AptNum=PIn.Long(appts.Rows[i]["AptNum"].ToString());
						plannedAppt.ItemOrder=1;
						PlannedAppts.Insert(plannedAppt);
					}
					log+=Lans.g("FormDatabaseMaintenance","Planned Appointments created for Appointments with status set to planned and no Planned Appointment: ")+appts.Rows.Count+"\r\n";
				}
			}
			return log;
		}

		public static string AppointmentSpecialCharactersInNotes(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT * FROM appointment WHERE (ProcDescript REGEXP '[^[:alnum:]^[:space:]^[:punct:]]+') OR (Note REGEXP '[^[:alnum:]^[:space:]^[:punct:]]+')";
				List<Appointment> apts=Crud.AppointmentCrud.SelectMany(command);
				List<char> specialCharsFound=new List<char>();
				int specialCharCount=0;
				int intC=0;
				foreach(Appointment apt in apts) {
					foreach(char c in apt.Note) {
						intC=(int)c;
						if((intC<126&&intC>31)//31 - 126 are all safe.
							||intC==9			//"Horizontal Tabulation"
							||intC==10		//Line Feed
							||intC==13) {	//carriage return
							continue;
						}
						specialCharCount++;
						if(specialCharsFound.Contains(c)) {
							continue;
						}
						specialCharsFound.Add(c);
					}
					foreach(char c in apt.ProcDescript) {//search every character in ProcDescript
						intC=(int)c;
						if((intC<126&&intC>31)//31 - 126 are all safe.
							||intC==9			//"Horizontal Tabulation"
							||intC==10		//Line Feed
							||intC==13) {	//carriage return
							continue;
						}
						specialCharCount++;
						if(specialCharsFound.Contains(c)) {
							continue;
						}
						specialCharsFound.Add(c);
					}
				}
				foreach(char c in specialCharsFound) {
					log+=c.ToString()+" doesn't work.\r\n";
				}
				if(specialCharCount!=0||verbose) {
					log+=specialCharCount.ToString()+" "+Lans.g("FormDatabaseMaintenance","Total special characters found.  These will cause mobile synch to fail.  If your mobile synch is failing, use the Spec Char button below to fix.")+"\r\n";
				}
			}
			else {
				//Fix code is in a dedicated button "Spec Char"
			}
			return log;
		}

		public static string AutoCodesDeleteWithNoItems(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM autocode WHERE NOT EXISTS(
					SELECT * FROM autocodeitem WHERE autocodeitem.AutoCodeNum=autocode.AutoCodeNum)";
        int numFound=PIn.Int(Db.GetCount(command));
        if(numFound!=0 || verbose) {
          log+=Lans.g("FormDatabaseMaintenance","Autocodes found with no items: ")+numFound+"\r\n";
        }
			}
			else{
				command=@"DELETE FROM autocode WHERE NOT EXISTS(
					SELECT * FROM autocodeitem WHERE autocodeitem.AutoCodeNum=autocode.AutoCodeNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0) {
					Signalods.SetInvalid(InvalidType.AutoCodes);
				}
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Autocodes deleted due to no items: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string AutomationTriggersWithNoSheetDefs(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM automation WHERE automation.SheetDefNum!=0 AND NOT EXISTS(
					SELECT SheetDefNum FROM sheetdef WHERE automation.SheetDefNum=sheetdef.SheetDefNum)";
        int numFound=PIn.Int(Db.GetCount(command));
        if(numFound!=0 || verbose) {
          log+=Lans.g("FormDatabaseMaintenance","Automation triggers found with no sheet defs: ")+numFound+"\r\n";
        }
			}
			else{
				command=@"DELETE FROM automation WHERE automation.SheetDefNum!=0 AND NOT EXISTS(
					SELECT SheetDefNum FROM sheetdef WHERE automation.SheetDefNum=sheetdef.SheetDefNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Automation triggers deleted due to no sheet defs: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string BillingTypesInvalid(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				//Make sure preference of default billing type is set.
				command="SELECT ValueString FROM preference WHERE PrefName = 'PracticeDefaultBillType'";
				long billingType=PIn.Long(Db.GetScalar(command));
				command="SELECT COUNT(*) FROM definition WHERE Category=4 AND definition.DefNum="+billingType;
				int prefExists=PIn.Int(Db.GetCount(command));
				if(prefExists!=1) {
					log+=Lans.g("FormDatabaseMaintenance","No default billing type set.")+"\r\n";
				}
				else{
					if(verbose) {
						log+=Lans.g("FormDatabaseMaintenance","Default practice billing type verified.")+"\r\n";
					}
				}
				//Check for any patients with invalid billingtype.
				command="SELECT COUNT(*) FROM patient WHERE NOT EXISTS(SELECT * FROM definition WHERE Category=4 AND patient.BillingType=definition.DefNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients with invalid billing type: ")+numFound.ToString()+"\r\n";
				}
			}
			else {
				//Fix for default billingtype not being set.
				command="SELECT ValueString FROM preference WHERE PrefName = 'PracticeDefaultBillType'";
				long billingType=PIn.Long(Db.GetScalar(command));
				command="SELECT COUNT(*) FROM definition WHERE Category=4 AND definition.DefNum="+billingType;
				int prefExists=PIn.Int(Db.GetCount(command));
				if(prefExists!=1) {//invalid billing type
					command="SELECT DefNum FROM definition WHERE Category = 4 AND IsHidden = 0 ORDER BY ItemOrder";
					table=Db.GetTable(command);
					if(table.Rows.Count==0) {//if all billing types are hidden
						command="SELECT DefNum FROM definition WHERE Category = 4 ORDER BY ItemOrder";
						table=Db.GetTable(command);
					}
					command="UPDATE preference SET ValueString='"+table.Rows[0][0].ToString()+"' WHERE PrefName='PracticeDefaultBillType'";
					Db.NonQ(command);
					log+=Lans.g("FormDatabaseMaintenance","Default billing type preference was set due to being invalid.")+"\r\n";
					Prefs.RefreshCache();//for the next line.
				}
				//Fix for patients with invalid billingtype.
				command="UPDATE patient SET patient.BillingType="+POut.Long(PrefC.GetLong(PrefName.PracticeDefaultBillType));
				command+=" WHERE NOT EXISTS(SELECT * FROM definition WHERE Category=4 AND patient.BillingType=definition.DefNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients billing type set to default due to being invalid: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string CanadaCarriersCdaMissingInfo(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(!CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				return "";
			}
			command="SELECT CanadianNetworkNum FROM canadiannetwork WHERE Abbrev='TELUS B' LIMIT 1";
			long canadianNetworkNumTelusB=PIn.Long(Db.GetScalar(command));
			command="SELECT CanadianNetworkNum FROM canadiannetwork WHERE Abbrev='CSI' LIMIT 1";
			long canadianNetworkNumCSI=PIn.Long(Db.GetScalar(command));
			command="SELECT CanadianNetworkNum FROM canadiannetwork WHERE Abbrev='CDCS' LIMIT 1";
			long canadianNetworkNumCDCS=PIn.Long(Db.GetScalar(command));
			command="SELECT CanadianNetworkNum FROM canadiannetwork WHERE Abbrev='TELUS A' LIMIT 1";
			long canadianNetworkNumTelusA=PIn.Long(Db.GetScalar(command));
			command="SELECT CanadianNetworkNum FROM canadiannetwork WHERE Abbrev='MBC' LIMIT 1";
			long canadianNetworkNumMBC=PIn.Long(Db.GetScalar(command));
			command="SELECT CanadianNetworkNum FROM canadiannetwork WHERE Abbrev='PBC' LIMIT 1";
			long canadianNetworkNumPBC=PIn.Long(Db.GetScalar(command));
			command="SELECT CanadianNetworkNum FROM canadiannetwork WHERE Abbrev='ABC' LIMIT 1";
			long canadianNetworkNumABC=PIn.Long(Db.GetScalar(command));
			CanSupTransTypes claimTypes=CanSupTransTypes.ClaimAckEmbedded_11e|CanSupTransTypes.ClaimEobEmbedded_21e;//Claim 01, claim ack 11, and claim eob 21 are implied.
			CanSupTransTypes reversalTypes=CanSupTransTypes.ClaimReversal_02|CanSupTransTypes.ClaimReversalResponse_12;
			CanSupTransTypes predeterminationTypes=CanSupTransTypes.PredeterminationAck_13|CanSupTransTypes.PredeterminationAckEmbedded_13e|CanSupTransTypes.PredeterminationMultiPage_03|CanSupTransTypes.PredeterminationSinglePage_03;
			CanSupTransTypes rotTypes=CanSupTransTypes.RequestForOutstandingTrans_04;
			CanSupTransTypes cobTypes=CanSupTransTypes.CobClaimTransaction_07;
			CanSupTransTypes eligibilityTypes=CanSupTransTypes.EligibilityTransaction_08|CanSupTransTypes.EligibilityResponse_18;
			CanSupTransTypes rprTypes=CanSupTransTypes.RequestForPaymentReconciliation_06;
			//Column order: ElectID,CanadianEncryptionMethod,CDAnetVersion,CanadianSupportedTypes,CanadianNetworkNum
			object[] carrierInfo=new object[] {
				//accerta
				"311140",1,"04",claimTypes|reversalTypes,canadianNetworkNumTelusB,
				//adsc
				"000105",1,"04",claimTypes|reversalTypes|predeterminationTypes|rotTypes|cobTypes|eligibilityTypes,canadianNetworkNumCSI,
				//aga
				"610226",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//appq
				"628112",1,"02",claimTypes|reversalTypes|predeterminationTypes|cobTypes,canadianNetworkNumTelusB,
				//alberta blue cross. Usually sent through ClaimStream instead of ITRANS.
				"000090",1,"04",claimTypes|reversalTypes|predeterminationTypes|rotTypes|cobTypes,canadianNetworkNumABC,
				//assumption life
				"610191",1,"04",claimTypes,canadianNetworkNumTelusB,
				//autoben
				"628151",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//benecaid health benefit solutions
				"610708",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumCSI,
				//benefits trust
				"610146",1,"02",claimTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//beneplan
				"400008",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//boilermakers' national benefit plan
				"000116",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumCSI,
				//canadian benefit providers
				"610202",1,"04",claimTypes|reversalTypes|predeterminationTypes|cobTypes,canadianNetworkNumTelusB,
				//capitale
				"600502",1,"04",claimTypes,canadianNetworkNumTelusB,
				//cdcs
				"610129",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumCDCS,
				//claimsecure
				"610099",1,"04",claimTypes|eligibilityTypes,canadianNetworkNumTelusB,
				//ccq
				"000036",1,"02",claimTypes|reversalTypes,canadianNetworkNumTelusB,
				//co-operators
				"606258",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumCSI,
				//coughlin & associates
				"610105",1,"04",claimTypes|reversalTypes|predeterminationTypes|rotTypes,canadianNetworkNumTelusB,
				//cowan wright beauchamps
				"610153",1,"04",claimTypes|reversalTypes,canadianNetworkNumCSI,
				//desjardins financial security
				"000051",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumCSI,
				//empire life insurance company
				"000033",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//equitable life
				"000029",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusA,
				//esorse corporation
				"610650",1,"04",claimTypes|reversalTypes|predeterminationTypes|rprTypes|cobTypes,canadianNetworkNumTelusB,
				//fas administrators
				"610614",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//great west life assurance company
				"000011",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusA,
				//green sheild canada
				"000102",1,"04",claimTypes|reversalTypes|predeterminationTypes|cobTypes,canadianNetworkNumTelusB,
				//group medical services
				"610217",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumCSI,
				//group medical services saskatchewan
				"610218",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumCSI,
				//groupsource
				"605064",1,"04",claimTypes|reversalTypes|eligibilityTypes,canadianNetworkNumCSI,
				//industrial alliance
				"000060",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusA,
				//industrial alliance pacific insuarnce and financial
				"000024",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusA,
				//internationale campagnie d'assurance vie
				"610643",1,"04",claimTypes|reversalTypes,canadianNetworkNumCSI,
				//johnson inc.
				"627265",1,"04",claimTypes,canadianNetworkNumTelusB,
				//johnston group
				"627223",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusA,
				//lee-power & associates
				"627585",1,"02",claimTypes,canadianNetworkNumTelusB,
				//local 1030 health benefity plan
				"000118",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumCSI,
				//manion wilkins
				"610158",1,"04",claimTypes|reversalTypes,canadianNetworkNumTelusB,
				//manitoba blue cross
				"000094",1,"04",claimTypes|reversalTypes|predeterminationTypes|rotTypes,canadianNetworkNumMBC,
				//manitoba cleft palate program
				"000114",1,"04",claimTypes|predeterminationTypes|rotTypes,canadianNetworkNumCSI,
				//manitoba health
				"000113",1,"04",claimTypes|rotTypes,canadianNetworkNumCSI,
				//manufacturers life insurance company
				"000034",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//manulife financial
				"610059",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//maritime life assurance company
				"311113",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//maritime pro
				"610070",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//mdm
				"601052",1,"02",claimTypes|reversalTypes|predeterminationTypes|eligibilityTypes,canadianNetworkNumTelusB,
				//medavie blue cross
				"610047",1,"02",claimTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//nexgenrx
				"610634",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumCSI,
				//nihb
				"610124",1,"04",claimTypes|reversalTypes,canadianNetworkNumCSI,
				//nova scotia community services
				"000109",1,"04",claimTypes|reversalTypes|predeterminationTypes|rotTypes|cobTypes|eligibilityTypes,canadianNetworkNumCSI,
				//nova scotia medical services insurance
				"000108",1,"04",claimTypes|reversalTypes|predeterminationTypes|rotTypes|cobTypes|eligibilityTypes,canadianNetworkNumCSI,
				//nunatsiavut government department of health
				"610172",1,"04",claimTypes|reversalTypes,canadianNetworkNumCSI,
				//pacific blue cross
				"000064",1,"04",claimTypes|predeterminationTypes|rotTypes,canadianNetworkNumPBC,
				//quickcard
				"000103",1,"04",claimTypes|reversalTypes|predeterminationTypes|rotTypes|cobTypes|eligibilityTypes,canadianNetworkNumCSI,
				//pbas
				"610256",1,"04",claimTypes|predeterminationTypes|rotTypes,canadianNetworkNumCSI,
				//rwam insurance
				"610616",1,"04",claimTypes|reversalTypes,canadianNetworkNumTelusB,
				//saskatchewan blue cross
				"000096",1,"04",claimTypes,canadianNetworkNumTelusB,
				//ses benefits
				"610196",1,"04",claimTypes|reversalTypes,canadianNetworkNumTelusB,
				//sheet metal workers local 30 benefit plan
				"000119",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumCSI,
				//ssq societe d'assurance-vie inc.
				"000079",1,"04",claimTypes,canadianNetworkNumCSI,
				//standard life assurance company
				"000020",1,"04",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
				//sun life of canada
				"000016",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusA,
				//survivance
				"000080",1,"04",claimTypes,canadianNetworkNumCSI,
				//syndicat des fonctionnaires municipaux mtl
				"610677",1,"04",claimTypes|reversalTypes,canadianNetworkNumCSI,
				//the building union of canada health beneift plan
				"000120",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumCSI,
				//u.a. local 46 dental plan
				"000115",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumCSI,
				//u.a. local 787 health trust fund dental plan
				"000110",1,"04",claimTypes|predeterminationTypes,canadianNetworkNumCSI,
				//wawanesa
				"311109",1,"02",claimTypes|reversalTypes|predeterminationTypes,canadianNetworkNumTelusB,
			};
			string log="";
			if(isCheck) {
				int numFound=0;
				for(int i=0;i<carrierInfo.Length;i+=5) {
					command="SELECT COUNT(*) "+
						"FROM carrier "+
						"WHERE IsCDA<>0 AND ElectID='"+POut.String((string)carrierInfo[i])+"' AND "+
						"(CanadianEncryptionMethod<>"+POut.Int((int)carrierInfo[i+1])+" OR "+
						"CDAnetVersion<>'"+POut.String((string)carrierInfo[i+2])+"' OR "+
						"CanadianSupportedTypes<>"+POut.Int((int)carrierInfo[i+3])+" OR "+
						"CanadianNetworkNum<>"+POut.Long((long)carrierInfo[i+4])+")";
					numFound+=PIn.Int(Db.GetCount(command));
				}
				if(numFound!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","CDANet carriers with incorrect network, encryption method or version, based on carrier identification number: ")+numFound+"\r\n";
				}
			}
			else {
				long numberFixed=0;
				for(int i=0;i<carrierInfo.Length;i+=5) {
					command="UPDATE carrier SET "+
						"CanadianEncryptionMethod="+POut.Int((int)carrierInfo[i+1])+","+
						"CDAnetVersion='"+POut.String((string)carrierInfo[i+2])+"',"+
						"CanadianSupportedTypes="+POut.Int((int)carrierInfo[i+3])+","+
						"CanadianNetworkNum="+POut.Long((long)carrierInfo[i+4])+" "+
						"WHERE IsCDA<>0 AND ElectID='"+POut.String((string)carrierInfo[i])+"' AND "+
						"(CanadianEncryptionMethod<>"+POut.Int((int)carrierInfo[i+1])+" OR "+
						"CDAnetVersion<>'"+POut.String((string)carrierInfo[i+2])+"' OR "+
						"CanadianSupportedTypes<>"+POut.Int((int)carrierInfo[i+3])+" OR "+
						"CanadianNetworkNum<>"+POut.Long((long)carrierInfo[i+4])+")";
					numberFixed+=Db.NonQ(command);
				}
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","CDANet carriers fixed based on carrier identification number: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimDeleteWithNoClaimProcs(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM claim WHERE NOT EXISTS(
					SELECT * FROM claimproc WHERE claim.ClaimNum=claimproc.ClaimNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims found with no procedures: ")+numFound+"\r\n";
				}
			}
			else{
				//Orphaned claims do not show in the account module (tested) so we need to delete them because no other way.
				command=@"DELETE FROM claim WHERE NOT EXISTS(
					SELECT * FROM claimproc WHERE claim.ClaimNum=claimproc.ClaimNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims deleted due to no procedures: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimWithInvalidInsSubNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				//check 2 situations:
				//1. claim.PlanNum=0 and inssub.PlanNum=0
				command=@"SELECT COUNT(*) FROM claim,inssub WHERE claim.InsSubNum=inssub.InsSubNum AND claim.PlanNum=0 AND inssub.PlanNum=0 ";
				int planCount=PIn.Int(Db.GetCount(command));
				//2. claim.PlanNum=0 and inssub does not exist
				command=@"SELECT COUNT(*) FROM claim WHERE PlanNum=0 AND InsSubNum NOT IN (SELECT InsSubNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum)";
				int existCount=PIn.Int(Db.GetCount(command));
				int numFound=planCount+existCount;
				if(numFound!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims with invalid InsSubNum: ")+numFound+"\r\n";
				}
				//situation where PlanNum and InsSubNum are both invalid and not zero is handled in InsSubNumMismatchPlanNum
			}
			else {
				command=@"SELECT claim.ClaimNum,claim.PatNum FROM claim,inssub WHERE claim.InsSubNum=inssub.InsSubNum AND claim.PlanNum=0 AND inssub.PlanNum=0 "
					+"UNION "
					+"SELECT ClaimNum,PatNum FROM claim WHERE PlanNum=0 AND InsSubNum NOT IN (SELECT InsSubNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum)";
				table=Db.GetTable(command);
				long numberFixed=table.Rows.Count;
				InsPlan plan=null;
				InsSub sub=null;
				if(numberFixed>0) {
					log+=Lans.g("FormDatabaseMaintenance","List of patients who will need insurance information reentered:")+"\r\n";
				}
				for(int i=0;i<numberFixed;i++) {
					plan=new InsPlan();//Create a dummy plan and carrier to attach claims and claim procs to.
					plan.IsHidden=true;
					plan.CarrierNum=Carriers.GetByNameAndPhone("UNKNOWN CARRIER","").CarrierNum;
					InsPlans.Insert(plan);
					long claimNum=PIn.Long(table.Rows[i]["ClaimNum"].ToString());
					long patNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
					sub=new InsSub();//Create inssubs and attach claim and procs to both plan and inssub.
					sub.PlanNum=plan.PlanNum;
					sub.Subscriber=PIn.Long(table.Rows[i]["PatNum"].ToString());
					sub.SubscriberID="unknown";
					InsSubs.Insert(sub);
					command="UPDATE claim SET PlanNum="+plan.PlanNum+",InsSubNum="+sub.InsSubNum+" WHERE ClaimNum="+claimNum;
					Db.NonQ(command);
					command="UPDATE claimproc SET PlanNum="+plan.PlanNum+",InsSubNum="+sub.InsSubNum+" WHERE ClaimNum="+claimNum;
					Db.NonQ(command);
					Patient pat=Patients.GetLim(patNum);
					log+="PatNum: "+pat.PatNum+" - "+Patients.GetNameFL(pat.LName,pat.FName,pat.Preferred,pat.MiddleI)+"\r\n";
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims with invalid InsSubNum fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimWriteoffSum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			string log="";
			//Sums for each claim---------------------------------------------------------------------
			command=@"SELECT claim.ClaimNum,SUM(claimproc.WriteOff) sumwo,claim.WriteOff
				FROM claim,claimproc
				WHERE claim.ClaimNum=claimproc.ClaimNum
				GROUP BY claim.ClaimNum,claim.WriteOff
				HAVING sumwo-claim.WriteOff > .01
				OR sumwo-claim.WriteOff < -.01";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Claim writeoff sums found incorrect: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE claim SET WriteOff='"+POut.Double(PIn.Double(table.Rows[i]["sumwo"].ToString()))+"' "
				    +"WHERE ClaimNum="+table.Rows[i]["ClaimNum"].ToString();
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claim writeoff sums fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		///<Summary>also fixes resulting deposit misbalances.</Summary>
		public static string ClaimPaymentCheckAmt(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(DataConnection.DBtype==DatabaseType.Oracle){
				return "";
			}
			string log="";
			//because of the way this is grouped, it will just get one of many patients for each
			command=@"SELECT claimproc.ClaimPaymentNum,ROUND(SUM(InsPayAmt),2) _sumpay,ROUND(CheckAmt,2) _checkamt,claimproc.PatNum
					FROM claimpayment,claimproc
					WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum
					GROUP BY claimproc.ClaimPaymentNum,CheckAmt
					HAVING _sumpay!=_checkamt";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Claim payment sums found incorrect: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				//Changing the claim payment sums automatically is dangerous so give the user enough information to investigate themselves.
				if(table.Rows.Count>1) {
					log=Lans.g("FormDatabaseMaintenance","The following claim payment sums are incorrect")+":\r\n";
					for(int i=0;i<table.Rows.Count;i++) {
						Patient pat=Patients.GetPat(PIn.Long(table.Rows[i]["PatNum"].ToString()));
						command="SELECT CheckDate,CheckAmt,IsPartial FROM claimpayment WHERE ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
						DataTable claimPayTable=Db.GetTable(command);
						if(pat==null) {
							//insert pat
							Patient dummyPatient=new Patient();
							dummyPatient.PatNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
							dummyPatient.Guarantor=dummyPatient.PatNum;
							dummyPatient.FName="MISSING";
							dummyPatient.LName="PATIENT";
							dummyPatient.AddrNote="This patient was inserted due to claimprocs with invalid PatNum on "+DateTime.Now.ToShortDateString()+" while doing database maintenance.";
							dummyPatient.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
							dummyPatient.PatStatus=PatientStatus.Archived;
							dummyPatient.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
							long dummyPatNum=Patients.Insert(dummyPatient,true);
							pat=Patients.GetPat(dummyPatient.PatNum);
						}
						log+="   Patient: #"+table.Rows[i]["PatNum"].ToString()+":"+pat.GetNameFirstOrPrefL()
							+" Date: "+PIn.Date(claimPayTable.Rows[0]["CheckDate"].ToString()).ToShortDateString()
							+" Amount: "+PIn.Double(claimPayTable.Rows[0]["CheckAmt"].ToString()).ToString("F");
						if(!PIn.Bool(claimPayTable.Rows[0]["IsPartial"].ToString())) {
							command="UPDATE claimpayment SET IsPartial=1 WHERE ClaimPaymentNum="+PIn.Long(table.Rows[i]["ClaimPaymentNum"].ToString()).ToString();
							Db.NonQ(command);
							log+=" (row has been unlocked and marked as partial)";
						}
						log+="\r\n";
					}
					log+=Lans.g("FormDatabaseMaintenance","   They need to be fixed manually.")+"\r\n";
				}
				/*
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE claimpayment SET CheckAmt='"+POut.Double(PIn.Double(table.Rows[i]["_sumpay"].ToString()))+"' "
				    +"WHERE ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0||verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claim payment sums fixed: ")+numberFixed.ToString()+"\r\n";
				}*/
			}
			//now deposits which were affected by the changes above--------------------------------------------------
			command=@"SELECT DepositNum,deposit.Amount,DateDeposit,
				IFNULL((SELECT SUM(CheckAmt) FROM claimpayment WHERE claimpayment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0)
				+IFNULL((SELECT SUM(PayAmt) FROM payment WHERE payment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0) _sum
				FROM deposit
				HAVING ROUND(_sum,2) != ROUND(deposit.Amount,2)";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deposit sums found incorrect: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				/*
				for(int i=0;i<table.Rows.Count;i++) {
					if(i==0) {
						log+=Lans.g("FormDatabaseMaintenance","PRINT THIS FOR REFERENCE. Deposit sums recalculated:")+"\r\n";
					}
					DateTime date=PIn.Date(table.Rows[i]["DateDeposit"].ToString());
					Double oldval=PIn.Double(table.Rows[i]["Amount"].ToString());
					Double newval=PIn.Double(table.Rows[i]["_sum"].ToString());
					log+=date.ToShortDateString()+" "+Lans.g("FormDatabaseMaintenance","OldSum:")+oldval.ToString("c")
				    +", "+Lans.g("FormDatabaseMaintenance","NewSum:")+newval.ToString("c")+"\r\n";
					command="UPDATE deposit SET Amount='"+POut.Double(PIn.Double(table.Rows[i]["_sum"].ToString()))+"' "
				    +"WHERE DepositNum="+table.Rows[i]["DepositNum"].ToString();
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0||verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deposit sums fixed: ")+numberFixed.ToString()+"\r\n";
				}*/
			}
			return log;
		}

		public static string ClaimPaymentDetachMissingDeposit(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM claimpayment "
					+"WHERE DepositNum != 0 " 
					+"AND NOT EXISTS(SELECT * FROM deposit WHERE deposit.DepositNum=claimpayment.DepositNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claim payments attached to deposits that no longer exist: ")+numFound+"\r\n";
				}
			}
			else {
				command="UPDATE claimpayment SET DepositNum=0 "
					+"WHERE DepositNum != 0 " 
					+"AND NOT EXISTS(SELECT * FROM deposit WHERE deposit.DepositNum=claimpayment.DepositNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claim payments detached from deposits that no longer exist: ")
				  +numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcDateNotMatchCapComplete(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM claimproc WHERE Status=7 AND DateCP != ProcDate";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
				  log+=Lans.g("FormDatabaseMaintenance","Capitation procs with mismatched dates found: ")+numFound+"\r\n";
				}
			}
			else{
				//js ok
				command="UPDATE claimproc SET DateCP=ProcDate WHERE Status=7 AND DateCP != ProcDate";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Capitation procs with mismatched dates fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcDateNotMatchPayment(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT claimproc.ClaimProcNum,claimpayment.CheckDate FROM claimproc,claimpayment "
				+"WHERE claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum "
				+"AND claimproc.DateCP!=claimpayment.CheckDate";
				table=Db.GetTable(command);
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Claim payments with mismatched dates found: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				//This is a very strict relationship that has been enforced rigorously for many years.
				//If there is an error, it is a fairly new error.  All errors must be repaired.
				//It won't change amounts of history, just dates.  The changes will typically be only a few days or weeks.
				//Various reports assume this enforcement and the reports will malfunction if this is not fixed.
				//Let's list out each change.  Patient name, procedure desc, date of service, old dateCP, new dateCP (check date).
				command="SELECT patient.LName,patient.FName,patient.MiddleI,claimproc.CodeSent,claim.DateService,claimproc.DateCP,claimpayment.CheckDate,claimproc.ClaimProcNum "
				+"FROM claimproc,patient,claim,claimpayment "
				+"WHERE claimproc.PatNum=patient.PatNum "
				+"AND claimproc.ClaimNum=claim.ClaimNum "
				+"AND claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum "
				+"AND claimproc.DateCP!=claimpayment.CheckDate";
				table=Db.GetTable(command);
				string patientName;
				string codeSent;
				DateTime dateService;
				DateTime oldDateCP;
				DateTime newDateCP;
				if(table.Rows.Count>0 || verbose){
					log+="Claim payments with mismatched dates (Patient Name, Code Sent, Date of Service, Old Date, New Date):\r\n";
				}
				for(int i=0;i<table.Rows.Count;i++) {
					patientName=table.Rows[i]["LName"].ToString() + ", " + table.Rows[i]["FName"].ToString() + " " + table.Rows[i]["MiddleI"].ToString();
					patientName=patientName.Trim();//Looks better when middle initial is not present.//Doesn't work though
					codeSent=table.Rows[i]["CodeSent"].ToString();
					dateService=PIn.Date(table.Rows[i]["DateService"].ToString());
					oldDateCP=PIn.Date(table.Rows[i]["DateCP"].ToString());
				  newDateCP=PIn.Date(table.Rows[i]["CheckDate"].ToString());
				  command="UPDATE claimproc SET DateCP="+POut.Date(newDateCP)
				    +" WHERE ClaimProcNum="+table.Rows[i]["ClaimProcNum"].ToString();
				  Db.NonQ(command);
					log+=patientName + ", " + codeSent + ", " + dateService.ToShortDateString() + ", " + oldDateCP.ToShortDateString() + ", " + newDateCP.ToShortDateString() + "\r\n";
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claim payments with mismatched dates fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcWithInvalidClaimNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM claimproc WHERE claimproc.ClaimNum!=0 "
				  +"AND NOT EXISTS(SELECT * FROM claim WHERE claim.ClaimNum=claimproc.ClaimNum) "
					+"AND (claimproc.InsPayAmt!=0 OR claimproc.WriteOff!=0)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs found with invalid ClaimNum: ")+numFound+"\r\n";
				}
			}
			else{//fix
				//We can't touch those claimprocs because it would mess up the accounting. 
				//For those that are not received, just warn the user
				command="SELECT * FROM claimproc WHERE claimproc.ClaimNum!=0 "
				  +"AND NOT EXISTS(SELECT * FROM claim WHERE claim.ClaimNum=claimproc.ClaimNum) "
					+"AND (claimproc.InsPayAmt!=0 OR claimproc.WriteOff!=0) "
					+"AND claimproc.Status!="+POut.Int((int)ClaimProcStatus.Received);
				table=Db.GetTable(command);
				List<ClaimProc> cpList=Crud.ClaimProcCrud.TableToList(table);
				for(int i=0;i<cpList.Count;i++) {
					Patient pat=Patients.GetLim(cpList[i].PatNum);
					log+=Lans.g("FormDatabaseMaintenance","Claimproc found with invalid ClaimNum for patient: ")
						+pat.PatNum+" - "+Patients.GetNameFL(pat.LName,pat.FName,pat.Preferred,pat.MiddleI)+", "
						+Lans.g("FormDatabaseMaintenance","Date: ")+cpList[i].ProcDate.ToShortDateString()
						+"\r\n";
				}
				//For those that are received, create dummy claim that has the specific ClaimNum that seems to be missing.
				command="SELECT * FROM claimproc WHERE claimproc.ClaimNum!=0 "
				  +"AND NOT EXISTS(SELECT * FROM claim WHERE claim.ClaimNum=claimproc.ClaimNum) "
					+"AND (claimproc.InsPayAmt!=0 OR claimproc.WriteOff!=0) "
					+"AND claimproc.Status="+POut.Int((int)ClaimProcStatus.Received);
				table=Db.GetTable(command);
				cpList=Crud.ClaimProcCrud.TableToList(table);
				Claim claim;
				for(int i=0;i<cpList.Count;i++) {
					claim=new Claim();
					claim.ClaimNum=cpList[i].ClaimNum;
					claim.PatNum=cpList[i].PatNum;
					claim.ClinicNum=cpList[i].ClinicNum;
					claim.ClaimStatus="R";//Status received because we know it's been paid on
					claim.PlanNum=cpList[i].PlanNum;
					claim.InsSubNum=cpList[i].InsSubNum;
					claim.ProvTreat=cpList[i].ProvNum;
					Crud.ClaimCrud.Insert(claim,true);//Allows us to use a primary key that was "used".
					Patient pat=Patients.GetLim(claim.PatNum);
					log+=Lans.g("FormDatabaseMaintenance","Claim created due to claimprocs with invalid ClaimNums for patient: ")
						+pat.PatNum+" - "+Patients.GetNameFL(pat.LName,pat.FName,pat.Preferred,pat.MiddleI)+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcDeleteDuplicateEstimateForSameInsPlan(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			//Get all the claimproc estimates that already have a claimproc marked as received from the same InsPlan.
			command="SELECT cp.ClaimProcNum FROM claimproc cp "
				+" INNER JOIN claimproc cp2 ON cp2.PatNum=cp.PatNum"
				+" AND cp2.PlanNum=cp.PlanNum"    //The same insurance plan
				+" AND cp2.InsSubNum=cp.InsSubNum"//for the same subscriber
				+" AND cp2.ProcNum=cp.ProcNum"    //for the same procedure.
				+" AND cp2.Status="+POut.Int((int)ClaimProcStatus.Received)
				+" WHERE cp.Status="+POut.Int((int)ClaimProcStatus.Estimate)
				+" AND cp.ClaimNum=0";//Make sure the estimate is not already attached to a claim somehow.
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Duplicate ClaimProc estimates for the same InsPlan found: ")+table.Rows.Count+"\r\n";
				}
			}
			else {
				if(table.Rows.Count>0) {
					command="DELETE FROM claimproc WHERE ClaimProcNum IN (";
					for(int i=0;i<table.Rows.Count;i++) {
						if(i>0) {
							command+=",";
						}
						command+=table.Rows[i]["ClaimProcNum"].ToString();
					}
					command+=")";
					Db.NonQ(command);
				}
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Duplicate ClaimProc estimates for the same InsPlan deleted: ")+table.Rows.Count+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcDeleteWithInvalidClaimNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM claimproc WHERE claimproc.ClaimNum!=0 "
				  +"AND NOT EXISTS(SELECT * FROM claim WHERE claim.ClaimNum=claimproc.ClaimNum) "
					+"AND claimproc.InsPayAmt=0 AND claimproc.WriteOff=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs found with invalid ClaimNum: ")+numFound+"\r\n";
				}
			}
			else {
				command="DELETE FROM claimproc WHERE claimproc.ClaimNum!=0 "
				  +"AND NOT EXISTS(SELECT * FROM claim WHERE claim.ClaimNum=claimproc.ClaimNum) "
					+"AND claimproc.InsPayAmt=0 AND claimproc.WriteOff=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs deleted due to invalid ClaimNum: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcDeleteMismatchPatNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				//claimproc.PatNum != procedurelog.PatNum
				command="SELECT COUNT(*) FROM claimproc "
					+"WHERE ProcNum > 0 " 
					+"AND claimproc.PatNum!=(SELECT procedurelog.PatNum FROM procedurelog WHERE claimproc.ProcNum=procedurelog.ProcNum) "
					+"AND claimproc.InsPayAmt=0 AND claimproc.WriteOff=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs found with PatNum that doesn't match the procedure PatNum: ")+numFound+"\r\n";
				}
			}
			else {
				command="DELETE FROM claimproc "
					+"WHERE ProcNum > 0 " 
					+"AND claimproc.PatNum!=(SELECT procedurelog.PatNum FROM procedurelog WHERE claimproc.ProcNum=procedurelog.ProcNum) "
					+"AND claimproc.InsPayAmt=0 AND claimproc.WriteOff=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs deleted due to PatNum not matching the procedure PatNum: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcDeleteEstimateWithInvalidProcNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM claimproc WHERE ProcNum>0 "
					+"AND Status="+POut.Int((int)ClaimProcStatus.Estimate)+" "
					+"AND NOT EXISTS(SELECT * FROM procedurelog "
				  +"WHERE claimproc.ProcNum=procedurelog.ProcNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Estimates found for procedures that no longer exist: ")+numFound+"\r\n";
				}
			}
			else{
				//These seem to pop up quite regularly due to the program forgetting to delete them
				command="DELETE FROM claimproc WHERE ProcNum>0 "
					+"AND Status="+POut.Int((int)ClaimProcStatus.Estimate)+" "
					+"AND NOT EXISTS(SELECT * FROM procedurelog "
				  +"WHERE claimproc.ProcNum=procedurelog.ProcNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Estimates deleted for procedures that no longer exist: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcDeleteCapEstimateWithProcComplete(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM claimproc WHERE ProcNum>0 "
					+"AND Status="+POut.Int((int)ClaimProcStatus.CapEstimate)+" "
					+"AND EXISTS("
						+"SELECT * FROM procedurelog "
						+"WHERE claimproc.ProcNum=procedurelog.ProcNum "
						+"AND procedurelog.ProcStatus="+POut.Int((int)ProcStat.C)
					+")";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Capitation estimates found for completed procedures: ")+numFound.ToString()+"\r\n";
				}
			}
			else{
				command="DELETE FROM claimproc WHERE ProcNum>0 "
					+"AND Status="+POut.Int((int)ClaimProcStatus.CapEstimate)+" "
					+"AND EXISTS("
						+"SELECT * FROM procedurelog "
						+"WHERE claimproc.ProcNum=procedurelog.ProcNum "
						+"AND procedurelog.ProcStatus="+POut.Int((int)ProcStat.C)
					+")";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Capitation estimates deleted for completed procedures: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcEstNoBillIns(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM claimproc WHERE NoBillIns=1 AND InsPayEst !=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs found with non-zero estimates marked NoBillIns: ")+numFound+"\r\n";
				}
			}
			else{
				//This is just estimate info, regardless of the claimproc status, so totally safe.
				command="UPDATE claimproc SET InsPayEst=0 WHERE NoBillIns=1 AND InsPayEst !=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimproc estimates set to zero because marked NoBillIns: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcEstWithInsPaidAmt(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM claimproc WHERE InsPayAmt > 0 AND ClaimNum=0 AND Status=6";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProc estimates with InsPaidAmt > 0 found: ")+numFound+"\r\n";
				}
			}
			else{
				//The InsPayAmt is already being ignored due to the status of the claimproc.  So changing its value is harmless.
				command=@"UPDATE claimproc SET InsPayAmt=0 WHERE InsPayAmt > 0 AND ClaimNum=0 AND Status=6";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProc estimates with InsPaidAmt > 0 fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcPatNumMissing(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM claimproc WHERE PatNum=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with missing patnums found: ")+numFound+"\r\n";
				}
			}
			else {
				command="DELETE FROM claimproc WHERE PatNum=0 AND InsPayAmt=0 AND WriteOff=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with missing patnums fixed: ")+numberFixed+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcProvNumMissing(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM claimproc WHERE ProvNum=0 AND Status!=3";//Status 3 is adjustment which does not require a provider.
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with missing provnums found: ")+numFound+"\r\n";
				}
			}
			else{
				//If estimate, set to default prov (doesn't affect finances)
				command="UPDATE claimproc SET ProvNum="+PrefC.GetString(PrefName.PracticeDefaultProv)+" WHERE ProvNum=0 AND Status="+POut.Int((int)ClaimProcStatus.Estimate);
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
				  log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with missing provnums fixed: ")+numberFixed.ToString()+"\r\n";
				}
				//create a dummy provider (using helper function in Providers.cs)
				//change provnum to the dummy prov (something like Providers.GetDummy())
				//Provider dummy=new Provider();
				//dummy.Abbr="Dummy";
				//dummy.FName="Dummy";
				//dummy.LName="Provider";
				//Will get to this soon.
				//01-17-2011 No fix yet. This has not caused issues except for notifying users.
			}
			return log;
		}

		public static string ClaimProcPreauthNotMatchClaim(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command=@"SELECT claimproc.ClaimProcNum 
				FROM claimproc,claim 
				WHERE claimproc.ClaimNum=claim.ClaimNum
				AND claim.ClaimType='PreAuth'
				AND claimproc.Status!=2";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs for preauths with status not preauth fixed: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				//Take no action.  Use descriptive explanation.
			}
			return log;
		}

		public static string ClaimProcStatusNotMatchClaim(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM claimproc,claim
					WHERE claimproc.ClaimNum=claim.ClaimNum
					AND claim.ClaimStatus='R'
					AND claimproc.Status="+POut.Int((int)ClaimProcStatus.NotReceived);
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with status not matching claim found: ")+numFound+"\r\n";
				}
			}
			else{
				//Take no action.  Use descriptive explanation.
				command=@"SELECT claim.PatNum,claim.DateService,claimproc.ProcDate,claimproc.CodeSent,claimproc.FeeBilled
					FROM claimproc,claim
					WHERE claimproc.ClaimNum=claim.ClaimNum
					AND claim.ClaimStatus='R'
					AND claimproc.Status="+POut.Int((int)ClaimProcStatus.NotReceived);
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					Patient pat=Patients.GetPat(PIn.Long(table.Rows[i]["PatNum"].ToString()));
					if(i==0) {
						log+=Lans.g("FormDatabaseMaintenance","The following ClaimProc statuses do not match the claim")+":\r\n";
					}
					log+="   Patient: #"+pat.PatNum.ToString()+":"+pat.GetNameFirstOrPrefL()
						+" ClaimDate: "+PIn.Date(table.Rows[i]["DateService"].ToString()).ToShortDateString()
						+" ProcDate: "+PIn.Date(table.Rows[i]["ProcDate"].ToString()).ToShortDateString()
						+" Code: "+table.Rows[i]["CodeSent"].ToString()
						+" FeeBilled: "+PIn.Double(table.Rows[i]["FeeBilled"].ToString()).ToString("F")+"\r\n";
				}
				if(table.Rows.Count>0) {
					log+=Lans.g("FormDatabaseMaintenance","   They need to be fixed manually.")+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcWithInvalidClaimPaymentNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM claimproc WHERE claimpaymentnum !=0 AND NOT EXISTS(
					SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with with invalid ClaimPaymentNumber found: ")+numFound+"\r\n";
				}
			}
			else{
				//slightly dangerous.  User will have to creat ins check again.  But does not alter financials.
				command=@"UPDATE claimproc SET ClaimPaymentNum=0 WHERE claimpaymentnum !=0 AND NOT EXISTS(
					SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with with invalid ClaimPaymentNumber fixed: ")+numberFixed.ToString()+"\r\n";
					//Tell user what items to create ins checks for?
				}
			}
			return log;
		}

		public static string ClaimProcWriteOffNegative(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM claimproc WHERE WriteOff < 0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Negative writeoffs found: ")+numFound+"\r\n";
				}
			}
			else{
				//Take no action.  Use descriptive explanation.
				command=@"SELECT patient.LName,patient.FName,patient.MiddleI,claimproc.CodeSent,procedurelog.ProcFee,procedurelog.ProcDate,claimproc.WriteOff
					FROM claimproc 
					LEFT JOIN patient ON claimproc.PatNum=patient.PatNum
					LEFT JOIN procedurelog ON claimproc.ProcNum=procedurelog.ProcNum 
					WHERE WriteOff<0";
				table=Db.GetTable(command);
				string patientName;
				string codeSent;
				decimal writeOff;
				decimal procFee;
				DateTime procDate;
				if(table.Rows.Count>0) {
					log+=Lans.g("FormDatabaseMaintenance","List of patients with procedures that have negative writeoffs:\r\n");
					for(int i=0;i<table.Rows.Count;i++) {
						patientName=table.Rows[i]["LName"].ToString() + ", " + table.Rows[i]["FName"].ToString() + " " + table.Rows[i]["MiddleI"].ToString();
						codeSent=table.Rows[i]["CodeSent"].ToString();
						procDate=PIn.Date(table.Rows[i]["ProcDate"].ToString());
						writeOff=PIn.Decimal(table.Rows[i]["WriteOff"].ToString());
						procFee=PIn.Decimal(table.Rows[i]["ProcFee"].ToString());
						log+=patientName+" "+codeSent+" fee:"+procFee.ToString("c")+" date:"+procDate.ToShortDateString()+" writeoff:"+writeOff.ToString("c")+"\r\n";
					}
					log+=Lans.g("FormDatabaseMaintenance","Go to the patients listed above and manually correct the writeoffs.\r\n");
				}
			}
			return log;
		}

		public static string ClockEventInFuture(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM clockevent WHERE TimeDisplayed1 > "+DbHelper.Now()+"+INTERVAL 15 MINUTE";
				int numFound=PIn.Int(Db.GetCount(command));
				command=@"SELECT COUNT(*) FROM clockevent WHERE TimeDisplayed2 > "+DbHelper.Now()+"+INTERVAL 15 MINUTE";
				numFound+=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Timecard entries invalid: ")+numFound+"\r\n";
				}
			}
			else{
				command=@"UPDATE clockevent SET TimeDisplayed1="+DbHelper.Now()+" WHERE TimeDisplayed1 > "+DbHelper.Now()+"+INTERVAL 15 MINUTE";
				long numberFixed=Db.NonQ(command);
				command=@"UPDATE clockevent SET TimeDisplayed2="+DbHelper.Now()+" WHERE TimeDisplayed2 > "+DbHelper.Now()+"+INTERVAL 15 MINUTE";
				numberFixed+=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Future timecard entry times fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string DocumentWithNoCategory(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT DocNum FROM document WHERE DocCategory=0";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Images with no category found: ")+table.Rows.Count+"\r\n";
				}
			}
			else{
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE document SET DocCategory="+POut.Long(DefC.Short[(int)DefCat.ImageCats][0].DefNum)
				    +" WHERE DocNum="+table.Rows[i][0].ToString();
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Images with no category fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string EduResourceInvalidDiseaseDefNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT EduResourceNum FROM eduresource WHERE DiseaseDefNum NOT IN (SELECT DiseaseDefNum FROM diseasedef)";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","EHR Educational Resources with invalid problem found: ")+table.Rows.Count+"\r\n";
				}
			}
			else {
				command="SELECT DiseaseDefNum FROM diseasedef WHERE ItemOrder=(SELECT MIN(ItemOrder) FROM diseasedef WHERE IsHidden=0)";
				long defNum=PIn.Long(Db.GetScalar(command));
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE eduresource SET DiseaseDefNum='"+defNum+"' WHERE EduResourceNum='"+table.Rows[i][0].ToString()+"'";
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","EHR Educational Resources with invalid problem fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string FeeDeleteDuplicates(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			command="SELECT FeeNum,FeeSched,CodeNum,Amount FROM fee GROUP BY FeeSched,CodeNum HAVING COUNT(CodeNum)>1";
			table=Db.GetTable(command);
			int count=table.Rows.Count;
			string log="";
			if(isCheck) {
				if(count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedure codes with duplicate fee entries: ")+count+"\r\n";
				}
			}
			else {//fix
				long numberFixed=0;
				for(int i=0;i<count;i++) {
					if(i==0) {
						log+=Lans.g("FormDatabaseMaintenance","The following procedure codes had duplicate fee entries.  Verify that the following amounts are correct:")+"\r\n";
					}
					//Make an entry in the log so that the user knows to verify the amount for this fee.
					log+="Fee Schedule: "+FeeScheds.GetDescription(PIn.Long(table.Rows[i]["FeeSched"].ToString()))//No call to db.
						+" - Code: "+ProcedureCodes.GetStringProcCode(PIn.Long(table.Rows[i]["CodeNum"].ToString()))//No call to db.
						+" - Amount: "+PIn.Double(table.Rows[i]["Amount"].ToString()).ToString("n")+"\r\n";
					//At least one fee needs to stay.  Each row in table is a random fee, so we'll just keep that one and delete the rest.
					command="DELETE FROM fee WHERE FeeSched="+table.Rows[i]["FeeSched"].ToString()
						+" AND CodeNum="+table.Rows[i]["CodeNum"].ToString()
						+" AND FeeNum!="+table.Rows[i]["FeeNum"].ToString();//This is the random fee we will keep.
					numberFixed+=Db.NonQ(command);
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Duplicate fees deleted: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}
		
		public static string InsPlanInvalidCarrier(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			//Gets a list of insurance plans that do not have a carrier attached. The list should be blank. If not, then you need to go to the plan listed and add a carrier. Missing carriers will cause the send claims function to give an error.
			command="SELECT PlanNum FROM insplan WHERE CarrierNum NOT IN (SELECT CarrierNum FROM carrier)";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Ins plans with carrier missing found: ")+table.Rows.Count+"\r\n";
				}
			}
			else {
				if(table.Rows.Count>0) {
					Carrier carrier=Carriers.GetByNameAndPhone("UNKNOWN CARRIER","");
					command="UPDATE insplan SET CarrierNum="+POut.Long(carrier.CarrierNum)//set this new carrier for all insplans
						+" WHERE CarrierNum NOT IN (SELECT CarrierNum FROM carrier)";//which have invalid carriernums
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Ins plans with carrier missing fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string InsPlanInvalidNum(bool verbose,bool isCheck) {
			//Many sections removed because they are now fixed in InsSubNumMismatchPlanNum.
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM appointment WHERE appointment.InsPlan1 != 0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=appointment.InsPlan1)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid appointment InsPlan1 values: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM appointment WHERE appointment.InsPlan2 != 0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=appointment.InsPlan2)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid appointment InsPlan2 values: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM benefit WHERE PlanNum !=0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=benefit.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid benefit PlanNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM inssub WHERE NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=inssub.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid inssub PlanNums: ")+numFound+"\r\n";
				}
			}
			else {//fix
				//One option will sometimes be to create a dummy plan to attach these things to, be we have not had to implement that yet.  
				//We need databases with actual problems to test these fixes against.
				//appointment.InsPlan1-----------------------------------------------------------------------------------------------
				command="UPDATE appointment SET InsPlan1=0 WHERE InsPlan1 != 0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=appointment.InsPlan1)";
				long numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid appointment InsPlan1 values fixed: ")+numFixed+"\r\n";
				}
				//appointment.InsPlan2-----------------------------------------------------------------------------------------------
				command="UPDATE appointment SET InsPlan2=0 WHERE InsPlan2 != 0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=appointment.InsPlan2)";
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid appointment InsPlan2 values fixed: ")+numFixed+"\r\n";
				}
				//benefit.PlanNum----------------------------------------------------------------------------------------------------
				command="DELETE FROM benefit WHERE PlanNum !=0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=benefit.PlanNum)";
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid benefit PlanNums fixed: ")+numFixed+"\r\n";
				}
				//inssub.PlanNum------------------------------------------------------------------------------------------------------
				numFixed=0;
				//1: PlanNum=0
				command="SELECT InsSubNum FROM inssub WHERE PlanNum=0";
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					long insSubNum=PIn.Long(table.Rows[i]["InsSubNum"].ToString());
					command="SELECT COUNT(*) FROM claim WHERE InsSubNum="+POut.Long(insSubNum);
					int countUsed=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM claimproc WHERE InsSubNum="+POut.Long(insSubNum)+" AND (ClaimNum<>0 OR (Status<>6 AND Status<>3))";//attached to a claim or (not an estimate or adjustment)
					countUsed+=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM etrans WHERE InsSubNum="+POut.Long(insSubNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					//command="SELECT COUNT(*) FROM patplan WHERE InsSubNum="+POut.Long(insSubNum);
					//countUsed+=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM payplan WHERE InsSubNum="+POut.Long(insSubNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					if(countUsed==0) {
						command="DELETE FROM claimproc WHERE InsSubNum="+POut.Long(insSubNum)+" AND ClaimNum=0 AND (Status=6 OR Status=3)";//ok to delete because no claim and just an estimate or adjustment
						Db.NonQ(command);
						command="DELETE FROM inssub WHERE InsSubNum="+POut.Long(insSubNum);
						Db.NonQ(command);
						command="DELETE FROM patplan WHERE InsSubNum="+POut.Long(insSubNum);//It's very safe to "drop coverage" for a patient.
						Db.NonQ(command);
						numFixed++;
						continue;
					}
				}
				//2: PlanNum invalid
				command="SELECT InsSubNum,PlanNum FROM inssub WHERE NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=inssub.PlanNum)";
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					long planNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());
					long insSubNum=PIn.Long(table.Rows[i]["InsSubNum"].ToString());
					command="SELECT COUNT(*) FROM claim WHERE InsSubNum="+POut.Long(insSubNum);
					int countUsed=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM claimproc WHERE InsSubNum="+POut.Long(insSubNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM etrans WHERE InsSubNum="+POut.Long(insSubNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM patplan WHERE InsSubNum="+POut.Long(insSubNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM payplan WHERE InsSubNum="+POut.Long(insSubNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					//planNum
					command="SELECT COUNT(*) FROM benefit WHERE PlanNum="+POut.Long(planNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM claim WHERE PlanNum="+POut.Long(planNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					command="SELECT COUNT(*) FROM claimproc WHERE PlanNum="+POut.Long(planNum);
					countUsed+=PIn.Int(Db.GetCount(command));
					if(countUsed==0) {//There are no other pointers to this invalid plannum or this inssub, delete this inssub
						command="DELETE FROM inssub WHERE InsSubNum="+POut.Long(insSubNum);
						Db.NonQ(command);
						numFixed++;
						continue;
					}
					else {//There are objects referencing this inssub or this insplan.  Insert a dummy plan linked to a dummy carrier with CarrierName=Unknown
						InsPlan insplan=new InsPlan();
						insplan.IsHidden=true;
						insplan.CarrierNum=Carriers.GetByNameAndPhone("UNKNOWN CARRIER","").CarrierNum;
						long insPlanNum=InsPlans.Insert(insplan);
						command="UPDATE inssub SET PlanNum="+POut.Long(insPlanNum)+" WHERE InsSubNum="+POut.Long(insSubNum);
						Db.NonQ(command);
						numFixed++;
						continue;
					}
				}
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid inssub PlanNums fixed: ")+numFixed+"\r\n";
				}
			}
			return log;
		}

		public static string InsPlanNoClaimForm(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM insplan WHERE ClaimFormNum=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Insplan claimforms missing: ")+numFound+"\r\n";
				}
			}
			else{
				command="UPDATE insplan SET ClaimFormNum="+POut.Long(PrefC.GetLong(PrefName.DefaultClaimForm))
				  +" WHERE ClaimFormNum=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Insplan claimforms set if missing: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string InsSubInvalidSubscriber(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT Subscriber FROM inssub WHERE Subscriber NOT IN (SELECT PatNum FROM patient) AND Subscriber != 0 GROUP BY Subscriber";
			table=Db.GetTable(command);
			if(isCheck) {
				int numFound=table.Rows.Count;
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","InsSub subscribers missing: ")+numFound+"\r\n";
				}
			}
			else {//Fix
				//Create dummy patients using the FKs that the Subscriber column is expecting.
				long priProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
				long billType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
				for(int i=0;i<table.Rows.Count;i++) {
					Patient pat=new Patient();
					pat.PatNum=PIn.Long(table.Rows[i]["Subscriber"].ToString());
					pat.LName="UNKNOWN";
					pat.FName="Unknown";
					pat.Guarantor=pat.PatNum;
					pat.PriProv=priProv;
					pat.BillingType=billType;
					Patients.Insert(pat,true);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","InsSub subscribers fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string InsSubNumMismatchPlanNum(bool verbose,bool isCheck) {
			//Checks for situations where there are valid InsSubNums, but mismatched PlanNums. 
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				int numFound=0;
				//Can't do the following because no inssubnum: appointmentx2, benefit.
				//Can't do inssub because that's what we're comparing against.  That's the one that's assumed to be correct.
				//claim.PlanNum -----------------------------------------------------------------------------------------------------
				command="SELECT COUNT(*) FROM claim "
					+"WHERE PlanNum NOT IN (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum) ";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched claim InsSubNum/PlanNum values: ")+numFound+"\r\n";
				}
				//claim.PlanNum2---------------------------------------------------------------------------------------------------
				command="SELECT COUNT(*) FROM claim WHERE PlanNum2 != 0 "//not really necessary; just a reminder
					+"AND PlanNum2 NOT IN (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum2)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched claim InsSubNum2/PlanNum2 values: ")+numFound+"\r\n";
				}
				//claimproc---------------------------------------------------------------------------------------------------
				command="SELECT COUNT(*) FROM claimproc "
					+"WHERE PlanNum NOT IN (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claimproc.InsSubNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched claimproc InsSubNum/PlanNum values: ")+numFound+"\r\n";
				}
				//etrans---------------------------------------------------------------------------------------------------
				command="SELECT COUNT(*) FROM etrans "
					+"WHERE PlanNum!=0 AND PlanNum NOT IN (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=etrans.InsSubNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched etrans InsSubNum/PlanNum values: ")+numFound+"\r\n";
				}
				//payplan---------------------------------------------------------------------------------------------------
				command="SELECT COUNT(*) FROM payplan "
					+"WHERE EXISTS (SELECT PlanNum FROM inssub WHERE inssub.InsSubNum=payplan.InsSubNum AND inssub.PlanNum!=payplan.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched payplan InsSubNum/PlanNum values: ")+numFound+"\r\n";
				}
			}
			else {//fix
				long numFixed=0;
				//claim.PlanNum (1/4) Mismatch------------------------------------------------------------------------------------------------------
				command="UPDATE claim SET PlanNum = (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum) "
					+"WHERE PlanNum != (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum)";
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched claim InsSubNum/PlanNum fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//claim.PlanNum (2/4) PlanNum zero, invalid InsSubNum--------------------------------------------------------------------------------
				//Will leave orphaned claimprocs. No finanicals to check.
				command="DELETE FROM claim WHERE PlanNum=0 AND ClaimStatus IN ('PreAuth','W','U') AND NOT EXISTS(SELECT * FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum)";
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims deleted with invalid InsSubNum and PlanNum=0: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//claim.PlanNum (3/4) PlanNum invalid, and claim.InsSubNum invalid-------------------------------------------------------------------
				command="SELECT claim.PatNum,claim.PlanNum,claim.InsSubNum FROM claim "
					+"WHERE PlanNum NOT IN (SELECT insplan.PlanNum FROM insplan) "
					+"AND InsSubNum NOT IN (SELECT inssub.InsSubNum FROM inssub) ";
				table=Db.GetTable(command);
				if(table.Rows.Count>0) {
					log+=Lans.g("FormDatabaseMaintenance","List of patients who will need insurance information reentered:")+"\r\n";
				}
				for(int i=0;i<table.Rows.Count;i++) {//Create simple InsPlans and InsSubs for each claim to replace the missing ones.
					//make sure a plan does not exist from a previous insert in this loop
					command="SELECT COUNT(*) FROM insplan WHERE PlanNum = " + table.Rows[i]["PlanNum"].ToString();
					if(Db.GetCount(command)=="0") {
						InsPlan plan=new InsPlan();
						plan.PlanNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());//reuse the existing FK
						plan.IsHidden=true;
						plan.CarrierNum=Carriers.GetByNameAndPhone("UNKNOWN CARRIER","").CarrierNum;
						InsPlans.Insert(plan,true);
					}
					long patNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
					//make sure an inssub does not exist from a previous insert in this loop
					command="SELECT COUNT(*) FROM inssub WHERE InsSubNum = " + table.Rows[i]["InsSubNum"].ToString();
					if(Db.GetCount(command)=="0") {
						InsSub sub=new InsSub();
						sub.InsSubNum=PIn.Long(table.Rows[i]["InsSubNum"].ToString());//reuse the existing FK
						sub.PlanNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());
						sub.Subscriber=patNum;//if this sub was created on a previous loop, this may be some other patient.
						sub.SubscriberID="unknown";
						InsSubs.Insert(sub,true);
					}
					Patient pat=Patients.GetLim(patNum);
					log+="PatNum: "+pat.PatNum+" - "+Patients.GetNameFL(pat.LName,pat.FName,pat.Preferred,pat.MiddleI)+"\r\n";
				}
				numFixed=table.Rows.Count;
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims with invalid PlanNums and invalid InsSubNums fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//claim.PlanNum (4/4) PlanNum valid, but claim.InsSubNum invalid-------------------------------------------------------------------
				command="SELECT PatNum,PlanNum,InsSubNum FROM claim "
					+"WHERE PlanNum IN (SELECT insplan.PlanNum FROM insplan) "
					+"AND InsSubNum NOT IN (SELECT inssub.InsSubNum FROM inssub) GROUP BY InsSubNum";
				table=Db.GetTable(command);
				//Create a dummy inssub and link it to the valid plan.
				for(int i=0;i<table.Rows.Count;i++) {
					InsSub sub=new InsSub();
					sub.InsSubNum=PIn.Long(table.Rows[i]["InsSubNum"].ToString());
					sub.PlanNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());
					sub.Subscriber=PIn.Long(table.Rows[i]["PatNum"].ToString());
					sub.SubscriberID="unknown";
					InsSubs.Insert(sub,true);
				}
				numFixed=table.Rows.Count;
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims with invalid InsSubNums and invalid PlanNums fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//claim.PlanNum2---------------------------------------------------------------------------------------------------
				command="UPDATE claim SET PlanNum2 = (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum2) "
					+"WHERE PlanNum2 != 0 "
					+"AND PlanNum2 NOT IN (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claim.InsSubNum2)";
				//if InsSubNum2 was completely invalid, then PlanNum2 gets set to zero here.
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched claim InsSubNum2/PlanNum2 fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//claimproc (1/2) If planNum is valid but InsSubNum does not exist, then add a dummy inssub----------------------------------------
				command="SELECT PatNum,PlanNum,InsSubNum FROM claimproc "
					+"WHERE PlanNum IN (SELECT insplan.PlanNum FROM insplan) "
					+"AND InsSubNum NOT IN (SELECT inssub.InsSubNum FROM inssub) GROUP BY InsSubNum";
				table=Db.GetTable(command);
				//Create a dummy inssub and link it to the valid plan.
				for(int i=0;i<table.Rows.Count;i++) {
					InsSub sub=new InsSub();
					sub.InsSubNum=PIn.Long(table.Rows[i]["InsSubNum"].ToString());
					sub.PlanNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());
					sub.Subscriber=PIn.Long(table.Rows[i]["PatNum"].ToString());
					sub.SubscriberID="unknown";
					InsSubs.Insert(sub,true);
				}
				numFixed=table.Rows.Count;
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims with valid PlanNums and invalid InsSubNums fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//claimproc (2/2) Mismatch, but InsSubNum is valid
				command="UPDATE claimproc SET PlanNum = (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claimproc.InsSubNum) "
					+"WHERE PlanNum != (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=claimproc.InsSubNum)";
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched claimproc InsSubNum/PlanNum fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//claimproc.PlanNum zero, invalid InsSubNum--------------------------------------------------------------------------------
				command="DELETE FROM claimproc WHERE PlanNum=0 AND NOT EXISTS(SELECT * FROM inssub WHERE inssub.InsSubNum=claimproc.InsSubNum)"
				  +" AND InsPayAmt=0 AND WriteOff=0"//Make sure this deletion will not affect financials.
				  +" AND Status IN (6,2)";//OK to delete because no claim and just an estimate (6) or preauth (2) claimproc
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs deleted with invalid InsSubNum and PlanNum=0: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//etrans---------------------------------------------------------------------------------------------------
				command="UPDATE etrans SET PlanNum = (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=etrans.InsSubNum) "
					+"WHERE PlanNum!=0 AND PlanNum != (SELECT inssub.PlanNum FROM inssub WHERE inssub.InsSubNum=etrans.InsSubNum)";
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched etrans InsSubNum/PlanNum fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
				//payplan--------------------------------------------------------------------------------------------------
				command="UPDATE payplan SET PlanNum=(SELECT PlanNum FROM inssub WHERE inssub.InsSubNum=payplan.InsSubNum) "
					+"WHERE EXISTS (SELECT PlanNum FROM inssub WHERE inssub.InsSubNum=payplan.InsSubNum AND inssub.PlanNum!=payplan.PlanNum)";
				numFixed=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Mismatched payplan InsSubNum/PlanNum fixed: ")+numFixed.ToString()+"\r\n";
				}
				numFixed=0;
			}
			return log;
		}

		public static string LabCaseWithInvalidLaboratory(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM labcase WHERE laboratoryNum NOT IN(SELECT laboratoryNum FROM laboratory)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Lab cases found with invalid laboratories")+": "+numFound+"\r\n";
				}
			}
			else {
				command="SELECT COUNT(*) FROM labcase WHERE laboratoryNum NOT IN(SELECT laboratoryNum FROM laboratory)";
				long numberFixed=long.Parse(Db.GetCount(command));
				command="SELECT * FROM labcase WHERE laboratoryNum NOT IN(SELECT laboratoryNum FROM laboratory) GROUP BY LaboratoryNum";
				table=Db.GetTable(command);
				long labnum;
				for(int i=0;i<table.Rows.Count;i++) {
					Laboratory lab=new Laboratory();
					lab.LaboratoryNum=PIn.Long(table.Rows[i]["LaboratoryNum"].ToString());
					lab.Description="Laboratory "+table.Rows[i]["LaboratoryNum"].ToString();
					//laboratoryNum is not allowed to be zero
					labnum=Crud.LaboratoryCrud.Insert(lab);
					command="UPDATE labcase SET LaboratoryNum="+POut.Long(labnum)+" WHERE LaboratoryNum="+table.Rows[i]["LaboratoryNum"].ToString();
					Db.NonQ(command);
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Lab cases fixed with invalid laboratories")+": "+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string LaboratoryWithInvalidSlip(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM laboratory WHERE Slip NOT IN(SELECT SheetDefNum FROM sheetdef) AND Slip != 0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Laboratories found with invalid lab slips")+": "+numFound+"\r\n";
				}
			}
			else {
				command="UPDATE laboratory SET Slip=0 WHERE Slip NOT IN(SELECT SheetDefNum FROM sheetdef) AND Slip != 0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Laboratories fixed with invalid lab slips")+": "+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string MedicationPatDeleteWithInvalidMedNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM medicationpat WHERE NOT EXISTS(SELECT * FROM medication "
				  +"WHERE medication.MedicationNum=medicationpat.MedicationNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Medications found where no defition exists for them: ")+numFound+"\r\n";
				}
			}
			else{
				command="DELETE FROM medicationpat WHERE NOT EXISTS(SELECT * FROM medication "
				  +"WHERE medication.MedicationNum=medicationpat.MedicationNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Medications deleted because no definition exists for them: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string MessageButtonDuplicateButtonIndex(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			string queryStr="SELECT COUNT(*) NumFound,SigButDefNum,ButtonIndex,ComputerName FROM sigbutdef GROUP BY ComputerName,ButtonIndex HAVING COUNT(*) > 1";
			table=Db.GetTable(queryStr);
			int numFound=0;
			for(int i=0;i<table.Rows.Count;i++) {
				numFound+=PIn.Int(table.Rows[i]["NumFound"].ToString())-1;//Gets the actual number of rows that will be altered.
			}
			if(isCheck) {
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Messaging buttons found with invalid button orders: ")+numFound+"\r\n";
				}
			}
			else {//fix
				do {
					//Loop through the messaging buttons and increment the duplicate button index by the max plus one.
					for(int i=0;i<table.Rows.Count;i++) {
						command="SELECT MAX(ButtonIndex) FROM sigbutdef WHERE ComputerName='"+table.Rows[i]["ComputerName"].ToString()+"'";
						int newIndex=PIn.Int(Db.GetScalar(command))+1;
						command="UPDATE sigbutdef SET ButtonIndex="+newIndex.ToString()+" WHERE SigButDefNum="+table.Rows[i]["SigButDefNum"].ToString();
						Db.NonQ(command);
					}
					//It's possible we need to loop through several more times depending on how many items shared the same button index. 
					table=Db.GetTable(queryStr);
				} while(table.Rows.Count > 0);
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Messaging buttons with invalid button orders fixed: ")+numFound.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatFieldsDeleteDuplicates(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			string log="";
			//This code is only needed for older db's. New DB's created after 12.2.30 and 12.3.2 shouldn't need this.
			string command=@"DROP TABLE IF EXISTS tempduplicatepatfields";
			Db.NonQ(command);
			string tableName="tempduplicatepatfields"+MiscUtils.CreateRandomAlphaNumericString(8);//max size for a table name in oracle is 30 chars.
			//This query run very fast on a db with no corruption.
			command=@"CREATE TABLE "+tableName+@"
								SELECT *
								FROM patfield
								GROUP BY PatNum,FieldName
								HAVING COUNT(*)>1";
			Db.NonQ(command);
			command=@"SELECT PatNum, LName, FName
								FROM patient 
								WHERE (PatNum IN (SELECT DISTINCT PatNum FROM "+tableName+"))";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients with duplicate field entries found: ")+table.Rows.Count+".\r\n";
				}
			}
			else {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","The following patients had corrupt Patient Fields. Please verify the Patient Fields of these patients:")+"\r\n";
					for(int i=0;i<table.Rows.Count;i++) {
						log+="#"+table.Rows[i]["PatNum"].ToString()+" "+table.Rows[i]["LName"]+", "+table.Rows[i]["FName"]+".\r\n";
					}
					//Without this index the delete process takes too long.
					command="ALTER TABLE "+tableName+" ADD INDEX(PatNum)";
					Db.NonQ(command);
					command="ALTER TABLE "+tableName+" ADD INDEX(FieldName)";
					Db.NonQ(command);
					command="DELETE FROM patfield WHERE ((PatNum, FieldName) IN (SELECT PatNum, FieldName FROM "+tableName+"));";
					Db.NonQ(command);
					command="INSERT INTO patfield SELECT * FROM "+tableName+";";
					Db.NonQ(command);
					log+=Lans.g("FormDatabaseMaintenance","Patients with duplicate field entries removed: ")+table.Rows.Count+".\r\n";
				}
			}
			command=@"DROP TABLE IF EXISTS "+tableName;
			Db.NonQ(command);
			return log;
		}

		public static string PatientBadGuarantor(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT p.PatNum FROM patient p LEFT JOIN patient p2 ON p.Guarantor = p2.PatNum WHERE p2.PatNum IS NULL";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients with invalid Guarantors found: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else {
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE patient SET Guarantor=PatNum WHERE PatNum="+table.Rows[i][0].ToString();
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients with invalid Guarantors fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatientBadGuarantorWithAnotherGuarantor(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT p.PatNum,p2.Guarantor FROM patient p LEFT JOIN patient p2 ON p.Guarantor=p2.PatNum WHERE p2.PatNum!=p2.Guarantor";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients with a guarantor who has another guarantor found: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else {
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE patient SET Guarantor="+table.Rows[i]["Guarantor"].ToString()+" WHERE PatNum="+table.Rows[i]["PatNum"].ToString();
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients with a guarantor who has another guarantor fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatientDeletedWithClinicNumSet(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM patient WHERE ClinicNum!=0 AND PatStatus="+POut.Int((int)PatientStatus.Deleted);
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deleted patients with a clinic still set: ")+numFound.ToString()+"\r\n";
				}
			}
			else {//fix
				command="UPDATE patient SET ClinicNum=0 WHERE ClinicNum!=0 AND PatStatus="+POut.Int((int)PatientStatus.Deleted);
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deleted patients with clinics cleared: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatientsNoClinicSet(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			//same behavior whether check or fix
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				return log;
			}
			//Get the number of patients not assigned to a clinic:
			command=@"SELECT COUNT(*) FROM patient WHERE ClinicNum=0 AND PatStatus!="+POut.Int((int)PatientStatus.Deleted);
			int count=PIn.Int(Db.GetCount(command));
			command=@"SELECT PatNum,LName,FName FROM patient WHERE ClinicNum=0 AND PatStatus!="+POut.Int((int)PatientStatus.Deleted)+" LIMIT 30";
			table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				return log;
			}
			log+=Lans.g("FormDatabaseMaintenance","Patients with no Clinic assigned: ")+count.ToString()+Lans.g("FormDatabaseMaintenance",", including: ");
			for(int i=0;i<table.Rows.Count;i++) {
				//Start a new line and indent every three patients for printing purposes.
				if(i%3==0) {
					log+="\r\n   ";
				}
				log+=table.Rows[i]["PatNum"].ToString()+"-"
					+table.Rows[i]["LName"].ToString()+", "
					+table.Rows[i]["FName"].ToString()+"; ";
			}
			log+="\r\n";
			return log;
		}

		public static string PatientPriProvHidden(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command=@"SELECT ProvNum,Abbr FROM provider WHERE ProvNum IN (SELECT PriProv FROM patient WHERE patient.PriProv=provider.ProvNum) AND IsHidden=1";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Hidden providers with patients: ")+table.Rows.Count+"\r\n";
					DataTable patTable;
					for(int i=0;i<table.Rows.Count;i++) {
						log+="     "+table.Rows[i]["Abbr"].ToString()+": ";
						command=@"SELECT PatNum,LName,FName FROM patient WHERE PriProv=(SELECT ProvNum FROM provider WHERE ProvNum="
							+table.Rows[i]["ProvNum"].ToString()+" AND IsHidden=1) LIMIT 10";
						patTable=Db.GetTable(command);
						for(int j=0;j<patTable.Rows.Count;j++) {
							if(j>0) {
								log+=", ";
							}
							log+=patTable.Rows[j]["PatNum"].ToString()+"-"+patTable.Rows[j]["FName"].ToString()+" "+patTable.Rows[j]["LName"].ToString();
						}
						log+="\r\n";
					}
				}
			}
			else {//Currently no fix.
				//Proposed fix is to add a tool to Lists>Providers and allow quick reassigning of patients and their providers.
			}
			return log;
		}

		public static string PatientPriProvMissing(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command=@"SELECT COUNT(*) FROM patient WHERE PriProv=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patient pri provs not set: ")+numFound+"\r\n";
				}
			}
			else {
				//previous versions of the program just dealt gracefully with missing provnum.
				//From now on, we can assum priprov is not missing, making coding easier.
				command=@"UPDATE patient SET PriProv="+PrefC.GetString(PrefName.PracticeDefaultProv)+" WHERE PriProv=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patient pri provs fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatientUnDeleteWithBalance(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT PatNum FROM patient	WHERE PatStatus=4 "
				+"AND (Bal_0_30 !=0	OR Bal_31_60 !=0 OR Bal_61_90 !=0	OR BalOver90 !=0 OR InsEst !=0 OR BalTotal !=0)";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients found who are marked deleted with non-zero balances: ")+table.Rows.Count+"\r\n";
				}
			}
			else {
				if(table.Rows.Count==0 && verbose) {
					log+=Lans.g("FormDatabaseMaintenance","No balances found for deleted patients.")+"\r\n";
					return log;
				}
				Patient pat;
				Patient old;
				for(int i=0;i<table.Rows.Count;i++) {
					pat=Patients.GetPat(PIn.Long(table.Rows[i][0].ToString()));
					old=pat.Copy();
					pat.LName=pat.LName+Lans.g("FormDatabaseMaintenance","DELETED");
					pat.PatStatus=PatientStatus.Archived;
					Patients.Update(pat,old);
					log+=Lans.g("FormDatabaseMaintenance","Warning!  Patient:")+" "+old.GetNameFL()+" "
				    +Lans.g("FormDatabaseMaintenance","was previously marked as deleted, but was found to have a balance. Patient has been changed to Archive status.  The account will need to be cleared, and the patient deleted again.")+"\r\n";
				}
			}
			return log;
		}

		public static string PatPlanDeleteWithInvalidInsSubNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
			command="SELECT COUNT(*) FROM patplan WHERE InsSubNum NOT IN (SELECT InsSubNum FROM inssub)";
			string countStr=Db.GetCount(command);
				if(countStr!="0" || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Pat plans found with invalid InsSubNums: ")+countStr+"\r\n";
				}
			}
			else {//fix
				command="DELETE FROM patplan WHERE InsSubNum NOT IN (SELECT InsSubNum FROM inssub)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Pat plans with invalid InsSubNums deleted: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatPlanDeleteWithInvalidPatNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM patplan WHERE PatNum NOT IN (SELECT PatNum FROM patient)";
				string countStr=Db.GetCount(command);
				if(countStr!="0" || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Pat plans found with invalid PatNums: ")+countStr+"\r\n";
				}
			}
			else {//fix
				command="DELETE FROM patplan WHERE PatNum NOT IN (SELECT PatNum FROM patient)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Pat plans with invalid PatNums deleted: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatPlanOrdinalDuplicates(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT patient.PatNum,patient.LName,patient.FName,COUNT(*) "
				+"FROM patplan "
				+"INNER JOIN patient ON patient.PatNum=patplan.PatNum "
				+"GROUP BY patplan.PatNum,patplan.Ordinal "
				+"HAVING COUNT(*)>1";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PatPlan duplicate ordinals: ")+table.Rows.Count+"\r\n";
				}
				for(int i=0;i<table.Rows.Count;i++) {
					log+=Lans.g("FormDatabaseMaintenance","PatPlan duplicate ordinals for patient must be manually fixed: ")
						+PIn.String(table.Rows[i]["FName"].ToString())+" "+PIn.String(table.Rows[i]["LName"].ToString())+"\r\n";
				}
			}
			else {
				//No fix. User needs to fix manually.
			}
			return log;
		}

		public static string PatPlanOrdinalZeroToOne(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT PatPlanNum,PatNum FROM patplan WHERE Ordinal=0";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PatPlan ordinals currently zero: ")+table.Rows.Count+"\r\n";
				}
			}
			else {
				int numberFixed=0;
				for(int i=0;i<table.Rows.Count;i++) {
					PatPlan patPlan=PatPlans.GetPatPlan(PIn.Long(table.Rows[i][1].ToString()),0);
					if(patPlan!=null) {//Unlikely but possible if plan gets deleted by a user during this check.
						PatPlans.SetOrdinal(patPlan.PatPlanNum,1);
						numberFixed++;
					}
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PatPlan ordinals changed from 0 to 1: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatPlanOrdinalTwoToOne(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT PatPlanNum,PatNum FROM patplan patplan1 WHERE Ordinal=2 AND NOT EXISTS("
				+"SELECT * FROM patplan patplan2 WHERE patplan1.PatNum=patplan2.PatNum AND patplan2.Ordinal=1)";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PatPlans for secondary found where no primary ins: ")+table.Rows.Count+"\r\n";
				}
			}
			else {
				int numberFixed=0;
				for(int i=0;i<table.Rows.Count;i++) {
					PatPlan patPlan=PatPlans.GetPatPlan(PIn.Int(table.Rows[i][1].ToString()),2);
					if(patPlan!=null) {//Unlikely but possible if plan gets deleted by a user during this check.
						PatPlans.SetOrdinal(patPlan.PatPlanNum,1);
						numberFixed++;
					}
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PatPlan ordinals changed from 2 to 1 if no primary ins: ")+numberFixed+"\r\n";
				}
			}
			return log;
		}

		public static string PaymentDetachMissingDeposit(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM payment "
					+"WHERE DepositNum != 0 " 
					+"AND NOT EXISTS(SELECT * FROM deposit WHERE deposit.DepositNum=payment.DepositNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Payments attached to deposits that no longer exist: ")+numFound+"\r\n";
				}
			}
			else {
				command="UPDATE payment SET DepositNum=0 "
					+"WHERE DepositNum != 0 " 
					+"AND NOT EXISTS(SELECT * FROM deposit WHERE deposit.DepositNum=payment.DepositNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Payments detached from deposits that no longer exist: ")
				  +numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PaymentMissingPaySplit(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM payment "
					+"WHERE PayNum NOT IN (SELECT PayNum FROM paysplit) "//Payments with no split that are
					+"AND ((DepositNum=0) "                              //not attached to a deposit
					+"OR (DepositNum!=0 AND PayAmt=0))";                 //or attached to a deposit with no amount.
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Payments with no attached paysplit: ")+numFound+"\r\n";
				}
			}
			else {
				command="DELETE FROM payment "
					+"WHERE PayNum NOT IN (SELECT PayNum FROM paysplit) "//Payments with no split that are
					+"AND ((DepositNum=0) "                              //not attached to a deposit
					+"OR (DepositNum!=0 AND PayAmt=0))";                 //or attached to a deposit with no amount.
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Payments with no attached paysplit fixed: ")+numberFixed+"\r\n";
				}
			}
			return log;
		}

		public static string PayPlanChargeGuarantorMatch(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			string log="";
			if(isCheck) {
				int numFound=0;
				command="SELECT COUNT(*) FROM payplancharge,payplan "
					+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
					+"AND payplancharge.Guarantor != payplan.Guarantor";
				numFound+=PIn.Int(Db.GetCount(command));
				command="SELECT COUNT(*) FROM payplancharge,payplan "
					+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
					+"AND payplancharge.PatNum != payplan.PatNum";
				numFound+=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PayPlanCharge guarantors and pats not matching payplan guarantors and pats: ")+numFound+"\r\n";
				}
			}
			else {
				//Fix the cases where payplan.Guarantor and payplan.PatNum are not zero. 
				command="UPDATE payplan,payplancharge "
					+"SET payplancharge.Guarantor=payplan.Guarantor "
					+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
					+"AND payplancharge.Guarantor != payplan.Guarantor "
				  +"AND payplan.Guarantor != 0";
				long numFixed=Db.NonQ(command);
				command="UPDATE payplan,payplancharge "
					+"SET payplancharge.PatNum=payplan.PatNum "
					+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
					+"AND payplancharge.PatNum != payplan.PatNum "
				  +"AND payplan.PatNum != 0";
				numFixed+=Db.NonQ(command);
				if(numFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PayPlanCharge guarantors and pats fixed to match payplan: ")+numFixed+"\r\n";
				}
				//No fix yet if payplan.Guarantor or payplan.PatNum are zero but there are good values in PayPlanCharge.
			}
			return log;
		}

		public static string PayPlanChargeProvNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM payplancharge WHERE ProvNum=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Pay plan charge providers missing: ")+numFound+"\r\n";
				}
			}
			else {
				//Take no action.  Use descriptive explanation.
			}
			return log;
		}

		public static string PayPlanSetGuarantorToPatForIns(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM payplan WHERE PlanNum>0 AND Guarantor != PatNum";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PayPlan Guarantors not equal to PatNum where used for insurance tracking: ")+numFound+"\r\n";
				}
			}
			else {
				//Too dangerous to do anything at all.  Just have a very descriptive explanation in the check.
			}
			return log;
		}

		public static string PaySplitAttachedToPayPlan(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT SplitNum,payplan.Guarantor FROM paysplit,payplan "
				+"WHERE paysplit.PayPlanNum=payplan.PayPlanNum "
				+"AND paysplit.PatNum!=payplan.Guarantor";
			DataTable table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Paysplits found with patnum not matching payplan guarantor: ")+table.Rows.Count+"\r\n";
				}
			}
			else {
				//Too dangerous to do anything at all.  Just have a very descriptive explanation in the check.
			}
			return log;
		}

		public static string PaySplitWithInvalidPayNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM paysplit WHERE NOT EXISTS(SELECT * FROM payment WHERE paysplit.PayNum=payment.PayNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Paysplits found with invalid PayNum: ")+numFound+"\r\n";
				}
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					return "";
				}
				command="SELECT *,SUM(SplitAmt) SplitAmt_ FROM paysplit WHERE NOT EXISTS(SELECT * FROM payment WHERE paysplit.PayNum=payment.PayNum) GROUP BY PayNum";
				DataTable table=Db.GetTable(command);
				if(table.Rows.Count>0 || verbose) {
					for(int i=0;i<table.Rows.Count;i++) {
						///<summary>There's only one place in the program where this is called from.  Date is today, so no need to validate the date.</summary>
						Payment payment=new Payment();
						payment.PayType=DefC.Short[(int)DefCat.PaymentTypes][0].DefNum;
						payment.DateEntry=PIn.Date(table.Rows[i]["DateEntry"].ToString());
						payment.PatNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
						payment.PayDate=PIn.Date(table.Rows[i]["DatePay"].ToString());
						payment.PayAmt=PIn.Double(table.Rows[i]["SplitAmt_"].ToString());
						payment.PayNote="Dummy payment. Original payment entry missing from the database.";
						payment.PayNum=PIn.Long(table.Rows[i]["PayNum"].ToString());
						Payments.Insert(payment,true);
					}
					log+=Lans.g("FormDatabaseMaintenance","Paysplits found with invalid PayNum fixed: ")+table.Rows.Count+"\r\n";
				}
			}
			return log;
		}

		public static string PerioMeasureWithInvalidIntTooth(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command=@"SELECT COUNT(*) FROM periomeasure WHERE IntTooth > 32 OR IntTooth < 1";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PerioMeasures found with invalid tooth number: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"DELETE FROM periomeasure WHERE IntTooth > 32 OR IntTooth < 1";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PerioMeasures deleted due to invalid tooth number: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PreferenceDateDepositsStarted(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				DateTime date=PrefC.GetDate(PrefName.DateDepositsStarted);
				if(date<DateTime.Now.AddMonths(-1)) {
					log+=Lans.g("FormDatabaseMaintenance","Deposit start date needs to be reset.")+"\r\n";
				}
				else if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deposit start date checked.")+"\r\n";
				}
			}
			else {
				//If the program locks up when trying to create a deposit slip, it's because someone removed the start date from the deposit edit window. Run this query to get back in.
				DateTime date=PrefC.GetDate(PrefName.DateDepositsStarted);
				if(date<DateTime.Now.AddMonths(-1)) {
					command="UPDATE preference SET ValueString="+POut.Date(DateTime.Today.AddDays(-21))
				  +" WHERE PrefName='DateDepositsStarted'";
					Db.NonQ(command);
					Signalods.SetInvalid(InvalidType.Prefs);
					log+=Lans.g("FormDatabaseMaintenance","Deposit start date reset.")+"\r\n";
				}
				else if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deposit start date checked.")+"\r\n";
				}
			}
			return log;
		}

		public static string PreferencePracticeProv(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT valuestring FROM preference WHERE prefname = 'PracticeDefaultProv'";
			table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="") {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Default practice provider verified.")+"\r\n";
				}
			}
			else{
				log+=Lans.g("FormDatabaseMaintenance","No default provider set.")+"\r\n";
				if(!isCheck) {
					if(DataConnection.DBtype==DatabaseType.Oracle) {
						command="SELECT ProvNum FROM provider WHERE IsHidden=0 ORDER BY itemorder";
					}
					else {//MySQL
						command="SELECT provnum FROM provider WHERE IsHidden=0 ORDER BY itemorder LIMIT 1";
					}
					table=Db.GetTable(command);
					command="UPDATE preference SET valuestring = '"+table.Rows[0][0].ToString()+"' WHERE prefname = 'PracticeDefaultProv'";
					Db.NonQ(command);
					log+="  "+Lans.g("FormDatabaseMaintenance","Fixed.")+"\r\n";
				}
			}
			return log;
		}

		public static string ProcButtonItemsDeleteWithInvalidAutoCode(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command=@"SELECT COUNT(*) FROM procbuttonitem WHERE CodeNum=0 AND NOT EXISTS(
					SELECT * FROM autocode WHERE autocode.AutoCodeNum=procbuttonitem.AutoCodeNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ProcButtonItems found with invalid autocode: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"DELETE FROM procbuttonitem WHERE CodeNum=0 AND NOT EXISTS(
					SELECT * FROM autocode WHERE autocode.AutoCodeNum=procbuttonitem.AutoCodeNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0) {
					Signalods.SetInvalid(InvalidType.ProcButtons);
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ProcButtonItems deleted due to invalid autocode: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ProcedurecodeCategoryNotSet(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM procedurecode WHERE procedurecode.ProcCat=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedure codes with no category found: "+numFound+"\r\n");
				}
			}
			else {//fix
				command="UPDATE procedurecode SET procedurecode.ProcCat="+POut.Long(DefC.Short[(int)DefCat.ProcCodeCats][0].DefNum)+" WHERE procedurecode.ProcCat=0";
				long numberfixed=Db.NonQ(command);
				if(numberfixed>0) {
					Signalods.SetInvalid(InvalidType.ProcCodes);
				}
				if(numberfixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedure codes with no category fixed: "+numberfixed.ToString()+"\r\n");
				}
			}
			return log;
		}

		public static string ProcedurelogAttachedToApptWithProcStatusDeleted(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM procedurelog "
					+"WHERE ProcStatus=6 AND (AptNum!=0 OR PlannedAptNum!=0)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deleted procedures still attached to appointments: ")+numFound+"\r\n";
				}
			}
			else {
				command="UPDATE procedurelog SET AptNum=0,PlannedAptNum=0 "
					+"WHERE ProcStatus=6 "
					+"AND (AptNum!=0 OR PlannedAptNum!=0)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deleted procedures detached from appointments: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ProcedurelogAttachedToWrongAppts(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					command="SELECT COUNT(*) FROM procedurelog p "
						+"WHERE (SELECT COUNT(*) FROM appointment a WHERE p.AptNum=a.AptNum AND p.PatNum!=a.PatNum AND ROWNUM<=1)>0";
				}
				else {
					command="SELECT COUNT(*) FROM appointment,procedurelog "
						+"WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.PatNum != appointment.PatNum";
				}
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures attached to appointments with incorrect patient: ")+numFound+"\r\n";
				}
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					command="UPDATE procedurelog p SET p.AptNum=0 "
						+"WHERE (SELECT COUNT(*) FROM appointment a WHERE p.AptNum=a.AptNum AND p.PatNum!=a.PatNum AND ROWNUM<=1)>0";
				}
				else {
					command="UPDATE appointment,procedurelog SET procedurelog.AptNum=0 "
						+"WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.PatNum != appointment.PatNum";
				}
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures detached from appointments: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ProcedurelogAttachedToWrongApptDate(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					command=@"SELECT COUNT(*) FROM procedurelog p
						WHERE p.ProcStatus=2 AND 
						(SELECT COUNT(*) FROM appointment a WHERE a.AptNum=p.AptNum AND TO_DATE(p.ProcDate)!=TO_DATE(a.AptDateTime) AND ROWNUM<=1)>0";
				}
				else {
					command=@"SELECT COUNT(*) FROM procedurelog,appointment
						WHERE procedurelog.AptNum = appointment.AptNum
						AND DATE(procedurelog.ProcDate) != DATE(appointment.AptDateTime)
						AND procedurelog.ProcStatus = 2";
				}
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures which are attached to appointments with mismatched dates: ")+numFound+"\r\n";
				}
			}
			else {
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					command=@"UPDATE procedurelog p
						SET p.AptNum=0
						WHERE p.ProcStatus=2 AND 
						(SELECT COUNT(*) FROM appointment a WHERE a.AptNum=p.AptNum AND TO_DATE(p.ProcDate)!=TO_DATE(a.AptDateTime) AND ROWNUM<=1)>0";
				}
				else {
					command=@"UPDATE procedurelog,appointment
						SET procedurelog.AptNum=0
						WHERE procedurelog.AptNum = appointment.AptNum
						AND DATE(procedurelog.ProcDate) != DATE(appointment.AptDateTime)
						AND procedurelog.ProcStatus = 2";//only detach completed procs 
				}
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures detached from appointments due to mismatched dates: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ProcedurelogBaseUnitsZero(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				//zero--------------------------------------------------------------------------------------
				command=@"SELECT COUNT(*) FROM procedurelog 
					WHERE baseunits != (SELECT procedurecode.BaseUnits FROM procedurecode WHERE procedurecode.CodeNum=procedurelog.CodeNum)
					AND baseunits = 0";
				//we do not want to change this automatically.  Do not fix these!
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedure BaseUnits are zero and are not matching procedurecode BaseUnits: ")+numFound+"\r\n";
				}
				//not zero----------------------------------------------------------------------------------
				command=@"SELECT COUNT(*)
					FROM procedurelog
					WHERE BaseUnits!=0
					AND (SELECT procedurecode.BaseUnits FROM procedurecode WHERE procedurecode.CodeNum=procedurelog.CodeNum)=0";
				//very safe to change them back to zero.
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedure BaseUnits not zero, but procedurecode BaseUnits are zero: ")+numFound+"\r\n";
				}
			}
			else {
				//first situation: don't fix.
				//second situation:
				//Writing the query this way allows it to work with Oracle.
				command=@"UPDATE procedurelog
					SET BaseUnits=0
					WHERE BaseUnits!=0 
					AND (SELECT procedurecode.BaseUnits FROM procedurecode WHERE procedurecode.CodeNum=procedurelog.CodeNum)=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedure BaseUnits set to zero because procedurecode BaseUnits are zero: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ProcedurelogCodeNumInvalid(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM procedurelog WHERE NOT EXISTS (SELECT * FROM procedurecode WHERE procedurecode.CodeNum=procedurelog.CodeNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures found with invalid CodeNum")+": "+numFound+"\r\n";
				}
			}
			else {
				long badCodeNum=0;
				if(!ProcedureCodes.IsValidCode("~BAD~")) {
					ProcedureCode badCode=new ProcedureCode();
					badCode.ProcCode="~BAD~";
					badCode.Descript="Invalid procedure";
					badCode.AbbrDesc="Invalid procedure";
					badCode.ProcCat=DefC.GetByExactNameNeverZero(DefCat.ProcCodeCats,"Never Used");
					ProcedureCodes.Insert(badCode);
					badCodeNum=badCode.CodeNum;
				}
				else {
					badCodeNum=ProcedureCodes.GetCodeNum("~BAD~");
				}				
				command="UPDATE procedurelog SET CodeNum=" + POut.Long(badCodeNum) + " WHERE NOT EXISTS (SELECT * FROM procedurecode WHERE procedurecode.CodeNum=procedurelog.CodeNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures fixed with invalid CodeNum")+": "+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ProcedurelogLabAttachedToDeletedProc(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM procedurelog "
					+"WHERE ProcStatus=2 AND ProcNumLab IN(SELECT ProcNum FROM procedurelog WHERE ProcStatus=6)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Completed procedure labs attached to deleted procedures: ")+numFound+"\r\n";
				}
			}
			else {
				command="SELECT patient.PatNum,patient.FName,patient.LName FROM procedurelog "
					+"LEFT JOIN patient ON procedurelog.PatNum=patient.PatNum "
					+"WHERE ProcStatus="+POut.Int((int)ProcStat.C)+" AND ProcNumLab IN(SELECT ProcNum FROM procedurelog WHERE ProcStatus="+POut.Int((int)ProcStat.D)+") ";
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command+="GROUP BY patient.PatNum";
				}
				else {//Oracle
					command+="GROUP BY patient.PatNum,patient.FName,patient.LName";
				}
				table=Db.GetTable(command);
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="UPDATE procedurelog plab,procedurelog p "
					+"SET plab.ProcNumLab=0 "
					+"WHERE plab.ProcStatus="+POut.Int((int)ProcStat.C)+" AND plab.ProcNumLab=p.ProcNum AND p.ProcStatus="+POut.Int((int)ProcStat.D);
				}
				else {//Oracle
					command="UPDATE procedurelog plab SET plab.ProcNumLab=0 "
						+"WHERE plab.ProcStatus="+POut.Int((int)ProcStat.C)+" "
						+"AND plab.ProcNumLab IN (SELECT p.ProcNum FROM procedurelog p WHERE p.ProcStatus="+POut.Int((int)ProcStat.D)+") ";
				}
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patients with completed lab procedures detached from deleted procedures: ")+numberFixed.ToString()+"\r\n";
					string patNames="";
					for(int i=0;i<table.Rows.Count;i++) {
						if(i>15) {
							break;
						}
						if(i>0) {
							patNames+=", ";
						}
						patNames+=table.Rows[i]["PatNum"].ToString()+":"+table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
					}
					log+=Lans.g("FormDatabaseMaintenance","Including: ")+patNames+"\r\n";
				}
			}
			return log;
		}

		public static string ProcedurelogProvNumMissing(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM procedurelog WHERE ProvNum=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures with missing provnums found: ")+numFound+"\r\n";
				}
			}
			else {
				//Create a new provider and attach procedures.

				//command="UPDATE procedurelog SET ProvNum="+PrefC.GetString(PrefName.PracticeDefaultProv)+" WHERE ProvNum=0";
				//long numberFixed=Db.NonQ(command);
				//if(numberFixed>0 || verbose) {
				//  log+=Lans.g("FormDatabaseMaintenance","Procedures with missing provnums fixed: ")+numberFixed.ToString()+"\r\n";
				//}
			}
			return log;
		}

		public static string ProcedurelogToothNums(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT procnum,toothnum,patnum FROM procedurelog";
			table=Db.GetTable(command);
			Patient Lim=null;
			string toothNum;
			int numberFixed=0;
			for(int i=0;i<table.Rows.Count;i++) {
				toothNum=table.Rows[i][1].ToString();
				if(toothNum=="")
					continue;
				if(Tooth.IsValidDB(toothNum)) {
					continue;
				}
				if(verbose) {
					Lim=Patients.GetLim(Convert.ToInt32(table.Rows[i][2].ToString()));
				}
				if(string.CompareOrdinal(toothNum,"a")>=0 && string.CompareOrdinal(toothNum,"t")<=0) {
					if(!isCheck) {
						command="UPDATE procedurelog SET ToothNum = '"+toothNum.ToUpper()+"' WHERE ProcNum = "+table.Rows[i][0].ToString();
						Db.NonQ(command);
					}
					if(verbose) {
						log+=Lim.GetNameLF()+" "+toothNum+" - "+toothNum.ToUpper()+"\r\n";
					}
					numberFixed++;
				}
				else {
					if(!isCheck) {
						command="UPDATE procedurelog SET ToothNum = '1' WHERE ProcNum = "+table.Rows[i][0].ToString();
						Db.NonQ(command);
					}
					if(verbose) {
						log+=Lim.GetNameLF()+" "+toothNum+" - 1\r\n";
					}
					numberFixed++;
				}
			}
			if(numberFixed!=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Check for invalid tooth numbers complete.  Records checked: ")
					+table.Rows.Count.ToString()+". "+Lans.g("FormDatabaseMaintenance","Records invalid: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogTpAttachedToClaim(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT procedurelog.ProcNum,claim.ClaimNum,claim.DateService,patient.PatNum,patient.LName,patient.FName,procedurecode.ProcCode "
				+"FROM procedurelog,claim,claimproc,patient,procedurecode "
				+"WHERE procedurelog.ProcNum=claimproc.ProcNum "
				+"AND claim.ClaimNum=claimproc.ClaimNum "
				+"AND claim.PatNum=patient.PatNum "
				+"AND procedurelog.CodeNum=procedurecode.CodeNum "
				+"AND procedurelog.ProcStatus!="+POut.Long((int)ProcStat.C)+" "//procedure not complete
				+"AND (claim.ClaimStatus='W' OR claim.ClaimStatus='S' OR claim.ClaimStatus='R') "//waiting, sent, or received
				+"AND (claim.ClaimType='P' OR claim.ClaimType='S' OR claim.ClaimType='Other')";//pri, sec, or other.  Eliminates preauths.
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures attached to claims, but with status of TP: ")+table.Rows.Count+"\r\n";
					for(int i=0;i<table.Rows.Count;i++) {
						log+=Lans.g("FormDatabaseMaintenance","Patient")
							+" "+table.Rows[i]["FName"].ToString()
							+" "+table.Rows[i]["LName"].ToString()
							+" #"+table.Rows[i]["PatNum"].ToString()
							+", for claim service date "+PIn.Date(table.Rows[i]["DateService"].ToString()).ToShortDateString()
							+", procedure code "+table.Rows[i]["ProcCode"].ToString()+"\r\n";
					}
				}
			}
			else {
				//Detach claimproc(s) from claim.

				//for(int i=0;i<table.Rows.Count;i++) {
				//  command="UPDATE procedurelog SET ProcStatus=2 WHERE ProcNum="+table.Rows[i][0].ToString();
				//  Db.NonQ(command);
				//}
				//int numberFixed=table.Rows.Count;
				//if(numberFixed>0 || verbose) {
				//  log+=Lans.g("FormDatabaseMaintenance","Procedures attached to claims, but with status of TP.  Status changed back to C: ")
				//    +numberFixed.ToString()+"\r\n";
				//}
			}
			return log;
		}

		public static string ProcedurelogTpAttachedToCompleteLabFeesCanada(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(!CultureInfo.CurrentCulture.Name.EndsWith("CA")) {//Canadian. en-CA or fr-CA
				return "";
			}
			string log="";
			command="SELECT pc.ProcCode ProcCode,pclab.ProcCode ProcCodeLab,proc.PatNum,proc.ProcDate "
				+"FROM procedurelog proc "
				+"INNER JOIN procedurecode pc ON pc.CodeNum=proc.CodeNum "
				+"INNER JOIN procedurelog lab ON proc.ProcNum=lab.ProcNumLab AND lab.ProcStatus="+POut.Long((int)ProcStat.C)+" "
				+"INNER JOIN procedurecode pclab ON pclab.CodeNum=lab.CodeNum "
				+"WHERE proc.ProcStatus="+POut.Long((int)ProcStat.TP);
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					for(int i=0;i<table.Rows.Count;i++) {
						log+=Lans.g("FormDatabaseMaintenance","Completed lab fee")+" "+table.Rows[i]["ProcCodeLab"].ToString()+" "
							+Lans.g("FormDatabaseMaintenance","is attached to TP procedure")+" "+table.Rows[i]["ProcCode"].ToString()+" "
							+Lans.g("FormDatabaseMaintenance","on date")+" "+PIn.Date(table.Rows[i]["ProcDate"].ToString()).ToShortDateString()+". "
							+Lans.g("FormDatabaseMaintenance","PatNum: ")+table.Rows[i]["PatNum"].ToString()+" "
							+Lans.g("FormDatabaseMaintenance","Fix manually from within the Chart module.")+"\r\n";
					}
				}
			}
			else {
				//User must fix manually.
			}
			return log;
		}

		public static string ProcedurelogUnitQtyZero(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM procedurelog WHERE UnitQty=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures with UnitQty=0 found: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"UPDATE procedurelog        
					SET UnitQty=1
					WHERE UnitQty=0";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures changed from UnitQty=0 to UnitQty=1: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ProviderHiddenWithClaimPayments(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command=@"SELECT MAX(claimproc.ProcDate) ProcDate,provider.ProvNum
				FROM claimproc,provider
				WHERE claimproc.ProvNum=provider.ProvNum
				AND provider.IsHidden=1
				AND claimproc.InsPayAmt>0
				GROUP BY provider.ProvNum";
			table=Db.GetTable(command);
			if(table.Rows.Count==0) {
			  if(verbose) {
			    log+=Lans.g("FormDatabaseMaintenance","Hidden providers checked for claim payments.")+"\r\n";
			  }
				return log;
			}
			if(isCheck) {
				Provider prov;
				for(int i=0;i<table.Rows.Count;i++) {
					prov=Providers.GetProv(PIn.Long(table.Rows[i][1].ToString()));
					log+=Lans.g("FormDatabaseMaintenance","Warning!  Hidden provider ")+" "+prov.Abbr+" "
						+Lans.g("FormDatabaseMaintenance","has claim payments entered as recently as ")
						+PIn.Date(table.Rows[i][0].ToString()).ToShortDateString()
						+Lans.g("FormDatabaseMaintenance",".  This data will be missing on income reports.")+"\r\n";
				}
			}
			else {
				//No fix implemented.
			}
			return log;
		}

		public static string ProviderWithInvalidFeeSched(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command=@"SELECT COUNT(*) FROM provider WHERE FeeSched NOT IN (SELECT FeeSchedNum FROM feesched)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Providers found with invalid FeeSched: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"UPDATE provider SET FeeSched="+POut.Long(FeeSchedC.ListShort[0].FeeSchedNum)+" "
					+"WHERE FeeSched NOT IN (SELECT FeeSchedNum FROM feesched)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Providers whose FeeSched has been changed: ")
				  +numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string RecallDuplicatesWarn(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			string log="";
			if(RecallTypes.PerioType<1 || RecallTypes.ProphyType<1) {
			  log+=Lans.g("FormDatabaseMaintenance","Warning!  Recall types not set up properly.  There must be at least one of each type: perio and prophy.")+"\r\n";
			  return log;
			}
			command="SELECT FName,LName,COUNT(*) countDups FROM patient LEFT JOIN recall ON recall.PatNum=patient.PatNum "
			  +"AND (recall.RecallTypeNum="+POut.Long(RecallTypes.PerioType)+" "
			  +"OR recall.RecallTypeNum="+POut.Long(RecallTypes.ProphyType)+") "
			  +"GROUP BY FName,LName,patient.PatNum HAVING countDups>1";
			table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Recalls checked for duplicates.")+"\r\n";
				}
				return log;
			}
			if(isCheck) {
				string patNames="";
				for(int i=0;i<table.Rows.Count;i++) {
					if(i>15) {
						break;
					}
					if(i>0) {
						patNames+=", ";
					}
					patNames+=table.Rows[i][0].ToString()+" "+table.Rows[i][1].ToString();
				}
				log+=Lans.g("FormDatabaseMaintenance","Warning!  Number of patients with duplicate recalls: ")+table.Rows.Count.ToString()+".  "
					+Lans.g("FormDatabaseMaintenance","including: ")+patNames+"\r\n";
			}
			else {
				//No fix implemented.
			}
			return log;
		}

		public static string RecallTriggerDeleteBadCodeNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM recalltrigger WHERE NOT EXISTS (SELECT * FROM procedurecode WHERE procedurecode.CodeNum=recalltrigger.CodeNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Recall triggers found with bad codenum: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"DELETE FROM recalltrigger
					WHERE NOT EXISTS (SELECT * FROM procedurecode WHERE procedurecode.CodeNum=recalltrigger.CodeNum)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0) {
					Signalods.SetInvalid(InvalidType.RecallTypes);
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Recall triggers deleted due to bad codenum: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string RefAttachDeleteWithInvalidReferral(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM refattach WHERE ReferralNum NOT IN (SELECT ReferralNum FROM referral)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Ref attachments found with invalid referrals: ")+numFound+"\r\n";
				}
			}
			else {//fix
				command="DELETE FROM refattach WHERE ReferralNum NOT IN (SELECT ReferralNum FROM referral)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Ref attachments with invalid referrals deleted: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string SchedulesDeleteHiddenProviders(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM provider WHERE IsHidden=1 AND ProvNum IN (SELECT ProvNum FROM schedule WHERE SchedDate > "+DbHelper.Now()+" GROUP BY ProvNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Hidden providers found on future schedules: ")+numFound+"\r\n";
				}
			}
			else {//Fix
				command="SELECT ProvNum FROM provider WHERE IsHidden=1 AND ProvNum IN (SELECT ProvNum FROM schedule WHERE SchedDate > "+DbHelper.Now()+" GROUP BY ProvNum)";
				table=Db.GetTable(command);
				List<long> provNums=new List<long>();
				for(int i=0;i<table.Rows.Count;i++) {
					provNums.Add(PIn.Long(table.Rows[i]["ProvNum"].ToString()));
				}
				Providers.RemoveProvsFromFutureSchedule(provNums);//Deletes future schedules for providers.
				if(provNums.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Hidden providers found on future schedules fixed: ")+provNums.Count.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string SchedulesDeleteShort(bool verbose,bool isCheck) {
			//No need to check RemotingRole; no call to db.
			string log="";
			if(isCheck) {
				int numFound=0;
				Schedule[] schedList=Schedules.RefreshAll();
				for(int i=0;i<schedList.Length;i++) {
					if(schedList[i].Status!=SchedStatus.Open) {
						continue;//closed and holiday statuses do not use starttime and stoptime
					}
					if(schedList[i].StopTime-schedList[i].StartTime < new TimeSpan(0,5,0)) {//Schedule items less than five minutes won't show up.
						//But we don't want to count provider notes, employee notes, or pratice notes.
						if(schedList[i].Note=="") {
							numFound++;
						}
					}
				}
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Schedule blocks invalid: ")+numFound+"\r\n";
				}
			}
			else {
				int numberFixed=0;
				Schedule[] schedList=Schedules.RefreshAll();
				for(int i=0;i<schedList.Length;i++) {
					if(schedList[i].Status!=SchedStatus.Open) {
						continue;//closed and holiday statuses do not use starttime and stoptime
					}
					if(schedList[i].StopTime-schedList[i].StartTime < new TimeSpan(0,5,0)) {//Schedule items less than five minutes won't show up. Remove them.
						//But we don't want to remove provider notes, employee notes, or pratice notes.
						if(schedList[i].Note=="") {
							Schedules.Delete(schedList[i]);
							numberFixed++;
						}
					}
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Schedule blocks fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string SchedulesDeleteProvClosed(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM schedule WHERE SchedType=1 AND Status=1";//type=prov,status=closed
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0||verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Schedules found which are causing printing issues: ")+numFound+"\r\n";
				}
			}
			else {
				command="DELETE FROM schedule WHERE SchedType=1 AND Status=1";//type=prov,status=closed
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0||verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Schedules deleted that were causing printing issues: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string SignalInFuture(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command=@"SELECT COUNT(*) FROM signalod WHERE SigDateTime > NOW() OR AckTime > NOW()";
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					string nowDateTime=POut.DateT(MiscData.GetNowDateTime());
					command=@"SELECT COUNT(*) FROM signalod WHERE SigDateTime > "+nowDateTime+" OR AckTime > "+nowDateTime;
				}
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Signalod entries with future time: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"DELETE FROM signalod WHERE SigDateTime > NOW() OR AckTime > NOW()";
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					string nowDateTime=POut.DateT(MiscData.GetNowDateTime());
					command=@"DELETE FROM signalod WHERE SigDateTime > "+nowDateTime+" OR AckTime > "+nowDateTime;
				}
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Signalod entries deleted: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string StatementDateRangeMax(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM statement WHERE DateRangeTo='9999-12-31'";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Statement DateRangeTo max found: ")+numFound+"\r\n";
				}
			}
			else {
				command="UPDATE statement SET DateRangeTo='2200-01-01' WHERE DateRangeTo='9999-12-31'";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Statement DateRangeTo max fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string TaskSubscriptionsInvalid(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM tasksubscription "
					+"WHERE NOT EXISTS(SELECT * FROM tasklist WHERE tasksubscription.TaskListNum=tasklist.TaskListNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Task subscriptions invalid: ")+numFound+"\r\n";
				}
			}
			else {
				command="DELETE FROM tasksubscription "
					+"WHERE NOT EXISTS(SELECT * FROM tasklist WHERE tasksubscription.TaskListNum=tasklist.TaskListNum)"; 
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Task subscriptions deleted: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string TimeCardRuleEmployeeNumInvalid(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM timecardrule "
					+"WHERE timecardrule.EmployeeNum!=0 " //0 is all employees, so it is a 'valid' employee number
					+"AND timecardrule.EmployeeNum NOT IN(SELECT employee.EmployeeNum FROM employee)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Timecard rules found with invalid employee number: ")+numFound+"\r\n";
				}
			}
			else {
				command="UPDATE timecardrule "
					+"SET timecardrule.EmployeeNum=0 "
					+"WHERE timecardrule.EmployeeNum!=0 " //don't set to 0 if already 0
					+"AND timecardrule.EmployeeNum NOT IN(SELECT employee.EmployeeNum FROM employee)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Timecard rules applied to All Employees due to invalid employee number: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		/// <summary>Only one user of a given UserName may be unhidden at a time. Warn the user and instruct them to hide extras.</summary>
		public static string UserodDuplicateUser(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			//check and fix are currently identical
			if(isCheck) {//Give them a warning to hide all but one of these users.
				command="SELECT UserName FROM userod WHERE IsHidden=0 GROUP BY UserName HAVING Count(*)>1;";
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					log+=Lans.g("FormDatabaseMaintenance","Warning! User ")+table.Rows[i]["UserName"].ToString()
						+" has duplicates. Please go to Setup | Security and hide all but one of these users.\r\n";
				}
			}
			else {//Give them a warning to hide all but one of these users.
				command="SELECT UserName FROM userod WHERE IsHidden=0 GROUP BY UserName HAVING Count(*)>1;";
				DataTable table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++){
					log+=Lans.g("FormDatabaseMaintenance","Warning! User ")+table.Rows[i]["UserName"].ToString()
						+" has duplicates. Please go to Setup | Security and hide all but one of these users.\r\n";
				}
			}
			return log;
		}
		
		public static string UserodInvalidClinicNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT Count(*) FROM userod WHERE ClinicNum<>0 AND ClinicNum NOT IN (SELECT ClinicNum FROM clinic)";
				long numFound=PIn.Long(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Users found with invalid ClinicNum: ")+numFound+"\r\n";
				}
			}
			else {//Fix
				command="UPDATE userod SET ClinicNum=0 WHERE ClinicNum<>0 AND ClinicNum NOT IN (SELECT ClinicNum FROM clinic)";
				long numberFixed=Db.NonQ(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Users fixed with invalid ClinicNum: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		/// <summary>userod has an invalid FK to usergroup</summary>
		public static string UserodInvalidUserGroupNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT Count(*) FROM userod WHERE UserGroupNum NOT IN (SELECT UserGroupNum FROM usergroup) ";
				long numFound=PIn.Long(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Users found with invalid UserGroupNum: ")+numFound+"\r\n";
				}
			}
			else {//isFix
				command="SELECT * FROM userod WHERE UserGroupNum NOT IN (SELECT UserGroupNum FROM usergroup) ";
				table=Db.GetTable(command);
				long userNum;
				string userName;
				long userGroupNum;
				long numberFixed=0;
				for(int i=0;i<table.Rows.Count;i++) {//Create a usergroup with the same name as the userod+"Group"
					userNum=PIn.Long(table.Rows[i]["UserNum"].ToString());
					userName=PIn.String(table.Rows[i]["UserName"].ToString());
					command="INSERT INTO usergroup (Description) VALUES('"+POut.String(userName+" Group")+"')";
					userGroupNum=Db.NonQ(command,true);
					command="UPDATE userod SET UserGroupNum="+POut.Long(userGroupNum)+" WHERE UserNum="+POut.Long(userNum);
					Db.NonQ(command);
					numberFixed++;
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Users fixed with invalid UserGroupNum: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}






		public static List<string> GetDatabaseNames(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<string>>(MethodBase.GetCurrentMethod());
			}
			List<string> retVal=new List<string>();
			command="SHOW DATABASES";
			//if this next step fails, table will simply have 0 rows
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		///<summary>Will return empty string if no problems.</summary>
		public static string GetDuplicateClaimProcs(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string retVal="";
			command=@"SELECT LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber, COUNT(*) cnt
FROM claimproc
LEFT JOIN patient ON patient.PatNum=claimproc.PatNum
WHERE ClaimNum > 0
AND ProcNum>0
AND Status!=4/*exclude supplemental*/
GROUP BY LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber 
HAVING cnt>1";
			table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return "";
			}
			retVal+="Duplicate claim payments found:\r\n";
			DateTime date;
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){//check for duplicate rows.  We only want to report each claim once.
					if(table.Rows[i]["ClaimNum"].ToString()==table.Rows[i-1]["ClaimNum"].ToString()){
						continue;
					}
				}
				date=PIn.Date(table.Rows[i]["ProcDate"].ToString());
				retVal+=table.Rows[i]["LName"].ToString()+", "
					+table.Rows[i]["FName"].ToString()+" "
					+"("+table.Rows[i]["PatNum"].ToString()+"), "
					+date.ToShortDateString()+"\r\n";
			}
			retVal+="\r\n";
			return retVal;
		}

		///<summary>Will return empty string if no problems.</summary>
		public static string GetDuplicateSupplementalPayments(){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string retVal="";
			command=@"SELECT LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber, COUNT(*) cnt
FROM claimproc
LEFT JOIN patient ON patient.PatNum=claimproc.PatNum
WHERE ClaimNum > 0
AND ProcNum>0
AND Status=4/*only supplemental*/
GROUP BY LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber
HAVING cnt>1";
			table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return "";
			}
			retVal+="Duplicate supplemental payments found (may be false positives):\r\n";
			DateTime date;
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					if(table.Rows[i]["ClaimNum"].ToString()==table.Rows[i-1]["ClaimNum"].ToString()){
						continue;
					}
				}
				date=PIn.Date(table.Rows[i]["ProcDate"].ToString());
				retVal+=table.Rows[i]["LName"].ToString()+", "
					+table.Rows[i]["FName"].ToString()+" "
					+"("+table.Rows[i]["PatNum"].ToString()+"), "
					+date.ToShortDateString()+"\r\n";
			}
			retVal+="\r\n";
			return retVal;
		}

		///<summary></summary>
		public static string GetMissingClaimProcs(string olddb) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),olddb);
			}
			string retVal="";
			command="SELECT LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber "
				+"FROM "+olddb+".claimproc "
				+"LEFT JOIN "+olddb+".patient ON "+olddb+".patient.PatNum="+olddb+".claimproc.PatNum "
				+"WHERE NOT EXISTS(SELECT * FROM claimproc WHERE claimproc.ClaimProcNum="+olddb+".claimproc.ClaimProcNum) "
				+"AND ClaimNum > 0 AND ProcNum>0";
			table=Db.GetTable(command);
			double insPayAmt;
			double feeBilled;
			int count=0;
			for(int i=0;i<table.Rows.Count;i++){
				insPayAmt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
				feeBilled=PIn.Double(table.Rows[i]["FeeBilled"].ToString());
				command="SELECT COUNT(*) FROM "+olddb+".claimproc "
					+"WHERE ClaimNum= "+table.Rows[i]["ClaimNum"].ToString()+" "
					+"AND ProcNum= "+table.Rows[i]["ProcNum"].ToString()+" "
					+"AND Status= "+table.Rows[i]["Status"].ToString()+" "
					+"AND InsPayAmt= '"+POut.Double(insPayAmt)+"' "
					+"AND FeeBilled= '"+POut.Double(feeBilled)+"' "
					+"AND LineNumber= "+table.Rows[i]["LineNumber"].ToString();
				string result=Db.GetCount(command);
				if(result!="1"){//only include in result if there are duplicates in old db.
					count++;
				}
			}
			command="SELECT ClaimPaymentNum "
				+"FROM "+olddb+".claimpayment "
				+"WHERE NOT EXISTS(SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum="+olddb+".claimpayment.ClaimPaymentNum) ";
			DataTable table2=Db.GetTable(command);
			if(count==0 && table2.Rows.Count==0) {
				return "";
			}
			retVal+="Missing claim payments found: "+count.ToString()+"\r\n";
			retVal+="Missing claim checks found (probably false positives): "+table2.Rows.Count.ToString()+"\r\n";
			return retVal;
		}

		//public static bool DatabaseIsOlderThanMarchSeventeenth(string olddb){
		//  if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
		//    return Meth.GetBool(MethodBase.GetCurrentMethod(),olddb);
		//  }
		//  command="SELECT COUNT(*) FROM "+olddb+".claimproc WHERE DateEntry > '2010-03-16'";
		//  if(Db.GetCount(command)=="0"){
		//    return true;
		//  }
		//  return false;
		//}

		/// <summary></summary>
		public static string FixClaimProcDeleteDuplicates() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string log="";
			//command=@"SELECT LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber, COUNT(*) cnt
			//	FROM claimproc
			//	LEFT JOIN patient ON patient.PatNum=claimproc.PatNum
			//	WHERE ClaimNum > 0
			//	AND ProcNum>0
			//	AND Status!=4/*exclude supplemental*/
			//	GROUP BY ClaimNum,ProcNum,Status,InsPayAmt,FeeBilled,LineNumber
			//	HAVING cnt>1";
			//table=Db.GetTable(command);
			//long numberFixed=0;
			//double insPayAmt;
			//double feeBilled;
			//for(int i=0;i<table.Rows.Count;i++) {
			//  insPayAmt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
			//  feeBilled=PIn.Double(table.Rows[i]["FeeBilled"].ToString());
			//  command="DELETE FROM claimproc "
			//    +"WHERE ClaimNum= "+table.Rows[i]["ClaimNum"].ToString()+" "
			//    +"AND ProcNum= "+table.Rows[i]["ProcNum"].ToString()+" "
			//    +"AND Status= "+table.Rows[i]["Status"].ToString()+" "
			//    +"AND InsPayAmt= '"+POut.Double(insPayAmt)+"' "
			//    +"AND FeeBilled= '"+POut.Double(feeBilled)+"' "
			//    +"AND LineNumber= "+table.Rows[i]["LineNumber"].ToString()+" "
			//    +"AND ClaimProcNum != "+table.Rows[i]["ClaimProcNum"].ToString();
			//  numberFixed+=Db.NonQ(command);
			//}
			//log+="Claimprocs deleted due duplicate entries: "+numberFixed.ToString()+".\r\n";
			return log;
		}

		/// <summary></summary>
		public static string FixMissingClaimProcs(string olddb) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string log="";
			//command="SELECT LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber "
			//  +"FROM "+olddb+".claimproc "
			//  +"LEFT JOIN "+olddb+".patient ON "+olddb+".patient.PatNum="+olddb+".claimproc.PatNum "
			//  +"WHERE NOT EXISTS(SELECT * FROM claimproc WHERE claimproc.ClaimProcNum="+olddb+".claimproc.ClaimProcNum) "
			//  +"AND ClaimNum > 0 AND ProcNum>0";
			//table=Db.GetTable(command);
			//long numberFixed=0;
			//command="SELECT ValueString FROM "+olddb+".preference WHERE PrefName = 'DataBaseVersion'";
			//string oldVersString=Db.GetScalar(command);
			//Version oldVersion=new Version(oldVersString);
			//if(oldVersion < new Version("6.7.1.0")) {
			//  return "Version of old database is too old to use with the automated tool: "+oldVersString;
			//}
			//double insPayAmt;
			//double feeBilled;
			//for(int i=0;i<table.Rows.Count;i++) {
			//  insPayAmt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
			//  feeBilled=PIn.Double(table.Rows[i]["FeeBilled"].ToString());
			//  command="SELECT COUNT(*) FROM "+olddb+".claimproc "
			//    +"WHERE ClaimNum= "+table.Rows[i]["ClaimNum"].ToString()+" "
			//    +"AND ProcNum= "+table.Rows[i]["ProcNum"].ToString()+" "
			//    +"AND Status= "+table.Rows[i]["Status"].ToString()+" "
			//    +"AND InsPayAmt= '"+POut.Double(insPayAmt)+"' "
			//    +"AND FeeBilled= '"+POut.Double(feeBilled)+"' "
			//    +"AND LineNumber= "+table.Rows[i]["LineNumber"].ToString();
			//  string result=Db.GetCount(command);
			//  if(result=="1"){//only include in result if there are duplicates in old db.
			//    continue;
			//  }
			//  command="INSERT INTO claimproc SELECT *";
			//  if(oldVersion < new Version("6.8.1.0")) {
			//    command+=",-1,-1,0";
			//  }
			//  else if(oldVersion < new Version("6.9.1.0")) {
			//    command+=",0";
			//  }
			//  command+=" FROM "+olddb+".claimproc "
			//    +"WHERE "+olddb+".claimproc.ClaimProcNum="+table.Rows[i]["ClaimProcNum"].ToString();
			//  numberFixed+=Db.NonQ(command);
			//}
			//command="SELECT ClaimPaymentNum "
			//  +"FROM "+olddb+".claimpayment "
			//  +"WHERE NOT EXISTS(SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum="+olddb+".claimpayment.ClaimPaymentNum) ";
			//table=Db.GetTable(command);
			//long numberFixed2=0;
			//for(int i=0;i<table.Rows.Count;i++) {
			//  command="INSERT INTO claimpayment SELECT * FROM "+olddb+".claimpayment "
			//    +"WHERE "+olddb+".claimpayment.ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
			//  numberFixed2+=Db.NonQ(command);
			//}
			//log+="Missing claimprocs added back: "+numberFixed.ToString()+".\r\n";
			//log+="Missing claimpayments added back: "+numberFixed2.ToString()+".\r\n";
			return log;
		}

		///<summary>Removes unsupported unicode characters from appointment.procdescript</summary>
		public static void FixSpecialCharacters() {
			string command="SELECT * FROM appointment WHERE (ProcDescript REGEXP '[^[:alnum:]^[:space:]^[:punct:]]+') OR (Note REGEXP '[^[:alnum:]^[:space:]^[:punct:]]+')";
			List<Appointment> apts=OpenDentBusiness.Crud.AppointmentCrud.SelectMany(command);
			List<char> specialCharsFound=new List<char>();
			int specialCharCount=0;
			int intC=0;
			if(apts.Count!=0) {
				foreach(Appointment apt in apts) {
					foreach(char c in apt.Note) {
						intC=(int)c;
						if((intC<126&&intC>31)//31 - 126 are all safe.
							||intC==9			//"Horizontal Tabulation"
							||intC==10		//Line Feed
							||intC==13) {	//carriage return
							continue;
						}
						specialCharCount++;
						if(specialCharsFound.Contains(c)) {
							continue;
						}
						specialCharsFound.Add(c);
					}
					foreach(char c in apt.ProcDescript) {//search every character in ProcDescript
						intC=(int)c;
						if((intC<126&&intC>31)//31 - 126 are all safe.
							||intC==9			//"Horizontal Tabulation"
							||intC==10		//Line Feed
							||intC==13) {	//carriage return
							continue;
						}
						specialCharCount++;
						if(specialCharsFound.Contains(c)) {
							continue;
						}
						specialCharsFound.Add(c);
					}
				}
				foreach(char c in specialCharsFound) {
					command="UPDATE appointment SET Note = REPLACE(Note,'"+POut.String(c.ToString())+"',''), ProcDescript = REPLACE(ProcDescript,'"+POut.String(c.ToString())+"','')";
					Db.NonQ(command);
				}
			}
			return;
		}

		///<summary>Return values look like 'MyISAM' or 'InnoDB'. Will return empty string on error.</summary>
		public static string GetStorageEngineDefaultName() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command="SHOW GLOBAL VARIABLES LIKE 'storage_engine'";
			DataTable dtEngine=Db.GetTable(command);
			if(dtEngine.Rows.Count>0) {
				try {
					return PIn.String(dtEngine.Rows[0]["Value"].ToString());
				}
				catch {
				}
			}
			return "";
		}

		///<summary>Gets the names of tables in InnoDB format, comma delimited (excluding the 'phone' table).  Returns empty string if none.</summary>
		public static string GetInnodbTableNames() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			//Using COUNT(*) with INFORMATION_SCHEMA is buggy.  It can return "1" even if no results.
			string command="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.tables "
				+"WHERE TABLE_SCHEMA='"+POut.String(DataConnection.GetDatabaseName())+"' "
				+"AND TABLE_NAME!='phone' "//this table is used internally at OD HQ, and is always innodb.
				+"AND ENGINE NOT LIKE 'MyISAM'";
			DataTable table=Db.GetTable(command);
			string tableNames="";
			for(int i=0;i<table.Rows.Count;i++) {
				if(tableNames!="") {
					tableNames+=",";
				}
				tableNames+=PIn.String(table.Rows[i][0].ToString());
			}
			return tableNames;
		}

		///<summary>Gets the number of tables in MyISAM format.</summary>
		public static int GetMyisamTableCount() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.tables "
				+"WHERE TABLE_SCHEMA='"+POut.String(DataConnection.GetDatabaseName())+"' "
				+"AND ENGINE LIKE 'MyISAM'";
			return Db.GetTable(command).Rows.Count;
		}

		///<summary>Returns true if the conversion was successfull or no conversion was necessary. The goal is to convert InnoDB tables (excluding the 'phone' table) to MyISAM format when there are a mixture of InnoDB and MyISAM tables but no conversion will be performed when all of the tables are already in the same format.</summary>
		public static bool ConvertTablesToMyisam() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod());
			}
			if(DataConnection.DBtype==DatabaseType.Oracle) { //Does not apply to Oracle.
				return true;
			}
			command="SELECT TABLE_NAME,ENGINE FROM INFORMATION_SCHEMA.tables "
				+"WHERE TABLE_SCHEMA='"+POut.String(DataConnection.GetDatabaseName())+"' "
				+"AND TABLE_NAME!='phone'";//this table is used internally at OD HQ, and is always innodb.
			DataTable dtTableTypes=Db.GetTable(command);
			int numInnodb=0;//Or possibly some other format.
			int numMyisam=0;
			for(int i=0;i<dtTableTypes.Rows.Count;i++) {
				if(PIn.String(dtTableTypes.Rows[i]["ENGINE"].ToString()).ToUpper()=="MYISAM") {
					numMyisam++;
				}
				else {
					numInnodb++;
				}
			}
			if(numInnodb>0 && numMyisam>0) {//Fix tables by converting them to MyISAM when there is a mixture of different table types.
				for(int i=0;i<dtTableTypes.Rows.Count;i++) {
					if(PIn.String(dtTableTypes.Rows[i]["ENGINE"].ToString()).ToUpper()=="MYISAM") {
						continue;
					}
					string tableName=PIn.String(dtTableTypes.Rows[i]["TABLE_NAME"].ToString());
					command="ALTER TABLE "+POut.String(tableName)+" ENGINE='MyISAM'";
					try {
						Db.NonQ(command);
					}
					catch {
						return false;
					}
				}
				command="SELECT TABLE_NAME FROM INFORMATION_SCHEMA.tables "
					+"WHERE TABLE_SCHEMA='"+POut.String(DataConnection.GetDatabaseName())+"' "
					+"AND TABLE_NAME!='phone' "
					+"AND ENGINE NOT LIKE 'MyISAM'";
				if(Db.GetTable(command).Rows.Count!=0) { //If any tables are still InnoDB.
					return false;
				}
			}
			return true;
		}





	}
}
