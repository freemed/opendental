using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAutoNotePromptMultiResp:Form {
		///<summary>Set this value externally.</summary>
		public string PromptText;
		///<summary>What the user picked.</summary>
		public string ResultText;
		///<summary>The string value representing the list to pick from.  One item per line.</summary>
		public string PromptOptions;

		public FormAutoNotePromptMultiResp() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNotePromptMultiResp_Load(object sender,EventArgs e) {
			Location=new Point(Left,Top+150);
			labelPrompt.Text=PromptText;
			string[] lines=PromptOptions.Split(new string[] {"\r\n"},StringSplitOptions.RemoveEmptyEntries);
			for(int i=0;i<lines.Length;i++) {
				listMain.Items.Add(lines[i]);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			ResultText="";
			for(int i=0;i<listMain.CheckedIndices.Count;i++) {
				if(i>0) {
					ResultText+=", ";
				}
				ResultText+=listMain.CheckedItems[i].ToString();
			}
			DialogResult=DialogResult.OK;
		}

		private void butSkip_Click(object sender,EventArgs e) {
			ResultText="";
			DialogResult=DialogResult.OK;
		}

		private void butPreview_Click(object sender,EventArgs e) {
			ResultText="";
			for(int i=0;i<listMain.CheckedIndices.Count;i++) {
				if(i>0) {
					ResultText+=", ";
				}
				ResultText+=listMain.CheckedItems[i].ToString();
			}
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