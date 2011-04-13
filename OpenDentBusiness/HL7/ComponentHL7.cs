using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary>A component in HL7 is a subportion of a field.  For example, a name field might have LName and FName components.</summary>
	public class ComponentHL7 {
		public string ComponentVal;

		public ComponentHL7(string componentVal) {
			ComponentVal=componentVal;
		}

		public override string ToString() {
			return ComponentVal;
		}
	}
}
