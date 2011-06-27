using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used by ehr.</summary>
	public class QualityMeasure {
		public QualityType Type;
		public string Id;
		public string Descript;
		public int Denominator;
		public int Numerator;
		public int Exclusions;
		public int NotMet;
		///<summary>Always 100</summary>
		public int ReportingRate;
		///<summary>Numerator/(Numerator+NotMet)</summary>
		public int PerformanceRate;
		public string DenominatorExplain;
		public string NumeratorExplain;
		public string ExclusionsExplain;
	}

	public enum QualityType {
		WeightOver65,
		WeightAdult,
		Hypertension,
		TobaccoUse,
		TobaccoCessation,
		InfluenzaAdult,
		WeightChild_1_1,
		WeightChild_1_2,
		WeightChild_1_3,
		WeightChild_2_1,
		WeightChild_2_2,
		WeightChild_2_3,
		WeightChild_3_1,
		WeightChild_3_2,
		WeightChild_3_3,
		ImmunizeChild//break this into 12

	}
}
