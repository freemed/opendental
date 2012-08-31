package com.opendental.odweb.client.tabletypes;

public class Account {
		/** Primary key. */
		public int AccountNum;
		/** . */
		public String Description;
		/** Enum:AccountType 0=Asset, 1=Liability, 2=Equity, 3=Income, 4=Expense */
		public AccountType AcctType;
		/** For asset accounts, this would be the bank account number for deposit slips. */
		public String BankNumber;
		/** Set to true to not normally view this account in the list. */
		public boolean Inactive;
		/** . */
		public int AccountColor;

		/** Memberwise Clone. */
		public Account Clone() {
			Account account=new Account();
			account.AccountNum=this.AccountNum;
			account.Description=this.Description;
			account.AcctType=this.AcctType;
			account.BankNumber=this.BankNumber;
			account.Inactive=this.Inactive;
			account.AccountColor=this.AccountColor;
			return account;
		}
		
		public enum AccountType	{
			/** 0- Asset */
			Asset,
			/** 1- Liability */
			Liability,
			/** 2- Equity */
			Equity,
			/** 3- Income */
			Income,
			/** 4- Expense */
			Expense
		}
}
