using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormReference:Form {
		private Patient PatCur;

		public FormReference() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReference_Load(object sender,EventArgs e) {

		}

		private void FillMain() {

		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {

		}

		private void checkUsedRefs_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void checkBadRefs_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}