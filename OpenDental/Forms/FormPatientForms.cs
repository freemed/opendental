using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormPatientForms:Form {
		public FormPatientForms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPatientForms_Load(object sender,EventArgs e) {

		}



		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}

		
	}
}