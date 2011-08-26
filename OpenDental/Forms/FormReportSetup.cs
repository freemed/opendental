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
			//TEMP------------------------------------------------------------------------------------------
			butAgg.Visible=false;//TODO: THIS IS TEMPORARY. DELETE THIS LINE WHEN THE AGGREGATOR IS WORKING.
			//TEMP------------------------------------------------------------------------------------------
		}

		private void butAgg_Click(object sender,EventArgs e) {
			FormAggPathSetup FormAPS = new FormAggPathSetup();
			FormAPS.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(Prefs.UpdateBool(PrefName.ReportsPPOwriteoffDefaultToProcDate,checkReportsProcDate.Checked)
				| Prefs.UpdateBool(PrefName.ReportsShowPatNum,checkReportsShowPatNum.Checked)
				| Prefs.UpdateBool(PrefName.ReportPandIschedProdSubtractsWO,checkReportProdWO.Checked)
				) {
				changed=true;
			}
			if(changed) {
				DataValid.SetInvalid(InvalidType.Prefs,InvalidType.Computers);
				ComputerPrefs.Update(ComputerPrefs.LocalComputer);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}