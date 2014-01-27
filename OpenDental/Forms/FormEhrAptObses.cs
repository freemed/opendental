using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrAptObses:Form {

		private long _aptNum=0;

		public FormEhrAptObses(long AptNum) {
			InitializeComponent();
			Lan.F(this);
			_aptNum=AptNum;
		}

		private void FormEhrAptObses_Load(object sender,EventArgs e) {
			FillGridObservations();
		}

		private void FillGridObservations() {
			gridObservations.BeginUpdate();
			gridObservations.Columns.Clear();
			gridObservations.Columns.Add(new UI.ODGridColumn("Description",200));//0
			gridObservations.Columns.Add(new UI.ODGridColumn("Value Type",200));//1
			gridObservations.Columns.Add(new UI.ODGridColumn("Value",0));//2
			gridObservations.Rows.Clear();
			List<EhrAptObs> listEhrAptObses=EhrAptObses.Refresh(_aptNum);
			for(int i=0;i<listEhrAptObses.Count;i++) {
				EhrAptObs obs=listEhrAptObses[i];
				UI.ODGridRow row=new UI.ODGridRow();
				row.Tag=obs;
				Loinc loinc=Loincs.GetByCode(obs.LoincCode);
				row.Cells.Add(loinc.NameShort);//0 Description
				if(obs.ValType==EhrAptObsType.Coded) {
					row.Cells.Add(obs.ValType.ToString()+" - "+obs.ValCodeSystem);//1 Value Type
					if(obs.ValCodeSystem=="LOINC") {
						Loinc loincValue=Loincs.GetByCode(obs.ValReported);
						row.Cells.Add(loincValue.NameShort);//2 Value
					}
					else if(obs.ValCodeSystem=="SNOMEDCT") {
						Snomed snomedValue=Snomeds.GetByCode(obs.ValReported);
						row.Cells.Add(snomedValue.Description);//2 Value
					}
					else if(obs.ValCodeSystem=="ICD9") {
						ICD9 icd9Value=ICD9s.GetByCode(obs.ValReported);
						row.Cells.Add(icd9Value.ICD9Code);//2 Value
					}
					else if(obs.ValCodeSystem=="ICD10") {
						Icd10 icd10Value=Icd10s.GetByCode(obs.ValReported);
						row.Cells.Add(icd10Value.Icd10Code);//2 Value
					}
				}
				else {
					row.Cells.Add(obs.ValType.ToString());//1 Value Type
					row.Cells.Add(obs.ValReported);//2 Value
				}
				gridObservations.Rows.Add(row);
			}
			gridObservations.EndUpdate();
		}

		private void gridObservations_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			EhrAptObs obs=(EhrAptObs)gridObservations.Rows[e.Row].Tag;
			FormEhrAptObsEdit formE=new FormEhrAptObsEdit(obs);
			if(formE.ShowDialog()==DialogResult.OK) {
				if(obs.EhrAptObsNum!=0) {//Was not deleted.
					EhrAptObses.Update(obs);
				}
				FillGridObservations();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			EhrAptObs obs=new EhrAptObs();
			obs.IsNew=true;
			obs.AptNum=_aptNum;
			FormEhrAptObsEdit formE=new FormEhrAptObsEdit(obs);
			if(formE.ShowDialog()==DialogResult.OK) {
				EhrAptObses.Insert(obs);
				FillGridObservations();
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}