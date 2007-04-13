using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used in the accounting section of the program.  Each row is one transaction in the ledger, and must always have at least two splits.  All splits must always add up to zero.</summary>
	public class Transaction{
		///<summary>Primary key.</summary>
		public int TransactionNum;
		///<summary>Not user editable.  Server time.</summary>
		public DateTime DateTimeEntry;
		///<summary>FK to user.UserNum.</summary>
		public int UserNum;
		///<summary>FK to deposit.DepositNum.  Will eventually be replaced by a source document table, and deposits will just be one of many types.</summary>
		public int DepositNum;
		///<summary>FK to payment.PayNum.  Like DepositNum, it will eventually be replaced by a source document table, and payments will just be one of many types.</summary>
		public int PayNum;

		///<summary></summary>
		public Transaction Copy() {
			Transaction t=new Transaction();
			t.TransactionNum=TransactionNum;
			t.DateTimeEntry=DateTimeEntry;
			t.UserNum=UserNum;
			t.DepositNum=DepositNum;
			t.PayNum=PayNum;
			return t;
		}


	}
}