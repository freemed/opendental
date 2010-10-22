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

		#region FormWebForm

			[WebMethod]
			public bool SetPreferences(string RegistrationKey,int ColorBorder,string Heading1,string Heading2) {
				try {
					ODWebServiceEntities db=new ODWebServiceEntities();
					long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
					if(DentalOfficeID==0) {
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
					Logger.Information("Preferences saved IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
					return false;
				}
				return true;
			}

			[WebMethod]
			public webforms_preference GetPreferences(string RegistrationKey) {
				Logger.Information("In GetPreferences IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKey);
                ODWebServiceEntities db = new ODWebServiceEntities();
				webforms_preference wspObj= null;
				int DefaultColorBorder=-12550016;
				string DefaultHeading1="PATIENT INFORMATION";
				string DefaultHeading2="We are pleased to welcome you to our office. Please take a few minutes to fill out this form as completely as you can. If you have any questions we'll be glad to help you.";

				try {
					long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
					if(DentalOfficeID==0) {
						return wspObj;
					}
                    /*
					var wspRes=from wsp in db.webforms_preference
							where wsp.DentalOfficeID==DentalOfficeID
							select wsp;
                    */
                    var wspRes = db.webforms_preference.Where(wsp => wsp.DentalOfficeID == DentalOfficeID);
                                 

                    if (wspRes.Count() > 0)
                    {
						wspObj=wspRes.First();
					}
					// if there is no entry for that dental office make a new entry.
                    if (wspRes.Count() == 0)
                    {
						wspObj=new webforms_preference();
						wspObj.DentalOfficeID=DentalOfficeID;
						wspObj.ColorBorder=DefaultColorBorder;
						wspObj.Heading1=DefaultHeading1;
						wspObj.Heading2=DefaultHeading2;
						db.AddTowebforms_preference(wspObj);
						Logger.Information("new entry IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
					}
					db.SaveChanges();
					Logger.Information("In GetPreferences IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				}
				catch(Exception ex) {
					Logger.LogError(ex);
					return wspObj;;
				}
				return wspObj;;
			}

			[WebMethod]
			public List<webforms_sheetfield> GetSheetFieldData(string RegistrationKey) {
				long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
				if(DentalOfficeID==0) {
				}
				ODWebServiceEntities db=new ODWebServiceEntities();
				var wsfObj=from wsf in db.webforms_sheetfield
					where wsf.webforms_sheet.webforms_preference.DentalOfficeID==DentalOfficeID
					select wsf;
				Logger.Information("In GetSheetFieldData IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				return wsfObj.ToList();
			}

			[WebMethod]
			public List<webforms_sheet> GetSheetData(string RegistrationKey) {
				List<webforms_sheet> wslist=null;
				try {
					long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
					if(DentalOfficeID==0) {
					}
					ODWebServiceEntities db=new ODWebServiceEntities();
					var wsRes=from wsf in db.webforms_sheet
						where wsf.webforms_preference.DentalOfficeID==DentalOfficeID
						select wsf;
					wslist=wsRes.ToList();
					Logger.Information("In GetSheetData IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID+" Sheets sent to Client ="+ wsRes.Count());
					return wslist;
					}
					catch(Exception ex) {
						Logger.Information(ex.Message.ToString());
						return wslist;
					}
			}

			[WebMethod]
			public List<SheetAndSheetField> GetSheets(string RegistrationKey) {
				List<SheetAndSheetField> sAndsfList= new List<SheetAndSheetField>();
			
				try {
					long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
					if(DentalOfficeID==0) {
					}
					ODWebServiceEntities db=new ODWebServiceEntities();
					var wsRes=from wsf in db.webforms_sheet
							  where wsf.webforms_preference.DentalOfficeID==DentalOfficeID
							  select wsf;
					//SheetDefObj.webforms_sheetfielddef.Load();

					for(int i=0;i<wsRes.Count();i++) {
						var wsobj=wsRes.ToList()[i];
						
						wsobj.webforms_sheetfield.Load();
						var sheetfieldList= wsobj.webforms_sheetfield;
						SheetAndSheetField sAnds=new SheetAndSheetField(wsobj,sheetfieldList.ToList());
						sAndsfList.Add(sAnds);

					}
					Logger.Information("In GetSheetData IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID+" Sheets sent to Client ="+ wsRes.Count());
					return sAndsfList;
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
					return sAndsfList;
				}
			}

			/// <summary>
			/// A class made  for just transferring both the sheets and it's fields in a web service.
			/// </summary>
			public class SheetAndSheetField {
				public webforms_sheet sh=null;
				public List<webforms_sheetfield> sf=null;

				public SheetAndSheetField() {
				
				}
				public SheetAndSheetField(webforms_sheet sh,List<webforms_sheetfield> sf) {
					this.sh=sh;
					this.sf=sf;
				}

			}

			[WebMethod]
			public void DeleteSheetData(string RegistrationKey,List<long> SheetsForDeletion) {
				try {
					long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
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
					Logger.Information(ex.Message.ToString());
				}
			}

			[WebMethod]
			public bool CheckRegistrationKey(string RegistrationKeyFromDentalOffice) {
				Logger.Information("In CheckRegistrationKey() RegistrationKeyFromDentalOffice="+RegistrationKeyFromDentalOffice);
				string connectStr=ConfigurationManager.ConnectionStrings["DBRegKey"].ConnectionString;
				OpenDentBusiness.DataConnection dc=new OpenDentBusiness.DataConnection();
				// sets a static variable
				dc.SetDb(connectStr,"",DatabaseType.MySql,true);
				RegistrationKey RegistrationKeyFromDb=null;
				try {
					RegistrationKeyFromDb=RegistrationKeys.GetByKey(RegistrationKeyFromDentalOffice);
					DateTime d1= new DateTime(1902,1,1);
					if(d1<RegistrationKeyFromDb.DateDisabled && RegistrationKeyFromDb.DateDisabled<DateTime.Today) {
						Logger.Information("RegistrationKey has been disabled. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
						return false;
					}
					if(d1<RegistrationKeyFromDb.DateEnded && RegistrationKeyFromDb.DateEnded<DateTime.Today) {
						Logger.Information("RegistrationKey DateEnded date is past. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
						return false;
					}
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
					Logger.Information("Incorrect registration key. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKeyFromDentalOffice);
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
					DateTime d1= new DateTime(1902,1,1);
					if(d1<RegistrationKeyFromDb.DateDisabled && RegistrationKeyFromDb.DateDisabled<DateTime.Today) {
						Logger.Information("RegistrationKey has been disabled. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
						return 0;
					}
					if(d1<RegistrationKeyFromDb.DateEnded && RegistrationKeyFromDb.DateEnded<DateTime.Today) {
						Logger.Information("RegistrationKey DateEnded date is past. Dental OfficeId="+RegistrationKeyFromDb.PatNum);
						return 0;
					}
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
					Logger.Information("Incorrect registration key. IpAddress="+HttpContext.Current.Request.UserHostAddress+" RegistrationKey="+RegistrationKeyFromDentalOffice);
					return 0;
				}
				return RegistrationKeyFromDb.PatNum;
			}

			[WebMethod]
			public string GetWebFormAddress(string RegistrationKeyFromDentalOffice) {
				string WebFormAddress="";
				try {
					WebFormAddress=ConfigurationManager.AppSettings["WebFormAddress"].ToString()+"?DentalOfficeID="+GetDentalOfficeID(RegistrationKeyFromDentalOffice);
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
				}
				Logger.Information("In GetWebFormAddress WebFormAddress="+WebFormAddress);
				return WebFormAddress;
			}

		#endregion

		
		#region FormWebForm Sheets

		/// <summary>
			/// An empty method to test if the webservice is up and running. this was made with the intention of testing the correctness of the webservice URL on an Open Dental Installation. If an incorrect webservice URL is used in a background thread of OD the exception cannot be handled easily.
		/// </summary>
		/// <returns></returns>
	
		[WebMethod]
			public bool ServiceExists() {
				return true;
			}

			[WebMethod]
			public string GetSheetDefAddress(string RegistrationKey) {
				long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
				if(DentalOfficeID==0) {
					return "";
				}
				string SheetDefAddress="";
				try {
					SheetDefAddress=ConfigurationManager.AppSettings["SheetDefAddress"];
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
				}
                Logger.Information("In GetSheetDefAddress SheetDefAddress=" + SheetDefAddress);
				return SheetDefAddress;
			}

			

			[WebMethod]
			public List<webforms_sheetdef> DownloadSheetDefs(string RegistrationKey) {
				List<webforms_sheetdef> sheetDefList=null;
				try {
					long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
					if(DentalOfficeID==0) {
						return sheetDefList;
					}
				ODWebServiceEntities db=new ODWebServiceEntities();
				var SheetDefResult=db.webforms_sheetdef.Where(sheetdef => sheetdef.webforms_preference.DentalOfficeID==DentalOfficeID);
				sheetDefList=SheetDefResult.ToList();
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
					return sheetDefList;
				}
				return sheetDefList;
			}

			[WebMethod]
			public void DeleteSheetDefs(string RegistrationKey,List<long> SheetDefsForDeletion) {

				try {
					long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
					if(DentalOfficeID==0) {
						return;
					}
					ODWebServiceEntities db=new ODWebServiceEntities();
					for(int i=0;i<SheetDefsForDeletion.Count();i++) {
						long WebSheetDefNum=SheetDefsForDeletion.ElementAt(i);// LINQ throws an error if this is directly put into the select expression
						webforms_sheetdef SheetDefObj=null;
						var SheetDefResult=db.webforms_sheetdef.Where(sd => sd.WebSheetDefNum==WebSheetDefNum);
						if(SheetDefResult.Count()>0) {
							SheetDefObj=SheetDefResult.First();
							//load and delete existing child objects i.e sheetfielddefs objects
							SheetDefObj.webforms_sheetfielddef.Load();
							var SheetFieldDefResult=SheetDefObj.webforms_sheetfielddef;
							while(SheetFieldDefResult.Count()>0) {
								db.DeleteObject(SheetFieldDefResult.First());//Delete SheetFieldDefObj
							}
							db.DeleteObject(SheetDefResult.First());//Delete SheetDefObj
						}

						Logger.Information("deleted WebSheetDefNum="+WebSheetDefNum+" DentalOfficeID="+DentalOfficeID);
					}
					db.SaveChanges();
					Logger.Information("In DeleteSheetData IpAddress="+HttpContext.Current.Request.UserHostAddress+" DentalOfficeID="+DentalOfficeID);
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
				}
			}


			/// <summary>
			/// Here sheetDef can be uploaded to the webhostsync from Open Dental
			/// This method deletes ( then inserts) any existing SheetDefs and corresponding sheetfielddef by the same SheetDefNum. Deleting(and then inserting) versus Updating is done because on occasions there can be multiple SheetDefs which have the same SheetDefNum (due to imperfect code). In case of sheetfielddef updating is not even an option because the SheetFieldDefNum can change.
			/// </summary>
			[WebMethod]
			public void UpLoadSheetDef(string RegistrationKey,List<SheetDef> sheetDefList) {
				ODWebServiceEntities db=new ODWebServiceEntities();
				long DentalOfficeID=GetDentalOfficeID(RegistrationKey);
				try{
					if(DentalOfficeID==0) {
						return;
					}
					

					foreach(SheetDef sheetDef in sheetDefList) {
						var PreferenceResult=db.webforms_preference.Where(pref => pref.DentalOfficeID==DentalOfficeID);
						webforms_sheetdef SheetDefObj=null;
						/* code below is not needed because Sheet defs are never updated.
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
						*/
						 SheetDefObj=new webforms_sheetdef();
						 SheetDefObj.SheetDefNum=sheetDef.SheetDefNum; // this line may be removed later after deleting SheetDefNum column form the database
						 PreferenceResult.First().webforms_sheetdef.Add(SheetDefObj);
						 FillSheetDef(sheetDef,SheetDefObj);
						 FillFieldSheetDef(sheetDef,SheetDefObj);
					}// end of foreach loop
					db.SaveChanges();
				}
				catch(Exception ex) {
					Logger.Information(ex.Message.ToString());
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

		#endregion


	}
}
