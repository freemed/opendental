using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms;
using WebHostSynch;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace ODWebsite {
	public partial class Login:System.Web.UI.Page {
		protected void Page_Load(object sender,EventArgs e) {

		}

		protected void ButtonLogin_Click(object sender,EventArgs e) {
			string user=TextBoxUserName.Text;
			string pwd =TextBoxPassword.Text;
			//if(user=="dennis") {
				SetPatient();
				Response.Redirect("~/PatientInformation.aspx");
			//}
			//else {
				LabelMessage.Text="Login Failed, Please Try Again";
			//}
		}




		public void SetPatient() {
			//if authenticated
			Patientm pat=Patientms.GetOne(6219,7);
			Session["Patient"]=pat;
		}



	}
}