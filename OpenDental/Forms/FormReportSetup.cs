using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormReportSetup:Form {
		private bool changed;

		public FormReportSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReportSetup_Load(object sender,EventArgs e) {
			checkReportsProcDate.Checked=PrefC.GetBool(PrefName.ReportsPPOwriteoffDefaultToProcDate);
			checkReportsShowPatNum.Checked=PrefC.GetBool(PrefName.ReportsShowPatNum);
			checkReportProdWO.Checked=PrefC.GetBool(PrefName.ReportPandIschedProdSubtractsWO);
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(Prefs.UpdateBool(PrefName.ReportsPPOwriteoffDefaultToProcDate,checkReportsProcDate.Checked)
				| Prefs.UpdateBool(PrefName.ReportsShowPatNum,checkReportsShowPatNum.Checked)
				| Prefs.UpdateBool(PrefName.ReportPandIschedProdSubtractsWO,checkReportProdWO.Checked)
				) {
				changed=true;
			}
			if(changed) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}