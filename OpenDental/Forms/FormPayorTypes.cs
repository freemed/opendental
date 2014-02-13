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
	public partial class FormPayorTypes:Form {
		private List<PayorType> ListPayorTypes;
		public Patient PatCur;

		public FormPayorTypes() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPayorTypes_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date Start",70);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Date End",70);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("SOP Code",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",250);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Note",100);
			gridMain.Columns.Add(col);
			ListPayorTypes=PayorTypes.Refresh(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListPayorTypes.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListPayorTypes[i].DateStart.ToShortDateString());
				if(i==ListPayorTypes.Count-1) {
					row.Cells.Add("Current");
				}
				else {
					row.Cells.Add(ListPayorTypes[i+1].DateStart.ToShortDateString());
				}
				row.Cells.Add(ListPayorTypes[i].SopCode);
				row.Cells.Add(Sops.GetOneDescription(ListPayorTypes[i].SopCode));
				row.Cells.Add(ListPayorTypes[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			PayorType payorType=ListPayorTypes[e.Row];
			FormPayorTypeEdit FormPTE=new FormPayorTypeEdit(payorType);
			FormPTE.ShowDialog();
			if(FormPTE.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			PayorType payorType=new PayorType();
			payorType.PatNum=PatCur.PatNum;
			payorType.DateStart=DateTime.Today;
			FormPayorTypeEdit FormPTE=new FormPayorTypeEdit(payorType);
			FormPTE.IsNew=true;
			FormPTE.ShowDialog();
			if(FormPTE.DialogResult!=DialogResult.OK) {
				return;
			}
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
