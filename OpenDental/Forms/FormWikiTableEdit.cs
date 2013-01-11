using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
using System.Text.RegularExpressions;

namespace OpenDental {
	public partial class FormWikiTableEdit:Form {
		///<summary>Both an incoming and outgoing parameter.</summary>
		public string Markup;
		private DataTable Table;
		private List<string> ColNames;
		///<summary>Widths must always be specified.  Not optional.</summary>
		private List<int> ColWidths;

		public FormWikiTableEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiTableEdit_Load(object sender,EventArgs e) {
			//strategy: when form loads, process incoming markup into column names, column widths, and a data table.
			//grid will need to be filled repeatedly, and it will pull from the above objects rather than the original markup
			//When user clicks ok, the objects are transformed back into markup.
			ParseMarkup();
			FillGrid();
		}

		///<summary>Happens on Load, and will also happen when user manually edits markup.  Recursive.</summary>
		private void ParseMarkup(){
			//{|
			//!Width="100"|Column Heading 1!!Width="150"|Column Heading 2!!Width="75"|Column Heading 3
			//|- 
			//|Cell 1||Cell 2||Cell 3 
			//|-
			//|Cell A||Cell B||Cell C 
			//|}
			Table=new DataTable();
			ColNames=new List<string>();
			ColWidths=new List<int>();
			DataRow row;
			string[] cells;
			string[] lines=Markup.Split(new string[] { "\r\n" },StringSplitOptions.None);
			for(int i=1;i<lines.Length-1;i++) {
				if(lines[i].StartsWith("!")) {//header
					lines[i]=lines[i].Substring(1);//strips off the leading !
					cells=lines[i].Split(new string[] { "!!" },StringSplitOptions.None);
					for(int c=0;c<cells.Length;c++) {
						string colName="";
						if(!Regex.IsMatch(cells[c],@"^(Width="")\d+""\|")) {//e.g. Width="90"| 
							MessageBox.Show("Table is corrupt.  Each header must start with Width=\"#\"|.  Please manually edit the markup in the following window.");
							ManuallyEdit();
							return;
						}
						string width=cells[c].Substring(7);//90"|Column Heading 1
						width=width.Substring(0,width.IndexOf("\""));//90
						ColWidths.Add(PIn.Int(width));
						colName=cells[c].Substring(cells[c].IndexOf("|")+1);
						//}
						//else {
						//	ColWidths.Add(0);
						//	colName=cells[c];
						//}
						ColNames.Add(colName);
						Table.Columns.Add(colName);
					}
					continue;
				}
				if(lines[i].Trim()=="|-") {
					continue;//totally ignore these rows
				}
				//normal row.  Headers will have already been filled
				lines[i]=lines[i].Substring(1);//strips off the leading |
				cells=lines[i].Split(new string[] { "||" },StringSplitOptions.None);
				if(cells.Length!=ColNames.Count || cells.Length!=ColWidths.Count) {
					MessageBox.Show("Table is corrupt.  There are "+ColNames.Count.ToString()+" columns, but row "+((i-1)/2).ToString()
						+" has "+cells.Length.ToString()+" cells.  Please manually edit the markup in the following window.");
					ManuallyEdit();
					return;
				}
				row=Table.NewRow();
				for(int c=0;c<cells.Length;c++) {
					row[c]=cells[c];
				}
				Table.Rows.Add(row);
			}
		}

		private void ManuallyEdit() {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(Markup);
			msgbox.ShowDialog();
			if(msgbox.DialogResult==DialogResult.OK) {
				Markup=msgbox.textMain.Text;
				ParseMarkup();//try again
			}
			else {
				DialogResult=DialogResult.Cancel;
			}
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			for(int c=0;c<ColNames.Count;c++){
				col=new ODGridColumn(ColNames[c],ColWidths[c],true);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Table.Rows.Count;i++){
				row=new ODGridRow();
				row.Height=19;//To handle the isEditable functionality
				for(int c=0;c<ColNames.Count;c++) {
					row.Cells.Add(Table.Rows[i][c].ToString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			
		}

		private void gridMain_CellTextChanged(object sender,EventArgs e) {
			
		}

		///<summary>This is done before generating markup and when adding or removing rows or columns.  FillGrid can't be done until this is done.</summary>
		private void PumpGridIntoTable() {
			//table and grid will always have the same numbers of rows and columns.
			for(int i=0;i<Table.Rows.Count;i++) {
				for(int c=0;c<Table.Columns.Count;c++) {
					Table.Rows[i][c]=gridMain.Rows[i].Cells[c].Text;
				}
			}
		}

		///<summary>Happens when user clicks OK.  Also happens when user wants to manually edit markup.</summary>
		private string GenerateMarkup() {
			StringBuilder strb=new StringBuilder();
			strb.AppendLine("{|");
			strb.Append("!");
			for(int c=0;c<ColWidths.Count;c++) {
				if(c>0) {
					strb.Append("!!");
				}
				if(ColWidths[c]>0) {//otherwise, no width specified for this column.  Dynamic.
					strb.Append("Width=\""+ColWidths[c].ToString()+"\"|");
				}
				strb.Append(ColNames[c]);
			}
			strb.AppendLine();
			for(int i=0;i<Table.Rows.Count;i++) {
				strb.AppendLine("|-");
				strb.Append("|");
				for(int c=0;c<ColWidths.Count;c++) {
					if(c>0) {
						strb.Append("||");
					}
					strb.Append(Table.Rows[i][c].ToString());
				}
				strb.AppendLine();
			}
			strb.Append("|}");
			return strb.ToString();
		}

		private void butManEdit_Click(object sender,EventArgs e) {
			PumpGridIntoTable();
			Markup=GenerateMarkup();
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(Markup);
			msgbox.ShowDialog();
			if(msgbox.DialogResult!=DialogResult.OK) {
				return;
			}
			Markup=msgbox.textMain.Text;
			ParseMarkup();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			PumpGridIntoTable();
			Markup=GenerateMarkup();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}