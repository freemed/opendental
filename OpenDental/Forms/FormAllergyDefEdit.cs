using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAllergyDefEdit:Form {
		public AllergyDef AllergyDefCur;

		public FormAllergyDefEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAllergyEdit_Load(object sender,EventArgs e) {
			if(!AllergyDefCur.IsNew) { 
				textDescription.Text=AllergyDefCur.Description;
				checkHidden.Checked=AllergyDefCur.IsHidden;
			}
			for(int i=0;i<Enum.GetNames(typeof(SnomedAllergy)).Length;i++) {
				comboSnomedAllergyType.Items.Add(Enum.GetNames(typeof(SnomedAllergy))[i]);
			}
			comboSnomedAllergyType.SelectedIndex=(int)AllergyDefCur.Snomed;
			textMedication.Text=Medications.GetDescription(AllergyDefCur.MedicationNum);
		}

		private void butMedicationSelect_Click(object sender,EventArgs e) {
			FormMedications FormM=new FormMedications();
			FormM.IsSelectionMode=true;
			FormM.ShowDialog();
			if(FormM.DialogResult!=DialogResult.OK){
				return;
			}
			AllergyDefCur.MedicationNum=FormM.SelectedMedicationNum;
			textMedication.Text=Medications.GetDescription(AllergyDefCur.MedicationNum);
		}

		private void butNone_Click(object sender,EventArgs e) {
			AllergyDefCur.MedicationNum=0;
			textMedication.Text="";
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDescription.Text.Trim()=="") {
				MsgBox.Show(this,"Description cannot be blank.");
				return;
			}
			AllergyDefCur.Description=textDescription.Text;
			AllergyDefCur.IsHidden=checkHidden.Checked;
			AllergyDefCur.Snomed=(SnomedAllergy)comboSnomedAllergyType.SelectedIndex;
			if(AllergyDefCur.IsNew) {
				AllergyDefs.Insert(AllergyDefCur);
			}
			else {
				AllergyDefs.Update(AllergyDefCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!AllergyDefCur.IsNew) {
				if(!AllergyDefs.DefIsInUse(AllergyDefCur.AllergyDefNum)) {
					AllergyDefs.Delete(AllergyDefCur.AllergyDefNum);
				}
				else {
					MsgBox.Show(this,"Cannot delete allergies in use.");
					return;
				}
			}
			DialogResult=DialogResult.Cancel;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}