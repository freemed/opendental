using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using WebForms;


namespace PatientPortalMVC.Models
{
    public class FamilyModel {
		public List<Patientm> patList;

		public FamilyModel(Patientm patm) {
			patList=Patientms.GetPatientmsOfFamily(patm.CustomerNum,patm.PatNum);
			foreach(Patientm pm in patList) {
				pm.Age=Patientms.DateToAge(pm.Birthdate);
			}
			
		}





    }
}
