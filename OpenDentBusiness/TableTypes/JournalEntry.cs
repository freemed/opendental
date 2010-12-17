using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used in accounting to represent a single credit or debit entry.  There will always be at least 2 journal enties attached to every transaction.  All transactions balance to 0.</summary>
	[Serializable]
	public class JournalEntry:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long JournalEntryNum;
		///<summary>FK to transaction.TransactionNum</summary>
		public long TransactionNum;
		///<summary>FK to account.AccountNum</summary>
		public long AccountNum;
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
		public long ReconcileNum;

		///<summary></summary>
		public JournalEntry Copy() {
			return (JournalEntry)this.MemberwiseClone();
		}






	}

	
}




