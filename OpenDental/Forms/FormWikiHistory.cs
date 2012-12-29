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
		private List<WikiPageHist> listWikiPageHists;

		public FormWikiHistory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiHistory_Load(object sender,EventArgs e) {
			Rectangle tempWorkAreaRect=System.Windows.Forms.Screen.GetWorkingArea(this);
			Top=0;
			Left=Math.Max(0,(tempWorkAreaRect.Width-1200)/2);
			Width=Math.Min(tempWorkAreaRect.Width,1200);
			Height=tempWorkAreaRect.Height;
			FillGrid();
			gridMain.SetSelected(gridMain.Rows.Count-1,true);
			LoadWikiPage(listWikiPageHists[gridMain.SelectedIndices[0]]);//should never be null.
			Text="Wiki History - "+PageTitleCur;
		}

		private void LoadWikiPage(WikiPageHist WikiPageCur) {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageCur.PageContent);
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
			listWikiPageHists=WikiPageHists.GetByTitle(PageTitleCur);
			listWikiPageHists.Add(WikiPages.GetByTitle(PageTitleCur).ToWikiPageHist());
			for(int i=0;i<listWikiPageHists.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(Userods.GetName(listWikiPageHists[i].UserNum));
				row.Cells.Add((listWikiPageHists[i].IsDeleted?"X":""));
				row.Cells.Add(listWikiPageHists[i].DateTimeSaved.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				return;
			}
			webBrowserWiki.AllowNavigation=true;
			LoadWikiPage(listWikiPageHists[gridMain.SelectedIndices[0]]);
			gridMain.Focus();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			MsgBoxCopyPaste mbox = new MsgBoxCopyPaste(listWikiPageHists[e.Row].PageContent);
			mbox.ShowDialog();
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