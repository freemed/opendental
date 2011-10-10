using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;
using WebForms;
using WebHostSynch;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using PatientPortalMVC.Models;

namespace PatientPortalMVC.Controllers
{
    public class AccountController : Controller
    {
		private long DentalOfficeID=0;
		
		[HttpPost]
		public ActionResult Login(LoginModel model,string returnUrl)
        {
			if(ModelState.IsValid) {
				HttpCookie DentalOfficeIDCookie=Request.Cookies["DentalOfficeIDCookie"];
				if(DentalOfficeIDCookie!=null) {
				Int64.TryParse(DentalOfficeIDCookie.Value,out DentalOfficeID);
				}
				Patientm pat=Patientms.GetOne(DentalOfficeID,model.UserName,model.Password);
				if(pat==null) {
					ModelState.AddModelError("","Login Failed, Please Try Again");
				}
				else {
					Session["Patient"]=pat;
					return RedirectToAction("EHRInformation","Medical");
				}
			}
			return View();
        }
		

		//get
		public ActionResult Login(long? DentalOfficeID) {
			// or use RouteData.Values["DentalOfficeID"]; to extract DentalOfficeID
			if(DentalOfficeID==null) {
				this.DentalOfficeID=0;
			}
			else {
				this.DentalOfficeID=(long)DentalOfficeID;
				SetCookie((long)DentalOfficeID);
			}
			return View();
		}

		public ActionResult LogOff() {
			long DentalOfficeID=((Patientm)Session["Patient"]).CustomerNum;
			Session["Patient"]=null;
			Session.Abandon();
			return RedirectToAction("Login",new { controller="Account",action="Login",DentalOfficeID=DentalOfficeID });
		}

		/// <summary>
		/// this cookie is used to retrieve the DentalOfficeID incase the session times out. The login depends on 3 parmeters 1)the username 2)the password and 3) the DentalOfficeID.
		/// </summary>
		private void SetCookie(long DentalOfficeID) {
			HttpCookie DentalOfficeIDCookie=new HttpCookie("DentalOfficeIDCookie");
			DentalOfficeIDCookie.Value=DentalOfficeID.ToString();
			DentalOfficeIDCookie.Expires=DateTime.Now.AddYears(1);
			Response.Cookies.Add(DentalOfficeIDCookie);
		}

		public ActionResult AccountView() {
			Patientm patm;
			if(Session["Patient"]==null) {
				return RedirectToAction("Login","Account");
			}
			else {
				patm=(Patientm)Session["Patient"];
			}
			AccountModel am= new AccountModel(patm);
			return View(am);
		}

		public ActionResult ShowPdfFile(long? d) {
			Documentm doc=null;
			long DocNum=0;
			if(d!=null) {
				DocNum=(long)d;
			}
			Patientm patm;
			if(Session["Patient"]==null) {
				return RedirectToAction("Login");
			}
			else {
				patm=(Patientm)Session["Patient"];
			}
			if(DocNum!=0) {
				doc=Documentms.GetOne(patm.CustomerNum,DocNum);
			}
			if(doc==null || patm.PatNum!=doc.PatNum) {//make sure that the patient does not pass the another DocNum of another patient.
				return new EmptyResult(); //return a blank page todo: return page with proper message.
			}
			ContentDisposition cd = new ContentDisposition();
			cd.Inline=true;//the browser will try and show the pdf inline i.e inside the browser window. If set to false it will force a download.
			Response.AppendHeader("Content-Disposition",cd.ToString());
			return File(Convert.FromBase64String(doc.RawBase64),"application/pdf","statement.pdf");
		}




    }
}
