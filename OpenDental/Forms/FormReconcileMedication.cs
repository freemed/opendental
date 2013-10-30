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
	public partial class FormReconcileMedication:Form {
		public List<MedicationPat> ListMedicationPatNew;
		private List<MedicationPat> ListMedicationReconcile;
		private List<MedicationPat> ListMedicationPatCur;
		public Patient PatCur;

		public FormReconcileMedication() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReconcileMedication_Load(object sender,EventArgs e) {
			PatCur=Patients.GetPat(FormOpenDental.CurPatNum);
			//-------------------------------Delete after testing
			ListMedicationPatNew=new List<MedicationPat>();
			ListMedicationReconcile=MedicationPats.GetMedPatsForReconcile(PatCur.PatNum);
			MedicationPat medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(5));
			medP.MedDescript="Valpax";
			medP.PatNote="Two a day";
			medP.RxCui=542687;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(2));
			medP.MedDescript="Usept";
			medP.PatNote="Three a day";
			medP.RxCui=405384;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(1));
			medP.MedDescript="SmileGuard";
			medP.PatNote="Two a day";
			medP.RxCui=1038751;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(4));
			medP.MedDescript="Slozem";
			medP.PatNote="One a day";
			medP.RxCui=151154;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(6));
			medP.MedDescript="Prax";
			medP.PatNote="Four a day";
			medP.RxCui=219336;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(5));
			medP.MedDescript="PrameGel";
			medP.PatNote="Two a day";
			medP.RxCui=93822;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(7));
			medP.MedDescript="Pramotic";
			medP.PatNote="Five a day";
			medP.RxCui=405268;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(3));
			medP.MedDescript="Medetomidine";
			medP.PatNote="Three a day";
			medP.RxCui=52016;
			ListMedicationPatNew.Add(medP);
			medP=new MedicationPat();
			medP.DateStart=DateTime.Now.Subtract(TimeSpan.FromDays(4));
			medP.MedDescript="Medcodin";
			medP.PatNote="One a day";
			medP.RxCui=218274;
			ListMedicationPatNew.Add(medP);
			//-------------------------------
			bool isValid;
			for(int i=0;i<ListMedicationPatNew.Count;i++) {
				isValid=true;
				for(int j=0;j<ListMedicationReconcile.Count;j++) {
					if(ListMedicationReconcile[j].RxCui==ListMedicationPatNew[i].RxCui) {
						isValid=false;
						break;
					}
				}
				if(isValid) {
					ListMedicationReconcile.Add(ListMedicationPatNew[i]);
				}
			}
			FillNewGrid();
			FillExistingGrid();
			FillReconcileGrid();
		}

		private void FillNewGrid() {
			gridMedImport.BeginUpdate();
			gridMedImport.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",130);
			gridMedImport.Columns.Add(col);
			col=new ODGridColumn("Details",600);
			gridMedImport.Columns.Add(col);
			gridMedImport.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListMedicationPatNew.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListMedicationPatNew[i].DateStart.ToShortDateString());
				row.Cells.Add(ListMedicationPatNew[i].RxCui.ToString());
				gridMedImport.Rows.Add(row);
			}
			gridMedImport.EndUpdate();
		}

		private void FillExistingGrid() {
			gridMedExisting.BeginUpdate();
			gridMedExisting.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",130);
			gridMedExisting.Columns.Add(col);
			col=new ODGridColumn("Details",600);
			gridMedExisting.Columns.Add(col);
			gridMedExisting.Rows.Clear();
			ListMedicationPatCur=MedicationPats.GetMedPatsForReconcile(PatCur.PatNum);
			ODGridRow row;
			for(int i=0;i<ListMedicationPatCur.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListMedicationPatCur[i].DateStart.ToShortDateString());
				row.Cells.Add(ListMedicationPatCur[i].RxCui.ToString());
				gridMedExisting.Rows.Add(row);
			}
			gridMedExisting.EndUpdate();
		}

		private void FillReconcileGrid() {
			gridReconcile.BeginUpdate();
			gridReconcile.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",130);
			gridReconcile.Columns.Add(col);
			col=new ODGridColumn("Details",600);
			gridReconcile.Columns.Add(col);
			gridReconcile.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListMedicationReconcile.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListMedicationReconcile[i].DateTStamp.ToString());
				row.Cells.Add(ListMedicationReconcile[i].RxCui.ToString());
				gridReconcile.Rows.Add(row);
			}
			gridReconcile.EndUpdate();
		}

		private void butAddNew_Click(object sender,EventArgs e) {
			if(gridMedImport.SelectedIndices.Length==0) {
				MsgBox.Show(this,"A row must be selected to add");
				return;
			}
			MedicationPat medP;
			int skipCount=0;
			bool isValid;
			for(int i=0;i<gridMedImport.SelectedIndices.Length;i++) {
				isValid=true;
				//Since gridMedImport and ListMedicationPatNew are a 1:1 list we can use the selected index position to get our medP
				medP=ListMedicationPatNew[gridMedImport.SelectedIndices[i]];
				for(int j=0;j<gridReconcile.Rows.Count;j++) {
					if(medP.RxCui==ListMedicationReconcile[j].RxCui) {
						isValid=false;
						skipCount++;
						break;
					}
				}
				if(isValid) {
					ListMedicationReconcile.Add(medP);
				}
			}
			if(skipCount>0) {
				MessageBox.Show(Lan.g(this,""+skipCount+" row(s) were skipped because the row already exists in the reconciled list."));
			}
			FillReconcileGrid();
		}

		private void butAddExist_Click(object sender,EventArgs e) {
			if(gridMedExisting.SelectedIndices.Length==0) {
				MsgBox.Show(this,"A row must be selected to add");
				return;
			}
			MedicationPat medP;
			int skipCount=0;
			bool isValid;
			for(int i=0;i<gridMedExisting.SelectedIndices.Length;i++) {
				isValid=true;
				//Since gridMedImport and ListMedicationPatNew are a 1:1 list we can use the selected index position to get our medP
				medP=ListMedicationPatCur[gridMedExisting.SelectedIndices[i]];
				for(int j=0;j<gridReconcile.Rows.Count;j++) {
					if(medP.RxCui!=0 && medP.RxCui==ListMedicationReconcile[j].RxCui) {
						isValid=false;
						skipCount++;
						break;
					}
					if(medP.MedicationPatNum==ListMedicationReconcile[j].MedicationPatNum) {
						isValid=false;
						skipCount++;
						break;
					}
				}
				if(isValid) {
					ListMedicationReconcile.Add(medP);
				}
			}
			if(skipCount>0) {
					MessageBox.Show(Lan.g(this,""+skipCount+" row(s) were skipped because the row already exists in the reconciled list."));
			}
			FillReconcileGrid();
		}

		private void butRemoveRec_Click(object sender,EventArgs e) {
			if(gridReconcile.SelectedIndices.Length==0) {
				MsgBox.Show(this,"A row must be selected to remove");
				return;
			}
			MedicationPat medP;
			for(int i=gridReconcile.SelectedIndices.Length-1;i>-1;i--) {//Loop backwards so that we can remove from the list as we go
				medP=ListMedicationReconcile[gridReconcile.SelectedIndices[i]];
				ListMedicationReconcile.Remove(medP);
			}
			FillReconcileGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(ListMedicationReconcile.Count==0) {
				if(!MsgBox.Show(this,true,"The reconcile list is empty. Clicking ok will cause the existing medications to be removed. Continue?")) {
					return;
				}
			}
			List<MedicationPat> listMedicationUpdate=ListMedicationReconcile;//Copy the reconciled list
			bool toUpdate;
			for(int i=0;i<ListMedicationPatCur.Count;i++) {//Start looping through all current medications
				toUpdate=true;
				for(int j=0;j<ListMedicationReconcile.Count;j++) {//Compare each reconcile medication to the current medication
					if(ListMedicationReconcile[j].RxCui!=0 && ListMedicationReconcile[j].RxCui==ListMedicationPatCur[i].RxCui) {//Has an RxNorm code and they are equal
						toUpdate=false;//Don't update the current version
						listMedicationUpdate.Remove(ListMedicationReconcile[j]);//Remove the row from the copied reconcile list
					}
					else if(ListMedicationReconcile[j].MedicationPatNum==ListMedicationPatCur[i].MedicationPatNum) {//Has identical MedicationPatNums
						toUpdate=false;//Don't update the current version
						listMedicationUpdate.Remove(ListMedicationReconcile[j]);//Remove the row from the copied reconcile list
					}
					if(ListMedicationReconcile[j].MedicationNum==0) {//Check if the database has a Medication for the current MedicationPat
						Medication med=new Medication();
						med.MedName=ListMedicationReconcile[j].MedDescript;
						med.RxCui=ListMedicationReconcile[j].RxCui;
						Medications.Insert(med);//Insert new Medication into the database.
					}
				}
				if(toUpdate) {//If toUpdate has not been set to false above
					ListMedicationPatCur[i].DateStop=DateTime.Now;//Set the current DateStop to today (to set the medication as discontinued)
					MedicationPats.Update(ListMedicationPatCur[i]);
				}
			}
			for(int z=0;z<listMedicationUpdate.Count;z++) {//Loop through the copied reconcile list
				listMedicationUpdate[z].PatNum=PatCur.PatNum;//TODO: Remove after testing
				MedicationPats.Insert(listMedicationUpdate[z]);//Insert all remaining rows from the list
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}