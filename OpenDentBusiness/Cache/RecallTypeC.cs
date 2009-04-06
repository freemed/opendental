using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class RecallTypeC {
		private static List<RecallType> listt;

		///<summary>A list of all recall Types.</summary>
		public static List<RecallType> Listt {
			get {
				if(listt==null) {
					RecallTypes.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}


	}
}
