using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class PatPlanT {
		public static PatPlan CreatePatPlan(byte ordinal,long patNum,long subNum){
			PatPlan patPlan=new PatPlan();
			patPlan.Ordinal=ordinal;
			patPlan.PatNum=patNum;
			patPlan.InsSubNum=subNum;
			PatPlans.Insert(patPlan);
			return patPlan;
		}




	}
}
