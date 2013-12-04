using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CodeBase;
using OpenDentBusiness;

namespace OpenDental {
	///<summary>DialogResult will be Abort if message was unable to be read. If message is read successfully (Ok or Cancel), then caller is responsible for updating SentOrReceived to read (where applicable).</summary>
	public partial class FormWebMailMessageEdit:Form {
		private EmailMessage _secureMessage;
		private EmailMessage _insecureMessage;
		private EmailAddress _emailAddressSender;
		private long _patNum;
		private long _replyToEmailMessageNum;
		private bool _allowSendSecureMessage=true;
		private bool _allowSendNotificationMessage=true;
		
		///<summary>Default ctor. This implies that we are composing a new message, NOT replying to an existing message. Provider attached to this message should be Security.CurUser.ProvNum</summary>
		public FormWebMailMessageEdit(long patNum) : this(patNum,0) { }

		///<summary>This implies that we are replying to an existing message. Provider attached to this message will be the ProvNum attached to the original message. If this ProvNum does not match Security.CurUser.ProvNum then the send action will be blocked.</summary>
		public FormWebMailMessageEdit(long patNum,long emailMessageNum) {
			InitializeComponent();
			Lan.F(this);
			_replyToEmailMessageNum=emailMessageNum;
			_patNum=patNum;
		}

		private void FormWebMailMessageEdit_Load(object sender,EventArgs e) {
			VerifyInputs();
		}

		private void BlockSendSecureMessage(string reason) {
			_allowSendSecureMessage=false;
			butSend.Enabled=false;
			butPreview.Enabled=false;
			labelNotification.Text=Lan.g(this,"Warning: Secure email send prevented - ")+Lan.g(this,reason);
			labelNotification.ForeColor=Color.Red;
		}

		private void BlockSendNotificationMessage(string reason) {
			_allowSendNotificationMessage=false;
			butSend.Enabled=false;
			butPreview.Enabled=false;
			labelNotification.Text=Lan.g(this,"Warning: Notification email send prevented - ")+Lan.g(this,reason);
			labelNotification.ForeColor=Color.Red;
		}

		private void VerifyInputs() {			
			long priProvNum=0;
			string notificationSubject;
			string notificationBodyNoUrl;
			string notificationURL;
			Patient patCur=Patients.GetPat(_patNum);
			if(patCur==null) {
				BlockSendSecureMessage("Patient is invalid.");
			}
			else {
				textBoxTo.Text=patCur.GetNameFL();
				Provider priProv=Providers.GetProv(patCur.PriProv);
				if(priProv==null) {
					BlockSendSecureMessage("Invalid primary provider for this patient.");
				}
				else {
					priProvNum=priProv.ProvNum;
					Provider userODProv=Providers.GetProv(Security.CurUser.ProvNum);
					if(userODProv==null) {
						BlockSendSecureMessage("Not logged in as valid provider.");
					}
					else if(userODProv.ProvNum!=priProv.ProvNum) {
						BlockSendSecureMessage("The patient's primary provider does not match the provider attached to the user currently logged in.");
					}
					else {
						textBoxFrom.Text=priProv.GetFormalName();					
					}
				}
				if(patCur.Email=="") {
					BlockSendNotificationMessage("Patient email is not setup properly.");
				}
				if(patCur.OnlinePassword=="") {
					BlockSendNotificationMessage("Patient has not been given online access.");
				}
			}
			//We are replying to an existing message so verify that the provider linked to this message matches our currently logged in provider.  
			//This is because web mail communications will be visible in the patients Chart Module.
			if(_replyToEmailMessageNum>0) {
				EmailMessage replyToEmailMessage=EmailMessages.GetOne(_replyToEmailMessageNum);
				if(replyToEmailMessage==null) {
					MsgBox.Show(this,"Invalid input email message");
					DialogResult=DialogResult.Abort;  //nothing to show so abort, caller should be waiting for abort to determine if message should be marked read
					return;
				}				
				textBoxSubject.Text="RE: "+replyToEmailMessage.Subject;
				textBoxBody.Text="\r\n\r\n-----"+Lan.g(this,"Original Message")+"-----\r\n"
					+Lan.g(this,"From")+": "+replyToEmailMessage.FromAddress+"\r\n"
					+Lan.g(this,"Sent")+": "+replyToEmailMessage.MsgDateTime.ToShortDateString()+" "+replyToEmailMessage.MsgDateTime.ToShortTimeString()+"\r\n"
					+Lan.g(this,"To")+":"+replyToEmailMessage.ToAddress+"\r\n"
					+Lan.g(this,"Subject")+": "+replyToEmailMessage.Subject
					+"\r\n\r\n"+replyToEmailMessage.BodyText;						
			}
			notificationSubject=PrefC.GetString(PrefName.PatientPortalNotifySubject);
			notificationBodyNoUrl=PrefC.GetString(PrefName.PatientPortalNotifyBody);
			notificationURL=PrefC.GetString(PrefName.PatientPortalURL);
			_emailAddressSender=EmailAddresses.GetByClinic(0);//Default for clinic/practice.
			if(_emailAddressSender==null) {
				BlockSendNotificationMessage("Provider email is not setup properly.");
			}			
			if(notificationSubject=="") {
				BlockSendNotificationMessage("Notification email Subject is not setup properly.");
			}
			if(notificationBodyNoUrl=="") {
				BlockSendNotificationMessage("Notification email Body is not setup properly.");
			}
			if(_allowSendSecureMessage) {
				_secureMessage=new EmailMessage();
				_secureMessage.FromAddress=textBoxFrom.Text;
				_secureMessage.ToAddress=textBoxTo.Text;
				_secureMessage.PatNum=patCur.PatNum;
				_secureMessage.SentOrReceived=EmailSentOrReceived.WebMailSent;  //this is secure so mark as webmail sent
				_secureMessage.ProvNumWebMail=priProvNum;
			}
			if(_allowSendNotificationMessage) {
				_insecureMessage=new EmailMessage();
				_insecureMessage.FromAddress=_emailAddressSender.SenderAddress;
				_insecureMessage.ToAddress=patCur.Email;
				_insecureMessage.PatNum=patCur.PatNum;
				_insecureMessage.Subject=notificationSubject;
				_insecureMessage.BodyText=notificationBodyNoUrl.Replace("[URL]",notificationURL);
				_insecureMessage.SentOrReceived=EmailSentOrReceived.Sent; //this is not secure so just mark as regular sent
			}
			if(_allowSendSecureMessage && _allowSendNotificationMessage) {
				labelNotification.Text=Lan.g(this,"Notification email will be sent to patient: ")+patCur.Email;
			}
		}

