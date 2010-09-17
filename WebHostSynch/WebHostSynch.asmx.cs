using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using OpenDentBusiness;
using System.Reflection;

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
		/// This method deletes ( then inserts) any existing SheetDefs and corresponding sheetfielddef by the same SheetDefNum. Deleting(and then inserting) versus Updating is done because on occasions there can be multiple SheetDefs which have the same SheetDefNum (due to imperfect code). In case of sheetfielddef updating is not even an option because the SheetFieldDefNum can change.
		/// </summary>
		[WebMethod]
		public void UpdateSheetDef(string RegistrationKey,List<SheetDef> sheetDefList) {
			ODWebServiceEntities db=new ODWebServiceEntities();
			long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				Logger.Information("Incorrect registration key. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
				return;
			}
			

			foreach(SheetDef sheetDef in sheetDefList) {
				var PreferenceResult = db.webforms_preference.Where(pref => pref.DentalOfficeID==DentalOfficeID);
				webforms_sheetdef SheetDefObj=null;
				PreferenceResult.First().webforms_sheetdef.Load();//Load associated SheetDefs object.
				var SheetDefResult=PreferenceResult.First().webforms_sheetdef.Where(sd => sd.SheetDefNum==sheetDef.SheetDefNum);
				 while(SheetDefResult.Count()>0) {
					 SheetDefObj=SheetDefResult.First();
					//load and delete existing child objects i.e sheetfielddefs objects
					 SheetDefObj.webforms_sheetfielddef.Load();
					var SheetFieldDefResult=SheetDefObj.webforms_sheetfielddef;
					//every time a SheetFieldDefResult is deleted the the SheetFieldDefResult.Count() reduces so at some point the count will ultimately become 0.
					while(SheetFieldDefResult.Count()>0) {
						db.DeleteObject(SheetFieldDefResult.First());//Delete SheetFieldDefObj
					}
					db.DeleteObject(SheetDefResult.First());//Delete SheetDefObj
				}
				 SheetDefObj=new webforms_sheetdef();
				 SheetDefObj.SheetDefNum=sheetDef.SheetDefNum;
				 PreferenceResult.First().webforms_sheetdef.Add(SheetDefObj);


				 FillSheetDef(sheetDef,SheetDefObj);
				 FillFieldSheetDef(sheetDef,SheetDefObj);
			}// end of foreach loop
			db.SaveChanges();
		
		}

		private void FillSheetDef(SheetDef sheetDef,webforms_sheetdef SheetDefObj) {
			SheetDefObj.Description=sheetDef.Description;
			SheetDefObj.FontName = sheetDef.FontName;
			SheetDefObj.SheetType=(int)sheetDef.SheetType;
			SheetDefObj.FontSize=sheetDef.FontSize;
			SheetDefObj.Width=sheetDef.Width;
			SheetDefObj.Height=sheetDef.Height;
			if(sheetDef.IsLandscape==true) {
				SheetDefObj.IsLandscape=(sbyte)1;
			}
			else {
				SheetDefObj.IsLandscape=(sbyte)0;
			}
		}

		private void FillFieldSheetDef(SheetDef sheetDef,webforms_sheetdef SheetDefObj) {
			for(int i=0;i<sheetDef.SheetFieldDefs.Count();i++) {//assign several webforms_sheetfielddef
				webforms_sheetfielddef SheetFieldDefObj = new webforms_sheetfielddef();
				SheetFieldDefObj.SheetFieldDefNum=sheetDef.SheetFieldDefs[i].SheetFieldDefNum;
				SheetDefObj.webforms_sheetfielddef.Add(SheetFieldDefObj);
				// assign each property of a single webforms_sheetfielddef with corresponding values.
				foreach(FieldInfo fieldinfo in sheetDef.SheetFieldDefs[i].GetType().GetFields()) {
					foreach(PropertyInfo propertyinfo in SheetFieldDefObj.GetType().GetProperties()) {
						if(fieldinfo.Name==propertyinfo.Name) {
							if(propertyinfo.PropertyType==typeof(SByte)) {
								if((bool)fieldinfo.GetValue(sheetDef.SheetFieldDefs[i])==true) {
									propertyinfo.SetValue(SheetFieldDefObj,(sbyte)1,null);
								}
								else {
									propertyinfo.SetValue(SheetFieldDefObj,(sbyte)0,null);
								}
							}
							else {
								propertyinfo.SetValue(SheetFieldDefObj,fieldinfo.GetValue(sheetDef.SheetFieldDefs[i]),null);
							}
						}
					}//foreach propertyinfo
				}//foreach fieldinfo
			}
		}




	}
}
