using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;

using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ProcedureL{
		///<summary>Loops through each proc. Does not add notes to a procedure that already has notes. Used twice, security checked in both places before calling this.  Also sets provider for each proc.</summary>
		public static void SetCompleteInAppt(Appointment apt,List<InsPlan> PlanList,List<PatPlan> patPlans,int siteNum) {
			Procedure[] ProcList=Procedures.Refresh(apt.PatNum);
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(apt.PatNum);
			Benefit[] benefitList=Benefits.Refresh(patPlans);
			//this query could be improved slightly to only get notes of interest.
			string command="SELECT * FROM procnote WHERE PatNum="+POut.PInt(apt.PatNum)+" ORDER BY EntryDateTime";
			DataTable rawNotes=Db.GetTable(command);
			//CovPats.Refresh(PlanList,patPlans);
			//bool doResetRecallStatus=false;
			ProcedureCode procCode;
			Procedure oldProc;
			//int siteNum=0;
			//if(!PrefC.GetBool("EasyHidePublicHealth")){
			//	siteNum=Patients.GetPat(apt.PatNum).SiteNum;
			//}
			for(int i=0;i<ProcList.Length;i++) {
				if(ProcList[i].AptNum!=apt.AptNum) {
					continue;
				}
				//attach the note, if it exists.
				for(int n=rawNotes.Rows.Count-1;n>=0;n--) {//loop through each note, backwards.
					if(ProcList[i].ProcNum.ToString()!=rawNotes.Rows[n]["ProcNum"].ToString()) {
						continue;
					}
					ProcList[i].UserNum=PIn.PInt(rawNotes.Rows[n]["UserNum"].ToString());
					ProcList[i].Note=PIn.PString(rawNotes.Rows[n]["Note"].ToString());
					ProcList[i].SigIsTopaz=PIn.PBool(rawNotes.Rows[n]["SigIsTopaz"].ToString());
					ProcList[i].Signature=PIn.PString(rawNotes.Rows[n]["Signature"].ToString());
					break;//out of note loop.
				}
				oldProc=ProcList[i].Copy();
				procCode=ProcedureCodes.GetProcCode(ProcList[i].CodeNum);
				if(procCode.PaintType==ToothPaintingType.Extraction) {//if an extraction, then mark previous procs hidden
					//SetHideGraphical(ProcList[i]);//might not matter anymore
					ToothInitials.SetValue(apt.PatNum,ProcList[i].ToothNum,ToothInitialType.Missing);
				}
				ProcList[i].ProcStatus=ProcStat.C;
				ProcList[i].ProcDate=apt.AptDateTime.Date;
				if(oldProc.ProcStatus!=ProcStat.C) {
					ProcList[i].DateEntryC=DateTime.Now;//this triggers it to set to server time NOW().
				}
				ProcList[i].PlaceService=(PlaceOfService)PrefC.GetInt("DefaultProcedurePlaceService");
				ProcList[i].ClinicNum=apt.ClinicNum;
				ProcList[i].SiteNum=siteNum;
				ProcList[i].PlaceService=Clinics.GetPlaceService(apt.ClinicNum);
				if(apt.ProvHyg!=0) {//if the appointment has a hygiene provider
					if(procCode.IsHygiene) {//hyg proc
						ProcList[i].ProvNum=apt.ProvHyg;
					} else {//regular proc
						ProcList[i].ProvNum=apt.ProvNum;
					}
				} else {//same provider for every procedure
					ProcList[i].ProvNum=apt.ProvNum;
				}
				//if procedure was already complete, then don't add more notes.
				if(oldProc.ProcStatus!=ProcStat.C) {
					ProcList[i].Note+=ProcCodeNotes.GetNote(ProcList[i].ProvNum,ProcList[i].CodeNum);
				}
				Procedures.Update(ProcList[i],oldProc);
				ComputeEstimates(ProcList[i],apt.PatNum,ClaimProcList,false,PlanList,patPlans,benefitList);
			}
			//if(doResetRecallStatus){
			//	Recalls.Reset(apt.PatNum);//this also synchs recall
			//}
			Recalls.Synch(apt.PatNum);
			Patient pt=Patients.GetPat(apt.PatNum);
			Reporting.Allocators.AllocatorCollection.CallAll_Allocators(pt.Guarantor);
		}

		///<summary>Used whenever a procedure changes or a plan changes.  All estimates for a given procedure must be updated. This frequently includes adding claimprocs, but can also just edit the appropriate existing claimprocs. Skips status=Adjustment,CapClaim,Preauth,Supplemental.  Also fixes date,status,and provnum if appropriate.  The claimProc array can be all claimProcs for the patient, but must at least include all claimprocs for this proc.  Only set IsInitialEntry true from Chart module; this is for cap procs.</summary>
		public static void ComputeEstimates(Procedure proc,int patNum,ClaimProc[] claimProcs,bool IsInitialEntry,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benefitList) {
			bool doCreate=true;
			if(proc.ProcDate<DateTime.Today&&proc.ProcStatus==ProcStat.C) {
				//don't automatically create an estimate for completed procedures
				//especially if they are older than today
				//Very important after a conversion from another software.
				//This may need to be relaxed a little for offices that enter treatment a few days after it's done.
				doCreate=false;
			}
			//first test to see if each estimate matches an existing patPlan (current coverage),
			//delete any other estimates
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				if(claimProcs[i].PlanNum==0) {
					continue;
				}
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					||claimProcs[i].Status==ClaimProcStatus.Preauth
					||claimProcs[i].Status==ClaimProcStatus.Supplemental) {
					continue;
					//ignored: adjustment
					//included: capComplete,CapEstimate,Estimate,NotReceived,Received
				}
				if(claimProcs[i].Status!=ClaimProcStatus.Estimate&&claimProcs[i].Status!=ClaimProcStatus.CapEstimate) {
					continue;
				}
				bool planIsCurrent=false;
				for(int p=0;p<patPlans.Length;p++) {
					if(patPlans[p].PlanNum==claimProcs[i].PlanNum) {
						planIsCurrent=true;
						break;
					}
				}
				//If claimProc estimate is for a plan that is not current, delete it
				if(!planIsCurrent) {
					ClaimProcs.Delete(claimProcs[i]);
				}
			}
			InsPlan PlanCur;
			bool estExists;
			bool cpAdded=false;
			//loop through all patPlans (current coverage), and add any missing estimates
			for(int p=0;p<patPlans.Length;p++) {//typically, loop will only have length of 1 or 2
				if(!doCreate) {
					break;
				}
				//test to see if estimate exists
				estExists=false;
				for(int i=0;i<claimProcs.Length;i++) {
					if(claimProcs[i].ProcNum!=proc.ProcNum) {
						continue;
					}
					if(claimProcs[i].PlanNum==0) {
						continue;
					}
					if(claimProcs[i].Status==ClaimProcStatus.CapClaim
						||claimProcs[i].Status==ClaimProcStatus.Preauth
						||claimProcs[i].Status==ClaimProcStatus.Supplemental) {
						continue;
						//ignored: adjustment
						//included: capComplete,CapEstimate,Estimate,NotReceived,Received
					}
					if(patPlans[p].PlanNum!=claimProcs[i].PlanNum) {
						continue;
					}
					estExists=true;
					break;
				}
				if(estExists) {
					continue;
				}
				//estimate is missing, so add it.
				ClaimProc cp=new ClaimProc();
				cp.ProcNum=proc.ProcNum;
				cp.PatNum=patNum;
				cp.ProvNum=proc.ProvNum;
				PlanCur=InsPlans.GetPlan(patPlans[p].PlanNum,PlanList);
				if(PlanCur==null) {
					continue;//??
				}
				if(PlanCur.PlanType=="c") {
					if(proc.ProcStatus==ProcStat.C) {
						cp.Status=ClaimProcStatus.CapComplete;
					} else {
						cp.Status=ClaimProcStatus.CapEstimate;//this may be changed below
					}
				} else {
					cp.Status=ClaimProcStatus.Estimate;
				}
				cp.PlanNum=PlanCur.PlanNum;
				cp.DateCP=proc.ProcDate;
				cp.AllowedOverride=-1;
				cp.PercentOverride=-1;
				cp.OverrideInsEst=-1;
				cp.NoBillIns=ProcedureCodes.GetProcCode(proc.CodeNum).NoBillIns;
				cp.OverAnnualMax=-1;
				cp.PaidOtherIns=-1;
				cp.CopayOverride=-1;
				cp.ProcDate=proc.ProcDate;
				//ComputeBaseEst will fill AllowedOverride,Percentage,CopayAmt,BaseEst
				ClaimProcs.Insert(cp);
				cpAdded=true;
			}
			//if any were added, refresh the list
			if(cpAdded) {
				claimProcs=ClaimProcs.Refresh(patNum);
			}
			for(int i=0;i<claimProcs.Length;i++) {
				if(claimProcs[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				//This was a longstanding bug. I hope there are not other consequences for commenting it out.
				//claimProcs[i].DateCP=proc.ProcDate;
				claimProcs[i].ProcDate=proc.ProcDate;
				//capitation estimates are always forced to follow the status of the procedure
				PlanCur=InsPlans.GetPlan(claimProcs[i].PlanNum,PlanList);
				if(PlanCur!=null
					&&PlanCur.PlanType=="c"
					&&(claimProcs[i].Status==ClaimProcStatus.CapComplete
					||claimProcs[i].Status==ClaimProcStatus.CapEstimate)) {
					if(IsInitialEntry) {
						//this will be switched to CapComplete further down if applicable.
						//This makes ComputeBaseEst work properly on new cap procs w status Complete
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					} else if(proc.ProcStatus==ProcStat.C) {
						claimProcs[i].Status=ClaimProcStatus.CapComplete;
					} else {
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
				}
				//ignored: adjustment
				//ComputeBaseEst automatically skips: capComplete,Preauth,capClaim,Supplemental
				//does recalc est on: CapEstimate,Estimate,NotReceived,Received
				if(claimProcs[i].PlanNum>0&&PatPlans.GetPlanNum(patPlans,1)==claimProcs[i].PlanNum) {
					ClaimProcL.ComputeBaseEst(claimProcs[i],proc,PriSecTot.Pri,PlanList,patPlans,benefitList);
				}
				if(claimProcs[i].PlanNum>0&&PatPlans.GetPlanNum(patPlans,2)==claimProcs[i].PlanNum) {
					ClaimProcL.ComputeBaseEst(claimProcs[i],proc,PriSecTot.Sec,PlanList,patPlans,benefitList);
				}
				if(IsInitialEntry
					&&claimProcs[i].Status==ClaimProcStatus.CapEstimate
					&&proc.ProcStatus==ProcStat.C) {
					claimProcs[i].Status=ClaimProcStatus.CapComplete;
				}
				//prov only updated if still an estimate
				if(claimProcs[i].Status==ClaimProcStatus.Estimate
					||claimProcs[i].Status==ClaimProcStatus.CapEstimate) {
					claimProcs[i].ProvNum=proc.ProvNum;
				}
				ClaimProcs.Update(claimProcs[i]);
			}
		}

		///<summary>After changing important coverage plan info, this is called to recompute estimates for all procedures for this patient.</summary>
		public static void ComputeEstimatesForAll(int patNum,ClaimProc[] claimProcs,Procedure[] procs,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benefitList) {
			for(int i=0;i<procs.Length;i++) {
				ComputeEstimates(procs[i],patNum,claimProcs,false,PlanList,patPlans,benefitList);
			}
		}

	}

}