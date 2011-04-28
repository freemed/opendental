using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.Mobile {
	[Serializable]
	[CrudTable(IsMobile=true)]
	public class DrugUnitm:TableBase {
		///<summary>Primary key 1.</summary>
		[CrudColumn(IsPriKeyMobile1=true)]
		public long CustomerNum;
		///<summary>Primary key 2.</summary>
		[CrudColumn(IsPriKeyMobile2=true)]
		public long DrugUnitNum;
		///<summary>Example ml, capitalization not critical. Usually entered as lowercase except for L.</summary>
		public string UnitIdentifier;//VARCHAR(20)/VARCHAR2(20).
		///<summary>Example milliliter.</summary>
		public string UnitText;


		///<summary></summary>
		public DrugUnitm Copy() {
			return (DrugUnitm)this.MemberwiseClone();
		}



	}
}



