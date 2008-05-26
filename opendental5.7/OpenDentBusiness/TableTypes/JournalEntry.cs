using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used in accounting to represent a single credit or debit entry.  There will always be at least 2 journal enties attached to every transaction.  All transactions balance to 0.</summary>
	public class JournalEntry{
		///<summary>Primary key.</summary>
		public int JournalEntryNum;
		///<summary>FK to transaction.TransactionNum</summary>
		public int TransactionNum;
		///<summary>FK to account.AccountNum</summary>
		public int AccountNum;
		///<summary>Always the same for all journal entries within one transaction.</summary>
		public DateTime DateDisplayed;
		///<summary>Negative numbers never allowed.</summary>
		public double DebitAmt;
		///<summary>Negative numbers never allowed.</summary>
		public double CreditAmt;
		///<summary>.</summary>
		public string Memo;
		///<summary>A human-readable description of the splits.  Used only for display purposes.</summary>
		public string Splits;
		///<summary>Any user-defined string.  Usually a check number, but can also be D for deposit, Adj, etc.</summary>
		public string CheckNumber;
		///<summary>FK to reconcile.ReconcileNum. 0 if not attached to a reconcile. Not allowed to alter amounts if attached.</summary>
		public int ReconcileNum;

		///<summary></summary>
		public JournalEntry Copy() {
			JournalEntry j=new JournalEntry();
			j.JournalEntryNum=JournalEntryNum;
			j.TransactionNum=TransactionNum;
			j.AccountNum=AccountNum;
			j.DateDisplayed=DateDisplayed;
			j.DebitAmt=DebitAmt;
			j.CreditAmt=CreditAmt;
			j.Memo=Memo;
			j.Splits=Splits;
			j.CheckNumber=CheckNumber;
			j.ReconcileNum=ReconcileNum;
			return j;
		}






	}

	
}




