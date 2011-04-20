using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDrugManufacturerSetup:Form {
		public FormDrugManufacturerSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDrugManufacturerSetup_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			DrugManufacturers.RefreshCache();
			listMain.Items.Clear();
			for(int i=0;i<DrugManufacturers.Listt.Count;i++) {
				listMain.Items.Add(DrugManufacturers.Listt[i].ManufacturerCode + " - " + DrugManufacturers.Listt[i].ManufacturerName);
			}
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			FormDrugManufacturerEdit FormD=new FormDrugManufacturerEdit();
			FormD.DrugManufacturerCur=DrugManufacturers.Listt[listMain.SelectedIndex];
			FormD.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormDrugManufacturerEdit FormD=new FormDrugManufacturerEdit();
			FormD.DrugManufacturerCur=new DrugManufacturer();
			FormD.IsNew=true;
			FormD.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}