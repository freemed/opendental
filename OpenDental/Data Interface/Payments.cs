using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Payments {
		///<summary>Gets all payments for the specified patient. This has NOTHING to do with pay splits.  Must use pay splits for accounting.  This is only for display in Account module.</summary>
		public static Payment[] Refresh(int patNum) {
			string command=
				"SELECT * from payment"
				+" WHERE PatNum="+patNum.ToString();
			return RefreshAndFill(command);
		}

		///<summary>Get one specific payment from db.</summary>
		public static Payment GetPayment(int payNum) {
			string command=
				"SELECT * from payment"
				+" WHERE PayNum = '"+payNum+"'";
			return RefreshAndFill(command)[0];
		}

		///<summary>Get all specified payments.</summary>
		public static Payment[] GetPayments(int[] payNums) {
			if(payNums.Length==0) {
				return new Payment[0];
			}
			string command=
				"SELECT * from payment"
				+" WHERE";
			for(int i=0;i<payNums.Length;i++) {
				if(i>0) {
					command+=" OR";
				}
				command+=" PayNum="+payNums[i].ToString();
			}
			return RefreshAndFill(command);
		}

		///<summary>Gets all payments attached to a single deposit.</summary>
		public static Payment[] GetForDeposit(int depositNum) {
			string command=
				"SELECT * from payment"
				+" WHERE DepositNum = "+POut.PInt(depositNum);
			return RefreshAndFill(command);
		}

		///<summary>Gets all unattached payments for a new deposit slip.  Excludes payments before dateStart.  There is a chance payTypes might be of length 1 or even 0.</summary>
		public static Payment[] GetForDeposit(DateTime dateStart,int clinicNum,int[] payTypes) {
			string command=
				"SELECT * FROM payment "
				+"WHERE DepositNum = 0 "
				+"AND PayDate >= "+POut.PDate(dateStart)+" "
				+"AND ClinicNum="+POut.PInt(clinicNum);
			for(int i=0;i<payTypes.Length;i++) {
				if(i==0) {
					command+=" AND (";
				}
				else {
					command+=" OR ";
				}
				command+="PayType="+POut.PInt(payTypes[i]);
				if(i==payTypes.Length-1) {
					command+=")";
				}
			}
			return RefreshAndFill(command);
		}

		private static Payment[] RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			Payment[] List=new Payment[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new Payment();
				List[i].PayNum    =PIn.PInt(table.Rows[i][0].ToString());
				List[i].PayType   =PIn.PInt(table.Rows[i][1].ToString());
				List[i].PayDate   =PIn.PDate(table.Rows[i][2].ToString());
				List[i].PayAmt    =PIn.PDouble(table.Rows[i][3].ToString());
				List[i].CheckNum  =PIn.PString(table.Rows[i][4].ToString());
				List[i].BankBranch=PIn.PString(table.Rows[i][5].ToString());
				List[i].PayNote   =PIn.PString(table.Rows[i][6].ToString());
				List[i].IsSplit   =PIn.PBool(table.Rows[i][7].ToString());
				List[i].PatNum    =PIn.PInt(table.Rows[i][8].ToString());
				List[i].ClinicNum =PIn.PInt(table.Rows[i][9].ToString());
				List[i].DateEntry =PIn.PDate(table.Rows[i][10].ToString());
				List[i].DepositNum=PIn.PInt(table.Rows[i][11].ToString());
			}
			return List;
		}


		///<summary>Updates this payment.  Must make sure to update the datePay of all attached paysplits so that they are always in synch.  Also need to manually set IsSplit before here.  Will throw an exception if bad date, so surround by try-catch.</summary>
		public static void Update(Payment pay){
			if(pay.PayDate.Date>DateTime.Today) {
				throw new ApplicationException(Lan.g("Payments","Date must not be a future date."));
			}
			if(pay.PayDate.Year<1880) {
				throw new ApplicationException(Lan.g("Payments","Invalid date"));
			}
			//the functionality below needs to be taken care of before calling the function:
			/*string command="SELECT DepositNum,PayAmt FROM payment "
					+"WHERE PayNum="+POut.PInt(PayNum);
			DataConnection dcon=new DataConnection();
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0) {
				return;
			}
			if(table.Rows[0][0].ToString()!="0"//if payment is already attached to a deposit
					&& PIn.PDouble(table.Rows[0][1].ToString())!=PayAmt) {//and PayAmt changes
				throw new ApplicationException(Lan.g("Payments","Not allowed to change the amount on payments attached to deposits."));
			}*/
			string command="UPDATE payment SET " 
				+ "paytype = '"      +POut.PInt   (pay.PayType)+"'"
				+ ",paydate = "     +POut.PDate  (pay.PayDate)
				+ ",payamt = '"      +POut.PDouble(pay.PayAmt)+"'"
				+ ",checknum = '"    +POut.PString(pay.CheckNum)+"'"
				+ ",bankbranch = '"  +POut.PString(pay.BankBranch)+"'"
				+ ",paynote = '"     +POut.PString(pay.PayNote)+"'"
				+ ",issplit = '"     +POut.PBool  (pay.IsSplit)+"'"
				+ ",patnum = '"      +POut.PInt   (pay.PatNum)+"'"
				+ ",ClinicNum = '"   +POut.PInt   (pay.ClinicNum)+"'"
				//DateEntry not allowed to change
				+ ",DepositNum = '"  +POut.PInt   (pay.DepositNum)+"'"
				+" WHERE payNum = '" +POut.PInt   (pay.PayNum)+"'";
			//MessageBox.Show(string command);
 			General.NonQ(command);
			/*
			command="UPDATE paysplit SET DatePay='"+POut.PDate(PayDate)
				+"' WHERE PayNum = "+POut.PInt(PayNum);
 			General.NonQ(command);
			//set IsSplit
			command="SELECT COUNT(*) FROM paysplit WHERE PayNum="+POut.PInt(PayNum);
			DataTable table=General.GetTable(command);
			if(table.Rows[0][0].ToString()=="1"){
				command="UPDATE payment SET IsSplit=0 WHERE PayNum="+POut.PInt(PayNum);
			}
			else{
				command="UPDATE payment SET IsSplit=1 WHERE PayNum="+POut.PInt(PayNum);
			}
			General.NonQ(command);*/
		}

		///<summary>There's only one place in the program where this is called from.  Date is today, so no need to validate the date.</summary>
		public static void Insert(Payment pay){
			if(PrefB.RandomKeys){
				pay.PayNum=MiscData.GetKey("payment","PayNum");
			}
			string command= "INSERT INTO payment (";
			if(PrefB.RandomKeys){
				command+="PayNum,";
			}
			command+="PayType,PayDate,PayAmt, "
				+"CheckNum,BankBranch,PayNote,IsSplit,PatNum,ClinicNum,DateEntry,DepositNum) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(pay.PayNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (pay.PayType)+"', "
				+POut.PDate  (pay.PayDate)+", "
				+"'"+POut.PDouble(pay.PayAmt)+"', "
				+"'"+POut.PString(pay.CheckNum)+"', "
				+"'"+POut.PString(pay.BankBranch)+"', "
				+"'"+POut.PString(pay.PayNote)+"', "
				+"'"+POut.PBool  (pay.IsSplit)+"', "
				+"'"+POut.PInt   (pay.PatNum)+"', "
				+"'"+POut.PInt   (pay.ClinicNum)+"', ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}else{//Assume MySQL
				command+="NOW()";
			}
			command+=", '"+POut.PInt   (pay.DepositNum)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				pay.PayNum=General.NonQ(command,true);
			}
		}

		///<summary>Deletes the payment as well as all splits.  Surround by try catch, because it will throw an exception if trying to delete a payment attached to a deposit.</summary>
		public static void Delete(Payment pay){
			string command="SELECT DepositNum FROM payment WHERE PayNum="+POut.PInt(pay.PayNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return;
			}
			if(table.Rows[0][0].ToString()!="0"){//if payment is already attached to a deposit
				throw new ApplicationException(Lan.g("Payments","Not allowed to delete a payment attached to a deposit."));
			}
			command= "DELETE from payment WHERE payNum = '"+pay.PayNum.ToString()+"'";
 			General.NonQ(command);
			//this needs to be improved to handle EstBal
			command= "DELETE from paysplit WHERE payNum = '"+pay.PayNum.ToString()+"'";
			General.NonQ(command);
			//PaySplit[] splitList=PaySplits.RefreshPaymentList(PayNum);
			//for(int i=0;i<splitList.Length;i++){
			//	splitList[i].Delete();
			//}
		}

		///<summary>Called just before Allocate in FormPayment.butOK click.  If true, then it will prompt the user before allocating.</summary>
		public static bool AllocationRequired(double payAmt, int patNum){
			string command="SELECT EstBalance FROM patient "
				+"WHERE PatNum = "+POut.PInt(patNum);
			double estBal=PIn.PDouble(General.GetCount(command));
			if(payAmt>estBal){
				return true;
			}
			return false;
		}

		/// <summary>Only Called only from FormPayment.butOK click.  Only called if the user did not enter any splits.  Usually just adds one split for the current patient.  But if that would take the balance negative, then it loops through all other family members and creates splits for them.  It might still take the current patient negative once all other family members are zeroed out.</summary>
		public static List<PaySplit> Allocate(Payment pay){//double amtTot,int patNum,Payment payNum){
			string command= 
				"SELECT Guarantor FROM patient "
				+"WHERE PatNum = "+POut.PInt(pay.PatNum);
 			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return new List<PaySplit>();
			}
			command= 
				"SELECT PatNum,EstBalance,PriProv FROM patient "
				+"WHERE Guarantor = "+table.Rows[0][0].ToString();
				//+" ORDER BY PatNum!="+POut.PInt(pay.PatNum);//puts current patient in position 0 //Oracle does not allow
 			table=General.GetTable(command);
			List<Patient> pats=new List<Patient>();
			Patient pat;
			//first, put the current patient at position 0.
			for(int i=0;i<table.Rows.Count;i++) {
				if(table.Rows[i]["PatNum"].ToString()==pay.PatNum.ToString()){
					pat=new Patient();
					pat.PatNum    = PIn.PInt(table.Rows[i][0].ToString());
					pat.EstBalance= PIn.PDouble(table.Rows[i][1].ToString());
					pat.PriProv   = PIn.PInt(table.Rows[i][2].ToString());
					pats.Add(pat.Copy());
				}
			}
			//then, do all the rest of the patients.
			for(int i=0;i<table.Rows.Count;i++){
				if(table.Rows[i]["PatNum"].ToString()==pay.PatNum.ToString()){
					continue;
				}
				pat=new Patient();
				pat.PatNum    = PIn.PInt   (table.Rows[i][0].ToString());
				pat.EstBalance= PIn.PDouble(table.Rows[i][1].ToString());
				pat.PriProv   = PIn.PInt   (table.Rows[i][2].ToString());
				pats.Add(pat.Copy());
			}
			//first calculate all the amounts
			double amtRemain=pay.PayAmt;//start off with the full amount
			double[] amtSplits=new double[pats.Count];
			//loop through each family member, starting with current
			for(int i=0;i<pats.Count;i++){
				if(pats[i].EstBalance==0 || pats[i].EstBalance<0){
					continue;//don't apply paysplits to anyone with a negative balance
				}
				if(amtRemain<pats[i].EstBalance){//entire remainder can be allocated to this patient
					amtSplits[i]=amtRemain;
					amtRemain=0;
					break;
				}
				else{//amount remaining is more than or equal to the estBal for this family member
					amtSplits[i]=pats[i].EstBalance;
					amtRemain-=pats[i].EstBalance;
				}
			}
			//add any remainder to the split for this patient
			amtSplits[0]+=amtRemain;
			//now create a split for each non-zero amount
			PaySplit PaySplitCur;
			List<PaySplit> retVal=new List<PaySplit>();
			for(int i=0;i<pats.Count;i++){
				if(amtSplits[i]==0){
					continue;
				}
				PaySplitCur=new PaySplit();
				PaySplitCur.PatNum=pats[i].PatNum;
				PaySplitCur.PayNum=pay.PayNum;
				PaySplitCur.ProcDate=pay.PayDate;
				PaySplitCur.DatePay=pay.PayDate;
				PaySplitCur.ProvNum=Patients.GetProvNum(pats[i]);
				PaySplitCur.SplitAmt=amtSplits[i];
				//PaySplitCur.InsertOrUpdate(true);
				retVal.Add(PaySplitCur);
			}
			//finally, adjust each EstBalance, but no need to do current patient
			//This no longer works here.  Must do it when closing payment window somehow
			/*for(int i=1;i<pats.Length;i++){
				if(amtSplits[i]==0){
					continue;
				}
				command="UPDATE patient SET EstBalance=EstBalance-"+POut.PDouble(amtSplits[i])
					+" WHERE PatNum="+POut.PInt(pats[i].PatNum);
				General.NonQ(command);
			}*/
			return retVal;
		}

		///<summary>This does all the validation before calling AlterLinkedEntries.  It had to be separated like this because of the complexity of saving a payment.  Surround with try-catch.  Will throw an exception if user is trying to change, but not allowed.  Will return false if no synch with accounting is needed.  Use -1 for newAcct to indicate no change.</summary>
		public static bool ValidateLinkedEntries(double oldAmt, double newAmt, bool isNew, int payNum, int newAcct){
			if(!Accounts.PaymentsLinked()){
				return false;//user has not even set up accounting links, so no need to check any of this.
			}
			bool amtChanged=false;
			if(oldAmt!=newAmt) {
				amtChanged=true;
			}
			Transaction trans=Transactions.GetAttachedToPayment(payNum);//this gives us the oldAcctNum
			if(trans==null && (newAcct==0 || newAcct==-1)) {//if there was no previous link, and there is no attempt to create a link
				return false;//no synch needed
			}
			if(trans==null){//no previous link, but user is trying to create one. newAcct>0.
				return true;//new transaction will be required
			}
			//at this point, we have established that there is a previous transaction.
			//If payment is attached to a transaction which is more than 48 hours old, then not allowed to change.
			if(amtChanged && trans.DateTimeEntry < MiscData.GetNowDateTime().AddDays(-2)) {
				throw new ApplicationException(Lan.g("Payments","Not allowed to change amount that is more than 48 hours old.  This payment is already attached to an accounting transaction.  You will need to detach it from within the accounting section of the program."));
			}
			if(amtChanged && Transactions.IsReconciled(trans)) {
				throw new ApplicationException(Lan.g("Payments","Not allowed to change amount.  This payment is attached to an accounting transaction that has been reconciled.  You will need to detach it from within the accounting section of the program."));
			}
			ArrayList jeAL=JournalEntries.GetForTrans(trans.TransactionNum);
			int oldAcct=0;
			JournalEntry jeDebit=null;
			JournalEntry jeCredit=null;
			double absOld=oldAmt;//the absolute value of the old amount
			if(oldAmt<0) {
				absOld=-oldAmt;
			}
			for(int i=0;i<jeAL.Count;i++) {//we make sure down below that this count is exactly 2.
				if(Accounts.GetAccount(((JournalEntry)jeAL[i]).AccountNum).AcctType==AccountType.Asset) {
					oldAcct=((JournalEntry)jeAL[i]).AccountNum;
				}
				if(((JournalEntry)jeAL[i]).DebitAmt==absOld) {
					jeDebit=(JournalEntry)jeAL[i];
				}
				//old credit entry
				if(((JournalEntry)jeAL[i]).CreditAmt==absOld) {
					jeCredit=(JournalEntry)jeAL[i];
				}
			}
			if(jeCredit==null || jeDebit==null) {
				throw new ApplicationException(Lan.g("Payments","Not able to automatically make changes in the accounting section to match the change made here.  You will need to detach it from within the accounting section."));
			}
			if(oldAcct==0){//something must have gone wrong.  But this should never happen
				throw new ApplicationException(Lan.g("Payments","Could not locate linked transaction.  You will need to detach it manually from within the accounting section of the program."));
			}
			if(newAcct==0){//detaching it from a linked transaction.
				//We will delete the transaction
				return true;
			}
			bool acctChanged=false;
			if(newAcct!=-1 && oldAcct!=newAcct){
				acctChanged=true;//changing linked acctNum
			}
			if(!amtChanged && !acctChanged){
				return false;//no changes being made to amount or account, so no synch required.
			}
			if(jeAL.Count!=2) {
				throw new ApplicationException(Lan.g("Payments","Not able to automatically change the amount in the accounting section to match the change made here.  You will need to detach it from within the accounting section."));
			}
			//Amount or account changed on an existing linked transaction.
			return true;
		}

		///<summary>Only called once from FormPayment when trying to change an amount or an account on a payment that's already linked to the Accounting section or when trying to create a new link.  This automates updating the Accounting section.  Do not surround with try-catch, because it was already validated in ValidateLinkedEntries above.  Use -1 for newAcct to indicate no changed. The name is required to give descriptions to new entries.</summary>
		public static void AlterLinkedEntries(double oldAmt, double newAmt, bool isNew, int payNum, int newAcct,DateTime payDate,
			string patName)
		{
			if(!Accounts.PaymentsLinked()) {
				return;//user has not even set up accounting links.
			}
			bool amtChanged=false;
			if(oldAmt!=newAmt) {
				amtChanged=true;
			}
			Transaction trans=Transactions.GetAttachedToPayment(payNum);//this gives us the oldAcctNum
			double absNew=newAmt;//absolute value of the new amount
			if(newAmt<0) {
				absNew=-newAmt;
			}
			//if(trans==null && (newAcct==0 || newAcct==-1)) {//then this method will not even be called
			if(trans==null) {//no previous link, but user is trying to create one.
				//this is the only case where a new trans is required.
				trans=new Transaction();
				trans.PayNum=payNum;
				trans.UserNum=Security.CurUser.UserNum;
				Transactions.Insert(trans);//sets entry date
				//first the deposit entry
				JournalEntry je=new JournalEntry();
				je.AccountNum=newAcct;//DepositAccounts[comboDepositAccount.SelectedIndex];
				je.CheckNumber=Lan.g("Payments","DEP");
				je.DateDisplayed=payDate;//it would be nice to add security here.
				if(absNew==newAmt){//amount is positive
					je.DebitAmt=newAmt;
				}
				else{
					je.CreditAmt=absNew;
				}
				je.Memo=Lan.g("Payments","Payment -")+" "+patName;
				je.Splits=Accounts.GetDescript(PrefB.GetInt("AccountingCashIncomeAccount"));
				je.TransactionNum=trans.TransactionNum;
				JournalEntries.Insert(je);
				//then, the income entry
				je=new JournalEntry();
				je.AccountNum=PrefB.GetInt("AccountingCashIncomeAccount");
				//je.CheckNumber=;
				je.DateDisplayed=payDate;//it would be nice to add security here.
				if(absNew==newAmt) {//amount is positive
					je.CreditAmt=newAmt;
				}
				else {
					je.DebitAmt=absNew;
				}
				je.Memo=Lan.g("Payments","Payment -")+" "+patName;
				je.Splits=Accounts.GetDescript(newAcct);
				je.TransactionNum=trans.TransactionNum;
				JournalEntries.Insert(je);
				return;
			}
			//at this point, we have established that there is a previous transaction.
			ArrayList jeAL=JournalEntries.GetForTrans(trans.TransactionNum);
			int oldAcct=0;
			JournalEntry jeDebit=null;
			JournalEntry jeCredit=null;
			bool signChanged=false;
			double absOld=oldAmt;//the absolute value of the old amount
			if(oldAmt<0){
				absOld=-oldAmt;
			}
			if(oldAmt<0 && newAmt>0){
				signChanged=true;
			}
			if(oldAmt>0 && newAmt<0){
				signChanged=true;
			}
			for(int i=0;i<2;i++){
				if(Accounts.GetAccount(((JournalEntry)jeAL[i]).AccountNum).AcctType==AccountType.Asset) {
					oldAcct=((JournalEntry)jeAL[i]).AccountNum;
				}
				if(((JournalEntry)jeAL[i]).DebitAmt==absOld) {
					jeDebit=(JournalEntry)jeAL[i];
				}
				//old credit entry
				if(((JournalEntry)jeAL[i]).CreditAmt==absOld) {
					jeCredit=(JournalEntry)jeAL[i];
				}
			}
			//Already validated that both je's are not null, and that oldAcct is not 0.
			if(newAcct==0){//detaching it from a linked transaction. We will delete the transaction
				//we don't care about the amount
				Transactions.Delete(trans);//we need to make sure this doesn't throw any exceptions by carefully checking all
					//possibilities in the validation routine above.
				return;
			}
			//Either the amount or the account changed on an existing linked transaction.
			bool acctChanged=false;
			if(newAcct!=-1 && oldAcct!=newAcct) {
				acctChanged=true;//changing linked acctNum
			}
			if(amtChanged){
				if(signChanged) {
					jeDebit.DebitAmt=0;
					jeDebit.CreditAmt=absNew;
					jeCredit.DebitAmt=absNew;
					jeCredit.CreditAmt=0;
				}
				else {
					jeDebit.DebitAmt=absNew;
					jeCredit.CreditAmt=absNew;
				}
			}
			if(acctChanged){
				if(jeDebit.AccountNum==oldAcct){
					jeDebit.AccountNum=newAcct;
				}
				if(jeCredit.AccountNum==oldAcct) {
					jeCredit.AccountNum=newAcct;
				}
			}
			JournalEntries.Update(jeDebit);
			JournalEntries.Update(jeCredit);
		}		

		///<summary>Used for display in ProcEdit. List MUST include the requested payment. Use GetPayments to get the list.</summary>
		public static Payment GetFromList(int payNum,Payment[] List){
			for(int i=0;i<List.Length;i++){
				if(List[i].PayNum==payNum){
					return List[i];
				}
			}
			return null;//should never happen
		}

		/*
		///<summary></summary>
		public static string GetInfo(int payNum){
			string retStr;
			Payment Cur=GetPayment(payNum);
			retStr=DefB.GetName(DefCat.PaymentTypes,Cur.PayType);
			if(Cur.IsSplit) retStr=retStr
				+"  "+Cur.PayAmt.ToString("c")
				+"  "+Cur.PayDate.ToString("d")
				+" "+Lan.g("Payments","split between patients");
			return retStr;
		}*/

		

	}

	

	

}










