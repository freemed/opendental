using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary></summary>
	public class InternalEcwStandalone {

		public static HL7Def GetHL7Def() {
			HL7Def def=HL7Defs.GetInternalFromDb("eCWstandalone");

			return def;
		}


	}
}
