using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Data;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using WebForms;

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

		public void SetMobileWebUserPassword(long customerNum,String UserName,String Password) {
			String command="INSERT INTO userm (CustomerNum,UserName,Password) VALUES ("+POut.Long(customerNum)+",'"+POut.String(UserName)+"','"+POut.String(MD5Encrypt(Password))+"')ON DUPLICATE KEY UPDATE UserName='"+POut.String(UserName)+"',Password='"+POut.String(MD5Encrypt(Password))+"'";
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			dc.NonQ(command);
		}

		public string GetMobileWebUserName(long customerNum){
			String command="SELECT UserName FROM userm WHERE CustomerNum="+POut.Long(customerNum);
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			DataTable table=dc.GetTable(command);
			String UserName="";
			if(table.Rows.Count!=0) {
				UserName=table.Rows[0][0].ToString();
			}
			return UserName;
		}

		private string MD5Encrypt(string data) {
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			data=data+"saturn";
			byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
			return Encoding.UTF8.GetString(result);
		}

	}
}