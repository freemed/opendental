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
		public string PageName;

		public FormWikiRename() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiRename_Load(object sender,EventArgs e) {
			if(PageName!="" && PageName!=null) {
				textPageName.Text=PageName;
				Text="Rename Wiki Page - "+PageName;
			}
			else {
				Text="New Page Name";
			}
		}

		private void ValidateName() {
			//TODO:
			//throw new NotImplementedException();
		}

		private void butOK_Click(object sender,EventArgs e) {
			ValidateName();
			PageName=textPageName.Text;
			//do not save here. Save is handled where this form is called from.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}