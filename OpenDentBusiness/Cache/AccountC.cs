using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	///<summary>The two lists get refreshed the first time they are needed rather than at startup.</summary>
	public class AccountC {
		internal static Account[] listLong;
		internal static Account[] listShort;

		///<summary></summary>
		public static Account[] ListLong {
			get {
				if(AccountC.listLong==null) {
					DataTable table=Accounts.RefreshCache();
					Accounts.FillCache(table);
				}
				return AccountC.listLong;
			}
			/*set {
				title=value;
			}*/
		}

		///<summary>Used for display. Does not include inactive</summary>
		public static Account[] ListShort {
			get {
				if(AccountC.listShort==null) {
					DataTable table=Accounts.RefreshCache();
					Accounts.FillCache(table);
				}
				return AccountC.listShort;
			}
		}

		///<summary>Loops through listLong to find a description for the specified account.  0 returns an empty string.</summary>
		public static string GetDescript(int accountNum){
			if(accountNum==0) {
				return "";
			}
			for(int i=0;i<ListLong.Length;i++){
				if(ListLong[i].AccountNum==accountNum){
					return ListLong[i].Description;
				}
			}
			return "";
		}

		///<summary>Loops through listLong to find an account.  Will return null if accountNum is 0.</summary>
		public static Account GetAccount(int accountNum) {
			if(accountNum==0){
				return null;
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].AccountNum==accountNum) {
					return ListLong[i].Copy();
				}
			}
			return null;//just in case
		}

	}
}
