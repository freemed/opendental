using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormLicenseToolEdit:Form {
		public ProcLicense ProcLic;

		public FormLicenseToolEdit() {
			InitializeComponent();
		}

		private void FormLicenseToolEdit_Load(object sender,EventArgs e) {
			textCode.Text=ProcLic.ProcCode;
			textDescription.Text=ProcLic.Descript;
		}

		private void deletebutton_Click(object sender,EventArgs e) {
			ProcLicenses.Delete(ProcLic);
			DialogResult=DialogResult.OK;
		}

		private void okbutton_Click(object sender,EventArgs e) {
			//if(!Regex.IsMatch(textCode.Text,"^D[0-9]{4}$")) {
			//	MessageBox.Show("Code must be in the form D####.");
			//	return;
			//}
			if(textDescription.Text.Length<1) {
				MessageBox.Show("Description must be specified.");
				return;
			}
			//if(!ProcLicenses.IsUniqueCode(textCode.Text)) {
			//	MessageBox.Show("That code already exists.");
			//	return;
			//}
			ProcLic.ProcCode=textCode.Text;
			ProcLic.Descript=textDescription.Text;
			ProcLicenses.Update(ProcLic);
			DialogResult=DialogResult.OK;
		}

		private void cancelbutton_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}