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
	public partial class FormEhrNotPerformed:Form {
		private List<EhrNotPerformed> listNotPerf;
		public Patient PatCur;

		public FormEhrNotPerformed() {
			InitializeComponent();
		}

		private void FormEhrNotPerformed_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Prov",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Code",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Reason Code",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Reason Description",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Note",100);
			gridMain.Columns.Add(col);
			listNotPerf=EhrNotPerformeds.Refresh(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listNotPerf.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listNotPerf[i].DateTimeEntry.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(listNotPerf[i].ProvNum));
				row.Cells.Add(listNotPerf[i].CodeValue);
				string descript="";
				//to get description, first determine which table the code is from.  EhrNotPerformed is allowed to be CPT, CVX, LOINC, SNOMEDCT.
				switch(listNotPerf[i].CodeSystem) {
					case "CPT":
						descript=ProcedureCodes.GetProcCode(listNotPerf[i].CodeValue).Descript;
						break;
					case "CVX":
						//descript=Cvxs.(listNotPerf[i].CodeValue).Descript;//this may need to get code from cdt table instead, if Ryan creates a one
						break;
					case "HCPCS":
						descript=Hcpcses.GetByCode(listNotPerf[i].CodeValue).DescriptionShort;
						break;
					case "SNOMEDCT":
						descript=Snomeds.GetByCode(listNotPerf[i].CodeValue).Description;
						break;
				}
				row.Cells.Add(descript);
				row.Cells.Add(listNotPerf[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrNotPerformedEdit formEE=new FormEhrNotPerformedEdit();
			formEE.EhrNotPerfCur=listNotPerf[e.Row];
			formEE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrNotPerformedEdit formEE=new FormEhrNotPerformedEdit();
			formEE.EhrNotPerfCur=new EhrNotPerformed();
			formEE.EhrNotPerfCur.PatNum=PatCur.PatNum;
			formEE.EhrNotPerfCur.ProvNum=PatCur.PriProv;
			formEE.EhrNotPerfCur.DateTimeEntry=DateTime.Now;
			formEE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}
	}
}