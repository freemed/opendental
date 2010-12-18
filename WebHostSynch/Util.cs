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
				Logger.Information("Incorrect registration key. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKeyFromDentalOffice);
				return false;
			}
			return true;
		}

		public void SetMobileDbConnection() {
			Logger.Information("In SetMobileDbConnection()");
			string connectStr=ConfigurationManager.ConnectionStrings["DBMobileWeb"].ConnectionString;
			if(previousConnectStr!=connectStr) {
				IsMobileDBSet=false;// this situation would occur if the connection sting in the  web.config file
			}
			if(!IsMobileDBSet) {
				OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
				Logger.Information("IsMobileDBSet is false");
				dc.SetDb(connectStr,"",DatabaseType.MySql,true);
				IsMobileDBSet=true;
				Logger.Information("IsMobileDBSet is true");
			}
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
				Logger.Information("Incorrect registration key. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKeyFromDentalOffice);
				return 0;
			}
			return RegistrationKeyFromDb.PatNum;
		}


	}
}