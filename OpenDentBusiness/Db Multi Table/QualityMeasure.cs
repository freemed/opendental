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
		public int ReportingRate;
		public int PerformanceRate;
	}

	public enum QualityType {
		WeightAdult_a,
		WeightAdult_b
	}
}
