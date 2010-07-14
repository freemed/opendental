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

		public string GuardianRelationshipStr(){
			switch(Relationship){
				case GuardianRelationship.Father: return "(d)";
				case GuardianRelationship.Mother: return "(m)";
				case GuardianRelationship.Stepfather: return "(sf)";
				case GuardianRelationship.Stepmother: return "(sm)";
				case GuardianRelationship.Grandfather: return "(gf)";
				case GuardianRelationship.Grandmother: return "(gm)";
				case GuardianRelationship.Sitter: return "(s)";
			}
			return "";
		}

		///<summary></summary>
		public Guardian Clone() {
			return (Guardian)this.MemberwiseClone();
		}

	}
}
