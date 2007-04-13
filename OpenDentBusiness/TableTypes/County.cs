using System;
using System.Collections;

namespace OpenDentBusiness{
	///<summary>Used in public health.</summary>
	public class County{
		///<summary>Primary key, but allowed to change.  Change is programmatically synchronized.</summary>
		public string CountyName;
		///<summary>Optional. Usage varies.</summary>
		public string CountyCode;
		///<summary>Not a database field. This is the unaltered CountyName. Used for Update.</summary>
		public string OldCountyName;
	}


	

}













