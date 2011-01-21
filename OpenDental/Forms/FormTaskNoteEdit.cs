using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormTaskNoteEdit:Form {
		public TaskNote TaskNoteCur;

		public FormTaskNoteEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTaskNoteEdit_Load(object sender,EventArgs e) {
			textDateTime.Text=TaskNoteCur.DateTimeNote.ToString();
			textUser.Text=Userods.GetName(TaskNoteCur.UserNum);
			textNote.Text=TaskNoteCur.Note;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(TaskNoteCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			TaskNotes.Delete(TaskNoteCur.TaskNoteNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textNote.Text=="") {
				MsgBox.Show(this,"Please enter a note, or delete this entry.");
				return;
			}
			try {
				TaskNoteCur.DateTimeNote=DateTime.Parse(textDateTime.Text);
			}
			catch{
				MsgBox.Show(this,"Please fix date.");
				return;
			}
			TaskNoteCur.Note=textNote.Text;
			if(TaskNoteCur.IsNew) {
				TaskNotes.Insert(TaskNoteCur);
			}
			else {
				TaskNotes.Update(TaskNoteCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	
	}
}