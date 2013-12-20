using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrExportCCD:Form {
		private Patient _patCur;
		private string _ccd;

		public string CCD { get { return _ccd; } }

		public FormEhrExportCCD(Patient patCur) {
			InitializeComponent();
			Lan.F(this);
			_patCur=patCur;
			_ccd="";
		}

		private void butCheckAll_Click(object sender,EventArgs e) {
			checkAllergy.Checked=true;
			checkEncounter.Checked=true;
			checkFunctionalStatus.Checked=true;
			checkImmunization.Checked=true;
			checkMedication.Checked=true;
			checkPlanOfCare.Checked=true;
			checkProblem.Checked=true;
			checkProcedure.Checked=true;
			checkResult.Checked=true;
			checkReferral.Checked=true;
			checkSocialHistory.Checked=true;
			checkVitalSign.Checked=true;
		}

		private void butCheckNone_Click(object sender,EventArgs e) {
			checkAllergy.Checked=false;
			checkEncounter.Checked=false;
			checkFunctionalStatus.Checked=false;
			checkImmunization.Checked=false;
			checkMedication.Checked=false;
			checkPlanOfCare.Checked=false;
			checkProblem.Checked=false;
			checkProcedure.Checked=false;
			checkResult.Checked=false;
			checkReferral.Checked=false;
			checkSocialHistory.Checked=false;
			checkVitalSign.Checked=false;
		}

		private void butOK_Click(object sender,EventArgs e) {
			_ccd=EhrCCD.GenerateClinicalSummary(_patCur,checkAllergy.Checked,checkEncounter.Checked,checkFunctionalStatus.Checked,checkImmunization.Checked,checkMedication.Checked,checkPlanOfCare.Checked,checkProblem.Checked,checkProcedure.Checked,checkReferral.Checked,checkResult.Checked,checkSocialHistory.Checked,checkVitalSign.Checked,textInstructions.Text);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}