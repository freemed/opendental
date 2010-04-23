using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable()]
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

			
	}

	

}









