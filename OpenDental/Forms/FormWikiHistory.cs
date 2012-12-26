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
		private List<WikiPage> listWikiPages;

		public FormWikiHistory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiHistory_Load(object sender,EventArgs e) {
			FillGrid();
			//LoadWikiPage(listWikiPages[0]);
			Text="Wiki History - "+PageTitleCur;
		}

		private void LoadWikiPage(WikiPage WikiPageCur) {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageCur.PageContent);
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"User"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Saved"),42);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			listWikiPages=WikiPages.GetHistoryByTitle(PageTitleCur);
			for(int i=0;i<listWikiPages.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(Userods.GetName(listWikiPages[i].UserNum));
				row.Cells.Add(listWikiPages[i].DateTimeSaved.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				return;
			}
			webBrowserWiki.AllowNavigation=true;
			LoadWikiPage(listWikiPages[gridMain.SelectedIndices[0]]);
			gridMain.Focus();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			MsgBoxCopyPaste mbox = new MsgBoxCopyPaste(listWikiPages[e.Row].PageContent);
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