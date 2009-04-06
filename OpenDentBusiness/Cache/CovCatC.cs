using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class CovCatC {
		private static CovCat[] listt;
		private static CovCat[] listShort;

		///<summary>All CovCats</summary>
		public static CovCat[] Listt {
			get {
				if(listt==null) {
					CovCats.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary>Only CovCats that are not hidden.</summary>
		public static CovCat[] ListShort {
			get {
				if(listShort==null) {
					CovCats.RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		///<summary></summary>
		public static int GetOrderLong(int covCatNum) {
			for(int i=0;i<Listt.Length;i++) {
				if(covCatNum==Listt[i].CovCatNum) {
					return i;
				}
			}
			return -1;
		}	

	}
}
