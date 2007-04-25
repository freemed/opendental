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
using CodeBase;

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
			ProcedureCodes.Refresh();
			//First start by deleting unused ADA codes from the procedurecode table.
			ProcedureCodes.DeleteUnusedADACodes();
			//Now get the list of all ADACodes in the form D#### from the procedure code table, as to get the 
			//list of all original ADA codes currently in use (since the unused were deleted).
			string[] usedADACodes=ProcedureCodes.GetAllStandardADACodes();
			//Get the list of codes the user has entered.
			ProcLicense[] procLicenses=ProcLicenses.Refresh();
			//Find all ADACodes which are currently in use in the database which are not present in the list that the user
			//has specified by hand so that the numbers can be merged if necessary.
			ArrayList missingCodes=new ArrayList();
			for(int i=0;i<usedADACodes.Length;i++){
				bool userDefined=false;
				for(int j=0;j<procLicenses.Length && !userDefined;j++){
					userDefined=(procLicenses[j].ADACode==usedADACodes[i]);
				}
				if(!userDefined){//The user did not specify an ADACode which is already in use in the database.
					missingCodes.Add(usedADACodes[i]);
				}
			}
			//Create a list of comments.
			ArrayList comments=new ArrayList();
			//Find all procedures which have disconnected ADA codes and report some information about those procedures.
			int[] patnums=Patients.GetAllPatNums();
			for(int i=0;i<patnums.Length;i++){
				Patient pat=null;
				Procedure[] patProcs=Procedures.Refresh(patnums[i]);
				for(int k=0;k<patProcs.Length;k++){
					for(int j=0;j<missingCodes.Count;j++){
						if(missingCodes[j].ToString()==patProcs[k].ADACode){
							missingCodes.RemoveAt(j--);
							if(pat==null){//Only grab patient from db if needed, and do not grab more than once in this check.
								pat=Patients.GetPat(patnums[i]);
							}
							comments.Add("An unspecified ADA code was detected for the patient "
								+"'"+pat.GetNameLF()+"' in a treatment plan date of "+patProcs[k].ProcDate.ToShortDateString()
								+" with a cost of "+((int)patProcs[k].ProcFee)+"."
								+((int)((patProcs[k].ProcFee-(int)patProcs[k].ProcFee)*100)).ToString().PadLeft(2,'0')						
								+". Click the chart module button to the left of the screen, then select the "
								+"specified patient and look for the ADA code for the procedure mentioned above.");
						}
					}
				}
			}
			Fees.Refresh();
			Def[] defs=DefB.Short[(int)DefCat.FeeSchedNames];
			for(int s=0;s<defs.Length;s++) {
				Def def=defs[s];
				for(int a=0;a<missingCodes.Count;a++){
					Fee fee=Fees.GetFeeByOrder(missingCodes[a].ToString(),def.ItemOrder);
					if(fee!=null){
						missingCodes.RemoveAt(a--);
						comments.Add("An unspecified ADA code was detected in the '"+def.ItemName
							+"' category with cost of "+((int)fee.Amount)+"."
							+((int)((fee.Amount-(int)fee.Amount)*100)).ToString().PadLeft(2,'0')
							+" in the procedure list. You can view the procedure list by first "
							+"selecting the chart module on the left hand portion of the screen, "
							+"then by select any patient and finally, clicking the "
							+"'Procedure List' button under the 'Enter Treatment' tab.");
					}
				}
			}	
			//Check for missed codes in the autocodes.
			AutoCodes.Refresh();
			for(int k=0;k<AutoCodes.List.Length;k++) {
				AutoCodeItems.GetListForCode(AutoCodes.List[k].AutoCodeNum);
				bool codeLocated=false;
				for(int j=0;j<AutoCodeItems.ListForCode.Length && !codeLocated;j++) {
					for(int i=0;i<missingCodes.Count && !codeLocated;i++){
						if(AutoCodeItems.ListForCode[j].ADACode==missingCodes[i].ToString()){
							codeLocated=true;
							missingCodes.RemoveAt(i--);	//The missing code is cleared, so that the same code does not generate 
																					//more than one comment per check.
							comments.Add("There is at least one unspecified ADA code for the auto-code named '"
								+AutoCodes.List[k].Description+"'. Click on 'Setup' in the main menu, then click 'Auto Codes',"
								+"and finally double-click '"+AutoCodes.List[k].Description+"' to locate missing ADA codes.");
						}
					}
				}
			}
			//Check to ensure that newly added codes have been properly merged.
			bool mergeCodes=false;
			for(int i=0;i<procLicenses.Length && !mergeCodes;i++) {
				ProcedureCode pc=ProcedureCodes.GetProcCode(procLicenses[i].ADACode);
				if(pc.ADACode==procLicenses[i].ADACode && pc.Descript!=procLicenses[i].Descript){
					mergeCodes=true;
					comments.Add("There is at least one unmerged ADA code. When finished adding new ADA codes, "
						+"be sure to click the 'Merge Codes' button on the window you saw just before this window.");
				}
			}
			//Ensure that there is nothing that we forgot to check inside the code above.
			if(missingCodes.Count>0){
				string message="There are "+missingCodes.Count+" ADA codes which are in use which cannot be "
					+"accounted for. Please call us for assistance.";
				comments.Add(message);
				/*Logger.openlog.Log(message,Logger.Severity.ERROR);
				for(int i=0;i<missingCodes.Count;i++){
					Logger.openlog.Log("No link to ADA code "+missingCodes[i].ToString(),Logger.Severity.ERROR);
				}*/
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