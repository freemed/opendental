using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDental;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace TestCanada {
	public class InsPlanTC {
		

		public static void SetAssignBen(long planNum,bool assignBen) {
			InsPlan plan=InsPlans.RefreshOne(planNum);
			if(plan.AssignBen==assignBen){
				return;//no change needed
			}
			plan.AssignBen=assignBen;
			InsPlans.Update(plan);
		}

		


	}
}
