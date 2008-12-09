using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPhoneNumbersManage:Form {
		public int PatNum;
		private Patient Pat;
		private List<PhoneNumber> otherList;

		public FormPhoneNumbersManage() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPhoneNumbersManage_Load(object sender,EventArgs e) {
			Pat=Patients.GetPat(PatNum);
			textName.Text=Pat.LName+", "+Pat.FName;
			textWork.Text=Pat.WkPhone;
			textHome.Text=Pat.HmPhone;
			textWireless.Text=Pat.WirelessPhone;
			FillList();
		}

		private void FillList(){
			listOther.Items.Clear();
			otherList=PhoneNumbers.GetPhoneNumbers(PatNum);
			for(int i=0;i<otherList.Count;i++){
				listOther.Items.Add(otherList[i].PhoneNumberVal);
			}

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}