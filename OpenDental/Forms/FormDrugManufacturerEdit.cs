using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDrugManufacturerEdit:Form {
		public DrugManufacturer DrugManufacturerCur;
		public bool IsNew;

		public FormDrugManufacturerEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDrugManufacturerEdit_Load(object sender,EventArgs e) {
			textManufacturerName.Text=DrugManufacturerCur.ManufacturerName.ToString();
			textManufacturerCode.Text=DrugManufacturerCur.ManufacturerCode.ToString();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			else {
				DrugManufacturers.Delete(DrugManufacturerCur.DrugManufacturerNum);
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}