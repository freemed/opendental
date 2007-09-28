using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1 {
	enum CRLPolicyOptions : int {
		/// <summary>
		/// CRL Policy is Not Used
		/// </summary>
		NotUsed = 0,
		/// <summary>
		/// CRL Policy is Optional
		/// </summary>
		Optional = 1,
		/// <summary>
		/// CRL Policy is Mandatory
		/// </summary>
		Mandatory = 2
	}
}
