using System;
using System.Collections.Generic;
using System.Text;
namespace OpenDentBusiness {

	///<summary>In the program, this is now called an autonote prompt.</summary>
	public class AutoNoteControl {
		///<summary>Primary key</summary>
		public int AutoNoteControlNum;
		///<summary>The description of the prompt as it will be referred to from other windows.</summary>
		public string Descript;
		///<summary>'Text' or 'OneResponse'.  More types to be added later.</summary>
		public string ControlType;
		///<summary>The prompt text.</summary>
		public string ControlLabel;
		///<summary>For TextBox, this is the default text.  For a ComboBox, this is the list of possible responses, one per line.</summary>
		public string ControlOptions;
		

		///<summary></summary>
		public AutoNote Copy() {
			return (AutoNote)this.MemberwiseClone();
		}
	}
}
