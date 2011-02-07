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
				String command="SELECT CustomerNum,UserName,Password FROM userm WHERE UserName='"+POut.String(username)+"'";
				OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
				DataTable table=dc.GetTable(command);
				String dbpassword="";
				if(table.Rows.Count==0) {
					DentalOfficeID=0;//user not found
				}
				else if(table.Rows.Count>0) {
					dbpassword=table.Rows[0]["Password"].ToString();
					if(md5password==dbpassword) {
						DentalOfficeID=PIn.Int(table.Rows[0]["CustomerNum"].ToString());
					}
				}
				else {
					DentalOfficeID=0;
				}
			}
			catch(Exception ex) {
				Logger.LogError(ex);
				return DentalOfficeID;
			}
			if(username.ToLower()=="demo") {//for demo only
				DentalOfficeID=1486;
			}
			return DentalOfficeID;
		}
	}
}