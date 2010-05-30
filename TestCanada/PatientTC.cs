using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class PatientTC {
		public static string SetInitialPatients() {
			Patient pat=new Patient();
			pat.Gender=PatientGender.Female;
			pat.Birthdate=new DateTime(1960,04,12);
			pat.LName="Fête";
			pat.FName="Lisa";
			pat.MiddleI="Ҫ";
			pat.Address="124 - 1500 Rue";
			pat.City="Montréal";
			pat.State="QC";
			pat.Zip="H1C2D4";
			pat.Language="fr";
			pat.PatNum=Patients.Insert(pat,false);
			pat.CanadianEligibilityCode=2;//disabled
			Patient oldPatient=pat.Copy();
			pat.Guarantor=pat.PatNum;
			Patients.Update(pat,oldPatient);
			ProcTC.SetExtracted("23",new DateTime(1995,2,7),pat.PatNum);
			ProcTC.SetExtracted("26",new DateTime(1996,11,13),pat.PatNum);
			InsPlan plan=new InsPlan();
			plan.CarrierNum=CarrierTC.GetCarrierNumById("666666");
			plan.GroupNum="PLAN012";
			plan.SubscriberID="AB123C4G";
			plan.Subscriber=pat.PatNum;
			plan.DentaideCardSequence=2;
			InsPlans.Insert(plan);
			PatPlan patplan=new PatPlan();
			patplan.PatNum=pat.PatNum;
			patplan.PlanNum=plan.PlanNum;
			patplan.Relationship=Relat.Self;
			PatPlans.Insert(patplan);
			//Patient 2---------------------------------------------------------------------






			return "Patient objects set.\r\n";
		}

	}
}
