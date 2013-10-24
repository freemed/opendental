using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrReminders:Form {
		public Patient PatCur;
		public List<ReminderRule> listReminders;
		public List<EhrMeasureEvent> reminderSentList;

		public FormEhrReminders() {
			InitializeComponent();
		}

		private void FormReminders_Load(object sender,EventArgs e) {
			textPreferedConfidentialContact.Text=PatCur.PreferContactConfidential.ToString();
			FillGrid();
			FillGridProvided();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Reminder Criterion",135);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Message",200);
			gridMain.Columns.Add(col);
			listReminders=ReminderRules.GetRemindersForPatient(PatCur);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listReminders.Count;i++) {
				row=new ODGridRow();
				switch(listReminders[i].ReminderCriterion) {
					case EhrCriterion.Problem:
						DiseaseDef def=DiseaseDefs.GetItem(listReminders[i].CriterionFK);
						row.Cells.Add("Problem ="+def.ICD9Code+" "+def.DiseaseName);
						break;
					case EhrCriterion.Medication:
						Medication tempMed = Medications.GetMedication(listReminders[i].CriterionFK);
						if(tempMed.MedicationNum==tempMed.GenericNum) {//handle generic medication names.
							row.Cells.Add("Medication = "+tempMed.MedName);
						}
						else {
							row.Cells.Add("Medication = "+tempMed.MedName+" ("+Medications.GetGenericName(tempMed.GenericNum)+")");
						}
						break;
					case EhrCriterion.Allergy:
						row.Cells.Add("Allergy = "+AllergyDefs.GetOne(listReminders[i].CriterionFK).Description);
						break;
					case EhrCriterion.Age:
						row.Cells.Add("Age "+listReminders[i].CriterionValue);
						break;
					case EhrCriterion.Gender:
						row.Cells.Add("Gender is "+listReminders[i].CriterionValue);
						break;
					case EhrCriterion.LabResult:
						row.Cells.Add("LabResult "+listReminders[i].CriterionValue);
						break;
					//case EhrCriterion.ICD9:
					//  row.Cells.Add("ICD9 "+ICD9s.GetDescription(listReminders[i].CriterionFK));
					//  break;
				}
				row.Cells.Add(listReminders[i].Message);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void FillGridProvided() {
			gridProvided.BeginUpdate();
			gridProvided.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",130);
			gridProvided.Columns.Add(col);
			col=new ODGridColumn("Details",600);
			gridProvided.Columns.Add(col);
			reminderSentList=EhrMeasureEvents.RefreshByType(PatCur.PatNum,EhrMeasureEventType.ReminderSent);
			gridProvided.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<reminderSentList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(reminderSentList[i].DateTEvent.ToString());
				row.Cells.Add(reminderSentList[i].MoreInfo);
				gridProvided.Rows.Add(row);
			}
			gridProvided.EndUpdate();
		}

		private void butSend_Click(object sender,EventArgs e) {
			EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
			newMeasureEvent.DateTEvent=DateTime.Now;
			newMeasureEvent.EventType=EhrMeasureEventType.ReminderSent;
			newMeasureEvent.PatNum=PatCur.PatNum;
			newMeasureEvent.MoreInfo="";
			EhrMeasureEvents.Insert(newMeasureEvent);
			FillGridProvided();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridProvided.SelectedIndices.Length<1) {
				MessageBox.Show("Please select at least one record to delete.");
				return;
			}
			for(int i=0;i<gridProvided.SelectedIndices.Length;i++) {
				EhrMeasureEvents.Delete(reminderSentList[gridProvided.SelectedIndices[i]].EhrMeasureEventNum);
			}
			FillGridProvided();
		}

		private void butEdit_Click(object sender,EventArgs e) {
			FormEhrPatientConfidentialPrefEdit FormCPE = new FormEhrPatientConfidentialPrefEdit();
			FormCPE.PatCur=PatCur;
			FormCPE.ShowDialog();
			textPreferedConfidentialContact.Text=PatCur.PreferContactConfidential.ToString();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}



	}
}
