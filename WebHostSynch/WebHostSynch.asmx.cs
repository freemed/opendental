using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using OpenDentBusiness;



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
		public bool SetPreferences(string RegistrationKey,int ColorBorder,string Heading1,string Heading2) {
			ODWebServiceEntities db=new ODWebServiceEntities();
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
				return false;
			}
			var wspObj=from wsp in db.webforms_preference
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
				webforms_preference wspNewObj=new webforms_preference();
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
		public List<webforms_sheetfield> GetSheetData(string RegistrationKey,DateTime StartDate,DateTime EndDate) {
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IPAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
			}
			ODWebServiceEntities db=new ODWebServiceEntities();
			EndDate=EndDate.AddDays(1);//if this is put in LINQ it will not work. so change date first
			var wsfObj=from wsf in db.webforms_sheetfield
					   where wsf.webforms_sheet.webforms_preference.DentalOfficeID==DentalOfficeID
					   &&(StartDate<=wsf.webforms_sheet.DateTimeSubmitted&&wsf.webforms_sheet.DateTimeSubmitted<=EndDate)
					   select wsf;
			return wsfObj.ToList();
		}

		[WebMethod]
		public void DeleteSheetData(string RegistrationKey,List<long> SheetsForDeletion) {
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IPAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
			}
			ODWebServiceEntities db=new ODWebServiceEntities();
			for(int i=0;i<SheetsForDeletion.Count();i++) {
				long SheetID=SheetsForDeletion.ElementAt(i);// LINQ throws an error if this is directly put into the selectexpression
				// first delete all sheet field then delete the sheet so that a foreign key error is not thrown
				var delSheetField=from wsf in db.webforms_sheetfield where wsf.webforms_sheet.SheetID==SheetID
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
		public bool CheckRegistrationKey(string RegistrationKeyFromDentalOffice) {
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			// sets a static variable
			dc.SetDb(connectStr,"",DatabaseType.MySql,true);
			RegistrationKey RegistrationKeyFromDb=null;
			try {
				RegistrationKeyFromDb=RegistrationKeys.GetByKey(RegistrationKeyFromDentalOffice);
			}
			catch(ApplicationException ex) {
				Logger.Information(ex.Message.ToString());
				return false;
			}
			return true;
		}

		[WebMethod]
		public long GetDentalOfficeID(string RegistrationKeyFromDentalOffice) {
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
			// sets a static variable
			dc.SetDb(connectStr,"",DatabaseType.MySql,true);
			RegistrationKey RegistrationKeyFromDb=null;
			try {
				RegistrationKeyFromDb=RegistrationKeys.GetByKey(RegistrationKeyFromDentalOffice);
			}
			catch(ApplicationException ex) {
				Logger.Information(ex.Message.ToString());
				return 0;
			}
			return RegistrationKeyFromDb.PatNum;
		}

		[WebMethod]
		public string GetWebFormAddress(string RegistrationKeyFromDentalOffice) {
			string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
			string WebFormAddress="";
			try {
				WebFormAddress="https://opendentalsoft.com/WebForms/WebForm1.aspx?DentalOfficeID="+GetDentalOfficeID(RegistrationKeyFromDentalOffice);
			}
			catch(ApplicationException ex) {
				Logger.Information(ex.Message.ToString());
				
			}
			return WebFormAddress;
		}



		/// <summary>
		/// Ignore this method - this is for the 'next' version of the Webforms.
		/// Here sheetDef can be uploaded to the webhostsync from Open Dental
		/// </summary>
		[WebMethod]
		public void ReadSheetDef(SheetDef sheetDef) {
			//string a=sheetDef.ToString();
		}



	}
}
