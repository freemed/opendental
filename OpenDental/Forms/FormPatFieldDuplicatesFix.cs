using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPatFieldDuplicatesFix:Form {
		public FormPatFieldDuplicatesFix() {
			InitializeComponent();
			Lan.F(this);

		}

		private void FormPatFieldDuplicatesFix_Load(object sender,EventArgs e) {
			FillLabels();
		}

		private void FillLabels() {
			labelCount.Text=PatFields.GetDuplicatePatFieldCount().ToString();
			if(labelCount.Text=="0") {
				labelInstructions.Text="";
			}
			else {
				labelInstructions.Text=Lan.g(this,"Click the Clear button to fix the duplicates.");
			}
		}

		private void butClear_Click(object sender,EventArgs e) {
			if(labelCount.Text=="0") {
				MsgBox.Show(this,"There are no duplicates to clear.");
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Clear all duplicates?")){
				return;
			}
			Cursor=Cursors.WaitCursor;
			PatFields.ClearDuplicatePatField();
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Done.");
			FillLabels();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}
	}
}