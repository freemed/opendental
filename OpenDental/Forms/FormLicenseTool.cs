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
	public partial class FormLicenseTool:Form {

		public FormLicenseTool() {
			InitializeComponent();
			RefreshGrid();
			adacode.Focus();
		}

		private void RefreshGrid(){
			codeGrid.BeginUpdate();
			codeGrid.Columns.Clear();
			ODGridColumn col=new ODGridColumn("ADA Code",80);
			codeGrid.Columns.Add(col);
			col=new ODGridColumn("Description",80);
			codeGrid.Columns.Add(col);
			codeGrid.Rows.Clear();
			/*for(int i=0;i<PayPeriods.List.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add();//ada code
				row.Cells.Add();//description
				codeGrid.Rows.Add(row);
			}*/
			codeGrid.EndUpdate();
		}

		private void adacode_KeyPress(object sender,KeyPressEventArgs e) {
			if(e.KeyChar=='\r'){
				e.Handled=true;
				description.Focus();
			}
		}

		private void description_KeyPress(object sender,KeyPressEventArgs e) {
			if(e.KeyChar=='\r') {
				e.Handled=true;
				addButton.Focus();
			}
		}

		private void addButton_Enter(object sender,EventArgs e) {
			adacode.Focus();
		}

		private void checkcompliancebutton_Click(object sender,EventArgs e) {
			bool complies=false;
			if(complies){
				MessageBox.Show(Lan.g(this,"The provided procedure codes are in compliance"),"");
			}else{
				FormLicenseMissing flm=new FormLicenseMissing();
				flm.ShowDialog();
			}
		}

		private void codeGrid_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			ProcLicense pl=null;//e.Row
			FormLicenseToolEdit flte=new FormLicenseToolEdit(pl);
			flte.ShowDialog();		
		}

	}
}