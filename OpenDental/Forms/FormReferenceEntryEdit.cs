using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormReferenceEntryEdit:Form {
		public FormReferenceEntryEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReferenceEdit_Load(object sender,EventArgs e) {

		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {

		}

		private void butDeleteAll_Click(object sender,EventArgs e) {

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}