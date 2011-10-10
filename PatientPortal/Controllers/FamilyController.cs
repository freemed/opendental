using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForms;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using PatientPortalMVC.Models;

namespace PatientPortalMVC.Controllers
{
    public class FamilyController : Controller
    {

		public ActionResult FamilyInformation() {
			Patientm patm;
			if(Session["Patient"]==null) {
				return RedirectToAction("Login","Account");
			}
			else {
				patm=(Patientm)Session["Patient"];
			}
			FamilyModel fm= new FamilyModel(patm);
			return View(fm);
		}

    }
}
