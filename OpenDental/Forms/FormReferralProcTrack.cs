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
	public partial class FormReferralProcTrack:Form {
		DataTable Table;

		public FormReferralProcTrack() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReferralProcTrack_Load(object sender,EventArgs e) {
			textDateFrom.Text=DateTime.Now.AddMonths(-1).ToShortDateString();
			textDateTo.Text=DateTime.Now.ToShortDateString();
			FillGrid();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			DateTime dateFrom=PIn.Date(textDateFrom.Text);
			DateTime dateTo=PIn.Date(textDateTo.Text);
			if(dateFrom==DateTime.MinValue || dateTo==DateTime.MinValue) {
				MsgBox.Show(this,"Please enter valid To and From dates.");
				return;
			}
			Table=Procedures.GetReferred(dateFrom,dateTo,checkUnfinished.Checked);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"ProcCode"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"ReferredTo"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date Referred"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date Done"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Status"),80);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			ProcedureCode procCode;
			DateTime date;
			for(int i=0;i<Table.Rows.Count;i++) {
				row=new ODGridRow();
				procCode=ProcedureCodes.GetProcCode(PIn.Long(Table.Rows[i]["CodeNum"].ToString()));
				row.Cells.Add(procCode.ProcCode);
				row.Cells.Add(Patients.GetPat(PIn.Long(Table.Rows[i]["PatNum"].ToString())).GetNameLF());
				row.Cells.Add(Table.Rows[i]["LName"].ToString()+", "+Table.Rows[i]["FName"].ToString()+" "+Table.Rows[i]["MName"].ToString());
				row.Cells.Add(ProcedureCodes.GetLaymanTerm(PIn.Long(Table.Rows[i]["CodeNum"].ToString())));
				date=PIn.Date(Table.Rows[i]["RefDate"].ToString());
				if(date.Year<1880) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(date.ToShortDateString());
				}
				date=PIn.Date(Table.Rows[i]["DateProcComplete"].ToString());
				if(date.Year<1880) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(date.ToShortDateString());
				}
				row.Cells.Add(Table.Rows[i]["Note"].ToString());
				row.Cells.Add(((ReferralToStatus)PIn.Int(Table.Rows[i]["RefToStatus"].ToString())).ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}