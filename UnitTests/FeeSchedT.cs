using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class FeeSchedT {
		///<summary>Returns feeSchedNum</summary>
		public static long CreateFeeSched(FeeScheduleType feeSchedType,string suffix){
			FeeSched feeSched=new FeeSched();
			feeSched.FeeSchedType=feeSchedType;
			feeSched.Description="FeeSched"+suffix;
			FeeScheds.Insert(feeSched);
			FeeScheds.RefreshCache();
			return feeSched.FeeSchedNum;
		}




	}
}
