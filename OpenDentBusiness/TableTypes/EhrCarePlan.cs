using System;

namespace OpenDentBusiness{
	[Serializable]
	public class EhrCarePlan:TableBase {

		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrCarePlanNum;
		///<summary>FK to patient.PatNum. </summary>
		public long PatNum;
		///<summary>Snomed code describing the type of educational instruction provided.  Limited to terms descending from the Snomed 409073007 (Education Hierarchy).</summary>
		public string SnomedEducation;
		///<summary>Instructions provided to the patient.</summary>
		public string Instructions;

		///<summary></summary>
		public EhrCarePlan Clone() {
			return (EhrCarePlan)this.MemberwiseClone();
		}

	}
}
