using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDrugUnitSetup:Form {
		public FormDrugUnitSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDrugUnitSetup_Load(object sender,EventArgs e) {

		}

		private void butAdd_Click(object sender,EventArgs e) {

		}

		private void butClose_Click(object sender,EventArgs e) {

		}

		private void butClose_Click_1(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}