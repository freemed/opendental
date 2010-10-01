using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A deposit slip.  Contains multiple insurance and patient checks.</summary>
	[Serializable]
	public class Deposit:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DepositNum;
		///<summary>The date of the deposit.</summary>
		public DateTime DateDeposit;
		///<summary>User editable.  Usually includes name on the account and account number.  Possibly the bank name as well.</summary>
		public string BankAccountInfo;
		///<summary>Total amount of the deposit. User not allowed to directly edit.</summary>
		public double Amount;
		///<summary>FK to sheetdef.SheetDefNum. If 0, then the default internal deposit slip sheet is used.</summary>
		public long SheetDefNum;
		
		///<summary></summary>
		public Deposit Copy(){
			return (Deposit)this.MemberwiseClone();
		}

	
	}

	

	


}




















