package com.opendental.odweb.client.datainterface;

import java.util.ArrayList;

import com.opendental.odweb.client.remoting.Meth;
import com.opendental.odweb.client.tabletypes.Account;
import com.opendental.odweb.client.tabletypes.Account.AccountType;
import com.opendental.odweb.client.ui.MsgBox;

public class Accounts {
	
	private static ArrayList<Account> ListLong;

	/** List of all accounts.  Includes inactive. */	
	public static ArrayList<Account> GetListLong() {
		if(ListLong==null){
			RefreshCache();
		}
		return ListLong;
	}

	private static ArrayList<Account> ListShort; 
	
	/** Used for display. Does not include inactive. */
	public static ArrayList<Account> GetListShort() {
		if(ListShort==null) {
			RefreshCache();
		}
		return ListShort;
	}

	/**  */
	public static void RefreshCache() {
		@SuppressWarnings("unused")
		String command="SELECT * FROM account "
			+" ORDER BY AcctType,Description";
		//DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
		//table.TableName="Account";
		ArrayList<Account> accounts=new ArrayList<Account>();
		FillCache(accounts);
	}

	private static void FillCache(ArrayList<Account> accounts) {
		ListLong=accounts;
		ArrayList<Account> listShort=new ArrayList<Account>();
		for(int i=0;i<ListLong.size();i++){
			if(!ListLong.get(i).Inactive) {
				listShort.add(ListLong.get(i).Copy());
			}
		}
		ListShort=listShort;
	}
	
	/** Gets the balance of an account directly from the database. */
	public static double GetBalance(int accountNum,AccountType acctType) {
		String[] paramTypes= { "long","AccountType" };//The parameter types in the C# method that we will be calling.
		try {
			Meth.GetInt("Accounts.GetBalance",paramTypes,accountNum,acctType);
		} catch (Exception e) {
			MsgBox.Show("Error:\r\n"+e.getMessage());
		}
		return 0;
	}
	
	
}
