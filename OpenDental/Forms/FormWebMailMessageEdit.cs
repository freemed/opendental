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
		public Patient PatCur;
		
		public FormWebMailMessageEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWebMailMessageEdit_Load(object sender,EventArgs e) {
			if(PatCur==null) {
				MsgBox.Show(this,"Select a valid patient");
				DialogResult=DialogResult.Cancel;
				return;
			}
			textToAddress.Text=PatCur.GetNameFL();
			textFromAddress.Text=Providers.GetFormalName(PatCur.PriProv);
			if(textFromAddress.Text=="") {
				MsgBox.Show(this,"Invalid primary provider for this patient");
				DialogResult=DialogResult.Cancel;
				return;
			}
		}

		private void butTo_Click(object sender,EventArgs e) {
			FormPatientSelect FormP=new FormPatientSelect();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) {
				return;
			}
			PatCur=Patients.GetPat(FormP.SelectedPatNum);
			textToAddress.Text=PatCur.GetNameLF();
		}

		private void butFrom_Click(object sender,EventArgs e) {
			FormProviderPick FormP=new FormProviderPick();
			FormP.ShowDialog();
			if(FormP.DialogResult!=DialogResult.OK) {
				return;
			}
			textFromAddress.Text=Providers.GetFormalName(FormP.SelectedProvNum);
		}

		private void butSend_Click(object sender,EventArgs e) {
			if(textBodyText.Text=="") {
				MsgBox.Show(this,"Email body is empty");
				return;
			}
			if(textSubject.Text=="") {
				MsgBox.Show(this,"Enter a subject");
				textSubject.Focus();
				return;
			}
			EmailMessage emailMessage=new EmailMessage();
			emailMessage.FromAddress=textFromAddress.Text;
			emailMessage.ToAddress=textToAddress.Text;
			emailMessage.PatNum=PatCur.PatNum;
			emailMessage.Subject=textSubject.Text;
			emailMessage.BodyText=textBodyText.Text;
			emailMessage.MsgDateTime=DateTime.Now;
			emailMessage.SentOrReceived=EmailSentOrReceived.WebMailSent;
			EmailMessages.Insert(emailMessage);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}