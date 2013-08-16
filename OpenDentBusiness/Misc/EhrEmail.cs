using System;
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

namespace OpenDentBusiness {
	public class EhrEmail {
		///<summary>This method is only for ehr testing purposes, and it always uses the hidden pref EHREmailToAddress to send to.  For privacy reasons, this cannot be used with production patient info.  AttachName should include extension.</summary>
		public static void Send(string subjectAndBody,string attachName,string attachContents) {
			Send(subjectAndBody,attachName,attachContents,"","");
		}

		///<summary>This method is only for ehr testing purposes, and it always uses the hidden pref EHREmailToAddress to send to.  For privacy reasons, this cannot be used with production patient info.  AttachName should include extension.</summary>
		public static void Send(string subjectAndBody,string attachName1,string attachContents1,string attachName2,string attachContents2) {
			if(PrefC.GetString(PrefName.EHREmailToAddress)=="") {
				throw new ApplicationException("This feature cannot be used except in a test environment because email is not secure.");
			}
			EmailAddress emailAddress=EmailAddresses.GetByClinic(0);
			SmtpClient client=new SmtpClient(emailAddress.SMTPserver,emailAddress.ServerPort);
			client.Credentials=new NetworkCredential(emailAddress.EmailUsername,emailAddress.EmailPassword);
			client.DeliveryMethod=SmtpDeliveryMethod.Network;
			client.EnableSsl=emailAddress.UseSSL;
			client.Timeout=180000;//Timeout of 3 minutes (in milliseconds).
			MailMessage message=new MailMessage();
			message.From=new MailAddress(emailAddress.SenderAddress);
			message.To.Add(PrefC.GetString(PrefName.EHREmailToAddress));
			message.Subject=subjectAndBody;
			message.Body=subjectAndBody;
			message.IsBodyHtml=false;
			Attachment attach=Attachment.CreateAttachmentFromString(attachContents1,attachName1);
			message.Attachments.Add(attach);
			if(attachContents2!="" && attachName2!="") {
				Attachment attach2=Attachment.CreateAttachmentFromString(attachContents2,attachName2);
				message.Attachments.Add(attach2);
			}
			client.Send(message);
			attach.Dispose();
		}

		///<summary>Receives one email from the inbox.  Will throw an exception if anything goes wrong, so surround with a try-catch.  Never returns null.</summary>
		public static EmailMessage ReceiveFromInbox(EmailAddress emailAddress) {
			EmailMessage retVal=null;
			string Data="";
			bool disconnect=false;
			byte[] sendData;
			//string message="";
			string readData="";
			NetworkStream NetStrm=null;
			StreamReader RdStrm=null;
			string error="";
			try {
				TcpClient Server=new TcpClient(emailAddress.SMTPserverIncoming,emailAddress.ServerPortIncoming);
				NetStrm=Server.GetStream();
				RdStrm=new StreamReader(Server.GetStream());
				disconnect=true;//Always dispose of the streams unless TcpClient fails
				RdStrm.ReadLine();//Will hang if connecting to SSL
				Data="USER "+emailAddress.EmailUsername+"\r\n";
				sendData=System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(sendData,0,sendData.Length);
				RdStrm.ReadLine();//Moves reader position.
				Data="PASS "+emailAddress.EmailPassword+"\r\n";
				sendData=System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(sendData,0,sendData.Length);
				if(!RdStrm.ReadLine().StartsWith("+")) {
					throw new ApplicationException("Username or password incorrect.");
				}
				//Display message
				int lastEmail=1;//Cannot be 0
				Data="STAT \r\n";//Get the newest email recieved.
				sendData=System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(sendData,0,sendData.Length);
				readData=RdStrm.ReadLine();
				if(readData.StartsWith("+OK")) {//eg. "+OK 2 342432", 2 being the message count
					string[] str=readData.Split(' ');
					if(str[1]=="0") {
						throw new ApplicationException("Inbox is empty.");
					}
					lastEmail=PIn.Int(str[1]);
				}
				Data="RETR "+lastEmail+"\r\n";
				sendData=System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(sendData,0,sendData.Length);
				readData=RdStrm.ReadLine();
				if(readData[0]=='-') {//Will be -ERR if something went wrong. 
					throw new ApplicationException(readData);
				}
				retVal=new EmailMessage();
				retVal.IsNew=true;
				retVal.SentOrReceived=EmailSentOrReceived.Received;
				retVal.MsgDateTime=DateTime.Now;
				retVal.PatNum=0;
				retVal.ToAddress=emailAddress.EmailUsername;
				string emailMsg=readData;
				//Reference for email format RFC2822: http://tools.ietf.org/html/rfc2822#page-9
				while(readData!="") {//Reads email header information and gets to the message body.
					readData=RdStrm.ReadLine();
					if(readData.ToLower().StartsWith("subject:")) {
						retVal.Subject=readData.Substring(8).Trim();
					}
					else if(readData.ToLower().StartsWith("from:")) {
						retVal.FromAddress=readData.Substring(5).Trim().Replace("<","").Replace(">","");//We remove < and >, because addresses sometimes look like "<ehr@opendental.com>"
					}
					emailMsg+=readData+"\r\n";
				}
				readData=RdStrm.ReadLine();//Moves stream reader position to beginning of message body.
				string emailBody="";
				while(readData!=".") {//Gets the body contents then exits.
					emailBody+=readData+"\r\n";
					emailMsg+=readData+"\r\n";
					readData=RdStrm.ReadLine();
				}
				if(emailMsg.Contains("application/pkcs7-mime")) {//The email MIME/body is encrypted (known as S/MIME).
					retVal.SentOrReceived=EmailSentOrReceived.ReceivedEncrypted;
					retVal.BodyText=emailMsg;//The entire contents of the email, so that if decryption fails, the email will still be saved to the database for decryption later if possible.
					try {
						DecryptDirect(retVal);
					}
					catch(Exception ex) {
						//The encrypted message will be saved to the database so that the user can try to decrypt later in FormEmailMessageEdit.
					}
				}
				else {//Unencrypted email.				
					retVal.BodyText=ExtractMimeAttach(emailBody);
				}				
				//Delete the message on the mail server.
				Data="DELE "+lastEmail+"\r\n";
				sendData=System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(sendData,0,sendData.Length);
			}
			catch(Exception ex) {
				//MessageBox.Show(ex.Message);
				error=ex.Message;//this could be more elegant.
			}
			//Disconnect
			if(disconnect) {
				Data="QUIT"+"\r\n";
				sendData=System.Text.Encoding.ASCII.GetBytes(Data.ToCharArray());
				NetStrm.Write(sendData,0,sendData.Length);
				NetStrm.Close();
				RdStrm.Close();
			}
			if(error!="") {
				throw new ApplicationException(error);
			}
			return retVal;
		}

