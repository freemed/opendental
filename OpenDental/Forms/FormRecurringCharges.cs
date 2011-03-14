using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormRecurringCharges:Form {
		private DataTable table;

		public FormRecurringCharges() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRecurringCharges_Load(object sender,EventArgs e) {
			labelCharged.Text=Lan.g(this,"Charged=")+"0";
			labelFailed.Text=Lan.g(this,"Failed=")+"0";
			FillGrid();
			gridMain.SetSelected(true);
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void FillGrid() {
			int scrollPos=gridMain.ScrollValue;
			List<long> selectedKeys=new List<long>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				selectedKeys.Add(PIn.Long(table.Rows[gridMain.SelectedIndices[i]]["StatementNum"].ToString()));
			}
			table=new DataTable();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRecurring","Name"),180);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecurring","BillType"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecurring","Mode"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecurring","LastStatement"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecurring","BalTot"),70,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new OpenDental.UI.ODGridRow();
				row.Cells.Add(table.Rows[i]["name"].ToString());
				row.Cells.Add(table.Rows[i]["billingType"].ToString());
				row.Cells.Add(table.Rows[i]["mode"].ToString());
				row.Cells.Add(table.Rows[i]["lastStatement"].ToString());
				row.Cells.Add(table.Rows[i]["balTotal"].ToString());
				row.Cells.Add(table.Rows[i]["insEst"].ToString());
				if(PrefC.GetBool(PrefName.BalancesDontSubtractIns)) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(table.Rows[i]["amountDue"].ToString());
				}
				row.Cells.Add(table.Rows[i]["payPlanDue"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			for(int i=0;i<selectedKeys.Count;i++) {
				for(int j=0;j<table.Rows.Count;j++) {
					if(table.Rows[j]["StatementNum"].ToString()==selectedKeys[i].ToString()) {
						gridMain.SetSelected(j,true);
					}
				}
			}
			gridMain.ScrollValue=scrollPos;
			labelTotal.Text=Lan.g(this,"Total=")+table.Rows.Count.ToString();
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butPrintList_Click(object sender,EventArgs e) {

		}

		private void butAll_Click(object sender,EventArgs e) {
			gridMain.SetSelected(true);
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void butNone_Click(object sender,EventArgs e) {
			gridMain.SetSelected(false);
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void butSend_Click(object sender,EventArgs e) {

		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}