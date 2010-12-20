using System;

namespace OpenDentBusiness{

	///<summary>Gradeschool.  Deprecated and replaced by the site table.  Still used in the screen table which is essentially outside of the main program.</summary>
	[Serializable]
	public class School{
		///<summary>Primary key, but allowed to change.  Change is programmatically synchronized so that keys stay intact.</summary>
		public string SchoolName;
		///<summary>Optional. Usage varies.</summary>
		public string SchoolCode;
		///<summary>Not a database field. This is the unaltered SchoolName. Used for Update.</summary>
		public string OldSchoolName;
	}

	

	

}













