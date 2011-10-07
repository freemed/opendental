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
    public class MedicalController : Controller
    {
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
			return File(Convert.FromBase64String(doc.RawBase64),"application/pdf","Statement.pdf");
		}

		public ActionResult EHRInformation() {
			Patientm patm;
			if(Session["Patient"]==null) {
				return RedirectToAction("Login","Account");
			}
			else {
				patm=(Patientm)Session["Patient"];
			}
			MedicalModel pm= new MedicalModel(patm);
			return View(pm);
		}


    }
}
