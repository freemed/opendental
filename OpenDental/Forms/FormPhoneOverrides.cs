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
	public partial class FormPhoneOverrides:Form {
		DataTable table;

		public FormPhoneOverrides() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPhoneOverrides_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){			
			table=PhoneOverrides.GetAll();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Ext",35);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Employee",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Avail",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Explanation",200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["Extension"].ToString());
				row.Cells.Add(Employees.GetEmp(PIn.PLong(table.Rows[i]["EmpCurrent"].ToString())).FName);
			  if(PIn.PBool(table.Rows[i]["IsAvailable"].ToString())){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(table.Rows[i]["Explanation"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormPhoneOverrideEdit FormP=new FormPhoneOverrideEdit();
			FormP.phoneCur=new PhoneOverride();
			FormP.IsNew=true;
			FormP.ShowDialog();
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			FormPhoneOverrideEdit FormP=new FormPhoneOverrideEdit();
			FormP.phoneCur=PhoneOverrides.GetPhoneOverride(PIn.PLong(table.Rows[e.Row]["PhoneOverrideNum"].ToString()));
			FormP.ShowDialog();
			FillGrid();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}

	
}