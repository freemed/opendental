using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
#if DEBUG
using EHR;
#endif

namespace OpenDental {
	public partial class FormEhrSetup:Form {
		public FormEhrSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butAllergies_Click(object sender,EventArgs e) {
			FormAllergySetup FAS=new FormAllergySetup();
			FAS.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butFormularies_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormFormularies FormE=new FormFormularies();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Formularies");
		}

		private void butICD9s_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormIcd9s FormE=new FormIcd9s();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"ICD9s");
		}
	}
}