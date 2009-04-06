using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ApptViewC {
		private static ApptView[] list;

		///<summary>A list of all apptviews, in order.</summary>
		public static ApptView[] List {
			get {
				if(list==null) {
					ApptViews.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		
	}
}
