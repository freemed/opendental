using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormFormularyMedEdit:Form {
		public FormularyMed ForMed;
		public bool IsNew;

		public FormFormularyMedEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormFormularyMedEdit_Load(object sender,EventArgs e) {
			textMedication.Text=Medications.GetDescription(ForMed.MedicationNum);
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete this formulary medication?")) {
				return;
			}
			FormularyMeds.Delete(ForMed.FormularyMedNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(ForMed.MedicationNum<1) {
				MsgBox.Show(this,"Must select a medication first.");
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butMedicationSelect_Click(object sender,EventArgs e) {
			FormMedications FormM=new FormMedications();
			FormM.IsSelectionMode=true;
			FormM.ShowDialog();
			if(FormM.DialogResult!=DialogResult.OK) {
				return;
			}
			textMedication.Text=Medications.GetDescription(FormM.SelectedMedicationNum);
			ForMed.MedicationNum=FormM.SelectedMedicationNum;
		}
	}
}