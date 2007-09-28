using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1 {
	enum OCSPPolicyOptions : int {
		/// <summary>
		/// OCSP Policy is Not Used
		/// </summary>
		NotUsed = 0,
		/// <summary>
		/// OCSP Policy is Optional
		/// </summary>
		Optional = 1,
		/// <summary>
		/// OCSP Policy is Mandatory
		/// </summary>
		Mandatory = 2
	}
}
