using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ODWebsite {
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
				Session["Patient"]=null;
				Response.Redirect("~/Login.aspx");
			}
			if(LinkButtonLoginStatus.Text=="Login") {
				Response.Redirect("~/Login.aspx");
			}
		}


	}
}