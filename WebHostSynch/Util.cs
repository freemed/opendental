using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using WebForms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace WebHostSynch {
	public class Util {

		private static bool IsMobileDBSet=false;
		string previousConnectStr="";

		/// <summary>
		/// This method is redundant. It may be deleted later. Some older versions of OD may use this method.
		/// </summary>
		public bool CheckRegistrationKey(string RegistrationKeyFromDentalOffice) {
			Logger.Information("In CheckRegistrationKey() RegistrationKeyFromDentalOffice="+RegistrationKeyFromDentalOffice);
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			RegistrationKey RegistrationKeyFromDb=null;
			try {
				RegistrationKeys registrationKeys=new RegistrationKeys();
				registrationKeys.SetDb(connectStr);
				RegistrationKeyFromDb=registrationKeys.GetByKey(RegistrationKeyFromDentalOffice);
				DateTime d1=new DateTime(1902,1,1);
				if(d1<RegistrationKeyFromDb.DateDisabled && RegistrationKeyFromDb.DateDisabled<DateTime.Today) {
					Logger.Information("RegistrationKey has been disabled. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
					return false;
				}
				if(d1<RegistrationKeyFromDb.DateEnded && RegistrationKeyFromDb.DateEnded<DateTime.Today) {
					Logger.Information("RegistrationKey DateEnded date is past. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
					return false;
				}
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				Logger.Information("Exception thrown. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKeyFromDentalOffice);
				return false;
			}
			return true;
		}

		public void SetMobileDbConnection() {
			/*Logger.Information("In SetMobileDbConnection()");
			string connectStr=ConfigurationManager.ConnectionStrings["DBMobileWeb"].ConnectionString;
			if(previousConnectStr!=connectStr) {
				IsMobileDBSet=false;// this situation would occur if the connection sting in the  web.config file
			}
			if(!IsMobileDBSet) {
				OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
				dc.SetDb(connectStr,"",DatabaseType.MySql,true);
				IsMobileDBSet=true;
			}
			*/
			DbInit.Init(); // The above code works but this is a cleaner.
		}
		public long GetDentalOfficeID(string RegistrationKeyFromDentalOffice) {
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			RegistrationKey RegistrationKeyFromDb=null;
			try {
				RegistrationKeys registrationKeys=new RegistrationKeys();
				registrationKeys.SetDb(connectStr);
				RegistrationKeyFromDb=registrationKeys.GetByKey(RegistrationKeyFromDentalOffice);
				DateTime d1=new DateTime(1902,1,1);
				if(d1<RegistrationKeyFromDb.DateDisabled && RegistrationKeyFromDb.DateDisabled<DateTime.Today) {
					Logger.Information("RegistrationKey has been disabled. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
					return 0;
				}
				if(d1<RegistrationKeyFromDb.DateEnded && RegistrationKeyFromDb.DateEnded<DateTime.Today) {
					Logger.Information("RegistrationKey DateEnded date is past. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
					return 0;
				}
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				Logger.Information("Exception thrown. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKeyFromDentalOffice);
				return 0;
			}
			return RegistrationKeyFromDb.PatNum;
		}


	}
}