using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class InsFilingCodeC {
		///<summary>A list of all insurance filing codes.</summary>
		private static List<InsFilingCode> listt;

		public static List<InsFilingCode> Listt {
			get {
				if(listt==null) {
					InsFilingCodes.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}
		
	}
}
