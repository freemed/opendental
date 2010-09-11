using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Links procedures to claims.  Also links ins payments to procedures or claims.  Also used for estimating procedures even if no claim yet.  Warning: One proc might be linked twice to a given claim if insurance made two payments.  Many of the important fields are actually optional.  For instance, ProcNum is only required if itemizing ins payment, and ClaimNum is blank if Status=adjustment,cap,or estimate.</summary>
	[Serializable()]
	public class ClaimProc:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ClaimProcNum;
		///<summary>FK to procedurelog.ProcNum.</summary>
		public long ProcNum;
		///<summary>FK to claim.ClaimNum.</summary>
		public long ClaimNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to provider.ProvNum.  At least one office has been manually setting their claimproc provider to a different provider when entering payments as a means to track provider income.  So we can't force this to always be the same as the procedure.  We also don't want to change any historical data, so only synched when setting appt complete or if an estimate.</summary>
		public long ProvNum;
		///<summary>Fee billed to insurance. Might not be the same as the actual fee.  The fee billed can be different than the actual procedure.  For instance, if you have set the insurance plan to bill insurance using UCR fees, then this field will contain the UCR fee instead of the fee that the patient was charged.</summary>
		public double FeeBilled;
		///<summary>Only if attached to a claim.  Actual amount this carrier is expected to pay, after taking everything else into account. Considers annual max, override, percentAmt, copayAmt, deductible, etc. This estimate is computed automatically when sent to ins.</summary>
		public double InsPayEst;
		///<summary>0 if blank.  Deductible applied to this procedure only. Only for procedures attached to claims.  Otherwise, the DedEst and DedEstOverride are used.</summary>
		public double DedApplied;
		///<summary>Enum:ClaimProcStatus .</summary>
		public ClaimProcStatus Status;
		///<summary>Amount insurance actually paid.</summary>
		public double InsPayAmt;
		///<summary>The remarks that insurance sends in the EOB about procedures.</summary>
		public string Remarks;
		///<summary>FK to claimpayment.ClaimPaymentNum(the insurance check).</summary>
		public long ClaimPaymentNum;
		///<summary>FK to insplan.PlanNum</summary>
		public long PlanNum;
		///<summary>This is the date that is used for payment reports and tracks the payment date.  Always exactly matches the date of the ClaimPayment it's attached to.  See the note under Ledgers.ComputePayments.  This will eventually not be used for aging. The ProcDate will instead be used. See ProcDate.</summary>
		public DateTime DateCP;
		///<summary>Amount not covered by ins which is written off.  The writeoff estimate goes in a different column.</summary>
		public double WriteOff;
		///<summary>The procedure code that was sent to insurance. This is not necessarily the usual procedure code.  It will already have been trimmed to 5 char if it started with "D", or it could be the alternate code.  Not allowed to be blank if it is procedure.</summary>
		public string CodeSent;
		///<summary>The allowed fee (not the override) is a complex calculation which is performed on the fly in Procedure.ComputeEstimates/ClaimProc.ComputeBaseEst.  It is the amount that the percentage is based on.  If this carrier has a lower UCR than the office, then the allowed fee is where that is handled.  It can be pulled from an allowed fee schedule.  It is also where substitutions for posterior composites are handled.  The AllowedOverride allows the user to override the calculation.  -1 indicates blank.  A new use of this field is for when entering insurance payments.  On the eob, it will tell you what the allowed/UCR fee is.  The user will now be able to enter this information into the AllowedOverride field.  They will simultaneously pass the info to the allowed fee schedule.  AllowedOverride is never changed automatically by the program except to sometimes set it to -1 if NoBillIns.</summary>
		public double AllowedOverride;
		///<summary>-1 if blank.  Otherwise a number between 0 and 100.  The percentage that insurance pays on this procedure, as determined from insurance categories. Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TinyIntUnsigned)]
		public int Percentage;
		///<summary>-1 if blank.  Otherwise a number between 0 and 100.  Can only be changed by user.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TinyIntUnsigned)]
		public int PercentOverride;
		///<summary>-1 if blank. Calculated automatically. User cannot edit but can use CopayOverride instead.  Opposite of InsEst, because this is the patient portion estimate.  Two different uses: 1. For capitation, this automates calculation of writeoff. 2. For any other insurance, it gets deducted during calculation as shown in the edit window. Neither use directly affects patient balance.</summary>
		public double CopayAmt;
		///<summary>Set to true to not bill to this insurance plan.</summary>
		public bool NoBillIns;
		///<summary>-1 if blank. The amount paid or estimated to be paid by another insurance.  This amount is then subtracted from what the current insurance would pay.  When running the calculation and considering other claimprocs, it will ignore any patPlan with a higher ordinal.  So, always blank for primary claims.  User cannot edit, but can use PaidOtherInsOverride.</summary>
		public double PaidOtherIns;
		///<summary>Always has a value. Used in TP, etc. The base estimate is the ((fee or allowedOverride)-Copay) x (percentage or percentOverride). Does not include all the extras like ded, annualMax,and paidOtherIns that InsEstTotal holds.  BaseEst cannot be overridden by the user.  Instead, the following fields can be manipulated: allowedOverride, CopayOverride, PercentOverride.</summary>
		public double BaseEst;
		///<summary>-1 if blank.  See description of CopayAmt.  This lets the user set a copay that will never be overwritten by automatic calculations.</summary>
		public double CopayOverride;
		///<summary>Date of the procedure.  Currently only used for tracking annual insurance benefits remaining. Important in Adjustments to benefits.  For total claim payments, MUST be the date of the procedures to correctly figure benefits.  Will eventually transition to use this field to actually calculate aging.  See the note under Ledgers.ComputePayments.</summary>
		public DateTime ProcDate;
		///<summary>Date that it was changed to status received or supplemental.  It is usually attached to a claimPayment at that point, but not if user forgets.  This is still the date that it becomes important financial data.  Only applies if Received or Supplemental.  Otherwise, the date is disregarded.  User may never edit. Important in audit trail.</summary>
		public DateTime DateEntry;
		///<summary>Assigned when claim is created as a way to order the procs showing on a claim.  Really only used in Canadian claims for now as F07.</summary>
		public byte LineNumber;
		///<summary>-1 if blank.  Not sure why we need to allow -1.  Calculated automatically.  User cannot edit, but can use DedEstOverride instead.</summary>
		public double DedEst;
		///<summary>-1 if blank.  Overrides the DedEst value.</summary>
		public double DedEstOverride;
		///<summary>Always has a value.  BaseEst-(DedEst or DedEstOverride)-PaidOtherIns-OverAnnualMax.  User cannot edit, but can instead use InsEstTotalOverride.</summary>
		public double InsEstTotal;
		///<summary>-1 if blank.  Overrides the InsEstTotal value.</summary>
		public double InsEstTotalOverride;
		///<summary>-1 if blank.  Overrides the PaidOtherIns value.</summary>
		public double PaidOtherInsOverride;
		///<summary>An automatically generated note that displays information about over max, exclusions, and other limitations for which there are no fields.  Only applies to estimate.  Once it's attached to a claim, similar information can go in the remarks field.</summary>
		public string EstimateNote;
		///<summary>-1 if blank.  The estimated writeoff as calculated by OD.  Usually only used for PPOs. </summary>
		public double WriteOffEst;
		///<summary>-1 if blank.  Overrides WriteOffEst.  Usually only used for PPOs.</summary>
		public double WriteOffEstOverride;
		///<summary>FK to clinic.ClinicNum.  Can be zero.  No user interface for editing.  Forced to always be the same as the procedure, or if no procedure, then the claim.</summary>
		public long ClinicNum;
		///<summary>Not a database column.  Used to help manage passing lists around.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public bool DoDelete;
		


		///<summary>Returns a copy of this ClaimProc.</summary>
		public ClaimProc Copy(){
			return (ClaimProc)MemberwiseClone();
		}

		public override bool Equals(object obj) {
			ClaimProc cp=(ClaimProc)obj;
			if(ClaimProcNum != cp.ClaimProcNum
				|| ProcNum != cp.ProcNum
				|| ClaimNum != cp.ClaimNum
				|| PatNum != cp.PatNum
				|| ProvNum != cp.ProvNum
				|| FeeBilled != cp.FeeBilled
				|| InsPayEst != cp.InsPayEst
				|| DedApplied != cp.DedApplied
				|| Status != cp.Status
				|| InsPayAmt != cp.InsPayAmt
				|| Remarks != cp.Remarks
				|| ClaimPaymentNum != cp.ClaimPaymentNum
				|| PlanNum != cp.PlanNum
				|| DateCP != cp.DateCP
				|| WriteOff != cp.WriteOff
				|| CodeSent != cp.CodeSent
				|| AllowedOverride != cp.AllowedOverride
				|| Percentage != cp.Percentage
				|| PercentOverride != cp.PercentOverride
				|| CopayAmt != cp.CopayAmt
				|| NoBillIns != cp.NoBillIns
				|| PaidOtherIns != cp.PaidOtherIns
				|| BaseEst != cp.BaseEst
				|| CopayOverride != cp.CopayOverride
			  || ProcDate != cp.ProcDate
				|| DateEntry != cp.DateEntry
				|| LineNumber != cp.LineNumber
				|| DedEst != cp.DedEst
				|| DedEstOverride != cp.DedEstOverride
				|| InsEstTotal != cp.InsEstTotal
				|| InsEstTotalOverride != cp.InsEstTotalOverride
				|| PaidOtherInsOverride != cp.PaidOtherInsOverride
				|| EstimateNote != cp.EstimateNote
				|| WriteOffEst != cp.WriteOffEst
				|| WriteOffEstOverride != cp.WriteOffEstOverride
				|| ClinicNum != cp.ClinicNum) 
			{
				return false;
			}
			return true;
		}

		public override string ToString() {
			return Status.ToString()+ProcDate.ToShortDateString()+" est:"+InsEstTotal.ToString()+" ded:"+DedEst.ToString();
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}




	}

}









