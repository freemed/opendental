using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>multiple segments per message</summary>
	[Serializable]
	public class HL7DefSegment:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7DefSegmentNum;
		///<summary>FK to HL7DefMessage.HL7DefMessageNum</summary>
		public long HL7DefMessageNum;
		///<summary>.</summary>
		public int ItemOrder;
		///<summary>For example, a DFT can have multiple FT1 segments.</summary>
		public bool CanRepeat;
		///<summary>An incoming message may or may not contain this segment. Not used for outgoing.</summary>
		public bool IsOptional;
		///<summary>.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Note;

		///<summary></summary>
		public HL7DefSegment Clone() {
			return (HL7DefSegment)this.MemberwiseClone();
		}

	}
}
