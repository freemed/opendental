using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable()]
	public class DependantRelat:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DependantRelatNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNumChild;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNumRelated;
		///<summary>Enum:FamilyRelationship Father, Mother, Stepfather, Stepmother, Grandfather, Grandmother, Sitter.</summary>
		public FamilyRelationship Relationship;

		public string FamilyRelationshipStr(){
			switch(Relationship){
				case FamilyRelationship.Father: return "(d)";
				case FamilyRelationship.Mother: return "(m)";
				case FamilyRelationship.Stepfather: return "(sf)";
				case FamilyRelationship.Stepmother: return "(sm)";
				case FamilyRelationship.Grandfather: return "(gf)";
				case FamilyRelationship.Grandmother: return "(gm)";
				case FamilyRelationship.Sitter: return "(s)";
			}
			return "";
		}

		///<summary></summary>
		public DependantRelat Clone() {
			return (DependantRelat)this.MemberwiseClone();
		}

	}
}
