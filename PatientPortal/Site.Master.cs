using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenDentBusiness.Mobile;

namespace PatientPortal {
	public partial class Site:System.Web.UI.MasterPage {
		protected void Page_Load(object sender,EventArgs e) {
			if(Session["Patient"]==null) {
				LinkButtonLoginStatus.Text="Login";
			}
			else {
				LinkButtonLoginStatus.Text="Logout";
			}

		}

		protected void LinkButtonLoginStatus_Click(object sender,EventArgs e) {
			if(LinkButtonLoginStatus.Text=="Logout") {
				long DentalOfficeID=((Patientm)Session["Patient"]).CustomerNum;
				Session["Patient"]=null;
				Response.Redirect("~/Login.aspx?DentalOfficeID="+DentalOfficeID);
			}
			if(LinkButtonLoginStatus.Text=="Login") {
				Response.Redirect("~/Login.aspx");
			}
		}


	}
}