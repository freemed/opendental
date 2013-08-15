using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormCodeSystemsImport:Form {
		//private list

		public FormCodeSystemsImport() {
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary></summary>
		private void FillGrid() {
			//gridMain.BeginUpdate();
			//gridMain.Columns.Clear();
			//ODGridColumn col;
			//for(int c=0;c<Table.Columns.Count;c++) {
			//  int colWidth = 100;//100 = default value in case something is malformed in the database.
			//  foreach(WikiListHeaderWidth colHead in colHeaderWidths) {
			//    if(colHead.ColName==Table.Columns[c].ColumnName) {
			//      colWidth=colHead.ColWidth;
			//      break;
			//    }
			//  }
			//  col=new ODGridColumn(Table.Columns[c].ColumnName,colWidth,false);
			//  gridMain.Columns.Add(col);
			//}
			//gridMain.Rows.Clear();
			//ODGridRow row;
			//for(int i=0;i<Table.Rows.Count;i++) {
			//  row=new ODGridRow();
			//  for(int c=0;c<Table.Columns.Count;c++) {
			//    row.Cells.Add(Table.Rows[i][c].ToString());
			//    if(textSearch.Text!="" && 
			//      Table.Rows[i][c].ToString().ToUpper().Contains(textSearch.Text.ToUpper())) {
			//      row.ColorBackG=Color.FromArgb(245,240,190);//lighter than paleGoldenrod
			//    }
			//  }
			//  gridMain.Rows.Add(row);
			//}
			//gridMain.EndUpdate();
			//gridMain.Title=WikiListCurName;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}