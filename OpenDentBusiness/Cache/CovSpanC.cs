using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class CovSpanC {
		///<summary></summary>
		private static CovSpan[] list;

		public static CovSpan[] List {
			get {
				if(list==null) {
					CovSpans.RefreshCache();
					DisplayFields.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

	}
}
