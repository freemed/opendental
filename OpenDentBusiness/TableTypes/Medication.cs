using System;

namespace OpenDentBusiness{

	///<summary>A list of medications, not attached to any particular patient.  Not allowed to delete if in use by a patient.  Not allowed to edit name once created due to possibility of damage to patient record.</summary>
	public class Medication{
		///<summary>Primary key.</summary>
		public int MedicationNum;
		///<summary>Name of the medication.</summary>
		public string MedName;
		///<summary>FK to medication.MedicationNum.  If this is a generic drug, then the GenericNum will be the same as the MedicationNum.</summary>
		public int GenericNum;
		///<summary>Notes.</summary>
		public string Notes;
	}
	


	




}










