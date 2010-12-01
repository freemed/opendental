using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace MobileWeb {
	public partial class AppointmentList:System.Web.UI.Page {
		
		protected void Page_Load(object sender,EventArgs e) {

			Message.Text="";
			if(Session["userid"]!=null) {
				Message.Text="LoggedIn";

				string[] ar= { "Appointmnet1","Appointmnet2","Appointmnet3","Appointmnet4","Appointmnet5" };

				var somevar = ar.Where(a => a.Contains("Appointmnet"));
				Repeater1.DataSource = somevar;
				Repeater1.DataBind();
			}


		}
	}
}