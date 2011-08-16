using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormClaimPayList:Form {
		public FormClaimPayList() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormClaimPayList_Load(object sender,EventArgs e) {
			FillMain();
		}

		private void FillMain(){
			DateTime dateFrom=PIn.Date(textDateFrom.Text);
			DateTime dateTo=PIn.Date(textDateTo.Text);
			long provNum=0;
			if(comboClinic.SelectedIndex!=0) {
				provNum=ProviderC.ListShort[comboClinic.SelectedIndex-1].ProvNum;
			}
			/*
			DataTable table=ClaimPayments.(dateFrom,dateTo,provNum);
			int scrollVal=gridMain.ScrollValue;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableConfirmList","Date Time"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Patient"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Age"),30);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Contact"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Addr/Ph Note"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Status"),80);//confirmed
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Procs"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Medical"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableConfirmList","Appt Note"),204);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			ODGridCell cell;
			for(int i=0;i<table.Rows.Count;i++){
				row=new ODGridRow();
				//aptDateTime=PIn.PDateT(table.Rows[i][4].ToString());
				row.Cells.Add(table.Rows[i]["aptDateTime"].ToString());
				//aptDateTime.ToShortDateString()+"\r\n"+aptDateTime.ToShortTimeString());
				row.Cells.Add(table.Rows[i]["patientName"].ToString());
				row.Cells.Add(table.Rows[i]["age"].ToString());
				row.Cells.Add(table.Rows[i]["contactMethod"].ToString());
				row.Cells.Add(table.Rows[i]["AddrNote"].ToString());
				row.Cells.Add(table.Rows[i]["confirmed"].ToString());
				row.Cells.Add(table.Rows[i]["ProcDescript"].ToString());
				cell=new ODGridCell(table.Rows[i]["medNotes"].ToString());
				cell.ColorText=Color.Red;
				row.Cells.Add(cell);
				row.Cells.Add(table.Rows[i]["Note"].ToString());
				grid.Rows.Add(row);
			}
			grid.EndUpdate();
			grid.ScrollValue=scrollVal;
			 */
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}