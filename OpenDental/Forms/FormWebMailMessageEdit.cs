using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormWebMailMessageEdit:Form {
		public EmailMessage EmailMessageCur;
		public bool IsNew;

		public FormWebMailMessageEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebMailMessageEdit_Load(object sender,EventArgs e) {
			textToAddress.Text=EmailMessageCur.ToAddress;
			textFromAddress.Text=EmailMessageCur.FromAddress;
			textBodyText.Text=EmailMessageCur.BodyText;
			if(IsNew) {
				butOK.Text="Send";
			}
			else {
				butTo.Visible=false;
				butFrom.Visible=false;
				textFromAddress.ReadOnly=true;
				textFromAddress.BackColor=SystemColors.Control;
				textBodyText.ReadOnly=true;
			}
		}

		private void butTo_Click(object sender,EventArgs e) {
			FormPatientSelect FormP=new FormPatientSelect();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) {
				return;
			}
			Patient pat=Patients.GetPat(FormP.SelectedPatNum);
			EmailMessageCur.ToAddress=pat.GetNameLF();
			textToAddress.Text=EmailMessageCur.ToAddress;
		}

		private void butFrom_Click(object sender,EventArgs e) {
			FormProviderPick FormP=new FormProviderPick();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) {
				return;
			}
			EmailMessageCur.FromAddress=Providers.GetFormalName(FormP.SelectedProvNum);
			textFromAddress.Text=EmailMessageCur.FromAddress;
		}

		private void butOK_Click(object sender,EventArgs e) {
			EmailMessageCur.BodyText=textBodyText.Text;
			EmailMessageCur.SentOrReceived=EmailSentOrReceived.WebMailSent;
			EmailMessages.Insert(EmailMessageCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}