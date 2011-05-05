using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using WebForms;

namespace PatientPortal {
	public partial class PatientInformation:System.Web.UI.Page {
		protected void Page_Load(object sender,EventArgs e) {
			try {
				if(Session["Patient"]==null) {
					Response.Redirect("~/Login.aspx");
				}
				LabelPatientName.Text=((Patientm)Session["Patient"]).LName + " " +((Patientm)Session["Patient"]).FName;
				List<LabPanelm> mLabPanelmList= LabPanelms.GetLabPanelms(((Patientm)Session["Patient"]).CustomerNum,((Patientm)Session["Patient"]).PatNum);
				GridViewLabPanel.DataSource = mLabPanelmList;
				GridViewLabPanel.DataBind();
				if(mLabPanelmList.Count==0) {
					LabelLabPanel.Text="Lab Panels: No Lab Panels found";
				}
				List<MedicationPatm> mMedicationPatmList= MedicationPatms.GetMedicationPatms(((Patientm)Session["Patient"]).CustomerNum,((Patientm)Session["Patient"]).PatNum);
				mMedicationPatmList=mMedicationPatmList.Where(mp=>mp.IsDiscontinued==false).ToList();// filter out discontinued medications.
				GridViewMedication.DataSource=mMedicationPatmList;
				GridViewMedication.DataBind();
				if(mMedicationPatmList.Count==0) {
					LabelMedication.Text="Medications: No Medications found";
				}
				List<Diseasem> mDiseasemList= Diseasems.GetDiseasems(((Patientm)Session["Patient"]).CustomerNum,((Patientm)Session["Patient"]).PatNum);
				mDiseasemList=mDiseasemList.Where(d=>d.ProbStatus==ProblemStatus.Active & d.ICD9Num!=0).ToList();// get only active diseases and ones where the ICD9NUM is not zero. ICD9NUM and DiseaseDefNum are mutually exculsive. If one is zero the other is not.
				GridViewProblem.DataSource=mDiseasemList;
				GridViewProblem.DataBind();
				if(mDiseasemList.Count==0) {
					LabelProblem.Text="Problems: No Problems found";
				}
				List<Allergym> mAllergymList=Allergyms.GetAllergyms(((Patientm)Session["Patient"]).CustomerNum,((Patientm)Session["Patient"]).PatNum);
				mAllergymList=mAllergymList.Where(a=>a.StatusIsActive==true).ToList();// get only active allergies
				GridViewAllergy.DataSource=mAllergymList;
				GridViewAllergy.DataBind();
				if(mAllergymList.Count==0) {
					LabelAllergy.Text="Allergies: No Allergies found";
				}
			}catch(Exception ex) {
			Logger.LogError(ex);
			}
		}

		protected void GridViewLabPanel_RowDataBound(object sender,GridViewRowEventArgs e) {
			try { 
				if (e.Row.RowType == DataControlRowType.DataRow){
					long LabPanelNum=((LabPanelm)e.Row.DataItem).LabPanelNum;
					GridView GridViewLabResult = (GridView)e.Row.FindControl("GridViewLabResult");
					List<LabResultm> mLabResultmList= LabResultms.GetLabResultms(((Patientm)Session["Patient"]).CustomerNum,LabPanelNum);
					GridViewLabResult.DataSource = mLabResultmList;
					GridViewLabResult.DataBind();
				}
			}
			catch(Exception ex) {
				Logger.LogError(ex);
			}
		}




	}
}