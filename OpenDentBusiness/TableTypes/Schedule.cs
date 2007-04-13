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
		///<summary>Enum:ScheduleType Practice,Provider,Blockout.</summary>
		public ScheduleType SchedType;
		///<summary>FK to provider.ProvNum if a provider type.</summary>
		public int ProvNum;
		///<summary>FK to definition.DefNum if blockout.  eg. HighProduction, RCT Only, Emerg.</summary>
		public int BlockoutType;
		///<summary>This contains various types of text entered by the user.</summary>
		public string Note;
		///<summary>Enum:SchedStatus enumeration Open, Closed, Holiday.  All blocks have a status of Open, but user doesn't see the status.  There is one hidden blockout with a status of closed for when user deletes the last default block on a day.</summary>
		public SchedStatus Status;
		///<summary>FK to definition.DefNum.  Only used right now for Blockouts.  Will later add practice type.  If 0, then it applies to all ops.</summary>
		public int Op;

		
		
	}

	

	

}













