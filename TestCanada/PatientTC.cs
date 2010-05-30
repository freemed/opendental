using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class PatientTC {
		public static string SetInitialPatients() {
			Patient pat;
			Patient oldPatient;
			InsPlan plan;
			PatPlan patplan;
			pat=new Patient();
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
			pat.CanadianEligibilityCode=2;//disabled
			//planFlag
			//bandNumber
			//FamilyNumber
			pat.PatNum=Patients.Insert(pat,false);
			oldPatient=pat.Copy();
			pat.Guarantor=pat.PatNum;
			Patients.Update(pat,oldPatient);
			ProcTC.SetExtracted("23",new DateTime(1995,2,7),pat.PatNum);
			ProcTC.SetExtracted("26",new DateTime(1996,11,13),pat.PatNum);
			plan=new InsPlan();
			plan.CarrierNum=CarrierTC.GetCarrierNumById("666666");
			plan.GroupNum="PLAN012";
			plan.SubscriberID="AB123C4G";
			plan.Subscriber=pat.PatNum;
			plan.DentaideCardSequence=2;
			InsPlans.Insert(plan);
			patplan=new PatPlan();
			patplan.PatNum=pat.PatNum;
			patplan.PlanNum=plan.PlanNum;
			patplan.Relationship=Relat.Self;//1
			patplan.PatID="00";
			patplan.Ordinal=1;
			PatPlans.Insert(patplan);
			//Patient 2---------------------------------------------------------------------
			pat=new Patient();
			pat.Gender=PatientGender.Male;
			pat.Birthdate=new DateTime(1948,3,2);
			pat.LName="Smith";
			pat.FName="John";
			pat.MiddleI="";
			pat.CanadianEligibilityCode=4;//code not applicable
			pat.Address="P.O. Box 1500";
			pat.Address2="Little Field Estates";
			pat.City="East Westchester";
			pat.State="ON";
			pat.Zip="M7F2J9";
			pat.Language="en";
			pat.PatNum=Patients.Insert(pat,false);
			oldPatient=pat.Copy();
			pat.Guarantor=pat.PatNum;
			Patients.Update(pat,oldPatient);
			plan=new InsPlan();
			plan.CarrierNum=CarrierTC.GetCarrierNumById("666666");
			plan.GroupNum="PLAN02";
			plan.SubscriberID="123432145222";
			plan.Subscriber=pat.PatNum;
			plan.DivisionNo="1542B";
			//plan.DentaideCardSequence=;
			InsPlans.Insert(plan);
			patplan=new PatPlan();
			patplan.PatNum=pat.PatNum;
			patplan.PlanNum=plan.PlanNum;
			patplan.Relationship=Relat.Self;//1
			patplan.PatID="00";
			patplan.Ordinal=1;
			PatPlans.Insert(patplan);
			plan=new InsPlan();
			plan.CarrierNum=CarrierTC.GetCarrierNumById("777777");
			plan.GroupNum="P9902";
			plan.SubscriberID="12343B7";
			plan.Subscriber=pat.PatNum;
			plan.DivisionNo="";
			//plan.DentaideCardSequence=;
			InsPlans.Insert(plan);
			patplan=new PatPlan();
			patplan.PatNum=pat.PatNum;
			patplan.PlanNum=plan.PlanNum;
			patplan.Relationship=Relat.Self;//1
			patplan.PatID="00";
			patplan.Ordinal=2;
			PatPlans.Insert(patplan);
			//Patient 3---------------------------------------------------------------------
			/*
			pat=new Patient();
			pat.Gender=PatientGender.Female;
			pat.Birthdate=new DateTime(1978,4,12);
			pat.LName="Walls";
			pat.FName="Mary";
			pat.MiddleI="A";
			pat.CanadianEligibilityCode=4;//code not applicable
			pat.Address="P.O. Box 1500";
			pat.Address2="Little Field Estates";
			pat.City="East Westchester";
			pat.State="ON";
			pat.Zip="M7F2J9";
			pat.Language="en";
			pat.PatNum=Patients.Insert(pat,false);
			oldPatient=pat.Copy();
			pat.Guarantor=pat.PatNum;
			Patients.Update(pat,oldPatient);
			plan=new InsPlan();
			plan.CarrierNum=CarrierTC.GetCarrierNumById("666666");
			plan.GroupNum="PLAN02";
			plan.SubscriberID="123432145222";
			plan.Subscriber=pat.PatNum;
			plan.DivisionNo="1542B";
			//plan.DentaideCardSequence=;
			InsPlans.Insert(plan);
			patplan=new PatPlan();
			patplan.PatNum=pat.PatNum;
			patplan.PlanNum=plan.PlanNum;
			patplan.Relationship=Relat.Self;//1
			patplan.PatID="00";
			patplan.Ordinal=1;
			PatPlans.Insert(patplan);
			plan=new InsPlan();
			plan.CarrierNum=CarrierTC.GetCarrierNumById("777777");
			plan.GroupNum="P9902";
			plan.SubscriberID="12343B7";
			plan.Subscriber=pat.PatNum;
			plan.DivisionNo="";
			//plan.DentaideCardSequence=;
			InsPlans.Insert(plan);
			patplan=new PatPlan();
			patplan.PatNum=pat.PatNum;
			patplan.PlanNum=plan.PlanNum;
			patplan.Relationship=Relat.Self;//1
			patplan.PatID="00";
			patplan.Ordinal=2;
			PatPlans.Insert(patplan);*/






			return "Patient objects set.\r\n";
		}

	}
}
