using System;
using System.Collections;

namespace OpenDentBusiness {

	/// <summary>Many-to-many relationship connecting Rx with DiseaseDef, AllergyDef, or Medication.  Only one of those links may be specified in a single row; the other two will be 0.</summary>
	[Serializable]
	public class RxAlert:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long RxAlertNum;
		///<summary>FK to rxdef.RxDefNum.</summary>
		public long RxDefNum;
		///<summary>FK to diseasedef.DiseaseDefNum.  Only if DrugProblem interaction.  This is compared against disease.DiseaseDefNum using PatNum.</summary>
		public long DiseaseDefNum;
		///<summary>FK to allergydef.AllergyDefNum.  Only if DrugAllergy interaction.  The allergy and allergydef tables do not yet exist.  Once they are in place in place, this will be compared against allergy.AllergyDefNum using PatNum.</summary>
		public long AllergyDefNum;
		///<summary>FK to medication.MedicationNum.  Only if DrugDrug interaction.  This will be compared against medicationpat.MedicationNum using PatNum.</summary>
		public long MedicationNum;
		///<summary>This is typically blank, so a default message will be displayed by OD.  But if this contains a message, then this message will be used instead.</summary>
		public string NotificationMsg;

		///<summary></summary>
		public RxAlert Copy() {
			return (RxAlert)this.MemberwiseClone();
		}

		
		
	}

		



		
	

	

	


}










