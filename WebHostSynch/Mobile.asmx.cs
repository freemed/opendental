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
			try{
				util.SetMobileDbConnection();
				Logger.Information("in ServiceExists()");
				return true;
			}catch(Exception ex) {
				Logger.LogError(ex);
				return false;
			}
		}

		[WebMethod]
		public long GetCustomerNum(string RegistrationKeyFromDentalOffice) {
			return util.GetDentalOfficeID(RegistrationKeyFromDentalOffice);
		}

		[WebMethod]
		public bool IsPaidCustomer(String RegistrationKey) {
			bool IsPaidCustomer=false;
			try {
				Logger.Information("In IsPaidCustomer");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum!=0) {
					IsPaidCustomer=util.IsPaidCustomer(customerNum);
				}
				return IsPaidCustomer;
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
				return IsPaidCustomer;
			}
		}

		[WebMethod]
		public void DeleteAllRecords(String RegistrationKey) {
			try {
				Logger.Information("In DeleteAllRecords");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				Patientms.DeleteAll(customerNum);
				Appointmentms.DeleteAll(customerNum);
				RxPatms.DeleteAll(customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
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
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
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
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}
		
		[WebMethod]
		public void SynchPrescriptions(String RegistrationKey,List<RxPatm> rxList) {
			try {
				Logger.Information("In SynchPrescriptions");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				RxPatms.UpdateFromChangeList(rxList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public string GetUserName(String RegistrationKey) {
			String UserName="";
			try {
				Logger.Information("In GetUserName");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum!=0) {
					UserName=util.GetMobileWebUserName(customerNum);
				}
				return UserName;
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
				return UserName;
			}
		}

		[WebMethod]
		public void SetMobileWebUserPassword(String RegistrationKey,String UserName,String Password) {
			try {
				Logger.Information("In SetMobileWebPassword");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				else {
					util.SetMobileWebUserPassword(customerNum,UserName,Password);
				}
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

	}
}
