using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenDentBusiness{

	///<summary>One block of time.  Either for practice, provider, or blockout.</summary>
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
		///<summary>FK to employee.EmployeeNum.</summary>
		public int EmployeeNum;
		///<summary>Not a db column.  Holds a list of ops that this schedule is assigned to.</summary>
		public List<int> Ops;

		public Schedule Copy(){
			Schedule retVal=(Schedule)this.MemberwiseClone();
			retVal.Ops=new List<int>(Ops);
			return retVal;
		}

		public Schedule(){
			Ops=new List<int>();
		}

		public Schedule(int scheduleNum,DateTime schedDate,DateTime startTime,DateTime stopTime,ScheduleType schedType,
			int provNum,int blockoutType,string note,SchedStatus status,int employeeNum)
		{
			ScheduleNum=scheduleNum;
			SchedDate=schedDate;
			StartTime=startTime;
			StopTime=stopTime;
			SchedType=schedType;
			ProvNum=provNum;
			BlockoutType=blockoutType;
			Note=note;
			Status=status;
			EmployeeNum=employeeNum;
		}
		
	}

	

	

}













