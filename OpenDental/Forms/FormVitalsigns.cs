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
	public partial class FormVitalsigns:Form {
		private List<Vitalsign> listVs;
		public long PatNum;

		public FormVitalsigns() {
			InitializeComponent();
		}

		private void FormVitalsigns_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Height",55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Weight",55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("BP",55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("BMI",55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Documentation for Followup or Ineligible",150);
			gridMain.Columns.Add(col);
			listVs=Vitalsigns.Refresh(PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listVs.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listVs[i].DateTaken.ToShortDateString());
				row.Cells.Add(listVs[i].Height.ToString()+" in.");
				row.Cells.Add(listVs[i].Weight.ToString()+" lbs.");
				row.Cells.Add(listVs[i].BpSystolic.ToString()+"/"+listVs[i].BpDiastolic.ToString());
				//BMI = (lbs*703)/(in^2)
				float bmi=Vitalsigns.CalcBMI(listVs[i].Weight,listVs[i].Height);
				if(bmi!=0) {
					row.Cells.Add(bmi.ToString("n1"));
				}
				else {//leave cell blank because there is not a valid bmi
					row.Cells.Add("");
				}
				row.Cells.Add(listVs[i].Documentation);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			long vitalNum=listVs[e.Row].VitalsignNum;
			//change for EHR 2014
			FormVitalsignEdit2014 FormVSE=new FormVitalsignEdit2014();
			//FormEhrVitalsignEdit FormVSE=new FormEhrVitalsignEdit();
			FormVSE.VitalsignCur=Vitalsigns.GetOne(vitalNum);
			FormVSE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//change for EHR 2014
			FormVitalsignEdit2014 FormVSE=new FormVitalsignEdit2014();
			//FormEhrVitalsignEdit FormVSE=new FormEhrVitalsignEdit();
			FormVSE.VitalsignCur=new Vitalsign();
			FormVSE.VitalsignCur.PatNum=PatNum;
			FormVSE.VitalsignCur.DateTaken=DateTime.Today;
			FormVSE.VitalsignCur.IsNew=true;
			FormVSE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}

		///<summary>Hidden if BPOnly vital sign measure.</summary>
		private void butGrowthChart_Click(object sender,EventArgs e) {
			FormEhrGrowthCharts FormGC=new FormEhrGrowthCharts();
			FormGC.PatNum=PatNum;
			FormGC.ShowDialog();
		}


	}
}
