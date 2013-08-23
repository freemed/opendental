using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Stores all the connection info for one email address.  Linked to clinic by clinic.EmailAddressNum.  Sends email based on patient's clinic.</summary>
	[Serializable()]
	public class EmailAddress:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EmailAddressNum;
		///<summary>For example smtp.gmail.com</summary>
		public string SMTPserver;
		///<summary>.</summary>
		public string EmailUsername;
		///<summary>.</summary>
		public string EmailPassword;
		///<summary>Usually 587, sometimes 25 or 465.</summary>
		public int ServerPort;
		///<summary>.</summary>
		public bool UseSSL;
		///<summary>The email address of the sender as it should appear to the recipient.</summary>
		public string SenderAddress;
		///<summary>For example pop.gmail.com</summary>
		public string Pop3ServerIncoming;
		///<summary>Usually 110, sometimes 995.</summary>
		public int ServerPortIncoming;

		///<summary></summary>
		public EmailAddress Clone() {
			return (EmailAddress)this.MemberwiseClone();
		}

	}


}