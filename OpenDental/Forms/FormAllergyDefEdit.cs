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
		private Snomed snomedAllergicTo;

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
			comboSnomedAllergyType.SelectedIndex=(int)AllergyDefCur.SnomedType;
			//snomedAllergicTo=Snomeds.GetByCode(AllergyDefCur.SnomedAllergyTo);// TODO: change to Unii
			if(snomedAllergicTo!=null) {
				//textSnomedAllergicTo.Text=snomedAllergicTo.Description;
			}
			textMedication.Text=Medications.GetDescription(AllergyDefCur.MedicationNum);
		}

		private void butUniiToSelect_Click(object sender,EventArgs e) {
			//FormSnomeds formS=new FormSnomeds();
			//formS.IsSelectionMode=true;
			//if(formS.ShowDialog()==DialogResult.OK) {
			//	snomedAllergicTo=formS.SelectedSnomed;
			//	//textSnomedAllergicTo.Text=snomedAllergicTo.Description;
			//}
			//TODO: Implement similar code for Unii
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

		private void butNoneUniiTo_Click(object sender,EventArgs e) {
			//TODO: Implement this
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
			AllergyDefCur.SnomedType=(SnomedAllergy)comboSnomedAllergyType.SelectedIndex;
			//AllergyDefCur.SnomedAllergyTo="";//TODO: Change to UNII
			//if(snomedAllergicTo!=null) {
			//	AllergyDefCur.SnomedAllergyTo=snomedAllergicTo.SnomedCode;
			//}
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