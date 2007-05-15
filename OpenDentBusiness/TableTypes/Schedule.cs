using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>One block of time that overrides the default sched.  Either for practice, provider, or blockout.</summary>
	public class Schedule{
		///<summary>Primary key.</summary>
		public int ScheduleNum;
		///<summary>Date for this timeblock.</summary>
		public DateTime SchedDate;
		///<summary>Start time for this timeblock.</summary>
		public DateTime StartTime;
		///<summary>Stop time for this timeblock.</summary>
		public DateTime StopTime;
		///<summary>Enum:ScheduleType 0=Practice,1=Provider,2=Blockout,3=Employee.  Practice is used as a way to indicate holidays and as a way to put a note in for the entire practice for one day.  But whenever type is Practice, times will be ignored.</summary>
		public ScheduleType SchedType;
		///<summary>FK to provider.ProvNum if a provider type.</summary>
		public int ProvNum;
		///<summary>FK to definition.DefNum if blockout.  eg. HighProduction, RCT Only, Emerg.</summary>
		public int BlockoutType;
		///<summary>This contains various types of text entered by the user.</summary>
		public string Note;
		///<summary>Enum:SchedStatus enumeration 0=Open,1=Closed,2=Holiday.  All blocks have a status of Open, but user doesn't see the status.  The "closed" status was previously used to override the defaults when the last timeblock was deleted.  But it's nearly phased out now.  Still used by blockouts.  Holidays are a special type of practice schedule item which do not have providers attached.</summary>
		public SchedStatus Status;
		///<summary>FK to operatory.OperatoryNum.  Only used right now for Blockouts.  If 0, then it applies to all ops.</summary>
		public int Op;
		///<summary>FK to employee.EmployeeNum.</summary>
		public int EmployeeNum;

		public Schedule Copy(){
			Schedule s=new Schedule();
			s.ScheduleNum=ScheduleNum;
			s.SchedDate=SchedDate;
			s.StartTime=StartTime;
			s.StopTime=StopTime;
			s.SchedType=SchedType;
			s.ProvNum=ProvNum;
			s.BlockoutType=BlockoutType;
			s.Note=Note;
			s.Status=Status;
			s.Op=Op;
			s.EmployeeNum=EmployeeNum;
			return s;
		}
		
	}

	

	

}













