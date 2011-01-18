using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenDentBusiness{

	///<summary>One block of time.  Either for practice, provider, or blockout.</summary>
	[Serializable]
	public class Schedule:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ScheduleNum;
		///<summary>Date for this timeblock.</summary>
		public DateTime SchedDate;
		///<summary>Start time for this timeblock.</summary>
		[XmlIgnore]
		public TimeSpan StartTime;
		///<summary>Stop time for this timeblock.</summary>
		[XmlIgnore]
		public TimeSpan StopTime;
		///<summary>Enum:ScheduleType 0=Practice,1=Provider,2=Blockout,3=Employee.  Practice is used as a way to indicate holidays and as a way to put a note in for the entire practice for one day.  But whenever type is Practice, times will be ignored.</summary>
		public ScheduleType SchedType;
		///<summary>FK to provider.ProvNum if a provider type.</summary>
		public long ProvNum;
		///<summary>FK to definition.DefNum if blockout.  eg. HighProduction, RCT Only, Emerg.</summary>
		public long BlockoutType;
		///<summary>This contains various types of text entered by the user.</summary>
		public string Note;
		///<summary>Enum:SchedStatus enumeration 0=Open,1=Closed,2=Holiday.  All blocks have a status of Open, but user doesn't see the status.  The "closed" status was previously used to override the defaults when the last timeblock was deleted.  But it's nearly phased out now.  Still used by blockouts.  Holidays are a special type of practice schedule item which do not have providers attached.</summary>
		public SchedStatus Status;
		///<summary>FK to employee.EmployeeNum.</summary>
		public long EmployeeNum;
		///<summary>Not a db column.  Holds a list of ops that this schedule is assigned to.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public List<long> Ops;

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("StartTime",typeof(long))]
		public long StartTimeXml {
			get {
				return StartTime.Ticks;
			}
			set {
				StartTime = TimeSpan.FromTicks(value);
			}
		}

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("StopTime",typeof(long))]
		public long StopTimeXml {
			get {
				return StopTime.Ticks;
			}
			set {
				StopTime = TimeSpan.FromTicks(value);
			}
		}

		public Schedule Copy() {
			Schedule retVal=(Schedule)this.MemberwiseClone();
			retVal.Ops=new List<long>(Ops);
			return retVal;
		}

		public Schedule(){
			Ops=new List<long>();
		}

		/*
		public Schedule(long scheduleNum,DateTime schedDate,TimeSpan startTime,TimeSpan stopTime,ScheduleType schedType,
			long provNum,long blockoutType,string note,SchedStatus status,long employeeNum)
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
		}*/
		
	}

	

	

}













