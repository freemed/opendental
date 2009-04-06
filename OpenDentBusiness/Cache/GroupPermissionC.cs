using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class GroupPermissionC {
		private static GroupPermission[] list;

		///<summary>A list of all GroupPermissions for all groups.</summary>
		public static GroupPermission[] List {
			get {
				if(list==null) {
					GroupPermissions.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

	}
}
