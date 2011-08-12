using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	public class DashboardQueries {
		public static DataTable GetProvList(DateTime dt) {
			string command;
			command="DROP TABLE IF EXISTS tempdash;";
			Db.NonQ(command);
			command=@"CREATE TABLE tempdash (
				ProvNum bigint NOT NULL PRIMARY KEY,
				production decimal NOT NULL,
				income decimal NOT NULL
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			//providers
			command=@"INSERT INTO tempdash (ProvNum)
				SELECT ProvNum
				FROM provider WHERE IsHidden=0
				ORDER BY ItemOrder";
			Db.NonQ(command);
			//production--------------------------------------------------------------------
			//procs
			command=@"UPDATE tempdash 
				SET production=(SELECT SUM(ProcFee*(UnitQty+BaseUnits)) FROM procedurelog 
				WHERE procedurelog.ProvNum=tempdash.ProvNum
				AND ProcDate="+POut.Date(dt)+")";
			Db.NonQ(command);
			//capcomplete writeoffs were skipped
			//adjustments
			command=@"UPDATE tempdash 
				SET production=production+(SELECT IFNULL(SUM(AdjAmt),0) FROM adjustment 
				WHERE adjustment.ProvNum=tempdash.ProvNum
				AND AdjDate="+POut.Date(dt)+")";
			Db.NonQ(command);
			//insurance writeoffs
			if(PrefC.GetBool(PrefName.ReportsPPOwriteoffDefaultToProcDate)) {//use procdate
				command=@"UPDATE tempdash 
					SET production=production-(SELECT IFNULL(SUM(WriteOff),0) FROM claimproc 
					WHERE claimproc.ProvNum=tempdash.ProvNum
					AND ProcDate="+POut.Date(dt)+@" 
					AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) )";//received or supplemental or notreceived
			}
			else {
				command=@"UPDATE tempdash 
					SET production=production-(SELECT IFNULL(SUM(WriteOff),0) FROM claimproc 
					WHERE claimproc.ProvNum=tempdash.ProvNum
					AND DateCP="+POut.Date(dt)+@" 
					AND (claimproc.Status=1 OR claimproc.Status=4) )";//received or supplemental 
			}
			Db.NonQ(command);
			//income------------------------------------------------------------------------
			//patient income
			command=@"UPDATE tempdash 
				SET income=(SELECT SUM(SplitAmt) FROM paysplit 
				WHERE paysplit.ProvNum=tempdash.ProvNum
				AND DatePay="+POut.Date(dt)+")";
			Db.NonQ(command);
			//ins income
			command=@"UPDATE tempdash 
				SET income=income+(SELECT IFNULL(SUM(InsPayAmt),0) FROM claimproc 
				WHERE claimproc.ProvNum=tempdash.ProvNum
				AND DateCP="+POut.Date(dt)+")";
			Db.NonQ(command);
			//final queries
			command="SELECT * FROM tempdash";
			DataTable table=Db.GetTable(command);
			command="DROP TABLE IF EXISTS tempdash;";
			Db.NonQ(command);
			return table;
		}

		public static List<decimal> GetProd12Months(DateTime dateFrom,DateTime dateTo){
			string command;
			command=@"SELECT procedurelog.ProcDate,
				SUM(procedurelog.ProcFee*(procedurelog.UnitQty+procedurelog.BaseUnits))-IFNULL(SUM(claimproc.WriteOff),0)
				FROM procedurelog
				LEFT JOIN claimproc ON procedurelog.ProcNum=claimproc.ProcNum
				AND claimproc.Status='7' /*only CapComplete writeoffs are subtracted here*/
				WHERE procedurelog.ProcStatus = '2'
				AND procedurelog.ProcDate >= "+POut.Date(dateFrom)+@"
				AND procedurelog.ProcDate <= "+POut.Date(dateTo)+@"
				GROUP BY MONTH(procedurelog.ProcDate)";
			DataTable tableProduction=Db.GetTable(command);
			command=@"SELECT AdjDate,
				SUM(AdjAmt)
				FROM adjustment
				WHERE AdjDate >= "+POut.Date(dateFrom)+@"
				AND AdjDate <= "+POut.Date(dateTo)+@"
				GROUP BY MONTH(AdjDate)";
			DataTable tableAdj=Db.GetTable(command);
			if(PrefC.GetBool(PrefName.ReportsPPOwriteoffDefaultToProcDate)) {//use procdate
				command="SELECT "
					+"claimproc.ProcDate," 
					+"SUM(claimproc.WriteOff) "
					+"FROM claimproc "
					+"WHERE claimproc.ProcDate >= "+POut.Date(dateFrom)+" "
					+"AND claimproc.ProcDate <= "+POut.Date(dateTo)+" "
					+"AND (claimproc.Status=1 OR claimproc.Status=4 OR claimproc.Status=0) " //received or supplemental or notreceived
					+"GROUP BY MONTH(claimproc.ProcDate)";
			}
			else {
				command="SELECT "
					+"claimproc.DateCP," 
					+"SUM(claimproc.WriteOff) "
					+"FROM claimproc "
					+"WHERE claimproc.DateCP >= "+POut.Date(dateFrom)+" "
					+"AND claimproc.DateCP <= "+POut.Date(dateTo)+" "
					+"AND (claimproc.Status=1 OR claimproc.Status=4) "//Received or supplemental
					+"GROUP BY MONTH(claimproc.DateCP)";
			}
			DataTable tableWriteoff=Db.GetTable(command);
			List<decimal> listDec=new List<decimal>();
			for(int i=0;i<12;i++) {
				decimal prod=0;
				decimal adjust=0;
				decimal inswriteoff=0;
				DateTime datePeriod=dateFrom.AddMonths(i);//only the month and year are important
				for(int j=0;j<tableProduction.Rows.Count;j++)  {
				  if(datePeriod.Year==PIn.Date(tableProduction.Rows[j][0].ToString()).Year
						&& datePeriod.Month==PIn.Date(tableProduction.Rows[j][0].ToString()).Month)
					{
		 			  prod+=PIn.Decimal(tableProduction.Rows[j][1].ToString());
					}
   			}
				for(int j=0;j<tableAdj.Rows.Count; j++){
				  if(datePeriod.Year==PIn.Date(tableAdj.Rows[j][0].ToString()).Year
						&& datePeriod.Month==PIn.Date(tableAdj.Rows[j][0].ToString()).Month)
					{
						adjust+=PIn.Decimal(tableAdj.Rows[j][1].ToString());
					}
   			}
				for(int j=0;j<tableWriteoff.Rows.Count; j++){
					if(datePeriod.Year==PIn.Date(tableWriteoff.Rows[j][0].ToString()).Year
						&& datePeriod.Month==PIn.Date(tableWriteoff.Rows[j][0].ToString()).Month)
					{
						inswriteoff-=PIn.Decimal(tableWriteoff.Rows[j][1].ToString());
					}
				}
				listDec.Add(prod+adjust-inswriteoff);
			}
			return listDec;
		}

	}
}
