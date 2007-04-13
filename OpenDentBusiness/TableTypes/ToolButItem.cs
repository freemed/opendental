using System;

namespace OpenDentBusiness{

	///<summary>Each row represents one toolbar button to be placed on a toolbar and linked to a program.</summary>
	public class ToolButItem{
		///<summary>Primary key.</summary>
		public int ToolButItemNum;
		///<summary>FK to program.ProgramNum.</summary>
		public int ProgramNum;
		///<summary>Enum:ToolBarsAvail The toolbar to show the button on.</summary>
		public ToolBarsAvail ToolBar;
		///<summary>The text to show on the toolbar button.</summary>
		public string ButtonText;
		//later include ComputerName.  If blank, then show on all computers.
		//also later, include an image.
	}

	

}













