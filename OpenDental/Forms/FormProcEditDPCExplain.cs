using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormProcEditDPCExplain:Form {

		public FormProcEditDPCExplain() {
			InitializeComponent();
			Lan.F(this);
		}

		private void radioButtonError_CheckedChanged(object sender,EventArgs e) {
			butOK.Enabled=true;
		}

		private void radioButtonReAssign_CheckedChanged(object sender,EventArgs e) {
			butOK.Enabled=true;
		}

		private void radioButtonNewProv_CheckedChanged(object sender,EventArgs e) {
			butOK.Enabled=true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




	}
}