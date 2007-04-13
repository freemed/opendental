using System;
using System.Collections;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness{

	///<summary>Used in the accounting section in chart of accounts.  Not related to patient accounts in any way.</summary>
	public class Account{
		///<summary>Primary key.</summary>
		public int AccountNum;
		///<summary>.</summary>
		public string Description;
		///<summary>Enum:AccountType Asset, Liability, Equity,Revenue, Expense</summary>
		public AccountType AcctType;
		///<summary>For asset accounts, this would be the bank account number for deposit slips.</summary>
		public string BankNumber;
		///<summary>Set to true to not normally view this account in the list.</summary>
		public bool Inactive;
		///<summary>.</summary>
		public Color AccountColor;

		///<summary></summary>
		public Account Copy() {
			Account a=new Account();
			a.AccountNum=AccountNum;
			a.Description=Description;
			a.AcctType=AcctType;
			a.BankNumber=BankNumber;
			a.Inactive=Inactive;
			a.AccountColor=AccountColor;
			return a;
		}
	}

	
}




