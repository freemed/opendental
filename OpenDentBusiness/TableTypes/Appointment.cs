using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Appointments can show in the Appointments module, or they can be on the unscheduled list.  An appointment object is also used to store the Planned appointment.  The planned appointment never gets scheduled, but instead gets copied.</summary>
	[Serializable()]
	public class Appointment:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AptNum;
		///<summary>FK to patient.PatNum.  The patient that the appointment is for.</summary>
		public long PatNum;
		///<summary>Enum:ApptStatus .</summary>
		public ApptStatus AptStatus;
		///<summary>Time pattern, X for Dr time, / for assist time. Stored in 5 minute increments.  Converted as needed to 10 or 15 minute representations for display.</summary>
		public string Pattern;
		///<summary>FK to definition.DefNum.  This field can also be used to show patient arrived, in chair, etc.  The Category column in the definition table is DefCat.ApptConfirmed.</summary>
		public long Confirmed;
		///<summary>If true, then the program will not attempt to reset the user's time pattern and length when adding or removing procedures.</summary>
		public bool TimeLocked;
		///<summary>FK to operatory.OperatoryNum.</summary>
		public long Op;
		///<summary>Note.</summary>
		public string Note;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;
		///<summary>FK to provider.ProvNum.  Optional.  Only used if a hygienist is assigned to this appt.</summary>
		public long ProvHyg;
		///<summary>Appointment Date and time.  If you need just the date or time for an SQL query, you can use DATE(AptDateTime) and TIME(AptDateTime) in your query.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime AptDateTime;
		///<summary>FK to appointment.AptNum.  A better description of this field would be PlannedAptNum.  Only used to show that this apt is derived from specified planned apt. Otherwise, 0.</summary>
		public long NextAptNum;
		///<summary>FK to definition.DefNum.  The definition.Category in the definition table is DefCat.RecallUnschedStatus.  Only used if this is an Unsched or Planned appt.</summary>
		public long UnschedStatus;
		///<summary>This is the first appoinment this patient has had at this office.  Somewhat automated.</summary>
		public bool IsNewPatient;
		///<summary>A one line summary of all procedures.  Can be used in various reports, Unscheduled list, and Planned appointment tracker.  Not user editable right now, so it doesn't show on the screen.</summary>
		public string ProcDescript;
		///<summary>FK to employee.EmployeeNum.  You can assign an assistant to the appointment.</summary>
		public long Assistant;
		///<summary>FK to clinic.ClinicNum.  0 if no clinic.</summary>
		public long ClinicNum;
		///<summary>Set true if this is a hygiene appt.  The hygiene provider's color will show.</summary>
		public bool IsHygiene;
		///<summary>Automatically updated by MySQL every time a row is added or changed.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>The date and time that the patient checked in.  Date is largely ignored since it should be the same as the appt.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeArrived;
		///<summary>The date and time that the patient was seated in the chair in the operatory.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeSeated;
		///<summary>The date and time that the patient got up out of the chair</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeDismissed;
		///<summary>FK to insplan.PlanNum for the primary insurance plan at the time the appointment is set complete. May be 0.</summary>
		public long InsPlan1;
		///<summary>FK to insplan.PlanNum for the secoondary insurance plan at the time the appointment is set complete. May be 0.</summary>
		public long InsPlan2;
		///<summary>Time. Ignore date portion.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime TimeAskedToArrive;

		///<summary>Returns a copy of the appointment.</summary>
    public Appointment Clone(){
			return (Appointment)this.MemberwiseClone();
		}

		
	}
	
	


}









