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
		}

		private void butChangePatientInto_Click(object sender,EventArgs e) {
			FormPatientSelect fps=new FormPatientSelect();
			if(fps.ShowDialog()==DialogResult.OK){
				this.textPatientIDInto.Text=fps.SelectedPatNum.ToString();
				Patient pat=Patients.GetPat(fps.SelectedPatNum);
				this.textPatientNameInto.Text=pat.GetNameFLFormal();
				this.textPatToBirthdate.Text=pat.Birthdate.ToShortDateString();
			}
			CheckUIState();
		}

		private void butChangePatientFrom_Click(object sender,EventArgs e) {
			FormPatientSelect fps=new FormPatientSelect();
			if(fps.ShowDialog()==DialogResult.OK) {
				this.textPatientIDFrom.Text=fps.SelectedPatNum.ToString();
				Patient pat=Patients.GetPat(fps.SelectedPatNum);
				this.textPatientNameFrom.Text=pat.GetNameFLFormal();
				this.textPatFromBirthdate.Text=pat.Birthdate.ToShortDateString();
			}
			CheckUIState();
		}

		private void CheckUIState(){
			this.butMerge.Enabled=(this.textPatientIDInto.Text.Trim()!="" && this.textPatientIDFrom.Text.Trim()!="");
		}

		private void butMerge_Click(object sender,EventArgs e) {
			long patTo=Convert.ToInt64(this.textPatientIDInto.Text.Trim());
			long patFrom=Convert.ToInt64(this.textPatientIDFrom.Text.Trim());
			if(patTo==patFrom){
				MsgBox.Show(this,"Cannot merge a patient account into itself. Please select a different patient to merge from.");
				return;
			}
			Patient patientFrom=Patients.GetPat(patFrom);
			Patient patientTo=Patients.GetPat(patTo);
			if(patientFrom.FName.Trim().ToLower()!=patientTo.FName.Trim().ToLower() ||
				patientFrom.LName.Trim().ToLower()!=patientTo.LName.Trim().ToLower() ||
				patientFrom.Birthdate!=patientTo.Birthdate){
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"WARNING: The two selected patients either have different "
					+"names or different birthdates. Continue merging the patient at the bottom into the patient shown at the top?")) {
					return;//The user chose not to merge.
				}
			}else{
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"Merge the patient at the bottom into the patient shown at the top?")) {
					return;//The user chose not to merge.
				}
			}
			this.Cursor=Cursors.WaitCursor;
			if(patientFrom.PatNum==patientFrom.Guarantor){
				Family fam=Patients.GetFamily(patFrom);
				if(fam.ListPats.Length>1){
					this.Cursor=Cursors.Default;
					if(!MsgBox.Show(this,MsgBoxButtons.YesNo,
						"The patient you have chosen to merge from is a guarantor. Merging this patient into another account will "
						+"cause all familiy members of the patient being merged from to be moved into the same family as the "
						+"patient account being merged into. Do you wish to continue with the merge?")) {
						return;//The user chose not to merge.
					}
					this.Cursor=Cursors.WaitCursor;
				}
			}
			Patients.MergeTwoPatients(patTo,patFrom);
			this.textPatientIDFrom.Text="";
			this.textPatientNameFrom.Text="";
			this.textPatFromBirthdate.Text="";
			CheckUIState();
			MsgBox.Show(this,"Patients merged successfully.");
			this.Cursor=Cursors.Default;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}