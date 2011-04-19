///Dennis Mathew: For using ADO.NET Entity Data Model/LINQ with Mysql/Visual Studio 2010, download and install Connector/Net from http://dev.mysql.com/downloads/connector/net/ 
/// Connector/Net is a ADO.NET driver for MySQL.
/// The web server which hosts the webservice will also need this install.
/// The integration with Visual Studio can be flaky. So a few cycles of install/uninstall/restart may be needed. I've also tried the non-install options of adding dlls but they don't seem to work in the few attempts that I made.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Reflection;
using System.Threading;
using OpenDentBusiness;
using WebForms;

namespace WebHostSynch {
	/// <summary>
	/// Summary description for WebHostSynch
	/// </summary>
	[WebService(Namespace="http://opendental.com/")]
	[WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class Sheets:System.Web.Services.WebService {
		private Util util=new Util();
		/// <summary>
		/// Dennis: This method is backward compatibilty only. Older version of OD may be using it. It may be deleted later. 14 April, 2011
		/// </summary>
		[WebMethod]
		public bool SetPreferences(string RegistrationKey,int ColorBorder) {
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			try {
				ODWebServiceEntities db=new ODWebServiceEntities();
				if(DentalOfficeID==0) {
				}
				var wspObj=from wsp in db.webforms_preference
					where wsp.DentalOfficeID==DentalOfficeID
					select wsp;
				//update preference
				if(wspObj.Count()>0) {
					wspObj.First().ColorBorder=ColorBorder;
				}
				// if there is no entry for that dental office make a new entry.
				if(wspObj.Count()==0) {
					webforms_preference wspNewObj=new webforms_preference();
					wspNewObj.DentalOfficeID=DentalOfficeID;
					wspNewObj.ColorBorder=ColorBorder;
					db.AddTowebforms_preference(wspNewObj);
				}
				db.SaveChanges();
				Logger.Information("Preferences saved IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
				return false;
			}
			return true;
		}

		/// <summary>
		/// Cannot easily overload a method in webservices hence the suffix V2
		/// </summary>
		[WebMethod]
		public bool SetPreferencesV2(string RegistrationKey,webforms_preference prefObj) {
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			int ColorBorder;
			try {
				ODWebServiceEntities db=new ODWebServiceEntities();
				if(DentalOfficeID==0) {
				}
				var wspObj=db.webforms_preference.Where(wsp => wsp.DentalOfficeID==DentalOfficeID);
				//update preference
				if(wspObj.Count()>0) {
					wspObj.First().ColorBorder=prefObj.ColorBorder;
					wspObj.First().CultureName=prefObj.CultureName;
				}
				// if there is no entry for that dental office make a new entry.
				if(wspObj.Count()==0) {
					prefObj.DentalOfficeID=DentalOfficeID;
					db.AddTowebforms_preference(prefObj);
				}
				db.SaveChanges();
				Logger.Information("Preferences saved IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
				return false;
			}
			return true;
		}

		[WebMethod]
		public webforms_preference GetPreferences(string RegistrationKey) {
				
			Logger.Information("In GetPreferences IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
            ODWebServiceEntities db=new ODWebServiceEntities();
			webforms_preference wspObj=null;
			int DefaultColorBorder=-12550016;
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			try {
				if(DentalOfficeID==0) {
					return wspObj;
				}
                var wspRes=db.webforms_preference.Where(wsp=>wsp.DentalOfficeID==DentalOfficeID);
                if (wspRes.Count()>0){
					wspObj=wspRes.First();
				}
				// if there is no entry for that dental office make a new entry.
                if (wspRes.Count()==0){
					wspObj=new webforms_preference();
					wspObj.DentalOfficeID=DentalOfficeID;
					wspObj.ColorBorder=DefaultColorBorder;
					db.AddTowebforms_preference(wspObj);
					Logger.Information("new entry IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				}
				db.SaveChanges();
				Logger.Information("In GetPreferences IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
				return wspObj;;
			}
			return wspObj;;
		}

		[WebMethod]
		public List<SheetAndSheetField> GetSheets(string RegistrationKey) {
			List<SheetAndSheetField> sAndsfList=new List<SheetAndSheetField>();
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			try {
				if(DentalOfficeID==0) {
				}
				ODWebServiceEntities db=new ODWebServiceEntities();
				var wsRes=from wsf in db.webforms_sheet
						  where wsf.webforms_preference.DentalOfficeID==DentalOfficeID
						  select wsf;
				for(int i=0;i<wsRes.Count();i++) {
					var wsobj=wsRes.ToList()[i];
					wsobj.webforms_sheetfield.Load();
					var sheetfieldList=wsobj.webforms_sheetfield;
					SheetAndSheetField sAnds=new SheetAndSheetField(wsobj,sheetfieldList.ToList());
					sAndsfList.Add(sAnds);
				}
				Logger.Information("In GetSheetData IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID+" Sheets sent to Client="+wsRes.Count());
				return sAndsfList;
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
				return sAndsfList;
			}
		}

		/// <summary>
		/// A class made  for just transferring both the sheets and it's fields in a web service.
		/// </summary>
		public class SheetAndSheetField {
			public webforms_sheet web_sheet=null;
			public List<webforms_sheetfield> web_sheetfieldlist=null;
			public SheetAndSheetField() {
			}
			public SheetAndSheetField(webforms_sheet web_sheet,List<webforms_sheetfield> web_sheetfieldlist) {
				this.web_sheet=web_sheet;
				this.web_sheetfieldlist=web_sheetfieldlist;
			}
		}

		[WebMethod]
		public void DeleteSheetData(string RegistrationKey,List<long> SheetsForDeletion) {
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			try {
				if(DentalOfficeID==0) {
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
					Logger.Information("deleted SheetID="+SheetID+" DentalOfficeID="+DentalOfficeID);
				}
				db.SaveChanges();
				Logger.Information("In DeleteSheetData IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
			}
		}

		/// <summary>
		/// This method is redundant. It may be deleted later. Older version of OD may be using it.
		/// </summary>
		[WebMethod]
		public bool CheckRegistrationKey(string RegistrationKeyFromDentalOffice) {
			return util.CheckRegistrationKey(RegistrationKeyFromDentalOffice);
		}

		[WebMethod]
		public long GetDentalOfficeID(string RegistrationKeyFromDentalOffice) {
			return util.GetDentalOfficeID(RegistrationKeyFromDentalOffice);
		}

		/// <summary>
		/// An empty method to test if the webservice is up and running. this was made with the intention of testing the correctness of the webservice URL on an Open Dental Installation. If an incorrect webservice URL is used in a background thread of OD the exception cannot be handled easily.
		/// </summary>
		[WebMethod]
		public bool ServiceExists() {
			return true;
		}

		[WebMethod]
		public string GetSheetDefAddress(string RegistrationKey) {
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			if(DentalOfficeID==0) {
				return "";
			}
			string SheetDefAddress="";
			try {
				SheetDefAddress=ConfigurationManager.AppSettings["SheetDefAddress"];
			}
			catch(Exception ex) {
				Logger.LogError(ex);
			}
            Logger.Information("In GetSheetDefAddress SheetDefAddress="+SheetDefAddress);
			return SheetDefAddress;
		}

		[WebMethod]
		public List<webforms_sheetdef> DownloadSheetDefs(string RegistrationKey) {
			List<webforms_sheetdef> sheetDefList=null;
			try {
				long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
				if(DentalOfficeID==0) {
					return sheetDefList;
				}
			ODWebServiceEntities db=new ODWebServiceEntities();
			var SheetDefResult=db.webforms_sheetdef.Where(sheetdef=>sheetdef.webforms_preference.DentalOfficeID==DentalOfficeID);
			sheetDefList=SheetDefResult.ToList();
			}
			catch(Exception ex) {
				Logger.Information(ex.Message.ToString());
				return sheetDefList;
			}
			return sheetDefList;
		}

		[WebMethod]
		public void DeleteSheetDef(string RegistrationKey,long WebSheetDefID) {
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			try {
				if(DentalOfficeID==0) {
					return;
				}
				ODWebServiceEntities db=new ODWebServiceEntities();
					webforms_sheetdef SheetDefObj=null;
					var SheetDefResult=db.webforms_sheetdef.Where(sd=>sd.WebSheetDefID==WebSheetDefID);
					if(SheetDefResult.Count()>0) {
						SheetDefObj=SheetDefResult.First();
						//load and delete existing child objects i.e sheetfielddefs objects
						SheetDefObj.webforms_sheetfielddef.Load();
						var SheetFieldDefResult=SheetDefObj.webforms_sheetfielddef;
						while(SheetFieldDefResult.Count()>0) {
							db.DeleteObject(SheetFieldDefResult.First());//Delete SheetFieldDefObj
						}
						db.DeleteObject(SheetDefResult.First());//Delete SheetDefObj
					Logger.Information("deleted WebSheetDefID="+WebSheetDefID+" DentalOfficeID="+DentalOfficeID);
				}
				db.SaveChanges();
				Logger.Information("In DeleteSheetDef IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
			}
		}

		/// <summary>
		/// Here a single SheetDef can be uploaded via webhostsync from an Open Dental installation.
		/// </summary>
		[WebMethod]
		public void UpLoadSheetDef(string RegistrationKey,SheetDef sheetDef) {
			ODWebServiceEntities db=new ODWebServiceEntities();
			long DentalOfficeID=util.GetDentalOfficeID(RegistrationKey);
			try{
				if(DentalOfficeID==0) {
					return;
				}
				var PreferenceResult=db.webforms_preference.Where(pref=>pref.DentalOfficeID==DentalOfficeID);
				webforms_sheetdef SheetDefObj=null;
				SheetDefObj=new webforms_sheetdef();
				PreferenceResult.First().webforms_sheetdef.Add(SheetDefObj);
				FillSheetDef(sheetDef,SheetDefObj);
				FillFieldSheetDef(sheetDef,SheetDefObj);
				db.SaveChanges();
			}
			catch(Exception ex) {
				Logger.LogError("IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID,ex);
				return ;
			}
		}

		private void FillSheetDef(SheetDef sheetDef,webforms_sheetdef SheetDefObj) {
			SheetDefObj.Description=sheetDef.Description;
			SheetDefObj.FontName=sheetDef.FontName;
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
				webforms_sheetfielddef SheetFieldDefObj=new webforms_sheetfielddef();
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
								if(fieldinfo.GetValue(sheetDef.SheetFieldDefs[i])==null) {
									propertyinfo.SetValue(SheetFieldDefObj,"",null);
								}
								else {
									propertyinfo.SetValue(SheetFieldDefObj,fieldinfo.GetValue(sheetDef.SheetFieldDefs[i]),null);
								}
							}
						}
					}//foreach propertyinfo
				}//foreach fieldinfo
			}
		}
		
	}
}
