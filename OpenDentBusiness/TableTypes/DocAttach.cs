using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	///<summary>Links documents (images) to patients.  This will allow one document to be shared between multiple patients in a future version.</summary>
	public class DocAttach {
		///<summary>Primary key.</summary>
		public int DocAttachNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to document.DocNum.</summary>
		public int DocNum;

	}

}
