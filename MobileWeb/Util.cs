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

		public bool AllowUser(string user,string password) {
			String md5password=new WebHostSynch.Util().MD5Encrypt(password);
			String command="SELECT UserName,Password FROM userm WHERE UserName='"+POut.String(user)+"'";
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			DataTable table=dc.GetTable(command);
			String dbpassword="";
			if(table.Rows.Count==0) {
				//user not found
				return false;
			}
			else if(table.Rows.Count>0) {
				dbpassword=table.Rows[0]["Password"].ToString();
				if(md5password==dbpassword) {
					return true;
				}
			}
			else {
				return false;
			}

			return false;
		}
	}
}