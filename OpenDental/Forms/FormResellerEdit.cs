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

		public FormResellerEdit(Reseller reseller) {
			ResellerCur=reseller;
			InitializeComponent();
			Lan.F(this);
		}

		private void FormResellerEdit_Load(object sender,EventArgs e) {
			textUserName.Text=ResellerCur.UserName;
			textPassword.Text=ResellerCur.ResellerPassword;
			FillGrid();
		}

		private void FillGrid() {
			TableCustomers=Resellers.GetResellerCustomersList();
			gridMain.BeginUpdate();
			ODGridColumn col=new ODGridColumn("Customer",500);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("RegKey",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Services",100);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<TableCustomers.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add("");
				row.Cells.Add("");
				row.Cells.Add("");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//Do not let the reseller be deleted if they have customers in their list.
			if(TableCustomers.Rows.Count>0) {
				MsgBox.Show(this,"This reseller cannot be deleted until they remove our services from this customer in their reseller portal.");
				return;
			}
			//TODO: Enhance to update the resellers status to "inactive".
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//TODO: Check if the username is already in use.
			//TODO: Update the username and password to the database.  Keep as plain text.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}