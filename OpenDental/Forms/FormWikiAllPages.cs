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
	public partial class FormWikiAllPages:Form {
		public bool IsSelectMode;
		public WikiPage SelectedWikiPage;
		private List<WikiPage> listWikiPages;

		public FormWikiAllPages() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiAllPages_Load(object sender,EventArgs e) {
			if(IsSelectMode) {
				//butOK.Text=Lan.g(this,"");
				butCancel.Font=new Font(FontFamily.GenericMonospace,butCancel.Font.Size);
				butCancel.Text=Lan.g(this,"[[   ]]");
			}
			else{
				butOK.Visible=false;
				butCancel.Text=Lan.g(this,"Close");
			}
			FillGrid();
		}

		private void textSearch_TextChanged(object sender,EventArgs e) {
			FillGrid();
			//gridMain.SetSelected(0,true);
			//LoadWikiPage(listWikiPages[0]);
		}

		private void LoadWikiPage(WikiPage WikiPageCur) {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageCur.PageContent);
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
			listWikiPages=WikiPages.GetCurrent(textSearch.Text);
			for(int i=0;i<listWikiPages.Count;i++) {
				ODGridRow row=new ODGridRow();
				//if(listWikiPages[i].IsDeleted) {//color is not a good way to indicate deleted because it is not self explanatory.  Need another column for Deleted X.
				//	row.ColorText=Color.Red;
				//}
				row.Cells.Add(listWikiPages[i].PageTitle);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			webBrowserWiki.AllowNavigation=true;
			LoadWikiPage(listWikiPages[e.Row]);
			gridMain.Focus();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			if(IsSelectMode) {
				SelectedWikiPage=listWikiPages[e.Row];
				DialogResult=DialogResult.OK;
				return;
			}
			else {//Non select mode.
				//do nothing
				//MsgBoxCopyPaste mbox = new MsgBoxCopyPaste(listWikiPages[gridMain.SelectedIndices[0]].PageContent);
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
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;//to disable links in pages.
		}

		/// <summary>Only visible if IsSelect.</summary>
		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a page first.");
				return;
			}
			SelectedWikiPage=listWikiPages[gridMain.GetSelectedIndex()];
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}