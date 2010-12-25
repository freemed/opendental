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

		protected void Page_Load(object sender,EventArgs e) {
			Message.Text="";
			if(Session["CustomerNum"]!=null) {
				Message.Text="LoggedIn";
				Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
				List<Patientm> patientmList=Patientms.GetPatientmsForList(CustomerNum);
				
				Repeater1.DataSource=patientmList;
				Repeater1.DataBind();
			}


		}
	}
}