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

		/// <summary>
		/// An empty method to test if the webservice is up and running. this was made with the intention of testing the correctness of the webservice URL on an Open Dental Installation. If an incorrect webservice URL is used in a background thread of OD the exception cannot be handled easily.
		/// </summary>
		[WebMethod]
		public bool ServiceExists() {
			return true;
		}
		
		[WebMethod]
		public DateTime GetLastDateTStampOfPatients(String RegistrationKey) {
			return DateTime.Today;
		}

		[WebMethod]
		public DateTime GetLastDateTStampOfAppointments(String RegistrationKey) {
			return DateTime.Today;
		}

		[WebMethod]
		public void SynchRecords(String RegistrationKey,List<Patientm> patientmList) {
		}

		[WebMethod]
		public void DeleteRecords(String RegistrationKey,List<long> patientNums) {

			/*
OpenDentBusiness\Mobile\Data Interface\Patientms.cs (uses PatientmCrud.cs to manupulate Patientm)

Methods that would be used exclusively  for webserver for web interface
Patientm GetPatm(long patNum) 


Methods that would be used exclusively on the webservice for updating data form an OD installation.

Insert(Patientm pat) 
Update(Patientm patient,Patientm oldPatient) 
Delete(Patientm pat)
SynchRecords(List<Patientm>) (method recieves a list that is returned by the GetNewAndUpdatedPatients() form an OD installation.)
*/

			/*
 * Crud\PatientmCrud.cs - this class won't be directly used here.
 * -------------------
Patientm SelectOne(long patNum) - on web page
Patientm SelectOne (string command)-- on webpage
List<Patientm> SelectMany (string command)
Insert(Patientm pat) 
Update(Patientm patient,Patientm oldPatient) 
Delete(Patientm pat)
 
*/
		}

		[WebMethod]
		public void SetMobileWebPassword(String RegistrationKey,String Password) {
		}

	}
}


/* Methods that would be used exclusively OD installation 
List<Patientm> GetNewAndUpdatedPatients() 

Patientm GetPatm(Patient  patient) - will convert a Patient into Patientm  (not to be confused with the overloaded method which )
*/