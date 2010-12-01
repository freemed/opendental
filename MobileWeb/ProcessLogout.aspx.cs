using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileWeb {
	public partial class ProcessLogout:System.Web.UI.Page {

		protected void Page_Load(object sender,EventArgs e) {

					Session["userid"]=null;
					Message.Text="LoggedOut";
			

		}
	}
}