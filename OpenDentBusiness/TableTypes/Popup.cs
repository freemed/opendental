using System;
using System.Collections;

namespace OpenDentBusiness{
	///<summary>If an existing popup message gets changed, then the db row does not get updated.  Instead, the current one gets archived, and a new one gets added so that we can track historical changes.  When a new one gets created, all the archived popups will get automatically repointed to the new one.</summary>
	[Serializable]
	public class Popup:TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PopupNum;
		/// <summary>FK to patient.PatNum.  If PopupLevel is Family/SuperFamily then this must be a guarantor/super family head.</summary>
		public long PatNum;
		/// <summary>The text of the popup.</summary>
		public string Description;
		/// <summary>If true, then the popup won't ever automatically show.</summary>
		public bool IsDisabled;
		/// <summary>Enum:EnumPopupFamily 0=Patient, 1=Family, 2=Superfamily. If Family, then this Popup will apply to the entire family and PatNum will the Guarantor PatNum.  If Superfamily, then this popup will apply to the entire superfamily and PatNum will be the head of the superfamily. This column will need to be synched for all family actions where the guarantor changes.  </summary>
		public EnumPopupLevel PopupLevel;//rename to PopupLevel
		///<summary>FK to userod.UserNum.</summary>
		public long UserNum;
		///<summary>The server time that this note was entered.  Cannot be changed by user.  Does not get changed automatically when level or isDisabled gets changed.  If note itself changes, then a new popup is created along with a new DateTimeEntry.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime DateTimeEntry;
		///<summary>True for any historical popup.</summary>
		public bool IsArchived;
		///<summary>This will be zero for current popups.  Archived popups which hold historical edit info will have this field filled with the FK to the current Popup.  This will also be zero for a deleted popup because it will have no current popup to point to.</summary>
		public long PopupNumArchive;
		//We will consider later adding Guarantor and SuperFamily FK's to speed up queries.  The disadvantage is that popups would then have to be synched every time guarantors or sf heads changed.

		public Popup Copy() {
			return (Popup)this.MemberwiseClone();
		}

	}


	public enum EnumPopupLevel {
		/// <summary>0=Patient</summary>
		Patient,
		/// <summary>1=Family</summary>
		Family,
		/// <summary>3=SuperFamily</summary>
		SuperFamily
	}

}









