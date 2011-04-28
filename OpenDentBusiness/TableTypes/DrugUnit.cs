using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>And other kinds of units.  We will only prefill this list with units needed for the tests.  Users would have to manually add any other units.</summary>
	[Serializable]
	public class DrugUnit:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DrugUnitNum;
		///<summary>Example ml, capitalization not critical. Usually entered as lowercase except for L.</summary>
		public string UnitIdentifier;//VARCHAR(20)/VARCHAR2(20).
		///<summary>Example milliliter.</summary>
		public string UnitText;
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary></summary>
		public DrugUnit Copy() {
			return (DrugUnit)this.MemberwiseClone();
		}

	}
}