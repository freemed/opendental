using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormBenefitElectHistory:Form {
		public FormBenefitElectHistory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormBenefitElectHistory_Load(object sender,EventArgs e) {

		}

		//private void butOK_Click(object sender,EventArgs e) {
		//	DialogResult=DialogResult.OK;
		//}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}