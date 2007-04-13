using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary>The two lists get refreshed the first time they are needed rather than at startup.</summary>
	public class Accounts {
		private static Account[] listLong;
		private static Account[] listShort;

		///<summary></summary>
		public static Account[] ListLong {
			get {
				if(listLong==null) {
					Refresh();
				}
				return listLong;
			}
			/*set {
				title=value;
			}*/
		}

		///<summary>Used for display. Does not include inactive</summary>
		public static Account[] ListShort {
			get {
				if(listShort==null) {
					Refresh();
				}
				return listShort;
			}
		}

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * from account "
				+" ORDER BY AcctType,Description";
			DataTable table=General.GetTable(command);
			listLong=new Account[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<listLong.Length;i++) {
				listLong[i]=new Account();
				listLong[i].AccountNum  = PIn.PInt(table.Rows[i][0].ToString());
				listLong[i].Description = PIn.PString(table.Rows[i][1].ToString());
				listLong[i].AcctType    = (AccountType)PIn.PInt(table.Rows[i][2].ToString());
				listLong[i].BankNumber  = PIn.PString(table.Rows[i][3].ToString());
				listLong[i].Inactive    = PIn.PBool(table.Rows[i][4].ToString());
				listLong[i].AccountColor= Color.FromArgb(PIn.PInt(table.Rows[i][5].ToString()));
				if(!listLong[i].Inactive) {
					AL.Add(listLong[i].Copy());
				}
			}
			listShort=new Account[AL.Count];
			AL.CopyTo(listShort);
		}

		///<summary></summary>
		public static void Insert(Account acct) {
			if(PrefB.RandomKeys) {
				acct.AccountNum=MiscData.GetKey("account","AccountNum");
			}
			string command="INSERT INTO account (";
			if(PrefB.RandomKeys) {
				command+="AccountNum,";
			}
			command+="Description,AcctType,BankNumber,Inactive,AccountColor) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(acct.AccountNum)+"', ";
			}
			command+=
				 "'"+POut.PString(acct.Description)+"', "
				+"'"+POut.PInt   ((int)acct.AcctType)+"', "
				+"'"+POut.PString(acct.BankNumber)+"', "
				+"'"+POut.PBool  (acct.Inactive)+"', "
				+"'"+POut.PInt   (acct.AccountColor.ToArgb())+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				acct.AccountNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(Account acct) {
			string command= "UPDATE account SET "
				+"Description = '"  +POut.PString(acct.Description)+"' "
				+",AcctType = '"    +POut.PInt   ((int)acct.AcctType)+"' "
				+",BankNumber = '"  +POut.PString(acct.BankNumber)+"' "
				+",Inactive = '"    +POut.PBool  (acct.Inactive)+"' "
				+",AccountColor = '"+POut.PInt   (acct.AccountColor.ToArgb())+"' "
				+"WHERE AccountNum = '"+POut.PInt(acct.AccountNum)+"'";
			General.NonQ(command);
		}

		///<summary>Throws exception if account is in use.</summary>
		public static void Delete(Account acct) {
			//check to see if account has any journal entries
			string command="SELECT COUNT(*) FROM journalentry WHERE AccountNum="+POut.PInt(acct.AccountNum);
			if(General.GetCount(command)!="0"){
				throw new ApplicationException(Lan.g("FormAccountEdit",
					"Not allowed to delete an account with existing journal entries."));
			}
			//Check various preference entries
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingDepositAccounts'";
			string result=General.GetCount(command);
			string[] strArray=result.Split(new char[] {','});
			for(int i=0;i<strArray.Length;i++){
				if(strArray[i]==acct.AccountNum.ToString()){
					throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
				}
			}
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingIncomeAccount'";
			result=General.GetCount(command);
			if(result==acct.AccountNum.ToString()) {
				throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
			}
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingCashIncomeAccount'";
			result=General.GetCount(command);
			if(result==acct.AccountNum.ToString()) {
				throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
			}
			//check AccountingAutoPay entries
			for(int i=0;i<AccountingAutoPays.AList.Count;i++){
				strArray=((AccountingAutoPay)AccountingAutoPays.AList[i]).PickList.Split(new char[] { ',' });
				for(int s=0;s<strArray.Length;s++){
					if(strArray[s]==acct.AccountNum.ToString()){
						throw new ApplicationException(Lan.g("FormAccountEdit","Account is in use in the setup section."));
					}
				}
			}
			command="DELETE FROM account WHERE AccountNum = "+POut.PInt(acct.AccountNum);
			General.NonQ(command);
		}

		///<summary>Loops through listLong to find a description for the specified account.  0 returns an empty string.</summary>
		public static string GetDescript(int accountNum){
			if(accountNum==0) {
				return "";
			}
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].AccountNum==accountNum){
					return ListLong[i].Description;
				}
			}
			return "";
		}

		///<summary>Loops through listLong to find an account.  Will return null if accountNum is 0.</summary>
		public static Account GetAccount(int accountNum) {
			if(accountNum==0){
				return null;
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].AccountNum==accountNum) {
					return ListLong[i].Copy();
				}
			}
			return null;//just in case
		}

		///<summary>Used to test the sign on debits and credits for the five different account types</summary>
		public static bool DebitIsPos(AccountType type){
			switch(type){
				case AccountType.Asset:
				case AccountType.Expense:
					return true;
				case AccountType.Liability:
				case AccountType.Equity://because liabilities and equity are treated the same
				case AccountType.Income:
					return false;
			}
			return true;//will never happen
		}

		///<summary>Gets the balance of an account directly from the database.</summary>
		public static double GetBalance(int accountNum,AccountType acctType){
			string command="SELECT SUM(DebitAmt),SUM(CreditAmt) FROM journalentry "
				+"WHERE AccountNum="+POut.PInt(accountNum)
				+" GROUP BY AccountNum";
			DataTable table=General.GetTable(command);
			double debit=0;
			double credit=0;
			if(table.Rows.Count>0){
				debit=PIn.PDouble(table.Rows[0][0].ToString());
				credit=PIn.PDouble(table.Rows[0][1].ToString());
			}
			if(DebitIsPos(acctType)){
				return debit-credit;
			}
			else{
				return credit-debit;
			}
			/*}
			catch {
				Debug.WriteLine(command);
				MessageBox.Show(command);
			}
			return 0;*/
		}

		///<summary>Checks the loaded prefs to see if user has setup deposit linking.  Returns true if so.</summary>
		public static bool DepositsLinked(){
			string depAccounts=PrefB.GetString("AccountingDepositAccounts");
			if(depAccounts==""){
				return false;
			}
			if(PrefB.GetInt("AccountingIncomeAccount")==0){
				return false;
			}
			//might add a few more checks later.
			return true;
		}

		///<summary>Checks the loaded prefs and accountingAutoPays to see if user has setup auto pay linking.  Returns true if so.</summary>
		public static bool PaymentsLinked() {
			if(AccountingAutoPays.AList.Count==0){
				return false;
			}
			if(PrefB.GetInt("AccountingIncomeAccount")==0) {
				return false;
			}
			//might add a few more checks later.
			return true;
		}

		///<summary></summary>
		public static int[] GetDepositAccounts(){
			string depStr=PrefB.GetString("AccountingDepositAccounts");
			string[] depStrArray=depStr.Split(new char[] { ',' });
			ArrayList depAL=new ArrayList();
			for(int i=0;i<depStrArray.Length;i++) {
				if(depStrArray[i]=="") {
					continue;
				}
				depAL.Add(PIn.PInt(depStrArray[i]));
			}
			int[] retVal=new int[depAL.Count];
			depAL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Gets the full list to display in the Chart of Accounts, including balances.</summary>
		public static DataTable GetFullList(DateTime asOfDate, bool showInactive){
			DataTable table=new DataTable("Accounts");
			DataRow row;
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("type");
			table.Columns.Add("Description");
			table.Columns.Add("balance");
			table.Columns.Add("BankNumber");
			table.Columns.Add("inactive");
			table.Columns.Add("color");
			table.Columns.Add("AccountNum");
			//but we won't actually fill this table with rows until the very end.  It's more useful to use a List<> for now.
			List<DataRow> rows=new List<DataRow>();
			//first, the entire history for the asset, liability, and equity accounts (except Retained Earnings)-----------
			string command="SELECT account.AcctType, account.Description, account.AccountNum, "
				+"SUM(DebitAmt) AS SumDebit, SUM(CreditAmt) AS SumCredit, account.BankNumber, account.Inactive, account.AccountColor "
				+"FROM account "
				+"LEFT JOIN journalentry ON journalentry.AccountNum=account.AccountNum AND "
				+"DateDisplayed <= "+POut.PDate(asOfDate)+" WHERE AcctType<=2 ";
			if(!showInactive){
				command+="AND Inactive=0 ";
			}
			command+="GROUP BY account.AccountNum, account.AcctType, account.Description, account.BankNumber,"
				+"account.Inactive, account.AccountColor ORDER BY AcctType, Description";
			DataTable rawTable=General.GetTable(command);
			AccountType aType;
			double debit=0;
			double credit=0;
			for(int i=0;i<rawTable.Rows.Count;i++){
				row=table.NewRow();
				aType=(AccountType)PIn.PInt(rawTable.Rows[i]["AcctType"].ToString());
				row["type"]=Lan.g("enumAccountType",aType.ToString());
				row["Description"]=rawTable.Rows[i]["Description"].ToString();
				debit=PIn.PDouble(rawTable.Rows[i]["SumDebit"].ToString());
				credit=PIn.PDouble(rawTable.Rows[i]["SumCredit"].ToString());
				if(DebitIsPos(aType)) {
					row["balance"]=(debit-credit).ToString("N");
				}
				else {
					row["balance"]=(credit-debit).ToString("N");
				}
				row["BankNumber"]=rawTable.Rows[i]["BankNumber"].ToString();
				if(rawTable.Rows[i]["Inactive"].ToString()=="0"){
					row["inactive"]="";
				}
				else{
					row["inactive"]="X";
				}
				row["color"]=rawTable.Rows[i]["AccountColor"].ToString();//it will be an unsigned int at this point.
				row["AccountNum"]=rawTable.Rows[i]["AccountNum"].ToString();
				rows.Add(row);
			}
			//now, the Retained Earnings (auto) account-----------------------------------------------------------------
			DateTime firstofYear=new DateTime(asOfDate.Year,1,1);
			command="SELECT AcctType, SUM(DebitAmt) AS SumDebit, SUM(CreditAmt) AS SumCredit "
				+"FROM account,journalentry "
				+"WHERE journalentry.AccountNum=account.AccountNum "
				+"AND DateDisplayed < "+POut.PDate(firstofYear)//all from previous years
				+" AND (AcctType=3 OR AcctType=4) "//income or expenses
				+"GROUP BY AcctType ORDER BY AcctType";//income first, but could return zero rows.
			rawTable=General.GetTable(command);
			double balance=0;
			for(int i=0;i<rawTable.Rows.Count;i++){
				aType=(AccountType)PIn.PInt(rawTable.Rows[i]["AcctType"].ToString());
				debit=PIn.PDouble(rawTable.Rows[i]["SumDebit"].ToString());
				credit=PIn.PDouble(rawTable.Rows[i]["SumCredit"].ToString());
				//this works for both income and expenses, because we are subracting expenses, so signs cancel
				balance+=credit-debit;
			}
			row=table.NewRow();
			row["type"]=Lan.g("enumAccountType",AccountType.Equity.ToString());
			row["Description"]=Lan.g("Accounts","Retained Earnings (auto)");
			row["balance"]=balance.ToString("N");
			row["BankNumber"]="";
			row["color"]=Color.White.ToArgb();
			row["AccountNum"]="0";
			rows.Add(row);
			//finally, income and expenses------------------------------------------------------------------------------
			command="SELECT account.AcctType, account.Description, account.AccountNum, "
				+"SUM(DebitAmt) AS SumDebit, SUM(CreditAmt) AS SumCredit, account.BankNumber, account.Inactive, account.AccountColor "
				+"FROM account "
				+"LEFT JOIN journalentry ON journalentry.AccountNum=account.AccountNum "
				+"AND DateDisplayed <= "+POut.PDate(asOfDate)
				+" AND DateDisplayed >= "+POut.PDate(firstofYear)//only for this year
				+" WHERE (AcctType=3 OR AcctType=4) ";
			if(!showInactive) {
				command+="AND Inactive=0 ";
			}
			command+="GROUP BY account.AccountNum, account.AcctType, account.Description, account.BankNumber,"
				+"account.Inactive, account.AccountColor ORDER BY AcctType, Description";
			rawTable=General.GetTable(command);
			for(int i=0;i<rawTable.Rows.Count;i++) {
				row=table.NewRow();
				aType=(AccountType)PIn.PInt(rawTable.Rows[i]["AcctType"].ToString());
				row["type"]=Lan.g("enumAccountType",aType.ToString());
				row["Description"]=rawTable.Rows[i]["Description"].ToString();
				debit=PIn.PDouble(rawTable.Rows[i]["SumDebit"].ToString());
				credit=PIn.PDouble(rawTable.Rows[i]["SumCredit"].ToString());
				if(DebitIsPos(aType)) {
					row["balance"]=(debit-credit).ToString("N");
				}
				else {
					row["balance"]=(credit-debit).ToString("N");
				}
				row["BankNumber"]=rawTable.Rows[i]["BankNumber"].ToString();
				if(rawTable.Rows[i]["Inactive"].ToString()=="0") {
					row["inactive"]="";
				}
				else {
					row["inactive"]="X";
				}
				row["color"]=rawTable.Rows[i]["AccountColor"].ToString();//it will be an unsigned int at this point.
				row["AccountNum"]=rawTable.Rows[i]["AccountNum"].ToString();
				rows.Add(row);
			}
			for(int i=0;i<rows.Count;i++) {
				table.Rows.Add(rows[i]);
			}
			return table;
		}

	}

	
}




