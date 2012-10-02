using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenDentalWebService {
	public class Accounts {
		///<summary>Serializes the object for Java.</summary>
		public static string SerializeForJava(object obj) {
			OpenDentBusiness.Account acct=(OpenDentBusiness.Account)obj;
			string xml="";//Figure out how to serialize objects for Java.
			return xml;
		}

		///<summary></summary>
		public static long Insert(OpenDentBusiness.Account acct) {
			return OpenDentBusiness.Accounts.Insert(acct);
		}

		///<summary></summary>
		public static void Update(OpenDentBusiness.Account acct) {
			OpenDentBusiness.Accounts.Update(acct);
		}

	}
}