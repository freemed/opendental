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
	public partial class FormEncounters:Form {
		private List<Encounter> listEncs;
		public Patient PatCur;

		public FormEncounters() {
			InitializeComponent();
		}

		public void FormEncounters_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Prov",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Code",110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",180);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Note",100);
			gridMain.Columns.Add(col);
			listEncs=Encounters.Refresh(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listEncs.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listEncs[i].DateEncounter.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(listEncs[i].ProvNum));
				row.Cells.Add(listEncs[i].CodeValue);
				string descript="";
				//to get description, first determine which table the code is from.  Encounter is only allowed to be a CDT, CPT, HCPCS, and SNOMEDCT.
				switch(listEncs[i].CodeSystem) {
					case "CDT":
						descript=ProcedureCodes.GetProcCode(listEncs[i].CodeValue).Descript;//this may need to get code from cdt table instead, if Ryan creates a one
						break;
					case "CPT":

						break;
					case "HCPCS":
						descript=Hcpcses.GetByCode(listEncs[i].CodeValue).DescriptionShort;
						break;
					case "SNOMEDCT":
						descript=Snomeds.GetByCode(listEncs[i].CodeValue).Description;
						break;
				}
				row.Cells.Add(descript);
				row.Cells.Add(listEncs[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEncounterEdit formEE=new FormEncounterEdit();
			formEE.EncCur=listEncs[e.Row];
			formEE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEncounterEdit formEE=new FormEncounterEdit();
			formEE.EncCur=new Encounter();
			formEE.EncCur.PatNum=PatCur.PatNum;
			formEE.EncCur.ProvNum=PatCur.PriProv;
			formEE.EncCur.DateEncounter=DateTime.Today;
			formEE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}
	}
}