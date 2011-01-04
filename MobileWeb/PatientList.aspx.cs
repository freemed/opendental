using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using WebForms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace MobileWeb {
	public partial class PatientList:System.Web.UI.Page {
		private long CustomerNum=0;
		private string searchterm="";
		List<Patientm> patientmList=new List<Patientm>();
		
		protected void Page_Load(object sender,EventArgs e) {
			Message.Text="";
			if(Session["CustomerNum"]!=null) {
				//Thread.Sleep(1500);
				Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
				Message.Text="LoggedIn";
				if(Request["searchterm"]!=null) {
					searchterm=Request["searchterm"].Trim();
				}
				if(searchterm!="") {
					patientmList=Patientms.GetPatientms(CustomerNum,searchterm);
				}
				Repeater1.DataSource=patientmList;
				Repeater1.DataBind();
			}


		}
	}
}