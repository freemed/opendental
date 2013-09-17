using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using OpenDentBusiness;
using CodeBase;

namespace OpenDentBusiness {
	public class EhrEmail {

		///<summary>Used to cache DirectAgent objects, because creating a new DirectAgent object takes up to 10 seconds. If we did not cache, then inbox load would be slow and so would Direct message sending.</summary>
		private static Hashtable HashDirectAgents=new Hashtable();

		#region Sending

		///<summary>Encrypts the message, verifies trust, locates the public encryption key for the To address (if already stored locally), etc.  patNum can be zero.  listAttachments can be null.</summary>
		public static void SendEmailDirect(string strTo,EmailAddress emailAddressFrom,string strBodyText,long patNum,List<Health.Direct.Common.Mime.MimeEntity> listAttachments) {
			Health.Direct.Agent.DirectAgent directAgent=GetDirectAgentForEmailAddress(emailAddressFrom.EmailUsername);//Cannot be emailAddressFrom.SenderAddress, or else will not find the right encryption certificate.
			Health.Direct.Common.Mail.Message message=new Health.Direct.Common.Mail.Message(strTo,emailAddressFrom.EmailUsername,strBodyText);//Don't think emailAddressFrom.SenderAddress would work, because of how strict encryption is.
			if(listAttachments!=null && listAttachments.Count>0) {
				message.SetParts(listAttachments,"multipart/mixed; boundary="+CodeBase.MiscUtils.CreateRandomAlphaNumericString(32)+";");
			}
			Health.Direct.Agent.MessageEnvelope messageEnvelope=new Health.Direct.Agent.MessageEnvelope(message);
			//Encrypt, verify trust, locate the public encryption key for the To address (if already stored locally), etc. Required by Direct protocol.
			Health.Direct.Agent.OutgoingMessage outMsgDirect=directAgent.ProcessOutgoing(messageEnvelope);
			SendEmailDirect(outMsgDirect,emailAddressFrom,patNum,EmailSentOrReceived.SentDirect);
		}

		///<summary>outMsgDirect must already be encrypted. emailSentOrReceived must be either SentDirect or AckDirect.</summary>
		private static void SendEmailDirect(Health.Direct.Agent.OutgoingMessage outMsgDirect,EmailAddress emailAddressFrom,long patNum,EmailSentOrReceived emailSentOrReceived) {
			MailMessage msgMdnDirect=new MailMessage(outMsgDirect.Message.FromValue,outMsgDirect.Message.ToValue,outMsgDirect.Message.SubjectValue,"");
			//Convert the email into a common .NET object, so we can send it using standard .NET libraries.
			for(int i=0;i<outMsgDirect.Message.Headers.Count;i++) {
				msgMdnDirect.Headers.Add(outMsgDirect.Message.Headers[i].Name,outMsgDirect.Message.Headers[i].ValueRaw);
			}
			byte[] arrayEncryptedBody=Encoding.UTF8.GetBytes(outMsgDirect.Message.Body.Text);
			MemoryStream ms=new MemoryStream(arrayEncryptedBody);
			ms.Position=0;
			//The memory stream for the alternate view must be mime (not an entire email), based on AlternateView use example http://msdn.microsoft.com/en-us/library/system.net.mail.mailmessage.alternateviews.aspx
			AlternateView alternateView=new AlternateView(ms,outMsgDirect.Message.ContentType);//Causes the receiver to recognize this email as an encrypted email.
			alternateView.TransferEncoding=TransferEncoding.SevenBit;
			msgMdnDirect.AlternateViews.Add(alternateView);
			SendEmailUnsecure(msgMdnDirect,emailAddressFrom);//Not really unsecure in this spot, because the message is already encrypted.
			ms.Dispose();
			EmailMessage emailMdnDirect=new EmailMessage();
			emailMdnDirect.BodyText=outMsgDirect.SerializeMessage();//Converts the entire Direct outgoing message to a raw email message text for email archive.
			emailMdnDirect.FromAddress=outMsgDirect.Message.FromValue;
			emailMdnDirect.MsgDateTime=DateTime.Now;
			emailMdnDirect.PatNum=patNum;
			emailMdnDirect.SentOrReceived=emailSentOrReceived;
			emailMdnDirect.Subject=outMsgDirect.Message.SubjectValue;
			emailMdnDirect.ToAddress=outMsgDirect.Message.ToValue;
			EmailMessages.Insert(emailMdnDirect);//Will not show in UI anywhere yet, just for history in case something goes wrong.			
		}

		/// <summary>Used for sending encrypted Message Disposition Notification (MDN) ack messages for Direct.
		/// An ack must be sent when a message is received, and other acks must be sent when other events occur (for example, when the user reads a decrypted message we must send an ack with notification type of Displayed).</summary>
		public static void SendAckDirect(Health.Direct.Agent.IncomingMessage inMsg,EmailAddress emailAddressFrom,long patNum,Health.Direct.Common.Mail.Notifications.MDNStandard.NotificationType notificationType) {
			//The CreateAcks() function handles the case where the incoming message is an MDN, in which case we do not reply with anything.
			//The CreateAcks() function also takes care of figuring out where to send the MDN, because the rules are complicated.
			//According to http://wiki.directproject.org/Applicability+Statement+for+Secure+Health+Transport+Working+Version#x3.0%20Message%20Disposition%20Notification,
			//The MDN must be sent to the first available of: Disposition-Notification-To header, MAIL FROM SMTP command, Sender header, From header.
			IEnumerable<Health.Direct.Common.Mail.Notifications.NotificationMessage> notificationMsgs=inMsg.CreateAcks("OpenDental","",notificationType);
			if(notificationMsgs==null) {
				return;
			}
			foreach(Health.Direct.Common.Mail.Notifications.NotificationMessage notificationMsg in notificationMsgs) {
				try {
					//According to RFC3798, section 3 - Format of a Message Disposition Notification http://tools.ietf.org/html/rfc3798#page-3
					//A message disposition notification is a MIME message with a top-level
					//content-type of multipart/report (defined in [RFC-REPORT]).  When
					//multipart/report content is used to transmit an MDN:
					//(a)  The report-type parameter of the multipart/report content is "disposition-notification".
					//(b)  The first component of the multipart/report contains a human-readable explanation of the MDN, as described in [RFC-REPORT].
					//(c)  The second component of the multipart/report is of content-type message/disposition-notification, described in section 3.1 of this document.
					//(d)  If the original message or a portion of the message is to be returned to the sender, it appears as the third component of the multipart/report.
					//     The decision of whether or not to return the message or part of the message is up to the MUA generating the MDN.  However, in the case of 
					//     encrypted messages requesting MDNs, encrypted message text MUST be returned, if it is returned at all, only in its original encrypted form.
					Health.Direct.Agent.OutgoingMessage outMsgDirect=new Health.Direct.Agent.OutgoingMessage(notificationMsg);
					Health.Direct.Agent.DirectAgent directAgent=GetDirectAgentForEmailAddress(emailAddressFrom.EmailUsername);//Cannot be emailAddressFrom.SenderAddress, or else will not find the right encryption certificate.
					outMsgDirect=directAgent.ProcessOutgoing(outMsgDirect);
					//Used EmailSentOrReceived.AckDirect, not EmailSentOrReceived.SentDirect, because we do not want these to be counted in our reports as messages sent using Direct.
					SendEmailDirect(outMsgDirect,emailAddressFrom,patNum,EmailSentOrReceived.AckDirect);
				}
				catch {
					//Nothing to do. Just an MDN. The sender can resend the email to us if they believe that we did not receive the message (due to lack of MDN response).
				}
			}
		}

