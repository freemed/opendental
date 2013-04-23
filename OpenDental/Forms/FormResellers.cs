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
	public partial class FormResellers:Form {
		private DataTable TableResellers;

		public FormResellers() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormResellers_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			TableResellers=Resellers.GetResellerList(textLName.Text,textFName.Text,textHmPhone.Text,textAddress.Text,textCity.Text,textState.Text,textPatNum.Text,textEmail.Text);
			gridMain.BeginUpdate();
			ODGridColumn col=new ODGridColumn("PatNum",500);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Customer",500);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("WkPhone",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Address",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("City",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("State",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Email",500);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<TableResellers.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add("");
				row.Cells.Add("");
				row.Cells.Add("");
				row.Cells.Add("");
				row.Cells.Add("");
				row.Cells.Add("");
				row.Cells.Add("");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(0,true);
		}

		#region TextChanged

		private void textLName_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textFName_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textHmPhone_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textAddress_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textCity_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textState_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textPatNum_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textEmail_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		#endregion TextChanged

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Reseller reseller=Resellers.GetOne(PIn.Long(TableResellers.Rows[e.Row]["ResellerNum"].ToString()));
			FormResellerEdit FormRE=new FormResellerEdit(reseller);
			FormRE.ShowDialog();
			FillGrid();//Could have deleted the reseller.
		}

		private void menuItemAccount_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()<0) {
				MsgBox.Show(this,"Please select a reseller first.");
				return;
			}
			GotoModule.GotoAccount(PIn.Long(TableResellers.Rows[gridMain.GetSelectedIndex()]["PatNum"].ToString()));
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK) {
				Reseller reseller=new Reseller();
				reseller.PatNum=formPS.SelectedPatNum;
				Resellers.Insert(reseller);
				FillGrid();
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}