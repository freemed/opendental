using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class InsPlanT {
		///<summary>Creats an insurance plan with the default fee schedule of 53.</summary>
		public static InsPlan CreateInsPlan(long carrierNum) {
			InsPlan plan=new InsPlan();
			plan.CarrierNum=carrierNum;
			plan.PlanType="";
			plan.FeeSched=53;
			InsPlans.Insert(plan);
			return plan;
		}

		///<summary>Creats an insurance plan with the default fee schedule of 53.</summary>
		public static InsPlan CreateInsPlanPPO(long carrierNum,long feeSchedNum){
			InsPlan plan=new InsPlan();
			plan.CarrierNum=carrierNum;
			plan.PlanType="p";
			plan.FeeSched=feeSchedNum;
			InsPlans.Insert(plan);
			return plan;
		}


	}
}
