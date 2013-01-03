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
	public partial class FormWikiFileFolder:Form {
		public bool IsFolderMode;

		public FormWikiFileFolder() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiFileFolder_Load(object sender,EventArgs e) {
			if(IsFolderMode) {
				Text=Lan.g(this,"Insert Folder Link");
			}
		}

	}
}