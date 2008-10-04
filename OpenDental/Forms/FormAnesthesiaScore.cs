using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAnesthesiaScore:Form {
		public FormAnesthesiaScore() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormAnesthesiaScore_Load(object sender, EventArgs e)
		{

		}

		private void radioButDischUnstable_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}