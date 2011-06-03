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
	public partial class FormOrthoChart:Form {
		private ODGrid gridMain;
		private ODGrid gridPat;
		public List<ReminderRule> listReminders;

		public FormOrthoChart() {
			InitializeComponent();
		}

		private void FormOrthoChart_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
/*			ODGridColumn col;
			//TODO : 
			col=new ODGridColumn("Reminder Criterion",135);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Message",200);
			gridMain.Columns.Add(col);
			listReminders=ReminderRules.SelectAll();
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listReminders.Count;i++) {
				row=new ODGridRow();
				switch(listReminders[i].ReminderCriterion) {
					case EhrCriterion.Problem:
						row.Cells.Add("Problem = "+DiseaseDefs.GetName(listReminders[i].CriterionFK));
						break;
					case EhrCriterion.Medication:
						Medication tempMed = Medications.GetMedication(listReminders[i].CriterionFK);
						if(tempMed.MedicationNum==tempMed.GenericNum) {//handle generic medication names.
							row.Cells.Add("Medication = "+tempMed.MedName);
						}
						else {
							row.Cells.Add("Medication = "+tempMed.MedName+" / "+Medications.GetGenericName(tempMed.GenericNum));
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
					case EhrCriterion.ICD9:
						row.Cells.Add("ICD9 "+ICD9s.GetDescription(listReminders[i].CriterionFK));
						break;
				}
				row.Cells.Add(listReminders[i].Message);
				gridMain.Rows.Add(row);
			}*/
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormReminderRuleEdit FormRRE=new FormReminderRuleEdit();
			FormRRE.RuleCur = listReminders[e.Row];
			FormRRE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormReminderRuleEdit FormRRE=new FormReminderRuleEdit();
			FormRRE.IsNew=true;
			FormRRE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		private void InitializeComponent() {
			this.gridMain = new OpenDental.UI.ODGrid();
			this.gridPat = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,133);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(684,350);
			this.gridMain.TabIndex = 0;
			this.gridMain.Title = "Ortho Chart";
			this.gridMain.TranslationName = null;
			// 
			// gridPat
			// 
			this.gridPat.HScrollVisible = false;
			this.gridPat.Location = new System.Drawing.Point(12,12);
			this.gridPat.Name = "gridPat";
			this.gridPat.ScrollValue = 0;
			this.gridPat.Size = new System.Drawing.Size(684,115);
			this.gridPat.TabIndex = 1;
			this.gridPat.Title = "Patient Fields";
			this.gridPat.TranslationName = null;
			// 
			// FormOrthoChart
			// 
			this.ClientSize = new System.Drawing.Size(778,548);
			this.Controls.Add(this.gridPat);
			this.Controls.Add(this.gridMain);
			this.Name = "FormOrthoChart";
			this.Load += new System.EventHandler(this.FormOrthoChart_Load);
			this.ResumeLayout(false);

		}

		
	}
}
