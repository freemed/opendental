using System;

using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile {
	public class PrefC {
		///<summary>Key is PrefName, value is ValueString</summary>
		public static Dictionary<string,string> HList;

		///<summary>Gets a pref of type string.</summary>
		public static string GetString(string prefName) {
			if(HList==null){
				Prefs.Refresh();
			}
			if(!HList.ContainsKey(prefName)) {
				throw new Exception(prefName+" is an invalid pref name.");
			}
			return HList[prefName];
		}







	}
}
