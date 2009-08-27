using System;

namespace OpenDentBusiness{
	
	///<summary>Links medications to patients.</summary>
	public class MedicationPat{
		///<summary>Primary key.</summary>
		public long MedicationPatNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to medication.MedicationNum.</summary>
		public long MedicationNum;
		///<summary>Medication notes specific to this patient.</summary>
		public string PatNote;
	}


	





}










