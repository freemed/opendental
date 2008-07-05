using System;

namespace OpenDentBusiness{

	///<summary>Gradeschool.  Used in public health.  Only used in the screen table and patient table.  Probably being deprecated in patient table in favor of sites.</summary>
	public class School{
		///<summary>Primary key, but allowed to change.  Change is programmatically synchronized so that keys stay intact.</summary>
		public string SchoolName;
		///<summary>Optional. Usage varies.</summary>
		public string SchoolCode;
		///<summary>Not a database field. This is the unaltered SchoolName. Used for Update.</summary>
		public string OldSchoolName;
	}

	

	

}













