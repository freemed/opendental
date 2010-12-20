using System;

namespace OpenDentBusiness{

	///<summary>Used in public health.  Programming note: There are many extra fields in common with the screen table, but they are only in this struct and not in the database itself, where that data is stored with the individual screen items. The data in this table is irrelevant in reports.  It is just used to help organize the user interface.</summary>
	[Serializable]
	public class ScreenGroup:TableBase {
		///<summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long ScreenGroupNum;
		///<summary>Up to the user.</summary>
		public string Description;
		///<summary>Date used to help order the groups.</summary>
		public DateTime SGDate;
		///<summary>Not a database column. Used if ProvNum=0.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public string ProvName;
		///<summary>Not a database column. Foreign key to provider.ProvNum. Can be 0 if not a standard provider.  In that case, a ProvName should be entered.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public long ProvNum;
		///<summary>Not a database column. See the PlaceOfService enum.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public PlaceOfService PlaceService;
		///<summary>Not a database column. Foreign key to county.CountyName, although it will not crash if key absent.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public string County;
		///<summary>Not a database column. Foreign key to school.SchoolName, although it will not crash if key absent.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public string GradeSchool;
	}

	

	

}













