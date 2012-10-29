using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService {
	public class Accounts {

		///<summary></summary>
		public static long Insert(OpenDentBusiness.Account acct) {
			//TODO: Have a unique "web" insert method call instead of using OpenDentBusiness.Insert.
			return OpenDentBusiness.Accounts.Insert(acct);
		}

		///<summary></summary>
		public static void Update(OpenDentBusiness.Account acct) {
			//TODO: Have a unique "web" update method call instead of using OpenDentBusiness.Update.
			OpenDentBusiness.Accounts.Update(acct);
		}

	}
}