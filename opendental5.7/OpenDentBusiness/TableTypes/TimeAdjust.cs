using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used on employee timecards to make adjustments.</summary>
	public class TimeAdjust{
		///<summary>Primary key.</summary>
		public int TimeAdjustNum;
		///<summary>FK to employee.EmployeeNum</summary>
		public int EmployeeNum;
		///<summary>The date and time that this entry will show on timecard.</summary>
		public DateTime TimeEntry;
		///<summary>The number of regular hours to adjust timecard by.  Can be + or -.</summary>
		public TimeSpan RegHours;
		///<summary>Overtime hours. Usually +.  Usually combined with a - adj to RegHours.</summary>
		public TimeSpan OTimeHours;
		///<summary>.</summary>
		public string Note;
		
		///<summary></summary>
		public TimeAdjust Copy() {
			TimeAdjust t=new TimeAdjust();
			t.TimeAdjustNum=TimeAdjustNum;
			t.EmployeeNum=EmployeeNum;
			t.TimeEntry=TimeEntry;
			t.RegHours=RegHours;
			t.OTimeHours=OTimeHours;
			t.Note=Note;
			return t;
		}


		




	}

	
}




