using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormNewCropBilling:Form {

		public FormNewCropBilling() {
			InitializeComponent();
		}

		private void butBrowse_Click(object sender,EventArgs e) {
			if(openFileDialog1.ShowDialog()==DialogResult.OK) {
				textBillingXmlPath.Text=openFileDialog1.FileName;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!File.Exists(textBillingXmlPath.Text)) {
				MessageBox.Show("File does not exist or could not be accessed. Make sure the file is not open in another program and try again.");
			}
			FormNewCropBillingList form=new FormNewCropBillingList(textBillingXmlPath.Text);
			form.ShowDialog();			
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
