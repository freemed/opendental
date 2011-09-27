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


namespace PatientPortalMVC.Models {
	public class LoginModel {

		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
	}


	public class PatientInformationModel {
		public Patientm patm { get; set; }
		public class LabResults {
			public List<LabResultm> mLabResultmList { get; set; }
		}

		public List<Statementm> mStatementmList { get; set; }
		public List<LabPanelm> mLabPanelmList { get; set; }
		public DataTable mMedicationmDataTable { get; set; }
		public DataTable mDiseasemDataTable { get; set; }
		public DataTable mAllergymDataTable { get; set; }

		public string MessageLabPanel { get; set; }
		public string MessageMedication { get; set; }
		public string MessageProblem { get; set; }
		public string MessageAllergy{ get; set; }

		public PatientInformationModel(Patientm patm) {//constructor
			this.patm=patm;
			FillData();
		}

		public LabResults GetLabResult(long LabPanelNum) {
			LabResults lr = new LabResults();// a vaiable made from the above declared  inner class LabResults
			lr.mLabResultmList=LabResultms.GetLabResultms(patm.CustomerNum,LabPanelNum);
			return lr;
		}

		public  PatientInformationModel FillData() {
			mStatementmList=Statementms.GetStatementms(patm.CustomerNum,patm.PatNum);

			mLabPanelmList=LabPanelms.GetLabPanelms(patm.CustomerNum,patm.PatNum);
			if(mLabPanelmList.Count==0) {
				MessageLabPanel="Lab Panels: No Lab Panels found";
			}
			mMedicationmDataTable=MedicationPatms.GetMedicationmDetails(patm.CustomerNum,patm.PatNum);
			//mMedicationPatmList=mMedicationPatmList.Where(mp => mp.DateStop==DateTime.MinValue).ToList();// filter out discontinued medications.
			if(mMedicationmDataTable.Rows.Count==0) {
				MessageMedication="Medications: No Medications found";
			}
			mDiseasemDataTable=Diseasems.GetDiseasemDetails(patm.CustomerNum,patm.PatNum);
			//mDiseasemDescriptionList=mDiseasemDescriptionList.Where(d => d.ProbStatus==ProblemStatus.Active & d.ICD9Num!=0).ToList();// get only active diseases and ones where the ICD9NUM is not zero. ICD9NUM and DiseaseDefNum are mutually exculsive. If one is zero the other is not.
			if(mDiseasemDataTable.Rows.Count==0) {
				MessageProblem="Problems: No Problems found";
			}
			mAllergymDataTable=Allergyms.GetAllergymDetails(patm.CustomerNum,patm.PatNum);
			//mAllergymList=mAllergymList.Where(a => a.StatusIsActive==true).ToList();// get only active allergies
			if(mAllergymDataTable.Rows.Count==0) {
				MessageAllergy="Allergies: No Allergies found";
			}
			return this;
		}

}

			






}