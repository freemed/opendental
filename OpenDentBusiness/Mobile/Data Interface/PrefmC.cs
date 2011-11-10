using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness.Mobile {
	public class PrefmC {
		///<summary>Dennis ToDo: this class has to be revised</summary>
		public Dictionary<string,Prefm> Dict=new Dictionary<string,Prefm>();// cannot have a static variable here because we want something unique for each patient.
		///<summary>Gets a pref of type string.</summary>
		public string GetString(PrefmName prefmName) {
			if(Dict==null) {
				Prefms.LoadPreferences(3);
			}
			if(!Dict.ContainsKey(prefmName.ToString())) {
				throw new Exception(prefmName+" is an invalid pref name.");
			}
			return Dict[prefmName.ToString()].ValueString;
		}


	}
}



	