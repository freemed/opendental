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
using WebApplication.UserControls;

namespace WebApplication {
	public partial class ApptModule:System.Web.UI.Page {
		protected void Page_Load(object sender,EventArgs e) {
			Session["SelectedModule"]=0;
			RefreshPeriod();
		}

		private void RefreshPeriod(){
			for(int i=1;i<10;i++) {
				ContrApptSingle aptSingle=new ContrApptSingle();
				//i,i.ToString(),DateTime.Today.AddHours(i));
				aptSingle.heightInRows=i;
				aptSingle.patientName="Patient"+i.ToString();
				aptSingle.aptDateTime=DateTime.Today.AddHours(1);
				aptSingle.op=i;
				ContrApptSheet2.Controls.Add(aptSingle);
			} 
		}


	}
}
