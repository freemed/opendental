using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClaimL{

		///<summary>Updates all claimproc estimates and also updates claim totals to db. Must supply procList which includes all procedures that this claim is linked to.  Will also need to refresh afterwards to see the results</summary>
		public static void CalculateAndUpdate(List<Procedure> procList,List <InsPlan> planList,Claim claimCur,List <PatPlan> patPlans,List <Benefit> benefitList,int patientAge){
			//we need more than just the claimprocs for this claim.
			//in order to run Procedures.ComputeEstimates, we need all claimprocs for all procedures attached to this claim
			List<ClaimProc> ClaimProcsAll=ClaimProcs.Refresh(claimCur.PatNum);
			List<ClaimProc> ClaimProcsForClaim=ClaimProcs.RefreshForClaim(claimCur.ClaimNum);//will be ordered by line number.
			double claimFee=0;
			double dedApplied=0;
			double insPayEst=0;
			double insPayAmt=0;
			double writeoff=0;
			InsPlan plan=InsPlans.GetPlan(claimCur.PlanNum,planList);
			if(plan==null){
				return;
			}
			long patPlanNum=PatPlans.GetPatPlanNum(patPlans,claimCur.PlanNum);
			//first loop handles totals for received items.
			for(int i=0;i<ClaimProcsForClaim.Count;i++){
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.Received){
					continue;//disregard any status except Receieved.
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				dedApplied+=ClaimProcsForClaim[i].DedApplied;
				insPayEst+=ClaimProcsForClaim[i].InsPayEst;
				insPayAmt+=ClaimProcsForClaim[i].InsPayAmt;
				writeoff+=ClaimProcsForClaim[i].WriteOff;
			}
			//loop again only for procs not received.
			//And for preauth.
			Procedure ProcCur;
			//InsPlan plan=InsPlans.GetPlan(claimCur.PlanNum,planList);
			List<ClaimProcHist> histList=ClaimProcs.GetHistList(claimCur.PatNum,benefitList,patPlans,planList,claimCur.ClaimNum,claimCur.DateService);
			List<ClaimProc> claimProcListOld=new List<ClaimProc>();//make a copy
			for(int i=0;i<ClaimProcsAll.Count;i++) {
				claimProcListOld.Add(ClaimProcsAll[i].Copy());
			}
			List<ClaimProcHist> loopList=new List<ClaimProcHist>();
			for(int i=0;i<ClaimProcsForClaim.Count;i++) {//loop through each proc
				ProcCur=Procedures.GetProcFromList(procList,ClaimProcsForClaim[i].ProcNum);
				Procedures.ComputeEstimates(ProcCur,claimCur.PatNum,ref ClaimProcsAll,false,planList,patPlans,benefitList,histList,loopList,false,patientAge);
				//then, add this information to loopList so that the next procedure is aware of it.
				loopList.AddRange(ClaimProcs.GetHistForProc(ClaimProcsAll,ProcCur.ProcNum,ProcCur.CodeNum));
			}
			//save changes in the list to the database
			ClaimProcs.Synch(ref ClaimProcsAll,claimProcListOld);
			ClaimProcsForClaim=ClaimProcs.RefreshForClaim(claimCur.ClaimNum);
			//But ClaimProcsAll has not been refreshed.
			for(int i=0;i<ClaimProcsForClaim.Count;i++) {
				if(ClaimProcsForClaim[i].Status!=ClaimProcStatus.NotReceived
					&& ClaimProcsForClaim[i].Status!=ClaimProcStatus.Preauth
					&& ClaimProcsForClaim[i].Status!=ClaimProcStatus.CapClaim) {
					continue;
				}
				ProcCur=Procedures.GetProcFromList(procList,ClaimProcsForClaim[i].ProcNum);
				if(ProcCur.ProcNum==0) {
					continue;//ignores payments, etc
				}
				//fee:
				int qty=ProcCur.UnitQty + ProcCur.BaseUnits;
				if(qty==0) {
					qty=1;
				}
				if(plan.ClaimsUseUCR) {//use UCR for the provider of the procedure
					long provNum=ProcCur.ProvNum;
					if(provNum==0) {//if no prov set, then use practice default.
						provNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
					}
					//get the fee based on code and prov fee sched
					double feebilled=Fees.GetAmount0(ProcCur.CodeNum,ProviderC.ListLong[Providers.GetIndexLong(provNum)].FeeSched);
					if(feebilled > ProcCur.ProcFee) {
						ClaimProcsForClaim[i].FeeBilled=qty*feebilled;
					}
					else {
						ClaimProcsForClaim[i].FeeBilled=qty*ProcCur.ProcFee;
					}
				}
				//else if(claimCur.ClaimType=="Cap") {//Even for capitation, use the proc fee.
				//	ClaimProcsForClaim[i].FeeBilled=0;
				//}
				else {//don't use ucr.  Use the procedure fee instead.
					ClaimProcsForClaim[i].FeeBilled=qty*ProcCur.ProcFee;
				}
				claimFee+=ClaimProcsForClaim[i].FeeBilled;
				if(claimCur.ClaimType=="PreAuth" || claimCur.ClaimType=="Other" || claimCur.ClaimType=="Cap") {
					//only the fee gets calculated, the rest does not
					ClaimProcs.Update(ClaimProcsForClaim[i]);
					continue;
				}
				//ClaimProcs.ComputeBaseEst(ClaimProcsForClaim[i],ProcCur.ProcFee,ProcCur.ToothNum,ProcCur.CodeNum,plan,patPlanNum,benefitList,histList,loopList);
				ClaimProcsForClaim[i].InsPayEst=ClaimProcs.GetInsEstTotal(ClaimProcsForClaim[i]);
				ClaimProcsForClaim[i].DedApplied=ClaimProcs.GetDedEst(ClaimProcsForClaim[i]);
				if(ClaimProcsForClaim[i].Status==ClaimProcStatus.NotReceived){//(vs preauth)
					ClaimProcsForClaim[i].WriteOff=ClaimProcs.GetWriteOffEstimate(ClaimProcsForClaim[i]);
					writeoff+=ClaimProcsForClaim[i].WriteOff;
					/*
					ClaimProcsForClaim[i].WriteOff=0;
					if(claimCur.ClaimType=="P" && plan.PlanType=="p") {//Primary && PPO
						double insplanAllowed=Fees.GetAmount(ProcCur.CodeNum,plan.FeeSched);
						if(insplanAllowed!=-1) {
							ClaimProcsForClaim[i].WriteOff=ProcCur.ProcFee-insplanAllowed;
						}
						//else, if -1 fee not found, then do not show a writeoff. User can change writeoff if they disagree.
					}
					writeoff+=ClaimProcsForClaim[i].WriteOff;*/
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