		///<summary>This is the root email sending function. Sends an already prepared System.Net.Mail.MailMessage. Sender port 465 is treated as implicit email, otherwise the email is treated as explicit.</summary>
		public static void SendEmailUnsecure(System.Net.Mail.MailMessage mailMessage,EmailAddress emailAddressFrom) {
			if(emailAddressFrom.ServerPort==465) {//implicit
				//uses System.Web.Mail, which is marked as deprecated, but still supports implicit
				System.Web.Mail.MailMessage mailMessageWeb=ConvertMailNetToMailWeb(mailMessage);
				mailMessageWeb.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver",emailAddressFrom.SMTPserver);
				mailMessageWeb.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport","465");
				mailMessageWeb.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing","2");//sendusing: cdoSendUsingPort, value 2, for sending the message using the network.
				mailMessageWeb.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate","1");//0=anonymous,1=clear text auth,2=context
				mailMessageWeb.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername",emailAddressFrom.EmailUsername);
				mailMessageWeb.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword",emailAddressFrom.EmailPassword);
				//if(PrefC.GetBool(PrefName.EmailUseSSL)) {
				mailMessageWeb.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl","true");//false was also tested and does not work			
				System.Web.Mail.SmtpMail.SmtpServer=emailAddressFrom.SMTPserver+":465";//"smtp.gmail.com:465";
				System.Web.Mail.SmtpMail.Send(mailMessageWeb);
			}
			else {//explicit default port 587 
				SmtpClient client=new SmtpClient(emailAddressFrom.SMTPserver,emailAddressFrom.ServerPort);
				//The default credentials are not used by default, according to: 
				//http://msdn2.microsoft.com/en-us/library/system.net.mail.smtpclient.usedefaultcredentials.aspx
				client.Credentials=new NetworkCredential(emailAddressFrom.EmailUsername,emailAddressFrom.EmailPassword);
				client.DeliveryMethod=SmtpDeliveryMethod.Network;
				client.EnableSsl=emailAddressFrom.UseSSL;
				client.Timeout=180000;//3 minutes
				client.Send(mailMessage);
			}
		}

		#endregion Sending

		///<summary>Fetches up to fetchCount number of messages from a POP3 server.  Set fetchCount=0 for all messages.  Typically, fetchCount is 0 or 1.
		///Example host name, pop3.live.com. Port is Normally 110 for plain POP3, 995 for SSL POP3.  Does not currently fetch attachments.</summary>
		public static List<EmailMessage> ReceiveFromInbox(int receiveCount,EmailAddress emailAddressInbox) {
			List<EmailMessage> retVal=new List<EmailMessage>();
			//This code is modified from the example at: http://hpop.sourceforge.net/exampleFetchAllMessages.php
			using(OpenPop.Pop3.Pop3Client client=new OpenPop.Pop3.Pop3Client()) {//The client disconnects from the server when being disposed.
				client.Connect(emailAddressInbox.Pop3ServerIncoming,emailAddressInbox.ServerPortIncoming,emailAddressInbox.UseSSL);
				client.Authenticate(emailAddressInbox.EmailUsername,emailAddressInbox.EmailPassword,OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);
				int messageCount=client.GetMessageCount();//Get the number of messages in the inbox.
				List<OpenPop.Mime.Message> openPopMsgsAll=new List<OpenPop.Mime.Message>(messageCount);
				int msgDownloadedCount=0;
				for(int i=messageCount;i>0;i--) {//Message numbers are 1-based. Most servers give the newest message the highest number.
					try {
						openPopMsgsAll.Add(client.GetMessage(i));
						OpenPop.Mime.Message openPopMsg=openPopMsgsAll[openPopMsgsAll.Count-1];
						EmailMessage emailMessage=new EmailMessage();
						emailMessage.IsNew=true;
						emailMessage.SentOrReceived=EmailSentOrReceived.Received;
						emailMessage.MsgDateTime=DateTime.Now;//Could pull from email header, but it is better to record the time that OD saved into db, since no user would view the email before it was in db.
						emailMessage.PatNum=0;//Is automatically set for some Direct messages when decypted (if a patient match can be found).
						emailMessage.ToAddress=emailAddressInbox.EmailUsername;
						emailMessage.Subject=openPopMsg.Headers.Subject;
						emailMessage.FromAddress=openPopMsg.Headers.From.Address;
						System.Net.Mime.ContentType contentType=openPopMsg.MessagePart.ContentType;
						if(contentType.MediaType.ToLower().Contains("application/pkcs7-mime")) {//The email MIME/body is encrypted (known as S/MIME). Treated as a Direct message.
							emailMessage.SentOrReceived=EmailSentOrReceived.ReceivedEncrypted;
							//The entire contents of the email are saved in the emailMessage.BodyText field, so that if decryption fails, the email will still be saved to the db for decryption later if possible.
							emailMessage.BodyText=openPopMsg.MessagePart.BodyEncoding.GetString(openPopMsg.RawMessage);
							try {
								DecryptDirect(emailMessage,emailAddressInbox);//If decryption succeeds, the BodyText will be set to the body text instead of the entire raw email contents and the email will be saved to db.
							}
							catch {
								//The encrypted message is saved to the db, so that the user can try to decrypt later in FormEmailMessageEdit.
								EmailMessages.Insert(emailMessage);
							}
						}
						else {//Unencrypted email.
							if(openPopMsg.MessagePart.Body!=null) {//Is null when a Direct Ack message (MDN) comes in.
								emailMessage.BodyText=openPopMsg.MessagePart.GetBodyAsText();
							}
							EmailMessages.Insert(emailMessage);
						}
						retVal.Add(emailMessage);
						client.DeleteMessage(i);//Only delete from server after successfully downloaded and stored into db.
						msgDownloadedCount++;
					}
					catch {
						//If one particular email fails to download, then skip it for now and move on to the next email.
					}
					if(receiveCount>0 && msgDownloadedCount>=receiveCount) {
						break;
					}
				}
				return retVal;
			}
		}

		#region Helpers

		private static Health.Direct.Agent.DirectAgent GetDirectAgentForEmailAddress(string strEmailAddress) {
			string domain=strEmailAddress.Substring(strEmailAddress.IndexOf("@")+1);//Used to locate the certificate for the incoming email. For example, if ToAddress is ehr@opendental.com, then this will be opendental.com
			Health.Direct.Agent.DirectAgent directAgent=(Health.Direct.Agent.DirectAgent)HashDirectAgents[domain];
			if(directAgent==null) {				
				directAgent=new Health.Direct.Agent.DirectAgent(domain);
				directAgent.EncryptMessages=true;
				HashDirectAgents[domain]=directAgent;
			}
			return directAgent;
		}

