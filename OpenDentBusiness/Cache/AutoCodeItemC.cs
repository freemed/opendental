using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class AutoCodeItemC {
		///<summary></summary>
		private static AutoCodeItem[] list;
		private static Hashtable hList;

		public static AutoCodeItem[] List{
			get {
				if(list==null) {
					AutoCodeItems.RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		///<summary>key=CodeNum,value=AutoCodeNum</summary>
		public static Hashtable HList {
			get {
				if(hList==null) {
					AutoCodeItems.RefreshCache();
				}
				return hList;
			}
			set {
				hList=value;
			}
		}

	}
}
