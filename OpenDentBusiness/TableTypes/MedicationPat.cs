using System;

namespace OpenDentBusiness{

	///<summary>Links medications to patients.  For ehr, some of these can be considered 'medication orders', but only if they contain a PatNote (instructions), a ProvNum, and a DateStart.</summary>
	[Serializable]
	public class MedicationPat:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long MedicationPatNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to medication.MedicationNum.</summary>
		public long MedicationNum;
		///<summary>Medication notes specific to this patient.</summary>
		public string PatNote;
		///<summary>The last date and time this row was altered.  Not user editable.  Will be set to NOW by OD if this patient gets an OnlinePassword assigned.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>Date that the medication was started.  Can be minval if unknown.</summary>
		public DateTime DateStart;
		///<summary>Date that the medication was stopped.  Can be minval if unknown.  If not minval, then this medication is "discontinued".</summary>
		public DateTime DateStop;
		///<summary>FK to provider.ProvNum. Can be 0. Gets set to the patient's primary provider when adding a new med.  If adding the med from EHR, gets set to the ProvNum of the logged-in user.</summary>
		public long ProvNum;
	}


	





}










