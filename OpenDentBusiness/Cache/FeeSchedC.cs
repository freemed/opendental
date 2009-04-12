using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class FeeSchedC {
		///<summary>A list of all feescheds.</summary>
		private static List<FeeSched> listLong;
		///<summary>A list of feescheds that are not hidden.</summary>
		private static List<FeeSched> listShort;

		public static List<FeeSched> ListLong{
			get {
				if(listLong==null) {
					FeeScheds.RefreshCache();
				}
				return listLong;
			}
			set {
				listLong=value;
			}
		}

		public static List<FeeSched> ListShort {
			get {
				if(listShort==null) {
					FeeScheds.RefreshCache();
				}
				return listShort;
			}
			set {
				listShort=value;
			}
		}
		
	}
}
