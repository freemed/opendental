using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormWikiLists:Form {
		private List<string> wikiLists;

		public FormWikiLists() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiLists_Load(object sender,EventArgs e) {
			FillList();
		}

		private void FillList() {
			listBox1.Items.Clear();
			wikiLists = WikiLists.GetAllLists();
			foreach(string list in wikiLists) {
				listBox1.Items.Add(list.Substring(9));
			}
		}

		private void listBox1_DoubleClick(object sender,EventArgs e) {
			if(listBox1.SelectedIndices.Count<1) {
				return;
			}
			FormWikiListEdit FormWLE = new FormWikiListEdit();
			FormWLE.WikiListCurName=wikiLists[listBox1.SelectedIndex].Substring(9);
			FormWLE.ShowDialog();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			InputBox inputListName = new InputBox("New List Name");
			inputListName.ShowDialog();
			FormWikiListEdit FormWLE = new FormWikiListEdit();
			FormWLE.WikiListCurName = inputListName.textResult.Text.ToLower().Replace(" ","");
			//FormWLE.IsNew=true;//set within the form.
			FormWLE.ShowDialog();
			FillList();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}