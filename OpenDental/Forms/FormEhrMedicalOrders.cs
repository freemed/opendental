using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormEhrMedicalOrders:Form {
		public Patient PatCur;
		private DataTable table;
		///<summary>If this is true after exiting, then launch MedicationPat dialog.</summary>
		public bool LaunchMedicationPat;
		/// <summary>If LaunchMedicationPat is true after exiting, then this specifies which MedicationPat to open.  Will never be zero.</summary>
		public long LaunchMedicationPatNum;

		public FormEhrMedicalOrders() {
			InitializeComponent();
		}

		private void FormMedicalOrders_Load(object sender,EventArgs e) {
			FillGridMedOrders();
			//If there is no provider logged in or if the provider logged in does not have a valid ehr number entered, then disable the Add buttons.
			bool disableButtons=false;
			if(Security.CurUser.ProvNum==0) {
				disableButtons=true;
			}
			else {
				Provider provUser=Providers.GetProv(Security.CurUser.ProvNum);
				if(provUser.EhrKey=="") {
					disableButtons=true;
				}
			}
			if(disableButtons) {
				butAddLabOrder.Enabled=false;
				butAddRadOrder.Enabled=false;
			}
			else {
				labelProv.Visible=false;
			}
		}

		private void FillGridMedOrders() {
			gridMedOrders.BeginUpdate();
			gridMedOrders.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date",70);
			gridMedOrders.Columns.Add(col);
			col=new ODGridColumn("Type",80);
			gridMedOrders.Columns.Add(col);
			col=new ODGridColumn("Prov",70);
			gridMedOrders.Columns.Add(col);
			col=new ODGridColumn("Instructions",280);
			gridMedOrders.Columns.Add(col);
			col=new ODGridColumn("Status",100);
			gridMedOrders.Columns.Add(col);
			table=MedicalOrders.GetOrderTable(PatCur.PatNum, checkBoxShowDiscontinued.Checked);
			gridMedOrders.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["date"].ToString());
				row.Cells.Add(table.Rows[i]["type"].ToString());
				row.Cells.Add(table.Rows[i]["prov"].ToString());
				row.Cells.Add(table.Rows[i]["description"].ToString());
				row.Cells.Add(table.Rows[i]["status"].ToString());
				gridMedOrders.Rows.Add(row);
			}
			gridMedOrders.EndUpdate();
		}

		private void gridMedOrders_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			long medicationPatNum=PIn.Long(table.Rows[e.Row]["MedicationPatNum"].ToString());
			long medicalOrderNum=PIn.Long(table.Rows[e.Row]["MedicalOrderNum"].ToString());
			if(medicationPatNum!=0) {//medication
				LaunchMedicationPat=true;
				LaunchMedicationPatNum=medicationPatNum;
				DialogResult=DialogResult.OK;
			}
			else {//medical order
				MedicalOrder ord=MedicalOrders.GetOne(medicalOrderNum);
				if(ord.MedOrderType==MedicalOrderType.Laboratory) {
					FormEhrMedicalOrderLabEdit FormMlab=new FormEhrMedicalOrderLabEdit();
					FormMlab.MedOrderCur=ord;
					FormMlab.ShowDialog();
				}
				else {//Rad
					FormEhrMedicalOrderRadEdit FormMrad=new FormEhrMedicalOrderRadEdit();
					FormMrad.MedOrderCur=ord;
					FormMrad.ShowDialog();
				}
				FillGridMedOrders();
			}
		}

		private void butAddLabOrder_Click(object sender,EventArgs e) {
			FormEhrMedicalOrderLabEdit FormMlab=new FormEhrMedicalOrderLabEdit();
			FormMlab.MedOrderCur=new MedicalOrder();
			FormMlab.MedOrderCur.MedOrderType=MedicalOrderType.Laboratory;
			FormMlab.MedOrderCur.PatNum=PatCur.PatNum;
			FormMlab.MedOrderCur.DateTimeOrder=DateTime.Now;
			FormMlab.MedOrderCur.ProvNum=Security.CurUser.ProvNum;
			FormMlab.IsNew=true;
			FormMlab.ShowDialog();
			FillGridMedOrders();
		}

		private void butAddRadOrder_Click(object sender,EventArgs e) {
			FormEhrMedicalOrderRadEdit FormMrad=new FormEhrMedicalOrderRadEdit();
			FormMrad.MedOrderCur=new MedicalOrder();
			FormMrad.MedOrderCur.MedOrderType=MedicalOrderType.Radiology;
			FormMrad.MedOrderCur.PatNum=PatCur.PatNum;
			FormMrad.MedOrderCur.DateTimeOrder=DateTime.Now;
			FormMrad.MedOrderCur.ProvNum=Security.CurUser.ProvNum;
			FormMrad.IsNew=true;
			FormMrad.ShowDialog();
			FillGridMedOrders();
		}

		private void butAddMedOrder_Click(object sender,EventArgs e) {
			LaunchMedicationPat=true;
			LaunchMedicationPatNum=0;
			DialogResult=DialogResult.OK;
		}

		private void checkBoxShowDiscontinued_Click(object sender,EventArgs e) {
			FillGridMedOrders();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		






	}
}
