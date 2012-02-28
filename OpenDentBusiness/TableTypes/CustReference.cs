using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {

	///<summary>One to one relation with the patient table representing each customer as a reference.</summary>
	[Serializable]
	public class CustReference:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long CustReferenceNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Most recent date the reference was used, loosely kept updated.</summary>
		public DateTime DateMostRecent;
		///<summary>Notes specific to this customer as a reference.</summary>
		public string Note;
		///<summary>Set to true if this customer was a bad reference.</summary>
		public bool IsBadRef;

		///<summary></summary>
		public CustReference Clone() {
			return (CustReference)this.MemberwiseClone();
		}

	}

}