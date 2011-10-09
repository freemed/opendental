using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using WebForms;

namespace PatientPortalMVC.Models
{

		public class MedicalModel {
			public Patientm patm;
			public class LabResults {//inner class used for convenience
				public List<LabResultm> mLabResultmList;
			}

			public List<Statementm> mStatementmList;
			public List<LabPanelm> mLabPanelmList;
			public DataTable mMedicationmDataTable;
			public DataTable mDiseasemDataTable;
			public DataTable mAllergymDataTable;

			public string MessageLabPanel;
			public string MessageMedication;
			public string MessageProblem;
			public string MessageAllergy;

			public MedicalModel(Patientm patm) {//constructor
				this.patm=patm;
				FillData();
			}

			public LabResults GetLabResult(long LabPanelNum) {
				LabResults lr = new LabResults();// a variable made from the above declared  inner class LabResults
				lr.mLabResultmList=LabResultms.GetLabResultms(patm.CustomerNum,LabPanelNum);
				return lr;
			}

			public MedicalModel FillData() {
				mStatementmList=Statementms.GetStatementms(patm.CustomerNum,patm.PatNum);
				mLabPanelmList=LabPanelms.GetLabPanelms(patm.CustomerNum,patm.PatNum);
				if(mLabPanelmList.Count==0) {
					MessageLabPanel="Lab Panels: No Lab Panels found";
				}
				mMedicationmDataTable=MedicationPatms.GetMedicationmDetails(patm.CustomerNum,patm.PatNum);
				if(mMedicationmDataTable.Rows.Count==0) {
					MessageMedication="Medications: No Medications found";
				}
				mDiseasemDataTable=Diseasems.GetDiseasemDetails(patm.CustomerNum,patm.PatNum);
				if(mDiseasemDataTable.Rows.Count==0) {
					MessageProblem="Problems: No Problems found";
				}
				mAllergymDataTable=Allergyms.GetAllergymDetails(patm.CustomerNum,patm.PatNum);
				if(mAllergymDataTable.Rows.Count==0) {
					MessageAllergy="Allergies: No Allergies found";
				}
				return this;
			}

		}

	
}
