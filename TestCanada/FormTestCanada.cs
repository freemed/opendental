using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace TestCanada {
	public partial class FormTestCanada:Form {
		public FormTestCanada() {
			InitializeComponent();
		}

		private void butNewDb_Click(object sender,EventArgs e) {
			textResults.Text="";
			Application.DoEvents();
			Cursor=Cursors.WaitCursor;
			if(!DatabaseTools.SetDbConnection("")){
				MessageBox.Show("Could not connect");
				return;
			}
			DatabaseTools.FreshFromDump();
			textResults.Text+="Fresh database loaded from sql dump.";
			Cursor=Cursors.Default;
		}

		private void butClear_Click(object sender,EventArgs e) {
			textResults.Text="";
			Application.DoEvents();
			Cursor=Cursors.WaitCursor;
			if(!DatabaseTools.SetDbConnection("canadatest")) {//if database doesn't exist
				//MessageBox.Show("Database canadatest does not exist.");
				DatabaseTools.SetDbConnection("");
				textResults.Text+=DatabaseTools.FreshFromDump();//this also sets database to be unittest.
			}
			else {
				textResults.Text+=DatabaseTools.ClearDb();
			}
			Cursor=Cursors.Default;
		}

		private void butObjects_Click(object sender,EventArgs e) {
			FillObjects();
			Cursor=Cursors.Default;
		}

		private void FillObjects(){
			textResults.Text="";
			Application.DoEvents();
			Cursor=Cursors.WaitCursor;
			if(!DatabaseTools.SetDbConnection("canadatest")) {//if database doesn't exist
				//MessageBox.Show("Database canadatest does not exist.");
				DatabaseTools.SetDbConnection("");
				textResults.Text+=DatabaseTools.FreshFromDump();//this also sets database to be unittest.
			}
			else {
				textResults.Text+=DatabaseTools.ClearDb();
			}
			Prefs.RefreshCache();
			textResults.Text+=ProviderTC.SetInitialProviders();
			Application.DoEvents();
			textResults.Text+=CarrierTC.SetInitialCarriers();
			Application.DoEvents();
			textResults.Text+=PatientTC.SetInitialPatients();			
		}

		private void butProcedures_Click(object sender,EventArgs e) {
			FillObjects();
			textResults.Text+="---------------------------------------\r\n";
			Application.DoEvents();
			textResults.Text+="(procs not implemented yet)\r\n";
			Cursor=Cursors.Default;
		}

		private void butScripts_Click(object sender,EventArgs e) {
			int singleScript=0;
			if(textSingleScript.Text!=""){
				int checkedCount=0;
				if(checkEligibility.Checked) {
					checkedCount++;
				}
				if(checkClaims.Checked){
					checkedCount++;
				}
				if(checkClaimReversals.Checked){
					checkedCount++;
				}
				if(checkOutstanding.Checked){
					checkedCount++;
				}
				if(checkPredeterm.Checked){
					checkedCount++;
				}
				if(checkPayReconcil.Checked){
					checkedCount++;
				}
				if(checkSumReconcil.Checked){
					checkedCount++;
				}
				if(checkedCount>1){
					MessageBox.Show("When running a single script, only one category can be checked.");
					return;
				}
				if(checkedCount==0){
					MessageBox.Show("Please select a category.");
					return;
				}
				try{
					singleScript=PIn.Int(textSingleScript.Text);
					if(singleScript==0){
						MessageBox.Show("Invalid number.");
						return;
					}
				}
				catch{
					MessageBox.Show("Invalid number.");
					return;
				}
			}
			FillObjects();
			textResults.Text+="---------------------------------------\r\n";
			Application.DoEvents();
			textResults.Text+="(procs not implemented yet)\r\n";
			Application.DoEvents();
			textResults.Text+="---------------------------------------\r\n";
			Application.DoEvents();
			if(checkEligibility.Checked) {
				if(singleScript==0 || singleScript==1){
					textResults.Text+=Eligibility.RunOne(checkShowForms.Checked);
				}
				Application.DoEvents();
				if(singleScript==0 || singleScript==2){
					textResults.Text+=Eligibility.RunTwo(checkShowForms.Checked);
				}
				Application.DoEvents();
				if(singleScript==0 || singleScript==3){
					textResults.Text+=Eligibility.RunThree(checkShowForms.Checked);
				}
				Application.DoEvents();
				if(singleScript==0 || singleScript==4){
					textResults.Text+=Eligibility.RunFour(checkShowForms.Checked);
				}
				Application.DoEvents();
				if(singleScript==0 || singleScript==5){
					textResults.Text+=Eligibility.RunFive(checkShowForms.Checked);
				}
				Application.DoEvents();
				if(singleScript==0 || singleScript==6){
					textResults.Text+=Eligibility.RunSix(checkShowForms.Checked);
				}
				Application.DoEvents();
			}
			if(checkClaims.Checked){
				textResults.Text+="Claims not implemented yet.\r\n";
			}
			if(checkClaimReversals.Checked){
				textResults.Text+="Claim Reversals not implemented yet.\r\n";
			}
			if(checkOutstanding.Checked){
				textResults.Text+="Outstanding Transactions not implemented yet.\r\n";
			}
			if(checkPredeterm.Checked){
				textResults.Text+="Predeterminations not implemented yet.\r\n";
			}
			if(checkPayReconcil.Checked){
				textResults.Text+="Payment Reconciliations not implemented yet.\r\n";
			}
			if(checkSumReconcil.Checked){
				textResults.Text+="Summary Reconciliations not implemented yet.\r\n";
			}
			textResults.Text+="Done.";
			Cursor=Cursors.Default;
		}

		private void checkEligibility_Click(object sender,EventArgs e) {
			SetCheckAll();
		}

		private void checkClaims_Click(object sender,EventArgs e) {
			SetCheckAll();
		}

		private void checkClaimReversals_Click(object sender,EventArgs e) {
			SetCheckAll();
		}

		private void checkOutstanding_Click(object sender,EventArgs e) {
			SetCheckAll();
		}

		private void checkPredeterm_Click(object sender,EventArgs e) {
			SetCheckAll();
		}

		private void checkPayReconcil_Click(object sender,EventArgs e) {
			SetCheckAll();
		}

		private void checkSumReconcil_Click(object sender,EventArgs e) {
			SetCheckAll();
		}

		private void SetCheckAll() {
			bool someChecked=false;
			if(checkEligibility.Checked
				|| checkClaims.Checked
				|| checkClaimReversals.Checked
				|| checkOutstanding.Checked
				|| checkPredeterm.Checked
				|| checkPayReconcil.Checked
				|| checkSumReconcil.Checked) 
			{
				someChecked=true;
			}
			bool someUnchecked=false;
			if(!checkEligibility.Checked
				|| !checkClaims.Checked
				|| !checkClaimReversals.Checked
				|| !checkOutstanding.Checked
				|| !checkPredeterm.Checked
				|| !checkPayReconcil.Checked
				|| !checkSumReconcil.Checked) 
			{
				someUnchecked=true;
			}
			if(someChecked && someUnchecked) {
				checkAll.CheckState=CheckState.Indeterminate;
			}
			else if(someChecked) {
				checkAll.CheckState=CheckState.Checked;
			}
			else {
				checkAll.CheckState=CheckState.Unchecked;
			}
		}

		private void checkAll_Click(object sender,EventArgs e) {
			if(checkAll.CheckState==CheckState.Indeterminate) {
				checkAll.CheckState=CheckState.Unchecked;//make it behave like a two state box
			}
			if(checkAll.CheckState==CheckState.Checked) {
				checkEligibility.Checked=true;
				checkClaims.Checked=true;
				checkClaimReversals.Checked=true;
				checkOutstanding.Checked=true;
				checkPredeterm.Checked=true;
				checkPayReconcil.Checked=true;
				checkSumReconcil.Checked=true;
			}
			if(checkAll.CheckState==CheckState.Unchecked) {
				checkEligibility.Checked=false;
				checkClaims.Checked=false;
				checkClaimReversals.Checked=false;
				checkOutstanding.Checked=false;
				checkPredeterm.Checked=false;
				checkPayReconcil.Checked=false;
				checkSumReconcil.Checked=false;
			}
		}
	}
}
