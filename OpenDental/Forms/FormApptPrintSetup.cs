using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormApptPrintSetup:Form {
		public FormApptPrintSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormApptPrintSetup_Load(object sender,EventArgs e) {
			//Load the prefs.
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Save the changes.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}