using System;
using System.Collections;

namespace OpenDentBusiness{
	///<summary>Only one popup per patient is currently supported.</summary>
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
		//We will consider later adding Guarantor and SuperFamily FK's to speed up queries.  The disadvantage is that popups would then have to be synched every time guarantors or sf heads changed.
			
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









