using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class InsFilingCodeSubtypeC {
		///<summary>A list of all insurance filing code subtypes.</summary>
		private static List <InsFilingCodeSubtype> listt;

		public static List <InsFilingCodeSubtype> Listt {
			get {
				if(listt==null) {
					InsFilingCodeSubtypes.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}
		
	}
}
