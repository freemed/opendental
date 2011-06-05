using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrProvKey:Form {
		public Provider ProvCur;

		public FormEhrProvKey() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrProvKey_Load(object sender,EventArgs e) {
			textLName.Text=ProvCur.LName;
			textFName.Text=ProvCur.FName;
			textEhrKey.Text=ProvCur.EhrKey;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//todo: validate key

			ProvCur.EhrKey=textEhrKey.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}