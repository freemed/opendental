using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEmailInbox:Form {

		private EmailAddress emailAddress;
		private List<EmailMessage> emailMessages;

		public FormEmailInbox() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEmailInbox_Load(object sender,EventArgs e) {
			GetMessages();//If no new messages, then the user will know based on what shows in the grid.
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormEmailAddresses formEA=new FormEmailAddresses();
			formEA.ShowDialog();
			GetMessages();//Get new messages, just in case the user entered email information for the first time.
		}

		///<summary>Gets new messages from email inbox, as well as older messages from the db. Also fills the grid.</summary>
		private int GetMessages() {
			Cursor=Cursors.WaitCursor;
			emailAddress=EmailAddresses.GetByClinic(0);//Default for clinic/practice.
			if(emailAddress.SMTPserverIncoming=="") {//Email address not setup.
				Text="Email Inbox";
				emailAddress=null;
				MsgBox.Show(this,"Default email address has not been setup properly.");
				return 0;
			}
			//Email address has been setup.
			Text="Email Inbox for "+emailAddress.EmailUsername;
			//Get new emails
			int newMessagesCount=0;
			EmailMessage emailMessage;
			do {
				emailMessage=null;
				try {
					emailMessage=EhrEmail.ReceiveFromInbox(emailAddress);
					newMessagesCount++;
				}
				catch(Exception ex) {
					if(ex.Message=="Inbox is empty.") {
						//Do not show message. Calling code decides to tell the user if no messages were found, because we do not want to show this message sometimes.
					}
					else {
						MessageBox.Show(Lan.g(this,"Failed to receive new email: ")+ex.Message);
					}
				}
				if(emailMessage!=null) {
					EmailMessages.Insert(emailMessage);
				}
			} while(emailMessage!=null);
			FillGridEmailMessages();
			Cursor=Cursors.Default;
			return newMessagesCount;
		}

		///<summary>Gets new emails and also shows older emails from the database.</summary>
		private void FillGridEmailMessages() {
			if(emailAddress==null) {
				emailMessages=new List<EmailMessage>();
			}
			else {
				emailMessages=EmailMessages.GetReceivedForAddress(emailAddress.EmailUsername);
			}
			gridEmailMessages.BeginUpdate();
			gridEmailMessages.Columns.Clear();
			int colReceivedDatePixCount=140;
			int colHasAttachPixCount=70;
			int colFromPixCount=200;
			int colSubjectPixCount=200;
			int colPatientPixCount=140;
			int colVariablePixCount=gridEmailMessages.Width-22-colReceivedDatePixCount-colHasAttachPixCount-colFromPixCount-colSubjectPixCount-colPatientPixCount;
			gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"ReceivedDate"),colReceivedDatePixCount,HorizontalAlignment.Center));
			gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"HasAttach"),colHasAttachPixCount,HorizontalAlignment.Center));
			gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"Subject"),colSubjectPixCount,HorizontalAlignment.Left));
			gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"From"),colFromPixCount,HorizontalAlignment.Left));
			gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"Patient"),colPatientPixCount,HorizontalAlignment.Left));
			gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"Preview"),colVariablePixCount,HorizontalAlignment.Left));
			gridEmailMessages.Rows.Clear();
			for(int i=0;i<emailMessages.Count;i++) {
				UI.ODGridRow row=new UI.ODGridRow();
				if(emailMessages[i].SentOrReceived==EmailSentOrReceived.Received || emailMessages[i].SentOrReceived==EmailSentOrReceived.WebMailReceived) {
					row.Bold=true;//unread
				}
				row.Cells.Add(new UI.ODGridCell(emailMessages[i].MsgDateTime.ToString()));//ReceivedDate
				if(emailMessages[i].Attachments.Count>0) {
					row.Cells.Add(new UI.ODGridCell("X"));//HasAttach
				}
				else {
					row.Cells.Add(new UI.ODGridCell(""));//HasAttach
				}
				row.Cells.Add(new UI.ODGridCell(emailMessages[i].Subject));//Subject
				row.Cells.Add(new UI.ODGridCell(emailMessages[i].FromAddress));//From
				if(emailMessages[i].PatNum==0) {
					row.Cells.Add(new UI.ODGridCell(""));//Patient
				}
				else {
					Patient pat=Patients.GetPat(emailMessages[i].PatNum);
					row.Cells.Add(new UI.ODGridCell(pat.GetNameLF()));//Patient
				}
				string preview=emailMessages[i].BodyText.Replace("\r\n"," ").Replace('\n',' ');//Replace newlines with spaces, in order to compress the preview.
				if(preview.Length>50) {
					preview=preview.Substring(0,50)+"...";
				}
				row.Cells.Add(new UI.ODGridCell(preview));//Preview
				gridEmailMessages.Rows.Add(row);
			}
			gridEmailMessages.EndUpdate();
		}

		private void gridEmailMessages_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			if(e.Row==-1) {
				return;
			}
			EmailMessage emailMessage=emailMessages[e.Row];
			MarkRead(emailMessage);
			FormEmailMessageEdit formEME=new FormEmailMessageEdit(emailMessage);
			formEME.ShowDialog();
			FillGridEmailMessages();//To show the email is read.
		}

		private void MarkRead(EmailMessage emailMessage) {
			if(emailMessage.SentOrReceived==EmailSentOrReceived.Received) {
				emailMessage.SentOrReceived=EmailSentOrReceived.Read;
				EmailMessages.Update(emailMessage);//Mark read.
			}
			else if(emailMessage.SentOrReceived==EmailSentOrReceived.WebMailReceived) {
				emailMessage.SentOrReceived=EmailSentOrReceived.WebMailRecdRead;
				EmailMessages.Update(emailMessage);//Mark read.
			}
		}

		private void MarkUnread(EmailMessage emailMessage) {
			if(emailMessage.SentOrReceived==EmailSentOrReceived.Read) {
				emailMessage.SentOrReceived=EmailSentOrReceived.Received;
				EmailMessages.Update(emailMessage);//Mark read.
			}
			else if(emailMessage.SentOrReceived==EmailSentOrReceived.WebMailRecdRead) {
				emailMessage.SentOrReceived=EmailSentOrReceived.WebMailReceived;
				EmailMessages.Update(emailMessage);//Mark read.
			}
		}

		private void butMarkUnread_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			for(int i=0;i<gridEmailMessages.SelectedIndices.Length;i++) {
				EmailMessage emailMessage=emailMessages[gridEmailMessages.SelectedIndices[i]];
				MarkUnread(emailMessage);
			}
			FillGridEmailMessages();
			Cursor=Cursors.Default;
		}

		private void butMarkRead_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			for(int i=0;i<gridEmailMessages.SelectedIndices.Length;i++) {
				EmailMessage emailMessage=emailMessages[gridEmailMessages.SelectedIndices[i]];
				MarkRead(emailMessage);
			}
			FillGridEmailMessages();
			Cursor=Cursors.Default;
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			int newMessageCount=GetMessages();
			if(newMessageCount==0) {
				MsgBox.Show(this,"Inbox is empty.");
				return;
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}