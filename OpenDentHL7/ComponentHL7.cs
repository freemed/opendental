using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentHL7 {
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
