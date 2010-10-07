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
			for(int i=0;i<Employees.ListShort.Length;i++){
				comboEmployee.Items.Add(Employees.ListShort[i].FName+" "+Employees.ListShort[i].LName);
				if(Employees.ListShort[i].EmployeeNum==timeCardRule.EmployeeNum){
					comboEmployee.SelectedIndex=i;
				}
			}
			if(comboEmployee.SelectedIndex<0){
				comboEmployee.SelectedIndex=0;
			}
			if(timeCardRule.OverHoursPerDay.Hours>0){
				textOverHoursPerDay.Text=timeCardRule.OverHoursPerDay.Hours.ToString();
			}
			if(timeCardRule.AfterTimeOfDay.Hours>0){
				textAfterTimeOfDay.Text=timeCardRule.AfterTimeOfDay.ToStringHmm();
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(timeCardRule.TimeCardRuleNum!=0){
				TimeCardRules.Delete(timeCardRule.TimeCardRuleNum);
			}
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Verify Data.
			if(comboEmployee.SelectedIndex<0){
				MsgBox.Show(this,"You must select an employee");
				return;
			}
			int overHoursPerDay=-1;
			try{
				overHoursPerDay=Int32.Parse(this.textOverHoursPerDay.Text);
			}
			catch{
			}
			if(overHoursPerDay<0 || overHoursPerDay>24){
				MsgBox.Show(this,"Over hours per day must be between 0 and 24.");
				return;
			}
			TimeSpan afterTimeOfDay=TimeSpan.Zero;
			try{
				afterTimeOfDay=TimeSpan.Parse(this.textAfterTimeOfDay.Text);
			}
			catch{
			}
			if(afterTimeOfDay==TimeSpan.Zero){
				MsgBox.Show(this,"After time of day must be in format hh:mm");
				return;
			}
			//Save the data entered.
			timeCardRule.EmployeeNum=Employees.ListShort[comboEmployee.SelectedIndex].EmployeeNum;
			timeCardRule.OverHoursPerDay=new TimeSpan(overHoursPerDay,0,0);
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