using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {

	public partial class FormAutoNoteEdit : Form {
		public bool IsNew;
		public AutoNote AutoNoteCur;
		private int textSelectionStart;

		public FormAutoNoteEdit() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAutoNoteEdit_Load(object sender, EventArgs e) {
			textBoxAutoNoteName.Text=AutoNoteCur.AutoNoteName;
			textMain.Text=AutoNoteCur.MainText;
			FillGrid();
		}

		///<summary></summary>
		private void FillGrid() {
			AutoNoteControls.RefreshCache();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<AutoNoteControls.Listt.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(AutoNoteControls.Listt[i].Descript);  
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAutoNoteControlEdit FormA=new FormAutoNoteControlEdit();
			FormA.ControlCur=AutoNoteControls.Listt[e.Row];
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormAutoNoteControlEdit FormA=new FormAutoNoteControlEdit();
			AutoNoteControl control=new AutoNoteControl();
			control.ControlType="Text";
			FormA.ControlCur=control;
			FormA.IsNew=true;
			FormA.ShowDialog();
			if(FormA.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butInsert_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a prompt first.");
				return;
			}
			string fieldStr=AutoNoteControls.Listt[gridMain.GetSelectedIndex()].Descript;
			if(textSelectionStart < textMain.Text.Length-1) {
				textMain.Text=textMain.Text.Substring(0,textSelectionStart)
					+"[Prompt:\""+fieldStr+"\"]"
					+textMain.Text.Substring(textSelectionStart);
			}
			else{//otherwise, just tack it on the end
				textMain.Text+="[Prompt:\""+fieldStr+"\"]";
			}
			textMain.Select(textSelectionStart+fieldStr.Length+11,0);
			textMain.Focus();
		}

		private void textMain_Leave(object sender,EventArgs e) {
			textSelectionStart=textMain.SelectionStart;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete this autonote?")){
				return;
			}
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			AutoNotes.Delete(AutoNoteCur.AutoNoteNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			AutoNoteCur.AutoNoteName=textBoxAutoNoteName.Text;
			AutoNoteCur.MainText=textMain.Text;
			if(IsNew) {
				AutoNotes.Insert(AutoNoteCur);
			}
			else {
				AutoNotes.Update(AutoNoteCur);
			}
			DialogResult=DialogResult.OK;
		}


		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		
	}
}