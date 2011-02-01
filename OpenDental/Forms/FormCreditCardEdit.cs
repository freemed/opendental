using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditCardEdit:Form {
		private Patient PatCur;
		public CreditCard CreditCardCur;

		public FormCreditCardEdit(Patient pat) {
			InitializeComponent();
			Lan.F(this);
			PatCur=pat;
		}

		private void FormPayConnect_Load(object sender,EventArgs e) {
			if(!CreditCardCur.IsNew) {
				textCardNumber.Text=CreditCardCur.CCNumberMasked;
				textAddress.Text=CreditCardCur.Address;
				textExpDate.Text=CreditCardCur.CCExpiration.ToString("MMyy");
				textZip.Text=CreditCardCur.Zip;
			}
		}

		private bool VerifyData(){
			if(textCardNumber.Text.Trim().Length<5){
				MsgBox.Show(this,"Invalid Card Number.");
				return false;
			}
			try {
				if(Regex.IsMatch(textExpDate.Text,@"^\d\d[/\- ]\d\d$")) {//08/07 or 08-07 or 08 07
					CreditCardCur.CCExpiration=new DateTime(Convert.ToInt32("20"+textExpDate.Text.Substring(3,2)),Convert.ToInt32(textExpDate.Text.Substring(0,2)),1);
				}
				else if(Regex.IsMatch(textExpDate.Text,@"^\d{4}$")) {//0807
					CreditCardCur.CCExpiration=new DateTime(Convert.ToInt32("20"+textExpDate.Text.Substring(2,2)),Convert.ToInt32(textExpDate.Text.Substring(0,2)),1);
				}
				else {
					MsgBox.Show(this,"Expiration format invalid.");
					return false;
				}
			}
			catch {
				MsgBox.Show(this,"Expiration format invalid.");
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!VerifyData()) {
				return;
			}
			CreditCardCur.Address=textAddress.Text;
			CreditCardCur.CCNumberMasked=textCardNumber.Text;
			CreditCardCur.PatNum=PatCur.PatNum;
			CreditCardCur.Zip=textZip.Text;
			if(CreditCardCur.IsNew) {
				CreditCards.FillCache(CreditCards.RefreshCache());
				CreditCardCur.ItemOrder=CreditCards.Listt.Count;
				CreditCards.Insert(CreditCardCur);
			}
			else {
				CreditCards.Update(CreditCardCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(CreditCardCur.IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			CreditCards.Delete(CreditCardCur.CreditCardNum);
			CreditCards.FillCache(CreditCards.RefreshCache());
			for(int i=0;i<CreditCards.Listt.Count;i++) {
				CreditCards.Listt[i].ItemOrder=CreditCards.Listt.Count-(i+1);
				CreditCards.Update(CreditCards.Listt[i]);//Resets ItemOrder.
			}
			DialogResult=DialogResult.OK;
		}

	}
}