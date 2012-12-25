using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormWikiRename:Form {
		public string PageTitle;

		public FormWikiRename() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiRename_Load(object sender,EventArgs e) {
			if(PageTitle!="" && PageTitle!=null) {
				textPageTitle.Text=PageTitle;
				Text="Page Title - "+PageTitle;
				textPageTitle.Text=PageTitle;
			}
			if(textPageTitle.Text=="Home") {//TODO:replace this with a dynamic "Home" pagename lookup like: PrefC.GetString(PrefName.WikiHomePage);
				MsgBox.Show(this,"Cannot rename the default homepage.");
				butOK.Enabled=false;
				textPageTitle.Enabled=false;
			}
		}

		private bool ValidateTitle() {
			if(textPageTitle.Text=="") {
				MsgBox.Show(this,"Page title cannot be empty.");
				return false;
			}
			if(textPageTitle.Text==PageTitle) {
				//"rename" to the same thing.
				DialogResult=DialogResult.Cancel;
				return false;
			}
			if(textPageTitle.Text.StartsWith("_")) {
				MsgBox.Show(this,"Page title cannot start with the underscore character.");
				return false;
			}
			if(WikiPages.GetByTitle(textPageTitle.Text)!=null){
				MsgBox.Show(this,"Page title already exists.");
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!ValidateTitle()) {
				return;
			}
			PageTitle=textPageTitle.Text;
			//do not save here. Save is handled where this form is called from.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}