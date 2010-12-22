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
	public partial class PatientList:System.Web.UI.Page {
		
		private Util util=new Util();

		protected void Page_Load(object sender,EventArgs e) {
			util.SetMobileDbConnection();
			List<Patientm> patientmList=Patientms.GetPatients(1486);
			Message.Text="";
			if(Session["userid"]!=null) {
				Message.Text="LoggedIn";
				Repeater1.DataSource=patientmList;
				Repeater1.DataBind();
			}


		}
	}
}