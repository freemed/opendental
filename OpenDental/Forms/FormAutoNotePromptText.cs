using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAutoNotePromptText:Form {
		///<summary>Set this value externally.</summary>
		public string PromptText;
		///<summary>What the user entered.  This can be set externally to the default value.</summary>
		public string ResultText;

		public FormAutoNotePromptText() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNotePromptText_Load(object sender,EventArgs e) {
			labelPrompt.Text=PromptText;
			textMain.Text=ResultText;
		}

		private void butOK_Click(object sender,EventArgs e) {
			ResultText=textMain.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Abort autonote entry?")) {
				return;
			}
			DialogResult=DialogResult.Cancel;
		}

		
	}
}