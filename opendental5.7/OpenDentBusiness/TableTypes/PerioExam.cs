using System;

namespace OpenDentBusiness{
	
	///<summary>One perio exam for one patient on one date.  Has lots of periomeasures attached to it.</summary>
	public class PerioExam{
		///<summary>Primary key.</summary>
		public int PerioExamNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>.</summary>
		public DateTime ExamDate;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
	}


	
	

}















