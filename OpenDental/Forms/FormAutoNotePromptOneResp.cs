using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAutoNotePromptOneResp:Form {
		///<summary>Set this value externally.</summary>
		public string PromptText;
		///<summary>What the user picked.</summary>
		public string ResultText;
		///<summary>The string value representing the list to pick from.  One item per line.</summary>
		public string PromptOptions;

		public FormAutoNotePromptOneResp() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNotePromptOneResp_Load(object sender,EventArgs e) {
			labelPrompt.Text=PromptText;
			string[] lines=PromptOptions.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
			for(int i=0;i<lines.Length;i++) {
				listMain.Items.Add(lines[i]);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				MsgBox.Show(this,"One response must be selected");
				return;
			}
			ResultText=listMain.SelectedItem.ToString();
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