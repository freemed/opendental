using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Links patient to patient in an n-n relationship.  A guardian need not be in the same family.
	///A guardian could be in another family if the relationship was entered, then one of the patients in the relationship is moved to another family.
	///This table can also be used for other types of relationships, and also allows the user to specify any relationship as a guardian.
	///For example, a retired person might specify their brother or child as their guardian.</summary>
	[Serializable()]
	public class Guardian:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long GuardianNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNumChild;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNumGuardian;
		///<summary>Enum:GuardianRelationship .</summary>
		public GuardianRelationship Relationship;
		///<summary>True if this specifies a guardian relationship, or false if any other relationship.</summary>
		public bool IsGuardian;

		///<summary></summary>
		public Guardian Clone() {
			return (Guardian)this.MemberwiseClone();
		}

	}

	public enum GuardianRelationship {
		///<summary>0 - Added due to feature request.  Needed for EHR.</summary>
		Father,
		///<summary>1 - Added due to feature request.  Needed for EHR.</summary>
		Mother,
		///<summary>2 - Added due to feature request.</summary>
		Stepfather,
		///<summary>3 - Added due to feature request.</summary>
		Stepmother,
		///<summary>4 - Added due to feature request.</summary>
		Grandfather,
		///<summary>5 - Added due to feature request.</summary>
		Grandmother,
		///<summary>6 - Added due to feature request.</summary>
		Sitter,
		/////<summary>7 - Added for EHR.</summary>
		//Brother,
		/////<summary>8 - Added for EHR.</summary>
		//CareGiver,
		/////<summary>9 - Added for EHR.</summary>
		//FosterChild,
		/////<summary>10 - Added for EHR.  Also meets request #154.</summary>
		//Guardian,
		/////<summary>11 - Added for EHR.</summary>
		//Grandparent,
		/////<summary>12 - Added for EHR.  Also meets request #154.</summary>
		//Other,
		/////<summary>13 - Added for EHR.  Also meets request #154.</summary>
		//Parent,
		/////<summary>14 - Added for EHR.</summary>
		//Stepchild,
		/////<summary>15 - Added for EHR.</summary>
		//Self,
		/////<summary>16 - Added for EHR.</summary>
		//Sibling,
		/////<summary>17 - Added for EHR.  Also meets request #154.</summary>
		//Sister,
		/////<summary>18 - Added for EHR.</summary>
		//Spouse,
		/////<summary>19 - Added for request #1645.</summary>
		//CaseWorker,
		/////<summary>20 - Added for EHR.</summary>
		//Child,
		/////<summary>21 - Added for EHR.</summary>
		//LifePartner,
		/////<summary>22 - Added for EHR.</summary>
		//Friend,
		/////<summary>23 - Added for EHR.</summary>
		//Grandchild,
	}

}
