using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class AppointmentRuleC {
		///<summary></summary>
		private static AppointmentRule[] list;

		public static AppointmentRule[] List {
			get {
				if(list==null) {
					AppointmentRules.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

	}
}
