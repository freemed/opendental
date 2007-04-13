using System;

namespace OpenDentBusiness{
	
	///<summary></summary>
	public class QuickPasteNote{
		///<summary>Primary key.</summary>
		public int QuickPasteNoteNum;
		///<summary>FK to quickpastecat.QuickPasteCatNum.  Keeps track of which category this note is in.</summary>
		public int QuickPasteCatNum;
		///<summary>The order of this note within it's category. 0-based.</summary>
		public int ItemOrder;
		///<summary>The actual note. Can be multiple lines and possibly very long.</summary>
		public string Note;
		///<summary>The abbreviation which will automatically substitute when preceded by a ?.</summary>
		public string Abbreviation;



	}

	


}









