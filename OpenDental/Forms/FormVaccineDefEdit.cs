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
			textCVXCode.Text=VaccineDefCur.CVXCode.ToString();
			textVaccineName.Text=VaccineDefCur.VaccineName.ToString();
			for(int i=0;i<DrugManufacturers.Listt.Count;i++) {
				comboManufacturer.Items.Add(DrugManufacturers.Listt[i].ManufacturerName);
				if(DrugManufacturers.Listt[i].DrugManufacturerNum==VaccineDefCur.DrugManufacturerNum) {
					comboManufacturer.SelectedIndex=i;
				}
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}