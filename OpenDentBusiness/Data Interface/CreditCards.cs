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
		public static DataTable GetRecurringChargeList(int payType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),payType);
			}
			DataTable table=new DataTable();
			//This query will return patient information and the latest recurring payment whom:
			//	-have recurring charges setup and today's date falls within the start and stop range they are setup.
			//	-have a total balance >= recurring charge amount
			//NOTE: Query will return patients with or without payments regardless of when that payment occurred, filtering is done below.
			string command="SELECT cc.PatNum,"+DbHelper.Concat("pat.LName","', '","pat.FName")+" PatName,"
					+"guar.BalTotal-guar.InsEst FamBalTotal,CASE WHEN MAX(pay.PayDate) IS NULL THEN DATE('0001-01-01') ELSE MAX(pay.PayDate) END LatestPayment,"
					+"cc.DateStart,cc.Address,cc.Zip,cc.XChargeToken,cc.CCNumberMasked,cc.CCExpiration,cc.ChargeAmt "
					+"FROM (creditcard cc,patient pat,patient guar) "
					+"LEFT JOIN payment pay ON cc.PatNum=pay.PatNum AND pay.PayType="+payType+" AND cc.DateStart<pay.PayDate AND pay.IsRecurringCC=1 "
					+"WHERE cc.PatNum=pat.PatNum "
					+"AND pat.Guarantor=guar.PatNum "
					+"AND cc.ChargeAmt>0 "
					+"AND cc.DateStart<="+DbHelper.Curdate()+" "
					+"AND (cc.DateStop>="+DbHelper.Curdate()+" OR YEAR(cc.DateStop)<1880) "
					+"AND guar.BalTotal-guar.InsEst>=cc.ChargeAmt "
					+"GROUP BY cc.CreditCardNum,"+DbHelper.Concat("pat.LName","', '","pat.FName")+",PatName,guar.BalTotal-guar.InsEst,"
					+"cc.Address,cc.Zip,cc.XChargeToken,cc.CCNumberMasked,cc.CCExpiration,cc.ChargeAmt";
			table=Db.GetTable(command);
			DateTime curDate=MiscData.GetNowDateTime();
			//Loop through table and remove patients that do not need to be charged yet.
			for(int i=0;i<table.Rows.Count;i++) {
				DateTime latestPayment=PIn.Date(table.Rows[i]["LatestPayment"].ToString());
				DateTime dateStart=PIn.Date(table.Rows[i]["DateStart"].ToString());
				//Always charge if more than 30 days has passed since last charge.
				if(curDate>latestPayment.AddDays(30)) {
					continue;
				}
				//Not enough days in the current month so show on the last day of the month
				//Example: DateStart=8/31/2010 and the current month is February 2011 which does not have 31 days.
				//So the patient needs to show in list if current day is the 28th (or last day of the month).
				int daysInMonth=DateTime.DaysInMonth(curDate.Year,curDate.Month);
				if(daysInMonth<=dateStart.Day && daysInMonth==curDate.Day) {
					continue;
				}
				if(curDate.Day>=dateStart.Day) {
					//No payment entries in the same month then charge.
					if(curDate.Month>latestPayment.Month || curDate.Year>latestPayment.Year) {//The next month.  For December that will be next year.
						continue;
					}
				}
				else {//Current date is before the recurring date in the current month.
					//Check if payment didn't happen last month.
					if(curDate.AddMonths(-1).Month!=latestPayment.Month){
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
			return table;
		}


	}
}