		///<summary>Only for email messages with SentOrReceived set to EncryptedDirect.
		///If decryption fails, then throws an exception and does not change email.
		///If decryption succeeds, then emailMessage.SentOrReceived is set to ReceivedDirect and the BodyText of the email is changed from the entire encrypted email contents to the decrypted body text.
		///Automatically sends a Direct "processed" acknowledgement (MDN) to the sender if the message is decrypted.</summary>
		public static void DecryptDirect(EmailMessage emailMessage,EmailAddress emailAddressFrom) {
			Health.Direct.Agent.DirectAgent directAgent=GetDirectAgentForEmailAddress(emailMessage.ToAddress);
			Health.Direct.Agent.IncomingMessage inMsg=null;
			try {
				inMsg=new Health.Direct.Agent.IncomingMessage(emailMessage.BodyText);//This is actually the entire contents of the email message for this specific case. Normally it would just be the body text.
			}
			catch(Exception ex) {
				throw new ApplicationException("Failed to parse Direct email message.\r\n"+ex.Message);
			}
			try {				
				inMsg=directAgent.ProcessIncoming(inMsg);
			}
			catch(Exception ex) {
				throw new ApplicationException("Email message decryption failed.\r\n"+ex.Message);
			}
			emailMessage.SentOrReceived=EmailSentOrReceived.ReceivedDirect;
			StringBuilder sbBodyText=new StringBuilder();
			Health.Direct.Common.Mime.MimeEntity mimeEntity=inMsg.Message.ExtractMimeEntity();
			List<Health.Direct.Common.Mime.MimeEntity> listAttachments=new List<Health.Direct.Common.Mime.MimeEntity>();
			foreach(Health.Direct.Common.Mime.MimeEntity mimePart in mimeEntity.GetParts()) {
				if(mimePart.ContentDisposition!=null && mimePart.ContentDisposition.ToLower().Contains("attachment")) {
					listAttachments.Add(mimePart);//File is created below, after the email message is inserted, because we need the primary key.
					continue;
				}
				if(sbBodyText.Length>0) {
					sbBodyText.Append("\r\n\r\n");
				}
				sbBodyText.Append(mimePart.Body.Text);
			}
			emailMessage.BodyText=sbBodyText.ToString();
			//emailMessage.PatNum=0;//TODO: Set for some Direct messages.
			//We could pull the email date and time from the server instead. The format is more difficult than normal, and might be different depending on who sent the email.
			//emailMessage.MsgDateTime=DateTime.ParseExact(inMsg.Message.DateValue,"ddd, d MMM yyyy HH:mm:ss",CultureInfo.InvariantCulture);
			if(emailMessage.IsNew && emailMessage.EmailMessageNum==0) {
				EmailMessages.Insert(emailMessage);
			}
			else {
				EmailMessages.Update(emailMessage);
			}
			for(int i=0;i<listAttachments.Count;i++) {
				string strAttachText=listAttachments[i].Body.Text;
				try {
					if(listAttachments[i].ContentTransferEncoding.ToLower().Contains("base64")) {
						strAttachText=Encoding.UTF8.GetString(Convert.FromBase64String(listAttachments[i].Body.Text));
					}
				}
				catch {
				}
				CreateAttachFromText(strAttachText,listAttachments[i].ParsedContentType.Name,emailMessage.EmailMessageNum);
			}
			//Send a Message Disposition Notification (MDN) message to the sender, as required by the Direct messaging specifications.
			//The MDN will be attached to the same patient as the incoming message.
			SendAckDirect(inMsg,emailAddressFrom,emailMessage.PatNum,Health.Direct.Common.Mail.Notifications.MDNStandard.NotificationType.Processed);
		}

		public static string GetEmailAttachPath() {
			string attachPath;
			if(PrefC.AtoZfolderUsed) {
				attachPath=ODFileUtils.CombinePaths(ImageStore.GetPreferredAtoZpath(),"EmailAttachments");
				if(!Directory.Exists(attachPath)) {
					Directory.CreateDirectory(attachPath);
				}
			}
			else {
				//For users who have the A to Z folders disabled, there is no defined image path, so we
				//have to use a temp path.  This means that the attachments might be available immediately afterward,
				//but probably not later.
				attachPath=Path.GetTempPath();
			}
			return attachPath;
		}

		///<summary>The strAttachFileName variable must include the extension.</summary>
		public static EmailAttach CreateAttachFromText(string strText,string strAttachFileName,long emailMessageNum) {
			string strAttachPath=GetEmailAttachPath();
			string strAttachFileNameAdjusted=strAttachFileName;
			if(String.IsNullOrEmpty(strAttachFileName)) {
				strAttachFileNameAdjusted=MiscUtils.CreateRandomAlphaNumericString(8)+".txt";//just in case
			}
			string strAttachFile=ODFileUtils.CombinePaths(strAttachPath,DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+"_"+strAttachFileNameAdjusted);
			while(File.Exists(strAttachFile)) {
				strAttachFile=ODFileUtils.CombinePaths(strAttachPath,DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+"_"+strAttachFileNameAdjusted);
			}
			File.WriteAllText(strAttachFile,strText);
			EmailAttach attach=new EmailAttach();
			attach.ActualFileName=Path.GetFileName(strAttachFile);
			attach.DisplayedFileName=Path.GetFileName(strAttachFile);
			attach.EmailMessageNum=emailMessageNum;
			EmailAttaches.Insert(attach);
			return attach;
		}

		///<summary>Converts the strBodyContents into base64, then creates a new mime attachment using the base64 data.</summary>
		public static Health.Direct.Common.Mime.MimeEntity CreateTextAttachment(string strBodyContents,string strFileName) {
			Health.Direct.Common.Mime.MimeEntity mimeEntity=new Health.Direct.Common.Mime.MimeEntity(Convert.ToBase64String(Encoding.UTF8.GetBytes(strBodyContents)));
			mimeEntity.ContentDisposition="attachment;";
			mimeEntity.ContentTransferEncoding="base64;";
			mimeEntity.ContentType="text/plain; name="+strFileName+";";
			return mimeEntity;
		}

		private static System.Web.Mail.MailMessage ConvertMailNetToMailWeb(System.Net.Mail.MailMessage mailMessage) {
			System.Web.Mail.MailMessage retVal=new System.Web.Mail.MailMessage();
			//retVal.From=emailMessage.FromAddress;
			//retVal.To=emailMessage.ToAddress;
			//retVal.Subject=emailMessage.Subject;
			//retVal.Body=emailMessage.BodyText;
			////retVal.Cc=;
			////retVal.Bcc=;
			////retVal.UrlContentBase=;
			////retVal.UrlContentLocation=;
			retVal.BodyEncoding=System.Text.Encoding.UTF8;
			retVal.BodyFormat=System.Web.Mail.MailFormat.Text;//or .Html
			//string attachPath=GetAttachPath();
			//System.Web.Mail.MailAttachment attach;
			////foreach (string sSubstr in sAttach.Split(delim)){
			//for(int i=0;i<emailMessage.Attachments.Count;i++) {
			//	attach=new System.Web.Mail.MailAttachment(ODFileUtils.CombinePaths(attachPath,emailMessage.Attachments[i].ActualFileName));
			//	//no way to set displayed filename
			//	retVal.Attachments.Add(attach);
			//}
			//TODO
			return retVal;
		}

		#endregion Helpers

		#region Test

		///<summary>This method is only for ehr testing purposes, and it always uses the hidden pref EHREmailToAddress to send to.  For privacy reasons, this cannot be used with production patient info.  AttachName should include extension.</summary>
		public static void SendTest(string subjectAndBody,string attachName,string attachContents) {
			SendTest(subjectAndBody,attachName,attachContents,"","");
		}

		///<summary>This method is only for ehr testing purposes, and it always uses the hidden pref EHREmailToAddress to send to.  For privacy reasons, this cannot be used with production patient info.  AttachName should include extension.</summary>
		public static void SendTest(string subjectAndBody,string attachName1,string attachContents1,string attachName2,string attachContents2) {
			string strTo=PrefC.GetString(PrefName.EHREmailToAddress);
			if(strTo=="") {
				throw new ApplicationException("This feature cannot be used except in a test environment because email is not secure.");
			}
			EmailAddress emailAddressFrom=EmailAddresses.GetByClinic(0);
			MailMessage message=new MailMessage();
			message.From=new MailAddress(emailAddressFrom.EmailUsername);//This probably cannot be emailAddressFrom.SenderAddress, because of strict security regarding Direct messages.
			message.To.Add(strTo);
			message.Subject=subjectAndBody;
			message.Body=subjectAndBody;
			message.IsBodyHtml=false;
			List<Health.Direct.Common.Mime.MimeEntity> listAttachments=new List<Health.Direct.Common.Mime.MimeEntity>();
			listAttachments.Add(CreateTextAttachment(attachContents1,attachName1));
			if(attachContents2!="" && attachName2!="") {
				listAttachments.Add(CreateTextAttachment(attachContents2,attachName2));
			}
			EhrEmail.SendEmailDirect(strTo,emailAddressFrom,subjectAndBody,0,listAttachments);
		}

