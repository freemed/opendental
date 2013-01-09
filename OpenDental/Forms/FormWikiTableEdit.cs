using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Text.RegularExpressions;

namespace OpenDental {
	public partial class FormWikiTableEdit:Form {
		///<summary>Both an incoming and outgoing parameter.</summary>
		public string TableMarkup;
		private DataTable TableCur;//might not use this and instead use the ODGrid control.

		public FormWikiTableEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiTableEdit_Load(object sender,EventArgs e) {
			if(TableMarkup=="") {
				TableMarkup=
@"{|
!Width=""""|Heading1!!Width=""""|Heading2!!Width=""""|Heading3
|-
|||
|-
|||
|}";
			}
			ProcessMarkupColumnsHelper(TableMarkup);
			FillGrid();
		}

		/// <summary>Fills UI Grid and DataTable.</summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			string[] tableRows = TableMarkup.Replace("{|","").Replace("|}","").Split("|-,\r\n".Split(",".ToCharArray()),StringSplitOptions.RemoveEmptyEntries);
			ODGridColumn col;
			int columnCount=0;
			foreach(string row in tableRows) {//might have 4 header columns and only 3 table columns... or one row might be deficient. etc.
				if(row.Split(new string[] { "!!" },StringSplitOptions.None).Length>columnCount) {
					columnCount=row.Split(new string[] { "!!" },StringSplitOptions.None).Length;
				}
				if(row.Split(new string[] { "||" },StringSplitOptions.None).Length>columnCount) {
					columnCount=row.Split(new string[] { "||" },StringSplitOptions.None).Length;
				}
			}
			//Columns--------------------------------------------------------
			//process tableRows[0] to add columns to dataTable and ODGrid
			string[] cells=tableRows[0].Substring(1).Split(new string[] {"!!"},StringSplitOptions.None);//this also strips off the leading !
			for(int c=0;c<columnCount;c++) {
				if(c>cells.Length) {//add blank column because not enough headers were defined.
					col=new ODGridColumn("",20);//blank column name
					gridMain.Columns.Add(col);
					continue;
				}
				if(Regex.IsMatch(cells[c],@"(Width="")\d+""\|")) {//e.g. Width="90"|
					string width=cells[c].Substring(7);//90"|Column Heading 1
					width=width.Substring(0,width.IndexOf("\""));//90
					string colName=cells[c].Substring(cells[c].IndexOf("|")+1);//column name
					col=new ODGridColumn(colName,int.Parse(width));//No translation.
					gridMain.Columns.Add(col);
					continue;
				}
				//forgiving code, allow columns to not have width defined
				col=new ODGridColumn("",20);//blank column name
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			for(int i=0;i<tableRows.Length;i++) {
				if(i==0) {
					//if first row contains data it should be processed here, otherwise ignored.
					continue;//columns handled above.
				}
				//TODO: Add ODGrid Rows.
			}
			gridMain.EndUpdate();
		}

		private void ProcessMarkupColumnsHelper(string tableMarkup) {
			
		}

		private string GenerateMarkup(DataTable table){
			StringBuilder retVal=new StringBuilder();

			return retVal.ToString();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}