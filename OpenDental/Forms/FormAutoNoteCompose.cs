using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAutoNoteCompose:Form {
		public string CompletedNote;

		public FormAutoNoteCompose() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNoteCompose_Load(object sender,EventArgs e) {
			AutoNotes.Refresh();
			AutoNoteControls.Refresh();
			listMain.Items.Clear();
			for(int i=0;i<AutoNotes.Listt.Count;i++) {
				listMain.Items.Add(AutoNotes.Listt[i].AutoNoteName);
			}
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1){
				return;
			}
			string note=AutoNotes.Listt[listMain.SelectedIndex].MainText;
			textMain.Text=note;
			List<AutoNoteControl> prompts=new List<AutoNoteControl>();
			MatchCollection matches=Regex.Matches(note,@"\[Prompt:""[a-zA-Z_0-9 ]+""\]");
			string autoNoteDescript;
			AutoNoteControl control;
			string promptResponse;
			for(int i=0;i<matches.Count;i++) {
				//MessageBox.Show(matches[i].Value);
				//matches[i].Index
				autoNoteDescript=matches[i].Value.Substring(9,matches[i].Value.Length-11);
				//MessageBox.Show(autoCodeName);
				control=AutoNoteControls.GetByDescript(autoNoteDescript);
				if(control==null) {
					continue;//couldn't find a prompt with that name, so just ignore it.
				}
				promptResponse="";
				if(control.ControlType=="Text") {
					FormAutoNotePromptText FormT=new FormAutoNotePromptText();
					FormT.PromptText=control.ControlLabel;
					FormT.ResultText=control.ControlOptions;
					FormT.ShowDialog();
					if(FormT.DialogResult==DialogResult.OK) {
						promptResponse=FormT.ResultText;
					}
					else {
						return;
					}
				}
				else if(control.ControlType=="OneResponse") {
					FormAutoNotePromptOneResp FormOR=new FormAutoNotePromptOneResp();
					FormOR.PromptText=control.ControlLabel;
					FormOR.PromptOptions=control.ControlOptions;
					FormOR.ShowDialog();
					if(FormOR.DialogResult==DialogResult.OK) {
						promptResponse=FormOR.ResultText;
					}
					else {
						return;
					}
				}
				textMain.Text=textMain.Text.Replace(matches[i].Value,promptResponse);
				Application.DoEvents();//refresh the textbox
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			CompletedNote=textMain.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}