using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using System.Text.RegularExpressions;

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
			ProcLicense[] procLicenses=ProcLicenses.Refresh();
			for(int i=0;i<procLicenses.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(procLicenses[i].ADACode);
				row.Cells.Add(procLicenses[i].Descript);
				codeGrid.Rows.Add(row);
			}
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

		///<summary>Returns an empty string if the given ADACode and Description pair are valid to be inserted or updated in the db (checks for uniqueness). Otherwise, a string of positive length will be returned with an error message.</summary>
		public static string IsValidCode(ProcLicense procLicense){
			if(!Regex.IsMatch(procLicense.ADACode,"^D[0-9]{4}$")) {
				return Lan.g("FormLicenseTool","ADA code must be in the form D####");
			}
			if(procLicense.Descript.Length<1) {
				return Lan.g("FormLicenseTool","Description must be specified");
			}
			if(!ProcLicenses.Unique(procLicense)){
				return Lan.g("FormLicenseTool","The ADA code or description already exists");
			}
			return "";
		}

		private void addButton_Enter(object sender,EventArgs e) {
			//Check the provided input to be sure it is kosher before it is added to the db.
			ProcLicense procLicense=new ProcLicense();
			procLicense.ADACode=adacode.Text;
			procLicense.Descript=description.Text;
			string errors=IsValidCode(procLicense);
			if(errors!=""){
				MessageBox.Show(errors);
				return;
			}
			//Add newly specified code to db.
			try{
				ProcLicenses.Insert(procLicense);
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
				adacode.Focus();
				return;
			}
			//Display the new data in the grid.
			RefreshGrid();
			//Clear old code and refocus so that another code can be added quickly.
			adacode.Text="";
			description.Text="";
			adacode.Focus();
		}

		private void checkcompliancebutton_Click(object sender,EventArgs e) {
			FormLicenseMissing flm=new FormLicenseMissing();
			flm.ShowDialog();
			RefreshGrid();//In case the user merged codes into the proclicense table.
		}

		private void codeGrid_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			ProcLicense[] procLicences=ProcLicenses.Refresh();
			FormLicenseToolEdit flte=new FormLicenseToolEdit(procLicences[e.Row]);
			if(flte.ShowDialog()==DialogResult.OK){
				RefreshGrid();
			}
		}

		///<summary>Updates ADA codes from the proclicense table into the procedurecode table.</summary>
		private void mergecodesbutton_Click(object sender,EventArgs e){
			ProcLicenses.MigrateToProcedureCodes();
		}

	}
}