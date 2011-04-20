using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDrugUnitSetup:Form {
		public FormDrugUnitSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDrugUnitSetup_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			DrugUnits.RefreshCache();
			listMain.Items.Clear();
			for(int i=0;i<DrugUnits.Listt.Count;i++) {
				listMain.Items.Add(DrugUnits.Listt[i].UnitIdentifier + " - " + DrugUnits.Listt[i].UnitText);
			}
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			FormDrugUnitEdit FormD=new FormDrugUnitEdit();
			FormD.DrugUnitCur=DrugUnits.Listt[listMain.SelectedIndex];
			FormD.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormDrugUnitEdit FormD=new FormDrugUnitEdit();
			FormD.DrugUnitCur=new DrugUnit();
			FormD.IsNew=true;
			FormD.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}