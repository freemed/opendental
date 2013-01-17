using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Text.RegularExpressions;

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
			ODGridColumn col=new ODGridColumn("",200,false);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<DictCustoms.Listt.Count;i++) {
				row=new ODGridRow();
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
			string newWord=Regex.Replace(textCustom.Text,"[\\s]|[\\p{P}\\p{S}-['-]]","");//don't allow words with spaces or punctuation except ' and - in them
			for(int i=0;i<DictCustoms.Listt.Count;i++) {//Make sure it's not already in the custom list
				if(DictCustoms.Listt[i].WordText==newWord) {
					MsgBox.Show(this,"The word "+newWord+" is already in the custom word list.");
					textCustom.Clear();
					return;
				}
			}
			DictCustom word=new DictCustom();
			word.WordText=newWord;
			DictCustoms.Insert(word);
			DataValid.SetInvalid(InvalidType.DictCustoms);
			textCustom.Clear();
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			InputBox editWord=new InputBox("Edit word");
			DictCustom origWord=DictCustoms.Listt[e.Row];
			editWord.textResult.Text=origWord.WordText;
			if(editWord.ShowDialog()!=DialogResult.OK) {
				return;
			}
			if(editWord.textResult.Text==origWord.WordText) {
				return;
			}
			if(editWord.textResult.Text=="") {
				DictCustoms.Delete(origWord.DictCustomNum);
				DataValid.SetInvalid(InvalidType.DictCustoms);
				FillGrid();
				return;
			}
			string newWord=Regex.Replace(editWord.textResult.Text,"[\\s]|[\\p{P}\\p{S}-['-]]","");//don't allow words with spaces or punctuation except ' and - in them
			for(int i=0;i<DictCustoms.Listt.Count;i++) {//Make sure it's not already in the custom list
				if(DictCustoms.Listt[i].WordText==newWord) {
					MsgBox.Show(this,"The word "+newWord+" is already in the custom word list.");
					editWord.textResult.Text=origWord.WordText;
					return;
				}
			}
			origWord.WordText=newWord;
			DictCustoms.Update(origWord);
			DataValid.SetInvalid(InvalidType.DictCustoms);
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Select a word to delete first.");
				return;
			}
			DictCustoms.Delete(DictCustoms.Listt[gridMain.GetSelectedIndex()].DictCustomNum);
			DataValid.SetInvalid(InvalidType.DictCustoms);
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

	}
}