using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormOrthoChart:Form {
		private List<DisplayField> listDisplayFields;
		private List<OrthoChart> listOrthoCharts;
		public Patient PatCur;
		///<summary>Usually only 1 item in this list: today's date.</summary>
		private List<DateTime> listDatesAdditional;

		public FormOrthoChart(Patient patCur) {
			PatCur = patCur;
			InitializeComponent();
			Lan.F(this);
		}

		private void FormOrthoChart_Load(object sender,EventArgs e) {
			listOrthoCharts=OrthoCharts.GetAllForPatient(PatCur.PatNum);
			listDisplayFields=DisplayFields.GetForCategory(DisplayFieldCategory.OrthoChart);
			listDatesAdditional=new List<DateTime>();
			bool addTodaysDate=true;
			for(int i=0;i<listOrthoCharts.Count;i++) {
				if(listOrthoCharts[i].DateService.ToShortDateString()==DateTime.Today.ToShortDateString()) {
					addTodaysDate=false;
					break;
				}
			}
			if(addTodaysDate) {
				listDatesAdditional.Add(DateTime.Today);
			}
			//Add today's date to DatesAdditional if it's not already in the listOrthoCharts.
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			//Add and define columns-------------------------------------------------------------------------------------------------------------------------------------------
			col=new ODGridColumn("Date",70);
			gridMain.Columns.Add(col);
			for(int i=0;i<listDisplayFields.Count;i++) {
				col=new ODGridColumn(listDisplayFields[i].Description,listDisplayFields[i].ColumnWidth,true);
				gridMain.Columns.Add(col);
			}
			listOrthoCharts=OrthoCharts.GetAllForPatient(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			//add blank rows with dates in the first column dates--------------------------------------------------------------------------------------------------------------
			for(int i=0;i<listOrthoCharts.Count;i++) {
				bool addRow=true;
				for(int j=0;j<gridMain.Rows.Count;j++) {
					if(listOrthoCharts[i].DateService.ToShortDateString()==gridMain.Rows[j].Cells[0].Text) {
						addRow=false;
						break;
					}
				}
				if(addRow) {
					row=new ODGridRow();
					row.Cells.Add(listOrthoCharts[i].DateService.ToShortDateString());
					for(int j=0;j<listDisplayFields.Count;j++) {//add blank cells for all display fields after date column
						row.Cells.Add(" ");
					}
					gridMain.Rows.Add(row);
				}
			}
			//add from additional dates which has already been checked for duplicate dates.
			for(int i=0;i<listDatesAdditional.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(listDatesAdditional[i].ToShortDateString());
				for(int j=0;j<listDisplayFields.Count;j++) {//add blank cells for all display fields after date column
					row.Cells.Add(" ");
				}
				gridMain.Rows.Add(row);
			}
			//add ortho charts data to cells-----------------------------------------------------------------------------------------------------------------------------------
			for(int i=0;i<listOrthoCharts.Count;i++) {
				for(int j=0;j<gridMain.Rows.Count;j++) {//check for date
					if(listOrthoCharts[i].DateService.ToShortDateString()==gridMain.Rows[j].Cells[0].Text) {
						for(int k=0;k<listDisplayFields.Count;k++) {//check for column
							if(listOrthoCharts[i].FieldName.ToLower()==gridMain.Columns[k].Heading.ToLower()) {//add to cell
								gridMain.Rows[j].Cells[k].Text=listOrthoCharts[i].FieldValue;
							}
						}//end check column
					}
				}//end check row/date
			}//end check orthoChart
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(e.Col==0){//cannot edit a date
				FormOrthoChartAddDate FormOCAD = new FormOrthoChartAddDate();
				FormOCAD.ShowDialog();
				if(FormOCAD.DialogResult!=DialogResult.OK) {
					return;
				}
				for(int i=0;i<gridMain.Rows.Count;i++) {
					if(FormOCAD.newDate.ToShortDateString()==gridMain.Rows[i].Cells[0].Text) {
						MsgBox.Show(this,"That date already exists.");
						return;
					}
				}
				listDatesAdditional.Add(FormOCAD.newDate);
				FillGrid();
				return;
			}
			////create an orthoChart that has this date and this type
			//FormOrthoChartEdit FormOCE = new FormOrthoChartEdit();
			//if(gridMain.Rows[e.Row].Cells[e.Col].Text==" ") {//new ortho chart
			//  FormOCE.OrthoCur.DateService = DateTime.Parse(gridMain.Rows[e.Row].Cells[0].Text);
			//  FormOCE.OrthoCur.FieldName = gridMain.Columns[e.Col].Heading;
			//  FormOCE.IsNew=true;
			//}
			//else {//existing ortho chart
			//  for(int i=0;i<listOrthoCharts.Count;i++) {
			//    if(listOrthoCharts[i].DateService.ToShortDateString()==gridMain.Rows[e.Row].Cells[0].Text
			//    && listOrthoCharts[i].FieldName==gridMain.Columns[e.Col].Heading) {
			//      FormOCE.OrthoCur=listOrthoCharts[i];
			//      break;
			//    }
			//  }
			//}
			//FormOCE.ShowDialog();
			//if(FormOCE.DialogResult!=DialogResult.OK) {
			//  return;
			//}
			//if(FormOCE.IsNew) {
			//  OrthoCharts.Insert(FormOCE.OrthoCur);
			//}
			//else {
			//  OrthoCharts.Update(FormOCE.OrthoCur);
			//}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//enter date in new form
			//if date not valid, return.
			//if date conflicts with existing date, notify user and return.
			//Add date to DatesAdditional.
			//FillGrid.
			FormOrthoChartAddDate FormOCAD = new FormOrthoChartAddDate();
			FormOCAD.ShowDialog();
			if(FormOCAD.DialogResult!=DialogResult.OK) {
				return;
			}
			for(int i=0;i<gridMain.Rows.Count;i++) {
				if(FormOCAD.newDate.ToShortDateString()==gridMain.Rows[i].Cells[0].Text) {
					MsgBox.Show(this,"That date already exists.");
					return;
				}
			}
			listDatesAdditional.Add(FormOCAD.newDate);
			FillGrid();
			return;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}