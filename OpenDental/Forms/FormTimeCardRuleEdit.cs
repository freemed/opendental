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

		public TimeCardRule timeCardRule=new TimeCardRule();

		public FormTimeCardRuleEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTimeCardRuleEdit_Load(object sender,EventArgs e) {
			listEmployees.Items.Add("All Employees");
			listEmployees.SelectedIndex=0;
			for(int i=0;i<Employees.ListShort.Length;i++){
				listEmployees.Items.Add(Employees.ListShort[i].FName+" "+Employees.ListShort[i].LName);
				if(Employees.ListShort[i].EmployeeNum==timeCardRule.EmployeeNum){
					listEmployees.SelectedIndex=i+1;
				}
			}
			if(timeCardRule.OverHoursPerDay.Hours>0){
				textOverHoursPerDay.Text=timeCardRule.OverHoursPerDay.ToStringHmm();
			}
			if(timeCardRule.AfterTimeOfDay.Hours>0){
				textAfterTimeOfDay.Text=timeCardRule.AfterTimeOfDay.ToStringHmm();
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Are you sure you want to delete this time card rule?")){
				return;
			}
			if(timeCardRule.TimeCardRuleNum!=0){
				TimeCardRules.Delete(timeCardRule.TimeCardRuleNum);
			}
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Verify Data.
			if(listEmployees.SelectedIndex<0){
				MsgBox.Show(this,"You must select an employee");
				return;
			}
			TimeSpan overHoursPerDay=TimeSpan.Zero;
			try{
				overHoursPerDay=TimeSpan.Parse(this.textOverHoursPerDay.Text);
			}
			catch{
			}
			if(overHoursPerDay==TimeSpan.Zero || overHoursPerDay.Days>0){
				MsgBox.Show(this,"Over hours per day invalid");
				return;
			}
			TimeSpan afterTimeOfDay=TimeSpan.Zero;
			try{
				afterTimeOfDay=TimeSpan.Parse(this.textAfterTimeOfDay.Text);
			}
			catch{
			}
			if(afterTimeOfDay==TimeSpan.Zero || afterTimeOfDay.Days>0){
				MsgBox.Show(this,"After time of day invalid");
				return;
			}
			//Save the data entered.
			if(listEmployees.SelectedIndex==0){
				timeCardRule.EmployeeNum=0;//All employees.
			}
			else{
				timeCardRule.EmployeeNum=Employees.ListShort[listEmployees.SelectedIndex-1].EmployeeNum;
			}
			timeCardRule.OverHoursPerDay=overHoursPerDay;
			timeCardRule.AfterTimeOfDay=afterTimeOfDay;
			if(timeCardRule.TimeCardRuleNum==0){
				TimeCardRules.Insert(timeCardRule);
			}
			else{
				TimeCardRules.Update(timeCardRule);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}