		private bool VerifyOutputs() {
			if(textBoxSubject.Text=="") {
				MsgBox.Show(this,"Enter a subject");
				textBoxSubject.Focus();
				return false;
			} 
			if(textBoxBody.Text=="") {
				MsgBox.Show(this,"Email body is empty");
				textBoxBody.Focus();
				return false;
			}
			return true;
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormPatientPortalSetup formPPS=new FormPatientPortalSetup();
			formPPS.ShowDialog();
			if(formPPS.DialogResult==DialogResult.OK) {
				VerifyInputs();
			}
		}

		private void butPreview_Click(object sender,EventArgs e) {
			if(!VerifyOutputs()) {
				return;
			}
			StringBuilder sb=new StringBuilder();
			sb.AppendLine("------ "+Lan.g(this,"Notification email that will be sent to the patient's email address:"));
			if(_allowSendNotificationMessage) {
				sb.AppendLine(Lan.g(this,"Subject")+": "+_insecureMessage.Subject);
				sb.AppendLine(Lan.g(this,"Body")+": "+_insecureMessage.BodyText);
			}
			else {
				sb.AppendLine(Lan.g(this,"------ "+Lan.g(this,"Notification email settings are not set up.  Click Setup from the web mail message edit window to set up notification emails")+" ------"));
			}
			sb.AppendLine();
			sb.AppendLine("------ "+Lan.g(this,"Secure web mail message that will be sent to the patient's portal:"));
			sb.AppendLine(Lan.g(this,"Subject")+": "+textBoxSubject.Text);
			sb.AppendLine(Lan.g(this,"Body")+": "+textBoxBody.Text);
			MsgBoxCopyPaste msgBox=new MsgBoxCopyPaste(sb.ToString());
			msgBox.ShowDialog();
		}

		private void butSend_Click(object sender,EventArgs e) {
			if(!_allowSendSecureMessage) {
				MsgBox.Show(this,"Send not permitted");
				return;
			}
			if(!VerifyOutputs()) {
				return;
			}
			//Insert the message. The patient will not see this as an actual email.
			//Rather, they must login to the patient portal (secured) and view the message that way.
			//This is how we get around sending the patient a secure message, which would be a hassle for all involved.
			_secureMessage.Subject=textBoxSubject.Text;
			_secureMessage.BodyText=textBoxBody.Text;
			_secureMessage.MsgDateTime=DateTime.Now;
			EmailMessages.Insert(_secureMessage);
			if(_allowSendNotificationMessage) { 
				//Send an insecure notification email to the patient.
				_insecureMessage.MsgDateTime=DateTime.Now;				
				try {
					EmailMessages.SendEmailUnsecure(_insecureMessage,_emailAddressSender);
					//Insert the notification email into the emailmessage table so we have a record that it was sent.
					EmailMessages.Insert(_insecureMessage);
				}
				catch(Exception ex) {
					MsgBox.Show(this,ex.Message);
					return;
				}				
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}