		///<summary>Only for email messages with SentOrReceived set to EncryptedDirect. If decryption fails, then throws an exception and does not change email. If decryption succeeds, then emailMessage.SentOrReceived is set to ReceivedDirect and the BodyText of the email is changed from the entire encrypted email contents to the decrypted body text.</summary>
		public static void DecryptDirect(EmailMessage emailMessage) {
			string domain=emailMessage.ToAddress.Substring(emailMessage.ToAddress.IndexOf("@")+1);//Used to locate the certificate for the incoming email. For example, if ToAddress is ehr@opendental.com, then this will be opendental.com
			Health.Direct.Agent.DirectAgent agent=new Health.Direct.Agent.DirectAgent(domain);
			Health.Direct.Agent.IncomingMessage inMsg=null;
			try {
				inMsg=agent.ProcessIncoming(emailMessage.BodyText);//This is actually the entire contents of the email message for this specific case. Normally it would just be the body text.
			}
			catch(Exception ex) {
				throw new ApplicationException("Decryption failed.\r\n"+ex.Message);
			}
			if(inMsg!=null) {
				emailMessage.SentOrReceived=EmailSentOrReceived.ReceivedDirect;
				emailMessage.BodyText=ExtractMimeAttach(inMsg.Message.Body.Text);
				//emailMessage.PatNum=0;//TODO: Set for some Direct messages.
				//emailMessage.MsgDateTime=DateTime.ParseExact(inMsg.Message.DateValue,"ddd, d MMM yyyy HH:mm:ss",CultureInfo.InvariantCulture);//We could pull the email date and time from the server instead. The format is more difficult than normal, and might be different depending on who sent the email.
			}
		}

		///<summary>Receives one email from the inbox, and returns the contents of the attachment as a string.  Will throw an exception if anything goes wrong, so surround with a try-catch.</summary>
		public static string Receive() {
			if(PrefC.GetString(PrefName.EHREmailToAddress)=="") {//this pref is hidden, so no practical way for user to turn this on.
				throw new ApplicationException("This feature cannot be used except in a test environment because email is not secure.");
			}
			if(PrefC.GetString(PrefName.EHREmailPOPserver)=="") {
				throw new ApplicationException("No POP server set up.");
			}
			EmailAddress emailAddress=new EmailAddress();
			emailAddress.SMTPserverIncoming=PrefC.GetString(PrefName.EHREmailPOPserver);
			emailAddress.ServerPortIncoming=PrefC.GetInt(PrefName.EHREmailPort);
			emailAddress.EmailUsername=PrefC.GetString(PrefName.EHREmailFromAddress);
			emailAddress.EmailPassword=PrefC.GetString(PrefName.EHREmailPassword);
			return ReceiveFromInbox(emailAddress).BodyText;
		}

