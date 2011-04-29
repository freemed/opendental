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
		public void DeleteObjects(String RegistrationKey,List<DeletedObject> dOList) {
			try {
				Logger.Information("In DeleteAllRecords");
				customerNum=util.GetDentalOfficeID(RegistrationKey);
				if(customerNum==0) {
					return;
				}
				DeletedObjects.DeleteForMobile(dOList,customerNum);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
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
				//mobile web
				Patientms.DeleteAll(customerNum);
				Appointmentms.DeleteAll(customerNum);
				RxPatms.DeleteAll(customerNum);
				Providerms.DeleteAll(customerNum);
				//pat portal
				DrugUnitms.DeleteAll(customerNum);
				LabPanelms.DeleteAll(customerNum);
				LabResultms.DeleteAll(customerNum);
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

		#region MobileWeb

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
			public void SynchProviders(String RegistrationKey,List<Providerm> providerList) {
				try {
					Logger.Information("In SynchProviders");
					customerNum=util.GetDentalOfficeID(RegistrationKey);
					if(customerNum==0) {
						return;
					}
					Providerms.UpdateFromChangeList(providerList,customerNum);
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
		#endregion

		#region PatientPortal

			[WebMethod]
			public void SynchLabPanels(String RegistrationKey,List<LabPanelm> labPanelmList) {
				try {
					Logger.Information("In SynchLabPanels");
					customerNum=util.GetDentalOfficeID(RegistrationKey);
					if(customerNum==0) {
						return;
					}
					LabPanelms.UpdateFromChangeList(labPanelmList,customerNum);
				}
				catch(Exception ex) {
					Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
				}
			}

			[WebMethod]
			public void SynchLabResults(String RegistrationKey,List<LabResultm> labResultmList) {
				try {
					Logger.Information("In SynchLabResults");
					customerNum=util.GetDentalOfficeID(RegistrationKey);
					if(customerNum==0) {
						return;
					}
					LabResultms.UpdateFromChangeList(labResultmList,customerNum);
				}
				catch(Exception ex) {
					Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
				}
			}
			
		[WebMethod]
			public void SynchDrugUnits(String RegistrationKey,List<DrugUnitm> drugUnitmList) {
				try {
					Logger.Information("In SynchDrugUnits");
					customerNum=util.GetDentalOfficeID(RegistrationKey);
					if(customerNum==0) {
						return;
					}
					DrugUnitms.UpdateFromChangeList(drugUnitmList,customerNum);
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
			
		/// <summary>
		/// Deletes records assoicated with the PatNums
		/// </summary>
		[WebMethod]
			public void DeletePatientsRecords(String RegistrationKey,List<long> patNumList) {
				try {
					Logger.Information("In DeletePatientsRecords");
					customerNum=util.GetDentalOfficeID(RegistrationKey);
					if(customerNum==0) {
						return;
					}	
					for(int i=0;i<patNumList.Count;i++) {//Dennis: an inefficient loop but will work fine for the small number of records and will use existing default methods of the ms class
							// on OD if a labpanel is deleted the corresponding labresults are also deleted. This will ensure that on the webserver labresults are deleted via 	the DeleteObjects function
							// a similar situation would be true for  medications, allergydefs and disease defs.
							LabPanelms.Delete(customerNum,patNumList[i]);
							MedicationPatms.Delete(customerNum,patNumList[i]);
							Allergyms.Delete(customerNum,patNumList[i]);
							Diseasems.Delete(customerNum,patNumList[i]);
					}
				}
				catch(Exception ex) {
					Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+customerNum,ex);
				}
		}

		#endregion
	}
}
