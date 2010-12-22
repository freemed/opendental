using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using WebForms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace MobileWeb {
	public class Util {
		private static bool IsMobileDBSet=false;
		string previousConnectStr="";
		
		public void SetMobileDbConnection() {
			Logger.Information("In SetMobileDbConnection()");
			string connectStr=Properties.Settings.Default.DBMobileWeb;
			if(previousConnectStr!=connectStr) {
				IsMobileDBSet=false;// this situation would occur if the connection sting in the  web.config file
			}
			if(!IsMobileDBSet) {
				OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
				dc.SetDb(connectStr,"",DatabaseType.MySql,true);
				IsMobileDBSet=true;
			}
		}
	}
}