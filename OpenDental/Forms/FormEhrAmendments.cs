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
		private List<EhrAmendment> ListAmendments;
		public Patient PatCur;

		public FormEhrAmendments() {
			InitializeComponent();
		}

		private void FormEhrAmendments_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Entry Date",70);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Status",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Source",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Scanned",25);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			ListAmendments=EhrAmendments.Refresh(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListAmendments.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListAmendments[i].DateTRequest.ToShortDateString());
				if(ListAmendments[i].IsAccepted==YN.Yes) {
					row.Cells.Add("Accepted");
				}
				else if(ListAmendments[i].IsAccepted==YN.No) {
					row.Cells.Add("Denied");
				}
				else {
					row.Cells.Add("Requested");
				}
				row.Cells.Add(ListAmendments[i].Source.ToString());
				row.Cells.Add(ListAmendments[i].Description);
				if(ListAmendments[i].FileName!="") {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			EhrAmendment ehrAmd=ListAmendments[e.Row];
			FormEhrAmendmentEdit FormEAE=new FormEhrAmendmentEdit(ehrAmd);
			FormEAE.ShowDialog();
			if(FormEAE.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			EhrAmendment ehrAmd=new EhrAmendment();
			ehrAmd.PatNum=PatCur.PatNum;
			ehrAmd.IsNew=true;
			EhrAmendments.Insert(ehrAmd);
			FormEhrAmendmentEdit FormEAE=new FormEhrAmendmentEdit(ehrAmd);
			FormEAE.ShowDialog();
			if(FormEAE.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
