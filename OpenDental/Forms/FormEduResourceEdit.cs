using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDental;
using OpenDentBusiness;

namespace OpenDental.Forms {
	public partial class FormEduResourceEdit:Form {
		public bool IsNew;
		private long tempID;
		private bool IsProblem;
		private bool IsICD9;
		private bool IsMedication;
		private bool IsLab;
		public EduResource EduResourceCur;

		public FormEduResourceEdit() {
			InitializeComponent();
			EduResourceCur=new EduResource();
		}

		private void FormEduResourceEdit_Load(object sender,EventArgs e) {
			if(EduResourceCur.DiseaseDefNum!=0) {
				textProblem.Text=DiseaseDefs.GetName(EduResourceCur.DiseaseDefNum);
			}
			if(EduResourceCur.Icd9Num!=0) {
				textICD9.Text=ICD9s.GetDescription(EduResourceCur.Icd9Num);
			}
			if(EduResourceCur.MedicationNum!=0) {
				textMedication.Text=Medications.GetDescription(EduResourceCur.MedicationNum);
			}
			textLabResultsID.Text=EduResourceCur.LabResultID;
			textLabTestName.Text=EduResourceCur.LabResultName;
			textCompareValue.Text=EduResourceCur.LabResultCompare;
			textUrl.Text=EduResourceCur.ResourceUrl;
		}

		private void butProblemSelect_Click(object sender,EventArgs e) {
			FormDiseaseDefs FormDD = new FormDiseaseDefs();
			FormDD.IsSelectionMode=true;
			FormDD.ShowDialog();
			if(FormDD.DialogResult==DialogResult.OK) {
				IsProblem=true;
				IsICD9=false;
				IsMedication=false;
				IsLab=false;
				tempID=FormDD.SelectedDiseaseDefNum;
				textProblem.Text=DiseaseDefs.GetName(FormDD.SelectedDiseaseDefNum);
				textICD9.Text="";
				textMedication.Text="";
				textLabResultsID.Text="";
				textLabTestName.Text="";
				textCompareValue.Text="";
			}
		}

		private void butICD9Select_Click(object sender,EventArgs e) {
			FormIcd9s FormICD9=new FormIcd9s();
			FormICD9.IsSelectionMode=true;
			FormICD9.ShowDialog();
			if(FormICD9.DialogResult==DialogResult.OK) {
				IsProblem=false;
				IsICD9=true;
				IsMedication=false;
				IsLab=false;
				tempID=FormICD9.SelectedIcd9Num;
				textProblem.Text="";
				textICD9.Text="ICD9: "+ICD9s.GetDescription(FormICD9.SelectedIcd9Num); ;
				textMedication.Text="";
				textLabResultsID.Text="";
				textLabTestName.Text="";
				textCompareValue.Text="";
			}
		}

		private void butMedicationSelect_Click(object sender,EventArgs e) {
			FormMedications FormM=new FormMedications();
			FormM.IsSelectionMode=true;
			FormM.ShowDialog();
			if(FormM.DialogResult==DialogResult.OK) {
				IsProblem=false;
				IsICD9=false;
				IsMedication=true;
				IsLab=false;
				tempID=FormM.SelectedMedicationNum;
				textProblem.Text="";
				textICD9.Text="";
				textMedication.Text=Medications.GetDescription(FormM.SelectedMedicationNum);
				textLabResultsID.Text="";
				textLabTestName.Text="";
				textCompareValue.Text="";
			}
		}

		private void textLabResults_Click(object sender,EventArgs e) {
			IsProblem=false;
			IsICD9=false;
			IsMedication=false;
			IsLab=true; 
			tempID=0;
			textProblem.Text="";
			textICD9.Text="";
			textMedication.Text="";
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			if(MessageBox.Show("Delete this educational resource?","",MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				return;
			}
			EduResources.Delete(EduResourceCur.EduResourceNum);
			DialogResult=DialogResult.OK;
		}

		private void butOk_Click(object sender,EventArgs e) {
			EduResource tempResource = new EduResource();
			tempResource.EduResourceNum=EduResourceCur.EduResourceNum;
			tempResource.LabResultCompare="";
			tempResource.LabResultID="";
			tempResource.LabResultName="";
			//validate
			if(IsProblem) {
				tempResource.DiseaseDefNum=tempID;
			}
			else if(IsICD9) {
				tempResource.Icd9Num=tempID;
			}
			else if(IsMedication) {
				tempResource.MedicationNum=tempID;
			}
			else if(IsLab) {
				tempResource.LabResultID=textLabResultsID.Text;
				if(textLabTestName.Text=="") {
					MessageBox.Show("Invalid test name for lab result.");
					return;
				}
				tempResource.LabResultName=textLabTestName.Text;
				if(textCompareValue.Text.Length<2) {
					MessageBox.Show("Compare value must be comparator followed by an age. eg. \"<18\".");
					return;
				}
				if(textCompareValue.Text[0]!='<' && textCompareValue.Text[0]!='>') {
					MessageBox.Show("Compare value must begin with either \"<\" or \">\".");
					return;
				}
				try {
					int.Parse(textCompareValue.Text.Substring(1,textCompareValue.Text.Length-1));
				}
				catch {
					MessageBox.Show("Compare value is not a valid number.");
					return;
				}
				tempResource.LabResultCompare=textCompareValue.Text;
			}
			else {//error, Not a valid problem, medication, or lab result.
				MessageBox.Show("Please Select a valid problem, medication, or lab result.");
				return;
			}
			if(textUrl.Text==""){
				MessageBox.Show("Please input a valid recource URL.");
				return;
			}
			tempResource.ResourceUrl=textUrl.Text;
			//save
			if(IsNew) {
				EduResources.Insert(tempResource);
			}
			else {
				EduResources.Update(tempResource);
			}
			DialogResult=DialogResult.OK;
		}





	}
}
