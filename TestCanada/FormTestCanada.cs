using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;

namespace TestCanada {
	public partial class FormTestCanada:Form {
		private static string dbname="canadatest";

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
			if(!DatabaseTools.SetDbConnection(dbname)) {//if database doesn't exist
				//MessageBox.Show("Database canadatest does not exist.");
				DatabaseTools.SetDbConnection("");
				textResults.Text+=DatabaseTools.FreshFromDump();//this also sets database to be unittest.
			}
			textResults.Text+=DatabaseTools.ClearDb();
			textResults.Text+="Done.";
			Cursor=Cursors.Default;
		}

		private void butObjects_Click(object sender,EventArgs e) {
			FillObjects();
			textResults.Text+="Done.";
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
			Application.DoEvents();
			textResults.Text+=ClaimTC.CreateAllClaims();
		}

		private void butScripts_Click(object sender,EventArgs e) {
			if(textSingleScript.Text==""){
				MessageBox.Show("Please enter a script number first.");
				return;
			}
			int singleScript=0;
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
			if(checkedCount==0){
				MessageBox.Show("Please select a category.");
				return;
			}
			FillObjects();
			textResults.Text+="---------------------------------------\r\n";
			Application.DoEvents();
			if(checkEligibility.Checked) {
				if(singleScript==1){
					textResults.Text+=Eligibility.RunOne(checkShowForms.Checked);
				}
				else if(singleScript==2){
					textResults.Text+=Eligibility.RunTwo(checkShowForms.Checked);
				}
				else if(singleScript==3){
					textResults.Text+=Eligibility.RunThree(checkShowForms.Checked);
				}
				else if(singleScript==4){
					textResults.Text+=Eligibility.RunFour(checkShowForms.Checked);
				}
				else if(singleScript==5){
					textResults.Text+=Eligibility.RunFive(checkShowForms.Checked);
				}
				else if(singleScript==6){
					textResults.Text+=Eligibility.RunSix(checkShowForms.Checked);
				}
				else{
					MessageBox.Show("Script number not found.");
					return;
				}
			}
			if(checkClaims.Checked){
				if(singleScript==1){
					textResults.Text+=ClaimTC.RunOne(checkShowForms.Checked);
				}
				else if(singleScript==2) {
					textResults.Text+=ClaimTC.RunTwo(checkShowForms.Checked);
				}
				else if(singleScript==3) {
					textResults.Text+=ClaimTC.RunThree(checkShowForms.Checked);
				}
				else if(singleScript==4) {
					textResults.Text+=ClaimTC.RunFour(checkShowForms.Checked);
				}
				else if(singleScript==5) {
					textResults.Text+=ClaimTC.RunFive(checkShowForms.Checked);
				}
				else if(singleScript==6) {
					textResults.Text+=ClaimTC.RunSix(checkShowForms.Checked);
				}
				else if(singleScript==7) {
					textResults.Text+=ClaimTC.RunSeven(checkShowForms.Checked);
				}
				else if(singleScript==8) {
					textResults.Text+=ClaimTC.RunEight(checkShowForms.Checked);
				}
				else if(singleScript==9) {
					textResults.Text+=ClaimTC.RunNine(checkShowForms.Checked);
				}
				else if(singleScript==10) {
					textResults.Text+=ClaimTC.RunTen(checkShowForms.Checked);
				}
				else if(singleScript==11) {
					textResults.Text+=ClaimTC.RunEleven(checkShowForms.Checked);
				}
				else if(singleScript==12) {
					textResults.Text+=ClaimTC.RunTwelve(checkShowForms.Checked);
				}
				else{
					MessageBox.Show("Script number not found (not implemented yet).");
					return;
				}
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
			UncheckAllExcept(checkEligibility);
		}

		private void checkClaims_Click(object sender,EventArgs e) {
			UncheckAllExcept(checkClaims);
		}

		private void checkClaimReversals_Click(object sender,EventArgs e) {
			UncheckAllExcept(checkClaimReversals);
		}

		private void checkOutstanding_Click(object sender,EventArgs e) {
			UncheckAllExcept(checkOutstanding);
		}

		private void checkPredeterm_Click(object sender,EventArgs e) {
			UncheckAllExcept(checkPredeterm);
		}

		private void checkPayReconcil_Click(object sender,EventArgs e) {
			UncheckAllExcept(checkPayReconcil);
		}

		private void checkSumReconcil_Click(object sender,EventArgs e) {
			UncheckAllExcept(checkSumReconcil);
		}

		private void UncheckAllExcept(CheckBox checkbox) {
			if(checkbox!=checkEligibility){
				checkEligibility.Checked=false;
			}
			if(checkbox!=checkClaims){
				checkClaims.Checked=false;
			}
			if(checkbox!=checkClaimReversals){
				checkClaimReversals.Checked=false;
			}
			if(checkbox!=checkOutstanding){
				checkOutstanding.Checked=false;
			}
			if(checkbox!=checkPredeterm){
				checkPredeterm.Checked=false;
			}
			if(checkbox!=checkPayReconcil){
				checkPayReconcil.Checked=false;
			}
			if(checkbox!=checkSumReconcil){
				checkSumReconcil.Checked=false;
			}
		}

		private void butShowEtrans_Click(object sender,EventArgs e) {
			if(!checkClaims.Checked){
				MessageBox.Show("Only works for claims right now.");
				return;
			}
			//In case the form was just opened
			DatabaseTools.SetDbConnection(dbname);
			int scriptNum=PIn.Int(textSingleScript.Text);
			long patNum=0;
			double claimFee=0;
			string predeterm="";
			switch(scriptNum){
				case 1:
					patNum=Patients.GetPatNumByNameAndBirthday("Fête","Lisa",new DateTime(1960,4,12));
					claimFee=222.35;
					break;
				case 2:
					patNum=Patients.GetPatNumByNameAndBirthday("Fête","Lisa",new DateTime(1960,4,12));
					claimFee=1254.85;
					break;
				case 3:
					patNum=Patients.GetPatNumByNameAndBirthday("Smith","John",new DateTime(1948,3,2));
					claimFee=439.55;
					break;
				case 4:
					patNum=Patients.GetPatNumByNameAndBirthday("Smith","John",new DateTime(1988,11,2));
					claimFee=222.35;
					break;
				case 5:
					patNum=Patients.GetPatNumByNameAndBirthday("Howard","Bob",new DateTime(1964,5,16));
					claimFee=222.35;
					break;
				case 6:
					patNum=Patients.GetPatNumByNameAndBirthday("Howard","Bob",new DateTime(1964,5,16));
					claimFee=232.35;
					break;
				case 7:
					patNum=Patients.GetPatNumByNameAndBirthday("Howard","Bob",new DateTime(1964,5,16));
					claimFee=232.35;
					predeterm="PD78901234";
					break;
				case 8:
					patNum=Patients.GetPatNumByNameAndBirthday("West","Martha",new DateTime(1954,12,25));
					claimFee=565.35;
					break;
			}
			List<Claim> claimList=Claims.Refresh(patNum);
			Claim claim=null;
			for(int i=0;i<claimList.Count;i++){
				if(claimList[i].ClaimFee==claimFee && claimList[i].PreAuthString==predeterm){
					claim=claimList[i];
				}
			}
			if(claim==null){
				MessageBox.Show("Claim not found.");
				return;
			}
			List<Etrans> etransList=Etranss.GetHistoryOneClaim(claim.ClaimNum);
			if(etransList.Count==0) {
				MessageBox.Show("No history found of sent e-claim.");
				return;
			}
			FormEtransEdit FormE=new FormEtransEdit();
			FormE.EtransCur=etransList[0];
			FormE.ShowDialog();
		}

		/*
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
		}*/
	}
}
