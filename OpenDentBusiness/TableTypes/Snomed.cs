using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Snomed for holding a large list of codes. Codes in use are copied into the DiseaseDef table. Primary key is an arbitrary number, allowing code to change if needed, although that should generally be prohibited.</summary>
	[Serializable]
	public class Snomed:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SnomedNum;
		///<summary>Also called the Concept ID. Not allowed to edit this column once saved in the database.</summary>
		public string SnomedCode;
		///<summary>Also called the "term" or sometimes the "name". Is editable</summary>
		public string Description;
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary></summary>
		public Snomed Copy() {
			return (Snomed)this.MemberwiseClone();
		}

	}
}