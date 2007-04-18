using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OpenDentBusiness{
	public class PatientB {
		///<summary>Returns a formatted name, Last, First.</summary>
		public static string GetNameLF(string LName,string FName, string Preferred,string MiddleI) {
			if(Preferred=="")
				return LName+", "+FName+" "+MiddleI;
			else
				return LName+", '"+Preferred+"' "+FName+" "+MiddleI;
		}

	}
}
