using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ProviderC {
		private static Provider[] listLong;
		private static Provider[] list;

		///<summary>Rarely used. Includes all providers, even if hidden.</summary>
		public static Provider[] ListLong {
			get {
				if(listLong==null) {
					Providers.RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		///<summary>This is the list used most often. It does not include hidden providers.</summary>
		public static Provider[] List {
			get {
				if(list==null) {
					Providers.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

	}
}
