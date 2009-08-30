using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class CovCatC {
		private static List<CovCat> listt;
		private static List<CovCat> listShort;

		///<summary>All CovCats</summary>
		public static List<CovCat> Listt {
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
		public static List<CovCat> ListShort {
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
		public static int GetOrderLong(long covCatNum) {
			for(int i=0;i<Listt.Count;i++) {
				if(covCatNum==Listt[i].CovCatNum) {
					return i;
				}
			}
			return -1;
		}	

	}
}
