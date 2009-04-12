using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class AccountingAutoPayC {
		///<summary></summary>
		private static ArrayList aList;

		public static ArrayList AList {
			get {
				if(aList==null) {
					AccountingAutoPays.RefreshCache();
				}
				return aList;
			}
			set {
				aList=value;
			}
		}

	}
}
