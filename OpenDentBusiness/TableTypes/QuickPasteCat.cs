using System;

namespace OpenDentBusiness{
	
	///<summary>Quick paste categories are used by the quick paste notes feature.</summary>
	public class QuickPasteCat{
		///<summary>Primary key.</summary>
		public int QuickPasteCatNum;
		///<summary>.</summary>
		public string Description;
		///<summary>The order of this category within the list. 0-based.</summary>
		public int ItemOrder;
		///<summary>Enum:QuickPasteType  Each Category can be set to be the default category for multiple types of notes. Stored as integers separated by commas.</summary>
		public string DefaultForTypes;


		

		


	}

	


}









