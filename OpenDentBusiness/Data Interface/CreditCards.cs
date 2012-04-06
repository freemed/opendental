using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CreditCards{

		///<summary></summary>
		public static List<CreditCard> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<CreditCard>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM creditcard WHERE PatNum = "+POut.Long(patNum)+" ORDER BY ItemOrder DESC";
			return Crud.CreditCardCrud.SelectMany(command);
		}

		///<summary>Gets one CreditCard from the db.</summary>
		public static CreditCard GetOne(long creditCardNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetObject<CreditCard>(MethodBase.GetCurrentMethod(),creditCardNum);
			}
			return Crud.CreditCardCrud.SelectOne(creditCardNum);
		}

		///<summary></summary>
		public static long Insert(CreditCard creditCard){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				creditCard.CreditCardNum=Meth.GetLong(MethodBase.GetCurrentMethod(),creditCard);
				return creditCard.CreditCardNum;
			}
			return Crud.CreditCardCrud.Insert(creditCard);
		}

		///<summary></summary>
		public static void Update(CreditCard creditCard){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				Meth.GetVoid(MethodBase.GetCurrentMethod(),creditCard);
				return;
			}
			Crud.CreditCardCrud.Update(creditCard);
		}

		///<summary></summary>
		public static void Delete(long creditCardNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),creditCardNum);
				return;
			}
			string command= "DELETE FROM creditcard WHERE CreditCardNum = "+POut.Long(creditCardNum);
			Db.NonQ(command);
		}

		///<summary>Returns list of credit cards that are ready for a recurring charge.</summary>
		public static DataTable GetRecurringChargeList() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod());
			}
			DataTable table=new DataTable();
			//This query will return patient information and the latest recurring payment whom:
			//	-have recurring charges setup and today's date falls within the start and stop range.
			//	-have a total balance >= recurring charge amount
			//NOTE: Query will return patients with or without payments regardless of when that payment occurred, filtering is done below.
			string command="SELECT PatNum,PatName,FamBalTotal,LatestPayment,DateStart,Address,Zip,XChargeToken,CCNumberMasked,CCExpiration,ChargeAmt,PayPlanNum "
				+"FROM (";
			#region Payments
			command+="(SELECT 1,cc.PatNum,"+DbHelper.Concat("pat.LName","', '","pat.FName")+" PatName,"//The 'SELECT 1' garuntees the UNION will not combine results with payment plans.
				+"guar.BalTotal-guar.InsEst FamBalTotal,CASE WHEN MAX(pay.PayDate) IS NULL THEN "+POut.Date(new DateTime(1,1,1))+" ELSE MAX(pay.PayDate) END LatestPayment,"
				+"cc.DateStart,cc.Address,cc.Zip,cc.XChargeToken,cc.CCNumberMasked,cc.CCExpiration,cc.ChargeAmt,cc.PayPlanNum,cc.DateStop "
				+"FROM (creditcard cc,patient pat,patient guar) "
				+"LEFT JOIN payment pay ON cc.PatNum=pay.PatNum AND pay.IsRecurringCC=1 "
				+"WHERE cc.PatNum=pat.PatNum "
				+"AND pat.Guarantor=guar.PatNum "
				+"AND cc.PayPlanNum=0 ";//Keeps card from showing up in case they have a balance AND is setup for payment plan. 
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command+="GROUP BY cc.CreditCardNum) ";
			}
			else {//Oracle
				command+="GROUP BY cc.CreditCardNum,cc.PatNum,"+DbHelper.Concat("pat.LName","', '","pat.FName")+",PatName,guar.BalTotal-guar.InsEst,"
					+"cc.Address,cc.Zip,cc.XChargeToken,cc.CCNumberMasked,cc.CCExpiration,cc.ChargeAmt,cc.PayPlanNum,cc.DateStop) ";
			}
			#endregion
			command+="UNION ";
			#region Payment Plans
			command+="(SELECT 2,cc.PatNum,"+DbHelper.Concat("pat.LName","', '","pat.FName")+" PatName,";//The 'SELECT 2' garuntees the UNION will not combine results with payments.
			//Special select statement to figure out how much is owed on a particular payment plan.  This total amount will be Labeled as FamBalTotal for UNION purposes.
			command+="(SELECT CASE WHEN SUM(ppc.Principal+ppc.Interest) IS NULL THEN 0 ELSE SUM(ppc.Principal+ppc.Interest) END "
				+"FROM PayPlanCharge ppc "
				+"WHERE ppc.ChargeDate <= "+DbHelper.Curdate()+" AND ppc.PayPlanNum=cc.PayPlanNum) "
				+"- CASE WHEN SUM(ps.SplitAmt) IS NULL THEN 0 ELSE SUM(ps.SplitAmt) END FamBalTotal,";
			command+="CASE WHEN MAX(ps.DatePay) IS NULL THEN "+POut.Date(new DateTime(1,1,1))+" ELSE MAX(pay.PayDate) END LatestPayment,"
				+"cc.DateStart,cc.Address,cc.Zip,cc.XChargeToken,cc.CCNumberMasked,cc.CCExpiration,cc.ChargeAmt,cc.PayPlanNum,cc.DateStop "
				+"FROM creditcard cc "
				+"INNER JOIN patient pat ON pat.PatNum=cc.PatNum "
				+"LEFT JOIN paysplit ps ON ps.PayPlanNum=cc.PayPlanNum AND ps.PayPlanNum<>0 "
				+"LEFT JOIN payment pay ON pay.PayNum=ps.PayNum AND pay.IsRecurringCC=1 "
				+"WHERE cc.PayPlanNum<>0 ";
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command+="GROUP BY cc.CreditCardNum) ";
			}
			else {//Oracle
				command+="GROUP BY cc.CreditCardNum,cc.PatNum,"+DbHelper.Concat("pat.LName","', '","pat.FName")+",PatName,guar.BalTotal-guar.InsEst,"
					+"cc.Address,cc.Zip,cc.XChargeToken,cc.CCNumberMasked,cc.CCExpiration,cc.ChargeAmt,cc.PayPlanNum,cc.DateStop) ";
			}
			#endregion
			//Now we have all the results for payments and payment plans, so do an obvious filter. A more thorough filter happens later.
			command+=") due "
				+"WHERE FamBalTotal>=ChargeAmt "
				+"AND ChargeAmt>0 "
				+"AND DateStart<="+DbHelper.Curdate()+" "
				+"AND (DateStop>="+DbHelper.Curdate()+" OR YEAR(DateStop)<1880) ";
			table=Db.GetTable(command);
			FilterRecurringChargeList(table);
			return table;
		}

		///<summary>Talbe must include columns labeled LatestPayment and DateStart.</summary>
		public static void FilterRecurringChargeList(DataTable table) {
			DateTime curDate=MiscData.GetNowDateTime();
			//Loop through table and remove patients that do not need to be charged yet.
			for(int i=0;i<table.Rows.Count;i++) {
				DateTime latestPayment=PIn.Date(table.Rows[i]["LatestPayment"].ToString());
				DateTime dateStart=PIn.Date(table.Rows[i]["DateStart"].ToString());
				if(curDate>latestPayment.AddDays(31)) {//if it's been more than a month since they made any sort of payment
					//if we reduce the days below 31, then slighly more people will be charged, especially from Feb to March.  31 eliminates those false positives.
					continue;//charge them
				}
				//Not enough days in the current month so show on the last day of the month
				//Example: DateStart=8/31/2010 and the current month is February 2011 which does not have 31 days.
				//So the patient needs to show in list if current day is the 28th (or last day of the month).
				int daysInMonth=DateTime.DaysInMonth(curDate.Year,curDate.Month);
				if(daysInMonth<=dateStart.Day && daysInMonth==curDate.Day && curDate.Date!=latestPayment.Date) {//if their recurring charge would fall on an invalid day of the month, and this is that last day of the month
					continue;//we want them to show because the charge should go in on this date.
				}
				if(curDate.Day>=dateStart.Day) {//If the recurring charge date was earlier in this month, then the recurring charge will go in for this month.
					if(curDate.Month>latestPayment.Month || curDate.Year>latestPayment.Year) {//if the latest payment was last month (or earlier).  The year check catches December
						continue;//No payments were made this month, so charge.
					}
				}
				else {//Else, current date is before the recurring date in the current month, so the recurring charge will be going in for last month
					//Check if payment didn't happen last month.
					if(curDate.AddMonths(-1).Month!=latestPayment.Month && curDate.Date!=latestPayment.Date) {
						//Charge did not happen last month so the patient needs to show up in list.
						//Example: Last month had a recurring charge set at the end of the month that fell on a weekend.
						//Today is the next month and still before the recurring charge date. 
						//This will allow the charge for the previous month to happen if the 30 day check didn't catch it above.
						continue;
					}
				}
				//Patient doesn't need to be charged yet so remove from the table.
				table.Rows.RemoveAt(i);
				i--;
			}
		}

		///<summary>Checks if token is in use.  This happened once and can cause the wrong card to be charged.</summary>
		public static bool IsDuplicateXChargeToken(string token) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb){
				return Meth.GetBool(MethodBase.GetCurrentMethod(),token);
			}
			string command="SELECT COUNT(*) FROM creditcard WHERE XChargeToken='"+POut.String(token)+"'";
			if(Db.GetCount(command)=="1") {
				return false;
			}
			return true;
		}

	}
}