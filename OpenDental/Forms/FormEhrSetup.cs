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

		private void butICD9s_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormIcd9s FormE=new FormIcd9s();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"ICD9s");
		}

		private void butAllergies_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			} 
			FormAllergySetup FAS=new FormAllergySetup();
			FAS.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"AllergySetup");
		}

		private void butFormularies_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormFormularies FormE=new FormFormularies();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Formularies");
		}

		private void butVaccineDef_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormVaccineDefSetup FormE=new FormVaccineDefSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"VaccineDefSetup");
		}

		private void butDrugManufacturer_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDrugManufacturerSetup FormE=new FormDrugManufacturerSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"FormDrugManufacturerSetup");
		}

		private void butDrugUnit_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDrugUnitSetup FormE=new FormDrugUnitSetup();
			FormE.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"FormDrugUnitSetup");
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}