using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	///<summary>The two lists get refreshed the first time they are needed rather than at startup.</summary>
	public class AccountC {
		private static Account[] listLong;
		private static Account[] listShort;

		///<summary></summary>
		public static Account[] ListLong {
			get {
				if(listLong==null) {
					Accounts.RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		///<summary>Used for display. Does not include inactive</summary>
		public static Account[] ListShort {
			get {
				if(listShort==null) {
					Accounts.RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		///<summary>Loops through listLong to find a description for the specified account.  0 returns an empty string.</summary>
		public static string GetDescript(long accountNum){
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
		public static Account GetAccount(long accountNum) {
			if(accountNum==0){
				return null;
			}
			for(int i=0;i<ListLong.Length;i++) {
				if(ListLong[i].AccountNum==accountNum) {
					return ListLong[i].Clone();
				}
			}
			return null;//just in case
		}

	}
}
