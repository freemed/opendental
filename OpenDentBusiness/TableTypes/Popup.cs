using System;
using System.Collections;

namespace OpenDentBusiness{
	///<summary>Only one popup per patient is currently supported.</summary>
	[Serializable]
	public class Popup:TableBase {
		/// <summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PopupNum;
		/// <summary>FK to patient.PatNum.</summary>
		public long PatNum;
		/// <summary>The text of the popup.</summary>
		public string Description;
		/// <summary>If true, then the popup won't ever automatically show.</summary>
		public bool IsDisabled;
		/// <summary>If true, then this Popup will apply to the entire family and PatNum for this popup will the Guarantor PatNum.  This column will need to be synched for all family actions where the guarantor changes.</summary>
		public bool IsFamily;

			
	}

	

}









