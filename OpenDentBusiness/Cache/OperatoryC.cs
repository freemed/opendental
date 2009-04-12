using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class OperatoryC {
		///<summary></summary>
		private static List<Operatory> listt;
		///<summary>A list of only those operatories that are visible.</summary>
		private static List<Operatory> listShort;

		public static List<Operatory> Listt {
			get {
				if(listt==null) {
					Operatories.RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		public static List<Operatory> ListShort {
			get {
				if(listShort==null) {
					Operatories.RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}

		

	}
}