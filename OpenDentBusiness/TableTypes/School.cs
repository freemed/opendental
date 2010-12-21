using System;

namespace OpenDentBusiness{

	///<summary>Gradeschool.  Deprecated and replaced by the site table.  Still used in the screen table which is essentially outside of the main program.</summary>
	[Serializable]
	public class School:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SchoolNum;
		///<summary>Used somewhat like a primary key, but allowed to change.  Change is programmatically synchronized so that keys stay intact.</summary>
		public string SchoolName;
		///<summary>Optional. Usage varies.</summary>
		public string SchoolCode;
		///<summary>Not a database column. This is the unaltered SchoolName. Used for Update.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public string OldSchoolName;
	}

	

	

}













