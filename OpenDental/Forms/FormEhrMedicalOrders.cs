using System;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrMedicalOrders:Form {
		public Patient _patCur;
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
			table=MedicalOrders.GetOrderTable(_patCur.PatNum, checkBoxShowDiscontinued.Checked);
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
			long medicalOrderNum=PIn.Long(table.Rows[e.Row]["MedicalOrderNum"].ToString());
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

		private void checkBoxShowDiscontinued_Click(object sender,EventArgs e) {
			FillGridMedOrders();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		






	}
}
