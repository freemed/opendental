using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using OpenDentBusiness;
using OpenDentBusiness.Mobile;
using WebForms;


namespace PatientPortalMVC.Models {
	public class LoginModel {

		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
	}

	public class AccountModel {
		public string BalTotal{ get; set; }
		public string InsEst{ get; set; }
		public string AfterIns { get; set; }

		public AccountModel(Patientm patm) {
			if(patm.BalTotal<0) {
				BalTotal="-$" + Math.Abs(patm.BalTotal);
			}
			else{
				BalTotal="$" + Math.Abs(patm.BalTotal);
			}
			if(patm.InsEst<0) {
				InsEst="-$" + Math.Abs(patm.InsEst);
			}
			else {
				InsEst="$" + Math.Abs(patm.InsEst);
			}
			if((patm.BalTotal-patm.InsEst)<0){
				AfterIns="-$" + Math.Abs(patm.BalTotal-patm.InsEst);
			}
			else {
				AfterIns="$" + Math.Abs(patm.BalTotal-patm.InsEst);
			}

		}

	}




}