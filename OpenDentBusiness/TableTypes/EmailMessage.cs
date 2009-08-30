using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>An outgoing email message is stored here.</summary>
	public class EmailMessage {
		///<summary>Primary key.</summary>
		public long EmailMessageNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>Single valid email address. Bcc field might be added later, although it won't be very useful.  We will never allow visible cc for privacy reasons.</summary>
		public string ToAddress;
		///<summary>Valid email address.</summary>
		public string FromAddress;
		///<summary>Subject line.</summary>
		public string Subject;
		///<summary>Body of the email</summary>
		public string BodyText;
		///<summary>Date and time the message was sent. Automated field.</summary>
		public DateTime MsgDateTime;
		///<summary>0=neither, 1=sent, 2=received.</summary>
		public CommSentOrReceived SentOrReceived;
		///<summary>Not a database column.</summary>
		public List<EmailAttach> Attachments;

		///<summary>Constructor</summary>
		public EmailMessage(){
			Attachments=new List<EmailAttach>();
		}

		public EmailMessage Copy() {
			EmailMessage e=(EmailMessage)this.MemberwiseClone();
			e.Attachments=new List<EmailAttach>();
			for(int i=0;i<Attachments.Count;i++) {
				e.Attachments.Add(Attachments[i].Copy());
			}
			return e;
		}
	}



}
