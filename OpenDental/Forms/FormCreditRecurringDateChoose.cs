using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditRecurringDateChoose:Form {
		private CreditCard CreditCardCur;
		private DateTime lastMonth;
		private DateTime thisMonth;
		public DateTime PayDate;

		public FormCreditRecurringDateChoose(CreditCard creditCard) {
			InitializeComponent();
			CreditCardCur=creditCard;
			lastMonth=GetValidPayDate(DateTime.Now.AddMonths(-1));
			thisMonth=GetValidPayDate(DateTime.Now);
			Lan.F(this);
		}

		private void FormCreditRecurringDateChoose_Load(object sender,EventArgs e) {
			labelLastMonth.Text+=" "+lastMonth.ToShortDateString();
			labelThisMonth.Text+=" "+thisMonth.ToShortDateString();
			//If the recurring pay date is in the future do not let them choose that option.
			if(thisMonth>DateTime.Now) {
				butThisMonth.Enabled=false;
				labelThisMonth.Text="Cannot make payment for future date: "+thisMonth.ToShortDateString();
			}
		}

		///<summary>Returns a valid date based on the Month and Year taken from the date passed in and the Day that is set for the recurring charges.</summary>
		private DateTime GetValidPayDate(DateTime date) {
			DateTime newDate;
			try {
				newDate=new DateTime(date.Year,date.Month,CreditCardCur.DateStart.Day);
			}
			catch {//Not a valid date, so use the max day in that month.
				newDate=new DateTime(date.Year,date.Month,DateTime.DaysInMonth(date.Year,date.Month));
			}
			return newDate;
		}

		private void butLastMonth_Click(object sender,EventArgs e) {
			PayDate=lastMonth;
			DialogResult=DialogResult.OK;
		}

		private void butThisMonth_Click(object sender,EventArgs e) {
			PayDate=thisMonth;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}