using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace MobileWeb {
	public partial class PharmacyDetails:System.Web.UI.Page {
		private long PharmacyNum=0;
		private long CustomerNum=0;
		private Util util=new Util();
		public Pharmacym phar;
		protected void Page_Load(object sender,EventArgs e) {
			try {
				CustomerNum=util.GetCustomerNum(Message);
				if(CustomerNum==0) {
					return;
				}
				if(Request["PharmacyNum"]!=null) {
					Int64.TryParse(Request["PharmacyNum"].ToString().Trim(),out PharmacyNum);
				}
				phar=Pharmacyms.GetOne(CustomerNum,PharmacyNum);
			}
			catch(Exception ex) {
				LabelError.Text=Util.ErrorMessage;
				Logger.LogError(ex);
			}

		}
	}
}