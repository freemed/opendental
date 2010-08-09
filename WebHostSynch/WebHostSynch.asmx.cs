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
		public List<webforms_sheetfield> GetSheetDataNew(int DentalOfficeID) {

			ODWebServiceEntities db = new ODWebServiceEntities();
			webforms_sheet NewSheetObj = new webforms_sheet();
			var wsfObj = from wsf in db.webforms_sheetfield where wsf.webforms_sheet.webforms_preference.DentalOfficeID==DentalOfficeID
						 select wsf;

			return wsfObj.ToList();

		}




	}


}
