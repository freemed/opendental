using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class PatPlanT {
		public static PatPlan CreatePatPlan(int ordinal,long patNum,long planNum){
			PatPlan patPlan=new PatPlan();
			patPlan.Ordinal=ordinal;
			patPlan.PatNum=patNum;
			patPlan.PlanNum=planNum;
			PatPlans.Insert(patPlan);
			return patPlan;
		}




	}
}
