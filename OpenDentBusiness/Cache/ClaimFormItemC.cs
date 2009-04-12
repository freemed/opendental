using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ClaimFormItemC {
		///<summary></summary>
		private static ClaimFormItem[] list;

		public static ClaimFormItem[] List {
			get {
				if(list==null) {
					ClaimFormItems.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

	}
}
