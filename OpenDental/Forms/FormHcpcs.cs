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
	public partial class FormHcpcs:Form {
		public bool IsSelectionMode;
		public Hcpcs SelectedHcpcs;
		private List<Hcpcs> listHcpcses;
		private bool changed;

		public FormHcpcs() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormHcpcs_Load(object sender,EventArgs e) {
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
			col=new ODGridColumn("HCPCS Code",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",500);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			listHcpcses=Hcpcses.GetBySearchText(textCode.Text);
			for(int i=0;i<listHcpcses.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listHcpcses[i].HcpcsCode);
				row.Cells.Add(listHcpcses[i].DescriptionShort);
				row.Tag=listHcpcses[i];
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectionMode) {
				SelectedHcpcs=(Hcpcs)gridMain.Rows[e.Row].Tag;
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
			SelectedHcpcs=(Hcpcs)gridMain.Rows[gridMain.GetSelectedIndex()].Tag;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}