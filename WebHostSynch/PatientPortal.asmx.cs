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
				Medicationms.DeleteAll(customerNum);
				MedicationPatms.DeleteAll(customerNum);
				AllergyDefms.DeleteAll(customerNum);
				Allergyms.DeleteAll(customerNum);
				DiseaseDefms.DeleteAll(customerNum);
				Diseasems.DeleteAll(customerNum);
				ICD9ms.DeleteAll(customerNum);
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
		public void SynchAllergies(String RegistrationKey,List<Allergym> allergyList) {
			try {
				Logger.Information("In SynchAllergies");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				Allergyms.UpdateFromChangeList(allergyList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchAllergyDefs(String RegistrationKey,List<AllergyDefm> allergyDefList) {
			try {
				Logger.Information("In SynchAllergyDefs");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				AllergyDefms.UpdateFromChangeList(allergyDefList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchDiseases(String RegistrationKey,List<Diseasem> diseaseList) {
			try {
				Logger.Information("In SynchDiseases");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				Diseasems.UpdateFromChangeList(diseaseList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchDiseaseDefs(String RegistrationKey,List<DiseaseDefm> diseaseDefList) {
			try {
				Logger.Information("In SynchDiseaseDefs");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				DiseaseDefms.UpdateFromChangeList(diseaseDefList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}

		[WebMethod]
		public void SynchICD9s(String RegistrationKey,List<ICD9m> icd9List) {
			try {
				Logger.Information("In SynchICD9s");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				ICD9ms.UpdateFromChangeList(icd9List,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
			}
		}
					
	

	}
}
