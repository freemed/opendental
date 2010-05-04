using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class ClaimProcT {
		public static void AddInsUsedAdjustment(long patNum,long planNum,double amtPaid){
			ClaimProc cp=new ClaimProc();
			cp.PatNum=patNum;
			cp.PlanNum=planNum;
			cp.ProcDate=DateTime.Today;
			cp.Status=ClaimProcStatus.Adjustment;
			cp.InsPayAmt=amtPaid;
			//cp.DedApplied=
			ClaimProcs.Insert(cp);
		}






	}
}
