using System;
using System.Collections;
using System.Data;
using System.Drawing;
namespace OpenDentBusiness{

	public class AutoNote{
		///<summary>Primary key</summary>
		public int AutoNoteNum;
		///<summary>Name of AutoNote</summary>
		public string AutoNoteName;
		///<summary>A list of all the controles to use.  Sequence of numbers separated by commas.  Numbers are foreign keys.</summary>
		public string ControlsToInc;
		/// <summary>The output of the Auto Note. This is not used by the database</summary>
		public string AutoNoteOutput;

		///<summary></summary>
		public AutoNote Copy() {
			return (AutoNote)this.MemberwiseClone();
		}
		
	}
}
