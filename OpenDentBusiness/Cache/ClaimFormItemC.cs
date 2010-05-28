using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ClaimFormItemC {
		///<summary></summary>
		private static ClaimFormItem[] listt;

		public static ClaimFormItem[] Listt {
			get {
				if(listt==null) {
					ClaimFormItems.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

	}
}
