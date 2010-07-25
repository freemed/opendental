using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>These are the definitions for the custom patient fields added and managed by the user.</summary>
	[Serializable]
	public class ApptFieldDef:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ApptFieldDefNum;
		///<summary>The name of the field that the user will be allowed to fill in the appt edit window.  Duplicates are prevented.</summary>
		public string FieldName;
	
		///<summary></summary>
		public Account Clone() {
			return (Account)this.MemberwiseClone();
		}

	}

	
}




