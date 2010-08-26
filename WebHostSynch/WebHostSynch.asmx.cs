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
			return "Hello World";
		}

		[WebMethod]
		public bool SetPreferences(long DentalOfficeID,string RegistrationKey,int ColorBorder,string Heading1,string Heading2) {

			ODWebServiceEntities db=new ODWebServiceEntities();
			if(CheckRegistrationKey(RegistrationKey)==false) {
				Logger.Information("Incorrect registration key. DentalOfficeID = "+DentalOfficeID+"RegistrationKey = "+RegistrationKey);
				DentalOfficeID=0;
			}
			var wspObj = from wsp in db.webforms_preference
						 where wsp.DentalOfficeID==DentalOfficeID
						 select wsp;

			//update preference
			if(wspObj.Count()>0) {
				wspObj.First().ColorBorder=ColorBorder;
				wspObj.First().Heading1=Heading1;
				wspObj.First().Heading2=Heading2;
			}
			// if there is no entry for that dental office make a new entry.
			if(wspObj.Count()==0) {
				webforms_preference wspNewObj = new webforms_preference();
				wspNewObj.DentalOfficeID=DentalOfficeID;
				wspNewObj.ColorBorder=ColorBorder;
				wspNewObj.Heading1=Heading1;
				wspNewObj.Heading2=Heading2;
				db.AddTowebforms_preference(wspNewObj);

			}
			db.SaveChanges();
			return true;
		}

		[WebMethod]
		public List<webforms_sheetfield> GetSheetData(long DentalOfficeID,string RegistrationKey,DateTime StartDate,DateTime EndDate) {
			ODWebServiceEntities db=new ODWebServiceEntities();
			if(CheckRegistrationKey(RegistrationKey)==false) {
				Logger.Information("Incorrect registration key. DentalOfficeID = "+DentalOfficeID+"RegistrationKey = "+RegistrationKey);
				DentalOfficeID=0;
			}
			EndDate=EndDate.AddDays(1);//if this is put in LINQ it will not work. so change date first
			var wsfObj=from wsf in db.webforms_sheetfield
					   where wsf.webforms_sheet.webforms_preference.DentalOfficeID==DentalOfficeID
					   &&(StartDate<=wsf.webforms_sheet.DateTimeSubmitted&&wsf.webforms_sheet.DateTimeSubmitted<=EndDate)
					   select wsf;
			return wsfObj.ToList();

		}

		[WebMethod]
		public void DeleteSheetData(long DentalOfficeID,string RegistrationKey,List<long> SheetsForDeletion) {
			ODWebServiceEntities db=new ODWebServiceEntities();
			for(int i=0;i<SheetsForDeletion.Count();i++) {
				long SheetID=SheetsForDeletion.ElementAt(i);// LINQ throws an error if this is directly put into the selectexpression
				// first delete all sheet field then delete the sheet so that a foreign key error is not thrown
				var delSheetField=from wsf in db.webforms_sheetfield where
								  wsf.webforms_sheet.SheetID==SheetID
								  select wsf;

				for(int j=0;j<delSheetField.Count();j++) {
					// the ElementAt operator only works with lists. Hence ToList()
					db.DeleteObject(delSheetField.ToList().ElementAt(j));
				}
				var delSheet=from ws in db.webforms_sheet where ws.SheetID==SheetID
							 select ws;
				db.DeleteObject(delSheet.First());
			}
			db.SaveChanges();

		}

		[WebMethod]
		public bool CheckRegistrationKey(string RegistrationKey) {
			return true;
		}


	}


}
