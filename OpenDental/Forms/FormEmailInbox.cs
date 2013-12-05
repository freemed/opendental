using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEmailInbox:Form {

		private EmailAddress AddressInbox;
		private List<EmailMessage> ListEmailMessages;

		public FormEmailInbox() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEmailInbox_Load(object sender,EventArgs e) {
			labelInboxComputerName.Text="Computer Name Where New Email Is Fetched: "+PrefC.GetString(PrefName.EmailInboxComputerName);
			labelThisComputer.Text+=Dns.GetHostName();
			Application.DoEvents();//Show the form contents before loading email into the grid.
			GetMessages();//If no new messages, then the user will know based on what shows in the grid.
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormEmailAddresses formEA=new FormEmailAddresses();
			formEA.ShowDialog();
			labelInboxComputerName.Text="Computer Name Where New Email Is Fetched: "+PrefC.GetString(PrefName.EmailInboxComputerName);
			GetMessages();//Get new messages, just in case the user entered email information for the first time.
		}

		///<summary>Gets new messages from email inbox, as well as older messages from the db. Also fills the grid.</summary>
		private int GetMessages() {
			AddressInbox=EmailAddresses.GetByClinic(0);//Default for clinic/practice.
			Cursor=Cursors.WaitCursor;
			FillGridEmailMessages();//Show what is in db.
			Cursor=Cursors.Default;
			if(AddressInbox.Pop3ServerIncoming=="") {//Email address not setup.
				Text="Email Inbox";
				AddressInbox=null;
				//todo: Message Box is too instrusive, move this to a status label.
				//MsgBox.Show(this,"Default email address has not been setup completely.");
				return 0;
			}
			Text="Email Inbox for "+AddressInbox.EmailUsername;
			Application.DoEvents();//So that something is showing while the page is loading.
			if(!CodeBase.ODEnvironment.IdIsThisComputer(PrefC.GetString(PrefName.EmailInboxComputerName))) {//This is not the computer to get new messages from.
				return 0;
			}
			if(PrefC.GetString(PrefName.EmailInboxComputerName)=="") {
				MsgBox.Show(this,"Computer name to fetch new email from has not been setup.");
				return 0;
			}
			Cursor=Cursors.WaitCursor;
			int emailMessagesTotalCount=0;
			Text="Email Inbox for "+AddressInbox.EmailUsername+" - Fetching new email...";
			try {
				bool hasMoreEmail=true;
				while(hasMoreEmail) {
					List<EmailMessage> emailMessages=EmailMessages.ReceiveFromInbox(1,AddressInbox);
					emailMessagesTotalCount+=emailMessages.Count;
					if(emailMessages.Count==0) {
						hasMoreEmail=false;
					}
					else { //Show messages as they are downloaded, to indicate to the user that the program is still processing.
						FillGridEmailMessages();
						Application.DoEvents();
					}
				}
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Error retrieving email messages")+": "+ex.Message);
			}
			finally {
				Text="Email Inbox for "+AddressInbox.EmailUsername;
			}
			Cursor=Cursors.Default;
			return emailMessagesTotalCount;
		}

		///<summary>Gets new emails and also shows older emails from the database.</summary>
		private void FillGridEmailMessages() {
			if(AddressInbox==null) {
				ListEmailMessages=new List<EmailMessage>();
			}
			else {
				ListEmailMessages=EmailMessages.GetInboxForAddress(AddressInbox.EmailUsername,Security.CurUser.ProvNum);
			}
			if(gridEmailMessages.Columns.Count==0) {//Columns do not change.  We only need to set them once.
				gridEmailMessages.BeginUpdate();
				gridEmailMessages.Columns.Clear();
				int colReceivedDatePixCount=140;
				int colStatusPixCount=120;
				int colFromPixCount=200;
				int colSubjectPixCount=200;
				int colPatientPixCount=140;
				int colVariablePixCount=gridEmailMessages.Width-22-colReceivedDatePixCount-colStatusPixCount-colFromPixCount-colSubjectPixCount-colPatientPixCount;
				gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"ReceivedDate"),colReceivedDatePixCount,HorizontalAlignment.Center));
				gridEmailMessages.Columns[gridEmailMessages.Columns.Count-1].SortingStrategy=UI.GridSortingStrategy.DateParse;
				gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"Sent/Received"),colStatusPixCount,HorizontalAlignment.Center));
				gridEmailMessages.Columns[gridEmailMessages.Columns.Count-1].SortingStrategy=UI.GridSortingStrategy.StringCompare;
				gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"Subject"),colSubjectPixCount,HorizontalAlignment.Left));
				gridEmailMessages.Columns[gridEmailMessages.Columns.Count-1].SortingStrategy=UI.GridSortingStrategy.StringCompare;
				gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"From"),colFromPixCount,HorizontalAlignment.Left));
				gridEmailMessages.Columns[gridEmailMessages.Columns.Count-1].SortingStrategy=UI.GridSortingStrategy.StringCompare;
				gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"Patient"),colPatientPixCount,HorizontalAlignment.Left));
				gridEmailMessages.Columns[gridEmailMessages.Columns.Count-1].SortingStrategy=UI.GridSortingStrategy.StringCompare;
				gridEmailMessages.Columns.Add(new UI.ODGridColumn(Lan.g(this,"Preview"),colVariablePixCount,HorizontalAlignment.Left));
				gridEmailMessages.Columns[gridEmailMessages.Columns.Count-1].SortingStrategy=UI.GridSortingStrategy.StringCompare;
			}
			gridEmailMessages.Rows.Clear();
			for(int i=0;i<ListEmailMessages.Count;i++) {
				UI.ODGridRow row=new UI.ODGridRow();
				row.Tag=ListEmailMessages[i];//Used to locate the correct email message if the user decides to sort the grid.
				if(ListEmailMessages[i].SentOrReceived==EmailSentOrReceived.Received || ListEmailMessages[i].SentOrReceived==EmailSentOrReceived.WebMailReceived
					|| ListEmailMessages[i].SentOrReceived==EmailSentOrReceived.ReceivedEncrypted || ListEmailMessages[i].SentOrReceived==EmailSentOrReceived.ReceivedDirect) {
					row.Bold=true;//unread
				}
				row.Cells.Add(new UI.ODGridCell(ListEmailMessages[i].MsgDateTime.ToString()));//ReceivedDate
				row.Cells.Add(new UI.ODGridCell(ListEmailMessages[i].SentOrReceived.ToString()));//Status
				row.Cells.Add(new UI.ODGridCell(ListEmailMessages[i].Subject));//Subject
				row.Cells.Add(new UI.ODGridCell(ListEmailMessages[i].FromAddress));//From
				long patNumRegardingPatient=ListEmailMessages[i].PatNum;
				//Webmail messages should list the patient as the PatNumSubj, which means "the patient whom this message is regarding".
				if(ListEmailMessages[i].SentOrReceived==EmailSentOrReceived.WebMailReceived || ListEmailMessages[i].SentOrReceived==EmailSentOrReceived.WebMailRecdRead) {
					patNumRegardingPatient=ListEmailMessages[i].PatNumSubj;
				}
				if(patNumRegardingPatient==0) {
					row.Cells.Add(new UI.ODGridCell(""));//Patient
				}
				else {
					Patient pat=Patients.GetPat(patNumRegardingPatient);
					row.Cells.Add(new UI.ODGridCell(pat.GetNameLF()));//Patient
				}
				string preview=ListEmailMessages[i].BodyText.Replace("\r\n"," ").Replace('\n',' ');//Replace newlines with spaces, in order to compress the preview.
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
			EmailMessage emailMessage=(EmailMessage)gridEmailMessages.Rows[e.Row].Tag;
			if(emailMessage.SentOrReceived==EmailSentOrReceived.WebMailReceived
					|| emailMessage.SentOrReceived==EmailSentOrReceived.WebMailRecdRead
					|| emailMessage.SentOrReceived==EmailSentOrReceived.WebMailSent
					|| emailMessage.SentOrReceived==EmailSentOrReceived.WebMailSentRead) 
			{
				//web mail uses special secure messaging portal
				FormWebMailMessageEdit FormWMME=new FormWebMailMessageEdit(emailMessage.PatNum,emailMessage.EmailMessageNum);
				if(FormWMME.ShowDialog()!=DialogResult.Abort) { //will only return Abort if validation fails on load, in which case the message will remain unread
					EmailMessages.UpdateSentOrReceivedRead(emailMessage);//Mark the message read.
				}				
			}
			else {
				FormEmailMessageEdit formEME=new FormEmailMessageEdit(emailMessage);
				formEME.ShowDialog();
				emailMessage=EmailMessages.GetOne(emailMessage.EmailMessageNum);//Fetch from DB, in case changed to to decrypt.
				if(emailMessage!=null && emailMessage.SentOrReceived!=EmailSentOrReceived.ReceivedEncrypted) {//emailMessage could be null if the message was deleted in FormEmailMessageEdit().
					EmailMessages.UpdateSentOrReceivedRead(emailMessage);
				}
			}
			FillGridEmailMessages();//To show the email is read.
		}

		private void butChangePat_Click(object sender,EventArgs e) {
			if(gridEmailMessages.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Please select an email message.");
				return;
			}
			FormPatientSelect form=new FormPatientSelect();
			if(form.ShowDialog()!=DialogResult.OK) {
				return;
			}
			for(int i=0;i<gridEmailMessages.SelectedIndices.Length;i++) {
				EmailMessage emailMessage=(EmailMessage)gridEmailMessages.Rows[gridEmailMessages.SelectedIndices[i]].Tag;
				emailMessage.PatNum=form.SelectedPatNum;
				EmailMessages.UpdatePatNum(emailMessage);
			}
			MessageBox.Show(Lan.g(this,"Email messages moved successfully")+": "+gridEmailMessages.SelectedIndices.Length);
			FillGridEmailMessages();//Refresh grid to show changed patient.
		}

		private void butMarkUnread_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			for(int i=0;i<gridEmailMessages.SelectedIndices.Length;i++) {
				EmailMessage emailMessage=(EmailMessage)gridEmailMessages.Rows[gridEmailMessages.SelectedIndices[i]].Tag;
				EmailMessages.UpdateSentOrReceivedUnread(emailMessage);
			}
			FillGridEmailMessages();
			Cursor=Cursors.Default;
		}

		private void butMarkRead_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			for(int i=0;i<gridEmailMessages.SelectedIndices.Length;i++) {
				EmailMessage emailMessage=(EmailMessage)gridEmailMessages.Rows[gridEmailMessages.SelectedIndices[i]].Tag;
				EmailMessages.UpdateSentOrReceivedRead(emailMessage);
			}
			FillGridEmailMessages();
			Cursor=Cursors.Default;
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			GetMessages();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}