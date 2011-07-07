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
using OpenDental.Forms;
#endif

namespace OpenDental {
	public partial class FormEhrSetup:Form {
		public FormEhrSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrSetup_Load(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.EhrEmergencyNow)) {
				panelEmergencyNow.BackColor=Color.Red;
			}
			else {
				panelEmergencyNow.BackColor=SystemColors.Control;
			}
			if(!Security.IsAuthorized(Permissions.Setup,true)) {
				//Hide all the buttons except Emergency Now and Close.
				butICD9s.Visible=false;
				butAllergies.Visible=false;
				butFormularies.Visible=false;
				butVaccineDef.Visible=false;
				butDrugManufacturer.Visible=false;
				butDrugUnit.Visible=false;
				butInboundEmail.Visible=false;
				butReminderRules.Visible=false;
				butEducationalResources.Visible=false;
				butRxNorm.Visible=false;
				butKeys.Visible=false;
			}
		}

		private void butICD9s_Click(object sender,EventArgs e) {
			FormIcd9s FormE=new FormIcd9s();
			FormE.ShowDialog();
		}

		private void butAllergies_Click(object sender,EventArgs e) {
			FormAllergySetup FAS=new FormAllergySetup();
			FAS.ShowDialog();
		}

		private void butFormularies_Click(object sender,EventArgs e) {
			FormFormularies FormE=new FormFormularies();
			FormE.ShowDialog();
		}

		private void butVaccineDef_Click(object sender,EventArgs e) {
			FormVaccineDefSetup FormE=new FormVaccineDefSetup();
			FormE.ShowDialog();
		}

		private void butDrugManufacturer_Click(object sender,EventArgs e) {
			FormDrugManufacturerSetup FormE=new FormDrugManufacturerSetup();
			FormE.ShowDialog();
		}

		private void butDrugUnit_Click(object sender,EventArgs e) {
			FormDrugUnitSetup FormE=new FormDrugUnitSetup();
			FormE.ShowDialog();
		}

		private void butInboundEmail_Click(object sender,EventArgs e) {
			FormEmailSetupEHR FormES=new FormEmailSetupEHR();
			FormES.ShowDialog();
		}

		private void butEmergencyNow_Click(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.EhrEmergencyNow)) {
				panelEmergencyNow.BackColor=SystemColors.Control;
				Prefs.UpdateBool(PrefName.EhrEmergencyNow,false);
			}
			else {
				panelEmergencyNow.BackColor=Color.Red;
				Prefs.UpdateBool(PrefName.EhrEmergencyNow,true);
			}
			DataValid.SetInvalid(InvalidType.Prefs);
		}
		
		private void butReminderRules_Click(object sender,EventArgs e) {
			FormReminderRules FormRR = new FormReminderRules();
			FormRR.ShowDialog();
		}

		private void butEducationalResources_Click(object sender,EventArgs e) {
			FormEduResourceSetup FormEDUSetup = new FormEduResourceSetup();
			FormEDUSetup.ShowDialog();
		}

		private void butRxNorm_Click(object sender,EventArgs e) {
			FormRxNorms formR=new FormRxNorms();
			formR.ShowDialog();
		}

		private void butKeys_Click(object sender,EventArgs e) {
			FormEhrQuarterlyKeys formK=new FormEhrQuarterlyKeys();
			formK.ShowDialog();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	}
}