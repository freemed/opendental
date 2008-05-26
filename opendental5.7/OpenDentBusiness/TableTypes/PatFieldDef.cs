using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>These are the definitions for the custom patient fields added and managed by the user.</summary>
	public class PatFieldDef{
		///<summary>Primary key.</summary>
		public int PatFieldDefNum;
		///<summary>The name of the field that the user will be allowed to fill in the patient info window.</summary>
		public string FieldName;

		///<summary></summary>
		public PatFieldDef Copy() {
			PatFieldDef p=new PatFieldDef();
			p.PatFieldDefNum=PatFieldDefNum;
			p.FieldName=FieldName;
			return p;
		}

		
	}

		



		
	

	

	


}










