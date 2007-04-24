using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormLicenseToolEdit:Form {

		ProcLicense originalProcLicense;

		public FormLicenseToolEdit(ProcLicense procLicense) {
			originalProcLicense=procLicense.Copy();
			InitializeComponent();
			adacode.Text=procLicense.ADACode;
			description.Text=procLicense.Description;
		}

		private void adacode_KeyPress(object sender,KeyPressEventArgs e) {
			if(e.KeyChar=='\r'){
				e.Handled=true;
				description.Focus();
			}
		}

		private void description_KeyPress(object sender,KeyPressEventArgs e) {
			if(e.KeyChar=='\r') {
				e.Handled=true;
				okbutton.Focus();
			}
		}

		private void okbutton_Enter(object sender,EventArgs e) {
			originalProcLicense.ADACode=adacode.Text;
			originalProcLicense.Description=description.Text;
			string errors=FormLicenseTool.IsValidCode(originalProcLicense);
			if(errors!="") {
				MessageBox.Show(errors);
				return;
			}
			//Update edited code to db.
			try{
				ProcLicenses.Update(originalProcLicense);
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
				adacode.Focus();
				return;
			}
			this.DialogResult=DialogResult.OK;
			Close();
		}

		private void cancelbutton_Click(object sender,EventArgs e) {
			this.DialogResult=DialogResult.Cancel;
			Close();
		}

		private void deletebutton_Click(object sender,EventArgs e) {
			ProcLicenses.Delete(originalProcLicense);
			this.DialogResult=DialogResult.OK;
			Close();
		}

	}
}