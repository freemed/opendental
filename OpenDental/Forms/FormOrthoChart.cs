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
		///<summary>Each row in this table has a date as the first cell.  There will be additional rows that are not yet in the db.  Each blank cell will be an empty string.  It will also store changes made by the user prior to closing the form.  When the form is closed, this table will be compared with the original listOrthoCharts and a synch process will take place to save to db.  An empty string in a cell will result in no db row or a deletion of existing db row.</summary>
		DataTable table;

		public FormOrthoChart(Patient patCur) {
			PatCur = patCur;
			InitializeComponent();
			Lan.F(this);
		}

		private void FormOrthoChart_Load(object sender,EventArgs e) {
			//define the table----------------------------------------------------------------------------------------------------------
			table=new DataTable("OrthoChartForPatient");
			//define columns----------------------------------------------------------------------------------------------------------
			table.Columns.Add("Date",typeof(DateTime));
			listDisplayFields = DisplayFields.GetForCategory(DisplayFieldCategory.OrthoChart);
			for(int i=0;i<listDisplayFields.Count;i++) {
				table.Columns.Add(listDisplayFields[i].Description,typeof(string));//column names
			}
			//define rows------------------------------------------------------------------------------------------------------------
			listOrthoCharts=OrthoCharts.GetAllForPatient(PatCur.PatNum);
			List<DateTime> datesShowing=new List<DateTime>();
			List<string> listDisplayFieldNames=new List<string>();
			for(int i=0;i<listDisplayFields.Count;i++) {//fill listDisplayFieldNames to be used in comparison
				listDisplayFieldNames.Add(listDisplayFields[i].Description);
			}
			//start adding dates starting with today's date
			datesShowing.Add(DateTime.Today);
			for(int i=0;i<listOrthoCharts.Count;i++) {
				if(!listDisplayFieldNames.Contains(listOrthoCharts[i].FieldName)) {//skip rows not in display fields
					continue;
				}
				if(!datesShowing.Contains(listOrthoCharts[i].DateService)) {//add dates not already in date list
					datesShowing.Add(listOrthoCharts[i].DateService);
				}
			}
			//List<DataRow> rows=new List<DataRow>();
			//add all blank cells to each row except for the date.
			DataRow row;
			//create and add row for each date in date showing
			for(int i=0;i<datesShowing.Count;i++) {
				row=table.NewRow();
				//row.BeginEdit();
				row[0]=datesShowing[i];
				for(int j=0;j<listDisplayFields.Count;j++) {
					row[j+1]=" ";//j+1 because first row is date field.
				}
				//row.AcceptChanges();
				//row.EndEdit();
				table.Rows.Add(row);
			}
			for(int i=0;i<listOrthoCharts.Count;i++) {//loop
				if(!datesShowing.Contains(listOrthoCharts[i].DateService)){
					continue;
				}
				if(!listDisplayFieldNames.Contains(listOrthoCharts[i].FieldName)){
					continue;
				}
				for(int j=0;j<table.Rows.Count;j++) {
					if(listOrthoCharts[i].DateService==(DateTime)table.Rows[j]["Date"]) {
						table.Rows[j][listOrthoCharts[i].FieldName]=listOrthoCharts[i].FieldValue;
					}
				}
			}
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Date",70);
			gridMain.Columns.Add(col);
			for(int i=0;i<listDisplayFields.Count;i++) {
				col=new ODGridColumn(listDisplayFields[i].Description,listDisplayFields[i].ColumnWidth,true);
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Height=19;//fixes isEditable look and feel
				DateTime tempDate=(DateTime)table.Rows[i][0];
				row.Cells.Add(tempDate.ToShortDateString());
				for(int j=0;j<listDisplayFields.Count;j++) {
					string tempString=(string)table.Rows[i][j+1];
					row.Cells.Add(tempString);
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			/*
			if(e.Col==0){//cannot edit a date
				FormOrthoChartAddDate FormOCAD = new FormOrthoChartAddDate();
				FormOCAD.ShowDialog();
				if(FormOCAD.DialogResult!=DialogResult.OK) {
					return;
				}
				for(int i=0;i<gridMain.Rows.Count;i++) {
					if(FormOCAD.SelectedDate.ToShortDateString()==gridMain.Rows[i].Cells[0].Text) {
						MsgBox.Show(this,"That date already exists.");
						return;
					}
				}
				listDatesAdditional.Add(FormOCAD.SelectedDate);
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
			//}*/
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormOrthoChartAddDate FormOCAD = new FormOrthoChartAddDate();
			FormOCAD.ShowDialog();
			if(FormOCAD.DialogResult!=DialogResult.OK) {
				return;
			}
			for(int i=0;i<table.Rows.Count;i++) {
				if(FormOCAD.SelectedDate==(DateTime)table.Rows[i][0]) {
					MsgBox.Show(this,"That date already exists.");
					return;
				}
			}
			//listDatesAdditional.Add(FormOCAD.SelectedDate);
			DataRow row;
			row=table.NewRow();
			row[0]=FormOCAD.SelectedDate;
			for(int j=0;j<listDisplayFields.Count;j++) {
				row[j+1]="";//j+1 because first row is date field.
			}
			table.Rows.Add(row);
//TODO:save grid data to table
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}