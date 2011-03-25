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

namespace ODWebsite {
	public class Util {

		public void SetDbConnection() {
			string connectStr=Properties.Settings.Default.DBPatientPortal;
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			dc.SetDb(connectStr,"",DatabaseType.MySql,true);
		}


		public long GetDentalOfficeID(string username,string password) {
			long DentalOfficeID=0;
			String md5password=new WebHostSynch.Util().MD5Encrypt(password);
			try {
				// a query involving both username and password is used because 2 dental offices could potentially have the same username
				String command="SELECT * FROM userm WHERE UserName='"+POut.String(username)+"' AND Password='" +POut.String(md5password)+"'";
				//String command="SELECT * FROM userm WHERE UserName='"+POut.String(username)+"'"; Old query
				Userm um=Userms.GetOne(command);
				if(um==null) {
					DentalOfficeID=0;//user password combination incorrect- specify message if necessary
				}
				else {
					DentalOfficeID=um.CustomerNum;
				}

			}
			catch(Exception ex) {
				Logger.LogError(ex);
				return DentalOfficeID;
			}
			return DentalOfficeID;
		}







	}


}