using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>This defines the light buttons on the left of the main screen.</summary>
	public class SigButDef{
		///<summary>Primary key.</summary>
		public int SigButDefNum;
		///<summary>The text on the button</summary>
		public string ButtonText;
		///<summary>0-based index defines the order of the buttons.</summary>
		public int ButtonIndex;
		///<summary>0=none, or 1-9. The cell in the 3x3 tic-tac-toe main program icon that is to be synched with this button.  It will light up or clear whenever this button lights or clears.</summary>
		public int SynchIcon;
		///<summary>Blank for the default buttons.  Or contains the computer name for the buttons that override the defaults.</summary>
		public string ComputerName;
		///<summary>Not a database field.  The sounds and lights attached to the button.</summary>
		public SigButDefElement[] ElementList;

		///<summary></summary>
		public SigButDef Copy() {
			SigButDef s=new SigButDef();
			s.SigButDefNum=SigButDefNum;
			s.ButtonText=ButtonText;
			s.ButtonIndex=ButtonIndex;
			s.SynchIcon=SynchIcon;
			s.ComputerName=ComputerName;
			s.ElementList=new SigButDefElement[ElementList.Length];
			ElementList.CopyTo(s.ElementList,0);
			return s;
		}

		
	}

		



		
	

	

	


}










