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
			string connectStr=Properties.Settings.Default.DBPatientPortal;
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			dc.SetDb(connectStr,"",DatabaseType.MySql,true);
		}
		///<summary>Dennis ToDo: this method has to be revised</summary>
		public void LoadPreferences(long customerNum) {
			PrefmC prefmC=Prefms.LoadPreferences(customerNum);
			HttpContext.Current.Session["prefmC"]=prefmC;
		}

		///<summary>Dennis ToDo: this method has to be revised</summary>
		public string GetString(PrefmName prefmName) {
			PrefmC prefmC=(PrefmC)HttpContext.Current.Session["prefmC"];
			return prefmC.GetString(prefmName);

		}

	}
}