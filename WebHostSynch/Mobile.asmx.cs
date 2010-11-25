using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OpenDentBusiness.Mobile;
using WebForms;


namespace WebHostSynch {
	/// <summary>
	/// Summary description for Mobile
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class Mobile:System.Web.Services.WebService {

		[WebMethod]
		public DateTime GetLastDateTStampOfPatients(string RegistrationKey) {
			return DateTime.Today;
		}

		[WebMethod]
		public DateTime GetLastDateTStampOfAppointments(string RegistrationKey) {
			return DateTime.Today;
		}

		[WebMethod]
		public void SynchRecords(string RegistrationKey,List<Patientm> patientmList) {
		}

		[WebMethod]
		public void DeleteRecords(List<long> patientNums) {
		}

	}
}