		///<summary>Receives one email from the inbox, and returns the contents of the attachment as a string.  Will throw an exception if anything goes wrong, so surround with a try-catch.</summary>
		public static string ReceiveOneForEhrTest() {
			if(PrefC.GetString(PrefName.EHREmailToAddress)=="") {//this pref is hidden, so no practical way for user to turn this on.
				throw new ApplicationException("This feature cannot be used except in a test environment because email is not secure.");
			}
			if(PrefC.GetString(PrefName.EHREmailPOPserver)=="") {
				throw new ApplicationException("No POP server set up.");
			}
			EmailAddress emailAddress=new EmailAddress();
			emailAddress.Pop3ServerIncoming=PrefC.GetString(PrefName.EHREmailPOPserver);
			emailAddress.ServerPortIncoming=PrefC.GetInt(PrefName.EHREmailPort);
			emailAddress.EmailUsername=PrefC.GetString(PrefName.EHREmailFromAddress);
			emailAddress.EmailPassword=PrefC.GetString(PrefName.EHREmailPassword);
			return ReceiveFromInbox(1,emailAddress)[0].BodyText;//List might be zero in length, but the caller is supposed to surround with try/catch anyway.
		}

		private static string GetTestEmail1() {
			return @"This is a multipart message in MIME format.

------=_NextPart_000_0074_01CC35A4.193BF450
Content-Type: multipart/alternative;
	boundary=""----=_NextPart_001_0075_01CC35A4.193BF450""


------=_NextPart_001_0075_01CC35A4.193BF450
Content-Type: text/plain;
	charset=""us-ascii""
Content-Transfer-Encoding: 7bit

test


------=_NextPart_001_0075_01CC35A4.193BF450
Content-Type: text/html;
	charset=""us-ascii""
Content-Transfer-Encoding: quoted-printable

<html xmlns:v=3D""urn:schemas-microsoft-com:vml"" =
xmlns:o=3D""urn:schemas-microsoft-com:office:office"" =
xmlns:w=3D""urn:schemas-microsoft-com:office:word"" =
xmlns:m=3D""http://schemas.microsoft.com/office/2004/12/omml"" =
xmlns=3D""http://www.w3.org/TR/REC-html40""><head><meta =
http-equiv=3DContent-Type content=3D""text/html; =
charset=3Dus-ascii""><meta name=3DGenerator content=3D""Microsoft Word 14 =
(filtered medium)""><style><!--
/* Font Definitions */
@font-face
	{font-family:Calibri;
	panose-1:2 15 5 2 2 2 4 3 2 4;}
/* Style Definitions */
p.MsoNormal, li.MsoNormal, div.MsoNormal
	{margin:0in;
	margin-bottom:.0001pt;
	font-size:11.0pt;
	font-family:""Calibri"",""sans-serif"";}
a:link, span.MsoHyperlink
	{mso-style-priority:99;
	color:blue;
	text-decoration:underline;}
a:visited, span.MsoHyperlinkFollowed
	{mso-style-priority:99;
	color:purple;
	text-decoration:underline;}
span.EmailStyle17
	{mso-style-type:personal-compose;
	font-family:""Calibri"",""sans-serif"";
	color:windowtext;}
..MsoChpDefault
	{mso-style-type:export-only;
	font-family:""Calibri"",""sans-serif"";}
@page WordSection1
	{size:8.5in 11.0in;
	margin:1.0in 1.0in 1.0in 1.0in;}
div.WordSection1
	{page:WordSection1;}
--></style><!--[if gte mso 9]><xml>
<o:shapedefaults v:ext=3D""edit"" spidmax=3D""1026"" />
</xml><![endif]--><!--[if gte mso 9]><xml>
<o:shapelayout v:ext=3D""edit"">
<o:idmap v:ext=3D""edit"" data=3D""1"" />
</o:shapelayout></xml><![endif]--></head><body lang=3DEN-US link=3Dblue =
vlink=3Dpurple><div class=3DWordSection1><p =
class=3DMsoNormal>test<o:p></o:p></p></div></body></html>
------=_NextPart_001_0075_01CC35A4.193BF450--

------=_NextPart_000_0074_01CC35A4.193BF450
Content-Type: text/plain;
	name=""SarahEbbert_v4.txt""
Content-Transfer-Encoding: quoted-printable
Content-Disposition: attachment;
	filename=""SarahEbbert_v4.txt""

<?xml version=3D""1.0"" encoding=3D""UTF-8""?>
<ClinicalDocument xmlns=3D""urn:hl7-org:v3"">
   <typeId extension=3D""POCD_HD0000040"" root=3D""2.16.840.1.113883.1.3"" =
/>
   <templateId root=3D""2.16.840.1.113883.10.20.1"" />
   <id />
   <code code=3D""34133-9"" codeSystemName=3D""LOINC"" =
codeSystem=3D""2.16.840.1.113883.6.1"" displayName=3D""Summary of episode =
note"" />
   <documentationOf>
      <serviceEvent classCode=3D""PCPR"">
         <effectiveTime>
            <high value=3D""20110628075321-0700"" />
            <low value=3D""19621008000000-0700"" />
         </effectiveTime>
      </serviceEvent>
   </documentationOf>
   <languageCode value=3D""en-US"" />
   <templateId root=3D""2.16.840.1.113883.10.20.1"" />
   <effectiveTime value=3D""20110628075321-0700"" />
   <recordTarget>
      <patientRole>
         <id value=3D""7"" />
         <addr use=3D""HP"">
            <streetAddressLine>856 Salt Street</streetAddressLine>
            <streetAddressLine></streetAddressLine>
            <city>Shawville</city>
            <state>PA</state>
            <country></country>
         </addr>
         <patient>
            <name use=3D""L"">
               <given>Sarah</given>
               <given></given>
               <family>Ebbert</family>
               <suffix qualifier=3D""TITLE""></suffix>
            </name>
         </patient>
      </patientRole>
      <text>
         <table width=3D""100%"" border=3D""1"">
            <thead>
               <tr>
                  <th>Name</th>
                  <th>Date of Birth</th>
                  <th>Gender</th>
                  <th>Identification Number</th>
                  <th>Identification Number Type</th>
                  <th>Address/Phone</th>
               </tr>
            </thead>
            <tbody>
               <tr>
                  <td>Ebbert, Sarah </td>
                  <td>10/08/1962</td>
                  <td>Female</td>
                  <td>7</td>
                  <td>Open Dental PatNum</td>
                  <td>856 Salt Street=20
Shawville, PA
16873
(814)645-6489</td>
               </tr>
            </tbody>
         </table>
      </text>
   </recordTarget>
   <author>
      <assignedAuthor>
         <assignedPerson>
            <name>Auto Generated</name>
         </assignedPerson>
      </assignedAuthor>
   </author>
   <component>
      <!--Problems-->
      <section>
         <templateId root=3D""2.16.840.1.113883.10.20.1.11"" =
assigningAuthorityName=3D""HL7 CCD"" />
         <!--Problems section template-->
         <code code=3D""11450-4"" codeSystemName=3D""LOINC"" =
codeSystem=3D""2.16.840.1.113883.6.1"" displayName=3D""Problem list"" />
         <title>Problems</title>
         <text>
            <table width=3D""100%"" border=3D""1"">
               <thead>
                  <tr>
                     <th>ICD-9 Code</th>
                     <th>Patient Problem</th>
                     <th>Date Diagnosed</th>
                     <th>Status</th>
                  </tr>
               </thead>
               <tbody>
                  <tr ID=3D""CondID-1"">
                     <td>272.4</td>
                     <td>OTHER AND UNSPECIFIED HYPERLIPIDEMIA</td>
                     <td>07/05/2006</td>
                     <td>Active</td>
                  </tr>
                  <tr ID=3D""CondID-1"">
                     <td>401.9</td>
                     <td>UNSPECIFIED ESSENTIAL HYPERTENSION</td>
                     <td>07/05/2006</td>
                     <td>Active</td>
                  </tr>
               </tbody>
            </table>
         </text>
      </section>
      <component>
         <!--Alerts-->
         <section>
            <templateId root=3D""2.16.840.1.113883.10.20.1.2"" =
assigningAuthorityName=3D""HL7 CCD"" />
            <!--Alerts section template-->
            <code code=3D""48765-2"" codeSystemName=3D""LOINC"" =
codeSystem=3D""2.16.840.1.113883.6.1"" displayName=3D""Allergies, adverse =
reactions, alerts"" />
            <title>Allergies and Adverse Reactions</title>
            <text>
               <table width=3D""100%"" border=3D""1"">
                  <thead>
                     <tr>
                        <th>SNOMED Allergy Type Code</th>
                        <th>Medication/Agent Allergy</th>
                        <th>Reaction</th>
                        <th>Adverse Event Date</th>
                     </tr>
                  </thead>
                  <tbody>
                     <tr>
                        <td>416098002 - Drug allergy (disorder)</td>
                        <td>617314 - Lipitor</td>
                        <td>Rash and anaphylaxis</td>
                        <td>05/22/1998</td>
                     </tr>
                  </tbody>
               </table>
            </text>
         </section>
         <component>
            <!--Medications-->
            <section>
               <templateId root=3D""2.16.840.1.113883.10.20.1.8"" =
assigningAuthorityName=3D""HL7 CCD"" />
               <!--Medications section template-->
               <code code=3D""10160-0"" codeSystemName=3D""LOINC"" =
codeSystem=3D""2.16.840.1.113883.6.1"" displayName=3D""History of =
medication use"" />
               <title>Medications</title>
               <text>
                  <table width=3D""100%"" border=3D""1"">
                     <thead>
                        <tr>
                           <th>RxNorm Code</th>
                           <th>Product</th>
                           <th>Generic Name</th>
                           <th>Brand Name</th>
                           <th>Instructions</th>
                           <th>Date Started</th>
                           <th>Status</th>
                        </tr>
                     </thead>
                     <tbody>
                        <tr>
                           <td>617314</td>
                           <td>Medication</td>
                           <td>atorvastatin calcium</td>
                           <td>Lipitor</td>
                           <td>10 mg, 1 Tablet, Q Day</td>
                           <td>07/05/2006</td>
                           <td>Active</td>
                        </tr>
                        <tr>
                           <td>200801</td>
                           <td>Medication</td>
                           <td>furosemide</td>
                           <td>Lasix</td>
                           <td>20 mg, 1 Tablet, BID</td>
                           <td>07/05/2006</td>
                           <td>Active</td>
                        </tr>
                        <tr>
                           <td>628958</td>
                           <td>Medication</td>
                           <td>potassium chloride</td>
                           <td>Klor-Con</td>
                           <td>10 mEq, 1 Tablet, BID</td>
                           <td>07/05/2006</td>
                           <td>Active</td>
                        </tr>
                     </tbody>
                  </table>
               </text>
            </section>
            <component>
               <!--Results-->
               <section>
                  <templateId root=3D""2.16.840.1.113883.10.20.1.14"" =
assigningAuthorityName=3D""HL7 CCD"" />
                  <!--Relevant diagnostic tests and/or labratory data-->
                  <code code=3D""30954-2"" codeSystemName=3D""LOINC"" =
codeSystem=3D""2.16.840.1.113883.6.1"" displayName=3D""Allergies, adverse =
reactions, alerts"" />
                  <title>Results</title>
                  <text>
                     <table width=3D""100%"" border=3D""1"">
                        <thead>
                           <tr>
                              <th>LOINC Code</th>
                              <th>Test</th>
                              <th>Result</th>
                              <th>Abnormal Flag</th>
                              <th>Date Performed</th>
                           </tr>
                        </thead>
                        <tbody>
                           <tr>
                              <td>2823-3</td>
                              <td>Potassium</td>
                              <td>Normal</td>
                              <td>02/15/2009</td>
                           </tr>
                           <tr>
                              <td>14647-2</td>
                              <td>Total cholesterol</td>
                              <td>Normal</td>
                              <td>07/15/2009</td>
                           </tr>
                           <tr>
                              <td>14646-4</td>
                              <td>HDL cholesterol</td>
                              <td>Normal</td>
                              <td>07/15/2009</td>
                           </tr>
                           <tr>
                              <td>2089-1</td>
                              <td>LDL cholesterol</td>
                              <td>Above</td>
                              <td>07/15/2009</td>
                           </tr>
                           <tr>
                              <td>14927-8</td>
                              <td>Triglycerides</td>
                              <td>Above</td>
                              <td>07/15/2009</td>
                           </tr>
                        </tbody>
                     </table>
                  </text>
               </section>
            </component>
         </component>
      </component>
   </component>
</ClinicalDocument>
------=_NextPart_000_0074_01CC35A4.193BF450--";
		}

