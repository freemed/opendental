using System;

namespace OpenDentBusiness{
	
	///<summary>Some program links (bridges), have properties that need to be set.  The property names are always hard coded.  User can change the value.  The property is usually retrieved based on its name.</summary>
	public class ProgramProperty{
		///<summary>Primary key.</summary>
		public int ProgramPropertyNum;
		///<summary>FK to program.ProgramNum</summary>
		public int ProgramNum;
		///<summary>The description or prompt for this property.</summary>
		public string PropertyDesc;
		///<summary>The value.</summary>
		public string PropertyValue;
	}

	



	

	


}










