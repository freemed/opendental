using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using OpenDental;

namespace UnitTests {
	public class ClaimT {
		/// <summary>claimType="P" or "S".</summary>
		public static Claim CreateClaim(string claimType,List<PatPlan> PatPlanList,List<InsPlan> InsPlanList,List<ClaimProc> ClaimProcList,List<Procedure> procsForPat,Patient pat,List<Procedure> procsForClaim,List<Benefit> benefitList,List<InsSub> SubList){
			//Claim ClaimCur=CreateClaim("P",PatPlanList,InsPlanList,ClaimProcList,procsForPat);
			long claimFormNum = 0;
			EtransType eFormat = 0;
			InsPlan PlanCur=new InsPlan();
			InsSub SubCur=new InsSub();
			Relat relatOther=Relat.Self;
			switch(claimType) {
				case "P":
					PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,1),InsPlanList);
					SubCur=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,1),SubList);
					break;
				case "S":
					PlanCur=InsPlans.GetPlan(PatPlans.GetPlanNum(PatPlanList,2),InsPlanList);
					SubCur=InsSubs.GetSub(PatPlans.GetInsSubNum(PatPlanList,2),SubList);
					break;
			}
			//DataTable table=DataSetMain.Tables["account"];
			Procedure proc;
			//proc=Procedures.GetProcFromList(procsForPat,PIn.Long(table.Rows[gridAccount.SelectedIndices[0]]["ProcNum"].ToString()));
			//long clinicNum=proc.ClinicNum;
			ClaimProc[] claimProcs=new ClaimProc[procsForClaim.Count];//1:1 with procs
			long procNum;
			for(int i=0;i<procsForClaim.Count;i++) {//loop through selected procs
				//and try to find an estimate that can be used
				procNum=procsForClaim[i].ProcNum;
				claimProcs[i]=Procedures.GetClaimProcEstimate(procNum,ClaimProcList,PlanCur);
			}
			for(int i=0;i<claimProcs.Length;i++) {//loop through each claimProc
				//and create any missing estimates. This handles claims to 3rd and 4th ins co's.
				if(claimProcs[i]==null) {
					claimProcs[i]=new ClaimProc();
					proc=procsForClaim[i];
					ClaimProcs.CreateEst(claimProcs[i],proc,PlanCur,SubCur);
				}
			}
			Claim claim=new Claim();
			Claims.Insert(claim);//to retreive a key for new Claim.ClaimNum
			claim.PatNum=pat.PatNum;
			claim.DateService=claimProcs[claimProcs.Length-1].ProcDate;
			claim.ClinicNum=procsForClaim[0].ClinicNum;
			claim.DateSent=DateTime.Today;
			claim.ClaimStatus="S";
			//datereceived
			switch(claimType) {
				case "P":
					claim.PlanNum=PatPlans.GetPlanNum(PatPlanList,1);
					claim.InsSubNum=PatPlans.GetInsSubNum(PatPlanList,1);
					claim.PatRelat=PatPlans.GetRelat(PatPlanList,1);
					claim.ClaimType="P";
					claim.PlanNum2=PatPlans.GetPlanNum(PatPlanList,2);//might be 0 if no sec ins
					claim.InsSubNum2=PatPlans.GetInsSubNum(PatPlanList,2);
					claim.PatRelat2=PatPlans.GetRelat(PatPlanList,2);
					break;
				case "S":
					claim.PlanNum=PatPlans.GetPlanNum(PatPlanList,2);
					claim.InsSubNum=PatPlans.GetInsSubNum(PatPlanList,2);
					claim.PatRelat=PatPlans.GetRelat(PatPlanList,2);
					claim.ClaimType="S";
					claim.PlanNum2=PatPlans.GetPlanNum(PatPlanList,1);
					claim.InsSubNum2=PatPlans.GetInsSubNum(PatPlanList,1);
					claim.PatRelat2=PatPlans.GetRelat(PatPlanList,1);
					break;
			}
			claim.ProvTreat=procsForClaim[0].ProvNum;
			claim.IsProsthesis="I";
			claim.ProvBill=Providers.GetBillingProvNum(claim.ProvTreat,claim.ClinicNum);
			claim.EmployRelated=YN.No;
			//attach procedures
			Procedure ProcCur;
			for(int i=0;i<claimProcs.Length;i++) {
				ProcCur=procsForClaim[i];
				claimProcs[i].ClaimNum=claim.ClaimNum;
				claimProcs[i].Status=ClaimProcStatus.NotReceived;//status for claims unsent or sent.
				//writeoff handled in ClaimL.CalculateAndUpdate()
				claimProcs[i].CodeSent=ProcedureCodes.GetProcCode(ProcCur.CodeNum).ProcCode;
				if(claimProcs[i].CodeSent.Length>5 && claimProcs[i].CodeSent.Substring(0,1)=="D") {
					claimProcs[i].CodeSent=claimProcs[i].CodeSent.Substring(0,5);
				}
				claimProcs[i].LineNumber=(byte)(i+1);
				ClaimProcs.Update(claimProcs[i]);
			}
			ClaimProcList=ClaimProcs.Refresh(pat.PatNum);
			ClaimL.CalculateAndUpdate(procsForPat,InsPlanList,claim,PatPlanList,benefitList,pat.Age,SubList);
			return claim;
		}




	}
}
