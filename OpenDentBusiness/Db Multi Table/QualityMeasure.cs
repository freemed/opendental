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
		WeightChild,
		ImmunizeChild//break this into 12

	}
}
