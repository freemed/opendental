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
			AutoNotes.RefreshCache();
			AutoNoteControls.RefreshCache();
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
			int selectionStart=textMain.SelectionStart;
			if(selectionStart==0) {
				textMain.Text=note+textMain.Text;
			}
			else if(selectionStart==textMain.Text.Length-1) {
				textMain.Text=textMain.Text+note;
			}
			else if(selectionStart==-1) {//?is this even possible?
				textMain.Text=textMain.Text+note;
			}
			else {
				textMain.Text=textMain.Text.Substring(0,selectionStart)+note+textMain.Text.Substring(selectionStart);
			}
			List<AutoNoteControl> prompts=new List<AutoNoteControl>();
			MatchCollection matches=Regex.Matches(note,@"\[Prompt:""[a-zA-Z_0-9 ]+""\]");
			string autoNoteDescript;
			AutoNoteControl control;
			string promptResponse;
			int matchloc;
			for(int i=0;i<matches.Count;i++) {
				//highlight the current match in red
				matchloc=textMain.Text.IndexOf(matches[i].Value);
				textMain.Select(matchloc,matches[i].Value.Length);
				textMain.SelectionBackColor=Color.Yellow;
				textMain.SelectionLength=0;
				Application.DoEvents();//refresh the textbox
				autoNoteDescript=matches[i].Value.Substring(9,matches[i].Value.Length-11);
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
						textMain.SelectAll();
						textMain.SelectionBackColor=Color.White;
						textMain.Select(textMain.Text.Length,0);
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
						textMain.SelectAll();
						textMain.SelectionBackColor=Color.White;
						textMain.Select(textMain.Text.Length,0);
						return;
					}
				}
				else if(control.ControlType=="MultiResponse") {
					FormAutoNotePromptMultiResp FormMR=new FormAutoNotePromptMultiResp();
					FormMR.PromptText=control.ControlLabel;
					FormMR.PromptOptions=control.ControlOptions;
					FormMR.ShowDialog();
					if(FormMR.DialogResult==DialogResult.OK) {
						promptResponse=FormMR.ResultText;
					}
					else {
						textMain.SelectAll();
						textMain.SelectionBackColor=Color.White;
						textMain.Select(textMain.Text.Length,0);
						return;
					}
				}
				string resultstr=textMain.Text.Substring(0,matchloc)+promptResponse;
				if(textMain.Text.Length > matchloc+matches[i].Value.Length) {
					resultstr+=textMain.Text.Substring(matchloc+matches[i].Value.Length);
				}
				textMain.Text=resultstr;
				Application.DoEvents();//refresh the textbox
			}
			textMain.SelectAll();
			textMain.SelectionBackColor=Color.White;
			textMain.Select(textMain.Text.Length,0);
		}

		private void butOK_Click(object sender,EventArgs e) {
			CompletedNote=textMain.Text.Replace("\n","\r\n");
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}