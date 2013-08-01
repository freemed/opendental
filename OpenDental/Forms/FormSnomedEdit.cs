using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSnomedEdit:Form {
		private Snomed SnomedCur;
		public bool IsNew;

		public FormSnomedEdit(Snomed snomedCur) {
			InitializeComponent();
			Lan.F(this);
			SnomedCur=snomedCur;
		}

		private void FormIcd9Edit_Load(object sender,EventArgs e) {
			if(!IsNew) {
				textCode.Enabled=false;
			}
			textCode.Text=SnomedCur.SnomedCode;
			textDescription.Text=SnomedCur.Description;
		}

		private void buttonDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")){
				return;
			}
			try {
				ICD9s.Delete(SnomedCur.SnomedNum);
				DialogResult=DialogResult.OK;
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			SnomedCur.SnomedCode=textCode.Text;
			SnomedCur.Description=textDescription.Text;
			if(IsNew) {//Used the "+Add" button to open this form.
				if(ICD9s.CodeExists(SnomedCur.SnomedCode)) {//Must enter a unique code.
					MsgBox.Show(this,"You must choose a unique code.");
					return;
				}
				Snomeds.Insert(SnomedCur);
			}
			else {
				Snomeds.Update(SnomedCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}