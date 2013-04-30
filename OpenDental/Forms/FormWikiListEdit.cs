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
	public partial class FormWikiListEdit:Form {
		///<summary>Name of the wiki list being manipulated. This does not include the "wikilist" prefix. i.e. "networkdevices" not "wikilistnetworkdevices"</summary>
		public string WikiListCurName;
		public bool IsNew;
		private DataTable Table;

		public FormWikiListEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiListEdit_Load(object sender,EventArgs e) {
			if(!WikiLists.CheckExists(WikiListCurName)) {
				IsNew=true;
				WikiLists.CreateNewWikiList(WikiListCurName);
			}
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
		}

		private void ManuallyEdit() {
			
		}

		/// <summary></summary>
		private void FillGrid() {
			List<WikiListHeaderWidth> colHeaderWidths = WikiListHeaderWidths.GetForList(WikiListCurName);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			for(int c=0;c<Table.Columns.Count;c++){
				int colWidth = 100;//100 = default value in case something is malformed in the database.
				foreach(WikiListHeaderWidth colHead in colHeaderWidths) {
					if(colHead.ColName==Table.Columns[c].ColumnName) {
						colWidth=colHead.ColWidth;
						break;
					}
				}
				col=new ODGridColumn(Table.Columns[c].ColumnName,colWidth,false);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Table.Rows.Count;i++){
				row=new ODGridRow();
				for(int c=0;c<Table.Columns.Count;c++) {
					row.Cells.Add(Table.Rows[i][c].ToString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.Title=WikiListCurName;
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			FormWikiListItemEdit FormWLIE = new FormWikiListItemEdit();
			FormWLIE.WikiListCur=WikiListCurName;
			FormWLIE.ItemNum=PIn.Long(Table.Rows[e.Row][0].ToString());
			FormWLIE.ShowDialog();
			//saving occurs from within the form.
			if(FormWLIE.DialogResult!=DialogResult.OK) {
				return;
			}
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
		}

		private void gridMain_CellTextChanged(object sender,EventArgs e) {
			
		}

		private void gridMain_CellLeave(object sender,ODGridClickEventArgs e) {
			/*
			Table.Rows[e.Row][e.Col]=gridMain.Rows[e.Row].Cells[e.Col].Text;
			Point cellSelected=new Point(gridMain.SelectedCell.X,gridMain.SelectedCell.Y);
			FillGrid();//gridMain.SelectedCell gets cleared.
			gridMain.SetSelected(cellSelected);*/
		}

		/*No longer necessary because gridMain_CellLeave does this as text is changed.
		///<summary>This is done before generating markup, when adding or removing rows or columns, and when changing from "none" view to another view.  FillGrid can't be done until this is done.</summary>
		private void PumpGridIntoTable() {
			//table and grid will only have the same numbers of rows and columns if the view is none.
			//Otherwise, table may have more columns
			//So this is only allowed when switching from the none view to some other view.
			if(ViewShowing!=0) {
				return;
			}
			for(int i=0;i<Table.Rows.Count;i++) {
				for(int c=0;c<Table.Columns.Count;c++) {
					Table.Rows[i][c]=gridMain.Rows[i].Cells[c].Text;
				}
			}
		}*/

		private void butColumnLeft_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.WikiListSetup)) {//gives a message box if no permission
				return;
			}
			if(gridMain.SelectedCell.X==-1){
				return;
			}
			Point newSelectedCell=gridMain.SelectedCell;
			newSelectedCell.X=Math.Max(1,newSelectedCell.X-1);
			WikiLists.ShiftColumnLeft(WikiListCurName,Table.Columns[gridMain.SelectedCell.X].ColumnName);
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
			gridMain.SetSelected(newSelectedCell);
		}

		private void butColumnRight_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.WikiListSetup)) {//gives a message box if no permission
				return;
			}
			if(gridMain.SelectedCell.X==-1) {
				return;
			}
			Point newSelectedCell=gridMain.SelectedCell;
			newSelectedCell.X=Math.Min(gridMain.Columns.Count-1,newSelectedCell.X+1);
			WikiLists.ShiftColumnRight(WikiListCurName,Table.Columns[gridMain.SelectedCell.X].ColumnName);
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
			gridMain.SetSelected(newSelectedCell);
		}

		private void butHeaders_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.WikiListSetup)) {//gives a message box if no permission
				return;
			}
			FormWikiListHeaders FormWLH = new FormWikiListHeaders();
			FormWLH.WikiListCurName=WikiListCurName;
			FormWLH.ShowDialog();
			if(FormWLH.DialogResult!=DialogResult.OK) {
				return;
			}
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
		}

		private void butColumnAdd_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.WikiListSetup)) {//gives a message box if no permission
				return;
			}
			WikiLists.AddColumn(WikiListCurName);
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
		}

		private void butColumnDelete_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.WikiListSetup)) {//gives a message box if no permission
				return;
			}
			if(gridMain.SelectedCell.X==-1) {
				MsgBox.Show(this,"Select cell in column to be deleted first.");
				return;
			}
			if(!WikiLists.CheckColumnEmpty(WikiListCurName,Table.Columns[gridMain.SelectedCell.X].ColumnName)){
				MsgBox.Show(this,"Column cannot be deleted because it conatins data.");
				return;
			}
			WikiLists.DeleteColumn(WikiListCurName,Table.Columns[gridMain.SelectedCell.X].ColumnName);
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
		}

		private void butAddItem_Click(object sender,EventArgs e) {
			WikiLists.AddItem(WikiListCurName);
			Table=WikiLists.GetByName(WikiListCurName);
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.WikiListSetup)) {//gives a message box if no permission
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete this entire list and all references to it?")) {
				return;
			}
			WikiLists.DeleteList(WikiListCurName);
			//TODO: update all wikipages and remove links to data that was contained in the table. if we implement it.
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
	

	

		

		

		

	

	}
}