using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class AutoCodeC {
		///<summary></summary>
		private static AutoCode[] list;
		///<summary></summary>
		private static AutoCode[] listShort;
		private static Hashtable hList;

		public static AutoCode[] List {
			get {
				if(list==null) {
					AutoCodes.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static AutoCode[] ListShort {
			get {
				if(listShort==null) {
					AutoCodes.RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		///<summary>key=AutoCodeNum, value=AutoCode</summary>
		public static Hashtable HList {
			get {
				if(hList==null) {
					AutoCodes.RefreshCache();
				}
				return hList;
			}
			set {
				hList=value;
			}
		}



	}
}
