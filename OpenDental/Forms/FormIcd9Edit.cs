using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormIcd9Edit:Form {
		private ICD9 Icd9Cur;

		public FormIcd9Edit(ICD9 icd9Cur) {
			InitializeComponent();
			Lan.F(this);
			Icd9Cur=icd9Cur;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormIcd9Edit_Load(object sender,EventArgs e) {
			textCode.Text=Icd9Cur.ICD9Code;
			textDescription.Text=Icd9Cur.Description;
		}
	}
}