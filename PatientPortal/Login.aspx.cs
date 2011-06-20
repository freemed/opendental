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

namespace PatientPortal {
	public partial class Login:System.Web.UI.Page {
		private long DentalOfficeID=0;
		protected void Page_Load(object sender,EventArgs e) {

		}

		protected void ButtonLogin_Click(object sender,EventArgs e) {
			try { 
				string user=TextBoxUserName.Text;
				string pwd=TextBoxPassword.Text;
				if(Request["DentalOfficeID"]!=null) {
					Int64.TryParse(Request["DentalOfficeID"].ToString().Trim(),out DentalOfficeID);
					SetCookie(DentalOfficeID);
				}
				else {
					HttpCookie DentalOfficeIDCookie=Request.Cookies["DentalOfficeIDCookie"];
					Int64.TryParse(DentalOfficeIDCookie.Value,out DentalOfficeID);
				}
				Patientm pat=Patientms.GetOne(DentalOfficeID,user,pwd);
				if(pat==null) {
					LabelMessage.Text="Login Failed, Please Try Again";
				}else{
					Session["Patient"]=pat;
					Response.Redirect("~/PatientInformation.aspx",false);// the second parameter ensures that an ThreadAbortException is not thrown and further lines of code(if any) are executed.
				}
			}
			catch(Exception ex) {
				Logger.LogError(ex);
			}
		}

		/// <summary>
		/// this cookie is used to retrieve the DentalOfficeID incase the session times out. The login depends on 3 parmeters 1)the username 2)the password and 3) the DentalOfficeID.
		/// </summary>
		private void SetCookie(long DentalOfficeID) {
			HttpCookie DentalOfficeIDCookie=new HttpCookie("DentalOfficeIDCookie");
			DentalOfficeIDCookie.Value=DentalOfficeID.ToString();
			DentalOfficeIDCookie.Expires=DateTime.Now.AddYears(1);
			Response.Cookies.Add(DentalOfficeIDCookie);
		}




	}
}