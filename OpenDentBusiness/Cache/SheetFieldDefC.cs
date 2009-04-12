using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class SheetFieldDefC {
		///<summary>A list of all sheetfielddefs.</summary>
		private static List<SheetFieldDef> listt;

		public static List<SheetFieldDef> Listt {
			get {
				if(listt==null) {
					SheetFieldDefs.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}
		
	}
}
