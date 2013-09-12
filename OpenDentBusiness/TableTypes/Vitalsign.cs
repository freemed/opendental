using System;

namespace OpenDentBusiness {
	///<summary>For EHR module, one dated vital sign entry.  BMI is calulated on demand based on height and weight and may be one of 4 ALOINC codes. 39156-5 "Body mass index (BMI) [Ratio]" is most applicable.</summary>
	[Serializable]
	public class Vitalsign:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long VitalsignNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Height of patient in inches. Fractions might be needed some day.  Allowed to be 0.  Six possible LOINC codes, most applicable is 8302-2, "Body height".</summary>
		public float Height;
		///<summary>Lbs.  Allowed to be 0. Six possible LOINC codes, most applicable is 29463-7, "Body weight".</summary>
		public float Weight;
		///<summary>Allowed to be 0. LOINC code 8480-6.</summary>
		public int BpSystolic;
		///<summary>Allowed to be 0. LOINC code 8462-4.</summary>
		public int BpDiastolic;
		///<summary>The date that the vitalsigns were taken.</summary>
		public DateTime DateTaken;
		///<summary>For an abnormal BMI measurement this must be true in order to meet quality measurement.</summary>//intervention? I think these should be deprecated and use an Intervention object instead.
		public bool HasFollowupPlan;
		///<summary>If a BMI was not recored, this must be true in order to meet quality measurement.  For children, this is used as an IsPregnant flag, the only valid reason for not taking BMI on children.</summary>//intervention? I think these should be deprecated and use an Intervention object instead.
		public bool IsIneligible;
		///<summary>For HasFollowupPlan or IsIneligible, this documents the specifics.</summary>//intervention? I think these should be deprecated and use an Intervention object instead.
		public string Documentation;
		///<summary>.</summary>//intervention? I think these should be deprecated and use an Intervention object instead.
		public bool ChildGotNutrition;
		///<summary>.</summary>//intervention? I think these should be deprecated and use an Intervention object instead.
		public bool ChildGotPhysCouns;
		/////<summary>Used for CQMs.  SNOMED CT code either Normal="", Overweight="238131007", or Underweight="248342006".  Set when BMI is found to be "out of range", based on age groups.  Should be calculated when vital sign is saved.  Calculate based on age as of Jan 1 of the year vitals were taken.  Not currently displayed to user.</summary>
		//public string WeightCode;

		///<summary></summary>
		public Vitalsign Copy() {
			return (Vitalsign)MemberwiseClone();
		}

	}
}
