using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormTimeCardManage:Form {

		public FormTimeCardManage() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTimeCardManage_Load(object sender,EventArgs e) {

		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {

		}

		private void butLeft_Click(object sender,EventArgs e) {

		}

		private void butRight_Click(object sender,EventArgs e) {

		}

		private void butPrint_Click(object sender,EventArgs e) {

		}

		private void butPreview_Click(object sender,EventArgs e) {

		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}