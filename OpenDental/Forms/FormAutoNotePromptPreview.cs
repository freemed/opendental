using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAutoNotePromptPreview:Form {
		public string ResultText;

		public FormAutoNotePromptPreview() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNotePromptPreview_Load(object sender,EventArgs e) {
			textMain.Text=ResultText;
		}

		private void butOK_Click(object sender,EventArgs e) {
			ResultText=textMain.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}