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
	[WebService(Namespace="http://opendental.com/")]
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
		public List<webforms_sheetfield> GetSheetFieldData(string RegistrationKey) {
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IPAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
			}
			ODWebServiceEntities db=new ODWebServiceEntities();
			var wsfObj=from wsf in db.webforms_sheetfield
				where wsf.webforms_sheet.webforms_preference.DentalOfficeID==DentalOfficeID
				select wsf;
			return wsfObj.ToList();
		}

		[WebMethod]
		public List<webforms_sheet> GetSheetData(string RegistrationKey) {
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IPAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
			}
			ODWebServiceEntities db=new ODWebServiceEntities();
			var wsObj=from wsf in db.webforms_sheet
				where wsf.webforms_preference.DentalOfficeID==DentalOfficeID
				select wsf;
			return wsObj.ToList();
		}

		[WebMethod]
		public void DeleteSheetData(string RegistrationKey,List<long> SheetsForDeletion) {
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IPAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
				return;
			}
			ODWebServiceEntities db=new ODWebServiceEntities();
			for(int i=0;i<SheetsForDeletion.Count();i++) {
				long SheetID=SheetsForDeletion.ElementAt(i);// LINQ throws an error if this is directly put into the select expression
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
			ODWebServiceEntities db=new ODWebServiceEntities();
			/*
			String RegistrationKey="sdgsd";
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
			}
			*/
			long DentalOfficeID=4;
			string a=sheetDef.ToString();
			/*
			var wsdResults=from wsd in db.webforms_sheetdef
					   where wsd.webforms_preference.DentalOfficeID==DentalOfficeID && wsd.SheetDefNum==sheetDef.SheetDefNum
					   select wsd;
			*/

			var wsdResults= db.webforms_sheetdef.Where(wsd => wsd.webforms_preference.DentalOfficeID==DentalOfficeID && wsd.SheetDefNum==sheetDef.SheetDefNum);

			webforms_sheetdef wsdObj=null;

			if(wsdResults.Count()>0) {
				wsdObj =wsdResults.First();

			}
			// if there is no entry for that dental office make a new entry.
			if(wsdResults.Count()==0) {
				wsdObj=new webforms_sheetdef();
				wsdObj.SheetDefNum=sheetDef.SheetDefNum;

				// create associated SheetFieldDefs and add them to the SheetDef object.
				for(int i=0;i<sheetDef.SheetFieldDefs.Count();i++) {
					webforms_sheetfielddef sdfObj = new webforms_sheetfielddef();
					sdfObj.SheetFieldDefNum=sheetDef.SheetFieldDefs[i].SheetFieldDefNum;
					wsdObj.webforms_sheetfielddef.Add(sdfObj);
				}
				
				db.AddTowebforms_sheetdef(wsdObj);
			}
			
			wsdObj.Description=sheetDef.Description;
			wsdObj.FontName = sheetDef.FontName;
			// list all other properties.
			

			
			for(int i=0;i<sheetDef.SheetFieldDefs.Count();i++) {
				var sdfObj=wsdObj.webforms_sheetfielddef.Where(sfd => sfd.SheetFieldDefNum==sheetDef.SheetFieldDefs[i].SheetFieldDefNum).First();
				sdfObj.FieldName = sheetDef.SheetFieldDefs[i].FieldName;
				sdfObj.XPos = sheetDef.SheetFieldDefs[i].XPos;
				sdfObj.YPos = sheetDef.SheetFieldDefs[i].YPos;
				int ab=3;

			}

			int b=3;

			//db.SaveChanges();
		}




	}
}
