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
	public partial class AppointmentDetails:System.Web.UI.Page {
		public Appointmentm apt;
		public Patientm pat;
		private long AptNum=0;
		private long CustomerNum=0;
		protected void Page_Load(object sender,EventArgs e) {
			try {
				if(!SetCustomerNum()) {
					return;
				}
				if(Request["AptNum"]!=null) {
					Int64.TryParse(Request["AptNum"].ToString().Trim(),out AptNum);
				}
				Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
				apt=Appointmentms.GetOne(CustomerNum,AptNum); apt=null;
				pat=Patientms.GetOne(CustomerNum,apt.PatNum);
			}
			catch(Exception ex) {
				LabelError.Text="There has been an error in processing your request.";
				Logger.LogError(ex);
			}
		}

		private bool SetCustomerNum() {
			Message.Text="";
			if(Session["CustomerNum"]==null) {
				return false;
			}
			Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
			if(CustomerNum!=0) {
				Message.Text="LoggedIn";
			}
			return true;
		}


	}
}