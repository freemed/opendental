using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClaimL{

		///<summary>Updates all claimproc estimates and also updates claim totals to db. Must supply all claimprocs for this patient (or for this plan if fam max or ded).  Must supply procList which includes all procedures that this claim is linked to.  Will also need to refresh afterwards to see the results</summary>
		public static void CalculateAndUpdate(List<Procedure> procList,List <InsPlan> planList,Claim claimCur,List <PatPlan> patPlans,List <Benefit> benefitList){
			List<ClaimProc> ClaimProcsForClaim=ClaimProcs.RefreshForClaim(claimCur.ClaimNum);
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			double writeoff=0;
			InsPlan PlanCur=InsPlans.GetPlan(claimCur.PlanNum,planList);
			if(PlanCur==null){
				return;
			}
			int provNum;
			double dedRem;
			int patPlanNum=PatPlans.GetPatPlanNum(patPlans,claimCur.PlanNum);
			//this next line has to be done outside the loop.  Takes annual max into consideration 
			double insRem=0;//no changes get made to insRem in the loop.
			/*
			if(patPlanNum==0){//patient does not have current coverage
				insRem=0;
			} 
			else if(ClaimProcsForClaim[0].ProcDate.Year<1880) {
				insRem=InsPlans.GetInsRem(claimProcList,DateTime.Today,claimCur.PlanNum,
						patPlanNum,claimCur.ClaimNum,planList,benefitList);
			} 
			else {
				insRem=InsPlans.GetInsRem(claimProcList,ClaimProcsForClaim[0].ProcDate,claimCur.PlanNum,
						patPlanNum,claimCur.ClaimNum,planList,benefitList);
			}*/
			//first loop handles totals for received items.
			for(int i=0;i<ClaimProcsForClaim.Count;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.Received){
					continue;//disregard any status except Receieved.
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				insPayAmt+=ClaimProcsForClaim[i].InsPayAmt;
			}
			//loop again only for procs not received.
			//And for preauth.
			Procedure ProcCur;
			InsPlan plan=InsPlans.GetPlan(claimCur.PlanNum,planList);
			List<ClaimProcHist> histList=null;
			List<ClaimProcHist> loopList=null;
			for(int i=0;i<ClaimProcsForClaim.Count;i++) {
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived
					&& ClaimProcsForClaim[i].Status!=ClaimProcStatus.Preauth){
					continue;
				}
				ProcCur=Procedures.GetProcFromList(procList,ClaimProcsForClaim[i].ProcNum);
				if(ProcCur.ProcNum==0){
					continue;//ignores payments, etc
				}
				//fee:
				int qty=ProcCur.UnitQty + ProcCur.BaseUnits;
				if(qty==0){
					qty=1;
				}
				if(PlanCur.ClaimsUseUCR){//use UCR for the provider of the procedure
					provNum=ProcCur.ProvNum;
					if(provNum==0){//if no prov set, then use practice default.
						provNum=PrefC.GetInt("PracticeDefaultProv");
					}
					ClaimProcsForClaim[i].FeeBilled=qty*(Fees.GetAmount0(//get the fee based on code and prov fee sched
						ProcCur.CodeNum,ProviderC.ListLong[Providers.GetIndexLong(provNum)].FeeSched));
				}
				else{//don't use ucr.  Use the procedure fee instead.
					ClaimProcsForClaim[i].FeeBilled=qty*ProcCur.ProcFee;
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				if(claimCur.ClaimType=="PreAuth" || claimCur.ClaimType=="Other") {
					//only the fee gets calculated, the rest does not
					ClaimProcs.Update(ClaimProcsForClaim[i]);
					continue;
				}
				//deduct:
				if(patPlanNum==0){//patient does not have current coverage
					dedRem=0;
				}
				else{
					dedRem=0;
/*
					dedRem=InsPlans.GetDedRem(claimProcList,ClaimProcsForClaim[i].ProcDate,claimCur.PlanNum,patPlanNum,
						claimCur.ClaimNum,planList,benefitList,ProcedureCodes.GetStringProcCode(ProcCur.CodeNum))
						-dedApplied;//subtracts deductible amounts already applied on this claim
					if(dedRem<0) {
						dedRem=0;
					*/
				}
				if(dedRem > ClaimProcsForClaim[i].FeeBilled){//if deductible is more than cost of procedure
					ClaimProcsForClaim[i].DedApplied=ClaimProcsForClaim[i].FeeBilled;
				}
				else{
					ClaimProcsForClaim[i].DedApplied=dedRem;
				}
				ClaimProcs.ComputeBaseEst(ClaimProcsForClaim[i],ProcCur.ProcFee,ProcCur.ToothNum,ProcCur.CodeNum,plan,patPlanNum,benefitList,histList,loopList);//handles dedBeforePerc
				if(claimCur.ClaimType=="P") {//primary
					ClaimProcsForClaim[i].InsPayEst=Procedures.GetEst(ProcCur,ClaimProcsForClaim,PriSecTot.Pri,patPlans,true);	
				}
				else if(claimCur.ClaimType=="S") {//secondary
					ClaimProcsForClaim[i].InsPayEst=Procedures.GetEst(ProcCur,ClaimProcsForClaim,PriSecTot.Sec,patPlans,true);
				}
				if(claimCur.ClaimType=="P" || claimCur.ClaimType=="S") {
					/*if(ClaimProcsForClaim[i].DedBeforePerc) {
						int percent=100;
						if(ClaimProcsForClaim[i].Percentage!=-1) {
							percent=ClaimProcsForClaim[i].Percentage;
						}
						if(ClaimProcsForClaim[i].PercentOverride!=-1) {
							percent=ClaimProcsForClaim[i].PercentOverride;
						}
						ClaimProcsForClaim[i].InsPayEst-=ClaimProcsForClaim[i].DedApplied*(double)percent/100d;
					}
					else {*/
						ClaimProcsForClaim[i].InsPayEst-=ClaimProcsForClaim[i].DedApplied;
					//}
				}
				//claimtypes other than P and S only changed manually
				if(ClaimProcsForClaim[i].InsPayEst < 0){
					//example: if inspayest = 19 - 50(ded) for total of -31.
					ClaimProcsForClaim[i].DedApplied+=ClaimProcsForClaim[i].InsPayEst;//eg. 50+(-31)=19
					ClaimProcsForClaim[i].InsPayEst=0;
					//so only 19 of deductible gets applied, and inspayest is 0
				}
				if(insRem-insPayEst<0) {//total remaining ins-Estimated so far on this claim
					ClaimProcsForClaim[i].InsPayEst=0;
				}
				else if(ClaimProcsForClaim[i].InsPayEst>insRem-insPayEst) {
					ClaimProcsForClaim[i].InsPayEst=insRem-insPayEst;
				}
				if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived){
					ClaimProcsForClaim[i].WriteOff=0;
					if(claimCur.ClaimType=="P" && PlanCur.PlanType=="p") {//Primary && PPO
						double insplanAllowed=Fees.GetAmount(ProcCur.CodeNum,PlanCur.FeeSched);
						if(insplanAllowed!=-1) {
							ClaimProcsForClaim[i].WriteOff=ProcCur.ProcFee-insplanAllowed;
						}
						//else, if -1 fee not found, then do not show a writeoff. User can change writeoff if they disagree.
					}
					writeoff+=ClaimProcsForClaim[i].WriteOff;
				}
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				ClaimProcsForClaim[i].ProcDate=ProcCur.ProcDate.Date;//this solves a rare bug. Keeps dates synched.
					//It's rare enough that I'm not goint to add it to the db maint tool.
				ClaimProcs.Update(ClaimProcsForClaim[i]);
				//but notice that the ClaimProcs lists are not refreshed until the loop is finished.
			}//for claimprocs.forclaim
			claimCur.ClaimFee=claimFee;
			claimCur.DedApplied=dedApplied;
			claimCur.InsPayEst=insPayEst;
			claimCur.InsPayAmt=insPayAmt;
			claimCur.WriteOff=writeoff;
			//Cur=ClaimCur;
			Claims.Update(claimCur);
		}



	}
}