		private static string GetTestEmail2() {
			return @"This is a multi-part message in MIME format.
--------------070304090505090508040909
Content-Type: text/plain; charset=ISO-8859-1; format=flowed
Content-Transfer-Encoding: 7bit

Clinical Exchange Test

--------------070304090505090508040909
Content-Type: text/plain;
 name=""SarahEbbert_v4.txt""
Content-Transfer-Encoding: base64
Content-Disposition: attachment;
 filename=""SarahEbbert_v4.txt""

PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4NCjxDbGluaWNhbERvY3Vt
ZW50IHhtbG5zPSJ1cm46aGw3LW9yZzp2MyI+DQogICA8dHlwZUlkIGV4dGVuc2lvbj0iUE9D
RF9IRDAwMDAwNDAiIHJvb3Q9IjIuMTYuODQwLjEuMTEzODgzLjEuMyIgLz4NCiAgIDx0ZW1w
bGF0ZUlkIHJvb3Q9IjIuMTYuODQwLjEuMTEzODgzLjEwLjIwLjEiIC8+DQogICA8aWQgLz4N
CiAgIDxjb2RlIGNvZGU9IjM0MTMzLTkiIGNvZGVTeXN0ZW1OYW1lPSJMT0lOQyIgY29kZVN5
c3RlbT0iMi4xNi44NDAuMS4xMTM4ODMuNi4xIiBkaXNwbGF5TmFtZT0iU3VtbWFyeSBvZiBl
cGlzb2RlIG5vdGUiIC8+DQogICA8ZG9jdW1lbnRhdGlvbk9mPg0KICAgICAgPHNlcnZpY2VF
dmVudCBjbGFzc0NvZGU9IlBDUFIiPg0KICAgICAgICAgPGVmZmVjdGl2ZVRpbWU+DQogICAg
ICAgICAgICA8aGlnaCB2YWx1ZT0iMjAxMTA2MjgwNzUzMjEtMDcwMCIgLz4NCiAgICAgICAg
ICAgIDxsb3cgdmFsdWU9IjE5NjIxMDA4MDAwMDAwLTA3MDAiIC8+DQogICAgICAgICA8L2Vm
ZmVjdGl2ZVRpbWU+DQogICAgICA8L3NlcnZpY2VFdmVudD4NCiAgIDwvZG9jdW1lbnRhdGlv
bk9mPg0KICAgPGxhbmd1YWdlQ29kZSB2YWx1ZT0iZW4tVVMiIC8+DQogICA8dGVtcGxhdGVJ
ZCByb290PSIyLjE2Ljg0MC4xLjExMzg4My4xMC4yMC4xIiAvPg0KICAgPGVmZmVjdGl2ZVRp
bWUgdmFsdWU9IjIwMTEwNjI4MDc1MzIxLTA3MDAiIC8+DQogICA8cmVjb3JkVGFyZ2V0Pg0K
ICAgICAgPHBhdGllbnRSb2xlPg0KICAgICAgICAgPGlkIHZhbHVlPSI3IiAvPg0KICAgICAg
ICAgPGFkZHIgdXNlPSJIUCI+DQogICAgICAgICAgICA8c3RyZWV0QWRkcmVzc0xpbmU+ODU2
IFNhbHQgU3RyZWV0PC9zdHJlZXRBZGRyZXNzTGluZT4NCiAgICAgICAgICAgIDxzdHJlZXRB
ZGRyZXNzTGluZT48L3N0cmVldEFkZHJlc3NMaW5lPg0KICAgICAgICAgICAgPGNpdHk+U2hh
d3ZpbGxlPC9jaXR5Pg0KICAgICAgICAgICAgPHN0YXRlPlBBPC9zdGF0ZT4NCiAgICAgICAg
ICAgIDxjb3VudHJ5PjwvY291bnRyeT4NCiAgICAgICAgIDwvYWRkcj4NCiAgICAgICAgIDxw
YXRpZW50Pg0KICAgICAgICAgICAgPG5hbWUgdXNlPSJMIj4NCiAgICAgICAgICAgICAgIDxn
aXZlbj5TYXJhaDwvZ2l2ZW4+DQogICAgICAgICAgICAgICA8Z2l2ZW4+PC9naXZlbj4NCiAg
ICAgICAgICAgICAgIDxmYW1pbHk+RWJiZXJ0PC9mYW1pbHk+DQogICAgICAgICAgICAgICA8
c3VmZml4IHF1YWxpZmllcj0iVElUTEUiPjwvc3VmZml4Pg0KICAgICAgICAgICAgPC9uYW1l
Pg0KICAgICAgICAgPC9wYXRpZW50Pg0KICAgICAgPC9wYXRpZW50Um9sZT4NCiAgICAgIDx0
ZXh0Pg0KICAgICAgICAgPHRhYmxlIHdpZHRoPSIxMDAlIiBib3JkZXI9IjEiPg0KICAgICAg
ICAgICAgPHRoZWFkPg0KICAgICAgICAgICAgICAgPHRyPg0KICAgICAgICAgICAgICAgICAg
PHRoPk5hbWU8L3RoPg0KICAgICAgICAgICAgICAgICAgPHRoPkRhdGUgb2YgQmlydGg8L3Ro
Pg0KICAgICAgICAgICAgICAgICAgPHRoPkdlbmRlcjwvdGg+DQogICAgICAgICAgICAgICAg
ICA8dGg+SWRlbnRpZmljYXRpb24gTnVtYmVyPC90aD4NCiAgICAgICAgICAgICAgICAgIDx0
aD5JZGVudGlmaWNhdGlvbiBOdW1iZXIgVHlwZTwvdGg+DQogICAgICAgICAgICAgICAgICA8
dGg+QWRkcmVzcy9QaG9uZTwvdGg+DQogICAgICAgICAgICAgICA8L3RyPg0KICAgICAgICAg
ICAgPC90aGVhZD4NCiAgICAgICAgICAgIDx0Ym9keT4NCiAgICAgICAgICAgICAgIDx0cj4N
CiAgICAgICAgICAgICAgICAgIDx0ZD5FYmJlcnQsIFNhcmFoIDwvdGQ+DQogICAgICAgICAg
ICAgICAgICA8dGQ+MTAvMDgvMTk2MjwvdGQ+DQogICAgICAgICAgICAgICAgICA8dGQ+RmVt
YWxlPC90ZD4NCiAgICAgICAgICAgICAgICAgIDx0ZD43PC90ZD4NCiAgICAgICAgICAgICAg
ICAgIDx0ZD5PcGVuIERlbnRhbCBQYXROdW08L3RkPg0KICAgICAgICAgICAgICAgICAgPHRk
Pjg1NiBTYWx0IFN0cmVldCANClNoYXd2aWxsZSwgUEENCjE2ODczDQooODE0KTY0NS02NDg5
PC90ZD4NCiAgICAgICAgICAgICAgIDwvdHI+DQogICAgICAgICAgICA8L3Rib2R5Pg0KICAg
ICAgICAgPC90YWJsZT4NCiAgICAgIDwvdGV4dD4NCiAgIDwvcmVjb3JkVGFyZ2V0Pg0KICAg
PGF1dGhvcj4NCiAgICAgIDxhc3NpZ25lZEF1dGhvcj4NCiAgICAgICAgIDxhc3NpZ25lZFBl
cnNvbj4NCiAgICAgICAgICAgIDxuYW1lPkF1dG8gR2VuZXJhdGVkPC9uYW1lPg0KICAgICAg
ICAgPC9hc3NpZ25lZFBlcnNvbj4NCiAgICAgIDwvYXNzaWduZWRBdXRob3I+DQogICA8L2F1
dGhvcj4NCiAgIDxjb21wb25lbnQ+DQogICAgICA8IS0tUHJvYmxlbXMtLT4NCiAgICAgIDxz
ZWN0aW9uPg0KICAgICAgICAgPHRlbXBsYXRlSWQgcm9vdD0iMi4xNi44NDAuMS4xMTM4ODMu
MTAuMjAuMS4xMSIgYXNzaWduaW5nQXV0aG9yaXR5TmFtZT0iSEw3IENDRCIgLz4NCiAgICAg
ICAgIDwhLS1Qcm9ibGVtcyBzZWN0aW9uIHRlbXBsYXRlLS0+DQogICAgICAgICA8Y29kZSBj
b2RlPSIxMTQ1MC00IiBjb2RlU3lzdGVtTmFtZT0iTE9JTkMiIGNvZGVTeXN0ZW09IjIuMTYu
ODQwLjEuMTEzODgzLjYuMSIgZGlzcGxheU5hbWU9IlByb2JsZW0gbGlzdCIgLz4NCiAgICAg
ICAgIDx0aXRsZT5Qcm9ibGVtczwvdGl0bGU+DQogICAgICAgICA8dGV4dD4NCiAgICAgICAg
ICAgIDx0YWJsZSB3aWR0aD0iMTAwJSIgYm9yZGVyPSIxIj4NCiAgICAgICAgICAgICAgIDx0
aGVhZD4NCiAgICAgICAgICAgICAgICAgIDx0cj4NCiAgICAgICAgICAgICAgICAgICAgIDx0
aD5JQ0QtOSBDb2RlPC90aD4NCiAgICAgICAgICAgICAgICAgICAgIDx0aD5QYXRpZW50IFBy
b2JsZW08L3RoPg0KICAgICAgICAgICAgICAgICAgICAgPHRoPkRhdGUgRGlhZ25vc2VkPC90
aD4NCiAgICAgICAgICAgICAgICAgICAgIDx0aD5TdGF0dXM8L3RoPg0KICAgICAgICAgICAg
ICAgICAgPC90cj4NCiAgICAgICAgICAgICAgIDwvdGhlYWQ+DQogICAgICAgICAgICAgICA8
dGJvZHk+DQogICAgICAgICAgICAgICAgICA8dHIgSUQ9IkNvbmRJRC0xIj4NCiAgICAgICAg
ICAgICAgICAgICAgIDx0ZD4yNzIuNDwvdGQ+DQogICAgICAgICAgICAgICAgICAgICA8dGQ+
T1RIRVIgQU5EIFVOU1BFQ0lGSUVEIEhZUEVSTElQSURFTUlBPC90ZD4NCiAgICAgICAgICAg
ICAgICAgICAgIDx0ZD4wNy8wNS8yMDA2PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgIDx0
ZD5BY3RpdmU8L3RkPg0KICAgICAgICAgICAgICAgICAgPC90cj4NCiAgICAgICAgICAgICAg
ICAgIDx0ciBJRD0iQ29uZElELTEiPg0KICAgICAgICAgICAgICAgICAgICAgPHRkPjQwMS45
PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgIDx0ZD5VTlNQRUNJRklFRCBFU1NFTlRJQUwg
SFlQRVJURU5TSU9OPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgIDx0ZD4wNy8wNS8yMDA2
PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgIDx0ZD5BY3RpdmU8L3RkPg0KICAgICAgICAg
ICAgICAgICAgPC90cj4NCiAgICAgICAgICAgICAgIDwvdGJvZHk+DQogICAgICAgICAgICA8
L3RhYmxlPg0KICAgICAgICAgPC90ZXh0Pg0KICAgICAgPC9zZWN0aW9uPg0KICAgICAgPGNv
bXBvbmVudD4NCiAgICAgICAgIDwhLS1BbGVydHMtLT4NCiAgICAgICAgIDxzZWN0aW9uPg0K
ICAgICAgICAgICAgPHRlbXBsYXRlSWQgcm9vdD0iMi4xNi44NDAuMS4xMTM4ODMuMTAuMjAu
MS4yIiBhc3NpZ25pbmdBdXRob3JpdHlOYW1lPSJITDcgQ0NEIiAvPg0KICAgICAgICAgICAg
PCEtLUFsZXJ0cyBzZWN0aW9uIHRlbXBsYXRlLS0+DQogICAgICAgICAgICA8Y29kZSBjb2Rl
PSI0ODc2NS0yIiBjb2RlU3lzdGVtTmFtZT0iTE9JTkMiIGNvZGVTeXN0ZW09IjIuMTYuODQw
LjEuMTEzODgzLjYuMSIgZGlzcGxheU5hbWU9IkFsbGVyZ2llcywgYWR2ZXJzZSByZWFjdGlv
bnMsIGFsZXJ0cyIgLz4NCiAgICAgICAgICAgIDx0aXRsZT5BbGxlcmdpZXMgYW5kIEFkdmVy
c2UgUmVhY3Rpb25zPC90aXRsZT4NCiAgICAgICAgICAgIDx0ZXh0Pg0KICAgICAgICAgICAg
ICAgPHRhYmxlIHdpZHRoPSIxMDAlIiBib3JkZXI9IjEiPg0KICAgICAgICAgICAgICAgICAg
PHRoZWFkPg0KICAgICAgICAgICAgICAgICAgICAgPHRyPg0KICAgICAgICAgICAgICAgICAg
ICAgICAgPHRoPlNOT01FRCBBbGxlcmd5IFR5cGUgQ29kZTwvdGg+DQogICAgICAgICAgICAg
ICAgICAgICAgICA8dGg+TWVkaWNhdGlvbi9BZ2VudCBBbGxlcmd5PC90aD4NCiAgICAgICAg
ICAgICAgICAgICAgICAgIDx0aD5SZWFjdGlvbjwvdGg+DQogICAgICAgICAgICAgICAgICAg
ICAgICA8dGg+QWR2ZXJzZSBFdmVudCBEYXRlPC90aD4NCiAgICAgICAgICAgICAgICAgICAg
IDwvdHI+DQogICAgICAgICAgICAgICAgICA8L3RoZWFkPg0KICAgICAgICAgICAgICAgICAg
PHRib2R5Pg0KICAgICAgICAgICAgICAgICAgICAgPHRyPg0KICAgICAgICAgICAgICAgICAg
ICAgICAgPHRkPjQxNjA5ODAwMiAtIERydWcgYWxsZXJneSAoZGlzb3JkZXIpPC90ZD4NCiAg
ICAgICAgICAgICAgICAgICAgICAgIDx0ZD42MTczMTQgLSBMaXBpdG9yPC90ZD4NCiAgICAg
ICAgICAgICAgICAgICAgICAgIDx0ZD5SYXNoIGFuZCBhbmFwaHlsYXhpczwvdGQ+DQogICAg
ICAgICAgICAgICAgICAgICAgICA8dGQ+MDUvMjIvMTk5ODwvdGQ+DQogICAgICAgICAgICAg
ICAgICAgICA8L3RyPg0KICAgICAgICAgICAgICAgICAgPC90Ym9keT4NCiAgICAgICAgICAg
ICAgIDwvdGFibGU+DQogICAgICAgICAgICA8L3RleHQ+DQogICAgICAgICA8L3NlY3Rpb24+
DQogICAgICAgICA8Y29tcG9uZW50Pg0KICAgICAgICAgICAgPCEtLU1lZGljYXRpb25zLS0+
DQogICAgICAgICAgICA8c2VjdGlvbj4NCiAgICAgICAgICAgICAgIDx0ZW1wbGF0ZUlkIHJv
b3Q9IjIuMTYuODQwLjEuMTEzODgzLjEwLjIwLjEuOCIgYXNzaWduaW5nQXV0aG9yaXR5TmFt
ZT0iSEw3IENDRCIgLz4NCiAgICAgICAgICAgICAgIDwhLS1NZWRpY2F0aW9ucyBzZWN0aW9u
IHRlbXBsYXRlLS0+DQogICAgICAgICAgICAgICA8Y29kZSBjb2RlPSIxMDE2MC0wIiBjb2Rl
U3lzdGVtTmFtZT0iTE9JTkMiIGNvZGVTeXN0ZW09IjIuMTYuODQwLjEuMTEzODgzLjYuMSIg
ZGlzcGxheU5hbWU9Ikhpc3Rvcnkgb2YgbWVkaWNhdGlvbiB1c2UiIC8+DQogICAgICAgICAg
ICAgICA8dGl0bGU+TWVkaWNhdGlvbnM8L3RpdGxlPg0KICAgICAgICAgICAgICAgPHRleHQ+
DQogICAgICAgICAgICAgICAgICA8dGFibGUgd2lkdGg9IjEwMCUiIGJvcmRlcj0iMSI+DQog
ICAgICAgICAgICAgICAgICAgICA8dGhlYWQ+DQogICAgICAgICAgICAgICAgICAgICAgICA8
dHI+DQogICAgICAgICAgICAgICAgICAgICAgICAgICA8dGg+UnhOb3JtIENvZGU8L3RoPg0K
ICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRoPlByb2R1Y3Q8L3RoPg0KICAgICAgICAg
ICAgICAgICAgICAgICAgICAgPHRoPkdlbmVyaWMgTmFtZTwvdGg+DQogICAgICAgICAgICAg
ICAgICAgICAgICAgICA8dGg+QnJhbmQgTmFtZTwvdGg+DQogICAgICAgICAgICAgICAgICAg
ICAgICAgICA8dGg+SW5zdHJ1Y3Rpb25zPC90aD4NCiAgICAgICAgICAgICAgICAgICAgICAg
ICAgIDx0aD5EYXRlIFN0YXJ0ZWQ8L3RoPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAg
PHRoPlN0YXR1czwvdGg+DQogICAgICAgICAgICAgICAgICAgICAgICA8L3RyPg0KICAgICAg
ICAgICAgICAgICAgICAgPC90aGVhZD4NCiAgICAgICAgICAgICAgICAgICAgIDx0Ym9keT4N
CiAgICAgICAgICAgICAgICAgICAgICAgIDx0cj4NCiAgICAgICAgICAgICAgICAgICAgICAg
ICAgIDx0ZD42MTczMTQ8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPk1l
ZGljYXRpb248L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPmF0b3J2YXN0
YXRpbiBjYWxjaXVtPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD5MaXBp
dG9yPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD4xMCBtZywgMSBUYWJs
ZXQsIFEgRGF5PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD4wNy8wNS8y
MDA2PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD5BY3RpdmU8L3RkPg0K
ICAgICAgICAgICAgICAgICAgICAgICAgPC90cj4NCiAgICAgICAgICAgICAgICAgICAgICAg
IDx0cj4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD4yMDA4MDE8L3RkPg0KICAg
ICAgICAgICAgICAgICAgICAgICAgICAgPHRkPk1lZGljYXRpb248L3RkPg0KICAgICAgICAg
ICAgICAgICAgICAgICAgICAgPHRkPmZ1cm9zZW1pZGU8L3RkPg0KICAgICAgICAgICAgICAg
ICAgICAgICAgICAgPHRkPkxhc2l4PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAg
IDx0ZD4yMCBtZywgMSBUYWJsZXQsIEJJRDwvdGQ+DQogICAgICAgICAgICAgICAgICAgICAg
ICAgICA8dGQ+MDcvMDUvMjAwNjwvdGQ+DQogICAgICAgICAgICAgICAgICAgICAgICAgICA8
dGQ+QWN0aXZlPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgIDwvdHI+DQogICAgICAg
ICAgICAgICAgICAgICAgICA8dHI+DQogICAgICAgICAgICAgICAgICAgICAgICAgICA8dGQ+
NjI4OTU4PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD5NZWRpY2F0aW9u
PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD5wb3Rhc3NpdW0gY2hsb3Jp
ZGU8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPktsb3ItQ29uPC90ZD4N
CiAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD4xMCBtRXEsIDEgVGFibGV0LCBCSUQ8
L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPjA3LzA1LzIwMDY8L3RkPg0K
ICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPkFjdGl2ZTwvdGQ+DQogICAgICAgICAg
ICAgICAgICAgICAgICA8L3RyPg0KICAgICAgICAgICAgICAgICAgICAgPC90Ym9keT4NCiAg
ICAgICAgICAgICAgICAgIDwvdGFibGU+DQogICAgICAgICAgICAgICA8L3RleHQ+DQogICAg
ICAgICAgICA8L3NlY3Rpb24+DQogICAgICAgICAgICA8Y29tcG9uZW50Pg0KICAgICAgICAg
ICAgICAgPCEtLVJlc3VsdHMtLT4NCiAgICAgICAgICAgICAgIDxzZWN0aW9uPg0KICAgICAg
ICAgICAgICAgICAgPHRlbXBsYXRlSWQgcm9vdD0iMi4xNi44NDAuMS4xMTM4ODMuMTAuMjAu
MS4xNCIgYXNzaWduaW5nQXV0aG9yaXR5TmFtZT0iSEw3IENDRCIgLz4NCiAgICAgICAgICAg
ICAgICAgIDwhLS1SZWxldmFudCBkaWFnbm9zdGljIHRlc3RzIGFuZC9vciBsYWJyYXRvcnkg
ZGF0YS0tPg0KICAgICAgICAgICAgICAgICAgPGNvZGUgY29kZT0iMzA5NTQtMiIgY29kZVN5
c3RlbU5hbWU9IkxPSU5DIiBjb2RlU3lzdGVtPSIyLjE2Ljg0MC4xLjExMzg4My42LjEiIGRp
c3BsYXlOYW1lPSJBbGxlcmdpZXMsIGFkdmVyc2UgcmVhY3Rpb25zLCBhbGVydHMiIC8+DQog
ICAgICAgICAgICAgICAgICA8dGl0bGU+UmVzdWx0czwvdGl0bGU+DQogICAgICAgICAgICAg
ICAgICA8dGV4dD4NCiAgICAgICAgICAgICAgICAgICAgIDx0YWJsZSB3aWR0aD0iMTAwJSIg
Ym9yZGVyPSIxIj4NCiAgICAgICAgICAgICAgICAgICAgICAgIDx0aGVhZD4NCiAgICAgICAg
ICAgICAgICAgICAgICAgICAgIDx0cj4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAg
IDx0aD5MT0lOQyBDb2RlPC90aD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0
aD5UZXN0PC90aD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0aD5SZXN1bHQ8
L3RoPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRoPkFibm9ybWFsIEZsYWc8
L3RoPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRoPkRhdGUgUGVyZm9ybWVk
PC90aD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDwvdHI+DQogICAgICAgICAgICAg
ICAgICAgICAgICA8L3RoZWFkPg0KICAgICAgICAgICAgICAgICAgICAgICAgPHRib2R5Pg0K
ICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRyPg0KICAgICAgICAgICAgICAgICAgICAg
ICAgICAgICAgPHRkPjI4MjMtMzwvdGQ+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAg
ICA8dGQ+UG90YXNzaXVtPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0
ZD5Ob3JtYWw8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPjAyLzE1
LzIwMDk8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90cj4NCiAgICAgICAg
ICAgICAgICAgICAgICAgICAgIDx0cj4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAg
IDx0ZD4xNDY0Ny0yPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD5U
b3RhbCBjaG9sZXN0ZXJvbDwvdGQ+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8
dGQ+Tm9ybWFsPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD4wNy8x
NS8yMDA5PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgIDwvdHI+DQogICAgICAg
ICAgICAgICAgICAgICAgICAgICA8dHI+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAg
ICA8dGQ+MTQ2NDYtNDwvdGQ+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dGQ+
SERMIGNob2xlc3Rlcm9sPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0
ZD5Ob3JtYWw8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPjA3LzE1
LzIwMDk8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90cj4NCiAgICAgICAg
ICAgICAgICAgICAgICAgICAgIDx0cj4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAg
IDx0ZD4yMDg5LTE8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPkxE
TCBjaG9sZXN0ZXJvbDwvdGQ+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dGQ+
QWJvdmU8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgPHRkPjA3LzE1LzIw
MDk8L3RkPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgPC90cj4NCiAgICAgICAgICAg
ICAgICAgICAgICAgICAgIDx0cj4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0
ZD4xNDkyNy04PC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD5Ucmln
bHljZXJpZGVzPC90ZD4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDx0ZD5BYm92
ZTwvdGQ+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8dGQ+MDcvMTUvMjAwOTwv
dGQ+DQogICAgICAgICAgICAgICAgICAgICAgICAgICA8L3RyPg0KICAgICAgICAgICAgICAg
ICAgICAgICAgPC90Ym9keT4NCiAgICAgICAgICAgICAgICAgICAgIDwvdGFibGU+DQogICAg
ICAgICAgICAgICAgICA8L3RleHQ+DQogICAgICAgICAgICAgICA8L3NlY3Rpb24+DQogICAg
ICAgICAgICA8L2NvbXBvbmVudD4NCiAgICAgICAgIDwvY29tcG9uZW50Pg0KICAgICAgPC9j
b21wb25lbnQ+DQogICA8L2NvbXBvbmVudD4NCjwvQ2xpbmljYWxEb2N1bWVudD4=
--------------070304090505090508040909--

";
		}

		#endregion Test




	}
}
