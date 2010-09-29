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
	public partial class FormPayConnect:Form {

		private Payment PaymentCur;
		private Patient PatCur;
		private string amountInit;
		private PayConnectService.Status status;

		public FormPayConnect(Payment payment,Patient pat,string amount) {
			InitializeComponent();
			Lan.F(this);
			PaymentCur=payment;
			PatCur=pat;
			amountInit=amount;
		}

		private void FormPayConnect_Load(object sender,EventArgs e) {
			this.textNameOnCard.Text=PatCur.GetNameFL();
			this.textZipCode.Text=PatCur.Zip;
			this.textAmount.Text=amountInit;
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public string AmountCharged{
			get { return textAmount.Text; }
		}

		///<summary>Only call after the form is closed and the DialogResult is DialogResult.OK.</summary>
		public PayConnectService.Status PaymentStatus{
			get{ return status; }
		}

		private bool VerifyData(out int expYear,out int expMonth){
			expYear=0;
			expMonth=0;
			// Consider adding more advanced verification methods using PayConnect validation requests.
			if(textCardNumber.Text.Trim().Length<5){
				MsgBox.Show(this,"Invalid Card Number.");
				return false;
			}
			if(Regex.IsMatch(textExpDate.Text,@"^\d\d[/\- ]\d\d$")){//08/07 or 08-07 or 08 07
				expYear=Convert.ToInt32("20"+textExpDate.Text.Substring(3,2));
				expMonth=Convert.ToInt32(textExpDate.Text.Substring(0,2));
			}
			else if(Regex.IsMatch(textExpDate.Text,@"^\d{4}$")){//0807
				expYear=Convert.ToInt32("20"+textExpDate.Text.Substring(2,2));
				expMonth=Convert.ToInt32(textExpDate.Text.Substring(0,2));
			}  
			else {
			  MsgBox.Show(this,"Expiration format invalid.");
				return false;
			}
			if(textNameOnCard.Text.Trim()==""){
				MsgBox.Show(this,"Name On Card required.");
				return false;
			}
			if(!Regex.IsMatch(textAmount.Text,"^[0-9]+$") && !Regex.IsMatch(textAmount.Text,"^[0-9]*\\.[0-9]+$")){
				MsgBox.Show(this,"Invalid amount.");
				return false;
			}
			return true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			int expYear;
			int expMonth;
			if(!VerifyData(out expYear,out expMonth)){
				Cursor=Cursors.Default;
				return;
			}
			status=Bridges.PayConnect.ProcessCreditCard(PaymentCur.PayNum,Convert.ToDecimal(textAmount.Text),
				textCardNumber.Text,expYear,expMonth,textNameOnCard.Text,textSecurityCode.Text,textZipCode.Text);
			if(status.code!=0){//error in transaction
				Cursor=Cursors.Default;
				DialogResult=DialogResult.Cancel;
				return;
			}
			Cursor=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}