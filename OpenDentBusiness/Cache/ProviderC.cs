using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ProviderC {
		private static List<Provider> listLong;
		private static List<Provider> list;

		///<summary>Rarely used. Includes all providers, even if hidden.</summary>
		public static List<Provider> ListLong {
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
		public static List<Provider> ListShort {
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
