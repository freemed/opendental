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
			DataRow row;
			table.Columns.Add("PatNum");
			table.Columns.Add("PatName");
			table.Columns.Add("balTotal");
			table.Columns.Add("ChargeAmt");
			string command="SELECT creditcard.PatNum,"+DbHelper.Concat("patient.LName","', '","patient.FName")+" PatName,patient.BalTotal,creditcard.ChargeAmt "
					+"FROM creditcard "
					+"LEFT JOIN patient ON creditcard.PatNum=patient.PatNum "
					+"LEFT JOIN payment ON creditcard.PatNum=payment.PatNum "
					+"WHERE payment.PayType="+payType+" "
					+"AND creditcard.DateStart<payment.PayDate "
					+"AND DATE_ADD(payment.PayDate,INTERVAL 1 MONTH) < NOW() "
					+"AND patient.BalTotal>=creditcard.ChargeAmt";
			DataTable rawTable=Db.GetTable(command);
			for(int i=0;i<rawTable.Rows.Count;i++) {
				row=table.NewRow();
				row["balTotal"]=PIn.Double(rawTable.Rows[i]["BalTotal"].ToString()).ToString("N");
				row["ChargeAmt"]=PIn.Double(rawTable.Rows[i]["ChargeAmt"].ToString()).ToString("N");
				row["PatName"]=rawTable.Rows[i]["PatName"].ToString();
				row["PatNum"]=rawTable.Rows[i]["PatNum"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}


	}
}