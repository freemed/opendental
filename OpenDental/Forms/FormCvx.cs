using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormCvxs:Form {
		public bool IsSelectionMode;
		public Cvx SelectedCvx;
		private List<Cvx> listCvxs;

		public FormCvxs() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCvxs_Load(object sender,EventArgs e) {
			if(IsSelectionMode) {
				butClose.Text=Lan.g(this,"Cancel");
			}
			else {
				butOK.Visible=false;
			}
			ActiveControl=textCode;
		}
		
		private void butSearch_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("CVX Code",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",500);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			listCvxs=Cvxs.GetBySearchText(textCode.Text);
			for(int i=0;i<listCvxs.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listCvxs[i].CvxCode);
				row.Cells.Add(listCvxs[i].Description);
				row.Tag=listCvxs[i];
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectionMode) {
				SelectedCvx=(Cvx)gridMain.Rows[e.Row].Tag;
				DialogResult=DialogResult.OK;
				return;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			//not even visible unless IsSelectionMode
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedCvx=(Cvx)gridMain.Rows[gridMain.GetSelectedIndex()].Tag;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}