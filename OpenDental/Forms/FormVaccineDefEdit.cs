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
				MsgBox.Show(this,"Bank fields are not allowed.");
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
					if(VaccineDefs.Listt[i].CVXCode==textCVXCode.Text) {
						MsgBox.Show(this,"CVX Code already exists.");
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