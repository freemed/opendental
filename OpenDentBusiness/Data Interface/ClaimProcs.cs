using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimProcs{

		///<summary></summary>
		public static List<ClaimProc> Refresh(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProc>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * from claimproc "
				+"WHERE PatNum = '"+patNum.ToString()+"' ORDER BY LineNumber";
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table);
		}

		///<summary>When using family deduct or max, this gets all claimprocs for the given plan.  This info is needed to compute used and pending insurance.</summary>
		public static List<ClaimProc> RefreshFam(int planNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ClaimProc>>(MethodBase.GetCurrentMethod(),planNum);
			}
			string command=
				"SELECT * FROM claimproc "
				+"WHERE PlanNum = "+POut.PInt(planNum);
				//+" OR PatPlanNum = "+POut.PInt(patPlanNum);
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table);
		}

		private static List<ClaimProc> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<ClaimProc> retVal=new List<ClaimProc>();
			ClaimProc cp;
			for(int i=0;i<table.Rows.Count;i++){
				cp=new ClaimProc();
				cp.ClaimProcNum   = PIn.PInt   (table.Rows[i][0].ToString());
				cp.ProcNum        = PIn.PInt   (table.Rows[i][1].ToString());
				cp.ClaimNum       = PIn.PInt   (table.Rows[i][2].ToString());	
				cp.PatNum         = PIn.PInt   (table.Rows[i][3].ToString());
				cp.ProvNum        = PIn.PInt   (table.Rows[i][4].ToString());
				cp.FeeBilled      = PIn.PDouble(table.Rows[i][5].ToString());
				cp.InsPayEst      = PIn.PDouble(table.Rows[i][6].ToString());
				cp.DedApplied     = PIn.PDouble(table.Rows[i][7].ToString());
				cp.Status         = (ClaimProcStatus)PIn.PInt(table.Rows[i][8].ToString());
				cp.InsPayAmt      = PIn.PDouble(table.Rows[i][9].ToString());
				cp.Remarks        = PIn.PString(table.Rows[i][10].ToString());
				cp.ClaimPaymentNum= PIn.PInt   (table.Rows[i][11].ToString());
				cp.PlanNum        = PIn.PInt   (table.Rows[i][12].ToString());
				cp.DateCP         = PIn.PDate  (table.Rows[i][13].ToString());
				cp.WriteOff       = PIn.PDouble(table.Rows[i][14].ToString());
				cp.CodeSent       = PIn.PString(table.Rows[i][15].ToString());
				cp.AllowedOverride= PIn.PDouble(table.Rows[i][16].ToString());
				cp.Percentage     = PIn.PInt   (table.Rows[i][17].ToString());
				cp.PercentOverride= PIn.PInt   (table.Rows[i][18].ToString());
				cp.CopayAmt       = PIn.PDouble(table.Rows[i][19].ToString());
				cp.OverrideInsEst = PIn.PDouble(table.Rows[i][20].ToString());
				cp.NoBillIns      = PIn.PBool  (table.Rows[i][21].ToString());
				cp.DedBeforePerc  = PIn.PBool  (table.Rows[i][22].ToString());
				cp.OverAnnualMax  = PIn.PDouble(table.Rows[i][23].ToString());
				cp.PaidOtherIns   = PIn.PDouble(table.Rows[i][24].ToString());
				cp.BaseEst        = PIn.PDouble(table.Rows[i][25].ToString());
				cp.CopayOverride  = PIn.PDouble(table.Rows[i][26].ToString());
				cp.ProcDate       = PIn.PDate  (table.Rows[i][27].ToString());
				cp.DateEntry      = PIn.PDate  (table.Rows[i][28].ToString());
				cp.LineNumber     = PIn.PInt   (table.Rows[i][29].ToString());
				retVal.Add(cp);
			}
			return retVal;
		}

		///<summary></summary>
		public static void Insert(ClaimProc cp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cp);
				return;
			}
			if(PrefC.RandomKeys) {
				cp.ClaimProcNum=MiscData.GetKey("claimproc","ClaimProcNum");
			}
			string command= "INSERT INTO claimproc (";
			if(PrefC.RandomKeys) {
				command+="ClaimProcNum,";
			}
			command+="ProcNum,ClaimNum,PatNum,ProvNum"
				+",FeeBilled,InsPayEst,DedApplied,Status,InsPayAmt,Remarks,ClaimPaymentNum"
				+",PlanNum,DateCP,WriteOff,CodeSent,AllowedOverride,Percentage,PercentOverride"
				+",CopayAmt,OverrideInsEst,NoBillIns,DedBeforePerc,OverAnnualMax"
				+",PaidOtherIns,BaseEst,CopayOverride,ProcDate,DateEntry,LineNumber) VALUES(";
			if(PrefC.RandomKeys) {
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
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}else{//Assume MySQL
				command+="NOW()";
			}
			command+=", '"+POut.PInt(cp.LineNumber)+"')";
			//MessageBox.Show(string command);
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				cp.ClaimProcNum=Db.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(ClaimProc cp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cp);
				return;
			}
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
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(ClaimProc cp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),cp);
				return;
			}
			string command= "DELETE from claimproc WHERE claimprocNum = '"+POut.PInt(cp.ClaimProcNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Used when creating a claim to create any missing claimProcs. Also used in FormProcEdit if click button to add Estimate.  Inserts it into db. It will still be altered after this to fill in the fields that actually attach it to the claim.</summary>
		public static void CreateEst(ClaimProc cp, Procedure proc, InsPlan plan) {
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
		public static List<ClaimProc> GetForClaim(List<ClaimProc> list,int claimNum){
			//No need to check RemotingRole; no call to db.
			List<ClaimProc> retVal=new List<ClaimProc>();
			for(int i=0;i<list.Count;i++){
				if(list[i].ClaimNum==claimNum){
					retVal.Add(list[i]);  
				}
			}
			return retVal;
		}

		///<summary>When sending or printing a claim, this converts the supplied list into a list of ClaimProcs that need to be sent.</summary>
		public static List<ClaimProc> GetForSendClaim(List<ClaimProc> claimProcList,int claimNum){
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
		public static List<ClaimProc> GetForProc(List<ClaimProc> claimProcList,int procNum){
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
		public static ClaimProc GetEstimate(List<ClaimProc> claimProcList,int procNum,int planNum) {
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
		public static string ProcDisplayInsEst(ClaimProc[] List,int procNum){
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
		public static double ProcEstNotReceived(List<ClaimProc> claimProcList,int procNum) {
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
		
		///<summary>Used in Account and in PaySplitEdit. The insurance amount paid based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPay(List<ClaimProc> claimProcList,int procNum) {
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
		public static double ProcWriteoff(List<ClaimProc> claimProcList,int procNum){
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

		///<summary>Used in E-claims to get the amount paid by primary. The insurance amount paid by the planNum based on all claimprocs with this procNum. The list can be all ClaimProcs for patient, or just those for this procedure.</summary>
		public static double ProcInsPayPri(List<ClaimProc> claimProcList,int procNum,int planNum){
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<claimProcList.Count;i++) {
				if(claimProcList[i].ProcNum==procNum
					&& claimProcList[i].PlanNum==planNum
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

		///<summary>Used once in Account on the Claim line.  The amount paid on a claim only by total, not including by procedure.  The list can be all ClaimProcs for patient, or just those for this claim.</summary>
		public static double ClaimByTotalOnly(ClaimProc[] List,int claimNum){
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
		public static double ClaimWriteoffByTotalOnly(ClaimProc[] List,int claimNum) {
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
		public static void SetForClaim(int claimNum,int claimPaymentNum,DateTime date,bool setAttached){
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
			command+=",DateCP="+POut.PDate(date)+" "
				+"WHERE claimnum = '"+claimNum+"' AND "
				+"inspayamt != 0 AND ("
				+"claimpaymentNum = '"+claimPaymentNum+"' OR claimpaymentNum = '0')";
			//MessageBox.Show(string command);
 			Db.NonQ(command);
		}

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
					if(!PrefC.GetBool("BalancesDontSubtractIns")) {//this typically happens
						retVal-=List[i].InsPayEst;
						retVal-=List[i].WriteOff;
					}
				}
			}
			return retVal;
		}

		///<summary>After entering estimates from a preauth, this routine is called for each proc to override the ins est.</summary>
		public static void OverrideInsEst(int procNum,int planNum,double insPayEst,List<ClaimProc> claimProcList){
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
				claimProcList[i].OverrideInsEst=insPayEst;
				Update(claimProcList[i]);
			}
		}


	}

	


}









