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

namespace OpenDental.Forms {
	public partial class FormReminderRules:Form {
		public List<ReminderRule> listReminders;
		public FormReminderRules() {
			InitializeComponent();
		}

		private void FormReminderRules_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
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
						row.Cells.Add("Problem = ");//+DiseaseDefs.GetName(listReminders[i].CriterionFK));
						break;
					case EhrCriterion.Medication:
						row.Cells.Add("Medication = ");//+Medications.GetGenericName(listReminders[i].CriterionFK));
						break;
					case EhrCriterion.Allergy:
						row.Cells.Add("Allergy = ");//+Allergies.GetOne(listReminders[i].CriterionFK).Reaction);
						break;
					case EhrCriterion.Age:
						row.Cells.Add("Age "+listReminders[i].CriterionValue);
						break;
					case EhrCriterion.Gender:
						row.Cells.Add("Gender "+listReminders[i].CriterionValue);
						break;
					case EhrCriterion.LabResult:
						row.Cells.Add("LabResult "+listReminders[i].CriterionValue);
						break;
				}
				row.Cells.Add(listReminders[i].Message);
				gridMain.Rows.Add(row);
			}
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

		
	}
}
