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
		public List<PatDetails> patDetailsList;

		public AccountModel(Patientm patm) {
			patDetailsList= new List<PatDetails>();
			List<Patientm>  patList=Patientms.GetPatientmsOfFamily(patm.CustomerNum,patm.PatNum);
			foreach(Patientm pm in patList) {
				patDetailsList.Add(GetDetails(pm));
			}
		}

		private PatDetails GetDetails(Patientm patm) {
			PatDetails pd=new PatDetails();
			pd.patm=patm;
			if(patm.BalTotal<0) {
				pd.BalTotal="-$" + Math.Abs(patm.BalTotal);
			}
			else{
				pd.BalTotal="$" + Math.Abs(patm.BalTotal);
			}
			if(patm.InsEst<0) {
				pd.InsEst="-$" + Math.Abs(patm.InsEst);
			}
			else {
				pd.InsEst="$" + Math.Abs(patm.InsEst);
			}
			if((patm.BalTotal-patm.InsEst)<0){
				pd.AfterIns="-$" + Math.Abs(patm.BalTotal-patm.InsEst);
			}
			else {
				pd.AfterIns="$" + Math.Abs(patm.BalTotal-patm.InsEst);
			}
			return pd;
		}

		public class PatDetails {//inner class used for convenience
			public Patientm patm;
			public string BalTotal;
			public string InsEst;
			public string AfterIns;
		}



	}




}