using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used on employee timecards to make adjustments.</summary>
	[Serializable]
	public class TimeAdjust:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TimeAdjustNum;
		///<summary>FK to employee.EmployeeNum</summary>
		public long EmployeeNum;
		///<summary>The date and time that this entry will show on timecard.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime TimeEntry;
		///<summary>The number of regular hours to adjust timecard by.  Can be + or -.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeSpanNeg)]
		public TimeSpan RegHours;
		///<summary>Overtime hours. Usually +.  Automatically combined with a - adj to RegHours.  Another option is clockevent.OTimeHours.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeSpanNeg)]
		public TimeSpan OTimeHours;
		///<summary>.</summary>
		public string Note;
		///<summary>Set to true if this adjustment was automatically made by the system.  When the calc weekly ot tool is run, these types of adjustments are fair game for deletion.  Other adjustments are preserved.</summary>
		public bool IsAuto;
		
		///<summary></summary>
		public TimeAdjust Copy() {
			return (TimeAdjust)MemberwiseClone();
		}


		




	}

	
}




