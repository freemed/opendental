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
		public Patient PatCur;
		private Bitmap BitmapOriginal;
		private List<EhrMeasureEvent> ehrMeasureEventsList;
		private List<MedicationPat> medList;

		public FormReconcileMedication() {
			InitializeComponent();
			Lan.F(this);
		}

		private void BasicTemplate_Load(object sender,EventArgs e) {
			//FillMeds();
			FillReconcilesGrid();
		}

		//private void FillMeds() {
		//  Medications.Refresh();
		//  medList=MedicationPats.Refresh(PatCur.PatNum,checkDiscontinued.Checked);
		//  gridMeds.BeginUpdate();
		//  gridMeds.Columns.Clear();
		//  ODGridColumn col=new ODGridColumn(Lan.g("TableMedications","Medication"),140);
		//  gridMeds.Columns.Add(col);
		//  col=new ODGridColumn(Lan.g("TableMedications","Notes for Patient"),225);
		//  gridMeds.Columns.Add(col);
		//  col=new ODGridColumn(Lan.g("TableMedications","Disc"),10,HorizontalAlignment.Center);
		//  gridMeds.Columns.Add(col);
		//  gridMeds.Rows.Clear();
		//  ODGridRow row;
		//  for(int i=0;i<medList.Count;i++) {
		//    row=new ODGridRow();
		//    Medication generic=Medications.GetGeneric(medList[i].MedicationNum);
		//    string medName=Medications.GetMedication(medList[i].MedicationNum).MedName;
		//    if(generic.MedicationNum!=medList[i].MedicationNum) {//not generic
		//      medName+=" ("+generic.MedName+")";
		//    }
		//    row.Cells.Add(medName);
		//    row.Cells.Add(medList[i].PatNote);
		//    if(medList[i].DateStop.Year>1880) {
		//      row.Cells.Add("X");
		//    }
		//    else {
		//      row.Cells.Add("");
		//    }
		//    gridMeds.Rows.Add(row);
		//  }
		//  gridMeds.EndUpdate();
		//}

		private void FillReconcilesGrid() {
			gridReconcileEvents.BeginUpdate();
			gridReconcileEvents.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",130);
			gridReconcileEvents.Columns.Add(col);
			col=new ODGridColumn("Details",600);
			gridReconcileEvents.Columns.Add(col);
			ehrMeasureEventsList=EhrMeasureEvents.RefreshByType(PatCur.PatNum,EhrMeasureEventType.MedicationReconcile);
			gridReconcileEvents.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ehrMeasureEventsList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ehrMeasureEventsList[i].DateTEvent.ToString());
				row.Cells.Add(ehrMeasureEventsList[i].EventType.ToString());
				gridReconcileEvents.Rows.Add(row);
			}
			gridReconcileEvents.EndUpdate();
		}

		private void FormMedicationReconcile_Resize(object sender,EventArgs e) {
			splitContainer1.SplitterDistance=splitContainer1.Width/2;
		}

		private void butAddEvent_Click(object sender,EventArgs e) {
			EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
			newMeasureEvent.DateTEvent=DateTime.Now;
			newMeasureEvent.EventType=EhrMeasureEventType.MedicationReconcile;
			newMeasureEvent.PatNum=PatCur.PatNum;
			newMeasureEvent.MoreInfo="";
			EhrMeasureEvents.Insert(newMeasureEvent);
			FillReconcilesGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridReconcileEvents.SelectedIndices.Length<1) {
				MessageBox.Show("Please select at least one record to delete.");
				return;
			}
			for(int i=0;i<gridReconcileEvents.SelectedIndices.Length;i++) {
				EhrMeasureEvents.Delete(ehrMeasureEventsList[gridReconcileEvents.SelectedIndices[i]].EhrMeasureEventNum);
			}
			FillReconcilesGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		

	}
}