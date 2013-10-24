using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Drawing.Printing;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrQualityMeasureEdit:Form {
		public QualityMeasure Qcur;
		private DataTable table;
		public DateTime DateStart;
		public DateTime DateEnd;
		public long ProvNum;

		public FormEhrQualityMeasureEdit() {
			InitializeComponent();
		}

		private void FormQualityEdit_Load(object sender,EventArgs e) {
			textId.Text=Qcur.Id;
			textDescription.Text=Qcur.Descript;
			FillGrid();
			textDenominator.Text=Qcur.Denominator.ToString();
			textNumerator.Text=Qcur.Numerator.ToString();
			textExclusions.Text=Qcur.Exclusions.ToString();
			textNotMet.Text=Qcur.NotMet.ToString();
			textReportingRate.Text=Qcur.ReportingRate.ToString()+"%";
			textPerformanceRate.Text=Qcur.Numerator.ToString()+"/"+(Qcur.Numerator+Qcur.NotMet).ToString()
					+"  = "+Qcur.PerformanceRate.ToString()+"%";
			textDenominatorExplain.Text=Qcur.DenominatorExplain;
			textNumeratorExplain.Text=Qcur.NumeratorExplain;
			textExclusionsExplain.Text=Qcur.ExclusionsExplain;
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("PatNum",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Patient Name",140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Numerator",60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Exclusion",60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Explanation",200);
			gridMain.Columns.Add(col);
			table=QualityMeasures.GetTable(Qcur.Type,DateStart,DateEnd,ProvNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["PatNum"].ToString());
				row.Cells.Add(table.Rows[i]["patientName"].ToString());
				row.Cells.Add(table.Rows[i]["numerator"].ToString());
				row.Cells.Add(table.Rows[i]["exclusion"].ToString());
				row.Cells.Add(table.Rows[i]["explanation"].ToString());
				//if(table.Rows[i]["met"].ToString()=="X") {
				//	row.ColorBackG=Color.LightGreen;
				//}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}

	

		

		

	}
}
