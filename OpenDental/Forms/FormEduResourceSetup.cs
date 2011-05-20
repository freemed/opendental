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

namespace OpenDental.Forms {
	public partial class FormEduResourceSetup:Form {
		private List<EduResource> eduResourceList;

		public FormEduResourceSetup() {
			InitializeComponent();
		}

		private void FormEduResourceSetup_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridEdu.BeginUpdate();
			gridEdu.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Criteria",300);
			gridEdu.Columns.Add(col);
			col=new ODGridColumn("Link",700);
			gridEdu.Columns.Add(col);
			eduResourceList=EduResources.SelectAll();
			gridEdu.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<eduResourceList.Count;i++) {
				row=new ODGridRow();
				if(eduResourceList[i].DiseaseDefNum!=0) {
					row.Cells.Add("Problem: "+DiseaseDefs.GetItem(eduResourceList[i].DiseaseDefNum).DiseaseName);
				}
				else if(eduResourceList[i].Icd9Num!=0) {
				  row.Cells.Add("ICD9: "+ICD9s.GetDescription(eduResourceList[i].Icd9Num));
				}
				else if(eduResourceList[i].MedicationNum!=0) {
					row.Cells.Add("Medication: "+Medications.GetDescription(eduResourceList[i].MedicationNum));
				}
				else {
					row.Cells.Add("Lab Results: "+eduResourceList[i].LabResultName+" "+eduResourceList[i].LabResultCompare);
				}
				row.Cells.Add(eduResourceList[i].ResourceUrl);
				gridEdu.Rows.Add(row);
			}
			gridEdu.EndUpdate();
		}

		private void gridEdu_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEduResourceEdit FormERE = new FormEduResourceEdit();
			FormERE.EduResourceCur=eduResourceList[e.Row];
			FormERE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEduResourceEdit FormERE = new FormEduResourceEdit();
			FormERE.IsNew=true;
			FormERE.EduResourceCur=new EduResource();
			FormERE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	


	}
}
