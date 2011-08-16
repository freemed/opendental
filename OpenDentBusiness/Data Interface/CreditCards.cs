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
			//This query will return all patients that:
			//	-have recurring setup and today's date falls within the start and stop range.
			//	-have a total balance > recurring charge amount
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
			//We need to remove the rows within table that do not belong.
			for(int i=0;i<table.Rows.Count;i++) {
				DateTime latestPayment=PIn.Date(table.Rows[i]["LatestPayment"].ToString());
				DateTime dateStart=PIn.Date(table.Rows[i]["DateStart"].ToString());
				//Always charge if more than 30 days has passed since last charge.
				if(curDate>latestPayment.AddDays(30)) {
					continue;
				}
				if(curDate.Day>=dateStart.Day) {
					//If no payment enteries in the same month then charge.
					if((curDate.Month>latestPayment.Month || curDate.Year>latestPayment.Year)) {//The next month.  For December that will be next year.
						continue;
					}
					//Same month charge but first charge was from previous month due to a weekend, etc.
					if(curDate.Month==latestPayment.Month && latestPayment.AddDays(21)<new DateTime(curDate.Year,curDate.Month,dateStart.Day)) {
						continue;
					}
				}
				//Otherwise we need to remove this row because user does not need to be charged just yet for whatever reason.
				table.Rows.RemoveAt(i);
				i--;
			}
			return table;
		}


	}
}