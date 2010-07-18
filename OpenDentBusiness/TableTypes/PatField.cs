using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>These are custom fields added and managed by the user.</summary>
	[Serializable]
	public class PatField:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatFieldNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to patfielddef.FieldName.  The full name is shown here for ease of use when running queries.  But the user is only allowed to change fieldNames in the patFieldDef setup window.</summary>
		public string FieldName;
		///<summary>Any text that the user types in.</summary>
		public string FieldValue;

		///<summary></summary>
		public PatField Copy() {
			return (PatField)this.MemberwiseClone();
		}

	}

		



		
	

	

	


}










