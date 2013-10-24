using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrEduResourcesPat:Form {
		public Patient patCur;
		private List<EduResource> eduResourceList = new List<EduResource>();
		private List<EhrMeasureEvent> eduMeasureProvidedList = new List<EhrMeasureEvent>();

		public FormEhrEduResourcesPat() {
			InitializeComponent();
		}

		private void FormEduResourcesPat_Load(object sender,EventArgs e) {
			FillGridEdu();
			FillGridProvided();
		}

		private void FillGridEdu() {
			gridEdu.BeginUpdate();
			gridEdu.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Criteria",300);
			gridEdu.Columns.Add(col);
			col=new ODGridColumn("Link",100);
			gridEdu.Columns.Add(col);
			eduResourceList=EduResources.GenerateForPatient(patCur.PatNum);
			gridEdu.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<eduResourceList.Count;i++) {
				row=new ODGridRow();
				if(eduResourceList[i].DiseaseDefNum!=0) {
					row.Cells.Add("Problem: "+DiseaseDefs.GetItem(eduResourceList[i].DiseaseDefNum).DiseaseName);
					//row.Cells.Add("ICD9: "+DiseaseDefs.GetItem(eduResourceList[i].DiseaseDefNum).ICD9Code);
				}
				else if(eduResourceList[i].MedicationNum!=0) {
					row.Cells.Add("Medication: "+Medications.GetDescription(eduResourceList[i].MedicationNum));
				}
				else {
					row.Cells.Add("Lab Results: "+eduResourceList[i].LabResultName);
				}
				row.Cells.Add(eduResourceList[i].ResourceUrl);
				gridEdu.Rows.Add(row);
			}
			gridEdu.EndUpdate();
		}

		private void gridEdu_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Col!=1) {
				return;
			}
			bool didPrint = false;
			try {
				FormEhrEduBrowser FormEDUB = new FormEhrEduBrowser(eduResourceList[e.Row].ResourceUrl);
				FormEDUB.ShowDialog();
				didPrint = FormEDUB.DidPrint;
				//System.Diagnostics.Process.Start(eduResourceList[e.Row].ResourceUrl);
			}
			catch {
				MessageBox.Show("Link not found.");
				return;
			}
			if(didPrint) {
				EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
				newMeasureEvent.DateTEvent=DateTime.Now;
				newMeasureEvent.EventType=EhrMeasureEventType.EducationProvided;
				newMeasureEvent.PatNum=patCur.PatNum;
				newMeasureEvent.MoreInfo=eduResourceList[e.Row].ResourceUrl;
				EhrMeasureEvents.Insert(newMeasureEvent);
				FillGridProvided();
			}
		}

		private void FillGridProvided() {
			gridProvided.BeginUpdate();
			gridProvided.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",140);
			gridProvided.Columns.Add(col);
			col=new ODGridColumn("Details",600);
			gridProvided.Columns.Add(col);
			eduMeasureProvidedList=EhrMeasureEvents.RefreshByType(patCur.PatNum,EhrMeasureEventType.EducationProvided);
			gridProvided.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<eduMeasureProvidedList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(eduMeasureProvidedList[i].DateTEvent.ToString());
				row.Cells.Add(eduMeasureProvidedList[i].MoreInfo);
				gridProvided.Rows.Add(row);
			}
			gridProvided.EndUpdate();
		}
		
		private void butDelete_Click(object sender,EventArgs e) {
			if(gridProvided.SelectedIndices.Length<1) {
				MessageBox.Show("Please select at least one record to delete.");
				return;
			}
			for(int i=0;i<gridProvided.SelectedIndices.Length;i++) {
				EhrMeasureEvents.Delete(eduMeasureProvidedList[gridProvided.SelectedIndices[i]].EhrMeasureEventNum);
			}
			FillGridProvided();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

	}
}
