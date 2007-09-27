using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	
	///<summary></summary>
	public class ClaimProcs{

		///<summary></summary>
		public static ClaimProc[] Refresh(int patNum){
			string command=
				"SELECT * from claimproc "
				+"WHERE PatNum = '"+patNum.ToString()+"' ORDER BY LineNumber";
			return RefreshAndFill(command);
		}

		///<summary>When using family deduct or max, this gets all claimprocs for the given plan.  This info is needed to compute used and pending insurance.</summary>
		public static ClaimProc[] RefreshFam(int planNum) {
			string command=
				"SELECT * FROM claimproc "
				+"WHERE PlanNum = "+POut.PInt(planNum);
				//+" OR PatPlanNum = "+POut.PInt(patPlanNum);
			return RefreshAndFill(command);
		}

		private static ClaimProc[] RefreshAndFill(string command){
 			DataTable table=General.GetTable(command);
			ClaimProc[] List=new ClaimProc[table.Rows.Count];
			for(int i=0;i<List.Length;i++){
				List[i]=new ClaimProc();
				List[i].ClaimProcNum   = PIn.PInt   (table.Rows[i][0].ToString());
				List[i].ProcNum        = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ClaimNum       = PIn.PInt   (table.Rows[i][2].ToString());	
				List[i].PatNum         = PIn.PInt   (table.Rows[i][3].ToString());
				List[i].ProvNum        = PIn.PInt   (table.Rows[i][4].ToString());
				List[i].FeeBilled      = PIn.PDouble(table.Rows[i][5].ToString());
				List[i].InsPayEst      = PIn.PDouble(table.Rows[i][6].ToString());
				List[i].DedApplied     = PIn.PDouble(table.Rows[i][7].ToString());
				List[i].Status         = (ClaimProcStatus)PIn.PInt(table.Rows[i][8].ToString());
				List[i].InsPayAmt      = PIn.PDouble(table.Rows[i][9].ToString());
				List[i].Remarks        = PIn.PString(table.Rows[i][10].ToString());
				List[i].ClaimPaymentNum= PIn.PInt   (table.Rows[i][11].ToString());
				List[i].PlanNum        = PIn.PInt   (table.Rows[i][12].ToString());
				List[i].DateCP         = PIn.PDate  (table.Rows[i][13].ToString());
				List[i].WriteOff       = PIn.PDouble(table.Rows[i][14].ToString());
				List[i].CodeSent       = PIn.PString(table.Rows[i][15].ToString());
				List[i].AllowedOverride= PIn.PDouble(table.Rows[i][16].ToString());
				List[i].Percentage     = PIn.PInt   (table.Rows[i][17].ToString());
				List[i].PercentOverride= PIn.PInt   (table.Rows[i][18].ToString());
				List[i].CopayAmt       = PIn.PDouble(table.Rows[i][19].ToString());
				List[i].OverrideInsEst = PIn.PDouble(table.Rows[i][20].ToString());
				List[i].NoBillIns      = PIn.PBool  (table.Rows[i][21].ToString());
				List[i].DedBeforePerc  = PIn.PBool  (table.Rows[i][22].ToString());
				List[i].OverAnnualMax  = PIn.PDouble(table.Rows[i][23].ToString());
				List[i].PaidOtherIns   = PIn.PDouble(table.Rows[i][24].ToString());
				List[i].BaseEst        = PIn.PDouble(table.Rows[i][25].ToString());
				List[i].CopayOverride  = PIn.PDouble(table.Rows[i][26].ToString());
				List[i].ProcDate       = PIn.PDate  (table.Rows[i][27].ToString());
				List[i].DateEntry      = PIn.PDate  (table.Rows[i][28].ToString());
				List[i].LineNumber     = PIn.PInt   (table.Rows[i][29].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Insert(ClaimProc cp) {
			if(PrefB.RandomKeys) {
				cp.ClaimProcNum=MiscData.GetKey("claimproc","ClaimProcNum");
			}
			string command= "INSERT INTO claimproc (";
			if(PrefB.RandomKeys) {
				command+="ClaimProcNum,";
			}
			command+="ProcNum,ClaimNum,PatNum,ProvNum"
				+",FeeBilled,InsPayEst,DedApplied,Status,InsPayAmt,Remarks,ClaimPaymentNum"
				+",PlanNum,DateCP,WriteOff,CodeSent,AllowedOverride,Percentage,PercentOverride"
				+",CopayAmt,OverrideInsEst,NoBillIns,DedBeforePerc,OverAnnualMax"
				+",PaidOtherIns,BaseEst,CopayOverride,ProcDate,DateEntry,LineNumber) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(cp.ClaimProcNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(cp.ProcNum)+"', "
				+"'"+POut.PInt(cp.ClaimNum)+"', "
				+"'"+POut.PInt(cp.PatNum)+"', "
				+"'"+POut.PInt(cp.ProvNum)+"', "
				+"'"+POut.PDouble(cp.FeeBilled)+"', "
				+"'"+POut.PDouble(cp.InsPayEst)+"', "
				+"'"+POut.PDouble(cp.DedApplied)+"', "
				+"'"+POut.PInt((int)cp.Status)+"', "
				+"'"+POut.PDouble(cp.InsPayAmt)+"', "
				+"'"+POut.PString(cp.Remarks)+"', "
				+"'"+POut.PInt(cp.ClaimPaymentNum)+"', "
				+"'"+POut.PInt(cp.PlanNum)+"', "
				+POut.PDate(cp.DateCP)+", "
				+"'"+POut.PDouble(cp.WriteOff)+"', "
				+"'"+POut.PString(cp.CodeSent)+"', "
				+"'"+POut.PDouble(cp.AllowedOverride)+"', "
				+"'"+POut.PInt(cp.Percentage)+"', "
				+"'"+POut.PInt(cp.PercentOverride)+"', "
				+"'"+POut.PDouble(cp.CopayAmt)+"', "
				+"'"+POut.PDouble(cp.OverrideInsEst)+"', "
				+"'"+POut.PBool(cp.NoBillIns)+"', "
				+"'"+POut.PBool(cp.DedBeforePerc)+"', "
				+"'"+POut.PDouble(cp.OverAnnualMax)+"', "
				+"'"+POut.PDouble(cp.PaidOtherIns)+"', "
				+"'"+POut.PDouble(cp.BaseEst)+"', "
				+"'"+POut.PDouble(cp.CopayOverride)+"', "
				+POut.PDate(cp.ProcDate)+", ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}else{//Assume MySQL
				command+="NOW()";
			}
			command+=", '"+POut.PInt(cp.LineNumber)+"')";
			//MessageBox.Show(string command);
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				cp.ClaimProcNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(ClaimProc cp) {
			string command= "UPDATE claimproc SET "
				+"ProcNum = '"        +POut.PInt(cp.ProcNum)+"'"
				+",ClaimNum = '"      +POut.PInt(cp.ClaimNum)+"' "
				+",PatNum = '"        +POut.PInt(cp.PatNum)+"'"
				+",ProvNum = '"       +POut.PInt(cp.ProvNum)+"'"
				+",FeeBilled = '"     +POut.PDouble(cp.FeeBilled)+"'"
				+",InsPayEst = '"     +POut.PDouble(cp.InsPayEst)+"'"
				+",DedApplied = '"    +POut.PDouble(cp.DedApplied)+"'"
				+",Status = '"        +POut.PInt((int)cp.Status)+"'"
				+",InsPayAmt = '"     +POut.PDouble(cp.InsPayAmt)+"'"
				+",Remarks = '"       +POut.PString(cp.Remarks)+"'"
				+",ClaimPaymentNum= '"+POut.PInt(cp.ClaimPaymentNum)+"'"
				+",PlanNum= '"        +POut.PInt(cp.PlanNum)+"'"
				+",DateCP= "         +POut.PDate(cp.DateCP)
				+",WriteOff= '"       +POut.PDouble(cp.WriteOff)+"'"
				+",CodeSent= '"       +POut.PString(cp.CodeSent)+"'"
				+",AllowedOverride= '"+POut.PDouble(cp.AllowedOverride)+"'"
				+",Percentage= '"     +POut.PInt(cp.Percentage)+"'"
				+",PercentOverride= '"+POut.PInt(cp.PercentOverride)+"'"
				+",CopayAmt= '"       +POut.PDouble(cp.CopayAmt)+"'"
				+",OverrideInsEst= '" +POut.PDouble(cp.OverrideInsEst)+"'"
				+",NoBillIns= '"      +POut.PBool(cp.NoBillIns)+"'"
				+",DedBeforePerc= '"  +POut.PBool(cp.DedBeforePerc)+"'"
				+",OverAnnualMax= '"  +POut.PDouble(cp.OverAnnualMax)+"'"
				+",PaidOtherIns= '"   +POut.PDouble(cp.PaidOtherIns)+"'"
				+",BaseEst= '"        +POut.PDouble(cp.BaseEst)+"'"
				+",CopayOverride= '"  +POut.PDouble(cp.CopayOverride)+"'"
				+",ProcDate= "       +POut.PDate(cp.ProcDate)
				+",DateEntry= "      +POut.PDate(cp.DateEntry)
				+",LineNumber= '"     +POut.PInt(cp.LineNumber)+"'"
				+" WHERE claimprocnum = '"+POut.PInt(cp.ClaimProcNum)+"'";
			//MessageBox.Show(string command);
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ClaimProc cp) {
			string command= "DELETE from claimproc WHERE claimprocNum = '"+POut.PInt(cp.ClaimProcNum)+"'";
			General.NonQ(command);
		}

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

		///<summary>Used when creating a claim to create any missing claimProcs. Also used in FormProcEdit if click button to add Estimate.  Inserts it into db. It will still be altered after this to fill in the fields that actually attach it to the claim.</summary>
		public static void CreateEst(ClaimProc cp, Procedure proc, InsPlan plan) {
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
			cp.DateCP=proc.ProcDate;
			//Writeoff=0
			cp.AllowedOverride=-1;
			cp.Percentage=-1;
			cp.PercentOverride=-1;
			cp.CopayAmt=-1;
			cp.OverrideInsEst=-1;
			cp.NoBillIns=false;
			cp.OverAnnualMax=-1;
			cp.PaidOtherIns=-1;
			cp.BaseEst=0;
			cp.CopayOverride=-1;
			cp.ProcDate=proc.ProcDate;
			Insert(cp);
		}


		///<summary>Converts the supplied list into a list of ClaimProcs for one claim.</summary>
		public static ClaimProc[] GetForClaim(ClaimProc[] List,int claimNum){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForClaim=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum==claimNum){
					ALForClaim.Add(List[i]);  
				}
			}
			ClaimProc[] ForClaim=new ClaimProc[ALForClaim.Count];
			for(int i=0;i<ALForClaim.Count;i++){
				ForClaim[i]=(ClaimProc)ALForClaim[i];
			}
			return ForClaim;
		}

		///<summary>When sending or printing a claim, this converts the supplied list into a list of ClaimProcs that need to be sent.</summary>
		public static ClaimProc[] GetForSendClaim(ClaimProc[] List,int claimNum){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForClaim=new ArrayList();
			bool includeThis;
			for(int i=0;i<List.Length;i++){
				if(List[i].ClaimNum!=claimNum){
					continue;
				}
				if(List[i].ProcNum==0){
					continue;//skip payments
				}
				includeThis=true;
				for(int j=0;j<ALForClaim.Count;j++){//loop through existing claimprocs
					if(((ClaimProc)ALForClaim[j]).ProcNum==List[i].ProcNum){
						includeThis=false;//skip duplicate procedures
					}
				}
				if(includeThis)
					ALForClaim.Add(List[i]);
			}
			ClaimProc[] ForClaim=new ClaimProc[ALForClaim.Count];
			for(int i=0;i<ALForClaim.Count;i++){
				ForClaim[i]=(ClaimProc)ALForClaim[i];
			}
			return ForClaim;
		}

		///<summary>Gets all ClaimProcs for the current Procedure. The List must be all ClaimProcs for this patient.</summary>
		public static ClaimProc[] GetForProc(ClaimProc[] List,int procNum){
			//MessageBox.Show(List.Length.ToString());
			ArrayList ALForProc=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					ALForProc.Add(List[i]);  
				}
			}
			//need to sort by pri, sec, etc.  BUT,
			//the only way to do it would be to add an ordinal field to claimprocs or something similar.
			//Then a sorter could be built.  Otherwise, we don't know which order to put them in.
			//Maybe supply PatPlanList to this function, because it's ordered.
			//But, then if patient changes ins, it will 'forget' which is pri and which is sec.
			ClaimProc[] ForProc=new ClaimProc[ALForProc.Count];
			for(int i=0;i<ALForProc.Count;i++){
				ForProc[i]=(ClaimProc)ALForProc[i];
			}
			return ForProc;
		}

		///<summary>Used in TP module to get one estimate. The List must be all ClaimProcs for this patient. If estimate can't be found, then return null.  The procedure is always status TP, so there shouldn't be more than one estimate for one plan.</summary>
		public static ClaimProc GetEstimate(ClaimProc[] List,int procNum,int planNum) {
			for(int i=0;i<List.Length;i++) {
				if(List[i].ProcNum==procNum && List[i].PlanNum==planNum) {
					return List[i];
				}
			}
			return null;
		}

		///<summary>Used once in Account.  The insurance estimate based on all claimprocs with this procNum that are attached to claims. Includes status of NotReceived,Received, and Supplemental. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static string ProcDisplayInsEst(ClaimProc[] List,int procNum){
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
		public static double ProcEstNotReceived(ClaimProc[] List,int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].Status==ClaimProcStatus.NotReceived
					){
					retVal+=List[i].InsPayEst;
				}
			}
			return retVal;
		}
		
		///<summary>Used in Account and in PaySplitEdit. The insurance amount paid based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPay(ClaimProc[] List,int procNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					//&& List[i].InsPayAmt > 0//ins paid
					&& List[i].Status!=ClaimProcStatus.Preauth
					&& List[i].Status!=ClaimProcStatus.CapEstimate
					&& List[i].Status!=ClaimProcStatus.CapComplete
					&& List[i].Status!=ClaimProcStatus.Estimate){
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>Used in E-claims to get the amount paid by primary. The insurance amount paid by the planNum based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPayPri(ClaimProc[] List,int procNum,int planNum){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum
					&& List[i].PlanNum==planNum
					&& List[i].Status!=ClaimProcStatus.Preauth
					&& List[i].Status!=ClaimProcStatus.CapEstimate
					&& List[i].Status!=ClaimProcStatus.CapComplete
					&& List[i].Status!=ClaimProcStatus.Estimate)
				{
					retVal+=List[i].InsPayAmt;
				}
			}
			return retVal;
		}

		///<summary>Used once in Account on the Claim line.  The amount paid on a claim only by total, not including by procedure.  The list can be all ClaimProcs for patient, or just those for this claim.</summary>
		public static double ClaimByTotalOnly(ClaimProc[] List,int claimNum){
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
		public static double ClaimWriteoffByTotalOnly(ClaimProc[] List,int claimNum) {
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
		public static void SetForClaim(int claimNum,int claimPaymentNum,DateTime date,bool setAttached){
			string command= "UPDATE claimproc SET ClaimPaymentNum = ";
			if(setAttached){
				command+="'"+claimPaymentNum+"' ";
			}
			else{
				command+="'0' ";
			}
			command+=",DateCP="+POut.PDate(date)+" "
				+"WHERE claimnum = '"+claimNum+"' AND "
				+"inspayamt != 0 AND ("
				+"claimpaymentNum = '"+claimPaymentNum+"' OR claimpaymentNum = '0')";
			//MessageBox.Show(string command);
 			General.NonQ(command);
		}

		///<summary></summary>
		public static double ComputeBal(ClaimProc[] List){
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
					|| List[i].Status==ClaimProcStatus.CapClaim){//would only have a payamt if received
					retVal-=List[i].InsPayAmt;
				}
				else if(List[i].Status==ClaimProcStatus.NotReceived){
					if(!PrefB.GetBool("BalancesDontSubtractIns")){
						retVal-=List[i].InsPayEst;//this typically happens
					}
				}
				retVal-=List[i].WriteOff;
			}
			return retVal;
		}


	}

	


}









