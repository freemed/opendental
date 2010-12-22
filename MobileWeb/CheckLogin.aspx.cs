using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileWeb {
	public partial class CheckLogin:System.Web.UI.Page {
		protected void Page_Load(object sender,EventArgs e) {
			Message.Text="";
			if(Session["CustomerNum"]!=null) {
				Message.Text="LoggedIn";
			}
		}
	}
}