using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormVaccineDefEdit:Form {
		public VaccineDef VaccineDefCur;
		public bool IsNew;

		public FormVaccineDefEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormVaccineDefEdit_Load(object sender,EventArgs e) {
			textCVXCode.Text=VaccineDefCur.CVXCode;
			textVaccineName.Text=VaccineDefCur.VaccineName;
			for(int i=0;i<DrugManufacturers.Listt.Count;i++) {
				comboManufacturer.Items.Add(DrugManufacturers.Listt[i].ManufacturerCode + " - " + DrugManufacturers.Listt[i].ManufacturerName);
				if(DrugManufacturers.Listt[i].DrugManufacturerNum==VaccineDefCur.DrugManufacturerNum) {
					comboManufacturer.SelectedIndex=i;
				}
			}
		}

		private void butCvxSelect_Click(object sender,EventArgs e) {
			FormCvxs FormC=new FormCvxs();
			FormC.IsSelectionMode=true;
			FormC.ShowDialog();
			if(FormC.DialogResult!=DialogResult.OK) {
				return;
			}
			textCVXCode.Text=FormC.SelectedCvx.CvxCode;
			textVaccineName.Text=FormC.SelectedCvx.Description;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			try {
				VaccineDefs.Delete(VaccineDefCur.VaccineDefNum);
			}
			catch(ApplicationException ex) {
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textCVXCode.Text=="" || textVaccineName.Text=="") {
				MsgBox.Show(this,"Blank fields are not allowed.");
				return;
			}
			if(comboManufacturer.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a manufacturer.");
				return;
			}
			VaccineDefCur.CVXCode=textCVXCode.Text;
			VaccineDefCur.VaccineName=textVaccineName.Text;
			VaccineDefCur.DrugManufacturerNum=DrugManufacturers.Listt[comboManufacturer.SelectedIndex].DrugManufacturerNum;
			if(IsNew) {
				for(int i=0;i<VaccineDefs.Listt.Count;i++) {
					//We used to block creation if the CVX code already existed.
					//For EHR immunization testing, we had to allow duplicate CVX codes, because some test cases had the same CVX codes but different manufacturers.
					if(VaccineDefs.Listt[i].CVXCode==textCVXCode.Text && VaccineDefs.Listt[i].DrugManufacturerNum==VaccineDefCur.DrugManufacturerNum) {
						MsgBox.Show(this,"CVX Code already exists for the chosen manufacturer.");
						return;
					}
				}
				VaccineDefs.Insert(VaccineDefCur);
			}
			else {
				VaccineDefs.Update(VaccineDefCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}