using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Keeps track of one file attached to a claim.  Multiple files can be attached to a claim using this method.</summary>
	public class ClaimAttach {
		///<summary>Primary key.</summary>
		public int ClaimAttachNum;
		///<summary>FK to claim.ClaimNum</summary>
		public int ClaimNum;
		///<summary>The name of the file that shows on the claim.  For example: tooth2.jpg.</summary>
		public string DisplayedFileName;
		///<summary>The actual file is stored in the A-Z folder in ClaimAttachments.  This field stores the name of the file.  The files are named automatically based on PatientName and Date/time.</summary>
		public string ActualFileName;

		/*public EmailMessage Copy() {
			EmailMessage e=new EmailMessage();
			e.EmailMessageNum=EmailMessageNum;
			e.PatNum=PatNum;
			e.ToAddress=ToAddress;
			e.FromAddress=FromAddress;
			e.Subject=Subject;
			e.BodyText=BodyText;
			e.MsgDateTime=MsgDateTime;
			return e;
		}*/
	}




}
