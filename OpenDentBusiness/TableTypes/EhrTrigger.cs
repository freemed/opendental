using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Used for CDS automation.  May later be expanded to replace "automation."</summary>
	[Serializable]
	public class EhrTrigger:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrTriggerNum;
		///<summary>Short description to describe the trigger.</summary>
		public string Description;
		///<summary></summary>
		public string ProblemSnomedList;
		///<summary></summary>
		public string ProblemIcd9List;
		///<summary></summary>
		public string ProblemIcd10List;
		///<summary></summary>
		public string ProblemDefNumList;
		///<summary></summary>
		public string MedicationNumList;
		///<summary></summary>
		public string RxCuiList;
		///<summary></summary>
		public string CvxList;
		///<summary></summary>
		public string AllergyDefNumList;
		///<summary>Age, Gender.  Can be multiple age entries but only one gender entry as coma delimited values.  Example: " age,>18  age&lt;=55  gender,male"
		///</summary>
		public string DemographicsList;
		///<summary></summary>
		public string LabLoincList;
		///<summary>Height, Weight, Bp s/d, and BMI</summary>
		public string VitalLoincList;
		///<summary>Requires One, OneOfEachCategory, TwoOrMore, or All for trigger to match.  </summary>
		public MatchCardinality Cardinality;

		///<summary></summary>
		public EhrTrigger Copy() {
			return (EhrTrigger)this.MemberwiseClone();
		}

		public EhrTrigger() {
			Description="";
			ProblemSnomedList="";
			ProblemIcd9List="";
			ProblemIcd10List="";
			ProblemDefNumList="";
			MedicationNumList="";
			RxCuiList="";
			CvxList="";
			AllergyDefNumList="";
			DemographicsList="";
			LabLoincList="";
			VitalLoincList="";
			Cardinality=MatchCardinality.One;
		}

		///<summary>Used for displaying what elements of the trigger are set. Example: Medication, Demographics</summary>
		public string GetTriggerCategories() {
			string retVal="";
			if(ProblemSnomedList.Trim()!=""
				|| ProblemIcd9List.Trim()!=""
				|| ProblemIcd10List.Trim()!=""
				|| ProblemDefNumList.Trim()!="") 
			{
				retVal+="Problem";
			}
			if(MedicationNumList.Trim()!=""
				|| CvxList.Trim()!=""
				|| RxCuiList.Trim()!="") {
				retVal+=(retVal==""?"":", ")+"Medication";
			}
			if(AllergyDefNumList.Trim()!=""){
				retVal+=(retVal==""?"":", ")+"Allergy";
			}
			if(DemographicsList.Trim()!="") {
				retVal+=(retVal==""?"":", ")+"Demographic";
			}
			if(LabLoincList.Trim()!="") {
				retVal+=(retVal==""?"":", ")+"Lab Result";
			}
			if(VitalLoincList.Trim()!="") {
				retVal+=(retVal==""?"":", ")+"Vitals";
			}
			return retVal;
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