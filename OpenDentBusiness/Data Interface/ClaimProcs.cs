using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimProcs{

		///<summary></summary>
		public static List<ClaimProc> Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProc>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * from claimproc "
				+"WHERE PatNum = '"+patNum.ToString()+"' ORDER BY LineNumber";
			return Crud.ClaimProcCrud.SelectMany(command);
		}

		///<summary>When using family deduct or max, this gets all claimprocs for the given plan.  This info is needed to compute used and pending insurance.</summary>
		public static List<ClaimProc> RefreshFam(long planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProc>>(MethodBase.GetCurrentMethod(),planNum);
			}
			string command=
				"SELECT * FROM claimproc "
				+"WHERE PlanNum = "+POut.Long(planNum);
				//+" OR PatPlanNum = "+POut.PInt(patPlanNum);
			return Crud.ClaimProcCrud.SelectMany(command);
		}

		///<summary>Gets a list of ClaimProcs for one claim.</summary>
		public static List<ClaimProc> RefreshForClaim(long claimNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProc>>(MethodBase.GetCurrentMethod(),claimNum);
			}
			string command=
				"SELECT * FROM claimproc "
				+"WHERE ClaimNum = "+POut.Long(claimNum)+" ORDER BY LineNumber";
			return Crud.ClaimProcCrud.SelectMany(command);
		}

		///<summary>Gets a list of ClaimProcs with status of estimate.</summary>
		public static List<ClaimProc> RefreshForTP(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProc>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM claimproc "
				+"WHERE (Status="+POut.Long((int)ClaimProcStatus.Estimate)
				+" OR Status="+POut.Long((int)ClaimProcStatus.CapEstimate)+") "
				+"AND PatNum = "+POut.Long(patNum);
			return Crud.ClaimProcCrud.SelectMany(command);
		}

		///<summary>Gets a list of ClaimProcs for one proc.</summary>
		public static List<ClaimProc> RefreshForProc(long procNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProc>>(MethodBase.GetCurrentMethod(),procNum);
			}
			string command=
				"SELECT * FROM claimproc "
				+"WHERE ProcNum="+POut.Long(procNum);
			return Crud.ClaimProcCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(ClaimProc cp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				cp.ClaimProcNum=Meth.GetLong(MethodBase.GetCurrentMethod(),cp);
				return cp.ClaimProcNum;
			}
			return Crud.ClaimProcCrud.Insert(cp);
		}

		///<summary></summary>
		public static void Update(ClaimProc cp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cp);
				return;
			}
			Crud.ClaimProcCrud.Update(cp);
		}

		///<summary></summary>
		public static void Delete(ClaimProc cp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cp);
				return;
			}
			string command= "DELETE FROM claimproc WHERE ClaimProcNum = "+POut.Long(cp.ClaimProcNum);
			Db.NonQ(command);
		}

		///<summary>Used when creating a claim to create any missing claimProcs. Also used in FormProcEdit if click button to add Estimate.  Inserts it into db. It will still be altered after this to fill in the fields that actually attach it to the claim.</summary>
		public static void CreateEst(ClaimProc cp, Procedure proc, InsPlan plan,InsSub sub) {
			//No need to check RemotingRole; no call to db.
			cp.ProcNum=proc.ProcNum;
			//claimnum
			cp.PatNum=proc.PatNum;
			cp.ProvNum=proc.ProvNum;
			if(plan.PlanType=="c") {//capitation
				if(proc.ProcStatus==ProcStat.C) {//complete
					cp.Status=ClaimProcStatus.CapComplete;//in this case, a copy will be made later.
				}
				else {//usually TP status
					cp.Status=ClaimProcStatus.CapEstimate;
				}
			}
			else {
				cp.Status=ClaimProcStatus.Estimate;
			}
			cp.PlanNum=plan.PlanNum;
			cp.InsSubNum=sub.InsSubNum;
			cp.DateCP=proc.ProcDate;
			//Writeoff=0
			cp.AllowedOverride=-1;
			cp.Percentage=-1;
			cp.PercentOverride=-1;
			cp.CopayAmt=-1;
			cp.NoBillIns=false;
			cp.PaidOtherIns=-1;
			cp.BaseEst=0;
			cp.DedEst=-1;
			cp.DedEstOverride=-1;
			cp.InsEstTotal=0;
			cp.InsEstTotalOverride=-1;
			cp.CopayOverride=-1;
			cp.PaidOtherInsOverride=-1;
			cp.ProcDate=proc.ProcDate;
			cp.WriteOffEst=-1;
			cp.WriteOffEstOverride=-1;
			cp.ClinicNum=proc.ClinicNum;
			Insert(cp);
		}

		///<summary>This compares the two lists and saves all the changes to the database.  It also removes all the items marked doDelete.</summary>
		public static void Synch(ref List<ClaimProc> ClaimProcList,List<ClaimProc> claimProcListOld) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<ClaimProcList.Count;i++) {
				if(ClaimProcList[i].DoDelete) {
					ClaimProcs.Delete(ClaimProcList[i]);
					continue;
				}
				//new procs
				if(i>=claimProcListOld.Count) {
					ClaimProcs.Insert(ClaimProcList[i]);//this should properly update the ClaimProcNum
					continue;
				}
				//changed procs
				if(!ClaimProcList[i].Equals(claimProcListOld[i])) {
					ClaimProcs.Update(ClaimProcList[i]);
				}
			}
			//go backwards to actually remove the deleted items.
			for(int i=ClaimProcList.Count-1;i>=0;i--) {
				if(ClaimProcList[i].DoDelete) {
					ClaimProcList.RemoveAt(i);
				}
			}
		}

		///<summary>When sending or printing a claim, this converts the supplied list into a list of ClaimProcs that need to be sent.</summary>
		public static List<ClaimProc> GetForSendClaim(List<ClaimProc> claimProcList,long claimNum) {
			//No need to check RemotingRole; no call to db.
			//MessageBox.Show(List.Length.ToString());
			List<ClaimProc> retVal=new List<ClaimProc>();
			bool includeThis;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ClaimNum!=claimNum) {
					continue;
				}
				if(claimProcList[i].ProcNum==0) {
					continue;//skip payments
				}
				includeThis=true;
				for(int j=0;j<retVal.Count;j++){//loop through existing claimprocs
					if(retVal[j].ProcNum==claimProcList[i].ProcNum) {
						includeThis=false;//skip duplicate procedures
					}
				}
				if(includeThis) {
					retVal.Add(claimProcList[i]);
				}
			}
			return retVal;
		}

		///<summary>Gets all ClaimProcs for the current Procedure. The List must be all ClaimProcs for this patient.</summary>
		public static List<ClaimProc> GetForProc(List<ClaimProc> claimProcList,long procNum) {
			//No need to check RemotingRole; no call to db.
			//MessageBox.Show(List.Length.ToString());
			//ArrayList ALForProc=new ArrayList();
			List<ClaimProc> retVal=new List<ClaimProc>();
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum) {
					retVal.Add(claimProcList[i]);  
				}
			}
			//need to sort by pri, sec, etc.  BUT,
			//the only way to do it would be to add an ordinal field to claimprocs or something similar.
			//Then a sorter could be built.  Otherwise, we don't know which order to put them in.
			//Maybe supply PatPlanList to this function, because it's ordered.
			//But, then if patient changes ins, it will 'forget' which is pri and which is sec.
			//ClaimProc[] ForProc=new ClaimProc[ALForProc.Count];
			//for(int i=0;i<ALForProc.Count;i++){
			//	ForProc[i]=(ClaimProc)ALForProc[i];
			//}
			//return ForProc;
			return retVal;
		}

		///<summary>Used in TP module to get one estimate. The List must be all ClaimProcs for this patient. If estimate can't be found, then return null.  The procedure is always status TP, so there shouldn't be more than one estimate for one plan.</summary>
		public static ClaimProc GetEstimate(List<ClaimProc> claimProcList,long procNum,long planNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].Status==ClaimProcStatus.Preauth) {
					continue;
				}
				if(claimProcList[i].ProcNum==procNum && claimProcList[i].PlanNum==planNum) {
					return claimProcList[i];
				}
			}
			return null;
		}

		///<summary>Used once in Account.  The insurance estimate based on all claimprocs with this procNum that are attached to claims. Includes status of NotReceived,Received, and Supplemental. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static string ProcDisplayInsEst(ClaimProc[] List,long procNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					//adj ignored
					//capClaim has no insEst yet
					&& (List[i].Status==ClaimProcStatus.NotReceived
					|| List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental)
					){
					retVal+=List[i].InsPayEst;
				}
			}
			return retVal.ToString("F");
		}

		///<summary>Used in Account and in PaySplitEdit. The insurance estimate based on all claimprocs with this procNum, but only for those claimprocs that are not received yet. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcEstNotReceived(List<ClaimProc> claimProcList,long procNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					&& claimProcList[i].Status==ClaimProcStatus.NotReceived
					){
					retVal+=claimProcList[i].InsPayEst;
				}
			}
			return retVal;
		}
		
		///<summary>Used in PaySplitEdit. The insurance amount paid based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPay(List<ClaimProc> claimProcList,long procNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					//&& List[i].InsPayAmt > 0//ins paid
					&& claimProcList[i].Status!=ClaimProcStatus.Preauth
					&& claimProcList[i].Status!=ClaimProcStatus.CapEstimate
					&& claimProcList[i].Status!=ClaimProcStatus.CapComplete
					&& claimProcList[i].Status!=ClaimProcStatus.Estimate) {
					retVal+=claimProcList[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>Used in PaySplitEdit. The insurance writeoff based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcWriteoff(List<ClaimProc> claimProcList,long procNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					//&& List[i].InsPayAmt > 0//ins paid
					&& claimProcList[i].Status!=ClaimProcStatus.Preauth
					&& claimProcList[i].Status!=ClaimProcStatus.CapEstimate
					&& claimProcList[i].Status!=ClaimProcStatus.CapComplete
					&& claimProcList[i].Status!=ClaimProcStatus.Estimate) {
					retVal+=claimProcList[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>Used in E-claims to get the amount paid by primary. The insurance amount paid by other planNums based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPayPri(List<ClaimProc> claimProcList,long procNum,long planNumExclude) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					&& claimProcList[i].PlanNum!=planNumExclude
					&& claimProcList[i].Status!=ClaimProcStatus.Preauth
					&& claimProcList[i].Status!=ClaimProcStatus.CapEstimate
					&& claimProcList[i].Status!=ClaimProcStatus.CapComplete
					&& claimProcList[i].Status!=ClaimProcStatus.Estimate)
				{
					retVal+=claimProcList[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>Used in E-claims to get the most recent date paid (by primary?). The insurance amount paid by the planNum based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static DateTime GetDatePaid(List<ClaimProc> claimProcList,long procNum,long planNum) {
			//No need to check RemotingRole; no call to db.
			DateTime retVal=DateTime.MinValue;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					&& claimProcList[i].PlanNum==planNum
					&& claimProcList[i].Status!=ClaimProcStatus.Preauth
					&& claimProcList[i].Status!=ClaimProcStatus.CapEstimate
					&& claimProcList[i].Status!=ClaimProcStatus.CapComplete
					&& claimProcList[i].Status!=ClaimProcStatus.Estimate) 
				{
					if(claimProcList[i].DateCP > retVal) {
						retVal=claimProcList[i].DateCP;
					}
				}
			}
			return retVal;
		}

		///<summary>Used once in Account on the Claim line.  The amount paid on a claim only by total, not including by procedure.  The list can be all ClaimProcs for patient, or just those for this claim.</summary>
		public static double ClaimByTotalOnly(ClaimProc[] List,long claimNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum==claimNum
					&& List[i].ProcNum==0
					&& List[i].Status!=ClaimProcStatus.Preauth){
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>Used once in Account on the Claim line.  The writeoff amount on a claim only by total, not including by procedure.  The list can be all ClaimProcs for patient, or just those for this claim.</summary>
		public static double ClaimWriteoffByTotalOnly(ClaimProc[] List,long claimNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<List.Length;i++) {
				if(List[i].ClaimNum==claimNum
					&& List[i].ProcNum==0
					&& List[i].Status!=ClaimProcStatus.Preauth)
				{
					retVal+=List[i].WriteOff;
				}
			}
			return retVal;
		}

		///<summary>Attaches or detaches claimprocs from the specified claimPayment. Updates all claimprocs on a claim with one query.  It also updates their DateCP's to match the claimpayment date.</summary>
		public static void SetForClaim(long claimNum,long claimPaymentNum,DateTime date,bool setAttached) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),claimNum,claimPaymentNum,date,setAttached);
				return;
			}
			string command= "UPDATE claimproc SET ClaimPaymentNum = ";
			if(setAttached){
				command+="'"+claimPaymentNum+"' ";
			}
			else{
				command+="'0' ";
			}
			command+=",DateCP="+POut.Date(date)+" "
				+"WHERE ClaimNum = '"+claimNum+"' AND "
				+"inspayamt != 0 AND ("
				+"claimpaymentNum = '"+claimPaymentNum+"' OR claimpaymentNum = '0')";
			//MessageBox.Show(string command);
 			Db.NonQ(command);
		}

		/*
		///<summary></summary>
		public static double ComputeBal(ClaimProc[] List){
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			//double pat;
			for(int i=0;i<List.Length;i++){
				if(List[i].Status==ClaimProcStatus.Adjustment//ins adjustments do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Preauth//preauthorizations do not affect patient balance
					|| List[i].Status==ClaimProcStatus.Estimate//estimates do not affect patient balance
					|| List[i].Status==ClaimProcStatus.CapEstimate//CapEstimates do not affect patient balance
					){
					continue;
				}
				if(List[i].Status==ClaimProcStatus.Received
					|| List[i].Status==ClaimProcStatus.Supplemental//because supplemental are always received
					|| List[i].Status==ClaimProcStatus.CapClaim)//would only have a payamt if received
				{
					retVal-=List[i].InsPayAmt;
					retVal-=List[i].WriteOff;
				}
				else if(List[i].Status==ClaimProcStatus.NotReceived) {
					if(!PrefC.GetBool(PrefName.BalancesDontSubtractIns")) {//this typically happens
						retVal-=List[i].InsPayEst;
						retVal-=List[i].WriteOff;
					}
				}
			}
			return retVal;
		}*/

		///<summary>After entering estimates from a preauth, this routine is called for each proc to override the ins est.</summary>
		public static void SetInsEstTotalOverride(long procNum,long planNum,double insPayEst,List<ClaimProc> claimProcList) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<claimProcList.Count;i++) {
				if(procNum!=claimProcList[i].ProcNum) {
					continue;
				}
				if(planNum!=claimProcList[i].PlanNum) {
					continue;
				}
				if(claimProcList[i].Status!=ClaimProcStatus.Estimate) {
					continue;
				}
				claimProcList[i].InsEstTotalOverride=insPayEst;
				Update(claimProcList[i]);
			}
		}

		///<summary>Calculates the Base estimate, InsEstTotal, and all the other insurance numbers for a single claimproc.  This is is not done on the fly.  Use Procedure.GetEst to later retrieve the estimate. This function replaces all of the upper estimating logic that was within FormClaimProc.  BaseEst=((fee or allowedOverride)-Copay) x (percentage or percentOverride).  The calling class must have already created the claimProc, and this function simply updates the BaseEst field of that claimproc. pst.Tot not used.  For Estimate and CapEstimate, all the estimate fields will be recalculated except the overrides.  histList and loopList can be null.  If so, then deductible and annual max will not be recalculated.  histList and loopList may only make sense in TP module and claimEdit.  loopList contains all claimprocs in the current list (TP or claim) that come before this procedure.  PaidOtherInsEstTotal should only contain sum of InsEstTotal/Override, or paid, depending on the status.  PaidOtherInsBaseEst also includes actual payments.</summary>
		public static void ComputeBaseEst(ClaimProc cp,double procFee,string toothNum,long codeNum,InsPlan plan,long patPlanNum,List<Benefit> benList,List<ClaimProcHist> histList,List<ClaimProcHist> loopList,List<PatPlan> patPlanList,double paidOtherInsTot,double paidOtherInsBase,int patientAge,double writeOffOtherIns) {
			//No need to check RemotingRole; no call to db.
			if(cp.Status==ClaimProcStatus.CapClaim
				|| cp.Status==ClaimProcStatus.CapComplete
				|| cp.Status==ClaimProcStatus.Preauth
				|| cp.Status==ClaimProcStatus.Supplemental) {
				return;//never compute estimates for those types listed above.
			}
			//NoBillIns is only calculated when creating the claimproc, even if resetAll is true.
			//If user then changes a procCode, it does not cause an update of all procedures with that code.
			if(cp.NoBillIns) {
				cp.AllowedOverride=-1;
				cp.CopayAmt=0;
				cp.CopayOverride=-1;
				cp.Percentage=-1;
				cp.PercentOverride=-1;
				cp.DedEst=-1;
				cp.DedEstOverride=-1;
				cp.PaidOtherIns=-1;
				cp.BaseEst=0;
				cp.InsEstTotal=0;
				cp.InsEstTotalOverride=-1;
				cp.WriteOff=0;
				cp.PaidOtherInsOverride=-1;
				cp.WriteOffEst=-1;
				cp.WriteOffEstOverride=-1;
				return;
			}
			cp.EstimateNote="";
			//This function is called every time a ProcFee is changed,
			//so the BaseEst does reflect the new ProcFee.
			//ProcFee----------------------------------------------------------------------------------------------
			cp.BaseEst=procFee;
			cp.InsEstTotal=procFee;
			//Allowed----------------------------------------------------------------------------------------------
			double allowed=procFee;//could be fee, or could be a little less.  Used further down in paidOtherIns.
			if(cp.AllowedOverride!=-1) {
				if(cp.AllowedOverride > procFee){
					cp.AllowedOverride=procFee;
				}
				allowed=cp.AllowedOverride;
				cp.BaseEst=cp.AllowedOverride;
				cp.InsEstTotal=cp.AllowedOverride;
			}
			else if(plan.PlanType=="c"){//capitation estimate.  No allowed fee sched.  No substitute codes.
				allowed=procFee;
				cp.BaseEst=procFee;
				cp.InsEstTotal=procFee;
			}
			else {
				//no point in wasting time calculating this unless it's needed.
				double carrierAllowed=InsPlans.GetAllowed(ProcedureCodes.GetProcCode(codeNum).ProcCode,plan.FeeSched,plan.AllowedFeeSched,
					plan.CodeSubstNone,plan.PlanType,toothNum,cp.ProvNum);
				if(carrierAllowed != -1) {
					if(carrierAllowed > procFee) {
						allowed=procFee;
						cp.BaseEst=procFee;
						cp.InsEstTotal=procFee;
					}
					else {
						allowed=carrierAllowed;
						cp.BaseEst=carrierAllowed;
						cp.InsEstTotal=carrierAllowed;
					}
				}
			}
			//Copay----------------------------------------------------------------------------------------------
			cp.CopayAmt=InsPlans.GetCopay(codeNum,plan.FeeSched,plan.CopayFeeSched);
			if(cp.CopayAmt > allowed) {//if the copay is greater than the allowed fee calculated above
				cp.CopayAmt=allowed;//reduce the copay
			}
			if(cp.CopayOverride > allowed) {//or if the copay override is greater than the allowed fee calculated above
				cp.CopayOverride=allowed;//reduce the override
			}
			if(cp.Status==ClaimProcStatus.CapEstimate) {
				//this does automate the Writeoff. If user does not want writeoff automated,
				//then they will have to complete the procedure first. (very rare)
				if(cp.CopayAmt==-1) {
					cp.CopayAmt=0;
				}
				if(cp.CopayOverride != -1) {//override the copay
					cp.WriteOffEst=cp.BaseEst-cp.CopayOverride;
				}
				else if(cp.CopayAmt!=-1) {//use the calculated copay
					cp.WriteOffEst=cp.BaseEst-cp.CopayAmt;
				}
				if(cp.WriteOffEst<0) {
					cp.WriteOffEst=0;
				}
				cp.WriteOff=cp.WriteOffEst;
				cp.DedApplied=0;
				cp.DedEst=0;
				cp.Percentage=-1;
				cp.PercentOverride=-1;
				cp.BaseEst=0;
				cp.InsEstTotal=0;
				return;
			}
			if(cp.CopayOverride != -1) {//subtract copay if override
				cp.BaseEst-=cp.CopayOverride;
				cp.InsEstTotal-=cp.CopayOverride;
			}
			else if(cp.CopayAmt != -1) {//otherwise subtract calculated copay
				cp.BaseEst-=cp.CopayAmt;
				cp.InsEstTotal-=cp.CopayAmt;
			}
			//Deductible----------------------------------------------------------------------------------------
			//The code below handles partial usage of available deductible. 
			DateTime procDate;
			if(cp.Status==ClaimProcStatus.Estimate) {
				procDate=DateTime.Today;
			}
			else {
				procDate=cp.ProcDate;
			}
			if(loopList!=null && histList!=null) {
				cp.DedEst=Benefits.GetDeductibleByCode(benList,plan.PlanNum,patPlanNum,procDate,ProcedureCodes.GetStringProcCode(codeNum),histList,loopList,plan,cp.PatNum);
			}
			if(cp.DedEst > cp.InsEstTotal){//if the deductible is more than the fee
				cp.DedEst=cp.InsEstTotal;//reduce the deductible
			}
			if(cp.DedEstOverride > cp.InsEstTotal) {//if the deductible override is more than the fee
				cp.DedEstOverride=cp.InsEstTotal;//reduce the override.
			}
			if(cp.DedEstOverride != -1) {//use the override
				cp.InsEstTotal-=cp.DedEstOverride;//subtract
			}
			else if(cp.DedEst != -1){//use the calculated deductible
				cp.InsEstTotal-=cp.DedEst;
			}
			//Percentage----------------------------------------------------------------------------------------
			cp.Percentage=Benefits.GetPercent(ProcedureCodes.GetProcCode(codeNum).ProcCode,plan.PlanType,plan.PlanNum,patPlanNum,benList);//will never =-1
			if(cp.PercentOverride != -1) {//override, so use PercentOverride
				cp.BaseEst=cp.BaseEst*(double)cp.PercentOverride/100d;
				cp.InsEstTotal=cp.InsEstTotal*(double)cp.PercentOverride/100d;
			}
			else if(cp.Percentage != -1) {//use calculated Percentage
				cp.BaseEst=cp.BaseEst*(double)cp.Percentage/100d;
				cp.InsEstTotal=cp.InsEstTotal*(double)cp.Percentage/100d;
			}
			//PaidOtherIns----------------------------------------------------------------------------------------
			//double paidOtherInsActual=GetPaidOtherIns(cp,patPlanList,patPlanNum,histList);//can return -1 for primary
			PatPlan pp=PatPlans.GetFromList(patPlanList.ToArray(),patPlanNum);
			//if -1, that indicates primary ins, not a proc, or no histlist.  We should not alter it in this case.
			//if(paidOtherInsActual!=-1) {
			//An older restriction was that histList must not be null.  But since this is now straight from db, that's not restriction.
			if(pp==null) {
				//corruption.  Do nothing.
			}
			else if(pp.Ordinal==1 || cp.ProcNum==0){
				cp.PaidOtherIns=0;
			}
			else{
				//The normal calculation uses the InsEstTotal from the primary ins.
				//But in TP module, if not using max and deduct, then the amount estimated to be paid by primary will be different.
				//It will use the primary BaseEst instead of the primary InsEstTotal.
				//Since the only use of BaseEst here is to handle this alternate viewing in the TP,
				//the secondary BaseEst should use the primary BaseEst when calculating paidOtherIns.
				//The BaseEst will, however, use PaidOtherInsOverride, if user has entered one.
				//This calculation doesn't need to be accurate unless viewing TP,
				//so it's ok to pass in a dummy value, like paidOtherInsTotal.
				//We do InsEstTotal first
				//cp.PaidOtherIns=paidOtherInsActual+paidOtherInsEstTotal;
				cp.PaidOtherIns=paidOtherInsTot;
				double paidOtherInsTotTemp=cp.PaidOtherIns;
				if(cp.PaidOtherInsOverride != -1) {//use the override
					paidOtherInsTotTemp=cp.PaidOtherInsOverride;
				}
				//example: Fee:200, InsEstT:80, BaseEst:100, PaidOI:110.
				//So... MaxPtP:90.
				//Since InsEstT is not greater than MaxPtoP, no change.
				//Since BaseEst is greater than MaxPtoP, BaseEst changed to 90.
				if(paidOtherInsTotTemp != -1) {
					double maxPossibleToPay=allowed-paidOtherInsTotTemp;
					if(maxPossibleToPay<0) {
						maxPossibleToPay=0;
					}
					if(cp.InsEstTotal > maxPossibleToPay) {
						cp.InsEstTotal=maxPossibleToPay;//reduce the estimate
					}
				}
				//Then, we do BaseEst
				double paidOtherInsBaseTemp=paidOtherInsBase;//paidOtherInsActual+paidOtherInsBaseEst;
				if(cp.PaidOtherInsOverride != -1) {//use the override
					paidOtherInsBaseTemp=cp.PaidOtherInsOverride;
				}
				if(paidOtherInsBaseTemp != -1) {
					double maxPossibleToPay=allowed-paidOtherInsBaseTemp;
					if(maxPossibleToPay<0) {
						maxPossibleToPay=0;
					}
					if(cp.BaseEst > maxPossibleToPay) {
						cp.BaseEst=maxPossibleToPay;//reduce the base est
					}
				}
			}
			//Exclusions---------------------------------------------------------------------------------------
			//We are not going to consider date of proc.  Just simple exclusions
			if(Benefits.IsExcluded(ProcedureCodes.GetStringProcCode(codeNum),benList,plan.PlanNum,patPlanNum)) {
				cp.BaseEst=0;
				cp.InsEstTotal=0;
				if(cp.EstimateNote!="") {
					cp.EstimateNote+=", ";
				}
				cp.EstimateNote+=Lans.g("ClaimProcs","Exclusion");
			}
			//base estimate is now done and will not be altered further.  From here out, we are only altering insEstTotal
			//annual max and other limitations--------------------------------------------------------------------------------
			if(loopList!=null && histList!=null) {
				string note="";
				cp.InsEstTotal=Benefits.GetLimitationByCode(benList,plan.PlanNum,patPlanNum,procDate,ProcedureCodes.GetStringProcCode(codeNum),histList,loopList,plan,cp.PatNum,out note,cp.InsEstTotal,patientAge);
				if(note != "") {
					if(cp.EstimateNote != "") {
						cp.EstimateNote+=", ";
					}
					cp.EstimateNote+=note;
				}
			}
			//procDate;//was already calculated in the deductible section.
			//Writeoff Estimate------------------------------------------------------------------------------------------
			if(plan.PlanType=="p") {//PPO
				//we can't use the allowed previously calculated, because it might be the allowed of a substituted code.
				//so we will calculate the allowed all over again, but this time, without using a substitution code.
				//AllowedFeeSched and toothNum do not need to be passed in.  codeSubstNone is set to true to not subst.
				double carrierAllowedNoSubst=InsPlans.GetAllowed(ProcedureCodes.GetProcCode(codeNum).ProcCode,plan.FeeSched,0,
					true,"p","",cp.ProvNum);
				double allowedNoSubst=procFee;
				if(carrierAllowedNoSubst != -1) {
					if(carrierAllowedNoSubst > procFee) {
						allowedNoSubst=procFee;
					}
					else {
						allowedNoSubst=carrierAllowedNoSubst;
					}
				}
				double normalWriteOff=procFee-allowedNoSubst;//This is what the normal writeoff would be if no other insurance was involved.
				if(normalWriteOff<0) {
					normalWriteOff=0;
				}
				double remainingWriteOff=procFee-paidOtherInsTot-writeOffOtherIns;//This is the fee minus whatever other ins has already paid or written off.
				if(remainingWriteOff<0) {
					remainingWriteOff=0;
				}
				if(writeOffOtherIns>0) {//no secondary writeoff estimates allowed
					cp.WriteOffEst=0;
				}
				//We can't go over either number.  We must use the smaller of the two.  If one of them is zero, then the writeoff is zero.
				else if(remainingWriteOff==0 || normalWriteOff==0) {
					cp.WriteOffEst=0;
				}
				else if(remainingWriteOff<=normalWriteOff) {
					cp.WriteOffEst=remainingWriteOff;
				}
				else {
					cp.WriteOffEst=normalWriteOff;
				}
			}
			//capitation calculation never makes it this far:
			//else if(plan.PlanType=="c") {//capitation
			//	cp.WriteOffEst=cp.WriteOff;//this probably needs to change
			//}
			else {
				cp.WriteOffEst=-1;
			}
		}

		/*
		///<summary>We don't care about a looplist because those would be for different procedures.  So this calculation really only makes sense when calculating secondary insurance in the claim edit window or when calculating secondary estimates in the TP module.  HistList will include actual payments and estimated pending payments for this proc, but it will not include primary estimates.  Estimates are not handled here, but are instead passed in to ComputeBaseEst</summary>
		private static double GetPaidOtherIns(ClaimProc cp,List<PatPlan> patPlanList,long patPlanNum,List<ClaimProcHist> histList) {
			if(cp.ProcNum==0) {
				return -1;
			}
			if(histList==null) {
				return -1;
			}
			PatPlan pp=PatPlans.GetFromList(patPlanList.ToArray(),patPlanNum);
			if(pp==null) {
				return -1;
			}
			int thisOrdinal=pp.Ordinal;
			if(thisOrdinal==1) {
				return -1;
			}
			double retVal=0;
			int ordinal;
			for(int i=0;i<histList.Count;i++) {
				ordinal=PatPlans.GetOrdinal(patPlanList,cp.PlanNum);
				if(ordinal >= thisOrdinal){
					continue;
				}
				retVal+=histList[i].Amount;
			}
			return retVal;
		}*/

		///<summary>Only useful if secondary ins or greater.  For one procedure, it gets the sum of InsEstTotal/Override for other insurances with lower ordinals.  Either estimates or actual payments.  Will return 0 if ordinal of this claimproc is 1.</summary>
		public static double GetPaidOtherInsTotal(ClaimProc cp,List<PatPlan> patPlanList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<double>(MethodBase.GetCurrentMethod(),cp,patPlanList);
			}
			if(cp.ProcNum==0) {
				return 0;
			}
			int thisOrdinal=PatPlans.GetOrdinal(patPlanList,cp.PlanNum);
			if(thisOrdinal==1) {
				return 0;
			}
			string command="SELECT PlanNum,InsEstTotal,InsEstTotalOverride,InsPayAmt,Status FROM claimproc WHERE ProcNum="+POut.Long(cp.ProcNum);
			DataTable table=Db.GetTable(command);
			double retVal=0;
			long planNum;
			int ordinal;
			double insEstTotal;
			double insEstTotalOverride;
			double insPayAmt;
			ClaimProcStatus status;
			for(int i=0;i<table.Rows.Count;i++) {
				planNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());
				ordinal=PatPlans.GetOrdinal(patPlanList,planNum);
				if(ordinal >= thisOrdinal) {
					continue;
				}
				insEstTotal=PIn.Double(table.Rows[i]["InsEstTotal"].ToString());
				insEstTotalOverride=PIn.Double(table.Rows[i]["InsEstTotalOverride"].ToString());
				insPayAmt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
				status=(ClaimProcStatus)PIn.Int(table.Rows[i]["Status"].ToString());
				if(status==ClaimProcStatus.Received || status==ClaimProcStatus.Supplemental) 
				{
					retVal+=insPayAmt;
				}
				if(status==ClaimProcStatus.Estimate || status==ClaimProcStatus.NotReceived) 
				{
					if(insEstTotalOverride != -1) {
						retVal+=insEstTotalOverride;
					}
					else {
						retVal+=insEstTotal;
					}
				}
			}
			return retVal;
		}

		///<summary>Only useful if secondary ins or greater.  For one procedure, it gets the sum of BaseEst for other insurances with lower ordinals.  Either estimates or actual payments.  Will return 0 if ordinal of this claimproc is 1.</summary>
		public static double GetPaidOtherInsBaseEst(ClaimProc cp,List<PatPlan> patPlanList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<double>(MethodBase.GetCurrentMethod(),cp,patPlanList);
			}
			if(cp.ProcNum==0) {
				return 0;
			}
			int thisOrdinal=PatPlans.GetOrdinal(patPlanList,cp.PlanNum);
			if(thisOrdinal==1) {
				return 0;
			}
			string command="SELECT PlanNum,BaseEst,InsPayAmt,Status FROM claimproc WHERE ProcNum="+POut.Long(cp.ProcNum);
			DataTable table=Db.GetTable(command);
			double retVal=0;
			long planNum;
			int ordinal;
			double baseEst;
			double insPayAmt;
			ClaimProcStatus status;
			for(int i=0;i<table.Rows.Count;i++) {
				planNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());
				ordinal=PatPlans.GetOrdinal(patPlanList,planNum);
				if(ordinal >= thisOrdinal) {
					continue;
				}
				baseEst=PIn.Double(table.Rows[i]["BaseEst"].ToString());
				insPayAmt=PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
				status=(ClaimProcStatus)PIn.Int(table.Rows[i]["Status"].ToString());
				if(status==ClaimProcStatus.Received || status==ClaimProcStatus.Supplemental) {
					retVal+=insPayAmt;
				}
				if(status==ClaimProcStatus.Estimate || status==ClaimProcStatus.NotReceived) {
					retVal+=baseEst;
				}
			}
			return retVal;
		}

		///<summary>Only useful if secondary ins or greater.  For one procedure, it gets the sum of WriteOffEstimates/Override for other insurances with lower ordinals.  Either estimates or actual writeoffs.  Will return 0 if ordinal of this claimproc is 1.</summary>
		public static double GetWriteOffOtherIns(ClaimProc cp,List<PatPlan> patPlanList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<double>(MethodBase.GetCurrentMethod(),cp,patPlanList);
			}
			if(cp.ProcNum==0) {
				return 0;
			}
			int thisOrdinal=PatPlans.GetOrdinal(patPlanList,cp.PlanNum);
			if(thisOrdinal==1) {
				return 0;
			}
			string command="SELECT PlanNum,WriteOffEst,WriteOffEstOverride,WriteOff,Status FROM claimproc WHERE ProcNum="+POut.Long(cp.ProcNum);
			DataTable table=Db.GetTable(command);
			double retVal=0;
			long planNum;
			int ordinal;
			double writeOffEst;
			double writeOffEstOverride;
			double writeOff;
			ClaimProcStatus status;
			for(int i=0;i<table.Rows.Count;i++) {
				planNum=PIn.Long(table.Rows[i]["PlanNum"].ToString());
				ordinal=PatPlans.GetOrdinal(patPlanList,planNum);
				if(ordinal >= thisOrdinal) {
					continue;
				}
				writeOffEst=PIn.Double(table.Rows[i]["WriteOffEst"].ToString());
				writeOffEstOverride=PIn.Double(table.Rows[i]["WriteOffEstOverride"].ToString());
				writeOff=PIn.Double(table.Rows[i]["WriteOff"].ToString());
				status=(ClaimProcStatus)PIn.Int(table.Rows[i]["Status"].ToString());
				if(status==ClaimProcStatus.Received || status==ClaimProcStatus.Supplemental) {
					retVal+=writeOff;
				}
				if(status==ClaimProcStatus.Estimate || status==ClaimProcStatus.NotReceived) {
					if(writeOffEstOverride != -1) {
						retVal+=writeOffEstOverride;
					}
					else if(writeOffEst !=-1){
						retVal+=writeOffEst;
					}
				}
			}
			return retVal;
		}

		///<summary>Simply gets insEstTotal or its override if applicable.</summary>
		public static double GetInsEstTotal(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.InsEstTotalOverride!=-1) {
				return cp.InsEstTotalOverride;
			}
			return cp.InsEstTotal;
		}

		///<summary>Simply gets dedEst or its override if applicable.  Can return 0, but never -1.</summary>
		public static double GetDedEst(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.DedEstOverride!=-1) {
				return cp.DedEstOverride;
			}
			else if(cp.DedEst!=-1) {
				return cp.DedEst;
			}
			return 0;
		}

		///<summary>Gets either the override or the calculated writeoff estimate.  Or zero if neither.</summary>
		public static double GetWriteOffEstimate(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.WriteOffEstOverride!=-1) {
				return cp.WriteOffEstOverride;
			}
			else if(cp.WriteOffEst!=-1) {
				return cp.WriteOffEst;
			}
			return 0;
		}

		public static string GetPercentageDisplay(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.Status==ClaimProcStatus.CapEstimate || cp.Status==ClaimProcStatus.CapComplete) {
				return "";
			}
			if(cp.PercentOverride!=-1) {
				return cp.PercentOverride.ToString();
			}
			else if(cp.Percentage!=-1) {
				return cp.Percentage.ToString();
			}
			return "";
		}

		public static string GetCopayDisplay(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.CopayOverride!=-1) {
				return cp.CopayOverride.ToString("f");
			}
			else if(cp.CopayAmt!=-1) {
				return cp.CopayAmt.ToString("f");
			}
			return "";
		}

		///<summary></summary>
		public static string GetWriteOffEstimateDisplay(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.WriteOffEstOverride!=-1) {
				return cp.WriteOffEstOverride.ToString("f");
			}
			else if(cp.WriteOffEst!=-1) {
				return cp.WriteOffEst.ToString("f");
			}
			return "";
		}

		public static string GetEstimateDisplay(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.Status==ClaimProcStatus.CapEstimate || cp.Status==ClaimProcStatus.CapComplete) {
				return "";
			}
			if(cp.Status==ClaimProcStatus.Estimate) {
				if(cp.InsEstTotalOverride!=-1) {
					return cp.InsEstTotalOverride.ToString("f");
				}
				else{//shows even if 0.
					return cp.InsEstTotal.ToString("f");
				}
			}
			return cp.InsPayEst.ToString("f");
		}

		///<summary>Returns 0 or -1 if no deduct.</summary>
		public static double GetDeductibleDisplay(ClaimProc cp) {
			//No need to check RemotingRole; no call to db.
			if(cp.Status==ClaimProcStatus.CapEstimate || cp.Status==ClaimProcStatus.CapComplete) {
				return -1;
			}
			if(cp.Status==ClaimProcStatus.Estimate) {
				if(cp.DedEstOverride != -1) {
					return cp.DedEstOverride;
				}
				//else if(cp.DedEst > 0) {
					return cp.DedEst;//could be -1
				//}
				//else {
				//	return "";
				//}
			}
			return cp.DedApplied;
		}

		///<summary>Used in TP module.  Gets all estimate notes for this proc.</summary>
		public static string GetEstimateNotes(long procNum,List<ClaimProc> cpList) {
			string retVal="";
			for(int i=0;i<cpList.Count;i++) {
				if(cpList[i].ProcNum!=procNum) {
					continue;
				}
				if(cpList[i].EstimateNote==""){
					continue;
				}
				if(retVal!="") {
					retVal+=", ";
				}
				retVal+=cpList[i].EstimateNote;
			}
			return retVal;
		}

		public static double GetTotalWriteOffEstimateDisplay(List<ClaimProc> cpList,long procNum) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<cpList.Count;i++) {
				if(cpList[i].ProcNum!=procNum) {
					continue;
				}
				if(cpList[i].WriteOffEstOverride!=-1) {
					retVal+=cpList[i].WriteOffEstOverride;
				}
				else if(cpList[i].WriteOffEst!=-1) {
					retVal+=cpList[i].WriteOffEst;
				}
			}
			return retVal;
		}

		public static List<ClaimProcHist> GetHistList(long patNum,List<Benefit> benList,List<PatPlan> patPlanList,List<InsPlan> planList,DateTime procDate) {
			//No need to check RemotingRole; no call to db.
			return GetHistList(patNum,benList,patPlanList,planList,-1,procDate);
		}

		///<summary>We pass in the benefit list so that we know whether to include family.  We are getting a simplified list of claimprocs.  History of payments and pending payments.  If the patient has multiple insurance, then this info will be for all of their insurance plans.  It runs a separate query for each plan because that's the only way to handle family history.  For some plans, the benefits will indicate entire family, but not for other plans.  And the date ranges can be different as well.   When this list is processed later, it is again filtered, but it can't have missing information.  Use excludeClaimNum=-1 to not exclude a claim.  A claim is excluded if editing from inside that claim.</summary>
		public static List<ClaimProcHist> GetHistList(long patNum,List<Benefit> benList,List<PatPlan> patPlanList,List<InsPlan> planList,long excludeClaimNum,DateTime procDate) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProcHist>>(MethodBase.GetCurrentMethod(),patNum,benList,patPlanList,planList,excludeClaimNum,procDate);
			}
			List<ClaimProcHist> retVal=new List<ClaimProcHist>();
			InsPlan plan;
			bool isFam;
			bool isLife;
			DateTime dateStart;
			DataTable table;
			ClaimProcHist cph;
			for(int p=0;p<patPlanList.Count;p++) {//loop through each plan that this patient is covered by
				//get the plan for the given patPlan
				plan=InsPlans.GetPlan(patPlanList[p].PlanNum,planList);
				//test benefits for fam and life
				isFam=false;
				isLife=false;
				for(int i=0;i<benList.Count;i++) {
					if(benList[i].PlanNum==0 && benList[i].PatPlanNum!=patPlanList[p].PatPlanNum) {
						continue;
					}
					if(benList[i].PatPlanNum==0 && benList[i].PlanNum!=plan.PlanNum) {
						continue;
					}
					else if(benList[i].TimePeriod==BenefitTimePeriod.Lifetime) {
						isLife=true;
					}
					if(benList[i].CoverageLevel==BenefitCoverageLevel.Family) {
						isFam=true;
					}
				}
				if(isLife) {
					dateStart=new DateTime(1880,1,1);
				}
				else {
					//unsure what date to use to start.  DateTime.Today?  That might miss procs from late last year when doing secondary claim, InsPaidOther.
					//If we use the proc date, then it will indeed get an accurate history.  And future procedures just don't matter when calculating things.
					dateStart=BenefitLogic.ComputeRenewDate(procDate,plan.MonthRenew);
				}
				//we don't include planNum in the query because we are already restricting to one plan
				//but we do include patnum because this one query can get results for multiple patients that all have this one plan.
				string command="SELECT claimproc.ProcDate,CodeNum,InsPayEst,InsPayAmt,DedApplied,claimproc.PatNum,Status,ClaimNum "
					+"FROM claimproc "
					+"LEFT JOIN procedurelog on claimproc.ProcNum=procedurelog.ProcNum "//to get the codenum
					+"WHERE claimproc.PlanNum="+POut.Long(plan.PlanNum)
					+" AND claimproc.ProcDate >= "+POut.Date(dateStart)//no upper limit on date.
					+" AND claimproc.Status IN("
					+POut.Long((int)ClaimProcStatus.NotReceived)+","
					+POut.Long((int)ClaimProcStatus.Adjustment)+","//insPayAmt and DedApplied
					+POut.Long((int)ClaimProcStatus.Received)+","
					+POut.Long((int)ClaimProcStatus.Supplemental)+")";
				if(!isFam) {
					command+=" AND claimproc.PatNum="+POut.Long(patNum);
				}
				if(excludeClaimNum != -1) {
					command+=" AND claimproc.ClaimNum != "+POut.Long(excludeClaimNum);
				}
				table=Db.GetTable(command);
				for(int i=0;i<table.Rows.Count;i++) {
					cph=new ClaimProcHist();
					cph.ProcDate   = PIn.Date (table.Rows[i]["ProcDate"].ToString());
					cph.StrProcCode= ProcedureCodes.GetStringProcCode(PIn.Long(table.Rows[i]["CodeNum"].ToString()));
					cph.Status=(ClaimProcStatus)PIn.Long(table.Rows[i]["Status"].ToString());
					if(cph.Status==ClaimProcStatus.NotReceived) {
						cph.Amount   = PIn.Double(table.Rows[i]["InsPayEst"].ToString());
					}
					else {
						cph.Amount   = PIn.Double(table.Rows[i]["InsPayAmt"].ToString());
					}
					cph.Deduct     = PIn.Double(table.Rows[i]["DedApplied"].ToString());
					cph.PatNum     = PIn.Long   (table.Rows[i]["PatNum"].ToString());
					cph.ClaimNum   = PIn.Long   (table.Rows[i]["ClaimNum"].ToString());
					cph.PlanNum=plan.PlanNum;
					retVal.Add(cph);
				}
			}
			return retVal;
		}

		/// <summary>Used in creation of the loopList.  Used in TP list estimation and in claim creation.  Some of the items in the claimProcList passed in will not have been saved to the database yet.</summary>
		public static List<ClaimProcHist> GetHistForProc(List<ClaimProc> claimProcList,long procNum,long codeNum) {
			List<ClaimProcHist> retVal=new List<ClaimProcHist>();
			ClaimProcHist cph;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum != procNum) {
					continue;
				}
				cph=new ClaimProcHist();
				cph.Amount=ClaimProcs.GetInsEstTotal(claimProcList[i]);
				cph.ClaimNum=0;
				if(claimProcList[i].DedEstOverride != -1) {
					cph.Deduct=claimProcList[i].DedEstOverride;
				}
				else {
					cph.Deduct=claimProcList[i].DedEst;
				}
				cph.PatNum=claimProcList[i].PatNum;
				cph.PlanNum=claimProcList[i].PlanNum;
				cph.ProcDate=DateTime.Today;
				cph.Status=ClaimProcStatus.Estimate;
				cph.StrProcCode=ProcedureCodes.GetStringProcCode(codeNum);
				retVal.Add(cph);
			}
			return retVal;
		}

		///<summary>Does not make call to db unless necessary.</summary>
		public static void SetProvForProc(Procedure proc,List<ClaimProc> ClaimProcList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),proc,ClaimProcList);
				return;
			}
			for(int i=0;i<ClaimProcList.Count;i++) {
				if(ClaimProcList[i].ProcNum!=proc.ProcNum) {
					continue;
				}
				if(ClaimProcList[i].ProvNum==proc.ProvNum) {
					continue;//no change needed
				}
				ClaimProcList[i].ProvNum=proc.ProvNum;
				Update(ClaimProcList[i]);
			}
		}


	}

	///<summary>During the ClaimProc.ComputeBaseEst() and related sections, this holds historical payment information for one procedure or an adjustment to insurance benefits from patplan.</summary>
	public class ClaimProcHist {
		public DateTime ProcDate;
		public string StrProcCode;
		///<summary>Insurance paid or est, depending on the status.</summary>
		public double Amount;
		///<summary>Deductible paid or est.</summary>
		public double Deduct;
		///<summary>Because a list can store info for an entire family.</summary>
		public long PatNum;
		///<summary>Because a list can store info about multiple plans.</summary>
		public long PlanNum;
		///<summary>So that we can exclude history from the claim that we are in.</summary>
		public long ClaimNum;
		///<summary>Only 4 statuses get used anyway.  This helps us filter the pending items sometimes.</summary>
		public ClaimProcStatus Status;

		public override string ToString() {
			return StrProcCode+" "+Status.ToString()+" "+Amount.ToString()+" ded:"+Deduct.ToString();
		}
	}


}









