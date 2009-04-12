using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class AutoCodeCondC {
		///<summary></summary>
		private static AutoCodeCond[] list;

		public static AutoCodeCond[] List {
			get {
				if(list==null) {
					AutoCodeConds.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

	}
}