		private static string ExtractMimeAttach(string fullMsg) {
			fullMsg=fullMsg.Trim();//removes any extra lines at the end, too.
			//break the message into lines
			string[] separator=new string[1];
			separator[0]="\r\n";
			string[] lines=fullMsg.Split(separator,StringSplitOptions.None);//keep blank lines
			//start at the end of the message and go backwards to establish the boundary string.
			string boundary="";
			for(int i=lines.Length-1;i>=0;i--) {
				if(!lines[i].EndsWith("--")) {
					continue;
				}
				if(!lines[i].StartsWith("--")) {
					continue;
				}
				boundary=lines[i].TrimEnd('-');
				//Example boundaries:
				//------=_NextPart_000_001A_01CC0FCF.42154980
				//--------------070304090505090508040909
				break;
			}
			string attach="";
			if(boundary!="") {
				//find the last section of the message, which will certainly be the attachment.
				fullMsg=fullMsg.TrimEnd('-');
				separator=new string[1];
				separator[0]=boundary;
				string[] sections=fullMsg.Split(separator,StringSplitOptions.RemoveEmptyEntries);
				if(sections.Length==0) {
					throw new ApplicationException("No sections found in MIME message.");
				}
				attach=sections[sections.Length-1];
			}
			else {
				attach=fullMsg;//Some unencrypted emails do not have mime boundries. For example, Godaddy email messages in html format.
			}
			separator=new string[1];
			separator[0]="\r\n";
			lines=attach.Split(separator,StringSplitOptions.None);//keep blank lines
			//determine how many lines are header.
			int contentStartI=0;
			bool headerPassed=false;//the first line might be blank.  We are looking for the blank line that follows the header.
			bool isbase64=false;
			for(int i=0;i<lines.Length;i++) {
				if(lines[i].Contains("base64")) {
					isbase64=true;
				}
				if(lines[i].StartsWith("Content-")) {
					headerPassed=true;
					continue;
				}
				if(lines[i].StartsWith("\t")) {
					continue;
				}
				if(!headerPassed) {
					continue;
				}
				if(lines[i]!="") {//a Content line can wrap.  This skips such a wrapped line.
					continue;
				}
				//we have hit a blank line
				contentStartI=i+1;
				break;
				//throw new ApplicationException("Header break not found for attachment section of MIME message.");
			}
			string retVal="";
			for(int i=contentStartI;i<lines.Length;i++) {
				if(isbase64) {
					retVal+=lines[i];
				}
				else {
					retVal+=lines[i]+"\r\n";
				}
			}
			if(isbase64) {
				byte[] rawData=Convert.FromBase64String(retVal);
				StringBuilder strb=new StringBuilder();
				for(int i=0;i<rawData.Length;i++){
					strb.Append((char)rawData[i]);
				}
				retVal=strb.ToString();
			}
			else{//qp encoding
				retVal=retVal.Replace("=\r\n","");//gets rid of the = at the end of wrapped lines.
				//Get rid of QP encoded characters (=xx).
				MatchCollection matches=Regex.Matches(retVal,@"=\w\w");
				//already wrote it to go backwards, but it doesn't actually matter.
				for(int i=matches.Count-1;i>=0;i--) {
					string matchRaw=matches[i].Value;
					char char1=matchRaw[1];
					byte byte1=CharToByte(char1);
					char char2=matchRaw[2];
					byte byte2=CharToByte(char2);
					byte byteFinal=(byte)((byte1*16)+byte2);
					char charFinal=(char)byteFinal;
					retVal=retVal.Replace(matchRaw,charFinal.ToString());
				}
			}
			return retVal;
		}

		private static byte CharToByte(char mychar) {
			if(mychar >= '0' && mychar <= '9') {
				return byte.Parse(mychar.ToString());
			}
			switch(mychar) {
				case 'A':
					return 10;
				case 'B':
					return 11;
				case 'C':
					return 12;
				case 'D':
					return 13;
				case 'E':
					return 14;
				case 'F':
					return 15;
			}
			throw new ApplicationException("char not convertible to byte:"+mychar.ToString());
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






	}
}
