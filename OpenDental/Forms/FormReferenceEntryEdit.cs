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
		private CustRefEntry RefEntryCur;

		public FormReferenceEntryEdit(CustRefEntry refEntry) {
			InitializeComponent();
			Lan.F(this);
			RefEntryCur=refEntry;
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