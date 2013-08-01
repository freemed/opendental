using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormIcd9s:Form {
		public bool IsSelectionMode;
		public ICD9 SelectedIcd9;
		private List<ICD9> icd9List;
		private bool changed;

		public FormIcd9s() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormIcd9s_Load(object sender,EventArgs e) {
			if(IsSelectionMode) {
				butClose.Text=Lan.g(this,"Cancel");
			}
			else {
				butOK.Visible=false;
			}
		}
		
		private void butSearch_Click(object sender,EventArgs e) {
			//if(textCode.Text.Length<3) {
			//	MsgBox.Show(this,"Please enter at least 3 characters before searching.");
			//	return;
			//}
			//forget about the above.  Allow general browsing by entering no search parameters.
			FillGrid();
		}

		private void FillGrid() {
			Cursor=Cursors.WaitCursor;
			icd9List=ICD9s.GetByCodeOrDescription(textCode.Text);
			listMain.Items.Clear();
			for(int i=0;i<icd9List.Count;i++) {
				listMain.Items.Add(icd9List[i].ICD9Code+" - "+icd9List[i].Description);
			}
			Cursor=Cursors.Default;
		}

		private void listMain_DoubleClick(object sender,System.EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			if(IsSelectionMode) {
				SelectedIcd9=icd9List[listMain.SelectedIndex];
				DialogResult=DialogResult.OK;
				return;
			}
			changed=true;
			FormIcd9Edit FormI=new FormIcd9Edit(icd9List[listMain.SelectedIndex]);
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			changed=true;
			ICD9 icd9=new ICD9();
			FormIcd9Edit FormI=new FormIcd9Edit(icd9);
			FormI.IsNew=true;
			FormI.ShowDialog();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless IsSelectionMode
			if(listMain.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedIcd9=icd9List[listMain.SelectedIndex];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}