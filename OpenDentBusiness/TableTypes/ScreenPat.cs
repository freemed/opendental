using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary> This allows users to set up a list of students prior to actually going to the school.  It also serves to attach the exam sheet to the screening.</summary>
	[Serializable()]
	public class ScreenPat:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ScreenPatNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to screengroup.ScreenGroupNum. Every screening is attached to a group (classroom)</summary>
		public long ScreenGroupNum;
		///<summary>FK to sheet.SheetNum. Starts out 0 to indicate a potential screening. Gets linked to an exam sheet once the screening is done.</summary>
		public long SheetNum;

		///<summary></summary>
		public ScreenPat Clone() {
			return (ScreenPat)this.MemberwiseClone();
		}

	}

	
}




