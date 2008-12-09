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
			textWkPhone.Text=Pat.WkPhone;
			textHmPhone.Text=Pat.HmPhone;
			textWirelessPhone.Text=Pat.WirelessPhone;
			FillList();
		}

		private void FillList(){
			listOther.Items.Clear();
			otherList=PhoneNumbers.GetPhoneNumbers(PatNum);
			for(int i=0;i<otherList.Count;i++){
				listOther.Items.Add(otherList[i].PhoneNumberVal);
			}
		}

		private void textWirelessPhone_TextChanged(object sender, System.EventArgs e) {
			int cursor=textWirelessPhone.SelectionStart;
			int length=textWirelessPhone.Text.Length;
			textWirelessPhone.Text=TelephoneNumbers.AutoFormat(textWirelessPhone.Text);
			if(textWirelessPhone.Text.Length>length)
				cursor++;
			textWirelessPhone.SelectionStart=cursor;		
		}

		private void textWkPhone_TextChanged(object sender, System.EventArgs e) {
		 	int cursor=textWkPhone.SelectionStart;
			int length=textWkPhone.Text.Length;
			textWkPhone.Text=TelephoneNumbers.AutoFormat(textWkPhone.Text);
			if(textWkPhone.Text.Length>length)
				cursor++;
			textWkPhone.SelectionStart=cursor;		
		}

		private void textHmPhone_TextChanged(object sender, System.EventArgs e) {
		 	int cursor=textHmPhone.SelectionStart;
			int length=textHmPhone.Text.Length;
			textHmPhone.Text=TelephoneNumbers.AutoFormat(textHmPhone.Text);
			if(textHmPhone.Text.Length>length)
				cursor++;
			textHmPhone.SelectionStart=cursor;		
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(listOther.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a phone number first.");
				return;
			}
			PhoneNumbers.DeleteObject(otherList[listOther.SelectedIndex].PhoneNumberNum);
			FillList();
		}

		private void butOK_Click(object sender,EventArgs e) {
			Patient PatOld=Pat.Copy();
			Pat.WkPhone=textWkPhone.Text;
			Pat.HmPhone=textHmPhone.Text;
			Pat.WirelessPhone=textWirelessPhone.Text;
			Pat.AddrNote=textAddrNotes.Text;
			Patients.Update(Pat,PatOld);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}