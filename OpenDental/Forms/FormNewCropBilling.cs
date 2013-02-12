using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormNewCropBilling:Form {

		public FormNewCropBilling() {
			InitializeComponent();
		}

		private void butPaste_Click(object sender,EventArgs e) {
			try {
				textBoxHTML.Text=Clipboard.GetText();
			}
			catch(Exception ex) {
				MessageBox.Show("Failed to paste: "+ex.Message);
			}
		}

		private void butGo_Click(object sender,EventArgs e) {
			FormNewCropBillingList form=new FormNewCropBillingList(textBoxHTML.Text);
			form.ShowDialog();			
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
