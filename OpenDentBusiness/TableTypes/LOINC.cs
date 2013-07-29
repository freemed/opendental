using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Used in the accounting section in chart of accounts.  Not related to patient accounts in any way.</summary>
	[Serializable()]
	public class LOINC:TableBase{
		///<summary>Primary key..</summary>
		[CrudColumn(IsPriKey=true)]
		public long LOINCNum;
		///<summary>.</summary>
		public string LOINCCode;
		///<summary>Unified Code for Units of Measure (UCUM).</summary>
		public string UCUMUnits;
		///<summary>.</summary>
		public string LongName;
		///<summary>.</summary>
		public string ShortName;
		///<summary>.</summary>
		public string OrderObs;

		///<summary></summary>
		public LOINC Clone() {
			return (LOINC)this.MemberwiseClone();
		}

	}

	
}




