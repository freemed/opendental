using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrCarePlans:Form {

		private Patient _patCur;
		private List<EhrCarePlan> _listCarePlans;

		public FormEhrCarePlans(Patient patCur) {
			InitializeComponent();
			Lan.F(this);
			_patCur=patCur;
		}

		private void FormEhrCarePlans_Load(object sender,EventArgs e) {
			FillCarePlans();
		}

		private void FormEhrCarePlans_Resize(object sender,EventArgs e) {
			FillCarePlans();//So the columns will resize.
		}

		private void FillCarePlans() {
			gridCarePlans.BeginUpdate();
			gridCarePlans.Columns.Clear();
			int colDatePixCount=66;
			int variablePixCount=gridCarePlans.Width-10-colDatePixCount;
			int colGoalPixCount=variablePixCount/2;
			int colInstructionsPixCount=variablePixCount-colGoalPixCount;
			gridCarePlans.Columns.Add(new UI.ODGridColumn("Date",colDatePixCount));
			gridCarePlans.Columns.Add(new UI.ODGridColumn("Goal",colGoalPixCount));
			gridCarePlans.Columns.Add(new UI.ODGridColumn("Instructions",colInstructionsPixCount));
			gridCarePlans.EndUpdate();
			gridCarePlans.BeginUpdate();
			gridCarePlans.Rows.Clear();
			_listCarePlans=EhrCarePlans.Refresh(_patCur.PatNum);
			for(int i=0;i<_listCarePlans.Count;i++) {
				UI.ODGridRow row=new UI.ODGridRow();
				row.Cells.Add(_listCarePlans[i].DatePlanned.ToShortDateString());//Date
				Snomed snomedEducation=Snomeds.GetByCode(_listCarePlans[i].SnomedEducation);
				row.Cells.Add(snomedEducation.Description);//GoalDescript
				row.Cells.Add(_listCarePlans[i].Instructions);//Instructions
				gridCarePlans.Rows.Add(row);
			}
			gridCarePlans.EndUpdate();
		}

		private void gridCarePlans_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormEhrCarePlanEdit formEdit=new FormEhrCarePlanEdit(_listCarePlans[e.Row]);
			if(formEdit.ShowDialog()==DialogResult.OK) {
				FillCarePlans();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			EhrCarePlan ehrCarePlan=new EhrCarePlan();
			ehrCarePlan.IsNew=true;
			ehrCarePlan.PatNum=_patCur.PatNum;
			ehrCarePlan.DatePlanned=DateTime.Today;
			FormEhrCarePlanEdit formEdit=new FormEhrCarePlanEdit(ehrCarePlan);
			if(formEdit.ShowDialog()==DialogResult.OK) {
				FillCarePlans();
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}