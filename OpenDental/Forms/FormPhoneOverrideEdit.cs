using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormPhoneOverrideEdit:Form {
		public bool IsNew;
		public PhoneOverride phoneCur;
		public bool ForceUnAndExplanation;

		public FormPhoneOverrideEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPhoneOverrideEdit_Load(object sender,EventArgs e) {
			if(phoneCur.Extension!=0){
				textExtension.Text=phoneCur.Extension.ToString();
			}
			for(int i=0;i<Employees.ListShort.Length;i++){
				comboEmp.Items.Add(Employees.ListShort[i].FName);
				if(Employees.ListShort[i].EmployeeNum==phoneCur.EmpCurrent){
					comboEmp.SelectedIndex=i;
				}
			}
			checkIsAvailable.Checked=phoneCur.IsAvailable;
			textExplanation.Text=phoneCur.Explanation;
			if(ForceUnAndExplanation){
				//don't even give them a chance to check the box
				checkIsAvailable.Visible=false;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			PhoneOverrides.Delete(phoneCur);
			DialogResult=DialogResult.OK;
		}


		private void butOK_Click(object sender,EventArgs e) {
			try{
				phoneCur.Extension=PIn.PInt32(textExtension.Text);
			}
			catch{
				MessageBox.Show("Bad extension number.");
				return;
			}
			if(comboEmp.SelectedIndex==-1){
				MessageBox.Show("Please select an employee first.");
				return;
			}
			if(ForceUnAndExplanation){
				if(textExplanation.Text==""){
					MessageBox.Show("An explanation must be provided.");
					return;
				}
			}
			phoneCur.EmpCurrent=Employees.ListShort[comboEmp.SelectedIndex].EmployeeNum;
			phoneCur.IsAvailable=checkIsAvailable.Checked;
			phoneCur.Explanation=textExplanation.Text;
			if(IsNew){
				PhoneOverrides.Insert(phoneCur);
			}
			else{
				PhoneOverrides.Update(phoneCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	
		
	}
}