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
	public partial class FormWikiTableEdit:Form {
		///<summary>Both an incoming and outgoing parameter.</summary>
		public string Markup;
		///<summary>Both an incoming and outgoing parameter.</summary>
		public string MarkupForViews;
		///<summary>This is set when loading to help determine later whether user is allowed to add a tableview.</summary>
		private bool MarkupForViewsWasBlank;
		private DataTable Table;
		private List<string> ColNames;
		///<summary>Widths must always be specified.  Not optional.</summary>
		private List<int> ColWidths;
		private List<WikiView> Views;
		///<summary>This is passed in from the calling form.  It is used when deciding whether to allow user to add tableviews.  Blocks them if more than one table in page.</summary>
		public int CountTablesInPage;
		public bool IsNew;
		///<summary>SelectedIndex of listView.  This is necessary because when user clicks on listView, the SelectedIndex changes before the mousedown event fires.  This is the only way to know what the view is that we are switching from.</summary>
		private int ViewShowing;

		public FormWikiTableEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiTableEdit_Load(object sender,EventArgs e) {
			//strategy: when form loads, process incoming markup into column names, column widths, and a data table.
			//grid will need to be filled repeatedly, and it will pull from the above objects rather than the original markup
			//When user clicks ok, the objects are transformed back into markup.
			if(MarkupForViews=="") {
				MarkupForViewsWasBlank=true;
			}
			ParseXmlViews();
			FillViews();//this must come before FillGrid
			ViewShowing=0;//none
			SetVisibilityForView();
			ParseMarkup();
			FillGrid();
		}

		///<summary>Happens on Load only.</summary>
		private void ParseXmlViews() {
			//<TableViews>
			//  <TableView Name="Cols 1&2" OrderBy="Column Heading 1">
			//    <TableViewCol>Column Heading 1</TableViewCol>
			//    <TableViewCol>Column Heading 2</TableViewCol>
			//  </TableView>	
			//  <TableView Name="Cols 2&3 "OrderBy="Column Heading 2">
			//    <TableViewCol>Column Heading 2</TableViewCol>
			//    <TableViewCol>Column Heading 3</TableViewCol>
			//  </TableView> 
			//</TableViews>
			Views=new List<WikiView>();
			if(MarkupForViews=="") {
				return;
			}
			XmlDocument doc=new XmlDocument();
			using(StringReader reader=new StringReader(MarkupForViews)) {
				doc.Load(reader);
			}
			foreach(XmlNode nodeView in doc.DocumentElement.ChildNodes) {//loop through the TableView nodes.
				WikiView view=new WikiView();
				view.ViewName=nodeView.Attributes["Name"].Value;
				XmlAttribute attribOrderBy=nodeView.Attributes["OrderBy"];
				if(attribOrderBy!=null){
					view.OrderBy=attribOrderBy.Value;
				}
				view.Columns=new List<string>();
				foreach(XmlNode nodeCol in nodeView.ChildNodes) {
					view.Columns.Add(nodeCol.InnerXml);
				}
				Views.Add(view);
			}
		}

		///<summary>Set's selectedIndex back to "none", so it may be necessary to set the selectedIndex after this.  Then, run SetVisibilityForView.</summary>
		private void FillViews(){//bool preserveSelectedView) {
			listView.Items.Clear();
			listView.Items.Add(Lan.g(this,"none"));
			listView.SelectedIndex=0;
			ViewShowing=0;
			for(int i=0;i<Views.Count;i++) {
				listView.Items.Add(Views[i].ViewName);
			}
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
				if(listView.SelectedIndex!=0) {
					if(Views[listView.SelectedIndex-1].Columns.Contains(ColNames[c])) {
						col=new ODGridColumn(ColNames[c],ColWidths[c],true);
						gridMain.Columns.Add(col);
					}
				}
				else {
					col=new ODGridColumn(ColNames[c],ColWidths[c],true);
					gridMain.Columns.Add(col);
				}
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Table.Rows.Count;i++){
				row=new ODGridRow();
				row.Height=19;//To handle the isEditable functionality
				for(int c=0;c<ColNames.Count;c++) {
					if(listView.SelectedIndex!=0) {
						if(Views[listView.SelectedIndex-1].Columns.Contains(ColNames[c])) {
							row.Cells.Add(Table.Rows[i][c].ToString());
						}
					}
					else{
						row.Cells.Add(Table.Rows[i][c].ToString());
					}
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			//new big window to edit row
		}

		private void gridMain_CellTextChanged(object sender,EventArgs e) {
			
		}

		private void gridMain_CellLeave(object sender,ODGridClickEventArgs e) {
			Table.Rows[e.Row][e.Col]=gridMain.Rows[e.Row].Cells[e.Col].Text;
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

		///<summary>Happens only when user clicks OK.</summary>
		private string GenerateMarkupForViews() {
			//<TableViews>
			//  <TableView Name="Cols 1&2" OrderBy="Column Heading 1">
			//    <TableViewCol>Column Heading 1</TableViewCol>
			//    <TableViewCol>Column Heading 2</TableViewCol>
			//  </TableView>	
			//  <TableView Name="Cols 2&3 "OrderBy="Column Heading 2">
			//    <TableViewCol>Column Heading 2</TableViewCol>
			//    <TableViewCol>Column Heading 3</TableViewCol>
			//  </TableView> 
			//</TableViews>
			if(Views.Count==0) {
				return "";
			}
			StringBuilder strb=new StringBuilder();
			XmlWriterSettings settings=new XmlWriterSettings();
			settings.Indent=true;
			settings.IndentChars="  ";
			settings.OmitXmlDeclaration=true;
			settings.NewLineChars="\r\n";
			using(XmlWriter writer=XmlWriter.Create(strb,settings)) {
				writer.WriteStartElement("TableViews");
				for(int i=0;i<Views.Count;i++) {
					writer.WriteStartElement("TableView");
					writer.WriteAttributeString("Name",Views[i].ViewName);
					if(Views[i].OrderBy!=null && Views[i].OrderBy!="") {
						writer.WriteAttributeString("OrderBy",Views[i].OrderBy);
					}
					for(int c=0;c<Views[i].Columns.Count;c++) {
						writer.WriteElementString("TableViewCol",Views[i].Columns[c]);
					}
					writer.WriteEndElement();//TableView
				}
				writer.WriteEndElement();//TableViews
			}
			return strb.ToString();
		}

		private void butManEdit_Click(object sender,EventArgs e) {
			//PumpGridIntoTable();
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

		private void butColumnLeft_Click(object sender,EventArgs e) {
			//check to make sure you're not already at the left
			//we are guaranteed to have the Table and the gridMain synched.  Same # of rows and columns.
			//swap ColNames
			//swap ColWidths
			//Loop through table rows.
			//  Swap 2 cells.  Remember one of the first as part of the swap.
			FillGrid();
		}

		private void butColumnRight_Click(object sender,EventArgs e) {

		}

		private void butHeaders_Click(object sender,EventArgs e) {
			FormWikiTableHeaders FormWTH=new FormWikiTableHeaders();
			FormWTH.ColNames=ColNames;//Shallow copy. Just passes the pointer to the list in memory, so no need to "collect" the changes afterwords.
			FormWTH.ColWidths=ColWidths;//Shallow copy. Just passes the pointer to the list in memory, so no need to "collect" the changes afterwords.
			FormWTH.ShowDialog();
			if(FormWTH.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butColumnInsert_Click(object sender,EventArgs e) {

		}

		private void butColumnDelete_Click(object sender,EventArgs e) {

		}

		private void butRowUp_Click(object sender,EventArgs e) {
			if(gridMain.SelectedCell.Y==-1) {
				MsgBox.Show(this,"Please select a cell first.");
				return;
			}
		}

		private void butRowDown_Click(object sender,EventArgs e) {
			//Table.Rows.InsertAt
			//DataRow row=Table.Rows[i];
			//Table.Rows.RemoveAt
		}

		private void butRowInsert_Click(object sender,EventArgs e) {
			//DataRow row=Table.NewRow();
			//Table.Rows.InsertAt(row,i);
		}

		private void butRowDelete_Click(object sender,EventArgs e) {

		}

		private void listView_Click(object sender,EventArgs e) {
			SetVisibilityForView();
//instead of this, always save to table as cells are edited?  This is getting too complex.
			//PumpGridIntoTable();
			FillGrid();
			ViewShowing=listView.SelectedIndex;
		}

		private void listView_DoubleClick(object sender,EventArgs e) {
			int selectedIdx=listView.SelectedIndex;
			if(selectedIdx < 1) {//-1 or 0
				return;
			}
			FormWikiTableViewEdit formW=new FormWikiTableViewEdit();
			formW.WikiViewCur=Views[selectedIdx-1];
			formW.ColsAll=new List<string>(ColNames);
			formW.IsNew=false;
			formW.ShowDialog();
			if(formW.DialogResult!=DialogResult.OK) {
				return;
			}
			if(formW.WikiViewCur==null) {//deleted
				Views.RemoveAt(selectedIdx-1);
				FillViews();//selectedIndex gets lost
				ViewShowing=0;
				SetVisibilityForView();
				return;
			}
			Views[selectedIdx-1]=formW.WikiViewCur;//I assume this needs to be done
			FillViews();//selectedIndex gets lost
			listView.SelectedIndex=selectedIdx;
			SetVisibilityForView();
		}

		///<summary>This is run as part of FillViews and also when user selects a view from the combo box.</summary>
		private void SetVisibilityForView(){
			if(listView.SelectedIndex==0) {
				gridMain.Enabled=true;
				butColumnLeft.Enabled=true;
				butColumnRight.Enabled=true;
				butColumnInsert.Enabled=true;
				butColumnDelete.Enabled=true;
				groupRow.Enabled=true;
			}
			else {//if a view is selected, then disable many functions
				gridMain.Enabled=false;
				butColumnLeft.Enabled=false;
				butColumnRight.Enabled=false;
				butColumnInsert.Enabled=false;
				butColumnDelete.Enabled=false;
				groupRow.Enabled=false;
			}
		}

		private void butViewAdd_Click(object sender,EventArgs e) {
			if(MarkupForViewsWasBlank){//user is trying to add table views.
				if(IsNew) {//adding another table
					if(CountTablesInPage>0){//and there's already a table
						MsgBox.Show(this,"Cannot add a Table View because this page has more than one table.");
						return;
					}
				}
				else {//editing an existing table
					if(CountTablesInPage>1){//and there are other tables
						MsgBox.Show(this,"Cannot add a Table View because this page has more than one table.");
						return;
					}
				}
			}
			WikiView wikiview=new WikiView();
			wikiview.ViewName="View"+listView.Items.Count.ToString();
			wikiview.OrderBy="";
			wikiview.Columns=new List<string>(ColNames);//start off showing all the columns
			FormWikiTableViewEdit formW=new FormWikiTableViewEdit();
			formW.WikiViewCur=wikiview;
			formW.ColsAll=new List<string>(ColNames);
			formW.IsNew=true;
			formW.ShowDialog();
			if(formW.DialogResult!=DialogResult.OK) {
				return;
			}
			Views.Add(formW.WikiViewCur);
			FillViews();
			listView.SelectedIndex=listView.Items.Count-1;
			SetVisibilityForView();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete this entire table?")) {
				return;
			}
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			else {
				Markup=null;
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			//PumpGridIntoTable();
			Markup=GenerateMarkup();
			MarkupForViews=GenerateMarkupForViews();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		
	

	

		

		

		

	

	}
}