using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Forms {
	public partial class FormReminderRuleEdit:Form {
		public ReminderRule RuleCur = new ReminderRule();
		//both the RuleCur.ReminderCriterion and RuleCur.CriterionFK will be altered below in response to user actions.
		public bool IsNew;

		public FormReminderRuleEdit() {
			InitializeComponent();
		}

		private void FormReminderRuleEdit_Load(object sender,EventArgs e) {
			for(int i=0;i<Enum.GetNames(typeof(EhrCriterion)).Length;i++) {
				comboReminderCriterion.Items.Add(Enum.GetNames(typeof(EhrCriterion))[i]);
			}
			comboReminderCriterion.SelectedIndex=(int)RuleCur.ReminderCriterion;
			textCriterionValue.Text=RuleCur.CriterionValue;
			textReminderMessage.Text=RuleCur.Message;
			FillFK();
		}

		private void FillFK() {
			if(RuleCur.CriterionFK==-1 || RuleCur.CriterionFK==0) {
				textCriterionFK.Text="";
				return;
			}
			switch((EhrCriterion)comboReminderCriterion.SelectedIndex) {
				case EhrCriterion.Problem:
					textCriterionFK.Text=DiseaseDefs.GetName(RuleCur.CriterionFK);
					break;
				case EhrCriterion.Medication:
					Medication tempMed = Medications.GetMedication(RuleCur.CriterionFK);
					if(tempMed.MedicationNum==tempMed.GenericNum) {//handle generic medication names.
						textCriterionFK.Text=tempMed.MedName;
					}
					else {
						textCriterionFK.Text=tempMed.MedName+" / "+Medications.GetGenericName(tempMed.GenericNum);
					}
					break;
				case EhrCriterion.Allergy:
					textCriterionFK.Text=AllergyDefs.GetOne(RuleCur.CriterionFK).Description;
					break;
				default:
					//Nothing should happen here.
					break;
			}
		}

		private void comboReminderCriterion_SelectedIndexChanged(object sender,EventArgs e) {
			if(RuleCur.ReminderCriterion!=(EhrCriterion)comboReminderCriterion.SelectedIndex) {
				RuleCur.CriterionFK=-1;
				FillFK();
			}
			RuleCur.ReminderCriterion=(EhrCriterion)comboReminderCriterion.SelectedIndex;
			if(RuleCur.ReminderCriterion==EhrCriterion.Problem || RuleCur.ReminderCriterion==EhrCriterion.Medication || RuleCur.ReminderCriterion==EhrCriterion.Allergy) {
				labelCriterionFK.Text=RuleCur.ReminderCriterion.ToString();
				textCriterionValue.Visible=false;
				labelCriterionValue.Visible=false;
				labelCriterionFK.Visible=true;
				textCriterionFK.Visible=true;
				butSelectFK.Visible=true;
			}
			else {//field only used when Age, Gender, or Labresult are selected.
				textCriterionValue.Visible=true;
				labelCriterionValue.Visible=true;
				labelCriterionFK.Visible=false;
				textCriterionFK.Visible=false;
				butSelectFK.Visible=false;
			}
		}

		private void butSelectFK_Click(object sender,EventArgs e) {
			switch((EhrCriterion)comboReminderCriterion.SelectedIndex) {
				case EhrCriterion.Problem:
					FormDiseaseDefs formD=new FormDiseaseDefs();
					formD.IsSelectionMode=true;
					formD.ShowDialog();
					if(formD.DialogResult!=DialogResult.OK) {
						RuleCur.CriterionFK=-1;
						return;
					}
					RuleCur.CriterionFK=formD.SelectedDiseaseDefNum;
					break;
				case EhrCriterion.Medication:
					FormMedications formM=new FormMedications();
					formM.IsSelectionMode=true;
					formM.ShowDialog();
					if(formM.DialogResult!=DialogResult.OK) {
						RuleCur.CriterionFK=-1;
						return;
					}
					RuleCur.CriterionFK=formM.SelectedMedicationNum;
					break;
				case EhrCriterion.Allergy:
					FormAllergySetup formA=new FormAllergySetup();
					formA.IsSelectionMode=true;
					formA.ShowDialog();
					if(formA.DialogResult!=DialogResult.OK) {
						RuleCur.CriterionFK=-1;
						return;
					}
					RuleCur.CriterionFK=formA.SelectedAllergyDefNum;
					break;
				default:
					MessageBox.Show("You should never see this error message. Something has stopped working properly.");
					break;
			}
			FillFK();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			else {
				ReminderRules.Delete(RuleCur.ReminderRuleNum);
				DialogResult=DialogResult.OK;
			}
		}

		private void butOk_Click(object sender,EventArgs e) {
			//Validate
			RuleCur.ReminderCriterion=(EhrCriterion)comboReminderCriterion.SelectedIndex;
			if(RuleCur.ReminderCriterion==EhrCriterion.Problem || RuleCur.ReminderCriterion==EhrCriterion.Medication || RuleCur.ReminderCriterion==EhrCriterion.Allergy) {
				RuleCur.CriterionValue="";
				if(RuleCur.CriterionFK==-1 || RuleCur.CriterionFK==0) {
					MessageBox.Show("Please select a valid "+RuleCur.ReminderCriterion.ToString().ToLower()+".");
					return;
				}
			}
			else if(RuleCur.ReminderCriterion==EhrCriterion.Gender){
				RuleCur.CriterionFK=0;
				if(textCriterionValue.Text.ToLower()!="male" && textCriterionValue.Text.ToLower()!="female") {
					MessageBox.Show("Please input male or female for gender value.");
					return;
				}
				RuleCur.CriterionValue=textCriterionValue.Text.ToLower();
			}
			else if(RuleCur.ReminderCriterion==EhrCriterion.LabResult) {
				RuleCur.CriterionFK=0;
				if(textCriterionValue.Text=="") {
					MessageBox.Show("Please input a valid lab result.");
					return;
				}
					RuleCur.CriterionValue=textCriterionValue.Text;
			}
			else {//Age
				if(textCriterionValue.Text.Length<2){
					MessageBox.Show("Criterion value must be comparator followed by an age. eg. \"<18\".");
					return;
				}
				if(textCriterionValue.Text[0]!='<' && textCriterionValue.Text[0]!='>'){
					MessageBox.Show("Age criterion must begin with either \"<\" or \">\".");
					return;
				}
				int tempAge;
				if(!int.TryParse(textCriterionValue.Text.Substring(1,textCriterionValue.Text.Length-1),out tempAge)) {
					MessageBox.Show("Age criterion is not in the form of a valid age.");
					return;
				}
				if(tempAge<0 || tempAge>200) {
					MessageBox.Show("Age must be between 0 and 200.");
					return;
				}
				RuleCur.CriterionValue=textCriterionValue.Text;
			}
			if(textReminderMessage.Text.Length>255){
				MessageBox.Show("Reminder message must be shorter than 255 characters.");
				return;
			}
			RuleCur.Message=textReminderMessage.Text;
			if(RuleCur.Message=="") {
				MessageBox.Show("Reminder will be saved with no reminder message.");
			}
			if(IsNew) {
				ReminderRules.Insert(RuleCur);
			}
			else {
				ReminderRules.Update(RuleCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	


	}
}
