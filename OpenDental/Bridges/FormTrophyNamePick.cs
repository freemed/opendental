using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental.Bridges {
	public partial class FormTrophyNamePick:Form {
		public List<TrophyFolder> ListMatches;
		///<summary>If dialogResult is OK, then this will contain the picked folder name.  If blank, then the calling class will need to generate a new folder name.</summary>
		public string PickedName;

		public FormTrophyNamePick() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTrophyNamePick_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormTrophyNamePick","FolderName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormTrophyNamePick","Last Name"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormTrophyNamePick","First Name"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormTrophyNamePick","Birthdate"),80);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListMatches.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListMatches[i].FolderName);
				row.Cells.Add(ListMatches[i].LName);
				row.Cells.Add(ListMatches[i].FName);
				row.Cells.Add(ListMatches[i].BirthDate.ToShortDateString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butNew_Click(object sender,EventArgs e) {
			PickedName="";
			DialogResult=DialogResult.OK;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			PickedName=ListMatches[e.Row].FolderName;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select an item from the list first.");
				return;
			}
			PickedName=ListMatches[gridMain.GetSelectedIndex()].FolderName;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}