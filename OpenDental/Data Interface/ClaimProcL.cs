using System;
using System.Collections;
using System.Data;
using OpenDentBusiness;

namespace OpenDental{
	
	///<summary></summary>
	public class ClaimProcL{

		///<summary>Calculates the Base estimate for a procedure.  This is not done on the fly.  Use Procedure.GetEst to later retrieve the estimate. This function duplicates/replaces all of the upper estimating logic that is within FormClaimProc.  BaseEst=((fee or allowedOverride)-Copay) x (percentage or percentOverride). The result is now stored in a claimProc.  The claimProcs do get updated frequently depending on certain actions the user takes.  The calling class must have already created the claimProc, and this function simply updates the BaseEst field of that claimproc. pst.Tot not used.  For Estimate and CapEstimate, all the estimate fields will be recalculated except the three overrides.</summary>
		public static void ComputeBaseEst(ClaimProc cp, Procedure proc,PriSecTot pst,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benList) {//,bool resetValues){ 
			if(cp.Status==ClaimProcStatus.CapClaim
				|| cp.Status==ClaimProcStatus.CapComplete
				|| cp.Status==ClaimProcStatus.Preauth
				|| cp.Status==ClaimProcStatus.Supplemental) {
				return;//never compute estimates for those types listed above.
			}
			bool resetAll=false;
			if(cp.Status==ClaimProcStatus.Estimate || cp.Status==ClaimProcStatus.CapEstimate) {
				resetAll=true;
			}
			//NoBillIns is only calculated when creating the claimproc, even if resetAll is true.
			//If user then changes a procCode, it does not cause an update of all procedures with that code.
			if(cp.NoBillIns) {
				cp.AllowedOverride=-1;
				cp.CopayAmt=0;
				cp.CopayOverride=-1;
				cp.DedApplied=0;
				cp.Percentage=-1;
				cp.PercentOverride=-1;
				cp.WriteOff=0;
				cp.BaseEst=0;
				return;
			}
			//This function is called every time a ProcFee is changed,
			//so the BaseEst does reflect the new ProcFee.
			cp.BaseEst=proc.ProcFee;
			//if(resetAll){
			//AllowedOverride=-1;
			//actually, this is a bad place for altering AllowedOverride.
			//Best to set it at the same time as the fee.
			//Actually, AllowedOverride should almost never be altered by the program, only by the user.
			//}
			InsPlan plan=null;
			if(pst==PriSecTot.Pri) {
				plan=InsPlans.GetPlan(patPlans[0].PlanNum,PlanList);
			}
			else if(pst==PriSecTot.Sec) {
				plan=InsPlans.GetPlan(patPlans[1].PlanNum,PlanList);
			}
			//if(cp.AllowedOverride==-1) {//If allowedOverride is blank
			//wrong:
			//	cp.AllowedOverride=InsPlans.GetAllowed(ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode,cp.PlanNum,PlanList);
			//}
			if(cp.AllowedOverride!=-1) {
				cp.BaseEst=cp.AllowedOverride;
			}
			else{
				double carrierAllowed=InsPlans.GetAllowed(ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode,cp.PlanNum,PlanList,
					proc.ToothNum,cp.ProvNum);
				if(carrierAllowed!=-1){
					cp.BaseEst=carrierAllowed;
				}
			}
			cp.DedBeforePerc=plan.DedBeforePerc;
			//dedApplied is never recalculated here
			//deductible is initially 0 anyway, so this calculation works.
			//Once there is a deductible included, this calculation would come out different, which is also ok.
			if(cp.DedBeforePerc) {
				//can't do this here.  Has to be done externally, just like when !DedBeforePerc
				//cp.BaseEst-=cp.DedApplied;
			}
			//copayAmt
			//copayOverride never recalculated
			if(resetAll) {
				if(pst==PriSecTot.Pri) {
					cp.CopayAmt=InsPlans.GetCopay(ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode,plan);
				}
				else if(pst==PriSecTot.Sec) {
					cp.CopayAmt=InsPlans.GetCopay(ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode,plan);
				}
				else {//pst.Other
					cp.CopayAmt=-1;
				}
				if(cp.CopayAmt>proc.ProcFee){
					cp.CopayAmt=proc.ProcFee;
				}
				if(cp.Status==ClaimProcStatus.CapEstimate) {
					//this does automate the Writeoff. If user does not want writeoff automated,
					//then they will have to complete the procedure first. (very rare)
					if(cp.CopayAmt==-1) {
						cp.CopayAmt=0;
					}
					if(cp.CopayOverride!=-1) {//override the copay
						cp.WriteOff=proc.ProcFee-cp.CopayOverride;
					}
					else if(cp.CopayAmt!=-1) {//use the calculated copay
						cp.WriteOff=proc.ProcFee-cp.CopayAmt;
					}
					//else{//no copay at all
					//	WriteOff=proc.ProcFee;
					//}
					if(cp.WriteOff<0) {
						cp.WriteOff=0;
					}
					cp.AllowedOverride=-1;
					cp.DedApplied=0;
					cp.Percentage=-1;
					cp.PercentOverride=-1;
					cp.BaseEst=0;
					return;
				}
			}
			if(cp.CopayOverride!=-1) {//subtract copay if override
				cp.BaseEst-=cp.CopayOverride;
			}
			else if(cp.CopayAmt!=-1) {//otherwise subtract calculated copay
				cp.BaseEst-=cp.CopayAmt;
			}
			//percentage
			//percentoverride never recalculated
			if(pst==PriSecTot.Pri) {
				cp.Percentage=Benefits.GetPercent(ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode,plan,patPlans[0],benList);//will never =-1
			}
			else if(pst==PriSecTot.Sec) {
				cp.Percentage=Benefits.GetPercent(ProcedureCodes.GetProcCode(proc.CodeNum).ProcCode,plan,patPlans[1],benList);
			}
			if(cp.PercentOverride==-1) {//no override, so use calculated Percentage
				cp.BaseEst=cp.BaseEst*(double)cp.Percentage/100;
			}
			else {//override, so use PercentOverride
				cp.BaseEst=cp.BaseEst*(double)cp.PercentOverride/100;
			}
		}

	}
}