using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using WebForms;
using OpenDentBusiness;

namespace WebHostSynch {
	public class Util {

		public bool CheckRegistrationKey(string RegistrationKeyFromDentalOffice) {
			Logger.Information("In CheckRegistrationKey() RegistrationKeyFromDentalOffice="+RegistrationKeyFromDentalOffice);
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			// sets a static variable
			dc.SetDb(connectStr,"",DatabaseType.MySql,true);
			RegistrationKey RegistrationKeyFromDb=null;
			try {
				RegistrationKeyFromDb=RegistrationKeys.GetByKey(RegistrationKeyFromDentalOffice);
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

		public long GetDentalOfficeID(string RegistrationKeyFromDentalOffice) {
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			// sets a static variable
			dc.SetDb(connectStr,"",DatabaseType.MySql,true);
			RegistrationKey RegistrationKeyFromDb=null;
			try {
				RegistrationKeyFromDb=RegistrationKeys.GetByKey(RegistrationKeyFromDentalOffice);
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