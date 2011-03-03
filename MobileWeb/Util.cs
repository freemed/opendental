using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using WebForms;
using WebHostSynch;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;

namespace MobileWeb {
	public class Util {
		public static string ErrorMessage="There has been an error in processing your request.";
		private static bool IsMobileDBSet=false;
		string previousConnectStr="";
		
		public void SetMobileDbConnection() {
			Logger.Information("In SetMobileDbConnection()");
			DbInit.Init();
		}


		public long GetDentalOfficeID(string username,string password) {
			long DentalOfficeID=0;
			String md5password=new WebHostSynch.Util().MD5Encrypt(password);
			try {
				String command="SELECT * FROM userm WHERE UserName='"+POut.String(username)+"'";
				Userm um=Userms.GetOne(command);
				String dbpassword="";
				if(um==null) {
					DentalOfficeID=0;//user not found - specify message if necessary
				}
				else {
					dbpassword=um.Password;
					if(md5password==dbpassword) {
						DentalOfficeID=um.CustomerNum;
					}
					else {
						//incorrect password - specify message if necessary
					}
				}
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				return DentalOfficeID;
			}
			if(username.ToLower()=="demo") {//for demo only
				DentalOfficeID=GetDemoDentalOfficeID();
			}
			return DentalOfficeID;
		}
		/// <summary>
		/// If Properties.Settings.Default.something is used in AppointmentList.aspx.cs page it give a  MobileWeb.Properties.Settings is inaccessible due to its protection level
		/// </summary>
		public long GetDemoDentalOfficeID() {
			return Properties.Settings.Default.DemoDentalOfficeID;
		}

		/// <summary>
		/// If Properties.Settings.Default.something is used in AppointmentList.aspx.cs page it give a  MobileWeb.Properties.Settings is inaccessible due to its protection level
		/// </summary>
		public DateTime GetDemoTodayDate() {
			return Properties.Settings.Default.DemoTodayDate;
		}
		public string GetPatientName(long PatNum,long CustomerNum) {
			try{
				String PatName="";
				Patientm pat=Patientms.GetOne(CustomerNum,PatNum);
				PatName=GetPatientName(pat);
				return PatName;
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				return "";
			}
		}

		public string GetPatientName(Patientm pat) {
			try {
				String PatName="";
				PatName+=pat.LName +", ";
				if(!String.IsNullOrEmpty(pat.Preferred)) {
					PatName+="'"+pat.Preferred +"'";
				}
				PatName+=" "+pat.FName +" ";
				if(!String.IsNullOrEmpty(pat.MiddleI)) {
					PatName+=pat.MiddleI +".";
				}
				return PatName;
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				return "";
			}
		}



	}
}