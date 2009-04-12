using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class SheetDefC {
		///<summary>A list of all sheetdefs.</summary>
		private static List<SheetDef> listt;

		public static List<SheetDef> Listt {
			get {
				if(listt==null) {
					SheetDefs.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		
	}
}
