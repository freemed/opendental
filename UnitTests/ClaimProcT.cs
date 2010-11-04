using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using OpenDentBusiness;

namespace UnitTests {
	public class ClaimProcT {
		public static void AddInsUsedAdjustment(long patNum,long planNum,double amtPaid,long subNum){
			ClaimProc cp=new ClaimProc();
			cp.PatNum=patNum;
			cp.PlanNum=planNum;
			cp.InsSubNum=subNum;
			cp.ProcDate=DateTime.Today;
			cp.Status=ClaimProcStatus.Adjustment;
			cp.InsPayAmt=amtPaid;
			//cp.DedApplied=
			ClaimProcs.Insert(cp);
		}

		///<summary>This tells the calculating logic that insurance paid on a procedure.  It avoids the creation of an actual claim.</summary>
		public static void AddInsPaid(long patNum,long planNum,long procNum,double amtPaid,long subNum) {
			ClaimProc cp=new ClaimProc();
			cp.ProcNum=procNum;
			cp.PatNum=patNum;
			cp.PlanNum=planNum;
			cp.InsSubNum=subNum;
			cp.InsPayAmt=amtPaid;
			cp.Status=ClaimProcStatus.Received;
			cp.DateCP=DateTime.Today;
			cp.ProcDate=DateTime.Today;
			ClaimProcs.Insert(cp);
		}




	}
}
