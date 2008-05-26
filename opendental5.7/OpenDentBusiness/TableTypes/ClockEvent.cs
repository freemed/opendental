using System;
using System.Collections;


namespace OpenDentBusiness{

	///<summary>Each row is either a clock-in or a clock-out event.  This table will soon be significantly changed so that each row will contain both the clock-in and the clock-out.  As it is right now, it's nearly impossible to do queries that give you summary results.</summary>
	public class ClockEvent{
		///<summary>Primary key.</summary>
		public int ClockEventNum;
		///<summary>FK to employee.EmployeeNum</summary>
		public int EmployeeNum;
		///<summary>The actual time that this entry was entered.</summary>
		public DateTime TimeEntered;
		///<summary>The time to display and to use in all calculations.</summary>
		public DateTime TimeDisplayed;
		///<summary>True for ClockIn, and false for ClockOut.</summary>
		public bool ClockIn;
		///<summary>Enum:TimeClockStatus  Home, Lunch, or Break.</summary>
		public TimeClockStatus ClockStatus;
		///<summary>.</summary>
		public string Note;
		//<summary></summary>
		//public DateTime TimeEnteredTwo;
		//<summary></summary>
		//public DateTime TimeDisplayedTwo;

		///<summary></summary>
		public ClockEvent Copy() {
			ClockEvent c=new ClockEvent();
			c.ClockEventNum=ClockEventNum;
			c.EmployeeNum=EmployeeNum;
			c.TimeEntered=TimeEntered;
			c.TimeDisplayed=TimeDisplayed;
			c.ClockIn=ClockIn;
			c.ClockStatus=ClockStatus;
			c.Note=Note;
			return c;
		}



	}

	
}




