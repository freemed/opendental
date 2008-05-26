using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Links procedures to claims.  Also links ins payments to procedures or claims.  Also used for estimating procedures even if no claim yet.  Warning: One proc might be linked twice to a given claim if insurance made two payments.  Many of the important fields are actually optional.  For instance, ProcNum is only required if itemizing ins payment, and ClaimNum is blank if Status=adjustment,cap,or estimate.</summary>
	public class ClaimProc{
		///<summary>Primary key.</summary>
		public int ClaimProcNum;
		///<summary>FK to procedurelog.ProcNum.</summary>
		public int ProcNum;
		///<summary>FK to claim.ClaimNum.</summary>
		public int ClaimNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Fee billed to insurance. Might not be the same as the actual fee.  The fee billed can be different than the actual procedure.  For instance, if you have set the insurance plan to bill insurance using UCR fees, then this field will contain the UCR fee instead of the fee that the patient was charged.</summary>
		public double FeeBilled;
		///<summary>Actual amount this carrier is expected to pay, after taking everything else into account. Considers annual max, override, percentAmt, copayAmt, deductible, etc. This estimate is computed automatically in TP module, and gets overwritten when sent to ins.</summary>
		public double InsPayEst;
		///<summary>Deductible applied to this procedure only. If not sent to ins yet, then this will be set to an estimated amount based on the order in the TP.  Will be overwritten when actually sent to ins.</summary>
		public double DedApplied;
		///<summary>Enum:ClaimProcStatus .</summary>
		public ClaimProcStatus Status;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>The remarks that insurance sends in the EOB about procedures.</summary>
		public string Remarks;
		///<summary>FK to claimpayment.ClaimPaymentNum(the insurance check).</summary>
		public int ClaimPaymentNum;
		///<summary>FK to insplan.PlanNum</summary>
		public int PlanNum;
		///<summary>This is the date that is used for payment reports and tracks the payment date.  Always exactly matches the date of the ClaimPayment it's attached to.  See the note under Ledgers.ComputePayments.  This will eventually not be used for aging. The ProcDate will instead be used. See ProcDate.</summary>
		public DateTime DateCP;
		///<summary>Amount not covered by ins which is written off</summary>
		public double WriteOff;
		///<summary>The procedure code that was sent to insurance. This is not necessarily the usual procedure code.  It will already have been trimmed to 5 char if it started with "D", or it could be the alternate code.  Not allowed to be blank if it is procedure.</summary>
		public string CodeSent;
		///<summary>-1 if blank which indicates allowed is same as fee. This is the amount that the percentage is based on. Usually the same as the fee, unless this ins plan has lower UCR. Could also be different for ins substitutions, like posterior composites. It is never changed automatically except to sometimes set it to -1.  During Procedure.ComputeEstimates/ClaimProc.ComputeBaseEst, an allowed amount is calculated on the fly, but is no longer saved here.</summary>
		public double AllowedOverride;
		///<summary>-1 if blank.  Otherwise a number between 0 and 100.  The percentage that insurance pays on this procedure, as determined from insurance categories. Not user editable.</summary>
		public int Percentage;
		///<summary>-1 if blank.  Otherwise a number between 0 and 100.  Can only be changed by user.</summary>
		public int PercentOverride;
		///<summary>-1 if blank. Calculated automatically. User can not edit but can use CopayOverride instead.  Opposite of InsEst, because this is the patient portion estimate.  Two different uses: 1. For capitation, this automates calculation of writeoff. 2. For any other insurance, it gets deducted during calculation as shown in the edit window. Neither use directly affects patient balance.</summary>
		public double CopayAmt;
		///<summary>-1 if blank. Lets user override the percentAmt. This field is not updated when recalculating and is only changed by user.</summary>
		public double OverrideInsEst;
		///<summary>Set to true to not bill to this insurance plan.</summary>
		public bool NoBillIns;
		///<summary>Set true to apply the deductible before the percentage instead of the usual way of applying it after.</summary>
		public bool DedBeforePerc;
		///<summary>-1 if blank. The amount to subtract during estimating because annual benefits have maxed out.</summary>
		public double OverAnnualMax;
		///<summary>-1 if blank. The amount paid by another insurance. This amount is then subtracted from what the current insurance would pay. So, always blank for primary claims.</summary>
		public double PaidOtherIns;
		///<summary>Always has a value. Used in TP, etc. The base estimate is the ((fee or allowedOverride)-Copay) x (percentage or percentOverride). Does not include all the extras like ded, annualMax,and paidOtherIns that InsPayEst will hold in future estimating.</summary>
		public double BaseEst;
		///<summary>-1 if blank.  See description of CopayAmt.  This lets the user set a copay that will never be overwritten by automatic calculations.</summary>
		public double CopayOverride;
		///<summary>Date of the procedure.  Currently only used for tracking annual insurance benefits remaining. Important in Adjustments to benefits.  For total claim payments, MUST be the date of the procedures to correctly figure benefits.  Will eventually transition to use this field to actually calculate aging.  See the note under Ledgers.ComputePayments.</summary>
		public DateTime ProcDate;
		///<summary>Date that it was changed to status received or supplemental.  It is usually attached to a claimPayment at that point, but not if user forgets.  This is still the date that it becomes important financial data.  Only applies if Received or Supplemental.  Otherwise, the date is disregarded.  User may never edit. Important in audit trail.</summary>
		public DateTime DateEntry;
		///<summary>Assigned when claim is created as a way to order the procs showing on a claim.  Really only used in Canadian claims for now as F07.</summary>
		public int LineNumber;

		///<summary>Returns a copy of this ClaimProc.</summary>
		public ClaimProc Copy(){
			ClaimProc cp=new ClaimProc();
			cp.ClaimProcNum=ClaimProcNum;
			cp.ProcNum=ProcNum;
			cp.ClaimNum=ClaimNum;
			cp.PatNum=PatNum;
			cp.ProvNum=ProvNum;
			cp.FeeBilled=FeeBilled;
			cp.InsPayEst=InsPayEst;
			cp.DedApplied=DedApplied;
			cp.Status=Status;
			cp.InsPayAmt=InsPayAmt;
			cp.Remarks=Remarks;
			cp.ClaimPaymentNum=ClaimPaymentNum;
			cp.PlanNum=PlanNum;
			cp.DateCP=DateCP;
			cp.WriteOff=WriteOff;
			cp.CodeSent=CodeSent;
			cp.AllowedOverride=AllowedOverride;
			cp.Percentage=Percentage;
			cp.PercentOverride=PercentOverride;
      cp.CopayAmt=CopayAmt;
			cp.OverrideInsEst=OverrideInsEst;
			cp.NoBillIns=NoBillIns;
			cp.DedBeforePerc=DedBeforePerc;
			cp.OverAnnualMax=OverAnnualMax;
			cp.PaidOtherIns=PaidOtherIns;
			cp.BaseEst=BaseEst;
			cp.CopayOverride=CopayOverride;
			cp.ProcDate=ProcDate;
			cp.DateEntry=DateEntry;
			cp.LineNumber=LineNumber;
			return cp;
		}

		




	}

}









