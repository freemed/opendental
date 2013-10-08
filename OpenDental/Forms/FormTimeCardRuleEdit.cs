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
			listEmp.Items.Add(Lan.g(this,"All Employees"));
			listEmp.SelectedIndex=0;
			for(int i=0;i<Employees.ListShort.Length;i++){
				listEmp.Items.Add(Employees.ListShort[i].FName+" "+Employees.ListShort[i].LName);
				if(Employees.ListShort[i].EmployeeNum==timeCardRule.EmployeeNum){
					listEmp.SelectedIndex=i+1;
				}
			}
			textOverHoursPerDay.Text=timeCardRule.OverHoursPerDay.ToStringHmm();
			textAfterTimeOfDay.Text=timeCardRule.AfterTimeOfDay.ToStringHmm();
			textBeforeTimeOfDay.Text=timeCardRule.BeforeTimeOfDay.ToStringHmm();
		}

		private void but5pm_Click(object sender,EventArgs e) {
			DateTime dt=new DateTime(2010,1,1,17,0,0);
			textAfterTimeOfDay.Text=dt.ToShortTimeString();
		}

		private void but6am_Click(object sender,EventArgs e) {
			DateTime dt=new DateTime(2010,1,1,6,0,0);
			textBeforeTimeOfDay.Text=dt.ToShortTimeString();
		}

		///<summary>If entering text in overtime, clear differential text boxes.</summary>
		private void textOverHoursPerDay_TextChanged(object sender,EventArgs e) {
			if(textOverHoursPerDay.Text!="") {
				textAfterTimeOfDay.Text="";
				textBeforeTimeOfDay.Text="";
			}
		}

		///<summary>If entering text in differential boxes, clear overtime text box.</summary>
		private void textBeforeTimeOfDay_TextChanged(object sender,EventArgs e) {
			if(textBeforeTimeOfDay.Text!="") {
				textOverHoursPerDay.Text="";
			}
		}

		///<summary>If entering text in differential boxes, clear overtime text box.</summary>
		private void textAfterTimeOfDay_TextChanged(object sender,EventArgs e) {
			if(textAfterTimeOfDay.Text!="") {
				textOverHoursPerDay.Text="";
			}
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
			if(listEmp.SelectedIndex<0){
				MsgBox.Show(this,"Please select an employee.");
				return;
			}
			TimeSpan overHoursPerDay=TimeSpan.Zero;
			if(textOverHoursPerDay.Text!="") {
				try {
					if(textOverHoursPerDay.Text.Contains(":")){
						overHoursPerDay=TimeSpan.Parse(textOverHoursPerDay.Text);
					}
					else{
						overHoursPerDay=TimeSpan.FromHours(PIn.Double(textOverHoursPerDay.Text));
					}
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
					afterTimeOfDay=DateTime.Parse(textAfterTimeOfDay.Text).TimeOfDay;
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
			TimeSpan beforeTimeOfDay=TimeSpan.Zero;
			if(textBeforeTimeOfDay.Text!="") {
				try {
					beforeTimeOfDay=DateTime.Parse(textBeforeTimeOfDay.Text).TimeOfDay;
				}
				catch {
					MsgBox.Show(this,"Before time of day invalid.");
					return;
				}
				if(beforeTimeOfDay==TimeSpan.Zero || beforeTimeOfDay.Days>0) {
					MsgBox.Show(this,"Before time of day invalid.");
					return;
				}
			}
			if(overHoursPerDay==TimeSpan.Zero && afterTimeOfDay==TimeSpan.Zero && beforeTimeOfDay==TimeSpan.Zero) {
				MsgBox.Show(this,"Either over hours, after or before time of day must be entered.");
				return;
			}
			//save-------------------------------------------------
			if(listEmp.SelectedIndex==0) {
				timeCardRule.EmployeeNum=0;//All employees.
			}
			else {
				timeCardRule.EmployeeNum=Employees.ListShort[listEmp.SelectedIndex-1].EmployeeNum;
			}
			timeCardRule.OverHoursPerDay=overHoursPerDay;
			timeCardRule.AfterTimeOfDay=afterTimeOfDay;
			timeCardRule.BeforeTimeOfDay=beforeTimeOfDay;
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