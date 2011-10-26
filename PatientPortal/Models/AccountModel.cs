using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
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
		public string BalTotal;
		public string InsEst;
		public string AfterIns;
		public List<Statementm> statementmList;

		public AccountModel(Patientm patm) {
			String formatstring="{0:0.00}";
			BalTotal="$"+String.Format(formatstring,Math.Abs(patm.BalTotal));
			InsEst="$"+String.Format(formatstring,Math.Abs(patm.InsEst));
			AfterIns="$"+String.Format(formatstring,Math.Abs(patm.BalTotal-patm.InsEst));
			if(patm.BalTotal<0) {
				BalTotal="-"+BalTotal;
			}
			if(patm.InsEst<0) {
				InsEst="-"+InsEst;
			}
			if((patm.BalTotal-patm.InsEst)<0) {
				AfterIns="-"+AfterIns;
			}
			statementmList=Statementms.GetStatementms(patm.CustomerNum,patm.PatNum);
		}

	}




}