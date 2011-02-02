using System;

namespace OpenDentBusiness{

	///<summary>Essentially more columns in the patient table.  They are stored here because these fields can contain a lot of information, and we want to try to keep the size of the patient table a bit smaller.</summary>
	[Serializable]
	public class PatientNote:TableBase {
		///<summary>FK to patient.PatNum.  Also the primary key for this table. Always one to one relationship with patient table.  A new patient might not have an entry here until needed.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatNum;
		///<summary>Only one note per family stored with guarantor.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.ExcludeFromUpdate)]
		public string FamFinancial;
		///<summary>No longer used.</summary>
		public string ApptPhone;
		///<summary>Medical Summary</summary>
		public string Medical;
		///<summary>Service notes</summary>
		public string Service;
		///<summary>Complete current Medical History</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string MedicalComp;
		///<summary>Shows in the Chart module just below the graphical tooth chart.</summary>
		public string Treatment;

	}


	

	

}










