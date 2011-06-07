using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrProvKeysCustomer:Form {
		public FormEhrProvKeysCustomer() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrProvKeysCustomer_Load(object sender,EventArgs e) {

		}

		private void butAdd_Click(object sender,EventArgs e) {

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}