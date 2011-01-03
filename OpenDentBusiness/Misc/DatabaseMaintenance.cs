using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

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
								log+=tableResults.Rows[t][j].ToString()+",";
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
					log+=Lans.g("FormDatabaseMaintenance","Corrupted files probably fixed.  Look closely at the log.  Also, run again to be sure they were really fixed.")+"\r\n";
				}
			}
			return log;
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
				  ,"SELECT COUNT(*) FROM recall WHERE DisableUntilDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM schedule WHERE SchedDate='0000-00-00'"
				  ,"SELECT COUNT(*) FROM signal WHERE DateViewing='0000-00-00'"
				  ,"SELECT COUNT(*) FROM signal WHERE SigDateTime LIKE '0000-00-00%'"
				  ,"SELECT COUNT(*) FROM signal WHERE AckTime LIKE '0000-00-00%'"
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
				  ,"UPDATE recall SET DisableUntilDate='0001-01-01' WHERE DisableUntilDate='0000-00-00'"
				  ,"UPDATE schedule SET SchedDate='0001-01-01' WHERE SchedDate='0000-00-00'"
				  ,"UPDATE signal SET DateViewing='0001-01-01' WHERE DateViewing='0000-00-00'"
				  ,"UPDATE signal SET SigDateTime='0001-01-01 00:00:00' WHERE SigDateTime LIKE '0000-00-00%'"
				  ,"UPDATE signal SET AckTime='0001-01-01 00:00:00' WHERE AckTime LIKE '0000-00-00%'"
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
			//int numberFixed=0;
			//for(int i=0;i<decimalCols.Length;i+=2) {
			//  string tablename=decimalCols[i];
			//  string colname=decimalCols[i+1];
			//  string command="UPDATE "+tablename+" SET "+colname+"=ROUND("+colname+","+decimalPlacessToRoundTo
			//    +") WHERE "+colname+"!=ROUND("+colname+","+decimalPlacessToRoundTo+")";
			//  numberFixed+=Db.NonQ32(command);
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
					command="UPDATE procedurelog P, appointment A SET P.AptNum = 0 WHERE P.AptNum = A.AptNum AND A.Pattern = ''";
					Db.NonQ(command);
					command="UPDATE procedurelog P, appointment A SET P.PlannedAptNum = 0 WHERE P.PlannedAptNum = A.AptNum AND A.Pattern = ''";
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
				  +"AND YEAR(AptDateTime)<1880 "//scheduled but no date 
				  +"AND NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.AptNum=appointment.AptNum)";//and no procs
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound!=0 || verbose) {
				  log+=Lans.g("FormDatabaseMaintenance","Appointments found with no date and no procs: ")+numFound+"\r\n";
				}
			}
			else{
				command="DELETE FROM appointment "
				  +"WHERE AptStatus=1 "//scheduled 
				  +"AND YEAR(AptDateTime)<1880 "//scheduled but no date 
				  +"AND NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.AptNum=appointment.AptNum)";//and no procs
				int numberFixed=Db.NonQ32(command);
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
			int numberFixed=PIn.Int(Db.GetCount(command));
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Appointments found abandoned: ")+numberFixed.ToString()+"\r\n";
				}
			}
			else{//Fix is safe because we are not deleting data, we are just attaching abandoned appointments to a dummy patient.
				if(numberFixed!=0) {
					Patient dummyPatient=new Patient();
					dummyPatient.FName="MISSING";
					dummyPatient.LName="PATIENT";
					dummyPatient.Birthdate=DateTime.MinValue;
					dummyPatient.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
					dummyPatient.PatStatus=PatientStatus.Archived;
					dummyPatient.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
					long dummyPatNum=Patients.Insert(dummyPatient,false);
					Patient oldDummyPatient=dummyPatient.Copy();
					dummyPatient.Guarantor=dummyPatNum;
					Patients.Update(dummyPatient,oldDummyPatient);
					command="UPDATE appointment SET PatNum="+POut.Long(dummyPatNum)+" WHERE PatNum NOT IN(SELECT PatNum FROM patient)";
					numberFixed=Db.NonQ32(command);
				}
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Appointments altered due to no patient: ")+numberFixed.ToString()+"\r\n";
				}
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0) {
					Signals.SetInvalid(InvalidType.AutoCodes);
				}
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Autocodes deleted due to no items: ")+numberFixed.ToString()+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed!=0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claims deleted due to no procedures: ")+numberFixed.ToString()+"\r\n";
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
			string log="";
			//because of the way this is grouped, it will just get one of many patients for each
			command=@"SELECT claimproc.ClaimPaymentNum,ROUND(SUM(InsPayAmt),2) "+"\"_sumpay\",ROUND(CheckAmt,2) \"_checkamt\""+@"
					FROM claimpayment,claimproc
					WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum
					GROUP BY claimproc.ClaimPaymentNum,CheckAmt
					HAVING "+"\"_sumpay\"!=\"_checkamt\"";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Claim payment sums found incorrect: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				for(int i=0;i<table.Rows.Count;i++) {
					command="UPDATE claimpayment SET CheckAmt='"+POut.Double(PIn.Double(table.Rows[i]["_sumpay"].ToString()))+"' "
				    +"WHERE ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
					Db.NonQ(command);
				}
				int numberFixed=table.Rows.Count;
				if(numberFixed>0||verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claim payment sums fixed: ")+numberFixed.ToString()+"\r\n";
				}
			}
			//now deposits which were affected by the changes above--------------------------------------------------
			command=@"SELECT DepositNum,deposit.Amount,DateDeposit,
				IFNULL((SELECT SUM(CheckAmt) FROM claimpayment WHERE claimpayment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0)
				+IFNULL((SELECT SUM(PayAmt) FROM payment WHERE payment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0) "+"\"_sum\""+@"
				FROM deposit
				HAVING ROUND("+"\"_sum\",2) != ROUND(deposit.Amount,2)";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deposit sums found incorrect: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
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
				}
			}
			return log;
		}

		public static string ClaimPaymentDeleteWithNoSplits(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command="SELECT COUNT(*) FROM claimpayment WHERE NOT EXISTS("
				  +"SELECT * FROM claimproc WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
				  log+=Lans.g("FormDatabaseMaintenance","Claim payments with no splits found: ")+numFound.ToString()+"\r\n";
				}
			}
			else{
				//Because it would change the sum on a deposit slip, can't easily delete these if attached to a deposit.
				//Only delete claimpayments that are not attached to deposit slips.  Others, no action.

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
				int numberFixed=Db.NonQ32(command);
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
			else{
				//Inform onlyInfo
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Claimprocs deleted due to invalid ClaimNum: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string ClaimProcWithInvalidPlanNum(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command=@"SELECT ClaimProcNum,PatNum FROM claimproc
				LEFT JOIN insplan ON claimproc.PlanNum=insplan.PlanNum
				WHERE insplan.PlanNum IS NULL";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs found with invalid PlanNum: ")+table.Rows.Count.ToString()+"\r\n";
				}
			}
			else{
				//Take no action.  Use descriptive explanation.
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Estimates deleted for procedures that no longer exist: ")+numberFixed.ToString()+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProc estimates with InsPaidAmt > 0 fixed: ")+numberFixed.ToString()+"\r\n";
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
				command="SELECT COUNT(*) FROM claimproc WHERE ProvNum=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with missing provnums found: ")+numFound+"\r\n";
				}
			}
			else{
				//create a dummy provider (using helper function).
				//change provnum to the dummy prov

				//command="UPDATE claimproc SET ProvNum="+PrefC.GetString(PrefName.PracticeDefaultProv)+" WHERE ProvNum=0";
				//int numberFixed=Db.NonQ32(command);
				//if(numberFixed>0 || verbose) {
				//  log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with missing provnums fixed: ")+numberFixed.ToString()+"\r\n";
				//}
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
					AND claimproc.Status=0";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with status not matching claim found: ")+numFound+"\r\n";
				}
			}
			else{
				//Take no action.  Use descriptive explanation.
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
				int numberFixed=Db.NonQ32(command);
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
			}
			return log;
		}

		public static string ClockEventInFuture(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck){
				command=@"SELECT COUNT(*) FROM clockevent WHERE TimeDisplayed1 > NOW()+INTERVAL 15 MINUTE";
				int numFound=PIn.Int(Db.GetCount(command));
				command=@"SELECT COUNT(*) FROM clockevent WHERE TimeDisplayed2 > NOW()+INTERVAL 15 MINUTE";
				numFound+=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Timecard entries invalid: ")+numFound+"\r\n";
				}
			}
			else{
				command=@"UPDATE clockevent SET TimeDisplayed1=NOW() WHERE TimeDisplayed1 > NOW()+INTERVAL 15 MINUTE";
				int numberFixed=Db.NonQ32(command);
				command=@"UPDATE clockevent SET TimeDisplayed2=NOW() WHERE TimeDisplayed2 > NOW()+INTERVAL 15 MINUTE";
				numberFixed+=Db.NonQ32(command);
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
		
		public static string InsPlanCheckNoCarrier(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			//Gets a list of insurance plans that do not have a carrier attached. The list should be blank. If not, then you need to go to the plan listed and add a carrier. Missing carriers will cause the send claims function to give an error.
			command="SELECT PlanNum FROM insplan WHERE CarrierNum=0";
			table=Db.GetTable(command);
			if(isCheck){
				if(table.Rows.Count>0 || verbose){
					log+=Lans.g("FormDatabaseMaintenance","Ins plans with carrier missing found: ")+table.Rows.Count+"\r\n";
				}
			}
			else{
				if(table.Rows.Count>0) {
					Carrier carrier=new Carrier();
					carrier.CarrierName="unknown";
					Carriers.Insert(carrier);
					command="UPDATE insplan SET CarrierNum="+POut.Long(carrier.CarrierNum)
				    +" WHERE CarrierNum=0";
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
			//not backported earlier than 7.6
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM appointment WHERE appointment.InsPlan1 != 0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=appointment.InsPlan1)";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid appointment InsSubNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM appointment WHERE appointment.InsPlan2 != 0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=appointment.InsPlan2)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid appointment InsSubNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM benefit WHERE NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=benefit.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid benefit InsSubNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM claim WHERE NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=claim.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid claim InsSubNums: ")+numFound+"\r\n";
				}
				//This one can cause an error when trying to open the send claims window:
				command="SELECT COUNT(*) FROM claim WHERE claim.PlanNum2 !=0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=claim.PlanNum2)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid claim InsSubNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM claimproc WHERE NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=claimproc.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid claimproc InsSubNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM etrans WHERE NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=etrans.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid etrans InsSubNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM patplan WHERE NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=patplan.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid patplan InsSubNums: ")+numFound+"\r\n";
				}
				command="SELECT COUNT(*) FROM payplan WHERE PlanNum != 0 AND NOT EXISTS(SELECT * FROM insplan WHERE insplan.PlanNum=payplan.PlanNum)";
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Invalid payplan InsSubNums: ")+numFound+"\r\n";
				}
			}
			else {
				//appointment.InsPlan1
				//command="UPDDATE appointment SET appointment.PlanNum=(SELECT inssub.PlanNum FROM inssub WHERE appointment.InsSubNum=inssub.InsSubNum)
				//appointment.InsPlan2
				//benefit.PlanNum
				//claim.PlanNum
				//claim.PlanNum2
				//claimproc.PlanNum
				//etrans.PlanNum
				//patplan.PlanNum
				//payplan.PlanNum
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Insplan claimforms set if missing: ")+numberFixed.ToString()+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Medications deleted because no defition exists for them: ")+numberFixed.ToString()+"\r\n";
				}
			}
			return log;
		}

		public static string PatFieldsDeleteDuplicates(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			string command=@"DROP TABLE IF EXISTS tempduplicatepatfields";
			Db.NonQ(command);
			//This query run very fast on a db with no corruption.
			command=@"CREATE TABLE tempduplicatepatfields
				SELECT DISTINCT PatNum
				FROM patfield
				GROUP BY PatNum,FieldName
				HAVING COUNT(*)>1";
			Db.NonQ(command);
			command=@"SELECT p.PatNum, p.LName, p.FName
				FROM patient p, tempduplicatepatfields t
				WHERE p.PatNum=t.PatNum";
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Patient Field duplicate entries found: ")+table.Rows.Count.ToString()+".\r\n";
				}
			}
			else {
				if(table.Rows.Count==0) {
					if(verbose) {
						log+=Lans.g("FormDatabaseMaintenance","Patient Field duplicate entries deleted")+": 0\r\n";
					}
					command=@"DROP TABLE IF EXISTS tempduplicatepatfields";
					Db.NonQ(command);
					return log;
				}
				else {
					log+=Lans.g("FormDatabaseMaintenance","The following patients had corrupted 'Patient Fields'.  Compare the Patient Fields of each patient in the list against a backup from before the most recent version upgrade.")+"\r\n";
				}
				for(int i=0;i<table.Rows.Count;i++) {
					log+="#"+table.Rows[i]["PatNum"].ToString()+" "+table.Rows[i]["LName"]+", "+table.Rows[i]["FName"]+".\r\n";
				}
				//Without this index the delete process takes too long.
				command=@"ALTER TABLE tempduplicatepatfields 
					ADD INDEX idx_temppatfield_patnum(PatNum)";
				command="DELETE FROM patfield "
					+"WHERE PatNum IN(SELECT PatNum FROM tempduplicatepatfields)";
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0) {
					log+=Lans.g("FormDatabaseMaintenance","Patient Field duplicate entries deleted: ")+numberFixed.ToString()+".\r\n";
				}
				command=@"DROP TABLE IF EXISTS tempduplicatepatfields";
				Db.NonQ(command);
			}
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
				int numberFixed=Db.NonQ32(command);
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
					PatPlan patPlan=PatPlans.GetPatPlan(PIn.Int(table.Rows[i][1].ToString()),0);
					if(patPlan!=null) {//Unlikely but possible if plan gets deleted by a user during this check.
						PatPlans.SetOrdinal(patPlan.PatPlanNum,1);
						numberFixed++;
					}
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","PatPlan ordinals changed from 0 to 1: ")+numberFixed+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Payments detached from deposits that no longer exist: ")
				  +numberFixed.ToString()+"\r\n";
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
				//Take no action.  Use descriptive explanation.
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
				//create a payment
				//attach one paysplit to the payment
				//repeat as needed.


				//command="DELETE FROM paysplit WHERE NOT EXISTS(SELECT * FROM payment WHERE paysplit.PayNum=payment.PayNum)";
				//int numberFixed=Db.NonQ32(command);
				//if(numberFixed>0 || verbose) {
				//  log+=Lans.g("FormDatabaseMaintenance","Paysplits deleted due to invalid PayNum: ")+numberFixed.ToString()+"\r\n";
				//}
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
					Signals.SetInvalid(InvalidType.Prefs);
					log+=Lans.g("FormDatabaseMaintenance","Deposit start date reset.")+"\r\n";
				}
				else if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Deposit start date checked.")+"\r\n";
				}
			}
			return log;
		}

		public static string PreferencePracticeBillingType(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			command="SELECT valuestring FROM preference WHERE prefname = 'PracticeDefaultBillType'";
			table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="") {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Default practice billing type verified.")+"\r\n";
				}
			}
			else {
				log+=Lans.g("FormDatabaseMaintenance","No default billing type set.");
				if(!isCheck) {
					command="SELECT defnum FROM definition WHERE category = 4 AND ishidden = 0 ORDER BY itemorder";
					table=Db.GetTable(command);
					command="UPDATE preference SET valuestring='"+table.Rows[0][0].ToString()+"' WHERE prefname='PracticeDefaultBillType'";
					Db.NonQ(command);
					log+="  "+Lans.g("FormDatabaseMaintenance","Fixed.")+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0) {
					Signals.SetInvalid(InvalidType.ProcButtons);
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","ProcButtonItems deleted due to invalid autocode: ")+numberFixed.ToString()+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
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
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			string log="";
			if(isCheck) {
				command="SELECT COUNT(*) FROM appointment,procedurelog "
					+"WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.PatNum != appointment.PatNum";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures attached to appointments with incorrect patient: ")+numFound+"\r\n";
				}
			}
			else {
				command="UPDATE appointment,procedurelog SET procedurelog.AptNum=0 "
					+"WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.PatNum != appointment.PatNum";
				int numberFixed=Db.NonQ32(command);
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
				command=@"SELECT COUNT(*) FROM procedurelog,appointment
					WHERE procedurelog.AptNum = appointment.AptNum
					AND DATE(procedurelog.ProcDate) != DATE(appointment.AptDateTime)
					AND procedurelog.ProcStatus = 2";
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures which are attached to appointments with mismatched dates: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"UPDATE procedurelog,appointment
					SET procedurelog.AptNum=0
					WHERE procedurelog.AptNum = appointment.AptNum
					AND DATE(procedurelog.ProcDate) != DATE(appointment.AptDateTime)
					AND procedurelog.ProcStatus = 2";//only detach completed procs 
				int numberFixed=Db.NonQ32(command);
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
				command=@"SELECT COUNT(*) FROM procedurelog 
					WHERE baseunits != (SELECT procedurecode.BaseUnits FROM procedurecode WHERE procedurecode.CodeNum=procedurelog.CodeNum)
					AND baseunits != 0";
				//Better query:
				//SELECT COUNT(*)
				//FROM procedurelog,procedurecode
				//WHERE procedurecode.CodeNum=procedurelog.CodeNum
				//AND procedurelog.BaseUnits != 0
				//AND procedurecode.BaseUnits = 0;

				//pretty safe to change them back to zero.
				numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedure BaseUnits not matching procedurecode BaseUnits: ")+numFound+"\r\n";
				}
			}
			else {
				////procedurelog.BaseUnits must match procedurecode.BaseUnits because there is no UI for procs.
				////For speed, we will use two different strategies
				//command="SELECT COUNT(*) FROM procedurecode WHERE BaseUnits != 0";
				//if(Db.GetCount(command)=="0") {
				//	command="UPDATE procedurelog SET BaseUnits=0 WHERE BaseUnits!=0";
				//}
				//else {
				//	command=@"UPDATE procedurelog
				//		SET baseunits =  (SELECT procedurecode.BaseUnits FROM procedurecode
				//		WHERE procedurecode.CodeNum=procedurelog.CodeNum)
				//		WHERE baseunits != (SELECT procedurecode.BaseUnits FROM procedurecode
				//		WHERE procedurecode.CodeNum=procedurelog.CodeNum)";
				//}
				//int numberFixed=Db.NonQ32(command);
				//if(numberFixed>0 || verbose) {
				//	log+=Lans.g("FormDatabaseMaintenance","Procedure BaseUnits set to match procedurecode BaseUnits: ")+numberFixed.ToString()+"\r\n";
				//}
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures fixed with invalid CodeNum")+": "+numberFixed.ToString()+"\r\n";
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
				//int numberFixed=Db.NonQ32(command);
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
			command="SELECT procedurelog.ProcNum FROM procedurelog,claim,claimproc "
					+"WHERE procedurelog.ProcNum=claimproc.ProcNum "
					+"AND claim.ClaimNum=claimproc.ClaimNum "
					+"AND procedurelog.ProcStatus!="+POut.Long((int)ProcStat.C)+" "//procedure not complete
					+"AND (claim.ClaimStatus='W' OR claim.ClaimStatus='S' OR claim.ClaimStatus='R') "//waiting, sent, or received
					+"AND (claim.ClaimType='P' OR claim.ClaimType='S' OR claim.ClaimType='Other')";//pri, sec, or other.  Eliminates preauths.
			table=Db.GetTable(command);
			if(isCheck) {
				if(table.Rows.Count>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Procedures attached to claims, but with status of TP: ")+table.Rows.Count+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
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

		public static string RecallDuplicatesWarn(bool verbose,bool isCheck) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose,isCheck);
			}
			string log="";
			if(RecallTypes.PerioType<1 || RecallTypes.ProphyType<1) {
			  log+=Lans.g("FormDatabaseMaintenance","Warning!  Recall types not set up properly.")+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0) {
					Signals.SetInvalid(InvalidType.RecallTypes);
				}
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Recall triggers deleted due to bad codenum: ")+numberFixed.ToString()+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
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
				command=@"SELECT COUNT(*) FROM signal WHERE SigDateTime > NOW() OR AckTime > NOW()";
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					string nowDateTime=POut.DateT(MiscData.GetNowDateTime());
					command=@"SELECT COUNT(*) FROM signal WHERE SigDateTime > "+nowDateTime+" OR AckTime > "+nowDateTime;
				}
				int numFound=PIn.Int(Db.GetCount(command));
				if(numFound>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Signal entries with future time: ")+numFound+"\r\n";
				}
			}
			else {
				command=@"DELETE FROM signal WHERE SigDateTime > NOW() OR AckTime > NOW()";
				if(DataConnection.DBtype==DatabaseType.Oracle) {
					string nowDateTime=POut.DateT(MiscData.GetNowDateTime());
					command=@"DELETE FROM signal WHERE SigDateTime > "+nowDateTime+" OR AckTime > "+nowDateTime;
				}
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Signal entries deleted: ")+numberFixed.ToString()+"\r\n";
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
				int numberFixed=Db.NonQ32(command);
				if(numberFixed>0 || verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Statement DateRangeTo max fixed: ")+numberFixed.ToString()+"\r\n";
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
			//int numberFixed=0;
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
			//  numberFixed+=Db.NonQ32(command);
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
			//int numberFixed=0;
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
			//  numberFixed+=Db.NonQ32(command);
			//}
			//command="SELECT ClaimPaymentNum "
			//  +"FROM "+olddb+".claimpayment "
			//  +"WHERE NOT EXISTS(SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum="+olddb+".claimpayment.ClaimPaymentNum) ";
			//table=Db.GetTable(command);
			//int numberFixed2=0;
			//for(int i=0;i<table.Rows.Count;i++) {
			//  command="INSERT INTO claimpayment SELECT * FROM "+olddb+".claimpayment "
			//    +"WHERE "+olddb+".claimpayment.ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
			//  numberFixed2+=Db.NonQ32(command);
			//}
			//log+="Missing claimprocs added back: "+numberFixed.ToString()+".\r\n";
			//log+="Missing claimpayments added back: "+numberFixed2.ToString()+".\r\n";
			return log;
		}

		

		


		
		

	}
}
