using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness.Mobile;

namespace OpenDental {
	public partial class FormMobileSetup:Form {
		public FormMobileSetup() {
			InitializeComponent();
			Lan.F(this);

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
			DateTime changedSince=DateTime.Now;// change this to time obtained from server.
			List<Patientm> ChangedPatientmList=Patientms.GetChanged(changedSince);
			//Synch here
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}