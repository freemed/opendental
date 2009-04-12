using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class MountDefC {
		///<summary>A list of all MountDefs.</summary>
		private static List<MountDef> listt;

		public static List<MountDef> Listt {
			get {
				if(listt==null) {
					MountDefs.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

	}
}
