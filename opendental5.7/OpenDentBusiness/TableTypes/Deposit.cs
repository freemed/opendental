using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A deposit slip.  Contains multiple insurance and patient checks.</summary>
	public class Deposit{
		///<summary>Primary key.</summary>
		public int DepositNum;
		///<summary>The date of the deposit.</summary>
		public DateTime DateDeposit;
		///<summary>User editable.  Usually includes name on the account and account number.  Possibly the bank name as well.</summary>
		public string BankAccountInfo;
		///<summary>Total amount of the deposit. User not allowed to directly edit.</summary>
		public double Amount;
		
		///<summary></summary>
		public Deposit Copy(){
			Deposit d=new Deposit();
			d.DepositNum=DepositNum;
			d.DateDeposit=DateDeposit;
			d.BankAccountInfo=BankAccountInfo;
			d.Amount=Amount;
			return d;
		}

	
	}

	

	


}




















