using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class PharmacyC {
		///<summary>A list of all pharmacies.</summary>
		private static List<Pharmacy> listt;

		public static List<Pharmacy> Listt {
			get {
				if(listt==null) {
					Pharmacies.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}
		
	}
}
