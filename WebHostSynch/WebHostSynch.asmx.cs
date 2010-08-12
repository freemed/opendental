using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebHostSynch {
	/// <summary>
	/// Summary description for WebHostSynch
	/// </summary>
	[WebService(Namespace="http://tempuri.org/")]
	[WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class WebHostSynch:System.Web.Services.WebService {

		[WebMethod]
		public string HelloWorld() {
			ODWebServiceEntities db = new ODWebServiceEntities();
			return "Hello World";
		}


		[WebMethod]
		public List<webforms_sheetfield> GetSheetData(int DentalOfficeID,string RegistrationKey,DateTime StartDate,DateTime EndDate) {
			ODWebServiceEntities db = new ODWebServiceEntities();
			if(CheckRegistrationKey(RegistrationKey)==false) {
				Logger.Information("In correct registration key. DentalOfficeID = " + DentalOfficeID + "RegistrationKey = " + RegistrationKey);
				DentalOfficeID=0;
			}
			EndDate=EndDate.AddDays(1);//if this is put in LINQ it will not work. so change date first
			var wsfObj = from wsf in db.webforms_sheetfield 
						 where wsf.webforms_sheet.webforms_preference.DentalOfficeID==DentalOfficeID
						 && (StartDate <= wsf.webforms_sheet.DateTimeSubmitted && wsf.webforms_sheet.DateTimeSubmitted <= EndDate)
						 select wsf;
			return wsfObj.ToList();

		}

		[WebMethod]
		public bool CheckRegistrationKey(string RegistrationKey) {

			return true;
		}


	}


}
