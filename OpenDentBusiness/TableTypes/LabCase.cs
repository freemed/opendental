using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>A lab case.</summary>
	public class LabCase{
		///<summary>Primary key.</summary>
		public int LabCaseNum;
		///<Summary>FK to patient.PatNum.</Summary>
		public int PatNum;
		///<Summary>FK to laboratory.LaboratoryNum. The lab that the case gets sent to.</Summary>
		public int LaboratoryNum;
		///<Summary>FK to appointment.AptNum.  This is how a lab case is attached to a scheduled appointment.</Summary>
		public int AptNum;
		///<Summary>FK to appointment.AptNum.  This is how a lab case is attached to a planned appointment in addition to the scheduled appointment.</Summary>
		public int PlannedAptNum;
		///<Summary>The due date that is put on the labslip.  NOT when you really need the labcase back, which is usually a day or two later and is the date of the appointment this case is attached to.</Summary>
		public DateTime DateTimeDue;
		///<Summary>When this lab case was created. User can edit.</Summary>
		public DateTime DateTimeCreated;
		///<Summary>Time that it actually went out to the lab.</Summary>
		public DateTime DateTimeSent;
		///<Summary>Date/time received back from the lab.  If this is filled, then the case is considered received.</Summary>
		public DateTime DateTimeRecd;
		///<Summary>Date/time that quality was checked.  It is now completely ready for the patient.</Summary>
		public DateTime DateTimeChecked;

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
			return l;
		}
		
	}
	
	
	

}













