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
		public string patFName="";
		private long PatNum=0;
		protected void Page_Load(object sender,EventArgs e) {
			Message.Text="";
			if(Session["userid"]!=null) {
				Message.Text="LoggedIn";
				////Thread.Sleep(500);
				if(Request["PatNum"]!=null) {
					Int64.TryParse(Request["PatNum"].ToString().Trim(),out PatNum);
				}
				pat=Patientms.GetOne(1486,PatNum);

				

			}
		}
	}
}