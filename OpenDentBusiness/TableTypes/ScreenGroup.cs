using System;

namespace OpenDentBusiness{

	///<summary>Used in public health.  Programming note: There are many extra fields in common with the screen table, but they are only in this struct and not in the database itself, where that data is stored with the individual screen items. The data in this table is irrelevant in reports.  It is just used to help organize the user interface.</summary>
	public class ScreenGroup{
		///<summary>Primary key</summary>
		public int ScreenGroupNum;
		///<summary>Up to the user.</summary>
		public string Description;
		///<summary>Date used to help order the groups.</summary>
		public DateTime SGDate;
		///<summary>Not a database column. Used if ProvNum=0.</summary>
		public string ProvName;
		///<summary>Not a database column. Foreign key to provider.ProvNum. Can be 0 if not a standard provider.  In that case, a ProvName should be entered.</summary>
		public int ProvNum;
		///<summary>Not a database column. See the PlaceOfService enum.</summary>
		public PlaceOfService PlaceService;
		///<summary>Not a database column. Foreign key to county.CountyName, although it will not crash if key absent.</summary>
		public string County;
		///<summary>Not a database column. Foreign key to school.SchoolName, although it will not crash if key absent.</summary>
		public string GradeSchool;
	}

	

	

}













