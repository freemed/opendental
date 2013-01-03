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

namespace OpenDental {
	public partial class FormWikiHistory:Form {
		public string PageTitleCur;
		private List<WikiPageHist> ListWikiPageHists;

		public FormWikiHistory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiHistory_Load(object sender,EventArgs e) {
			ResizeControls();
			FillGrid();
			gridMain.SetSelected(gridMain.Rows.Count-1,true);//There will always be at least one page in the history
			LoadWikiPage(ListWikiPageHists[gridMain.SelectedIndices[0]]);//should never be null.
			Text="Wiki History - "+PageTitleCur;
		}

		private void ResizeControls() {
			//assuming gridMain, textNumbers do not change width or location.
			Rectangle actualWorkingArea=new Rectangle(294,12,ClientSize.Width-397,ClientSize.Height-24);
			//textNumbers resize
			textNumbers.Height=actualWorkingArea.Height;
			//text resize
			textContent.Top=actualWorkingArea.Top;
			textContent.Height=actualWorkingArea.Height;
			textContent.Left=actualWorkingArea.Left;
			textContent.Width=actualWorkingArea.Width/2-2;
			//Browser resize
			webBrowserWiki.Top=actualWorkingArea.Top;
			webBrowserWiki.Height=actualWorkingArea.Height;
			webBrowserWiki.Left=actualWorkingArea.Left+actualWorkingArea.Width/2+2;
			webBrowserWiki.Width=actualWorkingArea.Width/2-2;
			//Button move
			//butRefresh.Left=ClientSize.Width/2+2;
		}

		private void LoadWikiPage(WikiPageHist WikiPageCur) {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageCur.PageContent);
			textContent.Text=WikiPageCur.PageContent;
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"User"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Del"),25);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Saved"),80);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ListWikiPageHists=WikiPageHists.GetByTitle(PageTitleCur);
			ListWikiPageHists.Add(WikiPages.PageToHist(WikiPages.GetByTitle(PageTitleCur)));
			for(int i=0;i<ListWikiPageHists.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(Userods.GetName(ListWikiPageHists[i].UserNum));
				row.Cells.Add((ListWikiPageHists[i].IsDeleted?"X":""));
				row.Cells.Add(ListWikiPageHists[i].DateTimeSaved.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				return;
			}
			webBrowserWiki.AllowNavigation=true;
			LoadWikiPage(ListWikiPageHists[gridMain.SelectedIndices[0]]);
			gridMain.Focus();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			//MsgBoxCopyPaste mbox = new MsgBoxCopyPaste(ListWikiPageHists[e.Row].PageContent);
			//mbox.ShowDialog();
			//FormWikiEdit FormWE = new FormWikiEdit();
			//FormWE.WikiPageCur=listWikiPages[gridMain.SelectedIndices[0]];
			//FormWE.ShowDialog();
			//if(FormWE.DialogResult!=DialogResult.OK) {
			//  return;
			//}
			//FillGrid();
			//LoadWikiPage(listWikiPages[0]);
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;//to disable links in pages.
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}