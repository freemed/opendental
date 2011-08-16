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
	public partial class FormClaimPayList:Form {
		public FormClaimPayList() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormClaimPayList_Load(object sender,EventArgs e) {
			textDateFrom.Text=DateTime.Now.AddMonths((int)(-1)).ToShortDateString();
			textDateTo.Text=DateTime.Now.ToShortDateString();
			comboClinic.Items.Add("All");
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++) {
				comboClinic.Items.Add(Clinics.List[i].Description);
			}
			FillMain();
		}

		private void FillMain(){
			Cursor=Cursors.WaitCursor;
			DateTime dateFrom=PIn.Date(textDateFrom.Text);
			DateTime dateTo=PIn.Date(textDateTo.Text);
			long clinicNum=0;
			if(comboClinic.SelectedIndex!=0) {
				clinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			List<ClaimPayment> listClaimPay=ClaimPayments.GetForDateRange(dateFrom,dateTo,clinicNum);
			int scrollVal=gridMain.ScrollValue;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableClaimPayList","Check Date"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Date Issued"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Check Amount"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Check Num"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Bank Branch"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Note"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Clinic"),75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Deposit Num"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableClaimPayList","Carrier"),75);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			Clinic clinic;
			string clinicName;
			string checkDate;
			string dateIssued;
			for(int i=0;i<listClaimPay.Count;i++){
				row=new ODGridRow();
				if(listClaimPay[i].CheckDate<new DateTime(1800,1,1)) {
					checkDate="";
				}
				else{
					checkDate=listClaimPay[i].CheckDate.ToShortDateString();
				}
				row.Cells.Add(checkDate);
				row.Cells.Add(listClaimPay[i].DateIssued.ToShortDateString());
				if(listClaimPay[i].DateIssued<new DateTime(1800,1,1)) {
					dateIssued="";
				}
				else{
					dateIssued=listClaimPay[i].DateIssued.ToShortDateString();
				}
				row.Cells.Add(dateIssued);
				row.Cells.Add(listClaimPay[i].CheckAmt.ToString());
				row.Cells.Add(listClaimPay[i].CheckNum);
				row.Cells.Add(listClaimPay[i].BankBranch);
				row.Cells.Add(listClaimPay[i].Note);
				clinic=Clinics.GetClinic(listClaimPay[i].ClinicNum);
				if(clinic==null) {
					clinicName="";
				}
				else {
					clinicName=clinic.Description;
				}
				row.Cells.Add(clinicName);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollValue=scrollVal;
			Cursor=Cursors.Default;
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillMain();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}