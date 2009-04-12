using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class SiteC {
		///<summary>A list of all sites.</summary>
		private static Site[] list;

		public static Site[] List {
			get {
				if(list==null) {
					Sites.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}
		
	}
}
