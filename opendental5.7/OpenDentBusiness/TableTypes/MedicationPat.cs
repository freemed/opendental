using System;

namespace OpenDentBusiness{
	
	///<summary>Links medications to patients.</summary>
	public class MedicationPat{
		///<summary>Primary key.</summary>
		public int MedicationPatNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to medication.MedicationNum.</summary>
		public int MedicationNum;
		///<summary>Medication notes specific to this patient.</summary>
		public string PatNote;
	}


	





}










