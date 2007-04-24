using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using System.Collections;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormLicenseMissing:Form {

		public FormLicenseMissing() {
			InitializeComponent();
			RefreshGrid();
		}

		///<summary>Checks for missing ADA codes in the ADA code list and reports how to find codes which were pre-existing elsewhere in the database. </summary>
		private void RefreshGrid() {
			//Create a list of comments detailing ADA code shortcomings (if possible).
			ArrayList comments=GetComplianceComments();
			if(comments.Count==0){//No bad comments?
				comments.Add(Lan.g(this,"Compliance test passed"));//Tell the user of success.
				mergecodesbutton.Enabled=false;
			}
			//Display those comments in the comment grid.
			codeGrid.BeginUpdate();
			codeGrid.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Comments",80);
			codeGrid.Columns.Add(col);
			codeGrid.Rows.Clear();
			for(int i=0;i<comments.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(comments[i].ToString());
				codeGrid.Rows.Add(row);
			}
			codeGrid.EndUpdate();
		}

		///<summary>Creates a list of comments directed at helping the user locate ADA codes which are already stored in their database, but which they have not yet entered manually.</summary>
		private ArrayList GetComplianceComments(){
			//Get the list of codes the user has entered.
			ProcLicense[] procLicenses=ProcLicenses.Refresh();
			//Create a list of comments.
			ArrayList comments=new ArrayList();
			//First start by deleting unused ADA codes from the procedurecode table.
			ProcedureCodes.DeleteUnusedADACodes();





			return comments;
		}

		private void okbutton_Click(object sender,EventArgs e) {
			Close();
		}

		private void printbutton_Click(object sender,EventArgs e) {

		}

	}
}