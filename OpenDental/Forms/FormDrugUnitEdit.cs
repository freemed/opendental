using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDrugUnitEdit:Form {
		public FormDrugUnitEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDrugUnitEdit_Load(object sender,EventArgs e) {

		}

		private void butDelete_Click(object sender,EventArgs e) {

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}