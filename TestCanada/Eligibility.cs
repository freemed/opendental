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
			Provider prov=ProviderC.List[0];
			PatPlan patplan=PatPlans.GetPatPlan(PatientTC.PatNum1,1);
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			//the UI would block this due to carrier not supporting this transaction type.
			long etransNum=CanadianOutput.SendElegibility(PatientTC.PatNum1,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,showForms);
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
					/*Provider prov=ProviderC.List[0];//dentist#1
			PatPlan patplan=PatPlans.GetPatPlan(PatientTC.PatNum7,1);
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			long etransNum=CanadianOutput.SendElegibility(PatientTC.PatNum1,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID,true);
			//should print Dentaide Form
			Etrans etrans=Etranss.GetEtrans(etransNum);
			string message=EtransMessageTexts.GetMessageText(etrans.EtransMessageTextNum);
			CCDFieldInputter formData=new CCDFieldInputter(message);
			string responseStatus=formData.GetValue("G05");
			if(responseStatus!="E") {//no errors
				throw new Exception("Should be E");
			}
			retVal+="Eligibility #2 successful.\r\n";*/
			retVal+="Eligibility #2 (not yet implemented).\r\n";
			return retVal;
		}

		public static string RunThree(bool showForms) {
			string retVal="";

			retVal+="Eligibility #3 (not yet implemented).\r\n";
			return retVal;
		}

		public static string RunFour(bool showForms) {
			string retVal="";

			retVal+="Eligibility #4 (not yet implemented).\r\n";
			return retVal;
		}

		public static string RunFive(bool showForms) {
			string retVal="";

			retVal+="Eligibility #5 (not yet implemented).\r\n";
			return retVal;
		}

		public static string RunSix(bool showForms) {
			string retVal="";

			retVal+="Eligibility #6 (not yet implemented).\r\n";
			return retVal;
		}


	}
}
