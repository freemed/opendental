using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class InsPlanT {
		///<summary>Creats an insurance plan with the default fee schedule of 53.</summary>
		public static InsPlan CreateInsPlan(long subscriberNum,long carrierNum){
			InsPlan plan=new InsPlan();
			plan.Subscriber=subscriberNum;
			plan.CarrierNum=carrierNum;
			plan.SubscriberID="1234";
			plan.PlanType="";
			plan.FeeSched=53;
			InsPlans.Insert(plan);
			return plan;
		}

		///<summary>Creats an insurance plan with the default fee schedule of 53.</summary>
		public static InsPlan CreateInsPlanPPO(long subscriberNum,long carrierNum,long feeSchedNum){
			InsPlan plan=new InsPlan();
			plan.Subscriber=subscriberNum;
			plan.CarrierNum=carrierNum;
			plan.SubscriberID="1234";
			plan.PlanType="p";
			plan.FeeSched=feeSchedNum;
			InsPlans.Insert(plan);
			return plan;
		}


	}
}
