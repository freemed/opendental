using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormVaccineDefSetup:Form {
		public FormVaccineDefSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormVaccineDefSetup_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			VaccineDefs.RefreshCache();
			listMain.Items.Clear();
			for(int i=0;i<VaccineDefs.Listt.Count;i++) {
				listMain.Items.Add(VaccineDefs.Listt[i].VaccineName);
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormVaccineDefEdit FormV=new FormVaccineDefEdit();
			FormV.VaccineDefCur=new VaccineDef();
			FormV.IsNew=true;
			FormV.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}