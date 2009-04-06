using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness {
	public class ApptViewItemC {
		private static ApptViewItem[] list;

		///<summary>A list of all ApptViewItems.</summary>
		public static ApptViewItem[] List {
			get {
				if(list==null) {
					ApptViewItems.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}
	}
}
