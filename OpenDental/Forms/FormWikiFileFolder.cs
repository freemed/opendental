using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormWikiFileFolder:Form {
		public bool IsFolderMode;
		public string SelectedLink;

		public FormWikiFileFolder() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiFileFolder_Load(object sender,EventArgs e) {
			if(IsFolderMode) {
				Text=Lan.g(this,"Insert Folder Link");
			}
		}

		private void butBrowse_Click(object sender,EventArgs e) {
			if(IsFolderMode) {
				FolderBrowserDialog folderBD=new FolderBrowserDialog();
				if(folderBD.ShowDialog()!=DialogResult.OK) {
					return;
				}
				textLink.Text=folderBD.SelectedPath;
				return;
			}
			OpenFileDialog openFileD=new OpenFileDialog();
			if(openFileD.ShowDialog()!=DialogResult.OK) {
				return;
			}
			textLink.Text=openFileD.FileName;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(IsFolderMode && !Directory.Exists(textLink.Text)) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Folder does not exist. Would you like to create it?")) {
					return;
				}
				try {
					Directory.CreateDirectory(textLink.Text);
				}
				catch(Exception ex) {
					MessageBox.Show(this,ex.Message);
					return;
				}
			}
			else if(!File.Exists(textLink.Text)){//file mode
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"File does not exist. Would you like to create the link anyways?")) {
					return;
				}
			}
			SelectedLink=textLink.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}