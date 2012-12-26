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
	public partial class FormWikiIncomingLinks:Form {
		public string PageTitleCur;
		public WikiPage JumpToPage;
		private List<WikiPage> ListWikiPages;

		public FormWikiIncomingLinks() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWiki_Load(object sender,EventArgs e) {
			Text="Incoming links to "+PageTitleCur;
			FillGrid();
			if(ListWikiPages.Count==0) {
				MsgBox.Show(this,"This page has no incoming links.");
				Close();
			}
		}

		private void LoadWikiPage(WikiPage wikiPage) {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(wikiPage.PageContent);
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Page Title"),70);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Saved"),42);
			//gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ListWikiPages=WikiPages.GetIncomingLinks(PageTitleCur);
			for(int i=0;i<ListWikiPages.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(ListWikiPages[i].PageTitle);
				//row.Cells.Add(page.DateTimeSaved.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				return;
			}
			webBrowserWiki.AllowNavigation=true;
			LoadWikiPage(ListWikiPages[gridMain.SelectedIndices[0]]);
			gridMain.Focus();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			JumpToPage=ListWikiPages[e.Row];
			DialogResult=DialogResult.OK;
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;//to disable links in pages.
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}