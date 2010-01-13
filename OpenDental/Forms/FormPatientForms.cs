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
	public partial class FormPatientForms:Form {
		public FormPatientForms() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPatientForms_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			/*
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Time"),);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,""),);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,""),);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,""),);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,""),);
			gridMain.Columns.Add(col);
			 
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<List.Length;i++){
				row=new ODGridRow();
				row.Cells.Add("");
				row.Cells.Add("");
			  
				gridMain.Rows.Add(row);
			}*/
			gridMain.EndUpdate();
		}


		private void butImage_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormDefinitions formD=new FormDefinitions(DefCat.ImageCats);
			formD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Defs");
		}

		private void butSheets_Click(object sender,EventArgs e) {
			if(!Security.IsAuthorized(Permissions.Setup)) {
				return;
			}
			FormSheetDefs FormSD=new FormSheetDefs();
			FormSD.ShowDialog();
			SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Sheets");
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}

		

		
	}
}