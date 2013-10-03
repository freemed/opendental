using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrNotPerformedEdit:Form {
		public EhrNotPerformed EhrNotPerfCur;
		public int SelectedItemIndex;
		private List<EhrCode> listEhrCodesReason;

		public FormEhrNotPerformedEdit() {
			InitializeComponent();
			SelectedItemIndex=-1;//this will be set to the value of the enum EhrNotPerformedItem when this form is called
			Lan.F(this);
		}

		///<summary>If using the Add button on FormEhrNotPerformed, an input box will allow the user to select from the list of available items that are not being performed.  The SelectedItemIndex will hold the index of the item selected wich corresponds to the enum EhrNotPerformedItem.  We will use this selected item index to set the EhrNotPerformed code and code system.</summary>
		private void FormEhrNotPerformedEdit_Load(object sender,EventArgs e) {
			List<string> listValueSetOIDs=new List<string>();
			switch(SelectedItemIndex) {
				case 0://BMIExam
					listValueSetOIDs=new List<string>{"2.16.840.1.113883.3.600.1.681"};//'BMI LOINC Value' value set
					break;
				case 1://InfluenzaVaccination
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.526.3.402","2.16.840.1.113883.3.526.3.1254" };//'Influenza Vaccination' and 'Influenza Vaccine' value sets
					radioMedReason.Visible=true;
					radioPatReason.Visible=true;
					radioSysReason.Visible=true;
					break;
				case 2://TobaccoScreening
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.526.3.1278" };//'Tobacco Use Screening' value set
					break;
				case 3://DocumentCurrentMeds
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.600.1.462" };//'Current Medications Documented SNMD' value set
					break;
				default://should never happen
					break;
			}
			List<EhrCode> listEhrCodes=EhrCodes.GetForValueSetOIDs(listValueSetOIDs);
			if(listEhrCodes.Count==0) {//this should never happen, just in case
				MsgBox.Show(this,"There are no codes in the database for the selected value set.");
				return;
			}
			if(listEhrCodes.Count==1) {//only one code in the selected value set, use it
				EhrNotPerfCur.CodeValue=listEhrCodes[0].CodeValue;
				EhrNotPerfCur.CodeSystem=listEhrCodes[0].CodeSystem;
				textDescription.Text=listEhrCodes[0].Description;
			}
			else {
				List<string> listCodeDescripts=new List<string>();
				for(int i=0;i<listEhrCodes.Count;i++) {
					listCodeDescripts.Add(listEhrCodes[i].Description);
				}
				InputBox chooseItem=new InputBox(Lan.g(this,"Select the "+Enum.GetNames(typeof(EhrNotPerformedItem))[SelectedItemIndex]+" not being performed from the list below."),listCodeDescripts);
				if(SelectedItemIndex==(int)EhrNotPerformedItem.InfluenzaVaccination) {
					chooseItem.comboSelection.DropDownWidth=690;
				}
				if(chooseItem.ShowDialog()!=DialogResult.OK) {
					DialogResult=DialogResult.Cancel;
				}
				if(chooseItem.comboSelection.SelectedIndex==-1) {
					MsgBox.Show(this,"You must select the "+Enum.GetNames(typeof(EhrNotPerformedItem))[SelectedItemIndex]+" not being performed.");
					DialogResult=DialogResult.Cancel;
				}
				EhrNotPerfCur.CodeValue=listEhrCodes[chooseItem.comboSelection.SelectedIndex].CodeValue;
				EhrNotPerfCur.CodeSystem=listEhrCodes[chooseItem.comboSelection.SelectedIndex].CodeSystem;
				textDescription.Text=listEhrCodes[chooseItem.comboSelection.SelectedIndex].Description;
			}
			textCode.Text=EhrNotPerfCur.CodeValue;
			textCodeSystem.Text=EhrNotPerfCur.CodeSystem;
			textDate.Text=EhrNotPerfCur.DateEntry.ToShortDateString();
			FillReasonList();
		}

		private void FillReasonList() {
			List<string> listValueSetOIDsReason=new List<string>();
			string medicalReason="2.16.840.1.113883.3.526.3.1007";//'Medical Reason' value set
			string patientReason="2.16.840.1.113883.3.526.3.1008";//'Patient Reason' value set
			string systemReason="2.16.840.1.113883.3.526.3.1009";//'System Reason' value set
			string patientRefusedReason="2.16.840.1.113883.3.600.1.1503";//'Patient Reason Refused' value set
			string medicalOrOtherReason="2.16.840.1.113883.3.600.1.1502";//'Medical or Other reason not done' value set
			switch(SelectedItemIndex) {
				case 0://BMIExam
					listValueSetOIDsReason=new List<string> { patientRefusedReason,medicalOrOtherReason };
					break;
				case 1://InfluenzaVaccination
					if(radioPatReason.Checked) {
						listValueSetOIDsReason=new List<string> { patientReason };
					}
					else if(radioSysReason.Checked) {
						listValueSetOIDsReason=new List<string> { systemReason };
					}
					else {
						listValueSetOIDsReason=new List<string> { medicalReason };//Default to the 'Medical Reason' value set, that radio button is checked by default
					}
					break;
				case 2://TobaccoScreening
					listValueSetOIDsReason=new List<string> { medicalReason };
					break;
				case 3://DocumentCurrentMeds
					listValueSetOIDsReason=new List<string> { medicalOrOtherReason };
					break;
				default://should never happen
					break;
			}
			listEhrCodesReason=EhrCodes.GetForValueSetOIDs(listValueSetOIDsReason);
			if(listEhrCodesReason.Count==0) {//this should never happen, just in case
				MsgBox.Show(this,"There are no codes in the database for the selected value set.");
				return;
			}
			comboCodeReason.Items.Clear();
			comboCodeReason.Items.Add(Lan.g(this,"none"));
			comboCodeReason.SelectedIndex=0;//default to 'none' if no reason set for the not performed item
			for(int i=0;i<listEhrCodesReason.Count;i++) {
				comboCodeReason.Items.Add(listEhrCodesReason[i].Description);
				if(EhrNotPerfCur.CodeValueReason==listEhrCodesReason[i].CodeValue && EhrNotPerfCur.CodeSystemReason==listEhrCodesReason[i].CodeSystem) {//check system, although right now all SNOMEDCT
					comboCodeReason.SelectedIndex=i+1;//+1 for 'none'
				}
			}
		}

		private void radioReasonMed_Click(object sender,EventArgs e) {
			FillReasonList();
		}

		private void radioReasonPat_Click(object sender,EventArgs e) {
			FillReasonList();
		}

		private void radioReasonSys_Click(object sender,EventArgs e) {
			FillReasonList();
		}

		private void comboReasonCode_SelectionChangeCommitted(object sender,EventArgs e) {
			//listReasonEhrCodes will have the SNOMEDCT values allowed for the selected item not being performed in the same order as comboReasonCode, selected index-1 to account for 'none'
			if(comboCodeReason.SelectedIndex==0) {//selected 'none'
				textCodeSystemReason.Clear();
				textDescriptionReason.Clear();
			}
			else {
				textCodeSystemReason.Text=listEhrCodesReason[comboCodeReason.SelectedIndex-1].CodeSystem;
				textDescriptionReason.Text=listEhrCodesReason[comboCodeReason.SelectedIndex-1].Description;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(EhrNotPerfCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			EhrNotPerformeds.Delete(EhrNotPerfCur.EhrNotPerformedNum);
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//validate
			DateTime date;
			if(textDate.Text=="") {
				MsgBox.Show(this,"Please enter a date.");
				return;
			}
			try {
				date=DateTime.Parse(textDate.Text);
			}
			catch {
				MsgBox.Show(this,"Please fix date first.");
				return;
			}
			//save
			if(comboCodeReason.SelectedIndex<1) {//selected 'none' or possibly still -1 (although -1 should never happen)
				if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"If you do not select one of the reasons provided it may be harder to meet your Clinical Quality Measures.  Are you sure you want to continue without selecting a valid reason for not performing the "+Enum.GetNames(typeof(EhrNotPerformedItem))[SelectedItemIndex]+"?")) {
					return;
				}
				EhrNotPerfCur.CodeValueReason="";
				EhrNotPerfCur.CodeSystemReason="";
			}
			else {
				EhrNotPerfCur.CodeValueReason=listEhrCodesReason[comboCodeReason.SelectedIndex-1].CodeValue;//-1 to account for 'none'
				EhrNotPerfCur.CodeSystemReason=listEhrCodesReason[comboCodeReason.SelectedIndex-1].CodeSystem;
			}
			//EhrNotPerfCur.DateTimeEntry=date.ToLongDateString();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}