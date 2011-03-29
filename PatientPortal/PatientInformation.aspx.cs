using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace ODWebsite {
	public partial class PatientInformation:System.Web.UI.Page {
		protected void Page_Load(object sender,EventArgs e) {
			Patientm pat=Patientms.GetOne(6219,7);
			Session["Patient"]=pat;
			
			if(Session["Patient"]==null) {
				Response.Redirect("~/Login.aspx");
			}
			LabelPatientName.Text=((Patientm)Session["Patient"]).LName + " " +((Patientm)Session["Patient"]).FName;
			List<MedicationPatm> mMedicationPatmList= MedicationPatms.GetMedicationPatms(((Patientm)Session["Patient"]).CustomerNum,((Patientm)Session["Patient"]).PatNum);
			GridViewMedication.DataSource = mMedicationPatmList;
			GridViewMedication.DataBind(); 
		}

		public string GetDiscontinued(bool IsDiscontinued) {
			if(IsDiscontinued) {
				return "discontinued";
			}
			return "";
		}


	}
}