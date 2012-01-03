using System;
using System.Collections.Generic;
//using System.Windows.Controls;//need a reference for this dll, or get msgbox into UI layer.
using System.Data;
using System.Text;
using System.Reflection;

namespace OpenDentBusiness {
	public class DashboardQueries {
		public static DataTable GetProvList(DateTime dt) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),dt);
			}
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
				AND procedurelog.ProcStatus="+POut.Int((int)ProcStat.C)+@"
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

		public static List<System.Windows.Media.Color> GetProdProvColors() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<System.Windows.Media.Color>>(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT ProvColor
				FROM provider WHERE IsHidden=0
				ORDER BY ItemOrder";
			DataTable table=Db.GetTable(command);
			List<System.Windows.Media.Color> retVal=new List<System.Windows.Media.Color>();
			for(int i=0;i<table.Rows.Count;i++){
				System.Drawing.Color dColor=System.Drawing.Color.FromArgb(PIn.Int(table.Rows[i]["ProvColor"].ToString()));
				System.Windows.Media.Color mColor=System.Windows.Media.Color.FromArgb(dColor.A,dColor.R,dColor.G,dColor.B);
				retVal.Add(mColor);
			}
			return retVal;
		}

		public static List<List<int>> GetProdProvs(DateTime dateFrom,DateTime dateTo) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<List<int>>>(MethodBase.GetCurrentMethod(),dateFrom,dateTo);
			}
			string command;
			command="DROP TABLE IF EXISTS tempdash;";
			Db.NonQ(command);
			//this table will contain approx 12x3xProv rows if there was production for each prov in each month.
			command=@"CREATE TABLE tempdash (
				DatePeriod date NOT NULL,
				ProvNum bigint NOT NULL,
				production decimal NOT NULL
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			//procs. Inserts approx 12xProv rows
			command=@"INSERT INTO tempdash
				SELECT procedurelog.ProcDate,procedurelog.ProvNum,
				SUM(procedurelog.ProcFee*(procedurelog.UnitQty+procedurelog.BaseUnits))-IFNULL(SUM(claimproc.WriteOff),0)
				FROM procedurelog
				LEFT JOIN claimproc ON procedurelog.ProcNum=claimproc.ProcNum
				AND claimproc.Status='7' /*only CapComplete writeoffs are subtracted here*/
				WHERE procedurelog.ProcStatus = '2'
				AND procedurelog.ProcDate >= "+POut.Date(dateFrom)+@"
				AND procedurelog.ProcDate <= "+POut.Date(dateTo)+@"
				GROUP BY procedurelog.ProvNum,MONTH(procedurelog.ProcDate)";
			Db.NonQ(command);
			
			//todo 2 more tables


			//get all the data as 12xProv rows
			command=@"SELECT DatePeriod,ProvNum,SUM(production) prod
				FROM tempdash 
				GROUP BY ProvNum,MONTH(DatePeriod)";//this fails with date issue
			DataTable tableProd=Db.GetTable(command);
			command="DROP TABLE IF EXISTS tempdash;";
			Db.NonQ(command);
			command=@"SELECT ProvNum
				FROM provider WHERE IsHidden=0
				ORDER BY ItemOrder";
			DataTable tableProv=Db.GetTable(command);
			List<List<int>> retVal=new List<List<int>>();
			for(int p=0;p<tableProv.Rows.Count;p++){//loop through each provider
				long provNum=PIn.Long(tableProv.Rows[p]["ProvNum"].ToString());
				List<int> listInt=new List<int>();//12 items
				for(int i=0;i<12;i++) {
					decimal prod=0;
					DateTime datePeriod=dateFrom.AddMonths(i);//only the month and year are important
					for(int j=0;j<tableProd.Rows.Count;j++)  {
						if(datePeriod.Year==PIn.Date(tableProd.Rows[j]["DatePeriod"].ToString()).Year
							&& datePeriod.Month==PIn.Date(tableProd.Rows[j]["DatePeriod"].ToString()).Month
							&& provNum==PIn.Long(tableProd.Rows[j]["ProvNum"].ToString()))
						{
		 					prod=PIn.Decimal(tableProd.Rows[j]["prod"].ToString());
							break;
						}
   				}
					listInt.Add((int)(prod));
				}
				retVal.Add(listInt);
			}
			return retVal;
		}

		///<summary>Only one dimension to the list for now.</summary>
		public static List<List<int>> GetAR(DateTime dateFrom,DateTime dateTo,List<DashboardAR> listDashAR) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<List<int>>>(MethodBase.GetCurrentMethod(),dateFrom,dateTo,listDashAR);
			}
			//assumes that dateFrom is the first of the month and that there are 12 periods
			//listDashAR may be empty, in which case, this routine will take about 18 seconds, but the user was warned.
			//listDashAR may also me incomplete, especially the most recent month(s).
			string command;
			List<int> listInt;
			listInt=new List<int>();
			bool agingWasRun=false;
			for(int i=0;i<12;i++) {
				DateTime dateLastOfMonth=dateFrom.AddMonths(i+1).AddDays(-1);
				DashboardAR dash=null;
				for(int d=0;d<listDashAR.Count;d++) {
					if(listDashAR[d].DateCalc!=dateLastOfMonth) {
						continue;
					}
					dash=listDashAR[d];
				}
				if(dash!=null) {//we found a DashboardAR object from the database for this month, so use it.
					listInt.Add((int)dash.BalTotal);
					continue;
				}
				agingWasRun=true;
				//run historical aging on all patients based on the date entered.
				Ledgers.ComputeAging(0,dateLastOfMonth,true);
				command=@"SELECT SUM(Bal_0_30+Bal_31_60+Bal_61_90+BalOver90),SUM(InsEst) FROM patient";
				DataTable table=Db.GetTable(command);
				dash=new DashboardAR();
				dash.DateCalc=dateLastOfMonth;
				dash.BalTotal=PIn.Double(table.Rows[0][0].ToString());
				dash.InsEst=PIn.Double(table.Rows[0][1].ToString());
				DashboardARs.Insert(dash);//save it to the db for later.
				listInt.Add((int)dash.BalTotal);//and also use it now.
			}
			if(agingWasRun) {
				Ledgers.RunAging();//set aging back to normal
			}
			List<List<int>> retVal=new List<List<int>>();
			retVal.Add(listInt);
			return retVal;
		}

		public static List<List<int>> GetProdInc(DateTime dateFrom,DateTime dateTo) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<List<int>>>(MethodBase.GetCurrentMethod(),dateFrom,dateTo);
			}
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
			command="SELECT "
				+"paysplit.DatePay,"
				+"SUM(paysplit.SplitAmt) "
				+"FROM paysplit "
				+"WHERE paysplit.IsDiscount=0 "
				+"AND paysplit.DatePay >= "+POut.Date(dateFrom)+" "
				+"AND paysplit.DatePay <= "+POut.Date(dateTo)+" "
				+"GROUP BY MONTH(paysplit.DatePay)";
			DataTable tablePay=Db.GetTable(command);
			command="SELECT claimpayment.CheckDate,SUM(claimproc.InsPayamt) "
				+"FROM claimpayment,claimproc WHERE "
				+"claimproc.ClaimPaymentNum = claimpayment.ClaimPaymentNum "
				+"AND claimpayment.CheckDate >= " + POut.Date(dateFrom)+" "
				+"AND claimpayment.CheckDate <= " + POut.Date(dateTo)+" "
				+" GROUP BY claimpayment.CheckDate ORDER BY checkdate";
			DataTable tableIns=Db.GetTable(command);
			//production--------------------------------------------------------------------
			List<int> listInt;
			listInt=new List<int>();
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
				listInt.Add((int)(prod+adjust-inswriteoff));
			}
			List<List<int>> retVal=new List<List<int>>();
			retVal.Add(listInt);
			//income----------------------------------------------------------------------
			listInt=new List<int>();
			for(int i=0;i<12;i++) {
				decimal ptincome=0;
				decimal insincome=0;
				DateTime datePeriod=dateFrom.AddMonths(i);//only the month and year are important
				for(int j=0;j<tablePay.Rows.Count;j++) {
					if(datePeriod.Year==PIn.Date(tablePay.Rows[j][0].ToString()).Year
						&& datePeriod.Month==PIn.Date(tablePay.Rows[j][0].ToString()).Month) 
					{
						ptincome+=PIn.Decimal(tablePay.Rows[j][1].ToString());
					}
				}
				for(int j=0;j<tableIns.Rows.Count;j++) {//
					if(datePeriod.Year==PIn.Date(tableIns.Rows[j][0].ToString()).Year
						&& datePeriod.Month==PIn.Date(tableIns.Rows[j][0].ToString()).Month) 
					{
						insincome+=PIn.Decimal(tableIns.Rows[j][1].ToString());
					}
				}
				listInt.Add((int)(ptincome+insincome));
			}
			retVal.Add(listInt);
			return retVal;
		}

		public static List<List<int>> GetNewPatients(DateTime dateFrom,DateTime dateTo) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<List<int>>>(MethodBase.GetCurrentMethod(),dateFrom,dateTo);
			}
			string command;
			command="DROP TABLE IF EXISTS tempdash;";
			Db.NonQ(command);
			command=@"CREATE TABLE tempdash (
				PatNum bigint NOT NULL PRIMARY KEY,
				dateFirstProc datetime NOT NULL
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			//table full of individual patients and their dateFirstProcs.
			command=@"INSERT INTO tempdash 
				SELECT PatNum, MIN(ProcDate) dateFirstProc FROM procedurelog
				WHERE ProcStatus=2 GROUP BY PatNum
				HAVING dateFirstProc >= "+POut.Date(dateFrom)+" "
				+"AND dateFirstProc <= "+POut.Date(dateTo);
			Db.NonQ(command);
			command="SELECT dateFirstProc,COUNT(*) "
				+"FROM tempdash "
				+"GROUP BY MONTH(dateFirstProc)";
			DataTable tableCounts=Db.GetTable(command);
			List<int> listInt=new List<int>();
			for(int i=0;i<12;i++) {
				int ptcount=0;
				DateTime datePeriod=dateFrom.AddMonths(i);//only the month and year are important
				for(int j=0;j<tableCounts.Rows.Count;j++) {
					if(datePeriod.Year==PIn.Date(tableCounts.Rows[j][0].ToString()).Year
						&& datePeriod.Month==PIn.Date(tableCounts.Rows[j][0].ToString()).Month)
					{
						ptcount+=PIn.Int(tableCounts.Rows[j][1].ToString());
					}
				}
				listInt.Add(ptcount);
			}
			List<List<int>> retVal=new List<List<int>>();
			retVal.Add(listInt);
			return retVal;
		}


	}
}
