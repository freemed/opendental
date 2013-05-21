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
	public partial class FormWikiListHeaders:Form {
		public string WikiListCurName;
		///<summary>Widths must always be specified.  Not optional.</summary>
		private List<WikiListHeaderWidth> ListTableHeaders;

		public FormWikiListHeaders() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiListHeaders_Load(object sender,EventArgs e) {
			ListTableHeaders=WikiListHeaderWidths.GetForList(WikiListCurName);
			FillGrid();
		}

		///<summary>Each row of data becomes a column in the grid. This pattern is only used in a few locations.</summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			for(int i=0;i<ListTableHeaders.Count;i++) {
				col=new ODGridColumn("",75,true);//blank table-column names. List column names are contained in the cells of the table.
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row0=new ODGridRow();
			ODGridRow row1=new ODGridRow();
			for(int i=0;i<ListTableHeaders.Count;i++) {
				row0.Cells.Add(ListTableHeaders[i].ColName);
				row1.Cells.Add(ListTableHeaders[i].ColWidth.ToString());
			}
			gridMain.Rows.Add(row0);
			gridMain.Rows.Add(row1);
			gridMain.EndUpdate();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Set primary key to correct name-----------------------------------------------------------------------
			gridMain.Rows[0].Cells[0].Text=WikiListCurName+"Num";//prevents exceptions from occuring when user tries to rename PK.
			//Validate column names---------------------------------------------------------------------------------
			for(int i=0;i<gridMain.Columns.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(Regex.IsMatch(gridMain.Rows[0].Cells[i].Text,@"^\d")) {
					MsgBox.Show(this,"Column cannot start with numbers.");
					return;
				}
				if(Regex.IsMatch(gridMain.Rows[0].Cells[i].Text,@"\s")) {
					MsgBox.Show(this,"Column names cannot contain spaces.");
					return;
				}
				if(Regex.IsMatch(gridMain.Rows[0].Cells[i].Text,@"\W")) {//W=non-word chars
					MsgBox.Show(this,"Column names cannot contain special characters.");
					return;
				}
			}
			//Check for reserved words--------------------------------------------------------------------------------
			for(int i=0;i<gridMain.Columns.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(DbHelper.isMySQLReservedWord(gridMain.Rows[0].Cells[i].Text)) {
					MessageBox.Show(Lan.g(this,"Column name is a reserved word in MySQL")+":"+gridMain.Rows[0].Cells[i].Text);
					return;
				}
				//primary key is caught by duplicate column name logic.
			}
			//Check for duplicates-----------------------------------------------------------------------------------
			List<string> listColNamesCheck=new List<string>();
			for(int i=0;i<gridMain.Columns.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(listColNamesCheck.Contains(gridMain.Rows[0].Cells[i].Text)) {
					MessageBox.Show(Lan.g(this,"Duplicate column name detected")+":"+gridMain.Rows[0].Cells[i].Text);
					return;
				}
				listColNamesCheck.Add(gridMain.Rows[0].Cells[i].Text);
			}
			//Validate column widths---------------------------------------------------------------------------------
			for(int i=0;i<gridMain.Columns.Count;i++) {//ODGridCell colNameCell in gridMain.Rows[0].Cells){
				if(Regex.IsMatch(gridMain.Rows[1].Cells[i].Text,@"\D")) {// "\D" matches any non-decimal character
					MsgBox.Show(this,"Column widths must only contain positive integers.");
					return;
				}
				if(gridMain.Rows[1].Cells[i].Text.Contains("-")
					|| gridMain.Rows[1].Cells[i].Text.Contains(".")
					|| gridMain.Rows[1].Cells[i].Text.Contains(",")) {//inlcude the comma for international support. For instance Pi = 3.1415 or 3,1415 depending on your region
					MsgBox.Show(this,"Column widths must only contain positive integers.");
					return;
				}
			}
			//save values to List<WikiListHeaderWidth> TableHeaders
			for(int i=0;i<ListTableHeaders.Count;i++) {
				ListTableHeaders[i].ColName=PIn.String(gridMain.Rows[0].Cells[i].Text);
				ListTableHeaders[i].ColWidth=PIn.Int(gridMain.Rows[1].Cells[i].Text);
			}
			//Save data to database-----------------------------------------------------------------------------------
			try {
				WikiListHeaderWidths.UpdateNamesAndWidths(WikiListCurName,ListTableHeaders);
			}
			catch (Exception ex){
				MessageBox.Show(ex.Message);//will throw exception if table schema has changed since the window was opened.
				DialogResult=DialogResult.Cancel;
			}
			DataValid.SetInvalid(InvalidType.Wiki);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}