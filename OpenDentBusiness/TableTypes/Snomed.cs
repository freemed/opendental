using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>We do not import synonyms, only "Fully Qualified Name records". Snomed for holding a large list of codes. Codes in use are copied into the DiseaseDef table. Primary key is an arbitrary number, allowing code to change if needed, although that should generally be prohibited.</summary>
	[Serializable]
	public class Snomed:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SnomedNum;
		///<summary>Also called the Concept ID. Not allowed to edit this column once saved in the database.</summary>
		public string SnomedCode;
		///<summary>This should be Also called "Term", "Name", or "Fully Qualified Name". Should not be editable.</summary>
		public string Description;
		///<summary>When a snomed code is depricated it is set to inactive.</summary>
		public bool IsActive;
		///<summary>Date that snomed code was defined by International Health Terminology Standards Development Organisation (IHTSDO).</summary>
		public DateTime DateOfStandard;
		/////<summary>The last date and time this row was altered.  Not user editable.</summary>
		//[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		//public DateTime DateTStamp;

		///<summary></summary>
		public Snomed Copy() {
			return (Snomed)this.MemberwiseClone();
		}

	}
}