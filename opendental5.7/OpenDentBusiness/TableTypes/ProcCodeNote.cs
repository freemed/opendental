using System;
using System.Collections;
using System.Drawing;
using System.Xml.Serialization;

namespace OpenDentBusiness{
	
	///<summary>Stores the default note and time increments for one procedure code for one provider.  That way, an unlimited number of providers can each have different notes and times.  These notes and times override the defaults which are part of the procedurecode table.  So, for single provider offices, there will be no change to the current interface.</summary>
	public class ProcCodeNote{
		///<summary>Primary Key.</summary>
		public int ProcCodeNoteNum;
		///<summary>FK to procedurecode.CodeNum.</summary>
		public int CodeNum;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>The note.</summary>
		public string Note;
		///<summary>X's and /'s describe Dr's time and assistant's time in the same increments as the user has set.</summary>
		public string ProcTime;

		///<summary>Returns a copy of this ProcCodeNote</summary>
		public ProcCodeNote Copy(){
			return (ProcCodeNote)MemberwiseClone();
		}


	}

	
	
	


}










