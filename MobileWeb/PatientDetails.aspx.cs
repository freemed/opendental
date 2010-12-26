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
	public partial class PatientDetails:System.Web.UI.Page {
		public Patientm pat;
		private long PatNum=0;
		private long CustomerNum=0;
		protected void Page_Load(object sender,EventArgs e) {
			Message.Text="";
			if(Session["CustomerNum"]!=null) {
				Message.Text="LoggedIn";
				if(Request["PatNum"]!=null) {
					Int64.TryParse(Request["PatNum"].ToString().Trim(),out PatNum);
				}
				Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
				pat=Patientms.GetOne(CustomerNum,PatNum);
			}
		}
	}
}