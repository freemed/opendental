using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile {
	///<summary>Unknown,Yes, or No.</summary>
	public enum YN{
		///<summary>0</summary>
		Unknown,
		///<summary>1</summary>
		Yes,
		///<summary>2</summary>
		No
	}

	///<summary>Appointment status.</summary>
	public enum ApptStatus{
		///<summary>0- No appointment should ever have this status.</summary>
		None,
		///<summary>1- Shows as a regularly scheduled appointment.</summary>
		Scheduled,
		///<summary>2- Shows greyed out.</summary>
		Complete,
		///<summary>3- Only shows on unscheduled list.</summary>
		UnschedList,
		///<summary>4- Functions almost the same as Scheduled, but also causes the appointment to show on the ASAP list.</summary>
		ASAP,
		///<summary>5- Shows with a big X on it.</summary>
		Broken,
		///<summary>6- Planned appointment.  Only shows in Chart module. User not allowed to change this status, and it does not display as one of the options.</summary>
		Planned,
		///<summary>7- Patient "post-it" note on the schedule. Shows light yellow. Shows on day scheduled just like appt, as well as in prog notes, etc.</summary>
		PtNote,
		///<summary>8- Patient "post-it" note completed</summary>
		PtNoteCompleted
	}

	///<summary></summary>
	public enum PatientStatus{
		///<summary>0</summary>
		Patient,
		///<summary>1</summary>
		NonPatient,
		///<summary>2</summary>
		Inactive,
		///<summary>3</summary>
		Archived,
		///<summary>4</summary>
		Deleted,
		///<summary>5</summary>
		Deceased
	}

	///<summary></summary>
	public enum PatientGender{
		///<summary>0</summary>
		Male,
		///<summary>1</summary>
		Female,
		///<summary>2- This is not a joke. Required by HIPAA for privacy.</summary>
		Unknown
	}

	///<summary></summary>
	public enum PatientPosition{
		///<summary>0</summary>
		Single,
		///<summary>1</summary>
		Married,
		///<summary>2</summary>
		Child,
		///<summary>3</summary>
		Widowed,
		///<summary>4</summary>
		Divorced
	}
}
