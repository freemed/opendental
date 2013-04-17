using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPatientPortalSetup:Form {
		public FormPatientPortalSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPatientPortalSetup_Load(object sender,EventArgs e) {
			textPatientPortalURL.Text=PrefC.GetString(PrefName.PatientPortalURL);
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!textPatientPortalURL.Text.ToUpper().StartsWith("HTTPS")) {
				MsgBox.Show(this,"Patient Portal URL must start with HTTPS.");
				return;
			}
			Prefs.UpdateString(PrefName.PatientPortalURL,textPatientPortalURL.Text);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}