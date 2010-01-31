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
	public partial class FormPatientMerge:Form {

		public FormPatientMerge() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPatientMerge_Load(object sender,EventArgs e) {
			this.gridMergeFromPatients.BeginUpdate();
			this.gridMergeFromPatients.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Patient ID",80);
			this.gridMergeFromPatients.Columns.Add(col);
			col=new ODGridColumn("Patient Name",120);
			this.gridMergeFromPatients.Columns.Add(col);
			this.gridMergeFromPatients.EndUpdate();
		}

		private void butChangePatientInto_Click(object sender,EventArgs e) {
			FormPatientSelect fps=new FormPatientSelect();
			if(fps.ShowDialog()==DialogResult.OK){
				this.textPatientIDInto.Text=fps.SelectedPatNum.ToString();
				Patient pat=Patients.GetPat(fps.SelectedPatNum);
				this.textPatientNameInto.Text=pat.GetNameFLFormal();
			}
			CheckUIState();
		}

		private void butAddPatientFrom_Click(object sender,EventArgs e) {
			FormPatientSelect fps=new FormPatientSelect();
			if(fps.ShowDialog()==DialogResult.OK){
				//Do not add the patient to the list if already added.
				bool alreadyExists=false;
				for(int i=0;i<this.gridMergeFromPatients.Rows.Count;i++){
					string patnum=this.gridMergeFromPatients.Rows[i].Cells[0].Text;
					if(patnum==fps.SelectedPatNum.ToString()){
						alreadyExists=true;
						break;
					}
				}
				if(!alreadyExists){
					this.gridMergeFromPatients.BeginUpdate();
					ODGridRow row=new ODGridRow();
					ODGridCell cell=new ODGridCell(fps.SelectedPatNum.ToString());
					row.Cells.Add(cell);
					Patient pat=Patients.GetPat(fps.SelectedPatNum);
					cell=new ODGridCell(pat.GetNameFLFormal());
					row.Cells.Add(cell);
					this.gridMergeFromPatients.Rows.Add(row);
					this.gridMergeFromPatients.EndUpdate();
				}
				//Select the most recently chosen patient (even if they were already in the list).
				for(int i=0;i<this.gridMergeFromPatients.Rows.Count;i++) {
					string patnum=this.gridMergeFromPatients.Rows[i].Cells[0].Text;
					if(patnum==fps.SelectedPatNum.ToString()) {
						this.gridMergeFromPatients.SetSelected(i,true);
					}
					else{
						this.gridMergeFromPatients.SetSelected(i,false);
					}
				}
			}
			CheckUIState();
		}

		private void butRemoveSelectedPatientsFrom_Click(object sender,EventArgs e) {
			List<int> selectedIndicies=new List <int> (this.gridMergeFromPatients.SelectedIndices);
			this.gridMergeFromPatients.BeginUpdate();
			List <ODGridRow> rowsKept=new List<ODGridRow> ();
			for(int i=0;i<this.gridMergeFromPatients.Rows.Count;i++){
				if(selectedIndicies.IndexOf(i)<0){
					//Keep the item if it is not selected for removal.
					rowsKept.Add(this.gridMergeFromPatients.Rows[i]);
				}
			}
			this.gridMergeFromPatients.Rows.Clear();
			for(int i=0;i<rowsKept.Count;i++){
				this.gridMergeFromPatients.Rows.Add(rowsKept[i]);
			}
			this.gridMergeFromPatients.EndUpdate();
			CheckUIState();
		}

		private void CheckUIState(){
			this.butMerge.Enabled=(this.textPatientIDInto.Text.Trim()!="" && this.gridMergeFromPatients.Rows.Count>0);
		}

		private void butMerge_Click(object sender,EventArgs e) {
			//Validate name and birthdate match
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Merge all patients from the list into the patient shown at the top?")) {
				return;
			}
			long patTo=Convert.ToInt64(this.textPatientIDInto.Text.Trim());
			for(int i=0;i<this.gridMergeFromPatients.Rows.Count;i++){
				long patFrom=Convert.ToInt64(this.gridMergeFromPatients.Rows[i].Cells[0].Text.Trim());
				Patients.MergeTwoPatients(patTo,patFrom);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}