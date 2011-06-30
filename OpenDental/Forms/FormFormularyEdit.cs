using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using OpenDental;

namespace OpenDental {
	public partial class FormFormularyEdit:Form {
		private List<FormularyMed> ListMeds;
		public Formulary FormularyCur;
		public bool IsNew;

		public FormFormularyEdit() {
			InitializeComponent();
		}

		private void FormFormularyEdit_Load(object sender,EventArgs e) {
			textDescription.Text=FormularyCur.Description;
			FillGrid();
		}

		private void FillGrid() {
			Cursor=Cursors.WaitCursor;
			Medications.Refresh();
			ListMeds=FormularyMeds.GetMedsForFormulary(FormularyCur.FormularyNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Name",150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Notes",250);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			Medication medication;
			for(int i=0;i<ListMeds.Count;i++) {
				medication=Medications.GetMedication(ListMeds[i].MedicationNum);
				row=new ODGridRow();
				row.Cells.Add(medication.MedName.ToString());
				row.Cells.Add(Medications.GetMedication(medication.GenericNum).Notes.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			Cursor=Cursors.Default;
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormFormularyMedEdit FormFME=new FormFormularyMedEdit();
			FormFME.ForMed=new FormularyMed();
			FormFME.IsNew=true;
			FormFME.ShowDialog();
			if(FormFME.DialogResult!=DialogResult.OK) {
				return;
			}
			FormFME.ForMed.FormularyNum=FormularyCur.FormularyNum;
			FormularyMeds.Insert(FormFME.ForMed);
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete this formulary?")) {
				return;
			}
			FormularyMeds.DeleteMedsForFormulary(FormularyCur.FormularyNum);
			Formularies.Delete(FormularyCur.FormularyNum);
			DialogResult=DialogResult.OK;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormFormularyMedEdit FormFME=new FormFormularyMedEdit();
			FormFME.ForMed=ListMeds[e.Row];
			FormFME.ShowDialog();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDescription.Text.Trim()=="") {
				MsgBox.Show(this,"Please enter a description.");
				return;
			}
			FormularyCur.Description=textDescription.Text;
			//if(IsNew) {
			//  Formularies.Insert(FormularyCur);
			//}
			//else {
			Formularies.Update(FormularyCur);
			//}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




	}
}
