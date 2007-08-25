using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>A lab case.</summary>
	public class LabCase{
		///<summary>Primary key.</summary>
		public int LabCaseNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to laboratory.LaboratoryNum. The lab that the case gets sent to.</summary>
		public int LaboratoryNum;
		///<summary>FK to appointment.AptNum.  This is how a lab case is attached to a scheduled appointment. 1:1 relationship for now.  Only one labcase per appointment, and (obviously) only one appointment per labcase.  Labcase can exist without being attached to any appointments at all, making this zero.</summary>
		public int AptNum;
		///<summary>FK to appointment.AptNum.  This is how a lab case is attached to a planned appointment in addition to the scheduled appointment.</summary>
		public int PlannedAptNum;
		///<summary>The due date that is put on the labslip.  NOT when you really need the labcase back, which is usually a day or two later and is the date of the appointment this case is attached to.</summary>
		public DateTime DateTimeDue;
		///<summary>When this lab case was created. User can edit.</summary>
		public DateTime DateTimeCreated;
		///<summary>Time that it actually went out to the lab.</summary>
		public DateTime DateTimeSent;
		///<summary>Date/time received back from the lab.  If this is filled, then the case is considered received.</summary>
		public DateTime DateTimeRecd;
		///<summary>Date/time that quality was checked.  It is now completely ready for the patient.</summary>
		public DateTime DateTimeChecked;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>The Rx for this labcase.</summary>
		public string Instructions;

		public LabCase Copy(){
			LabCase l=new LabCase();
			l.LabCaseNum=LabCaseNum;
			l.PatNum=PatNum;
			l.LaboratoryNum=LaboratoryNum;
			l.AptNum=AptNum;
			l.PlannedAptNum=PlannedAptNum;
			l.DateTimeDue=DateTimeDue;
			l.DateTimeCreated=DateTimeCreated;
			l.DateTimeSent=DateTimeSent;
			l.DateTimeRecd=DateTimeRecd;
			l.DateTimeChecked=DateTimeChecked;
			l.ProvNum=ProvNum;
			l.Instructions=Instructions;
			return l;
		}
		
	}
	
	
	

}













