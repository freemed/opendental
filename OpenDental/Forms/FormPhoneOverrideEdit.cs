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
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			string command="DELETE FROM phoneoverride WHERE PhoneOverrideNum="+phoneCur.PhoneOverrideNum.ToString();
			General.NonQ(command);
			DialogResult=DialogResult.OK;
		}


		private void butOK_Click(object sender,EventArgs e) {
			try{
				phoneCur.Extension=PIn.PInt(textExtension.Text);
			}
			catch{
				MessageBox.Show("Bad extension number.");
				return;
			}
			if(comboEmp.SelectedIndex==-1){
				MessageBox.Show("Please select an employee first.");
				return;
			}
			phoneCur.EmpCurrent=Employees.ListShort[comboEmp.SelectedIndex].EmployeeNum;
			phoneCur.IsAvailable=checkIsAvailable.Checked;
			phoneCur.Explanation=textExplanation.Text;
			string command;
			if(IsNew){
				command="INSERT INTO phoneoverride(Extension,EmpCurrent,IsAvailable,Explanation) "
					+"VALUES("
					+phoneCur.Extension.ToString()+","
					+phoneCur.EmpCurrent.ToString()+","
					+POut.PBool(phoneCur.IsAvailable)+","
					+"'"+POut.PString(phoneCur.Explanation)+"')";
				phoneCur.PhoneOverrideNum=General.NonQ(command,true);
			}
			else{
				command="UPDATE phoneoverride SET "
					+"Extension="+phoneCur.Extension.ToString()+","
					+"EmpCurrent="+phoneCur.EmpCurrent.ToString()+","
					+"IsAvailable="+POut.PBool(phoneCur.IsAvailable)+","
					+"Explanation='"+POut.PString(phoneCur.Explanation)+"' "
					+"WHERE PhoneOverrideNum="+phoneCur.PhoneOverrideNum.ToString();
				General.NonQ(command);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	
		
	}
}