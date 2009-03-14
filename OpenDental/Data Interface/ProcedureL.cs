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
		public static void SetCompleteInAppt(Appointment apt,InsPlan[] PlanList,PatPlan[] patPlans,int siteNum) {
			Procedure[] ProcList=Procedures.Refresh(apt.PatNum);
			ClaimProc[] ClaimProcList=ClaimProcs.Refresh(apt.PatNum);
			Benefit[] benefitList=Benefits.Refresh(patPlans);
			//this query could be improved slightly to only get notes of interest.
			string command="SELECT * FROM procnote WHERE PatNum="+POut.PInt(apt.PatNum)+" ORDER BY EntryDateTime";
			DataTable rawNotes=General.GetTable(command);
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
					if(ProcList[i].ProcNum.ToString() != rawNotes.Rows[n]["ProcNum"].ToString()) {
						continue;
					}
					ProcList[i].UserNum   =PIn.PInt(rawNotes.Rows[n]["UserNum"].ToString());
					ProcList[i].Note      =PIn.PString(rawNotes.Rows[n]["Note"].ToString());
					ProcList[i].SigIsTopaz=PIn.PBool(rawNotes.Rows[n]["SigIsTopaz"].ToString());
					ProcList[i].Signature =PIn.PString(rawNotes.Rows[n]["Signature"].ToString());
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
					}
					else {//regular proc
						ProcList[i].ProvNum=apt.ProvNum;
					}
				}
				else {//same provider for every procedure
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
			Patient pt = Patients.GetPat(apt.PatNum);
			Reporting.Allocators.AllocatorCollection.CallAll_Allocators(pt.Guarantor);
		}

		/// <summary>Used by GetProcsForSingle and GetProcsMultApts to generate a short string description of a procedure.</summary>
		public static string ConvertProcToString(int codeNum,string surf,string toothNum) {
			string strLine="";
			ProcedureCode code=ProcedureCodes.GetProcCode(codeNum);
			switch(code.TreatArea) {
				case TreatmentArea.Surf:
					strLine+="#"+Tooth.ToInternat(toothNum)+"-"+surf+"-";//""#12-MOD-"
					break;
				case TreatmentArea.Tooth:
					strLine+="#"+Tooth.ToInternat(toothNum)+"-";//"#12-"
					break;
				default://area 3 or 0 (mouth)
					break;
				case TreatmentArea.Quad:
					strLine+=surf+"-";//"UL-"
					break;
				case TreatmentArea.Sextant:
					strLine+="S"+surf+"-";//"S2-"
					break;
				case TreatmentArea.Arch:
					strLine+=surf+"-";//"U-"
					break;
				case TreatmentArea.ToothRange:
					//strLine+=table.Rows[j][13].ToString()+" ";//don't show range
					break;
			}//end switch
			strLine+=code.AbbrDesc;
			return strLine;
		}

		///<summary>Used do display procedure descriptions on appointments. The returned string also includes surf and toothNum.</summary>
		public static string GetDescription(Procedure proc) {
			return ConvertProcToString(proc.CodeNum,proc.Surf,proc.ToothNum);
		}

		///<Summary>Supply the list of procedures attached to the appointment.  It will loop through each and assign the correct provider.  Also sets clinic.</Summary>
		public static void SetProvidersInAppointment(Appointment apt,List<Procedure> procList) {
			ProcedureCode procCode;
			Procedure changedProc;
			for(int i=0;i<procList.Count;i++) {
				changedProc=procList[i].Copy();
				if(apt.ProvHyg!=0) {//if the appointment has a hygiene provider
					procCode=ProcedureCodes.GetProcCode(procList[i].CodeNum);
					if(procCode.IsHygiene) {//hygiene proc
						changedProc.ProvNum=apt.ProvHyg;
					}
					else {//dentist proc
						changedProc.ProvNum=apt.ProvNum;
					}
				}
				else {//same provider for every procedure
					changedProc.ProvNum=apt.ProvNum;
				}
				changedProc.ClinicNum=apt.ClinicNum;
				Procedures.Update(changedProc,procList[i]);//won't go to db unless a field has changed.
			}
		}

		///<summary>Gets a list of procedures representing extracted teeth.  Status of C,EC,orEO. Includes procs with toothNum "1"-"32".  Will not include procs with unreasonable dates.  Used for Canadian e-claims instead of the usual ToothInitials.GetMissingOrHiddenTeeth, because Canada requires dates on the extracted teeth.  Supply all procedures for the patient.</summary>
		public static List<Procedure> GetExtractedTeeth(Procedure[] procList) {
			List<Procedure> extracted=new List<Procedure>();
			ProcedureCode procCode;
			for(int i=0;i<procList.Length;i++) {
				if(procList[i].ProcStatus!=ProcStat.C && procList[i].ProcStatus!=ProcStat.EC && procList[i].ProcStatus!=ProcStat.EO) {
					continue;
				}
				if(!Tooth.IsValidDB(procList[i].ToothNum)) {
					continue;
				}
				if(Tooth.IsSuperNum(procList[i].ToothNum)) {
					continue;
				}
				if(Tooth.IsPrimary(procList[i].ToothNum)) {
					continue;
				}
				if(procList[i].ProcDate.Year<1880 || procList[i].ProcDate>DateTime.Today) {
					continue;
				}
				procCode=ProcedureCodes.GetProcCode(procList[i].CodeNum);
				if(procCode.TreatArea!=TreatmentArea.Tooth) {
					continue;
				}
				if(procCode.PaintType!=ToothPaintingType.Extraction) {
					continue;
				}
				extracted.Add(procList[i].Copy());
			}
			return extracted;
		}

		///<summary>Base estimate or override is retrieved from supplied claimprocs. Does not take into consideration annual max or deductible.  If limitToTotal set to true, then it does limit total of pri+sec to not be more than total fee.  The claimProc array typically includes all claimProcs for the patient, but must at least include all claimprocs for this proc.</summary>
		public static double GetEst(Procedure proc,ClaimProc[] claimProcs,PriSecTot pst,PatPlan[] patPlans,bool limitToTotal) {
			double priBaseEst=0;
			double secBaseEst=0;
			double priOverride=-1;
			double secOverride=-1;
			for(int i=0;i<claimProcs.Length;i++) {
				//adjustments automatically ignored since no ProcNum
				if(claimProcs[i].Status==ClaimProcStatus.CapClaim
					|| claimProcs[i].Status==ClaimProcStatus.Preauth
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental) {
					continue;
				}
				if(claimProcs[i].ProcNum==proc.ProcNum) {
					if(PatPlans.GetPlanNum(patPlans,1)==claimProcs[i].PlanNum) {
						//if this is a Cap, then this will still work. Est comes out 0.
						priBaseEst=claimProcs[i].BaseEst;
						priOverride=claimProcs[i].OverrideInsEst;
					}
					else if(PatPlans.GetPlanNum(patPlans,2)==claimProcs[i].PlanNum) {
						secBaseEst=claimProcs[i].BaseEst;
						secOverride=claimProcs[i].OverrideInsEst;
					}
				}
			}
			if(priOverride!=-1) {
				priBaseEst=priOverride;
			}
			if(secOverride!=-1) {
				secBaseEst=secOverride;
			}
			if(limitToTotal && proc.ProcFee-priBaseEst-secBaseEst < 0) {
				secBaseEst=proc.ProcFee-priBaseEst;
			}
			switch(pst) {
				case PriSecTot.Pri:
					return priBaseEst;
				case PriSecTot.Sec:
					return secBaseEst;
				case PriSecTot.Tot:
					return priBaseEst+secBaseEst;
			}
			return 0;
		}

		///<summary>Used whenever a procedure changes or a plan changes.  All estimates for a given procedure must be updated. This frequently includes adding claimprocs, but can also just edit the appropriate existing claimprocs. Skips status=Adjustment,CapClaim,Preauth,Supplemental.  Also fixes date,status,and provnum if appropriate.  The claimProc array can be all claimProcs for the patient, but must at least include all claimprocs for this proc.  Only set IsInitialEntry true from Chart module; this is for cap procs.</summary>
		public static void ComputeEstimates(Procedure proc,int patNum,ClaimProc[] claimProcs,bool IsInitialEntry,InsPlan[] PlanList,PatPlan[] patPlans,Benefit[] benefitList) {
			bool doCreate=true;
			if(proc.ProcDate<DateTime.Today && proc.ProcStatus==ProcStat.C) {
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
					|| claimProcs[i].Status==ClaimProcStatus.Preauth
					|| claimProcs[i].Status==ClaimProcStatus.Supplemental) {
					continue;
					//ignored: adjustment
					//included: capComplete,CapEstimate,Estimate,NotReceived,Received
				}
				if(claimProcs[i].Status!=ClaimProcStatus.Estimate && claimProcs[i].Status!=ClaimProcStatus.CapEstimate) {
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
						|| claimProcs[i].Status==ClaimProcStatus.Preauth
						|| claimProcs[i].Status==ClaimProcStatus.Supplemental) {
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
					}
					else {
						cp.Status=ClaimProcStatus.CapEstimate;//this may be changed below
					}
				}
				else {
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
					&& PlanCur.PlanType=="c"
					&& (claimProcs[i].Status==ClaimProcStatus.CapComplete
					|| claimProcs[i].Status==ClaimProcStatus.CapEstimate)) {
					if(IsInitialEntry) {
						//this will be switched to CapComplete further down if applicable.
						//This makes ComputeBaseEst work properly on new cap procs w status Complete
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
					else if(proc.ProcStatus==ProcStat.C) {
						claimProcs[i].Status=ClaimProcStatus.CapComplete;
					}
					else {
						claimProcs[i].Status=ClaimProcStatus.CapEstimate;
					}
				}
				//ignored: adjustment
				//ComputeBaseEst automatically skips: capComplete,Preauth,capClaim,Supplemental
				//does recalc est on: CapEstimate,Estimate,NotReceived,Received
				if(claimProcs[i].PlanNum>0 && PatPlans.GetPlanNum(patPlans,1)==claimProcs[i].PlanNum) {
					ClaimProcs.ComputeBaseEst(claimProcs[i],proc,PriSecTot.Pri,PlanList,patPlans,benefitList);
				}
				if(claimProcs[i].PlanNum>0 && PatPlans.GetPlanNum(patPlans,2)==claimProcs[i].PlanNum) {
					ClaimProcs.ComputeBaseEst(claimProcs[i],proc,PriSecTot.Sec,PlanList,patPlans,benefitList);
				}
				if(IsInitialEntry
					&& claimProcs[i].Status==ClaimProcStatus.CapEstimate
					&& proc.ProcStatus==ProcStat.C) {
					claimProcs[i].Status=ClaimProcStatus.CapComplete;
				}
				//prov only updated if still an estimate
				if(claimProcs[i].Status==ClaimProcStatus.Estimate
					|| claimProcs[i].Status==ClaimProcStatus.CapEstimate) {
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

		///<summary>Only fees, not estimates.  Returns number of fees changed.</summary>
		public static int GlobalUpdateFees() {
			string command=@"SELECT procedurecode.CodeNum,ProcNum,patient.PatNum,procedurelog.PatNum,
				insplan.FeeSched AS PlanFeeSched,patient.FeeSched AS PatFeeSched,patient.PriProv,
				procedurelog.ProcFee,insplan.PlanType
				FROM procedurelog
				LEFT JOIN patient ON patient.PatNum=procedurelog.PatNum
				LEFT JOIN patplan ON patplan.PatNum=procedurelog.PatNum
				AND patplan.Ordinal=1
				LEFT JOIN procedurecode ON procedurecode.CodeNum=procedurelog.CodeNum
				LEFT JOIN insplan ON insplan.PlanNum=patplan.PlanNum
				WHERE procedurelog.ProcStatus=1";
			/*@"SELECT procedurelog.ProcCode,insplan.FeeSched AS PlanFeeSched,patient.FeeSched AS PatFeeSched,
							patient.PriProv,ProcNum
							FROM procedurelog,patient
							LEFT JOIN patplan ON patplan.PatNum=procedurelog.PatNum
							AND patplan.Ordinal=1
							LEFT JOIN insplan ON insplan.PlanNum=patplan.PlanNum
							WHERE procedurelog.ProcStatus=1
							AND patient.PatNum=procedurelog.PatNum
						";*/
			DataTable table=General.GetTable(command);
			int priPlanFeeSched;
			//int feeSchedNum;
			int patFeeSched;
			int patProv;
			string planType;
			double insfee;
			double standardfee;
			double newFee;
			double oldFee;
			int rowsChanged=0;
			for(int i=0;i<table.Rows.Count;i++) {
				priPlanFeeSched=PIn.PInt(table.Rows[i]["PlanFeeSched"].ToString());
				patFeeSched=PIn.PInt(table.Rows[i]["PatFeeSched"].ToString());
				patProv=PIn.PInt(table.Rows[i]["PriProv"].ToString());
				planType=PIn.PString(table.Rows[i]["PlanType"].ToString());
				insfee=Fees.GetAmount0(PIn.PInt(table.Rows[i]["CodeNum"].ToString()),Fees.GetFeeSched(priPlanFeeSched,patFeeSched,patProv));
				if(planType=="p") {//PPO
					standardfee=Fees.GetAmount0(PIn.PInt(table.Rows[i]["CodeNum"].ToString()),Providers.GetProv(patProv).FeeSched);
					if(standardfee>insfee) {
						newFee=standardfee;
					}
					else {
						newFee=insfee;
					}
				}
				else {
					newFee=insfee;
				}
				oldFee=PIn.PDouble(table.Rows[i]["ProcFee"].ToString());
				if(newFee==oldFee) {
					continue;
				}
				command="UPDATE procedurelog SET ProcFee='"+POut.PDouble(newFee)+"' "
					+"WHERE ProcNum="+table.Rows[i]["ProcNum"].ToString();
				rowsChanged+=General.NonQ(command);
			}
			return rowsChanged;
		}

		///<summary>Used from TP to get a list of all TP procs, ordered by priority, toothnum.</summary>
		public static Procedure[] GetListTP(Procedure[] procList) {
			ArrayList AL=new ArrayList();
			for(int i=0;i<procList.Length;i++) {
				if(procList[i].ProcStatus==ProcStat.TP) {
					AL.Add(procList[i]);
				}
			}
			IComparer myComparer=new ProcedureComparer();
			AL.Sort(myComparer);
			Procedure[] retVal=new Procedure[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}






	}

	/*================================================================================================================
	=========================================== class ProcedureComparer =============================================*/

	///<summary>This sorts procedures based on priority, then tooth number, then code (but if Canadian lab code, uses proc code here instead of lab code).  Finally, if comparing a proc and its Canadian lab code, it puts the lab code after the proc.  It does not care about dates or status.  Currently used in TP module and Chart module sorting.</summary>
	public class ProcedureComparer:IComparer {
		///<summary>This sorts procedures based on priority, then tooth number.  It does not care about dates or status.  Currently used in TP module and Chart module sorting.</summary>
		int IComparer.Compare(Object objx,Object objy) {
			Procedure x=(Procedure)objx;
			Procedure y=(Procedure)objy;
			//first, by priority
			if(x.Priority!=y.Priority) {//if priorities are different
				if(x.Priority==0) {
					return 1;//x is greater than y. Priorities always come first.
				}
				if(y.Priority==0) {
					return -1;//x is less than y. Priorities always come first.
				}
				return DefC.GetOrder(DefCat.TxPriorities,x.Priority).CompareTo(DefC.GetOrder(DefCat.TxPriorities,y.Priority));
			}
			//priorities are the same, so sort by toothrange
			if(x.ToothRange != y.ToothRange) {
				//empty toothranges come before filled toothrange values
				return x.ToothRange.CompareTo(y.ToothRange);
			}
			//toothranges are the same (usually empty), so compare toothnumbers
			if(x.ToothNum != y.ToothNum) {
				//this also puts invalid or empty toothnumbers before the others.
				return Tooth.ToInt(x.ToothNum).CompareTo(Tooth.ToInt(y.ToothNum));
			}
			//priority and toothnums are the same, so sort by code.
			/*string adaX=x.Code;
			if(x.ProcNumLab !=0){//if x is a Canadian lab proc
				//then use the Code of the procedure instead of the lab code
				adaX=Procedures.GetOneProc(
			}
			string adaY=y.Code;*/
			return ProcedureCodes.GetStringProcCode(x.CodeNum).CompareTo(ProcedureCodes.GetStringProcCode(y.CodeNum));
			//return x.Code.CompareTo(y.Code);
			//return 0;//priority, tooth number, and code are all the same
		}
	}
}










