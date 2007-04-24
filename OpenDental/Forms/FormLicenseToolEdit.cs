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

		ProcLicense procLicense;

		public FormLicenseToolEdit(ProcLicense pProcLicense) {
			procLicense=pProcLicense.Copy();
			InitializeComponent();
			adacode.Text=pProcLicense.ADACode;
			description.Text=pProcLicense.Descript;
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
			procLicense.ADACode=adacode.Text;
			procLicense.Descript=description.Text;
			string errors=FormLicenseTool.IsValidCode(procLicense);
			if(errors!="") {
				MessageBox.Show(errors);
				return;
			}
			//Update edited ADA code to db.
			try{
				ProcLicenses.Update(procLicense);
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
			ProcLicenses.Delete(procLicense);
			this.DialogResult=DialogResult.OK;
			Close();
		}

	}
}