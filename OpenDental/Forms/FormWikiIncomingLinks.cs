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
		public WikiPage selectedWikiPage;
		private List<WikiPage> listWikiPages;
		private WikiPage PageMaster;
		private WikiPage PageStyle;
		private string AggregateContent;

		public FormWikiIncomingLinks() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWiki_Load(object sender,EventArgs e) {
			Text="Incoming links to "+PageTitleCur;
			PageMaster=WikiPages.GetMaster();
			PageStyle=WikiPages.GetStyle();
			FillGrid();
			if(listWikiPages.Count>0) {
				LoadWikiPage(listWikiPages[0]);
			}
			else {
				MsgBox.Show(this,"This page has no incoming links.");
				DialogResult=DialogResult.Cancel;
			}
		}

		private void LoadWikiPage(WikiPage WikiPageCur) {
			AggregateContent=PageMaster.PageContent;
			AggregateContent=AggregateContent.Replace("@@@Title@@@",WikiPageCur.PageTitle);
			AggregateContent=AggregateContent.Replace("@@@Style@@@",PageStyle.PageContent);
			AggregateContent=AggregateContent.Replace("@@@Content@@@",WikiPageCur.PageContent);
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(AggregateContent);
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Page Title"),70);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Saved"),42);
			//gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			listWikiPages=WikiPages.GetIncomingLinks(PageTitleCur);
			foreach(WikiPage page in listWikiPages) {
				ODGridRow row=new ODGridRow();
				if(page.IsDeleted) {
					row.ColorText=Color.Red;
				}
				row.Cells.Add(page.PageTitle);
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
			LoadWikiPage(listWikiPages[gridMain.SelectedIndices[0]]);
			gridMain.Focus();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			selectedWikiPage=listWikiPages[gridMain.SelectedIndices[0]];
			DialogResult=DialogResult.OK;
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;//to disable links in pages.
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				DialogResult=DialogResult.Cancel;
			}
			selectedWikiPage=listWikiPages[gridMain.SelectedIndices[0]];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}