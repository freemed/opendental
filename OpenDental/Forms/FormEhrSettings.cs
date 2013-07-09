using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrSettings:Form {
		public FormEhrSettings() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrSettings_Load(object sender,EventArgs e) {
			checkAlertHighSeverity.Checked=PrefC.GetBool(PrefName.EhrRxAlertHighSeverity);
		}

		private void checkAlertHighSeverity_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.SecurityAdmin,false)) {
				checkAlertHighSeverity.Checked=PrefC.GetBool(PrefName.EhrRxAlertHighSeverity);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			Prefs.UpdateBool(PrefName.EhrRxAlertHighSeverity,checkAlertHighSeverity.Checked);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}