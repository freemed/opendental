using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormInstallmentPlanEdit:Form {
		public InstallmentPlan InstallmentPlanCur;
		public bool IsNew;

		public FormInstallmentPlanEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormInstallmentPlanEdit_Load(object sender,EventArgs e) {
			textDateAgreement.Text=InstallmentPlanCur.DateAgreement.ToShortDateString();
			textDateFirstPay.Text=InstallmentPlanCur.DateFirstPayment.ToShortDateString();
			textMonthlyPayment.Text=InstallmentPlanCur.MonthlyPayment.ToString("f");
			textAPR.Text=InstallmentPlanCur.APR.ToString();
			textNote.Text=InstallmentPlanCur.Note;
			if(IsNew) {
				butDelete.Enabled=false;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(MsgBox.Show(this,MsgBoxButtons.YesNo,"Are you sure you would like to delete this installment plan?")) {
				InstallmentPlans.Delete(InstallmentPlanCur.InstallmentPlanNum);
				DialogResult=DialogResult.Cancel;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDateAgreement.errorProvider1.GetError(textDateAgreement)!=""
				|| textDateFirstPay.errorProvider1.GetError(textDateFirstPay)!=""){
				MsgBox.Show(this,"Please enter valid dates.");
				return;
			}
			if(textMonthlyPayment.errorProvider1.GetError(textMonthlyPayment)!=""){
				MsgBox.Show(this,"Please enter a valid monthly payment.");
				return;
			}
			if(textAPR.errorProvider1.GetError(textAPR)!=""){
				MsgBox.Show(this,"Please enter a valid annual percentage rate (APR).");
				return;
			}
			InstallmentPlanCur.DateAgreement=PIn.Date(textDateAgreement.Text);
			InstallmentPlanCur.DateFirstPayment=PIn.Date(textDateFirstPay.Text);
			InstallmentPlanCur.MonthlyPayment=PIn.Double(textMonthlyPayment.Text);
			InstallmentPlanCur.APR=PIn.Float(textAPR.Text);
			InstallmentPlanCur.Note=PIn.String(textNote.Text);
			if(IsNew) {
				InstallmentPlans.Insert(InstallmentPlanCur);
			}
			else {
				InstallmentPlans.Update(InstallmentPlanCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}