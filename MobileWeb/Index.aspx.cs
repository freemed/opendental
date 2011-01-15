using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileWeb {
	public partial class Index:System.Web.UI.Page {
		protected void Page_Load(object sender,EventArgs e) {

			if(Request.Cookies["UserNameCookie"] != null) {
				HttpCookie UserNameCookie=Request.Cookies["UserNameCookie"];
				username.Text=UserNameCookie.Value;
				rememberusername.Checked=true;
			}

		}
	}
}