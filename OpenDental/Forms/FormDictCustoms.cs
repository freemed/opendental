using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormDictCustoms:Form {

		public FormDictCustoms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDictCustoms_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",200,true);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<DictCustoms.Listt.Count;i++) {
				row=new ODGridRow();
				row.Height=19;//To handle the isEditable functionality
				row.Cells.Add(DictCustoms.Listt[i].WordText);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(textCustom.Text=="") {
				MsgBox.Show(this,"Please enter a custom word first.");
				return;
			}
			for(int i=0;i<DictCustoms.Listt.Count;i++) {//Make sure it's not already in the custom list
				if(DictCustoms.Listt[i].WordText==textCustom.Text) {
					MsgBox.Show(this,"The word "+textCustom.Text+" is already in the custom word list.");
					textCustom.Clear();
					return;
				}
			}
			DictCustom word=new DictCustom();
			word.WordText=textCustom.Text;
			DictCustoms.Insert(word);
			DataValid.SetInvalid(InvalidType.DictCustoms);
			textCustom.Clear();
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.SelectedCell.Y==-1) {
				MsgBox.Show(this,"Select a word to delete first.");
				return;
			}
			DictCustoms.Delete(DictCustoms.Listt[gridMain.SelectedCell.Y].DictCustomNum);
			DataValid.SetInvalid(InvalidType.DictCustoms);
			FillGrid();
		}

		private void gridMain_CellLeave(object sender,ODGridClickEventArgs e) {
			DictCustom oldWord=DictCustoms.Listt[e.Row];
			string newWord=gridMain.Rows[e.Row].Cells[e.Col].Text;
			if(oldWord.WordText==newWord) {
				return;
			}
			if(newWord=="") {//user deleted word text so delete from dictcustoms
				DictCustoms.Delete(oldWord.DictCustomNum);
				DataValid.SetInvalid(InvalidType.DictCustoms);
				FillGrid();
			}
			else {//update dictcustoms
				oldWord.WordText=newWord;
				DictCustoms.Update(oldWord);
				DataValid.SetInvalid(InvalidType.DictCustoms);
				FillGrid();
			}
			gridMain.SetSelected(new Point(e.Col,e.Row));
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

	}
}