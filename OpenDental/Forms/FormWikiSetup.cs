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
		public WikiPage MasterPage;
		public WikiPage StyleSheet;

		public FormWikiSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiSetup_Load(object sender,EventArgs e) {
			MasterPage=WikiPages.GetMaster();
			MasterPage.UserNum=Security.CurUser.UserNum;
			StyleSheet=WikiPages.GetStyle();
			StyleSheet.UserNum=Security.CurUser.UserNum;
			textMaster.Text=MasterPage.PageContent;
			textStyle.Text=StyleSheet.PageContent;
		}

		private void butOK_Click(object sender,EventArgs e) {
			MasterPage.PageContent=textMaster.Text;
			StyleSheet.PageContent=textStyle.Text;
			//Do not save here. Save from form that calls this.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}