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
			Location=new Point(Left,Top+150);
			labelPrompt.Text=PromptText;
			textMain.Text=ResultText;
		}

		private void butOK_Click(object sender,EventArgs e) {
			ResultText=textMain.Text;
			DialogResult=DialogResult.OK;
		}

		private void butSkip_Click(object sender,EventArgs e) {
			ResultText="";
			DialogResult=DialogResult.OK;
		}

		private void butPreview_Click(object sender,EventArgs e) {
			ResultText=textMain.Text;
			FormAutoNotePromptPreview FormP=new FormAutoNotePromptPreview();
			FormP.ResultText=ResultText;
			FormP.ShowDialog();
			if(FormP.DialogResult==DialogResult.OK) {
				ResultText=FormP.ResultText;
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Abort autonote entry?")) {
				return;
			}
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}