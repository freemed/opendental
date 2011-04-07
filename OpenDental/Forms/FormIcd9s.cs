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
		public long SelectedIcd9Num;

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
			FillGrid();
		}

		private void FillGrid() {
			Cursor=Cursors.WaitCursor;
			ICD9s.RefreshCache();
			listMain.Items.Clear();
			for(int i=0;i<ICD9s.Listt.Count;i++) {
				listMain.Items.Add(ICD9s.Listt[i].ICD9Code+" - "+ICD9s.Listt[i].Description);
			}
			Cursor=Cursors.Default;
		}

		private void listMain_DoubleClick(object sender,System.EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			if(IsSelectionMode) {
				SelectedIcd9Num=ICD9s.Listt[listMain.SelectedIndex].ICD9Num;
				DialogResult=DialogResult.OK;
				return;
			}
			FormIcd9Edit FormI=new FormIcd9Edit(ICD9s.Listt[listMain.SelectedIndex]);
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
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
			SelectedIcd9Num=ICD9s.Listt[listMain.SelectedIndex].ICD9Num;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}