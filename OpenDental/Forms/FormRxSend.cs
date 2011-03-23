using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormRxSend:Form {
		private List<RxPat> listRx;

		public FormRxSend() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRxSend_Load(object sender,EventArgs e) {
			for(int i=0;i<PharmacyC.Listt.Count;i++) {
				comboPharmacy.Items.Add(PharmacyC.Listt[i].StoreName);
			}
			comboPharmacy.SelectedIndex=0;
			FillGrid();
			gridMain.SetSelected(true);
		}

		private void FillGrid() {
			if(PharmacyC.Listt.Count<1) {
				MsgBox.Show(this,"Need to set up at least one pharmacy.");
				return;
			}
			listRx=RxPats.GetMultElectQueueRx(PharmacyC.Listt[comboPharmacy.SelectedIndex].PharmacyNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableQueue","Patient"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Provider"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Rx"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Pharmacy"),150);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listRx.Count;i++) {
				Patient patCur=Patients.GetLim(listRx[i].PatNum);
				row=new ODGridRow();
				row.Cells.Add(Patients.GetNameLF(patCur.LName,patCur.FName,patCur.Preferred,patCur.MiddleI));
				row.Cells.Add(Providers.GetAbbr(listRx[i].ProvNum));
				row.Cells.Add(listRx[i].Drug);
				row.Cells.Add(Pharmacies.GetDescription(listRx[i].PharmacyNum));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}
		
		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				MsgBox.Show(this,"Must select at least one Rx.");
				return;
			}
			Patient patCur=Patients.GetLim(listRx[gridMain.SelectedIndices[0]].PatNum);
			FormRxEdit FormRE=new FormRxEdit(patCur,listRx[gridMain.SelectedIndices[0]]);
			FormRE.ShowDialog();
			FillGrid();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}
		
		private void butAll_Click(object sender,EventArgs e) {
			gridMain.SetSelected(true);
		}

		private void butNone_Click(object sender,EventArgs e) {
			gridMain.SetSelected(false);
		}

		private void butSend_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				MsgBox.Show(this,"Must select at least one Rx.");
				return;
			}
			//TODO: Loop through selected indicies and send rx's.
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}