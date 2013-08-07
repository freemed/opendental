using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
using System.Text.RegularExpressions;

namespace OpenDental {
	public partial class FormWikiListItemEdit:Form {
		///<summary>Name of the wiki list.</summary>
		public string WikiListCur;
		public long ItemNum;
		public bool IsNew;
		///<summary>Creating a data table containing only one item allows us to use column names.</summary>
		DataTable TableItem;

		public FormWikiListItemEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiListEdit_Load(object sender,EventArgs e) {
			TableItem = WikiLists.GetItem(WikiListCur,ItemNum);
			//Show the PK in the title bar for informational purposes.  We don't put it in the grid because user can't change it.
			this.Text=this.Text+" - "+TableItem.Columns[0]+" "+TableItem.Rows[0][0].ToString();//OK to use 0 indices here. If this fails something else is wrong.
			FillGrid();
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Column"),200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Value"),400);
			col.IsEditable=true;
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=1;i<TableItem.Columns.Count;i++){//Start at 1 since row 0 (PK) goes in the title bar.
				row=new ODGridRow();
				row.Cells.Add(TableItem.Columns[i].ColumnName);
				row.Cells.Add(TableItem.Rows[0][i].ToString());
				if(i==0) {
					row.ColorBackG=Color.Gray;//darken the PK to imply that it cannot be edited.
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.Title="Edit List Item";
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			//remove this
		}

		private void gridMain_CellTextChanged(object sender,EventArgs e) {
			
		}

		private void gridMain_CellLeave(object sender,ODGridClickEventArgs e) {
			//Save data from grid into table. No call to DB, so this should be safe.
			for(int i=0;i<gridMain.Rows.Count;i++) {
				TableItem.Rows[0][i+1]=gridMain.Rows[i].Cells[1].Text;//Column 0 of TableItems.Rows[0] is in the title bar, so it is off from the grid by 1.
			}
			//FillGrid();//Causes errors with tabbing between cells. We put the PK in the title bar to fix this (now it doesn't need to be refreshed).
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.WikiListSetup)) {//might want to implement a new security permission.
				return;
			}
			//maybe require all empty or admin priv
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete this list item and all references to it?")) {
				return;
			}
			WikiLists.DeleteItem(WikiListCur,ItemNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			WikiLists.UpdateItem(WikiListCur,TableItem);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
	

	

		

		

		

	

	}
}