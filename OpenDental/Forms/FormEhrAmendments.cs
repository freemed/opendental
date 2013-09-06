using OpenDental.UI;
using OpenDentBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormEhrAmendments:Form {
		public List<EhrAmendment> listResults;
		public Patient AmdPatCur;


		public FormEhrAmendments() {
			InitializeComponent();
		}

		private void FormEhrAmendments_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Entry Date",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Source",130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Accepted Status",60);
			gridMain.Columns.Add(col);
			listResults = EhrAmendments.Refresh(AmdPatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listResults.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listResults[i].DateTCreated.ToShortDateString());
				row.Cells.Add(listResults[i].Description);
				row.Cells.Add(listResults[i].Source.ToString());
				if(listResults[i].IsAccepted) {
					row.Cells.Add("Accepted");
				}
				else {
					row.Cells.Add("Denied");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrAmendmentEdit FormAms = new FormEhrAmendmentEdit();
			FormAms.IsNew=false;
			FormAms.AmdCur = listResults[e.Row];
			FormAms.ShowDialog();
			FillGrid();
		}
		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrAmendmentEdit FormAms = new FormEhrAmendmentEdit();
			FormAms.IsNew=true;
			FormAms.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
