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
			fillMedOrderGrid();
		}

		private void fillMedOrderGrid() {
			gridEdu.BeginUpdate();
			gridEdu.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Criteria",150);
			gridEdu.Columns.Add(col);
			col=new ODGridColumn("Url",100);
			gridEdu.Columns.Add(col);
			eduResourceList=EduResources.SelectAll();
			gridEdu.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<eduResourceList.Count;i++) {
				row=new ODGridRow();
				if(eduResourceList[i].DiseaseDefNum!=0) {
					row.Cells.Add("Problem: "+DiseaseDefs.GetItem(eduResourceList[i].DiseaseDefNum).DiseaseName);
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

		private void butAdd_Click(object sender,EventArgs e) {
			FormEduResourceEdit FormERE = new FormEduResourceEdit();
			FormERE.IsNew=true;
			FormERE.ShowDialog();
			fillMedOrderGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void gridEdu_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEduResourceEdit FormERE = new FormEduResourceEdit();
			FormERE.EduResourceCur=eduResourceList[e.Row];
			FormERE.ShowDialog();
			fillMedOrderGrid();
		}


	}
}
