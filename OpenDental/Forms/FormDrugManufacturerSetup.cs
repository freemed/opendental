using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDrugManufacturerSetup:Form {
		public FormDrugManufacturerSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDrugManufacturerSetup_Load(object sender,EventArgs e) {

		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}