using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile {
	public partial class FormSetup:Form {
		//private bool changed;

		public FormSetup() {
			InitializeComponent();
		}

		private void FormSetup_Load(object sender,EventArgs e) {
			textPath.Text=PrefC.GetString("ImportPath").Replace(@"\","/");
		}

		private void FormSetup_Closing(object sender,CancelEventArgs e) {
			string newPath=textPath.Text.Replace("/",@"\");
			if(!Directory.Exists(newPath)) {
				//MessageBox.Show(MessageBoxButtons.
				if(!MsgBox.Show("Please enter a valid path.  Click Cancel to exit program.",true)){
					Application.Exit();
				}
				e.Cancel=true;
				return;
			}
			Prefs.UpdateString("ImportPath",newPath);

			
		}
	}
}