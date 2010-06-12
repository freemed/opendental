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
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			//the UI would block this due to carrier not supporting this transaction type.
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms);
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
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms);
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
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms);
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
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms);
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
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms);
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
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(pat.PatNum,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms);
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


	}
}
