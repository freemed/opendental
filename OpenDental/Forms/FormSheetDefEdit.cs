using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetDefEdit:Form {
		public SheetDef SheetDefCur;

		public FormSheetDefEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetDefEdit_Load(object sender,EventArgs e) {

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}