using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using WebForms;


namespace WebHostSynch {
	/// <summary>
	/// Summary description for Mobile
	/// </summary>
	[WebService(Namespace="http://opendental.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class Mobile:System.Web.Services.WebService {
		private Util util=new Util();
		private long customerNum=0;

		/// <summary>
		/// An empty method to test if the webservice is up and running. this was made with the intention of testing the correctness of the webservice URL on an Open Dental Installation. If an incorrect webservice URL is used in a background thread of OD the exception cannot be handled easily.
		/// </summary>
		[WebMethod]
		public bool ServiceExists() {
			util.SetMobileDbConnection();
			return true;
		}

		[WebMethod]
		public long GetCustomerNum(string RegistrationKeyFromDentalOffice) {
			return util.GetDentalOfficeID(RegistrationKeyFromDentalOffice);
		}

		[WebMethod]
		public void SynchPatients(String RegistrationKey,List<Patientm> patientmList) {
			try {
				Logger.Information("In SynchPatients");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				Patientms.UpdateFromChangeList(patientmList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError(ex);
			}
		}

		[WebMethod]
		public void SynchAppointments(String RegistrationKey,List<Appointmentm> appointmentList) {
			try {
				Logger.Information("In SynchAppointments");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				Appointmentms.UpdateFromChangeList(appointmentList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError(ex);
			}
		}

		[WebMethod]
		public void SetMobileWebPassword(String RegistrationKey,String Password) {
		}

	}
}
