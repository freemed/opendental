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

		private List<EmailMessage> emailMessages;

		public FormEmailInbox() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEmailInbox_Load(object sender,EventArgs e) {
			FillGridEmailMessages();
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormEmailAddresses formEA=new FormEmailAddresses();
			formEA.ShowDialog();
		}

		private void FillGridEmailMessages() {
			EmailAddress emailAddress=EmailAddresses.GetByClinic(0);//Default for practice.
			if(emailAddress.SMTPserverIncoming=="") {
				MsgBox.Show(this,"No email address has been setup yet. Go to Setup | Email to add one.");
				return;
			}
			emailMessages=EmailMessages.GetReceivedForAddress(emailAddress.EmailUsername);
			gridEmailMessages.BeginUpdate();
			gridEmailMessages.Columns.Clear();
			//TODO: Add more columns.
			gridEmailMessages.Columns.Add(new UI.ODGridColumn("From",200));
			gridEmailMessages.Columns.Add(new UI.ODGridColumn("Subject",200));
			gridEmailMessages.Columns.Add(new UI.ODGridColumn("Patient",200));
			gridEmailMessages.Rows.Clear();
			for(int i=0;i<emailMessages.Count;i++) {
				UI.ODGridRow row=new UI.ODGridRow();
				row.Cells.Add(new UI.ODGridCell(emailMessages[i].FromAddress));
				row.Cells.Add(new UI.ODGridCell(emailMessages[i].Subject));
				row.Cells.Add(new UI.ODGridCell(""));//No patients yet.
				gridEmailMessages.Rows.Add(row);
			}
			gridEmailMessages.EndUpdate();
		}

		private void gridEmailMessages_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			EmailMessage emailMessage=emailMessages[e.Row];
			FormEmailMessageEdit formEME=new FormEmailMessageEdit(emailMessage);
			formEME.ShowDialog();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			EmailAddress emailAddress=EmailAddresses.GetByClinic(0);//Default for practice.
			if(emailAddress.SMTPserverIncoming=="") {
				MsgBox.Show(this,"No email address has been setup yet. Go to Setup | Email to add one.");
				return;
			}
			EmailMessage emailMessage=null;
			try {
				emailMessage=EhrEmail.ReceiveFromInbox(emailAddress);
			}
			catch(Exception ex){
				MessageBox.Show(Lan.g(this,"Failed to receive email: ")+ex.Message);
				return;
			}
			EmailMessages.Insert(emailMessage);
			FormEmailMessageEdit formEME=new FormEmailMessageEdit(emailMessage);
			formEME.ShowDialog();
			FillGridEmailMessages();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}