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

		public FormEmailInbox() {
			InitializeComponent();
			Lan.F(this);
		}

		private void menuItemSetup_Click(object sender,EventArgs e) {
			FormEmailAddresses formEA=new FormEmailAddresses();
			formEA.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}