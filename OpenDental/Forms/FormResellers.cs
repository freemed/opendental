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
			gridMain.ContextMenu=menuRightClick;
		}

		private void FormResellers_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			TableResellers=Resellers.GetResellerList();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("PatNum",60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("LName",150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("FName",130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Email",200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("WkPhone",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("PhoneNumberVal",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Address",180);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("City",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("State",40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("PatStatus",80);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<TableResellers.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(TableResellers.Rows[i]["PatNum"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["LName"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["FName"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["Email"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["WkPhone"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["PhoneNumberVal"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["Address"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["City"].ToString());
				row.Cells.Add(TableResellers.Rows[i]["State"].ToString());
				row.Cells.Add(((PatientStatus)PIn.Int(TableResellers.Rows[i]["PatStatus"].ToString())).ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

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
			//Only Jordan should be able to add resellers.
			if(!Security.IsAuthorized(Permissions.SecurityAdmin)) {
				return;
			}
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(Patients.GetLim(FormPS.SelectedPatNum).Guarantor!=FormPS.SelectedPatNum) {
				MsgBox.Show(this,"Customer must be a guarantor before they can be added as a reseller.");
				return;
			}
			if(Resellers.IsResellerFamily(FormPS.SelectedPatNum)) {
				MsgBox.Show(this,"Customer is already a reseller.  CustomerNum: "+FormPS.SelectedPatNum);
				return;
			}
			Reseller reseller=new Reseller();
			reseller.PatNum=FormPS.SelectedPatNum;
			Resellers.Insert(reseller);
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
	}
}