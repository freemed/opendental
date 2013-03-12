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
		private List<WikiPage> listWikiPages;
		///<summary>Need a reference to the form where this was launched from so that we can tell it to refresh later.</summary>
		public FormWikiEdit OwnerForm;

		public FormWikiAllPages() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiAllPages_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void textSearch_TextChanged(object sender,EventArgs e) {
			FillGrid();
			//gridMain.SetSelected(0,true);
			//LoadWikiPage(listWikiPages[0]);
		}

		private void LoadWikiPage(WikiPage WikiPageCur) {
			webBrowserWiki.DocumentText=WikiPages.TranslateToXhtml(WikiPageCur.PageContent,false);
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
			listWikiPages=WikiPages.GetByTitleContains(textSearch.Text);
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
			if(OwnerForm!=null && !OwnerForm.IsDisposed) {
				OwnerForm.RefreshPage(listWikiPages[e.Row]);
			}
			Close();
		}

		private void webBrowserWiki_Navigated(object sender,WebBrowserNavigatedEventArgs e) {
			webBrowserWiki.AllowNavigation=false;//to disable links in pages.
		}

		/// <summary></summary>
		private void butBrackets_Click(object sender,EventArgs e) {
			if(OwnerForm!=null && !OwnerForm.IsDisposed) {
				OwnerForm.RefreshPage(null);
			}
			Close();
		}

		/// <summary></summary>
		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a page first.");
				return;
			}
			if(OwnerForm!=null && !OwnerForm.IsDisposed) {
				OwnerForm.RefreshPage(listWikiPages[gridMain.GetSelectedIndex()]);
			}
			Close();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		

		
	}
}