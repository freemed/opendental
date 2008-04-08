using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class CovCatC {
		///<summary>All CovCats</summary>
		public static CovCat[] Listt;
		///<summary>Only CovCats that are not hidden.</summary>
		public static CovCat[] ListShort;

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
