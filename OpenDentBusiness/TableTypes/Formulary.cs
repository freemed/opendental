using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>A list of medications that are preferred by an insurance or Medicaid.</summary>
	[Serializable]
	public class Formulary:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long FormularyNum;
		///<summary>Description.</summary>
		public string Description;

		///<summary></summary>
		public Formulary Copy() {
			return (Formulary)this.MemberwiseClone();
		}


	}
}
