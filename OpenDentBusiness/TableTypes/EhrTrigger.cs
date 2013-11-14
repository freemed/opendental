using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Used for CDS automation.  May later be expanded to replace "automation."</summary>
	[Serializable]
	public class EhrTrigger:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AutomationTriggerNum;
		///<summary>Short description to describe the trigger.</summary>
		public string Description;
		///<summary></summary>
		public string SnomedList;
		///<summary></summary>
		public string Icd9List;
		///<summary></summary>
		public string Icd10List;
		///<summary></summary>
		public string CvxList;
		///<summary></summary>
		public string RxCuiList;
		///<summary>LabResults and VitalSign. </summary>
		public string LoincList;
		///<summary>Applies if patient is younger than this many years. </summary>
		public int DemographicAgeLessThan;
		///<summary>Applies if patient is older than this many years. </summary>
		public int DemographicAgeGreaterThan;
		///<summary>M, F, U.  Comma delimited list. </summary>
		public string DemographicGender;
		///<summary>Requires One, OneOfEachCategory, TwoOrMore, or All for trigger to match.  </summary>
		public MatchCardinality Cardinality;



		///<summary></summary>
		public EhrTrigger Copy() {
			return (EhrTrigger)this.MemberwiseClone();
		}
	}

	/// <summary></summary>
	public enum MatchCardinality {
		///<summary>0 - If any one of the conditions are met from any of the categories.</summary>
		One,
		///<summary>1 - Must have one match from each of the categories with set values. Categories are :Medication, Allergy, Problem, Vitals, Age, Gender, and Lab Results.</summary>
		OneOfEachCategory,
		///<summary>2 - Must match any two conditions, may be from same category.</summary>
		TwoOrMore,
		///<summary>3 - Must match every code defined in the EhrTrigger.</summary>
		All
	}
}