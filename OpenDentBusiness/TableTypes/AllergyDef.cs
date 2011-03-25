using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class AllergyDef:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AllergyDefNum;
		///<summary></summary>
		public string Description;
		///<summary></summary>
		public bool IsHidden;

		///<summary></summary>
		public AllergyDef Copy() {
			return (AllergyDef)this.MemberwiseClone();
		}
	}
}