using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Only used by OD customer support to store and track Ehr Provider Keys for customers.</summary>
	[Serializable]
	public class EhrProvKey:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrProvKeyNum;
		///<summary>FK to patient.PatNum. There can be multiple EhrProvKeys per patient/customer.</summary>
		public long PatNum;
		///<summary>The provider LName.</summary>
		public string LName;
		///<summary>The provider FName.</summary>
		public string FName;
		///<summary>The key assigned to the provider</summary>
		public string ProvKey;

		///<summary></summary>
		public EhrMeasure Copy() {
			return (EhrMeasure)MemberwiseClone();
		}

	}

	

}
