using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OpenDentBusiness;

namespace WebApplication.UserControls {
	public partial class ContrApptSheet:System.Web.UI.UserControl {
		///<summary>Line height.</summary>
		public static int Lh=12;
		///<summary>The width of each operatory.</summary>
		public static int ColWidth=120;

		protected void Page_Load(object sender,EventArgs e) {
			
		}
	}
}