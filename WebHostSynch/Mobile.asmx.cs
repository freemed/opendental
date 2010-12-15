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
			return true;
		}

		/// <summary>A method which is used to decided if certain missing records are to be transferred to the webserver. This is especially true when synchronization happens for the very first time when there are no records on the  webserver./// </summary>
		[WebMethod]
		public int GetRecordCount(String RegistrationKey,List<Patientm> patientmList) {
			customerNum=util.GetDentalOfficeID(RegistrationKey);
			if(customerNum==0) {
				return 0;
			}
			return 0;
		}

		[WebMethod]
		public void SynchRecords(String RegistrationKey,List<Patientm> patientmList) {

			customerNum=util.GetDentalOfficeID(RegistrationKey);
			if(customerNum==0) {
				return;
			}

			for(int i=0;i<patientmList.Count();i++) {
				if(patientmList[i].PatStatus==PatientStatus.Deleted) {
					Patientms.Delete(customerNum,patientmList[i].PatNum);
					patientmList.RemoveAt(i);
				}

			}
			for(int i=0;i<patientmList.Count();i++) {
				Patientms.UpdateFromChangeList(patientmList,customerNum);
				
			}

		}

		[WebMethod]
		public void DeleteRecords(String RegistrationKey,List<long> patientNums) {


		}

		[WebMethod]
		public void SetMobileWebPassword(String RegistrationKey,String Password) {
		}

	}
}
