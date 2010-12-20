using System;

namespace OpenDentBusiness{

	///<summary>One perio exam for one patient on one date.  Has lots of periomeasures attached to it.</summary>
	[Serializable]
	public class PerioExam:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PerioExamNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>.</summary>
		public DateTime ExamDate;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;
	}


	
	

}















