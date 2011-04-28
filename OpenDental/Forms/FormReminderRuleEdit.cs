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
		public bool IsNew;

		public FormReminderRuleEdit() {
			InitializeComponent();
		}

		private void FormReminderRuleEdit_Load(object sender,EventArgs e) {
			for(int i=0;i<Enum.GetNames(typeof(EhrCriterion)).Length;i++) {
				comboReminderCriterion.Items.Add(Enum.GetNames(typeof(EhrCriterion))[i]);
			}
			comboReminderCriterion.SelectedIndex=(int)RuleCur.ReminderCriterion;
			textCriterionFK.Text=RuleCur.CriterionFK.ToString();
			textCriterionValue.Text=RuleCur.CriterionValue;
			textReminderMessage.Text=RuleCur.Message;
		}

		private void comboReminderCriterion_SelectedIndexChanged(object sender,EventArgs e) {
			RuleCur.ReminderCriterion=(EhrCriterion)comboReminderCriterion.SelectedIndex;
			if(RuleCur.ReminderCriterion==EhrCriterion.Problem || RuleCur.ReminderCriterion==EhrCriterion.Medication || RuleCur.ReminderCriterion==EhrCriterion.Allergy) {
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
				RuleCur.CriterionFK=int.Parse(textCriterionFK.Text);
				RuleCur.CriterionValue="";
			}
			else {//Age,Gender, or LabResult is selected trigger
				RuleCur.CriterionFK=0;
				RuleCur.CriterionValue=textCriterionValue.Text;
			}
			if(textReminderMessage.Text.Length>255){
				MessageBox.Show("Reminder message must be shorter than 255 characters.");
				return;
			}
			RuleCur.Message=textReminderMessage.Text;
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

		private void butSelectFK_Click(object sender,EventArgs e) {
			switch((EhrCriterion)comboReminderCriterion.SelectedValue){
				case EhrCriterion.Problem:
					//TODO:Select Disease dialog
					textCriterionFK.Text="1";
					break;
				case EhrCriterion.Medication:
					//TODO:Select Medication dialog
					textCriterionFK.Text="1";
					break;
				case EhrCriterion.Allergy:
					//TODO:Select Allergy dialog
					textCriterionFK.Text="1";
					break;
				default:
					MessageBox.Show("You should never see this error message. Something has stopped working properly.");
					break;
			}
		}

	


	}
}
