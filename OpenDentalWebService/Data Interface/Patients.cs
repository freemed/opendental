using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService {
	public class Patients {

		public static OpenDentBusiness.Patient GetPat(long patNum) {
			return OpenDentBusiness.Patients.GetPat(patNum);
		}

		///<summary></summary>
		public static DataTable GetPtDataTable(bool limit,string lname,string fname,string phone,
			string address,bool hideInactive,string city,string state,string ssn,string patnum,string chartnumber,
			long billingtype,bool guarOnly,bool showArchived,long clinicNum,DateTime birthdate,
			long siteNum,string subscriberId,string email) 
		{
			DataTable table=new DataTable();
			table=OpenDentBusiness.Patients.GetPtDataTable(limit,lname,fname,phone,address,hideInactive,city,state,ssn,patnum,chartnumber,billingtype,guarOnly,showArchived,clinicNum,birthdate,siteNum,subscriberId,email);
			return table;
		}


	}
}