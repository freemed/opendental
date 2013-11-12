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
		///<summary>Requires one, one-of-each, two+, all.  </summary>
		public MatchCardinality Cardinality;



		///<summary></summary>
		public EhrTrigger Copy() {
			return (EhrTrigger)this.MemberwiseClone();
		}
	}

	/// <summary></summary>
	public enum MatchCardinality {
		///<summary>0</summary>
		One,
		///<summary>1</summary>
		OneOfEach,
		///<summary>2</summary>
		TwoOrMore,
		///<summary>3</summary>
		All
	}
}