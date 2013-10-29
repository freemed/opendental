using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using OpenDentBusiness;
using CodeBase;

namespace OpenDentBusiness{
	///<summary>An email message is always attached to a patient.</summary>
	public class EmailMessages{

		///<summary>Used to cache DirectAgent objects, because creating a new DirectAgent object takes up to 10 seconds. If we did not cache, then inbox load would be slow and so would Direct message sending.</summary>
		private static Hashtable HashDirectAgents=new Hashtable();

		///<summary>Gets one email message from the database.</summary>
		public static EmailMessage GetOne(long msgNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<EmailMessage>(MethodBase.GetCurrentMethod(),msgNum);
			}
			string command="SELECT * FROM emailmessage WHERE EmailMessageNum = "+POut.Long(msgNum);
			EmailMessage message=Crud.EmailMessageCrud.SelectOne(msgNum);
			command="SELECT * FROM emailattach WHERE EmailMessageNum = "+POut.Long(msgNum);
			message.Attachments=Crud.EmailAttachCrud.SelectMany(command);
			return message;
		}

		public static List<EmailMessage> GetInboxForAddress(string emailAddressInbox) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List <EmailMessage>>(MethodBase.GetCurrentMethod(),emailAddressInbox);
			}
			string command="SELECT * FROM emailmessage "
				+"WHERE SentOrReceived IN ("
					+POut.Int((int)EmailSentOrReceived.Read)+","
					+POut.Int((int)EmailSentOrReceived.Received)+","
					+POut.Int((int)EmailSentOrReceived.ReceivedEncrypted)+","
					+POut.Int((int)EmailSentOrReceived.ReceivedDirect)+","
					+POut.Int((int)EmailSentOrReceived.ReadDirect)+","
					+POut.Int((int)EmailSentOrReceived.WebMailRecdRead)+","
					+POut.Int((int)EmailSentOrReceived.WebMailReceived)
				+") AND ToAddress='"+POut.String(emailAddressInbox)+"' "
				+"ORDER BY MsgDateTime";
			List<EmailMessage> retVal=Crud.EmailMessageCrud.SelectMany(command);
			for(int i=0;i<retVal.Count;i++) {
				command="SELECT * FROM emailattach WHERE EmailMessageNum = "+POut.Long(retVal[i].EmailMessageNum);
				retVal[i].Attachments=Crud.EmailAttachCrud.SelectMany(command);
			}
			return retVal;
		}

		///<summary></summary>
		public static void Update(EmailMessage message){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),message);
				return;
			}
			Crud.EmailMessageCrud.Update(message);
			//now, delete all attachments and recreate.
			string command="DELETE FROM emailattach WHERE EmailMessageNum="+POut.Long(message.EmailMessageNum);
			Db.NonQ(command);
			for(int i=0;i<message.Attachments.Count;i++) {
				message.Attachments[i].EmailMessageNum=message.EmailMessageNum;
				EmailAttaches.Insert(message.Attachments[i]);
			}
		}

		///<summary>Updates SentOrReceived and saves changes to db.  Better than using Update(), because does not delete and add attachments back into db.</summary>
		public static void UpdateSentOrReceivedRead(EmailMessage emailMessage) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),emailMessage);
				return;
			}
			EmailSentOrReceived sentOrReceived=emailMessage.SentOrReceived;
			if(emailMessage.SentOrReceived==EmailSentOrReceived.Received) {
				sentOrReceived=EmailSentOrReceived.Read;
			}
			else if(emailMessage.SentOrReceived==EmailSentOrReceived.WebMailReceived) {
				sentOrReceived=EmailSentOrReceived.WebMailRecdRead;
			}
			else if(emailMessage.SentOrReceived==EmailSentOrReceived.ReceivedDirect) {
				sentOrReceived=EmailSentOrReceived.ReadDirect;
			}
			if(sentOrReceived==emailMessage.SentOrReceived) {
				return;//Nothing to do.
			}
			string command="UPDATE emailmessage SET SentOrReceived="+POut.Int((int)sentOrReceived)+" WHERE EmailMessageNum="+POut.Long(emailMessage.EmailMessageNum);
			Db.NonQ(command);
		}

		///<summary>Updates SentOrReceived and saves changes to db.  Better than using Update(), because does not delete and add attachments back into db.</summary>
		public static void UpdateSentOrReceivedUnread(EmailMessage emailMessage) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),emailMessage);
				return;
			}
			EmailSentOrReceived sentOrReceived=emailMessage.SentOrReceived;
			if(emailMessage.SentOrReceived==EmailSentOrReceived.Read) {
				sentOrReceived=EmailSentOrReceived.Received;
			}
			else if(emailMessage.SentOrReceived==EmailSentOrReceived.WebMailRecdRead) {
				sentOrReceived=EmailSentOrReceived.WebMailReceived;
			}
			else if(emailMessage.SentOrReceived==EmailSentOrReceived.ReadDirect) {
				sentOrReceived=EmailSentOrReceived.ReceivedDirect;
			}
			if(sentOrReceived==emailMessage.SentOrReceived) {
				return;//Nothing to do.
			}
			string command="UPDATE emailmessage SET SentOrReceived="+POut.Int((int)sentOrReceived)+" WHERE EmailMessageNum="+POut.Long(emailMessage.EmailMessageNum);
			Db.NonQ(command);
		}

		///<summary>Updates SentOrReceived and saves changes to db.  Better than using Update(), because does not delete and add attachments back into db.</summary>
		public static void UpdatePatNum(EmailMessage emailMessage) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),emailMessage);
				return;
			}
			string command="UPDATE emailmessage SET PatNum="+POut.Long(emailMessage.PatNum)+" WHERE EmailMessageNum="+POut.Long(emailMessage.EmailMessageNum);
			Db.NonQ(command);
			if(emailMessage.Attachments==null) {
				return;
			}
			for(int i=0;i<emailMessage.Attachments.Count;i++) {
				EhrSummaryCcd ehrSummaryCcd=EhrSummaryCcds.GetOneForEmailAttach(emailMessage.Attachments[i].EmailAttachNum);
				if(ehrSummaryCcd!=null) {
					ehrSummaryCcd.PatNum=emailMessage.PatNum;
					EhrSummaryCcds.Update(ehrSummaryCcd);
				}
			}
		}

		///<summary></summary>
		public static long Insert(EmailMessage message) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				message.EmailMessageNum=Meth.GetLong(MethodBase.GetCurrentMethod(),message);
				return message.EmailMessageNum;
			}
			Crud.EmailMessageCrud.Insert(message);
			//now, insert all the attaches.
			for(int i=0;i<message.Attachments.Count;i++) {
				message.Attachments[i].EmailMessageNum=message.EmailMessageNum;
				EmailAttaches.Insert(message.Attachments[i]);
			}
			return message.EmailMessageNum;
		}

		///<summary></summary>
		public static void Delete(EmailMessage message){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),message);
				return;
			}
			if(message.EmailMessageNum==0){
				return;//this prevents deletion of all commlog entries of something goes wrong.
			}
			string command="DELETE FROM emailmessage WHERE EmailMessageNum="+POut.Long(message.EmailMessageNum);
			Db.NonQ(command);
		}

		#region Sending

		///<summary>Encrypts the message, verifies trust, locates the public encryption key for the To address (if already stored locally), etc.  emailMessage.  
		///Use this polymorphism when the attachments have already been saved to the email attachments folder in file form.  patNum can be 0.
		///Returns an empty string upon success, or an error string if there were errors.  It is possible that the email was sent to some trusted recipients and not sent to untrusted recipients (in which case there would be errors but some recipients would receive successfully).</summary>
		public static string SendEmailDirect(EmailMessage emailMessage,EmailAddress emailAddressFrom) {
			emailMessage.FromAddress=emailAddressFrom.EmailUsername;//Cannot be emailAddressFrom.SenderAddress, or else will not find the correct encryption certificate.  Used in ConvertEmailMessageToMessage().
			//Start by converting the emailMessage to an unencrypted message using the Direct libraries. The email must be in this form to carry out encryption.
			Health.Direct.Common.Mail.Message msgUnencrypted=ConvertEmailMessageToMessage(emailMessage,true);
			Health.Direct.Agent.MessageEnvelope msgEnvelopeUnencrypted=new Health.Direct.Agent.MessageEnvelope(msgUnencrypted);
			Health.Direct.Agent.OutgoingMessage outMsgUnencrypted=new Health.Direct.Agent.OutgoingMessage(msgEnvelopeUnencrypted);
			string strErrors=SendEmailDirect(outMsgUnencrypted,emailAddressFrom);
			return strErrors;
		}
		///<summary>outMsgDirect must be unencrypted, because this function will encrypt.  Encrypts the message, verifies trust, locates the public encryption key for the To address (if already stored locally), etc.
		///patNum can be zero.  emailSentOrReceived must be either SentDirect or a Direct Ack type such as AckDirectProcessed.
		///Returns an empty string upon success, or an error string if there were errors.  It is possible that the email was sent to some trusted recipients and not sent to untrusted recipients (in which case there would be errors but some recipients would receive successfully).</summary>
		private static string SendEmailDirect(Health.Direct.Agent.OutgoingMessage outMsgUnencrypted,EmailAddress emailAddressFrom) {
			string strErrors="";
			string strSenderAddress=emailAddressFrom.EmailUsername;//Cannot be emailAddressFrom.SenderAddress, or else will not find the right encryption certificate.
			Health.Direct.Agent.DirectAgent directAgent=GetDirectAgentForEmailAddress(strSenderAddress);
			//Locate or discover public certificates for each receiver for encryption purposes.
			for(int i=0;i<outMsgUnencrypted.Recipients.Count;i++) {
				if(outMsgUnencrypted.Recipients[i].Certificates!=null) {
					continue;//The certificate(s) for this recipient were already located somehow. Skip.
				}
				try {
					int certNewCount=FindPublicCertForAddress(outMsgUnencrypted.Recipients[i].Address);
					if(certNewCount!=0) {//If the certificate is already in the local public store or if one was discovered over the internet.
						string strSenderDomain=strSenderAddress.Substring(strSenderAddress.IndexOf("@")+1);//For example, if strSenderAddress is ehr@opendental.com, then this will be opendental.com
						//Refresh the directAgent class using the updated list of public certs while leaving everything else alone. This must be done, or else the certificate will not be found when encrypting the outgoing email.
						directAgent=new Health.Direct.Agent.DirectAgent(strSenderDomain,directAgent.PrivateCertResolver,Health.Direct.Common.Certificates.SystemX509Store.OpenExternal().CreateResolver(),directAgent.TrustAnchors);
						directAgent.EncryptMessages=true;
						HashDirectAgents[strSenderDomain]=directAgent;
					}
				}
				catch(Exception ex) {
					if(strErrors!="") {
						strErrors+="\r\n";
					}
					strErrors+=ex.Message;
				}
			}
			Health.Direct.Agent.OutgoingMessage outMsgEncrypted=null;
			try {
				outMsgEncrypted=directAgent.ProcessOutgoing(outMsgUnencrypted);//This is where encryption and trust verification occurs.
			}
			catch(Exception ex) {
				if(strErrors!="") {
					strErrors+="\r\n";
				}
				strErrors+=ex.Message;
				return strErrors;//Cannot recover from an encryption error.
			}
			EmailMessage emailMessageEncrypted=ConvertMessageToEmailMessage(outMsgEncrypted.Message,false);//No point in saving the encrypted attachment, because nobody can read it and it will bloat the OpenDentImages folder.
			NameValueCollection nameValueCollectionHeaders=new NameValueCollection();
			for(int i=0;i<outMsgEncrypted.Message.Headers.Count;i++) {
				nameValueCollectionHeaders.Add(outMsgEncrypted.Message.Headers[i].Name,outMsgEncrypted.Message.Headers[i].ValueRaw);
			}
			byte[] arrayEncryptedBody=Encoding.UTF8.GetBytes(outMsgEncrypted.Message.Body.Text);//The bytes of the encrypted and base 64 encoded body string.
			MemoryStream ms=new MemoryStream(arrayEncryptedBody);
			ms.Position=0;
			//The memory stream for the alternate view must be mime (not an entire email), based on AlternateView use example http://msdn.microsoft.com/en-us/library/system.net.mail.mailmessage.alternateviews.aspx
			AlternateView alternateView=new AlternateView(ms,outMsgEncrypted.Message.ContentType);//Causes the receiver to recognize this email as an encrypted email.
			alternateView.TransferEncoding=TransferEncoding.SevenBit;
			SendEmailUnsecure(emailMessageEncrypted,emailAddressFrom,nameValueCollectionHeaders,alternateView);//Not really unsecure in this spot, because the message is already encrypted.
			ms.Dispose();
			return strErrors;
		}

		/// <summary>Used for sending encrypted Message Disposition Notification (MDN) ack messages for Direct.
		/// An ack must be sent when a message is received, and other acks must be sent when other events occur (for example, when the user reads a decrypted message we must send an ack with notification type of Displayed).</summary>
		private static string SendAckDirect(Health.Direct.Agent.IncomingMessage inMsg,EmailAddress emailAddressFrom,long patNum,EmailSentOrReceived emailSentOrReceivedAck) {
			//The CreateAcks() function handles the case where the incoming message is an MDN, in which case we do not reply with anything.
			//The CreateAcks() function also takes care of figuring out where to send the MDN, because the rules are complicated.
			//According to http://wiki.directproject.org/Applicability+Statement+for+Secure+Health+Transport+Working+Version#x3.0%20Message%20Disposition%20Notification,
			//The MDN must be sent to the first available of: Disposition-Notification-To header, MAIL FROM SMTP command, Sender header, From header.
			Health.Direct.Common.Mail.Notifications.MDNStandard.NotificationType notificationType=Health.Direct.Common.Mail.Notifications.MDNStandard.NotificationType.Failed;
			if(emailSentOrReceivedAck==EmailSentOrReceived.AckDirectProcessed) {
				notificationType=Health.Direct.Common.Mail.Notifications.MDNStandard.NotificationType.Processed;
			}
			IEnumerable<Health.Direct.Common.Mail.Notifications.NotificationMessage> notificationMsgs=inMsg.CreateAcks("OpenDental","",notificationType);
			if(notificationMsgs==null) {
				return "";
			}
			string strErrorsAll="";
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
					if(notificationMsg.ToValue.Trim().ToLower()==notificationMsg.FromValue.Trim().ToLower()) {
						continue;//Do not send an ack to self.
					}
					string strErrors=SendEmailDirect(outMsgDirect,emailAddressFrom);//encryption is performed in this step
					if(strErrors=="") {
						EmailMessage emailMessage=ConvertMessageToEmailMessage(outMsgDirect.Message,false);
						emailMessage.PatNum=patNum;
						emailMessage.SentOrReceived=emailSentOrReceivedAck;
						Insert(emailMessage);
					}
					else {
						if(strErrorsAll!="") {
							strErrorsAll+="\r\n";
						}
						strErrorsAll+=strErrors;
					}
				}
				catch {
					//Nothing to do. Just an MDN. The sender can resend the email to us if they believe that we did not receive the message (due to lack of MDN response).
				}
			}
			return strErrorsAll;
		}

		/// <summary>This is used from wherever email needs to be sent throughout the program.  If a message must be encrypted, then encrypt it before calling this function.  nameValueCollectionHeaders can be null.</summary>
		private static void SendEmailUnsecure(EmailMessage emailMessage,EmailAddress emailAddress,NameValueCollection nameValueCollectionHeaders,params AlternateView[] arrayAlternateViews) {
			if(emailAddress.ServerPort==465) {//implicit
				//uses System.Web.Mail, which is marked as deprecated, but still supports implicit
				System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver",emailAddress.SMTPserver);
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport","465");
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing","2");//sendusing: cdoSendUsingPort, value 2, for sending the message using the network.
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate","1");//0=anonymous,1=clear text auth,2=context
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername",emailAddress.EmailUsername);
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword",emailAddress.EmailPassword);
				//if(PrefC.GetBool(PrefName.EmailUseSSL)) {
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl","true");//false was also tested and does not work
				message.From=emailMessage.FromAddress;
				message.To=emailMessage.ToAddress;
				message.Subject=emailMessage.Subject;
				message.Body=emailMessage.BodyText;
				//message.Cc=;
				//message.Bcc=;
				//message.UrlContentBase=;
				//message.UrlContentLocation=;
				message.BodyEncoding=System.Text.Encoding.UTF8;
				message.BodyFormat=System.Web.Mail.MailFormat.Text;//or .Html
				if(nameValueCollectionHeaders!=null) {
					string[] arrayHeaderKeys=nameValueCollectionHeaders.AllKeys;
					for(int i=0;i<arrayHeaderKeys.Length;i++) {//Needed for Direct Acks to work.
						message.Headers.Add(arrayHeaderKeys[i],nameValueCollectionHeaders[arrayHeaderKeys[i]]);
					}
				}
				string attachPath=EmailMessages.GetEmailAttachPath();
				System.Web.Mail.MailAttachment attach;
				//foreach (string sSubstr in sAttach.Split(delim)){
				for(int i=0;i<emailMessage.Attachments.Count;i++) {
					attach=new System.Web.Mail.MailAttachment(ODFileUtils.CombinePaths(attachPath,emailMessage.Attachments[i].ActualFileName));
					//no way to set displayed filename
					message.Attachments.Add(attach);
				}
				System.Web.Mail.SmtpMail.SmtpServer=emailAddress.SMTPserver+":465";//"smtp.gmail.com:465";
				System.Web.Mail.SmtpMail.Send(message);
			}
			else {//explicit default port 587 
				SmtpClient client=new SmtpClient(emailAddress.SMTPserver,emailAddress.ServerPort);
				//The default credentials are not used by default, according to: 
				//http://msdn2.microsoft.com/en-us/library/system.net.mail.smtpclient.usedefaultcredentials.aspx
				client.Credentials=new NetworkCredential(emailAddress.EmailUsername,emailAddress.EmailPassword);
				client.DeliveryMethod=SmtpDeliveryMethod.Network;
				client.EnableSsl=emailAddress.UseSSL;
				client.Timeout=180000;//3 minutes
				MailMessage message=new MailMessage();
				Attachment attach;
				message.From=new MailAddress(emailMessage.FromAddress);
				message.To.Add(emailMessage.ToAddress);
				message.Subject=emailMessage.Subject;
				message.Body=emailMessage.BodyText;
				message.IsBodyHtml=false;
				if(nameValueCollectionHeaders!=null) {
					message.Headers.Add(nameValueCollectionHeaders);//Needed for Direct Acks to work.
				}
				for(int i=0;i<arrayAlternateViews.Length;i++) {//Needed for Direct messages to be interpreted encrypted on the receiver's end.
					message.AlternateViews.Add(arrayAlternateViews[i]);
				}
				string attachPath=EmailMessages.GetEmailAttachPath();
				for(int i=0;i<emailMessage.Attachments.Count;i++) {
					attach=new Attachment(ODFileUtils.CombinePaths(attachPath,emailMessage.Attachments[i].ActualFileName));
					//@"C:\OpenDentalData\EmailAttachments\1");
					attach.Name=emailMessage.Attachments[i].DisplayedFileName;
					//"canadian.gif";
					message.Attachments.Add(attach);
				}
				client.Send(message);
			}
		}

		/// <summary>This is used from wherever unencrypted email needs to be sent throughout the program.  If a message must be encrypted, then encrypt it before calling this function.</summary>
		public static void SendEmailUnsecure(EmailMessage emailMessage,EmailAddress emailAddress) {
			SendEmailUnsecure(emailMessage,emailAddress,null);
		}

		#endregion Sending

		#region Receiving

		///<summary>Fetches up to fetchCount number of messages from a POP3 server.  Set fetchCount=0 for all messages.  Typically, fetchCount is 0 or 1.
		///Example host name, pop3.live.com. Port is Normally 110 for plain POP3, 995 for SSL POP3.  Does not currently fetch attachments.</summary>
		public static List<EmailMessage> ReceiveFromInbox(int receiveCount,EmailAddress emailAddressInbox) {
			List<EmailMessage> retVal=new List<EmailMessage>();
			//This code is modified from the example at: http://hpop.sourceforge.net/exampleFetchAllMessages.php
			using(OpenPop.Pop3.Pop3Client client=new OpenPop.Pop3.Pop3Client()) {//The client disconnects from the server when being disposed.
				client.Connect(emailAddressInbox.Pop3ServerIncoming,emailAddressInbox.ServerPortIncoming,emailAddressInbox.UseSSL);
				client.Authenticate(emailAddressInbox.EmailUsername,emailAddressInbox.EmailPassword,OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);
				int messageCount=client.GetMessageCount();//Get the number of messages in the inbox.
				int msgDownloadedCount=0;
				for(int i=messageCount;i>0;i--) {//Message numbers are 1-based. Most servers give the newest message the highest number.
					try {
						OpenPop.Mime.Message openPopMsg=client.GetMessage(i);
						string strRawEmail=openPopMsg.MessagePart.BodyEncoding.GetString(openPopMsg.RawMessage);
						EmailMessage emailMessage=ProcessRawEmailMessage(strRawEmail,0,emailAddressInbox);
						retVal.Add(emailMessage);
						msgDownloadedCount++;
						client.DeleteMessage(i);//Only delete from server after successfully downloaded and stored into db.
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

		#endregion Receiving

		#region Helpers

		private static Health.Direct.Agent.DirectAgent GetDirectAgentForEmailAddress(string strEmailAddress) {
			string domain=strEmailAddress.Substring(strEmailAddress.IndexOf("@")+1);//For example, if ToAddress is ehr@opendental.com, then this will be opendental.com
			Health.Direct.Agent.DirectAgent directAgent=(Health.Direct.Agent.DirectAgent)HashDirectAgents[domain];
			if(directAgent==null) {
				try {
					directAgent=new Health.Direct.Agent.DirectAgent(domain);
				}
				catch(Exception ex) {
					if(ex.Message.Contains("cannot find the file specified")) {//A typical exception when the 3 required certificate stores needed for Direct have not been created on one particular client machine.
						Health.Direct.Common.Certificates.SystemX509Store.OpenAnchorEdit().Dispose();//Create the NHINDAnchor certificate store if it does not already exist on the local machine.
						Health.Direct.Common.Certificates.SystemX509Store.OpenExternalEdit().Dispose();//Create the NHINDExternal certificate store if it does not already exist on the local machine.
						Health.Direct.Common.Certificates.SystemX509Store.OpenPrivateEdit().Dispose();//Create the NHINDPrivate certificate store if it does not already exist on the local machine.
						directAgent=new Health.Direct.Agent.DirectAgent(domain);
					}
				}
				directAgent.EncryptMessages=true;
				HashDirectAgents[domain]=directAgent;
			}
			return directAgent;
		}

		public static bool IsDirectAddressTrusted(string strAddressTest) {
			Health.Direct.Common.Certificates.SystemX509Store storeAnchors=Health.Direct.Common.Certificates.SystemX509Store.OpenAnchorEdit();//Open for read and write.  Corresponds to NHINDAnchors/Certificates.
			if(GetValidCertForAddressFromStore(storeAnchors,strAddressTest,false)==null) {//Look for domain level and address level trust certificates (anchors).
				return false;//None found.
			}
			return true;
		}

		public static void TryAddTrustDirect(string strAddressTest) {
			if(IsDirectAddressTrusted(strAddressTest)) {
				return;//Already trusted.
			}
			try {
				if(FindPublicCertForAddress(strAddressTest)==0) {//Could not find certificate.
					return;//Cannot trust because we need the certificate to trust.
				}
			}
			catch {
				return;//Possibly a network failure.
			}
			Health.Direct.Common.Certificates.SystemX509Store storePublicCerts=Health.Direct.Common.Certificates.SystemX509Store.OpenExternalEdit();//Open for read and write.  Corresponds to NHINDExternal/Certificates.
			X509Certificate2 cert=GetValidCertForAddressFromStore(storePublicCerts,strAddressTest,false);
			if(cert==null) {
				return;//Should never happen, but just in case.
			}
			Health.Direct.Common.Certificates.SystemX509Store storeAnchors=Health.Direct.Common.Certificates.SystemX509Store.OpenAnchorEdit();//Open for read and write.  Corresponds to NHINDAnchors/Certificates.
			storeAnchors.Add(cert);//Adds to NHINDAnchors/Certificates within the windows certificate store manager (mmc).
			//Clear all cached DirectAgent instances to force trust anchors to reload.
			HashDirectAgents.Clear();
		}

		///<summary>Set isAddressSpecific if you need to allow/prefer domain certificates over email address specific certificates.</summary>
		private static X509Certificate2 GetValidCertForAddressFromStore(Health.Direct.Common.Certificates.SystemX509Store store,string strAddressTest,bool isAddressSpecific) {
			X509Certificate2Collection collectionCerts=null;
			MailAddress mailAddressQuery=new MailAddress(strAddressTest);
			Health.Direct.Common.Certificates.ICertificateResolver certResolverLocalCache=store.CreateResolver();
			if(certResolverLocalCache==null) {
				return null;
			}
			collectionCerts=certResolverLocalCache.GetCertificates(mailAddressQuery);
			if(collectionCerts==null) {
				return null;
			}
			List<X509Certificate2> listDomainCerts=new List<X509Certificate2>();
			List<X509Certificate2> listAddressCerts=new List<X509Certificate2>();
			for(int i=0;i<collectionCerts.Count;i++) {
				if(DateTime.Now<collectionCerts[i].NotBefore || DateTime.Now>collectionCerts[i].NotAfter) {
					//If the certificate is not yet valid or is expired, then ignore.
					continue;
				}
				string strCertSubjectName=collectionCerts[i].Subject.Trim().ToLower();
				if(strCertSubjectName.Contains("e="+strAddressTest.ToLower())) {//Address specific
					listAddressCerts.Add(collectionCerts[i]);
				}
				else {
					listDomainCerts.Add(collectionCerts[i]);
				}
			}
			if(!isAddressSpecific && listDomainCerts.Count>0) {//Domain certificates allowed/preferred and there is one.
				return listDomainCerts[0];
			}
			if(listAddressCerts.Count>0) {
				return listAddressCerts[0];
			}
			//A certificate was found in the local store, but it was a domain level certificate and was not for the specific address provided.
			return null;
		}

		///<summary>First attemtps to find the public certificate for the provided address in the public certificate store and returns the located certificate if found.
		///If the public certificate could not be found from the public certificate store, then this function will search the internet for the hosted public certificate.
		///If a public certificate is discovered from the Internet, then it will be added to the public certificate store, but the trust for any certificate must be added separately.
		///Returns the number of new public certificates discovered (0,1,or 2 (if one for the address and domain separately)), or -1 if the certificate is already in the local store of public certificates.
		///Throws exceptions when no certificates were found or if there was a network failure.</summary>
		private static int FindPublicCertForAddress(string strAddressTest) {
			Health.Direct.Common.Certificates.SystemX509Store storePublicCerts=Health.Direct.Common.Certificates.SystemX509Store.OpenExternalEdit();//Open for read and write.  Corresponds to NHINDExternal/Certificates.
			if(GetValidCertForAddressFromStore(storePublicCerts,strAddressTest,true)!=null) {//Address specific (excludes domain level certificates).
				return -1;//The certificate was found in the local certificate store within Windows and is already loaded into memory for the specific recipient address given.  No need to query the Internet.
			}
			//Cert not found locally.  Attempt to discover the certificate for the exact recipient address provided (below) before using a domain level certificate.
			//An address specific certificate was not found in the local certificate store.  Attempt to discover an address specific certificate by querying the Internet.
			//It may be useful in the future to attempt communicating with a secondary DNS server if the primary DNS is not available.
			//const string strDnsServer = "184.73.237.102";//Amazon - This is the DNS server used within the Direct resolverPlugins test project. Appears to have worked the best for them, compared to the others listed below, but was not accessible.
			//const string strDnsServer = "10.110.22.16";//This address was tried in the Direct resolverPlugins test project and is commented out, implying that it might not be the best DNS server to use.
			//const string strDnsServer = "207.170.210.162";//This address was tried in the Direct resolverPlugins test project and is commented out, implying that it might not be the best DNS server to use.
			const string strGlobalDnsServer = "8.8.8.8";//Google - This address was tried in the Direct resolverPlugins test project and is commented out, implying that it might not be the best DNS server to use.
			IPAddress ipAddressGlobalDnsServer=IPAddress.Parse(strGlobalDnsServer);
			MailAddress mailAddressQuery=new MailAddress(strAddressTest);
			//Attempt to discover the certificate via DNS.
			Health.Direct.Common.Certificates.ICertificateResolver certResolverInternetDns=new Health.Direct.Common.Certificates.DnsCertResolver(ipAddressGlobalDnsServer);
			X509Certificate2Collection collectionCerts=certResolverInternetDns.GetCertificates(mailAddressQuery);//Can return null.
			List<X509Certificate2> listCertsDiscoveredActive=new List<X509Certificate2>();
			List<X509Certificate2> listCertsDiscoveredInactive=new List<X509Certificate2>();
			if(collectionCerts!=null) {//Certificates found via DNS. Remove any invalid or expired certificates.
				for(int i=0;i<collectionCerts.Count;i++) {
					if(DateTime.Now<collectionCerts[i].NotBefore || DateTime.Now>collectionCerts[i].NotAfter) {
						//If the certificate is not yet valid or is expired, then discard so we can possibly discover a better certificate below.
						listCertsDiscoveredInactive.Add(collectionCerts[i]);
						continue;
					}
					listCertsDiscoveredActive.Add(collectionCerts[i]);
				}
			}
			if(listCertsDiscoveredActive.Count==0) {//A valid certificate was not found via DNS.  Attempt to locate via LDAP.
				Health.Direct.Common.Certificates.ICertificateResolver certResolverInternetLdap=new Health.Direct.ResolverPlugins.LdapCertResolver(ipAddressGlobalDnsServer,TimeSpan.FromMinutes(3));
				collectionCerts=certResolverInternetLdap.GetCertificates(mailAddressQuery);//Can return null.
				if(collectionCerts!=null) {
					for(int i=0;i<collectionCerts.Count;i++) {
						if(DateTime.Now<collectionCerts[i].NotBefore || DateTime.Now>collectionCerts[i].NotAfter) {
							//If the certificate is not yet valid or is expired, then discard.
							listCertsDiscoveredInactive.Add(collectionCerts[i]);
							continue;
						}
						listCertsDiscoveredActive.Add(collectionCerts[i]);
					}
				}
			}
			if(listCertsDiscoveredActive.Count==0) { //A certificate was not discovered via DNS or LDAP.
				string strErrorMessage=Lans.g("EmailMessages","No active certificates discovered for recipient")+" "+strAddressTest;
				if(listCertsDiscoveredInactive.Count>0) {
					strErrorMessage+="\r\n"+Lans.g("EmailMessages","Inactive certificates discovered")+": "+listCertsDiscoveredInactive.Count;
				}
				throw new ApplicationException(strErrorMessage);
			}
			//A certificate was discovered via DNS or LDAP.  Save it locally for later reference.
			storePublicCerts.Add(listCertsDiscoveredActive);//Write the discovered certificate to the Windows certificate store for future reference.
			return listCertsDiscoveredActive.Count;
		}

		///<summary>Converts any raw email message (encrypted or not) into an EmailMessage object, and saves any email attachments to the emailattach table in the db.
		///The emailMessageNum will be used to set EmailMessage.EmailMessageNum.  If emailMessageNum is 0, then the EmailMessage will be inserted into the db, otherwise the EmailMessage will be updated in the db.
		///If the raw message is encrypted, then will attempt to decrypt.  If decryption fails, then the EmailMessage SentOrReceived will be ReceivedEncrypted and the EmailMessage body will be set to the entire contents of the raw email.  If decryption succeeds, then EmailMessage SentOrReceived will be set to ReceivedDirect, the EmailMessage body will contain the decrypted body text, and a Direct Ack "processed" message will be sent back to the sender using the email settings from emailAddressReceiver.</summary>
		public static EmailMessage ProcessRawEmailMessage(string strRawEmail,long emailMessageNum,EmailAddress emailAddressReceiver) {
			Health.Direct.Agent.IncomingMessage inMsg=null;
			try {
				inMsg=new Health.Direct.Agent.IncomingMessage(strRawEmail);//Used to parse all email (encrypted or not).
			}
			catch(Exception ex) {
				throw new ApplicationException("Failed to parse raw email message.\r\n"+ex.Message);
			}
			bool isEncrypted=false;
			if(inMsg.Message.ContentType.ToLower().Contains("application/pkcs7-mime")) {//The email MIME/body is encrypted (known as S/MIME). Treated as a Direct message.
				isEncrypted=true;
			}
			EmailMessage emailMessage=null;
			if(isEncrypted) {
				emailMessage=ConvertMessageToEmailMessage(inMsg.Message,false);//Exclude attachments until we decrypt.
				emailMessage.EmailMessageNum=emailMessageNum;
				emailMessage.SentOrReceived=EmailSentOrReceived.ReceivedEncrypted;
				//The entire contents of the email are saved in the emailMessage.BodyText field, so that if decryption fails, the email will still be saved to the db for decryption later if possible.
				emailMessage.BodyText=strRawEmail;
				try {
					Health.Direct.Agent.DirectAgent directAgent=GetDirectAgentForEmailAddress(inMsg.Message.ToValue);
					//throw new ApplicationException("test decryption failure");
					inMsg=directAgent.ProcessIncoming(inMsg);//Decrypts, valudates trust, etc.
					emailMessage=ConvertMessageToEmailMessage(inMsg.Message,true);//If the message was wrapped, then the To, From, Subject and Date can change after decyption. We also need to create the attachments for the decrypted message.
					emailMessage.EmailMessageNum=emailMessageNum;
					emailMessage.SentOrReceived=EmailSentOrReceived.ReceivedDirect;
				}
				catch(Exception ex) {
					//SentOrReceived will be ReceivedEncrypted, indicating to the calling code that decryption failed.
					if(emailMessageNum==0) {
						EmailMessages.Insert(emailMessage);
						return emailMessage;//If the message was just downloaded, then this function was called from the inbox, simply return the inserted email without an exception (it can be decypted later manually by the user).
					}
					//Do not update if emailMessageNum<>0, because nothing changed (was encrypted and still is).
					throw ex;//Throw an exception if trying to decrypt an email that was already in the database, so the user can see the error message in the UI.
				}
			}
			else {//Unencrypted
				emailMessage=ConvertMessageToEmailMessage(inMsg.Message,true);
				emailMessage.EmailMessageNum=emailMessageNum;
				emailMessage.SentOrReceived=EmailSentOrReceived.Received;
			}
			EhrSummaryCcd ehrSummaryCcd=null;
			if(isEncrypted) {
				for(int i=0;i<emailMessage.Attachments.Count;i++) {
					if(Path.GetExtension(emailMessage.Attachments[i].ActualFileName).ToLower()!=".xml") {
						continue;
					}
					string strAttachPath=GetEmailAttachPath();
					string strAttachFile=ODFileUtils.CombinePaths(strAttachPath,emailMessage.Attachments[i].ActualFileName);
					string strAttachText=File.ReadAllText(strAttachFile);
					if(EhrCCD.IsCCD(strAttachText)) {
						if(emailMessage.PatNum==0) {
							try {
								XmlDocument xmlDocCcd=new XmlDocument();
								xmlDocCcd.LoadXml(strAttachText);
								emailMessage.PatNum=EhrCCD.GetCCDpat(xmlDocCcd);// A match is not guaranteed, which is why we have a button to allow the user to change the patient.
							}
							catch {
								//Invalid XML.  Cannot match patient.
							}
						}
						ehrSummaryCcd=new EhrSummaryCcd();
						ehrSummaryCcd.ContentSummary=strAttachText;
						ehrSummaryCcd.DateSummary=DateTime.Today;
						ehrSummaryCcd.EmailAttachNum=i;//Temporary value, so we can locate the FK down below.
						ehrSummaryCcd.PatNum=emailMessage.PatNum;
						break;//We can only handle one CCD message per email, because we only have one patnum field per email record and the ehrsummaryccd record requires a patnum.
					}
				}
			}
			if(emailMessageNum==0) {
				EmailMessages.Insert(emailMessage);//Also inserts all of the attachments in emailMessage.Attachments after setting each attachment EmailMessageNum properly.
			}
			else {
				EmailMessages.Update(emailMessage);//Also deletes all previous attachments, then recreates all of the attachments in emailMessage.Attachments after setting each attachment EmailMessageNum properly.
			}
			if(ehrSummaryCcd!=null) {
				ehrSummaryCcd.EmailAttachNum=emailMessage.Attachments[(int)ehrSummaryCcd.EmailAttachNum].EmailAttachNum;
				EhrSummaryCcds.Insert(ehrSummaryCcd);
			}
			if(isEncrypted) {
				//Send a Message Disposition Notification (MDN) message to the sender, as required by the Direct messaging specifications.
				//The MDN will be attached to the same patient as the incoming message.
				string strErrors=SendAckDirect(inMsg,emailAddressReceiver,emailMessage.PatNum,EmailSentOrReceived.AckDirectProcessed);
				if(strErrors!="") {
					//This could happen if a message is received from an untrusted source who is not hosting their certificate for discovery (probably a temporary technical issue).
					//Even if an error happens here, we should either ignore or at best log an error message.  Not worth mentioning to user, since this is just an ack.
				}
			}
			return emailMessage;
		}

		///<summary>Converts the Health.Direct.Common.Mail.Message into an OD EmailMessage.  The Direct library is used for both encrypted and unencrypted email.  Set hasAttachments to false to exclude attachments.</summary>
		private static EmailMessage ConvertMessageToEmailMessage(Health.Direct.Common.Mail.Message message,bool hasAttachments) {
			EmailMessage emailMessage=new EmailMessage();
			emailMessage.FromAddress=message.FromValue;
			emailMessage.MsgDateTime=DateTime.Now;
			emailMessage.Subject=message.SubjectValue;
			emailMessage.ToAddress=message.ToValue;
			List<Health.Direct.Common.Mime.MimeEntity> listMimeParts=new List<Health.Direct.Common.Mime.MimeEntity>();//We want to treat one part and multiple part emails the same way below, so we make our own list.  If GetParts() is called when IsMultiPart is false, then an exception will be thrown by the Direct library.
			Health.Direct.Common.Mime.MimeEntity mimeEntity=null;
			try {
				mimeEntity=message.ExtractMimeEntity();
			}
			catch {
				emailMessage.BodyText=message.Body.Text;
				return emailMessage;
			}
			if(message.IsMultiPart) {
				foreach(Health.Direct.Common.Mime.MimeEntity mimePart in mimeEntity.GetParts()) {
					listMimeParts.Add(mimePart);
				}
			}
			else {//Single body part.
				listMimeParts.Add(mimeEntity);
			}
			List<Health.Direct.Common.Mime.MimeEntity> listMimeBodyTextParts=new List<Health.Direct.Common.Mime.MimeEntity>();
			List<Health.Direct.Common.Mime.MimeEntity> listMimeAttachParts=new List<Health.Direct.Common.Mime.MimeEntity>();
			for(int i=0;i<listMimeParts.Count;i++) {
				Health.Direct.Common.Mime.MimeEntity mimePart=listMimeParts[i];
				if(mimePart.ContentDisposition==null || !mimePart.ContentDisposition.ToLower().Contains("attachment")) {//Not an email attachment.  Treat as body text.
					listMimeBodyTextParts.Add(mimePart);
				}
				else {
					listMimeAttachParts.Add(mimePart);
				}
			}
			string strTextPartBoundary="";
			if(listMimeBodyTextParts.Count>1) {
				strTextPartBoundary=message.ParsedContentType.Boundary;
			}
			StringBuilder sbBodyText=new StringBuilder();
			for(int i=0;i<listMimeBodyTextParts.Count;i++) {
				if(strTextPartBoundary!="") {//For incoming Direct Ack messages.
					sbBodyText.Append("\r\n--"+strTextPartBoundary+"\r\n");
					sbBodyText.Append(listMimeBodyTextParts[i].ToString());//Includes not only the body text, but also content type and content disposition.
				}
				else {
					sbBodyText.Append(listMimeBodyTextParts[i].Body.Text);
				}
			}
			if(strTextPartBoundary!="") {
				sbBodyText.Append("\r\n--"+strTextPartBoundary+"--\r\n");
			}
			emailMessage.BodyText=sbBodyText.ToString();
			emailMessage.Attachments=new List<EmailAttach>();
			if(!hasAttachments) {
				return emailMessage;
			}
			for(int i=0;i<listMimeAttachParts.Count;i++) {
				Health.Direct.Common.Mime.MimeEntity mimePartAttach=listMimeAttachParts[i];
				string strAttachText=mimePartAttach.Body.Text;
				try {
					if(mimePartAttach.ContentTransferEncoding.ToLower().Contains("base64")) {
						strAttachText=Encoding.UTF8.GetString(Convert.FromBase64String(mimePartAttach.Body.Text));
					}
				}
				catch {
				}
				EmailAttach emailAttach=CreateAttachInAttachPath(mimePartAttach.ParsedContentType.Name,strAttachText);
				if(mimePartAttach.ParsedContentType.Name.ToLower()=="smime.p7m") {//encrypted attachment
					message.ContentType="application/pkcs7-mime; name=smime.p7m; boundary="+strTextPartBoundary+";";
				}
				emailMessage.Attachments.Add(emailAttach);//The attachment EmailMessageNum is set when the emailMessage is inserted/updated below.					
			}
			return emailMessage;
		}

		private static Health.Direct.Common.Mail.Message ConvertEmailMessageToMessage(EmailMessage emailMessage,bool hasAttachments) {
			//We need to use emailAddressFrom.Username instead of emailAddressFrom.SenderAddress, because of how strict encryption is for matching the name to the certificate.
			Health.Direct.Common.Mail.Message message=new Health.Direct.Common.Mail.Message(emailMessage.ToAddress,emailMessage.FromAddress);
			if(emailMessage.Subject!="") {
				Health.Direct.Common.Mime.Header headerSubject=new Health.Direct.Common.Mime.Header("Subject",emailMessage.Subject);
				message.Headers.Add(headerSubject);
			}
			string strBoundry="";
			List<Health.Direct.Common.Mime.MimeEntity> listMimeParts=new List<Health.Direct.Common.Mime.MimeEntity>();
			if(emailMessage.BodyText.Trim().Length>4 && emailMessage.BodyText.Trim().StartsWith("--") && emailMessage.BodyText.Trim().EndsWith("--")) {//The body text is multi-part.
				strBoundry=emailMessage.BodyText.Trim().Split(new string[] { "\r\n","\r","\n" },StringSplitOptions.None)[0];
				string[] arrayBodyTextParts=emailMessage.BodyText.Trim().TrimEnd('-').Split(new string[] { strBoundry },StringSplitOptions.RemoveEmptyEntries);
				for(int i=0;i<arrayBodyTextParts.Length;i++) {
					Health.Direct.Common.Mime.MimeEntity mimeEntityBodyText=new Health.Direct.Common.Mime.MimeEntity(arrayBodyTextParts[i]);
					mimeEntityBodyText.ContentType="text/plain;";
					listMimeParts.Add(mimeEntityBodyText);
				}
			}
			else {
				Health.Direct.Common.Mime.MimeEntity mimeEntityBodyText=new Health.Direct.Common.Mime.MimeEntity(emailMessage.BodyText);
				mimeEntityBodyText.ContentType="text/plain;";
				listMimeParts.Add(mimeEntityBodyText);
			}
			if(hasAttachments && emailMessage.Attachments!=null && emailMessage.Attachments.Count>0) {
				string strAttachPath=GetEmailAttachPath();
				for(int i=0;i<emailMessage.Attachments.Count;i++) {
					string strAttachFileName=emailMessage.Attachments[i].DisplayedFileName;
					string strAttachFile=ODFileUtils.CombinePaths(strAttachPath,emailMessage.Attachments[i].ActualFileName);
					string strAttachText=File.ReadAllText(strAttachFile);
					Health.Direct.Common.Mime.MimeEntity mimeEntityAttach=new Health.Direct.Common.Mime.MimeEntity(Convert.ToBase64String(Encoding.UTF8.GetBytes(strAttachText)));
					mimeEntityAttach.ContentDisposition="attachment;";
					mimeEntityAttach.ContentTransferEncoding="base64;";
					mimeEntityAttach.ContentType="text/plain; name="+strAttachFileName+";";
					listMimeParts.Add(mimeEntityAttach);
				}
			}
			if(strBoundry=="") {
				strBoundry=CodeBase.MiscUtils.CreateRandomAlphaNumericString(32);
			}
			if(listMimeParts.Count==1) {//Single body part
				message.Body=listMimeParts[0].Body;
			}
			else if(listMimeParts.Count>1) {//multiple body parts
				message.SetParts(listMimeParts,"multipart/mixed; boundary="+strBoundry+";");
			}
			return message;
		}

		///<summary>Creates a new file inside of the email attachment path (inside OpenDentImages) and returns an EmailAttach object referencing the new file, but with EmailMessageNum set to zero so it can be set later.</summary>
		private static EmailAttach CreateAttachInAttachPath(string strAttachFileName,string strAttachText) {
			string strAttachFileNameAdjusted=strAttachFileName;
			if(String.IsNullOrEmpty(strAttachFileName)) {
				strAttachFileNameAdjusted=MiscUtils.CreateRandomAlphaNumericString(8)+".txt";//just in case
			}
			string strAttachPath=GetEmailAttachPath();
			string strAttachFile=ODFileUtils.CombinePaths(strAttachPath,DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+"_"+strAttachFileNameAdjusted);
			while(File.Exists(strAttachFile)) {
				strAttachFile=ODFileUtils.CombinePaths(strAttachPath,DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+"_"+strAttachFileNameAdjusted);
			}
			File.WriteAllText(strAttachFile,strAttachText);
			EmailAttach emailAttach=new EmailAttach();
			emailAttach.ActualFileName=Path.GetFileName(strAttachFile);
			emailAttach.DisplayedFileName=Path.GetFileName(strAttachFileNameAdjusted);//shorter name, excludes date and time stamp info.
			return emailAttach;
		}

		private static string ProcessMimeTextPart(string strBody) {
			//For unencrypted emails from GoDaddy, the body text is html, but each line is wrapped at 75 characters and an extra '=' is appended.
			//Our algorithm to handle the extra equal signs is more generic, in case GoDaddy every changes their wrap character count, or in case other email providers manimulate the email body in a similar manner.
			bool isWrappedEmail=true;
			string[] arrayMimeBodyLines=strBody.Split(new string[] { "\r\n","\r","\n" },StringSplitOptions.None);
			int lastTextLineIndex=arrayMimeBodyLines.Length-1;//GoDaddy emails also have trailing blank lines after the body text, which we need to ignore when determining if the email is wrapped or not.
			while(arrayMimeBodyLines[lastTextLineIndex].Length==0 && lastTextLineIndex>0) {
				lastTextLineIndex--;
			}
			for(int i=0;i<lastTextLineIndex;i++) {//Ignore the last line in this consideration, because it is almost always a shorter line than the wrapped lines.
				if(arrayMimeBodyLines[i].Length<50) {//Why would any email provider wrap an email to less than 50 characters?
					isWrappedEmail=false;
					break;
				}
				if(i>0 && arrayMimeBodyLines[i].Length!=arrayMimeBodyLines[i-1].Length) {//Wrapped email messages have lines of the same length (excluding the last line)
					isWrappedEmail=false;
					break;
				}
				if(!arrayMimeBodyLines[i].EndsWith("=")) {
					isWrappedEmail=false;
					break;
				}
			}
			string retVal=strBody;
			if(isWrappedEmail) {
				StringBuilder sbBodyText=new StringBuilder();
				for(int i=0;i<lastTextLineIndex;i++) {
					sbBodyText.Append(arrayMimeBodyLines[i].Substring(0,arrayMimeBodyLines[i].Length-1));//The whole line, excluding the last character.
				}
				for(int i=lastTextLineIndex;i<arrayMimeBodyLines.Length;i++) {//Copy the the last text line and all trailing blank lines as is.
					sbBodyText.Append(arrayMimeBodyLines[i]);
				}
				retVal=sbBodyText.ToString();
			}
			return retVal;
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

		///<summary>Can throw an exception if there is a permission issue saving the file.</summary>
		public static void CreateAttachmentFromText(EmailMessage emailMessage,string strAttachText,string strDisplayFileName) {
			Random rnd=new Random();
			EmailAttach emailAttach;
			//create the attachment
			emailAttach=new EmailAttach();
			emailAttach.DisplayedFileName=strDisplayFileName;
			emailAttach.ActualFileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+Path.GetExtension(strDisplayFileName);//To make unique.
			string strAttachFilePath=ODFileUtils.CombinePaths(EmailMessages.GetEmailAttachPath(),emailAttach.ActualFileName);
			File.WriteAllText(strAttachFilePath,strAttachText);
			emailMessage.Attachments.Add(emailAttach);
		}

		#endregion Helpers

		#region Testing

		///<summary>This method is only for ehr testing purposes, and it always uses the hidden pref EHREmailToAddress to send to.  For privacy reasons, this cannot be used with production patient info.  AttachName should include extension.</summary>
		public static void SendTestUnsecure(string subjectAndBody,string attachName,string attachContents) {
			SendTestUnsecure(subjectAndBody,attachName,attachContents,"","");
		}

		///<summary>This method is only for ehr testing purposes, and it always uses the hidden pref EHREmailToAddress to send to.  For privacy reasons, this cannot be used with production patient info.  AttachName should include extension.</summary>
		public static void SendTestUnsecure(string subjectAndBody,string attachName1,string attachContents1,string attachName2,string attachContents2) {
			string strTo=PrefC.GetString(PrefName.EHREmailToAddress);
			if(strTo=="") {
				throw new ApplicationException("This feature cannot be used except in a test environment because email is not secure.");
			}
			EmailAddress emailAddressFrom=EmailAddresses.GetByClinic(0);
			EmailMessage emailMessage=new EmailMessage();
			emailMessage.FromAddress=emailAddressFrom.EmailUsername;
			emailMessage.ToAddress=strTo;
			emailMessage.Subject=subjectAndBody;
			emailMessage.BodyText=subjectAndBody;
			if(attachName1!="") {
				EmailAttach emailAttach=CreateAttachInAttachPath(attachName1,attachContents1);
				emailMessage.Attachments.Add(emailAttach);
			}
			if(attachName2!="") {
				EmailAttach emailAttach=CreateAttachInAttachPath(attachName2,attachContents2);
				emailMessage.Attachments.Add(emailAttach);
			}
			SendEmailUnsecure(emailMessage,emailAddressFrom);
			Insert(emailMessage);
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
			List<EmailMessage> emailMessages=ReceiveFromInbox(1,emailAddress);
			if(emailMessages.Count==0) {
				throw new Exception("Inbox empty.");
			}
			EmailMessage emailMessage=emailMessages[0];
			if(emailMessage.Attachments==null || emailMessage.Attachments.Count==0) {
				throw new Exception("No attachments");
			}
			string strAttachFile=ODFileUtils.CombinePaths(GetEmailAttachPath(),emailMessage.Attachments[0].ActualFileName);
			return File.ReadAllText(strAttachFile);
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

		#endregion Testing

		
	}

	
	

}













