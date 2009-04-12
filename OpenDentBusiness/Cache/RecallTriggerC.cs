using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class RecallTriggerC {
		///<summary>A list of all recall triggers.</summary>
		private static List<RecallTrigger> listt;

		public static List<RecallTrigger> Listt {
			get {
				if(listt==null) {
					RecallTriggers.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}
		
	}
}
