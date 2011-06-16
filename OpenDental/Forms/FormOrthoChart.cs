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

		public FormOrthoChart(Patient patCur) {
			PatCur = patCur;
			InitializeComponent();
			Lan.F(this);
		}

		private void FormOrthoChart_Load(object sender,EventArgs e) {
			listDisplayFields=DisplayFields.GetForCategory(DisplayFieldCategory.OrthoChart);
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			for(int i=0;i<listDisplayFields.Count;i++) {
				col=new ODGridColumn(listDisplayFields[i].Description,listDisplayFields[i].ColumnWidth);
				gridMain.Columns.Add(col);
			}
			listOrthoCharts=OrthoCharts.GetAllForPatient(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listOrthoCharts.Count;i++) {
				//check all existing rows if date matches insert into proper column of that row
				//if date doesn't match add a new row into the grid with all cells containing blank data except the new one.
				for(int j=0;j<gridMain.Rows.Count;j++) {
					if(listOrthoCharts[i].DateService.ToShortDateString()==gridMain.Rows[j].Cells[0].Text) {//record matches an existing date
						for(int k=1;k<listDisplayFields.Count;k++) {//start at k=1 because first column "Date" is already added
							if(listOrthoCharts[i].FieldName==gridMain.Columns[k].Heading) {
								gridMain.Rows[j].Cells[k].Text=listOrthoCharts[i].FieldValue;
							}
						}
						break;//moves on to next ortho chart
					}
					else {//no matching dates for ortho chart, make a new row
						row=new ODGridRow();
						row.Cells.Add(listOrthoCharts[i].DateService.ToShortDateString());
						for(int k=1;k<listDisplayFields.Count;k++) {//start at k=1 because first column "Date" is already added
							if(listDisplayFields[k].Description==listOrthoCharts[i].FieldName){
								row.Cells.Add(listOrthoCharts[i].FieldValue);
							}
							else{
								row.Cells.Add("");//place holders for each cell in each row.
							}
						}
						gridMain.Rows.Add(row);//added only if row doesn't match an existing date.
					}
				}
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(e.Col==0){//cannot edit a date
				return;
			}
			//create an orthoChart that has this date and this type
			FormOrthoChartEdit FormOCE = new FormOrthoChartEdit();
			if(gridMain.Rows[e.Row].Cells[e.Col].Text=="") {//new ortho chart
				FormOCE.OrthoCur.DateService = DateTime.Parse(gridMain.Rows[e.Row].Cells[0].Text);
				FormOCE.OrthoCur.FieldName = gridMain.Columns[e.Col].Heading;
				FormOCE.IsNew=true;
			}
			else {//existing ortho chart
				for(int i=0;i<listOrthoCharts.Count;i++) {
					if(listOrthoCharts[i].DateService.ToShortDateString()==gridMain.Rows[e.Row].Cells[0].Text
					&& listOrthoCharts[i].FieldName==gridMain.Columns[e.Col].Heading) {
						FormOCE.OrthoCur=listOrthoCharts[i];
						break;
					}
				}
			}
			FormOCE.ShowDialog();
			if(FormOCE.DialogResult!=DialogResult.OK) {
				return;
			}
			if(FormOCE.IsNew) {
				OrthoCharts.Insert(FormOCE.OrthoCur);
			}
			else {
				OrthoCharts.Update(FormOCE.OrthoCur);
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//create a new orthochart and allow the user to choose a new date and a new type
			FormOrthoChartEdit FormOCE = new FormOrthoChartEdit();
			FormOCE.ShowDialog();
			if(FormOCE.DialogResult!=DialogResult.OK) {
				return;
			}
			//Todo: check for existing date and type
				//if exists, show dialog for replace append or cancel
					//update if replace is selected
					//append(new querry) is append is selected
					//cancel save if replace or append is not selected
				//if there is no match for date and type, insert.
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