using System;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace ODR{
	///<summary></summary>
	public class GetData{
		///<summary></summary>
		public static string Pref(string prefName) {
			string command="SELECT ValueString FROM preference WHERE PrefName='"+POut.PString(prefName)+"'";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			return table.Rows[0][0].ToString();
		}

		public static float NetIncome(object asOfDateObj) {
			DateTime asOfDate;
			if(asOfDateObj.GetType()==typeof(string)){
				asOfDate=PIn.PDate(asOfDateObj.ToString());
			}
			else if(asOfDateObj.GetType()==typeof(DateTime)){
				asOfDate=(DateTime)asOfDateObj;
			}
			else{
				return 0;
			}
			DateTime firstOfYear=new DateTime(asOfDate.Year,1,1);
			string command="SELECT SUM(CreditAmt), SUM(DebitAmt), AcctType "
			+"FROM journalentry,account "
			+"WHERE journalentry.AccountNum=account.AccountNum "
			+"AND DateDisplayed >= "+POut.PDate(firstOfYear)
			+" AND DateDisplayed <= "+POut.PDate(asOfDate)
			+" GROUP BY AcctType";
			DataConnection dcon=new DataConnection();
			DataTable table=dcon.GetTable(command);
			float retVal=0;
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i][2].ToString()=="3"//income
					|| table.Rows[i][2].ToString()=="4")//expense
				{
					retVal+=PIn.PFloat(table.Rows[i][0].ToString());//add credit
					retVal-=PIn.PFloat(table.Rows[i][1].ToString());//subtract debit
					//if it's an expense, we are subtracting (income-expense), but the signs cancel.
				}
			}
			return retVal;
		}

	}

	

}
