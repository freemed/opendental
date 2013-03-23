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

		#region Only used for the patient portal

		///<summary>Gets one Patient from the db based on username.  This is used when the patient is attempting to log in.</summary>
		public static OpenDentBusiness.Patient GetOnePatientPortal(string patUserName,string OnlinePassword) {
			string command="SELECT * FROM patient"
				+" WHERE OnlinePassword= '"+OpenDentBusiness.POut.String(OnlinePassword)+"' "
				+" AND LCASE(Concat(FName,PatNum))='"+OpenDentBusiness.POut.String(patUserName.ToLower())+"'";
			List<OpenDentBusiness.Patient> list=OpenDentBusiness.Crud.PatientCrud.SelectMany(command);
			if(list.Count>0) {
				return list[0];
			}
			//No patient was found or the password was incorrect.  Return null.
			return null;
		}

		///<summary>Gets all family members of the patient passed in.</summary>
		public static List<OpenDentBusiness.Patient> GetFamilyPatientPortal(long patNum) {
			string command="SELECT * FROM patient WHERE guarantor in (SELECT guarantor FROM patient WHERE PatNum ="+OpenDentBusiness.POut.Long(patNum)+")";
			List<OpenDentBusiness.Patient> listFam=OpenDentBusiness.Crud.PatientCrud.SelectMany(command);
			for(int i=0;i<listFam.Count;i++) {
				listFam[i].Age=OpenDentBusiness.Patients.DateToAge(listFam[i].Birthdate);
			}
			return listFam;
		}

		#endregion


	}
}