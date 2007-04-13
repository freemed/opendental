using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Keeps track of one file attached to an email.  Multiple files can be attached to an email using this method.</summary>
	public class EmailAttach {
		///<summary>Primary key.</summary>
		public int EmailAttachNum;
		///<summary>FK to emailmessage.EmailMessageNum</summary>
		public int EmailMessageNum;
		///<summary>The name of the file that shows on the email.  For example: tooth2.jpg.</summary>
		public string DisplayedFileName;
		///<summary>The actual file is stored in the A-Z folder in EmailAttachments.  This field stores the name of the file.  The files are named automatically based on Date/time along with a random number.  This ensures that they will be sequential as well as unique.</summary>
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
