using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>For EHR module, automate measure calculation.</summary>
	[Serializable]
	public class EhrMeasure:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrMeasureNum;
		///<summary>Enum:EhrMeasureType</summary>
		public EhrMeasureType MeasureType;
		///<summary>0-100, -1 indicates not entered yet.</summary>
		public int Numerator;
		///<summary>0-100, -1 indicates not entered yet.</summary>
		public int Denominator;
		///<summary></summary>
		[CrudColumn(IsNotDbColumn=true)]
		public string Objective;
		///<summary></summary>
		[CrudColumn(IsNotDbColumn=true)]
		public string Measure;

		///<summary></summary>
		public EhrMeasure Copy() {
			return (EhrMeasure)MemberwiseClone();
		}

	}

	public enum EhrMeasureType {

	}
}
