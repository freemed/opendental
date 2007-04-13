using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>These are custom fields added and managed by the user.</summary>
	public class PatField{
		///<summary>Primary key.</summary>
		public int PatFieldNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum;
		///<summary>FK to patfielddef.FieldName.  The full name is shown here for ease of use when running queries.  But the user is only allowed to change fieldNames in the patFieldDef setup window.</summary>
		public string FieldName;
		///<summary>Any text that the user types in.</summary>
		public string FieldValue;

		///<summary></summary>
		public PatField Copy() {
			PatField p=new PatField();
			p.PatFieldNum=PatFieldNum;
			p.PatNum=PatNum;
			p.FieldName=FieldName;
			p.FieldValue=FieldValue;
			return p;
		}

	}

		



		
	

	

	


}










