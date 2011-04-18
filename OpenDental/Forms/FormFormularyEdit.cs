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
				row.Cells.Add(medication.Notes.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			Cursor=Cursors.Default;
		}

		//private void butAdd_Click(object sender,EventArgs e) {
			//FormMedications FormM=new FormMedications();
			//FormM.FormularyCur=new Formulary();
			//FormM.IsSelectionMode=true;
			//FormM.ShowDialog();
			//if(FormM.DialogResult!=DialogResult.OK) {
			//  return;
			//}
			//FillGrid();
		//}

		private void butOK_Click(object sender,EventArgs e) {
			FormularyCur.Description=textDescription.Text;
			//if(IsNew) {//Used the "+Add" button to open this form. (Add button never visible)
			//	Formularies.Insert(FormularyCur);
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
