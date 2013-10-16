using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace EHR {
	public partial class FormVitalsigns:Form {
		private List<Vitalsign> listVs;
		public long PatNum;
		public bool IsBPOnly;
		public bool IsBMIOnly;

		public FormVitalsigns() {
			InitializeComponent();
		}

		private void FormVitalsigns_Load(object sender,EventArgs e) {
			if(IsBPOnly) {
				butGrowthChart.Visible=false;
			}
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			if(IsBMIOnly) {
				gridMain.Title="Vital Signs Height and Weight Only";
			}
			if(IsBPOnly) {
				gridMain.Title="Vital Signs Blood Pressure Only";
			}
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
				if(IsBPOnly) {
					row.Cells.Add("N/A");
					row.Cells.Add("N/A");
				}
				else {
					row.Cells.Add(listVs[i].Height.ToString()+" in.");
					row.Cells.Add(listVs[i].Weight.ToString()+" lbs.");
				}
				if(IsBMIOnly) {
					row.Cells.Add("N/A");
				}
				else {
					row.Cells.Add(listVs[i].BpSystolic.ToString()+"/"+listVs[i].BpDiastolic.ToString());
				}
				if(IsBPOnly) {
					row.Cells.Add("N/A");
				}
				else {
					//BMI = (lbs*703)/(in^2)
					float bmi=Vitalsigns.CalcBMI(listVs[i].Weight,listVs[i].Height);
					if(bmi!=0) {
						row.Cells.Add(bmi.ToString("n1"));
					}
					else {//leave cell blank because there is not a valid bmi
						row.Cells.Add("");
					}
				}
				row.Cells.Add(listVs[i].Documentation);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			long vitalNum=listVs[e.Row].VitalsignNum;
			FormVitalsignEdit2014 FormVSE=new FormVitalsignEdit2014();
			FormVSE.VitalsignCur=Vitalsigns.GetOne(vitalNum);
			FormVSE.IsBMIOnly=IsBMIOnly;
			FormVSE.IsBPOnly=IsBPOnly;
			FormVSE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormVitalsignEdit2014 FormVSE=new FormVitalsignEdit2014();
			FormVSE.VitalsignCur=new Vitalsign();
			FormVSE.VitalsignCur.PatNum=PatNum;
			FormVSE.VitalsignCur.DateTaken=DateTime.Today;
			FormVSE.VitalsignCur.IsNew=true;
			FormVSE.IsBMIOnly=IsBMIOnly;
			FormVSE.IsBPOnly=IsBPOnly;
			FormVSE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}

		///<summary>Hidden if BPOnly vital sign measure.</summary>
		private void butGrowthChart_Click(object sender,EventArgs e) {
			FormGrowthCharts FormGC=new FormGrowthCharts();
			FormGC.PatNum=PatNum;
			FormGC.ShowDialog();
		}


	}
}
