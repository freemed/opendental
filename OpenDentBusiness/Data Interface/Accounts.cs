using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	
	public class Accounts {
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * from account "
				+" ORDER BY AcctType,Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Account";
			FillCache(table);//on the server side
			return table;
		}

		private static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			AccountC.ListLong=new Account[table.Rows.Count];
			ArrayList AL=new ArrayList();
			for(int i=0;i<AccountC.ListLong.Length;i++) {
				AccountC.ListLong[i]=new Account();
				AccountC.ListLong[i].AccountNum  = PIn.Long(table.Rows[i][0].ToString());
				AccountC.ListLong[i].Description = PIn.String(table.Rows[i][1].ToString());
				AccountC.ListLong[i].AcctType    = (AccountType)PIn.Int(table.Rows[i][2].ToString());
				AccountC.ListLong[i].BankNumber  = PIn.String(table.Rows[i][3].ToString());
				AccountC.ListLong[i].Inactive    = PIn.Bool(table.Rows[i][4].ToString());
				AccountC.ListLong[i].AccountColor= Color.FromArgb(PIn.Int(table.Rows[i][5].ToString()));
				if(!AccountC.ListLong[i].Inactive) {
					AL.Add(AccountC.ListLong[i].Clone());
				}
			}
			AccountC.ListShort=new Account[AL.Count];
			AL.CopyTo(AccountC.ListShort);
		}

		///<summary></summary>
		public static long Insert(Account acct) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				acct.AccountNum=Meth.GetLong(MethodBase.GetCurrentMethod(),acct);
				return acct.AccountNum;
			}
			if(PrefC.RandomKeys) {
				acct.AccountNum=ReplicationServers.GetKey("account","AccountNum");
			}
			string command="INSERT INTO account (";
			if(PrefC.RandomKeys) {
				command+="AccountNum,";
			}
			command+="Description,AcctType,BankNumber,Inactive,AccountColor) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(acct.AccountNum)+"', ";
			}
			command+=
				 "'"+POut.String(acct.Description)+"', "
				+"'"+POut.Long   ((int)acct.AcctType)+"', "
				+"'"+POut.String(acct.BankNumber)+"', "
				+"'"+POut.Bool  (acct.Inactive)+"', "
				+"'"+POut.Long   (acct.AccountColor.ToArgb())+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				acct.AccountNum=Db.NonQ(command,true);
			}
			return acct.AccountNum;
		}

		///<summary></summary>
		public static void Update(Account acct) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),acct);
				return;
			}
			string command= "UPDATE account SET "
				+"Description = '"  +POut.String(acct.Description)+"' "
				+",AcctType = '"    +POut.Long   ((int)acct.AcctType)+"' "
				+",BankNumber = '"  +POut.String(acct.BankNumber)+"' "
				+",Inactive = '"    +POut.Bool  (acct.Inactive)+"' "
				+",AccountColor = '"+POut.Long   (acct.AccountColor.ToArgb())+"' "
				+"WHERE AccountNum = '"+POut.Long(acct.AccountNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Throws exception if account is in use.</summary>
		public static void Delete(Account acct) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),acct);
				return;
			}
			//check to see if account has any journal entries
			string command="SELECT COUNT(*) FROM journalentry WHERE AccountNum="+POut.Long(acct.AccountNum);
			if(Db.GetCount(command)!="0"){
				throw new ApplicationException(Lans.g("FormAccountEdit",
					"Not allowed to delete an account with existing journal entries."));
			}
			//Check various preference entries
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingDepositAccounts'";
			string result=Db.GetCount(command);
			string[] strArray=result.Split(new char[] {','});
			for(int i=0;i<strArray.Length;i++){
				if(strArray[i]==acct.AccountNum.ToString()){
					throw new ApplicationException(Lans.g("FormAccountEdit","Account is in use in the setup section."));
				}
			}
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingIncomeAccount'";
			result=Db.GetCount(command);
			if(result==acct.AccountNum.ToString()) {
				throw new ApplicationException(Lans.g("FormAccountEdit","Account is in use in the setup section."));
			}
			command="SELECT ValueString FROM preference WHERE PrefName='AccountingCashIncomeAccount'";
			result=Db.GetCount(command);
			if(result==acct.AccountNum.ToString()) {
				throw new ApplicationException(Lans.g("FormAccountEdit","Account is in use in the setup section."));
			}
			//check AccountingAutoPay entries
			for(int i=0;i<AccountingAutoPayC.AList.Count;i++){
				strArray=((AccountingAutoPay)AccountingAutoPayC.AList[i]).PickList.Split(new char[] { ',' });
				for(int s=0;s<strArray.Length;s++){
					if(strArray[s]==acct.AccountNum.ToString()){
						throw new ApplicationException(Lans.g("FormAccountEdit","Account is in use in the setup section."));
					}
				}
			}
			command="DELETE FROM account WHERE AccountNum = "+POut.Long(acct.AccountNum);
			Db.NonQ(command);
		}

		///<summary>Used to test the sign on debits and credits for the five different account types</summary>
		public static bool DebitIsPos(AccountType type){
			//No need to check RemotingRole; no call to db.
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
		public static double GetBalance(long accountNum,AccountType acctType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<double>(MethodBase.GetCurrentMethod(),accountNum,acctType);
			}
			string command="SELECT SUM(DebitAmt),SUM(CreditAmt) FROM journalentry "
				+"WHERE AccountNum="+POut.Long(accountNum)
				+" GROUP BY AccountNum";
			DataTable table=Db.GetTable(command);
			double debit=0;
			double credit=0;
			if(table.Rows.Count>0){
				debit=PIn.Double(table.Rows[0][0].ToString());
				credit=PIn.Double(table.Rows[0][1].ToString());
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
			//No need to check RemotingRole; no call to db.
			string depAccounts=PrefC.GetString(PrefName.AccountingDepositAccounts);
			if(depAccounts==""){
				return false;
			}
			if(PrefC.GetLong(PrefName.AccountingIncomeAccount)==0){
				return false;
			}
			//might add a few more checks later.
			return true;
		}

		///<summary>Checks the loaded prefs and accountingAutoPays to see if user has setup auto pay linking.  Returns true if so.</summary>
		public static bool PaymentsLinked() {
			//No need to check RemotingRole; no call to db.
			if(AccountingAutoPayC.AList.Count==0){
				return false;
			}
			if(PrefC.GetLong(PrefName.AccountingIncomeAccount)==0) {
				return false;
			}
			//might add a few more checks later.
			return true;
		}

		///<summary></summary>
		public static long[] GetDepositAccounts(){
			//No need to check RemotingRole; no call to db.
			string depStr=PrefC.GetString(PrefName.AccountingDepositAccounts);
			string[] depStrArray=depStr.Split(new char[] { ',' });
			ArrayList depAL=new ArrayList();
			for(int i=0;i<depStrArray.Length;i++) {
				if(depStrArray[i]=="") {
					continue;
				}
				depAL.Add(PIn.Long(depStrArray[i]));
			}
			long[] retVal=new long[depAL.Count];
			depAL.CopyTo(retVal);
			return retVal;
		}

		///<summary>Gets the full list to display in the Chart of Accounts, including balances.</summary>
		public static DataTable GetFullList(DateTime asOfDate, bool showInactive){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),asOfDate,showInactive);
			}
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
				+"DateDisplayed <= "+POut.Date(asOfDate)+" WHERE AcctType<=2 ";
			if(!showInactive){
				command+="AND Inactive=0 ";
			}
			command+="GROUP BY account.AccountNum, account.AcctType, account.Description, account.BankNumber,"
				+"account.Inactive, account.AccountColor ORDER BY AcctType, Description";
			DataTable rawTable=Db.GetTable(command);
			AccountType aType;
			double debit=0;
			double credit=0;
			for(int i=0;i<rawTable.Rows.Count;i++){
				row=table.NewRow();
				aType=(AccountType)PIn.Long(rawTable.Rows[i]["AcctType"].ToString());
				row["type"]=Lans.g("enumAccountType",aType.ToString());
				row["Description"]=rawTable.Rows[i]["Description"].ToString();
				debit=PIn.Double(rawTable.Rows[i]["SumDebit"].ToString());
				credit=PIn.Double(rawTable.Rows[i]["SumCredit"].ToString());
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
				+"AND DateDisplayed < "+POut.Date(firstofYear)//all from previous years
				+" AND (AcctType=3 OR AcctType=4) "//income or expenses
				+"GROUP BY AcctType ORDER BY AcctType";//income first, but could return zero rows.
			rawTable=Db.GetTable(command);
			double balance=0;
			for(int i=0;i<rawTable.Rows.Count;i++){
				aType=(AccountType)PIn.Long(rawTable.Rows[i]["AcctType"].ToString());
				debit=PIn.Double(rawTable.Rows[i]["SumDebit"].ToString());
				credit=PIn.Double(rawTable.Rows[i]["SumCredit"].ToString());
				//this works for both income and expenses, because we are subracting expenses, so signs cancel
				balance+=credit-debit;
			}
			row=table.NewRow();
			row["type"]=Lans.g("enumAccountType",AccountType.Equity.ToString());
			row["Description"]=Lans.g("Accounts","Retained Earnings (auto)");
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
				+"AND DateDisplayed <= "+POut.Date(asOfDate)
				+" AND DateDisplayed >= "+POut.Date(firstofYear)//only for this year
				+" WHERE (AcctType=3 OR AcctType=4) ";
			if(!showInactive) {
				command+="AND Inactive=0 ";
			}
			command+="GROUP BY account.AccountNum, account.AcctType, account.Description, account.BankNumber,"
				+"account.Inactive, account.AccountColor ORDER BY AcctType, Description";
			rawTable=Db.GetTable(command);
			for(int i=0;i<rawTable.Rows.Count;i++) {
				row=table.NewRow();
				aType=(AccountType)PIn.Long(rawTable.Rows[i]["AcctType"].ToString());
				row["type"]=Lans.g("enumAccountType",aType.ToString());
				row["Description"]=rawTable.Rows[i]["Description"].ToString();
				debit=PIn.Double(rawTable.Rows[i]["SumDebit"].ToString());
				credit=PIn.Double(rawTable.Rows[i]["SumCredit"].ToString());
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




