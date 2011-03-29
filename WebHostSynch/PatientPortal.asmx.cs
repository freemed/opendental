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
	/// Summary description for PatientPortal
	/// </summary>
	[WebService(Namespace="http://opendental.com/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class PatientPortal:System.Web.Services.WebService {
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
		public void DeleteAllRecords(String RegistrationKey) {
			try {
				Logger.Information("In DeleteAllRecords");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				Medicationms.DeleteAll(customerNum);
				MedicationPatms.DeleteAll(customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchMedications(String RegistrationKey,List<Medicationm> medicationmList) {
			try {
				Logger.Information("In SynchMedications");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				Medicationms.UpdateFromChangeList(medicationmList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchMedicationPats(String RegistrationKey,List<MedicationPatm> medicationPatList) {
			try {
				Logger.Information("In SynchMedicationPats");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				MedicationPatms.UpdateFromChangeList(medicationPatList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}


		[WebMethod]
		public void SynchMedicationPats(String RegistrationKey,List<MedicationPatm> medicationPatList) {
			try {
				Logger.Information("In SynchMedicationPats");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				MedicationPatms.UpdateFromChangeList(medicationPatList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchAllergies(String RegistrationKey,List<MedicationPatm> medicationPatList) {
			try {
				Logger.Information("In SynchMedicationPats");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				MedicationPatms.UpdateFromChangeList(medicationPatList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchAllergyDefs(String RegistrationKey,List<MedicationPatm> medicationPatList) {
			try {
				Logger.Information("In SynchMedicationPats");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				MedicationPatms.UpdateFromChangeList(medicationPatList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}


		[WebMethod]
		public void SynchDiseases(String RegistrationKey,List<MedicationPatm> medicationPatList) {
			try {
				Logger.Information("In SynchMedicationPats");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				MedicationPatms.UpdateFromChangeList(medicationPatList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}


		[WebMethod]
		public void SynchDiseaseDefs(String RegistrationKey,List<MedicationPatm> medicationPatList) {
			try {
				Logger.Information("In SynchMedicationPats");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				MedicationPatms.UpdateFromChangeList(medicationPatList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchICD9s(String RegistrationKey,List<MedicationPatm> medicationPatList) {
			try {
				Logger.Information("In SynchMedicationPats");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				MedicationPatms.UpdateFromChangeList(medicationPatList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}
					


	}
}
