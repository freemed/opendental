using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class InsSubT {
		///<summary></summary>
		public static InsSub CreateInsSub(long subscriberNum,long planNum){
			InsSub sub=new InsSub();
			sub.Subscriber=subscriberNum;
			sub.PlanNum=planNum;
			sub.SubscriberID="1234";
			InsSubs.Insert(sub);
			return sub;
		}



	}
}
