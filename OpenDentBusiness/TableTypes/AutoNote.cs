using System;
using System.Collections;
using System.Data;
using System.Drawing;
namespace OpenDentBusiness{
	
	[Serializable()]
	public class AutoNote:TableBase{
		///<summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long AutoNoteNum;
		///<summary>Name of AutoNote</summary>
		public string AutoNoteName;
		///<summary>Was 'ControlsToInc' in previous versions.</summary>
		public string MainText;
		// <summary></summary>
		//public string AutoNoteOutput;

		///<summary></summary>
		public AutoNote Copy() {
			return (AutoNote)this.MemberwiseClone();
		}
		
	}
}
