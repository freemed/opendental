using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace TestCanada {
	public class Eligibility {
		
		public static string RunOne(bool showForms) {
			string retVal="";
			long provNum=ProviderC.List[0].ProvNum;//dentist #1
			Patient pat=Patients.GetPat(PatientTC.PatNum1);//patient#1
			if(pat.PriProv!=provNum){
				Patient oldPat=pat.Copy();
				pat.PriProv=provNum;//this script uses the primary provider for the patient
				Patients.Update(pat,oldPat);
			}
			PatPlan patplan=PatPlans.GetPatPlan(pat.PatNum,1);
			InsSub sub=InsSubs.GetOne(patplan.InsSubNum);
			InsPlan plan=InsPlans.GetPlan(sub.PlanNum,new List<InsPlan>());
			//the UI would block this due to carrier not supporting this transaction type.
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms,sub);
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!="R") {
				throw new Exception("Should be R");
			}
			retVal+="Eligibility #1 successful.\r\n";
			return retVal;
		}

		public static string RunTwo(bool showForms) {
			string retVal="";
			long provNum=ProviderC.List[0].ProvNum;//dentist #1
			Patient pat=Patients.GetPat(PatientTC.PatNum7);//patient#7
			if(pat.PriProv!=provNum){
				Patient oldPat=pat.Copy();
				pat.PriProv=provNum;//this script uses the primary provider for the patient
				Patients.Update(pat,oldPat);
			}
			PatPlan patplan=PatPlans.GetPatPlan(pat.PatNum,1);
			InsSub sub=InsSubs.GetOne(patplan.InsSubNum);
			InsPlan plan=InsPlans.GetPlan(sub.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms,sub);
			//should print Eligibility response on Dentaide Form
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!="E") {//no errors
				throw new Exception("Should be E");
			}
			retVal+="Eligibility #2 successful.\r\n";
			return retVal;
		}

		public static string RunThree(bool showForms) {
			string retVal="";
			long provNum=ProviderC.List[0].ProvNum;//dentist #1
			Patient pat=Patients.GetPat(PatientTC.PatNum6);//patient#6
			if(pat.PriProv!=provNum){
				Patient oldPat=pat.Copy();
				pat.PriProv=provNum;//this script uses the primary provider for the patient
				Patients.Update(pat,oldPat);
			}
			PatPlan patplan=PatPlans.GetPatPlan(pat.PatNum,2);
			InsSub sub=InsSubs.GetOne(patplan.InsSubNum);
			InsPlan plan=InsPlans.GetPlan(sub.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms,sub);
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!="R") {
				throw new Exception("Should be R");
			}
			retVal+="Eligibility #3 successful.\r\n";
			return retVal;
		}

		public static string RunFour(bool showForms) {
			string retVal="";
			long provNum=ProviderC.List[1].ProvNum;//dentist #2
			Patient pat=Patients.GetPat(PatientTC.PatNum6);//patient#6
			if(pat.PriProv!=provNum){
				Patient oldPat=pat.Copy();
				pat.PriProv=provNum;//this script uses the primary provider for the patient
				Patients.Update(pat,oldPat);
			}
			PatPlan patplan=PatPlans.GetPatPlan(pat.PatNum,1);
			InsSub sub=InsSubs.GetOne(patplan.InsSubNum);
			InsPlan plan=InsPlans.GetPlan(sub.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms,sub);
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!="M") {
				throw new Exception("Should be M");
			}
			retVal+="Eligibility #4 successful.\r\n";
			return retVal;
		}

		public static string RunFive(bool showForms) {
			string retVal="";
			long provNum=ProviderC.List[0].ProvNum;//dentist #1
			Patient pat=Patients.GetPat(PatientTC.PatNum5);//patient#5
			if(pat.PriProv!=provNum){
				Patient oldPat=pat.Copy();
				pat.PriProv=provNum;//this script uses the primary provider for the patient
				Patients.Update(pat,oldPat);
			}
			PatPlan patplan=PatPlans.GetPatPlan(pat.PatNum,1);
			InsSub sub=InsSubs.GetOne(patplan.InsSubNum);
			InsPlan plan=InsPlans.GetPlan(sub.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms,sub);
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!="E") {
				throw new Exception("Should be E");
			}
			retVal+="Eligibility #5 successful.\r\n";
			return retVal;
		}

		public static string RunSix(bool showForms) {
			string retVal="";
			long provNum=ProviderC.List[0].ProvNum;//dentist #1
			Patient pat=Patients.GetPat(PatientTC.PatNum9);//patient#9
			if(pat.PriProv!=provNum){
				Patient oldPat=pat.Copy();
				pat.PriProv=provNum;//this script uses the primary provider for the patient
				Patients.Update(pat,oldPat);
			}
			PatPlan patplan=PatPlans.GetPatPlan(pat.PatNum,1);
			InsSub sub=InsSubs.GetOne(patplan.InsSubNum);
			InsPlan plan=InsPlans.GetPlan(sub.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms,sub);
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!="E") {
				throw new Exception("Should be E");
			}
			retVal+="Eligibility #6 successful.\r\n";
			return retVal;
		}

		public static string RunEligibility(long provNum,Patient pat,string expectedStatus,int scriptNum,bool showForms) {
			string retVal="";
			if(pat.PriProv!=provNum) {
				Patient oldPat=pat.Copy();
				pat.PriProv=provNum;//this script uses the primary provider for the patient
				Patients.Update(pat,oldPat);
			}
			PatPlan patplan=PatPlans.GetPatPlan(pat.PatNum,1);
			InsSub sub=InsSubs.GetOne(patplan.InsSubNum);
			InsPlan plan=InsPlans.GetPlan(sub.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms,sub);
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!=expectedStatus) {
				throw new Exception("Should be "+expectedStatus);
			}
			retVal+="Eligibility #"+scriptNum+" successful.\r\n";
			return retVal;
		}

		public static string RunSeven(bool showForms) {
			return RunEligibility(ProviderC.List[2].ProvNum,//dentist #1b
				Patients.GetPat(PatientTC.PatNum1B),//patient#1b
				"R",7,showForms);			
		}

		public static string RunEight(bool showForms) {
			return RunEligibility(ProviderC.List[2].ProvNum,//dentist #1b
				Patients.GetPat(PatientTC.PatNum7B),//patient#7b
				"E",8,showForms);
		}

		public static string RunNine(bool showForms) {
			return RunEligibility(ProviderC.List[2].ProvNum,//dentist #1b
				Patients.GetPat(PatientTC.PatNum6B),//patient#6b
				"R",9,showForms);
		}

		public static string RunTen(bool showForms) {
			return RunEligibility(ProviderC.List[3].ProvNum,//dentist #2b
				Patients.GetPat(PatientTC.PatNum6B),//patient#6b
				"E",10,showForms);
		}

		public static string RunEleven(bool showForms) {
			return RunEligibility(ProviderC.List[2].ProvNum,//dentist #1b
				Patients.GetPat(PatientTC.PatNum5B),//patient#5b
				"E",11,showForms);
		}

		public static string RunTwelve(bool showForms) {
			return RunEligibility(ProviderC.List[2].ProvNum,//dentist #1b
				Patients.GetPat(PatientTC.PatNum9B),//patient#9b
				"E",12,showForms);
		}

	}
}
