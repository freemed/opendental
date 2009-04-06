using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class DisplayFieldC {
		private static List<DisplayField> listt;

		///<summary>A list of all DisplayFields, sorted by ItemOrder, but not by Category.</summary>
		public static List<DisplayField> Listt {
			get {
				if(listt==null) {
					DisplayFields.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

	}
}
