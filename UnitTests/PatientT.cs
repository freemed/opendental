using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class PatientT {
		public static Patient CreatePatient(string suffix){
			Patient pat=new Patient();
			pat.IsNew=true;
			pat.LName="LName"+suffix;
			pat.FName="FName"+suffix;
			pat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			pat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);//This causes standard fee sched to be 53.
			Patients.Insert(pat,false);
			Patient oldPatient=pat.Copy();
			pat.Guarantor=pat.PatNum;
			Patients.Update(pat,oldPatient);
			return pat;
		}

		public static void SetGuarantor(Patient pat,long guarantorNum){
			Patient oldPatient=pat.Copy();
			pat.Guarantor=guarantorNum;
			Patients.Update(pat,oldPatient);
		}


	}
}
