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
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary></summary>
		public AllergyDef Copy() {
			return (AllergyDef)this.MemberwiseClone();
		}
	}
}