/*using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Keeps track of one image file attached to a claim.  Multiple files can be attached to a claim using this method.</summary>
	public class ClaimAttach {
		///<summary>Primary key.</summary>
		public int ClaimAttachNum;
		///<summary>FK to claim.ClaimNum</summary>
		public int ClaimNum;
		///<summary>The actual file is stored in the patient's A-Z folder.  This field stores the name of the file rather than using a FK to the document object, because we need to refer to the unaltered image, as it was actually sent.  So it will become common to include an 'export' function of sorts before attaching.</summary>
		public string FileName;

		public ClaimAttach Copy(){
			return (ClaimAttach)this.MemberwiseClone();
		}
	}




}
*/