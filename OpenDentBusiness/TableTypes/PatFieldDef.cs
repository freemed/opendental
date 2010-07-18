using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>These are the definitions for the custom patient fields added and managed by the user.</summary>
	[Serializable]
	public class PatFieldDef:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatFieldDefNum;
		///<summary>The name of the field that the user will be allowed to fill in the patient info window.</summary>
		public string FieldName;

		///<summary></summary>
		public PatFieldDef Copy() {
			return (PatFieldDef)this.MemberwiseClone();
		}

		
	}

		


}










