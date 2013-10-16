using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using OpenDental;

namespace OpenDental {
	public partial class FormInterventions:Form {
		public Patient PatCur;
		private List<Intervention> listIntervention;

		public FormInterventions() {
			InitializeComponent();
		}
		
		private void FormInterventions_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Prov",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Intervention Type",115);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Code",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Code System",85);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Code Description",300);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Note",100);
			gridMain.Columns.Add(col);
			listIntervention=Interventions.Refresh(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listIntervention.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listIntervention[i].DateTimeEntry.ToShortDateString());
				row.Cells.Add(Providers.GetAbbr(listIntervention[i].ProvNum));
				row.Cells.Add(listIntervention[i].CodeSet.ToString());
				row.Cells.Add(listIntervention[i].CodeValue);
				row.Cells.Add(listIntervention[i].CodeSystem);
				//Description of Intervention---------------------------------------------
				//to get description, first determine which table the code is from.  Interventions are allowed to be SNOMEDCT, ICD9, ICD10, HCPCS, or CPT.
				string descript="";
				switch(listIntervention[i].CodeSystem) {
					case "SNOMEDCT":
						Snomed sCur=Snomeds.GetByCode(listIntervention[i].CodeValue);
						if(sCur!=null) {
							descript=sCur.Description;
						}
						break;
					case "ICD9CM":
						ICD9 i9Cur=ICD9s.GetByCode(listIntervention[i].CodeValue);
						if(i9Cur!=null) {
							descript=i9Cur.Description;
						}
						break;
					case "ICD10CM":
						Icd10 i10Cur=Icd10s.GetByCode(listIntervention[i].CodeValue);
						if(i10Cur!=null) {
							descript=i10Cur.Description;
						}
						break;
					case "HCPCS":
						Hcpcs hCur=Hcpcses.GetByCode(listIntervention[i].CodeValue);
						if(hCur!=null) {
							descript=hCur.DescriptionShort;
						}
						break;
					case "CPT":
						//no need to check for null, return new ProcedureCode object if not found, Descript will be blank
						descript=ProcedureCodes.GetProcCode(listIntervention[i].CodeValue).Descript;
						break;
				}
				row.Cells.Add(descript);
				//Reason Code-------------------------------------------------------------------
				row.Cells.Add(listIntervention[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormInterventionEdit FormInt=new FormInterventionEdit();
			FormInt.InterventionCur=listIntervention[e.Row];
			FormInt.IsCodeSetLocked=true;
			FormInt.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormInterventionEdit FormInt=new FormInterventionEdit();
			FormInt.InterventionCur=new Intervention();
			FormInt.InterventionCur.IsNew=true;
			FormInt.InterventionCur.PatNum=PatCur.PatNum;
			FormInt.InterventionCur.ProvNum=PatCur.PriProv;
			FormInt.InterventionCur.DateTimeEntry=DateTime.Now;
			FormInt.IsCodeSetLocked=false;
			FormInt.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}
	}
}