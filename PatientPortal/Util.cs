using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebForms;
using WebHostSynch;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace PatientPortalMVC {
	public class Util {

		public void SetDbConnection() {
			try {
				string connectStr=Properties.Settings.Default.DBPatientPortal;
				OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
				dc.SetDb(connectStr,"",DatabaseType.MySql,true);
			}
			catch(Exception ex) {
				Logger.LogError(ex);
			}
		}


	}
}