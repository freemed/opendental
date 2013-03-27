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
		///<summary>By passing in the whole table we preserve the column names and positions even though this table should only have one row in it.</summary>
		public DataTable Item;
		public bool IsNew;

		public FormWikiListItemEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiListEdit_Load(object sender,EventArgs e) {
			FillGrid();
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Column"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Value"),110);;
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Item.Columns.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(Item.Columns[i].ColumnName);
				row.Cells.Add(Item.Rows[0][i].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.Title="Edit List Item";
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			//new big window to edit row
		}

		private void gridMain_CellTextChanged(object sender,EventArgs e) {
			
		}

		private void gridMain_CellLeave(object sender,ODGridClickEventArgs e) {
			//Table.Rows[e.Row][e.Col]=gridMain.Rows[e.Row].Cells[e.Col].Text;
			//Point cellSelected=new Point(gridMain.SelectedCell.X,gridMain.SelectedCell.Y);
			//FillGrid();//gridMain.SelectedCell gets cleared.
			//gridMain.SetSelected(cellSelected);
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
		}

		private void butColumnRight_Click(object sender,EventArgs e) {
		}

		private void butHeaders_Click(object sender,EventArgs e) {
		}

		private void butColumnAdd_Click(object sender,EventArgs e) {
			InputBox newColumnInput = new InputBox("New Column Name");
			if(newColumnInput.ShowDialog()!=DialogResult.OK) {
				return;
			}
			//Format New column name------------------------------------------------------------------------------
			string tempColumnName = newColumnInput.textResult.Text;
			tempColumnName=tempColumnName.Replace(" ","");
			tempColumnName=tempColumnName.ToUpper()[0]+tempColumnName.ToLower().Substring(1);
			//Validate New column name----------------------------------------------------------------------------
			foreach(DataColumn column in Table.Columns) {
				if(column.ColumnName==tempColumnName){
					MsgBox.Show(this,"Column name already exists.");
					return;
				}
			}
			//Add New Column--------------------------------------------------------------------------------------
			Table.Columns.Add(tempColumnName);
			FillGrid();
		}

		private void butColumnDelete_Click(object sender,EventArgs e) {
		}

		private void butRowUp_Click(object sender,EventArgs e) {
			if(gridMain.SelectedCell.Y==-1) {
				MsgBox.Show(this,"Please select a row first.");
				return;
			}
			if(gridMain.SelectedCell.Y==0) {
				return;//Row is already at the top.
			}
			DataRow row=Table.NewRow();
			for(int i=0;i<Table.Columns.Count;i++) {
				row[i]=Table.Rows[gridMain.SelectedCell.Y][i];
			}
			Table.Rows.InsertAt(row,gridMain.SelectedCell.Y-1);
			Table.Rows.RemoveAt(gridMain.SelectedCell.Y+1);
			Point newCellSelected=new Point(gridMain.SelectedCell.X,gridMain.SelectedCell.Y-1);
			FillGrid();//gridMain.SelectedCell gets cleared.
			gridMain.SetSelected(newCellSelected);
		}

		private void butRowDown_Click(object sender,EventArgs e) {
			//Table.Rows.InsertAt
			//DataRow row=Table.Rows[i];
			//Table.Rows.RemoveAt
			if(gridMain.SelectedCell.Y==-1) {
				MsgBox.Show(this,"Please select a row first.");
				return;
			}
			if(gridMain.SelectedCell.Y==Table.Rows.Count-1) {
				return;//Row is already at the bottom.
			}
			DataRow row=Table.NewRow();
			for(int i=0;i<Table.Columns.Count;i++) {
				row[i]=Table.Rows[gridMain.SelectedCell.Y+1][i];
			}
			Table.Rows.InsertAt(row,gridMain.SelectedCell.Y);
			Table.Rows.RemoveAt(gridMain.SelectedCell.Y+2);
			Point newCellSelected=new Point(gridMain.SelectedCell.X,gridMain.SelectedCell.Y+1);
			FillGrid();//gridMain.SelectedCell gets cleared.
			gridMain.SetSelected(newCellSelected);
		}

		private void butRowAdd_Click(object sender,EventArgs e) {
			Point selectedCell;
			if(gridMain.SelectedCell.Y==-1) {
				selectedCell=new Point(0,Table.Rows.Count-1);
			}
			else {
				selectedCell=gridMain.SelectedCell;
			}
			//DataRow row=Table.NewRow();
			//row.ItemArray[0]=Table.Columns[0].
			Table.Rows.Add(Table.NewRow());//InsertAt(row,selectedCell.Y+1);
			Point newCellSelected=new Point(selectedCell.X,selectedCell.Y+1);
			FillGrid();//gridMain.SelectedCell gets cleared.
			gridMain.SetSelected(newCellSelected);
		}

		private void butRowDelete_Click(object sender,EventArgs e) {
			if(gridMain.SelectedCell.Y==-1) {
				MsgBox.Show(this,"Please select a row first.");
				return;
			}
			if(gridMain.Rows.Count==1) {
				MsgBox.Show(this,"Cannot delete last row.");
				return;
			}
			Table.Rows.RemoveAt(gridMain.SelectedCell.Y);
			Point newCellSelected=new Point(gridMain.SelectedCell.X,Math.Max(gridMain.SelectedCell.Y-1,0));
			FillGrid();//gridMain.SelectedCell gets cleared.
			if(newCellSelected.X>-1 && newCellSelected.Y >-1) {
				gridMain.SetSelected(newCellSelected);
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete this entire list and all references to it?")) {
				return;
			}
			//TODO: delete table from DB.
			//TODO: update all wikipages and remove links to data that was contained in the table.
			DialogResult=DialogResult.Cancel;
		}

		//private void butOK_Click(object sender,EventArgs e) {
		//  //if(IsNew) {
		//  //  List<string> columnNames = new List<string>();
		//  //  foreach(DataColumn column in Table.Columns) {
		//  //    //TODO: validate and clean column names.
		//  //    columnNames.Add(column.ColumnName);
		//  //  }
		//  //  WikiLists.CreateNewWikiList(WikiListCur,columnNames);
		//  //}
		//  WikiLists.UpdateList(WikiListCur,Table);
		//  DialogResult=DialogResult.OK;
		//}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
	

	

		

		

		

	

	}
}