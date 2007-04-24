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
			//Create a list of comments detailing ADA code shortcomings (if possible).
			ArrayList comments=GetComplianceComments();
			if(comments.Count==0) {//No bad comments?
				comments.Add(Lan.g(this,"Compliance test passed"));//Tell the user of success.
			}
			RefreshGrid(comments);
		}

		private void RefreshGrid(ArrayList comments) {
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
			//First start by deleting unused ADA codes from the procedurecode table.
			ProcedureCodes.DeleteUnusedADACodes();
			//Now get the list of all ADACodes in the form D#### from the procedure code table, as to get the 
			//list of all original ADA codes currently in use (since the unused were deleted).
			string[] usedADACodes=ProcedureCodes.GetAllStandardADACodes();
			//Get the list of codes the user has entered.
			ProcLicense[] procLicenses=ProcLicenses.Refresh();
			//Create a list of comments.
			ArrayList comments=new ArrayList();
			//Find all ADACodes which are currently in use in the database which are not present in the list that the user
			//has specified by hand so that the numbers can be merged if necessary.
			for(int i=0;i<usedADACodes.Length;i++){
				bool userDefined=false;
				for(int j=0;j<procLicenses.Length && !userDefined;j++){
					userDefined=(procLicenses[j].ADACode==usedADACodes[i]);
				}
				if(!userDefined){//The user did not specify an ADACode which is already in use in the database.
					//TODO: Replace the given text comment with one which tells the user how to find the existing ADA code, but
					//does not give it to the directly.
					ProcedureCode pc=ProcedureCodes.GetProcCode(usedADACodes[i]);
					comments.Add("ADA code "+usedADACodes[i]+" with description '"+pc.Descript+"' is in use in the "+
						"database but has not yet been defined using the license tool.");
				}
			}
			return comments;
		}

		private void okbutton_Click(object sender,EventArgs e) {
			Close();
		}

		private void printbutton_Click(object sender,EventArgs e) {

		}

	}
}