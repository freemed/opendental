using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormTimeCardRuleEdit:Form {

		public TimeCardRule timeCardRule;

		public FormTimeCardRuleEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTimeCardRuleEdit_Load(object sender,EventArgs e) {
			listEmployees.Items.Add(Lan.g(this,"All Employees"));
			listEmployees.SelectedIndex=0;
			for(int i=0;i<Employees.ListShort.Length;i++){
				listEmployees.Items.Add(Employees.ListShort[i].FName+" "+Employees.ListShort[i].LName);
				if(Employees.ListShort[i].EmployeeNum==timeCardRule.EmployeeNum){
					listEmployees.SelectedIndex=i+1;
				}
			}
			textOverHoursPerDay.Text=timeCardRule.OverHoursPerDay.ToStringHmm();
			textAfterTimeOfDay.Text=timeCardRule.AfterTimeOfDay.ToStringHmm();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(timeCardRule.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Are you sure you want to delete this time card rule?")){
				return;
			}
			TimeCardRules.Delete(timeCardRule.TimeCardRuleNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Verify Data.
			if(listEmployees.SelectedIndex<0){
				MsgBox.Show(this,"Please select an employee.");
				return;
			}
			TimeSpan overHoursPerDay=TimeSpan.Zero;
			if(textOverHoursPerDay.Text!="") {
				try {
					overHoursPerDay=TimeSpan.Parse(textOverHoursPerDay.Text);
				}
				catch {
					MsgBox.Show(this,"Over hours per day invalid.");
					return;
				}
				if(overHoursPerDay==TimeSpan.Zero || overHoursPerDay.Days>0) {
					MsgBox.Show(this,"Over hours per day invalid.");
					return;
				}
			}
			TimeSpan afterTimeOfDay=TimeSpan.Zero;
			if(textAfterTimeOfDay.Text!="") {
				try {
					afterTimeOfDay=TimeSpan.Parse(textAfterTimeOfDay.Text);
				}
				catch {
					MsgBox.Show(this,"After time of day invalid.");
					return;
				}
				if(afterTimeOfDay==TimeSpan.Zero || afterTimeOfDay.Days>0) {
					MsgBox.Show(this,"After time of day invalid.");
					return;
				}
			}
			if(overHoursPerDay==TimeSpan.Zero && afterTimeOfDay==TimeSpan.Zero) {
				MsgBox.Show(this,"Hours or time of day must be entered.");
				return;
			}
			//save-------------------------------------------------
			if(listEmployees.SelectedIndex==0) {
				timeCardRule.EmployeeNum=0;//All employees.
			}
			else {
				timeCardRule.EmployeeNum=Employees.ListShort[listEmployees.SelectedIndex-1].EmployeeNum;
			}
			timeCardRule.OverHoursPerDay=overHoursPerDay;
			timeCardRule.AfterTimeOfDay=afterTimeOfDay;
			if(timeCardRule.IsNew) {
				TimeCardRules.Insert(timeCardRule);
			}
			else {
				TimeCardRules.Update(timeCardRule);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}