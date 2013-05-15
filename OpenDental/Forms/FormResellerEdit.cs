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
	public partial class FormResellerEdit:Form {
		private Reseller ResellerCur;
		private DataTable TableCustomers;
		private List<ResellerService> ListServices;

		public FormResellerEdit(Reseller reseller) {
			ResellerCur=reseller;
			InitializeComponent();
			Lan.F(this);
			gridMain.ContextMenu=menuRightClick;
		}

		private void FormResellerEdit_Load(object sender,EventArgs e) {
			textUserName.Text=ResellerCur.UserName;
			textPassword.Text=ResellerCur.ResellerPassword;
			FillGridMain();
			FillGridServices();
		}

		private void FillGridMain() {
			double total=0;
			TableCustomers=Resellers.GetResellerCustomersList(ResellerCur.PatNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("PatNum",55);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("RegKey",130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("ProcCode",60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Descript",180);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Fee",70);
			col.TextAlign=HorizontalAlignment.Right;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("DateStart",80);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("DateStop",80);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Note",200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<TableCustomers.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(TableCustomers.Rows[i]["PatNum"].ToString());
				row.Cells.Add(TableCustomers.Rows[i]["RegKey"].ToString());
				row.Cells.Add(TableCustomers.Rows[i]["ProcCode"].ToString());
				row.Cells.Add(TableCustomers.Rows[i]["Descript"].ToString());
				double fee=PIn.Double(TableCustomers.Rows[i]["Fee"].ToString());
				row.Cells.Add(fee.ToString("F"));
				total+=fee;
				row.Cells.Add(PIn.Date(TableCustomers.Rows[i]["DateStart"].ToString()).ToShortDateString());
				row.Cells.Add(PIn.Date(TableCustomers.Rows[i]["DateStop"].ToString()).ToShortDateString());
				row.Cells.Add(TableCustomers.Rows[i]["Note"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			labelTotal.Text="Total: "+total.ToString("C");
		}

		private void FillGridServices() {
			ListServices=ResellerServices.GetServicesForReseller(ResellerCur.ResellerNum);
			gridServices.BeginUpdate();
			gridServices.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Description",180);
			gridServices.Columns.Add(col);
			col=new ODGridColumn("Fee",0);
			col.TextAlign=HorizontalAlignment.Right;
			gridServices.Columns.Add(col);
			gridServices.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListServices.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ProcedureCodes.GetLaymanTerm(ListServices[i].CodeNum));
				row.Cells.Add(ListServices[i].Fee.ToString("F"));
				gridServices.Rows.Add(row);
			}
			gridServices.EndUpdate();
		}

		private void menuItemAccount_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()<0) {
				MsgBox.Show(this,"Please select a customer first.");
				return;
			}
			GotoModule.GotoAccount(PIn.Long(TableCustomers.Rows[gridMain.GetSelectedIndex()]["PatNum"].ToString()));
		}

		private void butAdd_Click(object sender,EventArgs e) {
			ResellerService resellerService=new ResellerService();
			resellerService.ResellerNum=ResellerCur.ResellerNum;
			FormResellerServiceEdit FormRSE=new FormResellerServiceEdit(resellerService);
			FormRSE.IsNew=true;
			FormRSE.ShowDialog();
			if(FormRSE.DialogResult==DialogResult.OK) {
				FillGridServices();
			}
		}

		private void gridServices_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			ResellerService resellerService=ListServices[gridServices.GetSelectedIndex()];
			FormResellerServiceEdit FormRSE=new FormResellerServiceEdit(resellerService);
			FormRSE.ShowDialog();
			if(FormRSE.DialogResult==DialogResult.OK) {
				FillGridServices();
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//Only Jordan should be able to delete resellers.
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)) {
				return;
			}
			//Do not let the reseller be deleted if they have customers in their list.
			if(Resellers.HasActiveResellerCustomers(ResellerCur)) {
				MsgBox.Show(this,"This reseller cannot be deleted until all active services are removed from their customers.  This should be done using the reseller portal.");
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.YesNo,"This will update PatStatus to inactive and set every registartion key's stop date.\r\nContinue?")) {
				return;
			}
			Patient patOld=Patients.GetPat(ResellerCur.PatNum);
			Patient patCur=patOld.Copy();
			patCur.PatStatus=PatientStatus.Inactive;
			Patients.Update(patCur,patOld);
			RegistrationKey[] regKeys=RegistrationKeys.GetForPatient(patCur.PatNum);
			for(int i=0;i<regKeys.Length;i++) {
				DateTime dateTimeNow=MiscData.GetNowDateTime();
				if(regKeys[i].DateEnded.Year>1880 && regKeys[i].DateEnded<dateTimeNow) {
					continue;//Key already ended.  Nothing to do.
				}
				regKeys[i].DateEnded=MiscData.GetNowDateTime();
				RegistrationKeys.Update(regKeys[i]);
			}
			Resellers.Delete(ResellerCur.ResellerNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textPassword.Text!="" && textUserName.Text.Trim()=="") {
				MsgBox.Show(this,"User Name cannot be blank.");
				return;
			}
			if(textUserName.Text!="" && textPassword.Text.Trim()=="") {
				MsgBox.Show(this,"Password cannot be blank.");
				return;
			}
			if(textUserName.Text!="" && Resellers.IsUserNameInUse(ResellerCur.PatNum,textUserName.Text)) {
				MsgBox.Show(this,"User Name already in use.");
				return;
			}
			ResellerCur.UserName=textUserName.Text;
			ResellerCur.ResellerPassword=textPassword.Text;
			Resellers.Update(ResellerCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}