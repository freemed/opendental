using System;

namespace OpenDentBusiness.Mobile {

	///<summary>Links medications to patients. Patient portal version</summary>
	[Serializable]
	[CrudTable(IsMobile=true)]
	public class MedicationPatm:TableBase {
		///<summary>Primary key 1.</summary>
		[CrudColumn(IsPriKeyMobile1=true)]
		public long CustomerNum;
		///<summary>Primary key 2.</summary>
		[CrudColumn(IsPriKeyMobile2=true)]
		public long MedicationPatNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to medication.MedicationNum.</summary>
		public long MedicationNum;
		///<summary>Medication notes specific to this patient.</summary>
		public string PatNote;
		///<summary>If true, not a current medication.</summary>
		public bool IsDiscontinued;

		///<summary></summary>
		public MedicationPatm Copy() {
			return (MedicationPatm)this.MemberwiseClone();
		}

	}








}










