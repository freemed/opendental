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

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void BasicTemplate_Load(object sender,EventArgs e) {
			Table=Procedures.GetReferred(DateTime.Now,DateTime.Now,true);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"Procedure Code"),180);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Patient"),103);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date Referred Out"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Date Done"),93);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Status"),85);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string type;
			for(int i=0;i<Table.Rows.Count;i++) {
				row=new ODGridRow();
				//row.Cells.Add(Table.Rows[i]["Phone"].ToString());
				//type=Table.Rows[i]["ClaimType"].ToString();
				//row.Cells.Add(PIn.Date(Table.Rows[i]["DateSent"].ToString()).ToShortDateString());
				//row.Cells.Add(PIn.Double(Table.Rows[i]["ClaimFee"].ToString()).ToString("c"));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}
	}
}