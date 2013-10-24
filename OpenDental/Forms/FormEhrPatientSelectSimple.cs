using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrPatientSelectSimple:Form {
		public long SelectedPatNum;
		private DataTable table;
		public string FName;
		public string LName;

		public FormEhrPatientSelectSimple() {
			InitializeComponent();
		}

		private void FormPatientSelectSimple_Load(object sender,EventArgs e) {
			textFName.Text=FName;
			textLName.Text=LName;
			FillGrid();
		}

		private void FillGrid() {
			table=Patients.GetPtDataTable(false,textLName.Text,textFName.Text,"",
				"",false,"","",
				"","","",0,
				false,false,
				Security.CurUser.ClinicNum,DateTime.MinValue,0,"","");
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("PatNum",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("LName",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("FName",120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["PatNum"].ToString());
				row.Cells.Add(table.Rows[i]["LName"].ToString());
				row.Cells.Add(table.Rows[i]["FName"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			PatSelected();
		}

		private void butSearch_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void PatSelected() {
			SelectedPatNum=PIn.Long(table.Rows[gridMain.GetSelectedIndex()]["PatNum"].ToString());
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MessageBox.Show("Please select a patient first.");
				return;
			}
			PatSelected();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	
		
		
	}
}
