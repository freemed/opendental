using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Links patient to patient in an n-n relationship.  A guardian need not be in the same family.</summary>
	[Serializable()]
	public class Guardian:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long GuardianNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNumChild;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNumGuardian;
		///<summary>Enum:GuardianRelationship Father, Mother, Stepfather, Stepmother, Grandfather, Grandmother, Sitter.</summary>
		public GuardianRelationship Relationship;

		///<summary></summary>
		public Guardian Clone() {
			return (Guardian)this.MemberwiseClone();
		}

	}
}
