using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormRecallMessageEdit:Form {
		public string MessageVal;

		public FormRecallMessageEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRecallMessageEdit_Load(object sender,EventArgs e) {
			textMain.Text=MessageVal;
		}

		private void butOK_Click(object sender,EventArgs e) {
			MessageVal=textMain.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}