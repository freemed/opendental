using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace MobileWeb {
	public partial class AppointmentDetails:System.Web.UI.Page {
		public int id=0;
		protected void Page_Load(object sender,EventArgs e) {

			Message.Text="";
			if(Session["userid"]!=null) {
				Message.Text="LoggedIn";
				Thread.Sleep(1000);
				if(Request["id"]!=null) {
					Int32.TryParse(Request["id"].ToString().Trim(),out id);
				}
				id = id+7;
			}
		}
	}
}