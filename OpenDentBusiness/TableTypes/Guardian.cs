using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
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
