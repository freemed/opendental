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

		public bool IsPaidCustomer(long customerNum) {
			int count=0;
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			string command ="SELECT COUNT(*) FROM repeatcharge WHERE PatNum="+POut.Long(customerNum)+
						" AND ProcCode='027' AND (DateStop='0001-01-01' OR DateStop > NOW())";
			WebHostSynch.Db db = new WebHostSynch.Db();
			db.setConn(connectStr);
			DataTable table=db.GetTable(command);
			if(table.Rows.Count!=0) {
				count=PIn.Int(table.Rows[0][0].ToString());
			}
			if(count>0) {
				return true;
			}
			else {
				return false;
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
				Logger.Information("Exception thrown. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKeyFromDentalOffice);
				return 0;
			}
			return RegistrationKeyFromDb.PatNum;
		}

		public void SetMobileWebUserPassword(long customerNum,String UserName,String Password) {
			Userm um=Userms.GetOne(customerNum,1);
			bool UserExists=true;
			if(um==null) {
				um=new Userm();
				UserExists=false;
			}
			um.CustomerNum=customerNum;
			um.UsermNum=1;//always 1
			um.UserName=UserName;
			if(Password!="") {//do not update password if password string is empty
				um.Password=MD5Encrypt(Password);
			}
			if(UserExists) {
				Userms.Update(um);
			}
			else{
				Userms.Insert(um);
			}
		}

		public string GetMobileWebUserName(long customerNum){
			String UserName="";
			Userm um=Userms.GetOne(customerNum,1);
			if(um!=null) {
				UserName=um.UserName;
			}
			return UserName;
		}

		public string MD5Encrypt(string inputPass) {
		/*
				byte[] unicodeBytes=Encoding.Unicode.GetBytes(inputPass);
				HashAlgorithm algorithm=MD5.Create();
				byte[] hashbytes2=algorithm.ComputeHash(unicodeBytes);
				return Convert.ToBase64String(hashbytes2);
		 */
			String salt="saturn";
			return Userods.EncryptPassword(inputPass+salt,false);
		}

	}
}