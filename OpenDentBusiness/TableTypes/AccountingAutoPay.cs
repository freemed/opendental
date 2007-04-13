using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>In the accounting section, this automates entries into the database when user enters a payment into a patient account.  This table presents the user with a picklist specific to that payment type.  For example, a cash payment would create a picklist of cashboxes for user to put the cash into.</summary>
	public class AccountingAutoPay{
		///<summary>Primary key.</summary>
		public int AccountingAutoPayNum;
		///<summary>FK to definition.DefNum.</summary>
		public int PayType;
		///<summary>FK to account.AccountNum.  AccountNums separated by commas.  No spaces.</summary>
		public string PickList;

		///<summary>Returns a copy of this AccountingAutoPay.</summary>
		public AccountingAutoPay Copy(){
			AccountingAutoPay a=new AccountingAutoPay();
			a.AccountingAutoPayNum=AccountingAutoPayNum;
			a.PayType=PayType;
			a.PickList=PickList;
			return a;
		}

	}
		
	
	


}













