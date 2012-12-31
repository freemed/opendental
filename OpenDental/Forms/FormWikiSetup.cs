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

		public FormWikiSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiSetup_Load(object sender,EventArgs e) {
			textMaster.Text=WikiPages.MasterPage.PageContent;
		}

		private void butOK_Click(object sender,EventArgs e) {
			WikiPage masterPage=WikiPages.MasterPage;
			masterPage.PageContent=textMaster.Text;
			masterPage.UserNum=Security.CurUser.UserNum;
			WikiPages.InsertAndArchive(masterPage);
			DataValid.SetInvalid(InvalidType.WikiMaster);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}