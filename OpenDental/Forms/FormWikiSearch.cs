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
	public partial class FormWikiSearch:Form {
		private List<string> listWikiPageTitles;
		public string wikiPageTitleSelected;

		public FormWikiSearch() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiSearch_Load(object sender,EventArgs e) {
			Rectangle rectWorkingArea=System.Windows.Forms.Screen.GetWorkingArea(this);
			Top=0;
			Left=Math.Max(0,((rectWorkingArea.Width-1200)/2)+rectWorkingArea.Left);
			Width=Math.Min(rectWorkingArea.Width,1200);
			Height=rectWorkingArea.Height;
			FillGrid();
			wikiPageTitleSelected="";
		}

		private void LoadWikiPage(string WikiPageTitleCur) {
			webBrowserWiki.AllowNavigation=true;
			if(checkDeletedOnly.Checked) {
				webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageHists.GetDeletedByTitle(WikiPageTitleCur).PageContent,false);
			}
			else {
				webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPages.GetByTitle(WikiPageTitleCur).PageContent,false);
			}
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Title"),70);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Saved"),42);
			//gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			if(checkDeletedOnly.Checked) {
				listWikiPageTitles=WikiPageHists.GetDeletedPages(textSearch.Text,checkIgnoreContent.Checked);
			}
			else {
				listWikiPageTitles=WikiPages.GetForSearch(textSearch.Text,checkIgnoreContent.Checked);
			}
			for(int i=0;i<listWikiPageTitles.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(listWikiPageTitles[i]);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			webBrowserWiki.DocumentText="";
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			webBrowserWiki.AllowNavigation=true;
			LoadWikiPage(listWikiPageTitles[e.Row]);
			gridMain.Focus();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {			
			//SelectedWikiPage=listWikiPages[e.Row];
			if(checkDeletedOnly.Checked) {
				return;
			}
			wikiPageTitleSelected=listWikiPageTitles[e.Row];
			DialogResult=DialogResult.OK;
		}

		private void textSearch_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkIgnoreContent_CheckedChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkDeletedOnly_CheckedChanged(object sender,EventArgs e) {
			butOK.Enabled=!checkDeletedOnly.Checked;
			FillGrid();
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;//to disable links in pages.
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length>0) {
				wikiPageTitleSelected=listWikiPageTitles[gridMain.SelectedIndices[0]];
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}