using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class Eligibility {
		
		public static string RunOne() {
			string retVal="";
			Provider prov=ProviderC.List[0];
			PatPlan patplan=PatPlans.GetPatPlan(PatientTC.PatNum1,1);
			InsPlan plan=InsPlans.GetPlan(patplan.PlanNum,new List<InsPlan>());
			string result=OpenDental.Eclaims.CanadianOutput.SendElegibility(PatientTC.PatNum1,plan,new DateTime(1999,1,1),patplan.Relationship,patplan.PatID);
			retVal+="Eligibility #1 successful.\r\n";
			return retVal;
		}

	}
}
