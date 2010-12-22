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
		private long CustomerNum=0;

		protected void Page_Load(object sender,EventArgs e) {
			util.SetMobileDbConnection();
			Message.Text="";
			if(Session["CustomerNum"]!=null) {
				Int64.TryParse(Session["CustomerNum"].ToString(),out CustomerNum);
				List<Patientm> patientmList=Patientms.GetPatientsForList(CustomerNum);
				Message.Text="LoggedIn";
				Repeater1.DataSource=patientmList;
				Repeater1.DataBind();
			}


		}
	}
}