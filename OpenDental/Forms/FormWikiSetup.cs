using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormWikiSetup:Form {
		private WikiPage MasterPage;
		private WikiPage StyleSheet;

		public FormWikiSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiSetup_Load(object sender,EventArgs e) {
			MasterPage=WikiPages.GetByTitle("_Master");
			StyleSheet=WikiPages.GetByTitle("_Style");
			textMaster.Text=MasterPage.PageContent;
			textStyle.Text=StyleSheet.PageContent;
		}

		private void butOK_Click(object sender,EventArgs e) {
			MasterPage.PageContent=textMaster.Text;
			MasterPage.UserNum=Security.CurUser.UserNum;
			WikiPages.InsertOrUpdate(MasterPage);
			WikiPages.MasterPage=MasterPage;//This is temporary until we build better caching
			StyleSheet.PageContent=textStyle.Text;
			StyleSheet.UserNum=Security.CurUser.UserNum;
			WikiPages.InsertOrUpdate(StyleSheet);
			WikiPages.StyleSheet=StyleSheet;//Temporary			
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}