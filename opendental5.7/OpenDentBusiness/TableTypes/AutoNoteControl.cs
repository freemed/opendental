using System;
using System.Collections.Generic;
using System.Text;
namespace OpenDentBusiness {

	public class AutoNoteControl {
		///<summary>Primary key</summary>
		public int AutoNoteControlNum;
		///<summary>'Descript' is the name of the custom control</summary>
		public string Descript;
		///<summary>The type of control such as 'TextBox' or 'ComboBox'</summary>
		public string ControlType;
		///<summary>Is the text for the label the user wants</summary>
		public string ControlLabel;
		///<summary>The preface text</summary>
		public string PrefaceText;
		///<summary>If the control is a multi line text box then store the text here</summary>
		public string MultiLineText;
		///<summary>If the control is a combo box then the user puts the the selection options for the combo box here</summary>
		public string ControlOptions;
		

		///<summary></summary>
		public AutoNote Copy() {
			return (AutoNote)this.MemberwiseClone();
		}
	}
}
