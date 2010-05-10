using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class DatabaseMaintenance {
		//public static bool verbose;
		//<summary>This gets initialized to empty before starting, and then stores the running log.</summary>
		//public static string textLog;
		private static DataTable table;
		private static string command;
		private static bool success=false;

		public static bool GetSuccess() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod());
			}
			return DatabaseMaintenance.success; 
		}

		public static string MySQLTables(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
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
			//DialogResult result;
			//ArrayList corruptTables=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++) {
				tableNames[i]=table.Rows[i][0].ToString();
			}
			for(int i=0;i<tableNames.Length;i++) {
				command="CHECK TABLE "+tableNames[i];
				table=Db.GetTable(command);
				lastRow=table.Rows.Count-1;
				if(table.Rows[lastRow][3].ToString()!="OK") {
					log+=Lans.g("FormDatabaseMaintenance","Corrupt file found for table")+" "+tableNames[i]+"\r\n";
					allOK=false;
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
			if(allOK) {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","No corrupted tables.")+"\r\n";
				}
			}
			else {
				success=false;//no other checks should be done until we can successfully get past this.
				log+=Lans.g("FormDatabaseMaintenance","Corrupted files probably fixed.  Look closely at the log.  Also, run again to be sure they were really fixed.")+"\r\n";
			}
			return log;
		}

		public static string DatesNoZeros(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";//This check is not valid for Oracle, because each of the following fields are defined as non-null,
				//and 0000-00-00 is not a valid Oracle date.
			}
			string[] commands=new string[]
			{
				"UPDATE adjustment SET AdjDate='0001-01-01' WHERE AdjDate='0000-00-00'"
				,"UPDATE adjustment SET DateEntry='1980-01-01' WHERE DateEntry<'1980'"
				,"UPDATE adjustment SET ProcDate='0001-01-01' WHERE ProcDate='0000-00-00'"
				,"UPDATE appointment SET AptDateTime='0001-01-01 00:00:00' WHERE AptDateTime LIKE '0000-00-00%'"
				,"UPDATE appointment SET DateTimeArrived='0001-01-01 00:00:00' WHERE DateTimeArrived LIKE '0000-00-00%'"
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
				,"UPDATE insplan SET DateEffective='0001-01-01' WHERE DateEffective='0000-00-00'"
				,"UPDATE insplan SET DateTerm='0001-01-01' WHERE DateTerm='0000-00-00'"
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
			int rowsChanged=Db.NonQ32(commands);
			if(rowsChanged !=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Dates fixed. Rows changed:")+" "+rowsChanged.ToString()+"\r\n";
			}
			return log;
		}

		public static string DecimalValues(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//Holds columns to be checked. Strings are in pairs in the following order: table-name,column-name
			string[] decimalCols=new string[] {
				"patient","EstBalance"
			};
			int decimalPlacessToRoundTo=8;
			int numberFixed=0;
			for(int i=0;i<decimalCols.Length;i+=2) {
				string tablename=decimalCols[i];
				string colname=decimalCols[i+1];
				string command="UPDATE "+tablename+" SET "+colname+"=ROUND("+colname+","+decimalPlacessToRoundTo
					+") WHERE "+colname+"!=ROUND("+colname+","+decimalPlacessToRoundTo+")";
				numberFixed+=Db.NonQ32(command);
			}
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Decimal values fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		//Methods that apply to specific tables----------------------------------------------------------------------------------

		public static string AppointmentsNoPattern(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"SELECT AptNum FROM appointment WHERE Pattern=''";
			table=Db.GetTable(command);
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
			return log;
		}

		public static string AppointmentsNoDateOrProcs(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="DELETE FROM appointment "
				+"WHERE AptStatus=1 "//scheduled 
				+"AND DATE(AptDateTime)='0001-01-01' "//scheduled but no date 
				+"AND NOT EXISTS(SELECT * FROM procedurelog WHERE procedurelog.AptNum=appointment.AptNum)";//and no procs
			int numberFixed=Db.NonQ32(command);
			if(numberFixed!=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Appointments deleted due to no date and no procs: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string AutoCodesDeleteWithNoItems(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"DELETE FROM autocode WHERE NOT EXISTS(
				SELECT * FROM autocodeitem WHERE autocodeitem.AutoCodeNum=autocode.AutoCodeNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0) {
				Signals.SetInvalid(InvalidType.AutoCodes);
			}
			if(numberFixed!=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Autocodes deleted due to no items: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimPlanNum2NotValid(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//This fixes a slight database inconsistency that might cause an error when trying to open the send claims window. 
			command="UPDATE claim SET PlanNum2=0 WHERE PlanNum2 !=0 AND NOT EXISTS( SELECT * FROM insplan "
				+"WHERE claim.PlanNum2=insplan.PlanNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claims with invalid PlanNum2 fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimDeleteWithInvalidPlanNums(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"SELECT ClaimNum,PatNum FROM claim
				LEFT JOIN insplan ON claim.PlanNum=insplan.PlanNum
				WHERE insplan.PlanNum IS NULL";
			table=Db.GetTable(command);
			Patient Lim;
			int numberFixed=0;
			for(int i=0;i<table.Rows.Count;i++) {
				command="DELETE FROM claim WHERE ClaimNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
				Lim=Patients.GetLim(PIn.Long(table.Rows[i][1].ToString()));
				log+=Lans.g("FormDatabaseMaintenance","Claim with invalid PlanNum deleted for ")+Lim.GetNameLF()+"\r\n";
				numberFixed++;
			}
			if(numberFixed!=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claims deleted due to invalid PlanNum: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimDeleteWithNoClaimProcs(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"DELETE FROM claim WHERE NOT EXISTS(
				SELECT * FROM claimproc WHERE claim.ClaimNum=claimproc.ClaimNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed!=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claims deleted due to no procedures: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimWriteoffSum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//Sums for each claim---------------------------------------------------------------------
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			command=@"SELECT claim.ClaimNum,SUM(claimproc.WriteOff) sumwo,claim.WriteOff
				FROM claim,claimproc
				WHERE claim.ClaimNum=claimproc.ClaimNum
				GROUP BY claim.ClaimNum
				HAVING sumwo-claim.WriteOff > .01
				OR sumwo-claim.WriteOff < -.01";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE claim SET WriteOff='"+POut.Double(PIn.Double(table.Rows[i]["sumwo"].ToString()))+"' "
					+"WHERE ClaimNum="+table.Rows[i]["ClaimNum"].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claim writeoff sums fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		///<Summary>also fixes resulting deposit misbalances.</Summary>
		public static string ClaimPaymentCheckAmt(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//because of the way this is grouped, it will just get one of many patients for each
			int numberFixed=0;
			command=@"SELECT claimproc.ClaimPaymentNum,ROUND(SUM(InsPayAmt),2) _sumpay,ROUND(CheckAmt,2) _checkamt
				FROM claimpayment,claimproc
				WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum
				GROUP BY claimproc.ClaimPaymentNum
				HAVING _sumpay!=_checkamt";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE claimpayment SET CheckAmt='"+POut.Double(PIn.Double(table.Rows[i]["_sumpay"].ToString()))+"' "
					+"WHERE ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
				Db.NonQ(command);
			}
			numberFixed=table.Rows.Count;
			if(numberFixed>0||verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claim payment sums fixed: ")+numberFixed.ToString()+"\r\n";
			}
			//now deposits which were affected by the changes above--------------------------------------------------
			command=@"SELECT DepositNum,deposit.Amount,DateDeposit,
				IFNULL((SELECT SUM(CheckAmt) FROM claimpayment WHERE claimpayment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0)
				+IFNULL((SELECT SUM(PayAmt) FROM payment WHERE payment.DepositNum=deposit.DepositNum GROUP BY deposit.DepositNum),0) _sum
				FROM deposit
				HAVING ROUND(_sum,2) != ROUND(deposit.Amount,2)";
			table=Db.GetTable(command);
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
			if(numberFixed>0||verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Deposit sums fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimPaymentDeleteWithNoSplits(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="DELETE FROM claimpayment WHERE NOT EXISTS("
				+"SELECT * FROM claimproc WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claim payments with no splits removed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcDateNotMatchCapComplete(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE claimproc SET DateCP=ProcDate WHERE Status=7 AND DateCP != ProcDate";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Capitation procs with mismatched dates fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcDateNotMatchPayment(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT claimproc.ClaimProcNum,claimpayment.CheckDate FROM claimproc,claimpayment "
				+"WHERE claimproc.ClaimPaymentNum=claimpayment.ClaimPaymentNum "
				+"AND claimproc.DateCP!=claimpayment.CheckDate";
			table=Db.GetTable(command);
			DateTime datecp;
			for(int i=0;i<table.Rows.Count;i++) {
				datecp=PIn.Date(table.Rows[i][1].ToString());
				command="UPDATE claimproc SET DateCP="+POut.Date(datecp)
					+" WHERE ClaimProcNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claim payments with mismatched dates fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		/// <summary>This addresses a bug discovered on 3/17/10.  Some duplicate claimprocs had been created.  The bug only lasted a few days.</summary>
		public static string ClaimProcDeleteDuplicates(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			/*Please see http://www.opendental.com/manual/bugcp.html
			command=@"SELECT PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber, COUNT(*) cnt
FROM claimproc
WHERE ClaimNum > 0
AND ProcNum>0
AND Status!=4/*exclude supplemental*
GROUP BY Claimnum,ProcNum,Status,InsPayAmt,FeeBilled,LineNumber
HAVING cnt>1";
			table=Db.GetTable(command);
			int numberFixed=0;
			double insPayAmt;
			double feeBilled;
			for(int i=0;i<table.Rows.Count;i++) {
				insPayAmt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
				feeBilled=PIn.Double(table.Rows[i]["FeeBilled"].ToString());
				command="DELETE FROM claimproc "
					+"WHERE ClaimNum= "+table.Rows[i]["ClaimNum"].ToString()+" "
					+"AND ProcNum= "+table.Rows[i]["ProcNum"].ToString()+" "
					+"AND Status= "+table.Rows[i]["Status"].ToString()+" "
					+"AND InsPayAmt= '"+POut.Double(insPayAmt)+"' "
					+"AND FeeBilled= '"+POut.Double(feeBilled)+"' "
					+"AND LineNumber= "+table.Rows[i]["LineNumber"].ToString()+" "
					+"AND ClaimProcNum != "+table.Rows[i]["ClaimProcNum"].ToString();
				numberFixed+=Db.NonQ32(command);
			}
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claimprocs deleted due duplicate entries: ")+numberFixed.ToString()+".  Run this tool again to fix related totals.\r\n";
			}*/
			return log;
		}

		public static string ClaimProcDeleteWithInvalidClaimNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="DELETE FROM claimproc WHERE claimproc.ClaimNum!=0 "
				+"AND NOT EXISTS(SELECT * FROM claim WHERE claim.ClaimNum=claimproc.ClaimNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claimprocs deleted due to invalid ClaimNum: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcDeleteWithInvalidPlanNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"SELECT ClaimProcNum,PatNum FROM claimproc
				LEFT JOIN insplan ON claimproc.PlanNum=insplan.PlanNum
				WHERE insplan.PlanNum IS NULL";
			table=Db.GetTable(command);
			Patient Lim;
			int numberFixed=0;
			for(int i=0;i<table.Rows.Count;i++) {
				command="DELETE FROM claimproc WHERE ClaimProcNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
				Lim=Patients.GetLim(PIn.Long(table.Rows[i][1].ToString()));
				log+=Lans.g("FormDatabaseMaintenance","Claimproc with invalid PlanNum deleted for ")+Lim.GetNameLF()+"\r\n";
				numberFixed++;
			}
			if(numberFixed!=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","ClaimProcs deleted due to invalid PlanNum: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcDeleteWithInvalidProcNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//These seem to pop up quite regularly due to the program forgetting to delete them
			command="DELETE FROM claimproc WHERE ProcNum>0 AND NOT EXISTS(SELECT * FROM procedurelog "
				+"WHERE claimproc.ProcNum=procedurelog.ProcNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Estimates deleted for procedures that no longer exist: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcEstNoBillIns(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE claimproc SET InsPayEst=0 WHERE NoBillIns=1 AND InsPayEst !=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Claimproc estimates set to zero because marked NoBillIns: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcEstWithInsPaidAmt(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"UPDATE claimproc SET InsPayAmt=0 WHERE InsPayAmt > 0 AND ClaimNum=0 AND Status=6";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","ClaimProc estimates with InsPaidAmt > 0 fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcProvNumMissing(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE claimproc SET ProvNum="+PrefC.GetString(PrefName.PracticeDefaultProv)+" WHERE ProvNum=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with missing provnums fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcPreauthNotMatchClaim(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"SELECT claimproc.ClaimProcNum 
				FROM claimproc,claim 
				WHERE claimproc.ClaimNum=claim.ClaimNum
				AND claim.ClaimType='PreAuth'
				AND claimproc.Status!=2";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE claimproc SET Status=2"
					+" WHERE ClaimProcNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","ClaimProcs for preauths with status not preauth fixed: ")+numberFixed.ToString()+"\r\n";
			}
			//this gives the wrong number of rows fixed, so we had to get more complicated:
			//command=@"UPDATE claimproc,claim
			//	SET claimproc.Status=2
			//	WHERE claimproc.ClaimNum=claim.ClaimNum
			//	AND claim.ClaimType='PreAuth'";
			return log;
		}

		public static string ClaimProcStatusNotMatchClaim(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			command=@"UPDATE claimproc,claim
				SET claimproc.Status=1
				WHERE claimproc.ClaimNum=claim.ClaimNum
				AND claim.ClaimStatus='R'
				AND claimproc.Status=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with status not matching claim fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcWithInvalidClaimPaymentNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"UPDATE claimproc SET ClaimPaymentNum=0 WHERE claimpaymentnum !=0 AND NOT EXISTS(
				SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum=claimproc.ClaimPaymentNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","ClaimProcs with with invalid ClaimPaymentNumber fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClaimProcWriteOffNegative(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"UPDATE claimproc SET WriteOff = -WriteOff WHERE WriteOff < 0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Negative writeoffs fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ClockEventInFuture(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"UPDATE clockevent SET TimeDisplayed1=TimeEntered1 WHERE TimeDisplayed1 > NOW()";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Timecard entries fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string DocumentWithNoCategory(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT DocNum FROM document WHERE DocCategory=0";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE document SET DocCategory="+POut.Long(DefC.Short[(int)DefCat.ImageCats][0].DefNum)
					+" WHERE DocNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Images with no category fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		/*No longer relevant due to database changes.
		public static string EtransRemoveOldReceivedClaimTransactions(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			//Set the etrans entries to blank where the associated claim has been
			//received for at least a month.
			command="UPDATE etrans e,claim c "+
				"SET e.messagetext='' "+
				"WHERE e.ClaimNum=c.ClaimNum AND "+
				"UPPER(c.ClaimStatus)='R' AND "+
				"DATE(c.DateReceived)<=ADDDATE(CURDATE(),INTERVAL -1 MONTH) AND "+
				"YEAR(c.DateReceived)>1";
			int rowsCleared=Db.NonQ(command);
			//Now, alter the etrans table messagetext column to force MySQL to delete
			//the dead space still in the file which is no longer needed. By default,
			//MySQL will leave dead space when you shrink the contents of a string
			//within a row, I presume this is for speed efficiency.
			command="ALTER TABLE etrans CHANGE messagetext messagetext text NULL";
			Db.NonQ(command);
			if(rowsCleared>0 && verbose){
				return Lans.g("FormDatabaseMaintenance","Number of old/received etrans entries cleared: ")+
					rowsCleared.ToString()+"\r\n";
			}
			return "";
		}*/

		public static string InsPlanCheckNoCarrier(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//Gets a list of insurance plans that do not have a carrier attached. The list should be blank. If not, then you need to go to the plan listed and add a carrier. Missing carriers will cause the send claims function to give an error.
			command="SELECT PlanNum FROM insplan WHERE CarrierNum=0";
			table=Db.GetTable(command);
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
			return log;
		}

		public static string InsPlanNoClaimForm(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE insplan SET ClaimFormNum="+POut.Long(PrefC.GetLong(PrefName.DefaultClaimForm))
				+" WHERE ClaimFormNum=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Insplan claimforms set if missing: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string MedicationPatDeleteWithInvalidMedNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="DELETE FROM medicationpat WHERE NOT EXISTS(SELECT * FROM medication "
				+"WHERE medication.MedicationNum=medicationpat.MedicationNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Medications deleted because no defition exists for them: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PatientBadGuarantor(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT p.PatNum FROM patient p LEFT JOIN patient p2 ON p.Guarantor = p2.PatNum WHERE p2.PatNum IS NULL";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE patient SET Guarantor=PatNum WHERE PatNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Patients with invalid Guarantors fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PatientPriProvMissing(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//previous versions of the program just dealt gracefully with missing provnum.
			//From now on, we can assum priprov is not missing, making coding easier.
			command=@"UPDATE patient SET PriProv="+PrefC.GetString(PrefName.PracticeDefaultProv)+" WHERE PriProv=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Patient pri provs fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PatientUnDeleteWithBalance(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT PatNum FROM patient	WHERE PatStatus=4 "
				+"AND (Bal_0_30 !=0	OR Bal_31_60 !=0 OR Bal_61_90 !=0	OR BalOver90 !=0 OR InsEst !=0 OR BalTotal !=0)";
			table=Db.GetTable(command);
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
			return log;
		}

		public static string PatPlanOrdinalTwoToOne(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT PatPlanNum FROM patplan patplan1 WHERE Ordinal=2 AND NOT EXISTS("
				+"SELECT * FROM patplan patplan2 WHERE patplan1.PatNum=patplan2.PatNum AND patplan2.Ordinal=1)";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE patplan SET Ordinal=1 WHERE PatPlanNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","PatPlan ordinals changed from 2 to 1 if no primary ins: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PaymentDetachMissingDeposit(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE payment SET DepositNum=0 "
				+"WHERE DepositNum != 0 " 
				+"AND NOT EXISTS(SELECT * FROM deposit WHERE deposit.DepositNum=payment.DepositNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Payments detached from deposits that no longer exist: ")
					+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PayPlanChargeGuarantorMatch(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			int numberFixed=0;
			command="UPDATE payplancharge,payplan SET payplancharge.Guarantor=payplan.Guarantor "
				+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
				+"AND payplancharge.Guarantor != payplan.Guarantor";
			numberFixed+=Db.NonQ32(command);
			command="UPDATE payplancharge,payplan SET payplancharge.PatNum=payplan.PatNum "
				+"WHERE payplan.PayPlanNum=payplancharge.PayPlanNum "
				+"AND payplancharge.PatNum != payplan.PatNum";
			numberFixed+=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","PayPlanCharge guarantors and pats set to match payplan guarantors and pats: ")
					+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PayPlanChargeProvNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//I would rather set the provnum to that of the patient, but it's more complex.
			command="UPDATE payplancharge SET ProvNum="+POut.Long(PrefC.GetLong(PrefName.PracticeDefaultProv))
				+" WHERE ProvNum=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Pay plan charge providers set if missing: ")
					+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PayPlanSetGuarantorToPatForIns(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE payplan SET Guarantor=PatNum WHERE PlanNum>0 AND Guarantor != PatNum";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","PayPlan Guarantors set to PatNum if used for insurance tracking: ")
					+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PaySplitAttachedToPayPlan(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT SplitNum,payplan.Guarantor FROM paysplit,payplan "
				+"WHERE paysplit.PayPlanNum=payplan.PayPlanNum "
				+"AND paysplit.PatNum!=payplan.Guarantor";
			DataTable table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE paysplit SET PatNum="+table.Rows[i]["Guarantor"].ToString()
					+" WHERE SplitNum="+table.Rows[i]["SplitNum"].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Paysplits changed patnum to match payplan guarantor: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PaySplitDeleteWithInvalidPayNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="DELETE FROM paysplit WHERE NOT EXISTS(SELECT * FROM payment WHERE paysplit.PayNum=payment.PayNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Paysplits deleted due to invalid PayNum: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string PreferenceDateDepositsStarted(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
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
			return log;
		}

		public static string PreferencePracticeBillingType(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT valuestring FROM preference WHERE prefname = 'PracticeDefaultBillType'";
			table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="") {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Default practice billing type verified.")+"\r\n";
				}
				return log;
			}
			log+=Lans.g("FormDatabaseMaintenance","No default billing type set.");
			command="SELECT defnum FROM definition WHERE category = 4 AND ishidden = 0 ORDER BY itemorder";
			table=Db.GetTable(command);
			command="UPDATE preference SET valuestring='"+table.Rows[0][0].ToString()+"' WHERE prefname='PracticeDefaultBillType'";
			Db.NonQ(command);
			log+="  "+Lans.g("FormDatabaseMaintenance","Fixed.")+"\r\n";
			return log;
		}

		public static string PreferencePracticeProv(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT valuestring FROM preference WHERE prefname = 'PracticeDefaultProv'";
			table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="") {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Default practice provider verified.")+"\r\n";
				}
				return log;
			}
			log+=Lans.g("FormDatabaseMaintenance","No default provider set.");
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
			return log;
		}

		public static string ProcButtonItemsDeleteWithInvalidAutoCode(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"DELETE FROM procbuttonitem WHERE CodeNum=0 AND NOT EXISTS(
				SELECT * FROM autocode WHERE autocode.AutoCodeNum=procbuttonitem.AutoCodeNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0) {
				Signals.SetInvalid(InvalidType.ProcButtons);
			}
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","ProcButtonItems deleted due to invalid autocode: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogAttachedToWrongAppts(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			command="UPDATE appointment,procedurelog SET procedurelog.AptNum=0 "
				+"WHERE procedurelog.AptNum=appointment.AptNum AND procedurelog.PatNum != appointment.PatNum";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedures detached from appointments: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogAttachedToWrongApptDate(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"UPDATE procedurelog,appointment
				SET procedurelog.AptNum=0
				WHERE procedurelog.AptNum = appointment.AptNum
				AND DATE(procedurelog.ProcDate) != DATE(appointment.AptDateTime)
				AND procedurelog.ProcStatus = 2";//only detach completed procs 
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedures detached from appointments due to mismatched dates: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogBaseUnitsZero(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			//procedurelog.BaseUnits must match procedurecode.BaseUnits because there is no UI for procs.
			//For speed, we will use two different strategies
			command="SELECT COUNT(*) FROM procedurecode WHERE BaseUnits != 0";
			if(Db.GetCount(command)=="0") {
				command="UPDATE procedurelog SET BaseUnits=0 WHERE BaseUnits!=0";
			}
			else {
				command=@"UPDATE procedurelog
					SET baseunits =  (SELECT procedurecode.BaseUnits FROM procedurecode
					WHERE procedurecode.CodeNum=procedurelog.CodeNum)
					WHERE baseunits != (SELECT procedurecode.BaseUnits FROM procedurecode
					WHERE procedurecode.CodeNum=procedurelog.CodeNum)";
			}
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedure BaseUnits set to match procedurecode BaseUnits: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogCodeNumZero(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="DELETE FROM procedurelog WHERE CodeNum=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedures deleted with CodeNum=0: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogProvNumMissing(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE procedurelog SET ProvNum="+PrefC.GetString(PrefName.PracticeDefaultProv)+" WHERE ProvNum=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedures with missing provnums fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogToothNums(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			Patient Lim=null;
			string toothNum;
			int numberFixed=0;
			command="SELECT procnum,toothnum,patnum FROM procedurelog";
			table=Db.GetTable(command);
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
					command="UPDATE procedurelog SET ToothNum = '"+toothNum.ToUpper()+"' WHERE ProcNum = "+table.Rows[i][0].ToString();
					Db.NonQ(command);
					if(verbose) {
						log+=Lim.GetNameLF()+" "+toothNum+" - "+toothNum.ToUpper()+"\r\n";
					}
					numberFixed++;
				}
				else {
					command="UPDATE procedurelog SET ToothNum = '1' WHERE ProcNum = "+table.Rows[i][0].ToString();
					Db.NonQ(command);
					if(verbose) {
						log+=Lim.GetNameLF()+" "+toothNum+" - 1\r\n";
					}
					numberFixed++;
				}
			}
			if(numberFixed!=0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Check for invalid tooth numbers complete.  Records checked: ")
					+table.Rows.Count.ToString()+". "+Lans.g("FormDatabaseMaintenance","Records fixed: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProcedurelogTpAttachedToClaim(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="SELECT procedurelog.ProcNum FROM procedurelog,claim,claimproc "
				+"WHERE procedurelog.ProcNum=claimproc.ProcNum "
				+"AND claim.ClaimNum=claimproc.ClaimNum "
				+"AND procedurelog.ProcStatus!="+POut.Long((int)ProcStat.C)+" "//procedure not complete
				+"AND (claim.ClaimStatus='W' OR claim.ClaimStatus='S' OR claim.ClaimStatus='R') "//waiting, sent, or received
				+"AND (claim.ClaimType='P' OR claim.ClaimType='S' OR claim.ClaimType='Other')";//pri, sec, or other.  Eliminates preauths.
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command="UPDATE procedurelog SET ProcStatus=2 WHERE ProcNum="+table.Rows[i][0].ToString();
				Db.NonQ(command);
			}
			int numberFixed=table.Rows.Count;
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedures attached to claims, but with status of TP.  Status changed back to C: ")
					+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		/*public static string ProcedurelogUndeleteAttachedToClaim(bool verbose) {
			//This is no longer used and can be considered buggy.  It was replaced by the better ProcedurelogTpAttachedToClaim
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				return "";
			}
			command=@"UPDATE procedurelog,claimproc         
				SET procedurelog.ProcStatus=2
				WHERE procedurelog.ProcNum=claimproc.ProcNum
				AND procedurelog.ProcStatus=6
				AND claimproc.ClaimNum!=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedures undeleted because found attached to claims: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}*/

		public static string ProcedurelogUnitQtyZero(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"UPDATE procedurelog        
				SET UnitQty=1
				WHERE UnitQty=0";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Procedures changed from UnitQty=0 to UnitQty=1: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string ProviderHiddenWithClaimPayments(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"SELECT MAX(claimproc.ProcDate),provider.ProvNum
				FROM claimproc,provider
				WHERE claimproc.ProvNum=provider.ProvNum
				AND provider.IsHidden=1
				AND claimproc.InsPayAmt>0
				GROUP BY provider.ProvNum";
			table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Hidden providers checked for claim payments.")+"\r\n";
					return log;
				}
			}
			Provider prov;
			for(int i=0;i<table.Rows.Count;i++) {
				prov=Providers.GetProv(PIn.Long(table.Rows[i][1].ToString()));
				log+=Lans.g("FormDatabaseMaintenance","Warning!  Hidden provider ")+" "+prov.Abbr+" "
					+Lans.g("FormDatabaseMaintenance","has claim payments entered as recently as ")
					+PIn.Date(table.Rows[i][0].ToString()).ToShortDateString()
					+Lans.g("FormDatabaseMaintenance",".  This data will be missing on income reports.")+"\r\n";
			}
			return log;
		}

		public static string RecallDuplicatesWarn(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			if(RecallTypes.PerioType<1 || RecallTypes.ProphyType<1) {
				log+=Lans.g("FormDatabaseMaintenance","Warning!  Recall types not set up properly.")+"\r\n";
				return log;
			}
			command="SELECT FName,LName,COUNT(*) countDups FROM patient LEFT JOIN recall ON recall.PatNum=patient.PatNum "
				+"AND (recall.RecallTypeNum="+POut.Long(RecallTypes.PerioType)+" "
				+"OR recall.RecallTypeNum="+POut.Long(RecallTypes.ProphyType)+") "
				+"GROUP BY patient.PatNum HAVING countDups>1";
			table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				if(verbose) {
					log+=Lans.g("FormDatabaseMaintenance","Recalls checked for duplicates.")+"\r\n";
				}
				return log;
			}
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
			return log;
		}

		public static string RecallTriggerDeleteBadCodeNum(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"DELETE FROM recalltrigger
				WHERE NOT EXISTS (SELECT * FROM procedurecode WHERE procedurecode.CodeNum=recalltrigger.CodeNum)";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0) {
				Signals.SetInvalid(InvalidType.RecallTypes);
			}
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Recall triggers deleted due to bad codenum: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		//no longer relevant:
		/*public static string RecallDeleteDuplicate(bool verbose) {
			//command="SELECT COUNT(*) AS repetitions,PatNum FROM recall GROUP BY PatNum HAVING repetitions >1";
			command="SELECT COUNT(*),PatNum FROM recall GROUP BY PatNum HAVING COUNT(*) >1";
			table=Db.GetTable(command);
			int numberFound=table.Rows.Count;
			//we're going to do one patient at a time.
			DataTable tableRecalls;
			for(int i=0;i<table.Rows.Count;i++) {
				command="SELECT RecallNum FROM recall WHERE PatNum="+table.Rows[i][1].ToString();
				tableRecalls=Db.GetTable(command);
				command="DELETE FROM recall WHERE ";
				for(int r=0;r<tableRecalls.Rows.Count-1;r++) {//we ignore the last row
					if(r>0) {
						command+="OR ";
					}
					command+="RecallNum="+tableRecalls.Rows[r][0].ToString()+" ";
				}
				Db.NonQ(command);
				//pats+=table.Rows[i][1].ToString();
			}
			if(numberFound>0) {
				Recalls.SynchAllPatients();
			}
			if(numberFound>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Duplicate recall entries fixed: ")+numberFound.ToString()+"\r\n";
			}
		}*/

		public static string SchedulesDeleteShort(bool verbose) {
			//No need to check RemotingRole; no call to db.
			string log="";
			int numberFixed=0;
			Schedule[] schedList=Schedules.RefreshAll();
			for(int i=0;i<schedList.Length;i++) {
				if(schedList[i].Status!=SchedStatus.Open) {
					continue;//closed and holiday statuses do not use starttime and stoptime
				}
				if(schedList[i].StopTime.TimeOfDay-schedList[i].StartTime.TimeOfDay<new TimeSpan(0,5,0)) {
					Schedules.Delete(schedList[i]);
					numberFixed++;
				}
			}
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Schedule blocks fixed: ")+numberFixed.ToString()+"\r\n";
			}
			//DataValid.SetInvalid(InvalidTypes.Sched);
			return log;
		}

		public static string SchedulesDeleteProvClosed(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="DELETE FROM schedule WHERE SchedType=1 AND Status=1";//type=prov,status=closed
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0||verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Schedules deleted that were causing printing issues: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string SignalInFuture(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command=@"DELETE FROM signal WHERE SigDateTime > NOW() OR AckTime > NOW()";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				string nowDateTime=POut.DateT(MiscData.GetNowDateTime());
				command=@"DELETE FROM signal WHERE SigDateTime > "+nowDateTime+" OR AckTime > "+nowDateTime;
			}
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Signal entries deleted: ")+numberFixed.ToString()+"\r\n";
			}
			return log;
		}

		public static string StatementDateRangeMax(bool verbose) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod(),verbose);
			}
			string log="";
			command="UPDATE statement SET DateRangeTo='2200-01-01' WHERE DateRangeTo='9999-12-31'";
			int numberFixed=Db.NonQ32(command);
			if(numberFixed>0 || verbose) {
				log+=Lans.g("FormDatabaseMaintenance","Statement DateRangeTo max fixed: ")+numberFixed.ToString()+"\r\n";
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
GROUP BY ClaimNum,ProcNum,Status,InsPayAmt,FeeBilled,LineNumber
HAVING cnt>1";
			table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return "";
			}
			retVal+="Duplicate claim payments found:\r\n";
			//double amt;
			DateTime date;
			//long patNum;
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){//check for duplicate rows.  We only want to report each claim once.
					if(table.Rows[i]["ClaimNum"].ToString()==table.Rows[i-1]["ClaimNum"].ToString()){
						continue;
					}
				}
				//amt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
				date=PIn.Date(table.Rows[i]["ProcDate"].ToString());
				//patNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
				retVal+=table.Rows[i]["LName"].ToString()+", "
					+table.Rows[i]["FName"].ToString()+" "
					+"("+table.Rows[i]["PatNum"].ToString()+"), "
					+date.ToShortDateString()+"\r\n";
					//+amt.ToString("c")+"\r\n";
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
GROUP BY Claimnum,ProcNum,Status,InsPayAmt,FeeBilled,LineNumber,ClaimPaymentNum
HAVING cnt>1";
			table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return "";
			}
			retVal+="Duplicate supplemental payments found (may be false positives):\r\n";
			//double amt;
			DateTime date;
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					if(table.Rows[i]["ClaimNum"].ToString()==table.Rows[i-1]["ClaimNum"].ToString()){
						continue;
					}
				}
				//amt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
				date=PIn.Date(table.Rows[i]["ProcDate"].ToString());
				retVal+=table.Rows[i]["LName"].ToString()+", "
					+table.Rows[i]["FName"].ToString()+" "
					+"("+table.Rows[i]["PatNum"].ToString()+"), "
					+date.ToShortDateString()+"\r\n";
					//+amt.ToString("c")+"\r\n";
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

		public static bool DatabaseIsOlderThanMarchSeventeenth(string olddb){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),olddb);
			}
			command="SELECT COUNT(*) FROM "+olddb+".claimproc WHERE DateEntry > '2010-03-16'";
			if(Db.GetCount(command)=="0"){
				return true;
			}
			return false;
		}

		/// <summary></summary>
		public static string FixClaimProcDeleteDuplicates() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string log="";
			command=@"SELECT LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber, COUNT(*) cnt
FROM claimproc
LEFT JOIN patient ON patient.PatNum=claimproc.PatNum
WHERE ClaimNum > 0
AND ProcNum>0
AND Status!=4/*exclude supplemental*/
GROUP BY ClaimNum,ProcNum,Status,InsPayAmt,FeeBilled,LineNumber
HAVING cnt>1";
			table=Db.GetTable(command);
			int numberFixed=0;
			double insPayAmt;
			double feeBilled;
			for(int i=0;i<table.Rows.Count;i++) {
				insPayAmt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
				feeBilled=PIn.Double(table.Rows[i]["FeeBilled"].ToString());
				command="DELETE FROM claimproc "
					+"WHERE ClaimNum= "+table.Rows[i]["ClaimNum"].ToString()+" "
					+"AND ProcNum= "+table.Rows[i]["ProcNum"].ToString()+" "
					+"AND Status= "+table.Rows[i]["Status"].ToString()+" "
					+"AND InsPayAmt= '"+POut.Double(insPayAmt)+"' "
					+"AND FeeBilled= '"+POut.Double(feeBilled)+"' "
					+"AND LineNumber= "+table.Rows[i]["LineNumber"].ToString()+" "
					+"AND ClaimProcNum != "+table.Rows[i]["ClaimProcNum"].ToString();
				numberFixed+=Db.NonQ32(command);
			}
			log+="Claimprocs deleted due duplicate entries: "+numberFixed.ToString()+".\r\n";
			return log;
		}

		/// <summary></summary>
		public static string FixMissingClaimProcs(string olddb) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string log="";
			command="SELECT LName,FName,patient.PatNum,ClaimNum,FeeBilled,Status,ProcNum,ProcDate,ClaimProcNum,InsPayAmt,LineNumber "
				+"FROM "+olddb+".claimproc "
				+"LEFT JOIN "+olddb+".patient ON "+olddb+".patient.PatNum="+olddb+".claimproc.PatNum "
				+"WHERE NOT EXISTS(SELECT * FROM claimproc WHERE claimproc.ClaimProcNum="+olddb+".claimproc.ClaimProcNum) "
				+"AND ClaimNum > 0 AND ProcNum>0";
			table=Db.GetTable(command);
			int numberFixed=0;
			command="SELECT ValueString FROM "+olddb+".preference WHERE PrefName = 'DataBaseVersion'";
			string oldVersString=Db.GetScalar(command);
			Version oldVersion=new Version(oldVersString);
			if(oldVersion < new Version("6.7.1.0")) {
				return "Version of old database is too old to use with the automated tool: "+oldVersString;
			}
			double insPayAmt;
			double feeBilled;
			for(int i=0;i<table.Rows.Count;i++) {
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
				if(result=="1"){//only include in result if there are duplicates in old db.
					continue;
				}
				command="INSERT INTO claimproc SELECT *";
				if(oldVersion < new Version("6.8.1.0")) {
					command+=",-1,-1,0";
				}
				else if(oldVersion < new Version("6.9.1.0")) {
					command+=",0";
				}
				command+=" FROM "+olddb+".claimproc "
					+"WHERE "+olddb+".claimproc.ClaimProcNum="+table.Rows[i]["ClaimProcNum"].ToString();
				numberFixed+=Db.NonQ32(command);
			}
			command="SELECT ClaimPaymentNum "
				+"FROM "+olddb+".claimpayment "
				+"WHERE NOT EXISTS(SELECT * FROM claimpayment WHERE claimpayment.ClaimPaymentNum="+olddb+".claimpayment.ClaimPaymentNum) ";
			table=Db.GetTable(command);
			int numberFixed2=0;
			for(int i=0;i<table.Rows.Count;i++) {
				command="INSERT INTO claimpayment SELECT * FROM "+olddb+".claimpayment "
					+"WHERE "+olddb+".claimpayment.ClaimPaymentNum="+table.Rows[i]["ClaimPaymentNum"].ToString();
				numberFixed2+=Db.NonQ32(command);
			}
			log+="Missing claimprocs added back: "+numberFixed.ToString()+".\r\n";
			log+="Missing claimpayments added back: "+numberFixed2.ToString()+".\r\n";
			return log;
		}
		
		

	}
}
