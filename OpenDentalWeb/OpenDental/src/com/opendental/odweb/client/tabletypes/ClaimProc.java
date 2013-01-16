package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ClaimProc {
		/** Primary key. */
		public int ClaimProcNum;
		/** FK to procedurelog.ProcNum. */
		public int ProcNum;
		/** FK to claim.ClaimNum. */
		public int ClaimNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to provider.ProvNum.  At least one office has been manually setting their claimproc provider to a different provider when entering payments as a means to track provider income.  So we can't force this to always be the same as the procedure.  We also don't want to change any historical data, so only synched when setting appt complete or if an estimate.  Right now on e-claims, we are sending the prov from the procedure.  When we have time, we will change e-claims to send the proc from the ClaimProc. */
		public int ProvNum;
		/** Fee billed to insurance. Might not be the same as the actual fee.  The fee billed can be different than the actual procedure.  For instance, if you have set the insurance plan to bill insurance using UCR fees, then this field will contain the UCR fee instead of the fee that the patient was charged. */
		public double FeeBilled;
		/** Only if attached to a claim.  Actual amount this carrier is expected to pay, after taking everything else into account. Considers annual max, override, percentAmt, copayAmt, deductible, etc. This estimate is computed automatically when sent to ins. */
		public double InsPayEst;
		/** 0 if blank.  Deductible applied to this procedure only. Only for procedures attached to claims.  Otherwise, the DedEst and DedEstOverride are used. */
		public double DedApplied;
		/** Enum:ClaimProcStatus . */
		public ClaimProcStatus Status;
		/** Amount insurance actually paid. */
		public double InsPayAmt;
		/** The remarks that insurance sends in the EOB about procedures. */
		public String Remarks;
		/** FK to claimpayment.ClaimPaymentNum(the insurance check). */
		public int ClaimPaymentNum;
		/** FK to insplan.PlanNum */
		public int PlanNum;
		/** This is the date that is used for payment reports and tracks the payment date.  Always exactly matches the date of the ClaimPayment it's attached to.  See the note under Ledgers.ComputePayments.  This will eventually not be used for aging. The ProcDate will instead be used. See ProcDate. */
		public Date DateCP;
		/** Amount not covered by ins which is written off.  The writeoff estimate goes in a different column. */
		public double WriteOff;
		/** The procedure code that was sent to insurance. This is not necessarily the usual procedure code.  It will already have been trimmed to 5 char if it started with "D", or it could be the alternate code.  Not allowed to be blank if it is procedure. */
		public String CodeSent;
		/** The allowed fee (not the override) is a complex calculation which is performed on the fly in Procedure.ComputeEstimates/ClaimProc.ComputeBaseEst.  It is the amount that the percentage is based on.  If this carrier has a lower UCR than the office, then the allowed fee is where that is handled.  It can be pulled from an allowed fee schedule.  It is also where substitutions for posterior composites are handled.  The AllowedOverride allows the user to override the calculation.  -1 indicates blank.  A new use of this field is for when entering insurance payments.  On the eob, it will tell you what the allowed/UCR fee is.  The user will now be able to enter this information into the AllowedOverride field.  They will simultaneously pass the info to the allowed fee schedule.  AllowedOverride is never changed automatically by the program except to sometimes set it to -1 if NoBillIns. */
		public double AllowedOverride;
		/** -1 if blank.  Otherwise a number between 0 and 100.  The percentage that insurance pays on this procedure, as determined from insurance categories. Not user editable. */
		public int Percentage;
		/** -1 if blank.  Otherwise a number between 0 and 100.  Can only be changed by user. */
		public int PercentOverride;
		/** -1 if blank. Calculated automatically. User cannot edit but can use CopayOverride instead.  Opposite of InsEst, because this is the patient portion estimate.  Two different uses: 1. For capitation, this automates calculation of writeoff. 2. For any other insurance, it gets deducted during calculation as shown in the edit window. Neither use directly affects patient balance. */
		public double CopayAmt;
		/** Set to true to not bill to this insurance plan. */
		public boolean NoBillIns;
		/** -1 if blank. The amount paid or estimated to be paid by another insurance.  This amount is then subtracted from what the current insurance would pay.  When running the calculation and considering other claimprocs, it will ignore any patPlan with a higher ordinal.  So, always blank for primary claims.  User cannot edit, but can use PaidOtherInsOverride. */
		public double PaidOtherIns;
		/** Always has a value. Used in TP, etc. The base estimate is the ((fee or allowedOverride)-Copay) x (percentage or percentOverride). Does not include all the extras like ded, annualMax,and paidOtherIns that InsEstTotal holds.  BaseEst cannot be overridden by the user.  Instead, the following fields can be manipulated: allowedOverride, CopayOverride, PercentOverride. */
		public double BaseEst;
		/** -1 if blank.  See description of CopayAmt.  This lets the user set a copay that will never be overwritten by automatic calculations. */
		public double CopayOverride;
		/** Date of the procedure.  Currently only used for tracking annual insurance benefits remaining. Important in Adjustments to benefits.  For total claim payments, MUST be the date of the procedures to correctly figure benefits.  Will eventually transition to use this field to actually calculate aging.  See the note under Ledgers.ComputePayments. */
		public Date ProcDate;
		/** Date that it was changed to status received or supplemental.  It is usually attached to a claimPayment at that point, but not if user forgets.  This is still the date that it becomes important financial data.  Only applies if Received or Supplemental.  Otherwise, the date is disregarded.  User may never edit. Important in audit trail. */
		public Date DateEntry;
		/** Assigned when claim is created as a way to order the procs showing on a claim.  Really only used in Canadian claims for now as F07. */
		public byte LineNumber;
		/** -1 if blank.  Not sure why we need to allow -1.  Calculated automatically.  User cannot edit, but can use DedEstOverride instead. */
		public double DedEst;
		/** -1 if blank.  Overrides the DedEst value. */
		public double DedEstOverride;
		/** Always has a value.  BaseEst-(DedEst or DedEstOverride)-PaidOtherIns-OverAnnualMax.  User cannot edit, but can instead use InsEstTotalOverride. */
		public double InsEstTotal;
		/** -1 if blank.  Overrides the InsEstTotal value. */
		public double InsEstTotalOverride;
		/** -1 if blank.  Overrides the PaidOtherIns value. */
		public double PaidOtherInsOverride;
		/** An automatically generated note that displays information about over max, exclusions, and other limitations for which there are no fields.  Only applies to estimate.  Once it's attached to a claim, similar information can go in the remarks field. */
		public String EstimateNote;
		/** -1 if blank.  The estimated writeoff as calculated by OD.  Usually only used for PPOs.  */
		public double WriteOffEst;
		/** -1 if blank.  Overrides WriteOffEst.  Usually only used for PPOs. */
		public double WriteOffEstOverride;
		/** FK to clinic.ClinicNum.  Can be zero.  No user interface for editing.  Forced to always be the same as the procedure, or if no procedure, then the claim. */
		public int ClinicNum;
		/** FK to inssub.InsSubNum. */
		public int InsSubNum;
		/** 1-indexed.  Allows user to sort the order of payments on an EOB.  All claimprocs for a payment will have the same PaymentRow value. */
		public int PaymentRow;
		/** Not a database column.  Used to help manage passing lists around. */
		public boolean DoDelete;

		/** Deep copy of object. */
		public ClaimProc deepCopy() {
			ClaimProc claimproc=new ClaimProc();
			claimproc.ClaimProcNum=this.ClaimProcNum;
			claimproc.ProcNum=this.ProcNum;
			claimproc.ClaimNum=this.ClaimNum;
			claimproc.PatNum=this.PatNum;
			claimproc.ProvNum=this.ProvNum;
			claimproc.FeeBilled=this.FeeBilled;
			claimproc.InsPayEst=this.InsPayEst;
			claimproc.DedApplied=this.DedApplied;
			claimproc.Status=this.Status;
			claimproc.InsPayAmt=this.InsPayAmt;
			claimproc.Remarks=this.Remarks;
			claimproc.ClaimPaymentNum=this.ClaimPaymentNum;
			claimproc.PlanNum=this.PlanNum;
			claimproc.DateCP=this.DateCP;
			claimproc.WriteOff=this.WriteOff;
			claimproc.CodeSent=this.CodeSent;
			claimproc.AllowedOverride=this.AllowedOverride;
			claimproc.Percentage=this.Percentage;
			claimproc.PercentOverride=this.PercentOverride;
			claimproc.CopayAmt=this.CopayAmt;
			claimproc.NoBillIns=this.NoBillIns;
			claimproc.PaidOtherIns=this.PaidOtherIns;
			claimproc.BaseEst=this.BaseEst;
			claimproc.CopayOverride=this.CopayOverride;
			claimproc.ProcDate=this.ProcDate;
			claimproc.DateEntry=this.DateEntry;
			claimproc.LineNumber=this.LineNumber;
			claimproc.DedEst=this.DedEst;
			claimproc.DedEstOverride=this.DedEstOverride;
			claimproc.InsEstTotal=this.InsEstTotal;
			claimproc.InsEstTotalOverride=this.InsEstTotalOverride;
			claimproc.PaidOtherInsOverride=this.PaidOtherInsOverride;
			claimproc.EstimateNote=this.EstimateNote;
			claimproc.WriteOffEst=this.WriteOffEst;
			claimproc.WriteOffEstOverride=this.WriteOffEstOverride;
			claimproc.ClinicNum=this.ClinicNum;
			claimproc.InsSubNum=this.InsSubNum;
			claimproc.PaymentRow=this.PaymentRow;
			claimproc.DoDelete=this.DoDelete;
			return claimproc;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimProc>");
			sb.append("<ClaimProcNum>").append(ClaimProcNum).append("</ClaimProcNum>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<FeeBilled>").append(FeeBilled).append("</FeeBilled>");
			sb.append("<InsPayEst>").append(InsPayEst).append("</InsPayEst>");
			sb.append("<DedApplied>").append(DedApplied).append("</DedApplied>");
			sb.append("<Status>").append(Status.ordinal()).append("</Status>");
			sb.append("<InsPayAmt>").append(InsPayAmt).append("</InsPayAmt>");
			sb.append("<Remarks>").append(Serializing.escapeForXml(Remarks)).append("</Remarks>");
			sb.append("<ClaimPaymentNum>").append(ClaimPaymentNum).append("</ClaimPaymentNum>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<DateCP>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateCP)).append("</DateCP>");
			sb.append("<WriteOff>").append(WriteOff).append("</WriteOff>");
			sb.append("<CodeSent>").append(Serializing.escapeForXml(CodeSent)).append("</CodeSent>");
			sb.append("<AllowedOverride>").append(AllowedOverride).append("</AllowedOverride>");
			sb.append("<Percentage>").append(Percentage).append("</Percentage>");
			sb.append("<PercentOverride>").append(PercentOverride).append("</PercentOverride>");
			sb.append("<CopayAmt>").append(CopayAmt).append("</CopayAmt>");
			sb.append("<NoBillIns>").append((NoBillIns)?1:0).append("</NoBillIns>");
			sb.append("<PaidOtherIns>").append(PaidOtherIns).append("</PaidOtherIns>");
			sb.append("<BaseEst>").append(BaseEst).append("</BaseEst>");
			sb.append("<CopayOverride>").append(CopayOverride).append("</CopayOverride>");
			sb.append("<ProcDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ProcDate)).append("</ProcDate>");
			sb.append("<DateEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEntry)).append("</DateEntry>");
			sb.append("<LineNumber>").append(LineNumber).append("</LineNumber>");
			sb.append("<DedEst>").append(DedEst).append("</DedEst>");
			sb.append("<DedEstOverride>").append(DedEstOverride).append("</DedEstOverride>");
			sb.append("<InsEstTotal>").append(InsEstTotal).append("</InsEstTotal>");
			sb.append("<InsEstTotalOverride>").append(InsEstTotalOverride).append("</InsEstTotalOverride>");
			sb.append("<PaidOtherInsOverride>").append(PaidOtherInsOverride).append("</PaidOtherInsOverride>");
			sb.append("<EstimateNote>").append(Serializing.escapeForXml(EstimateNote)).append("</EstimateNote>");
			sb.append("<WriteOffEst>").append(WriteOffEst).append("</WriteOffEst>");
			sb.append("<WriteOffEstOverride>").append(WriteOffEstOverride).append("</WriteOffEstOverride>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("<PaymentRow>").append(PaymentRow).append("</PaymentRow>");
			sb.append("<DoDelete>").append((DoDelete)?1:0).append("</DoDelete>");
			sb.append("</ClaimProc>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ClaimProcNum")!=null) {
					ClaimProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FeeBilled")!=null) {
					FeeBilled=Double.valueOf(Serializing.getXmlNodeValue(doc,"FeeBilled"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsPayEst")!=null) {
					InsPayEst=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsPayEst"));
				}
				if(Serializing.getXmlNodeValue(doc,"DedApplied")!=null) {
					DedApplied=Double.valueOf(Serializing.getXmlNodeValue(doc,"DedApplied"));
				}
				if(Serializing.getXmlNodeValue(doc,"Status")!=null) {
					Status=ClaimProcStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Status"))];
				}
				if(Serializing.getXmlNodeValue(doc,"InsPayAmt")!=null) {
					InsPayAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsPayAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"Remarks")!=null) {
					Remarks=Serializing.getXmlNodeValue(doc,"Remarks");
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimPaymentNum")!=null) {
					ClaimPaymentNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimPaymentNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateCP")!=null) {
					DateCP=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateCP"));
				}
				if(Serializing.getXmlNodeValue(doc,"WriteOff")!=null) {
					WriteOff=Double.valueOf(Serializing.getXmlNodeValue(doc,"WriteOff"));
				}
				if(Serializing.getXmlNodeValue(doc,"CodeSent")!=null) {
					CodeSent=Serializing.getXmlNodeValue(doc,"CodeSent");
				}
				if(Serializing.getXmlNodeValue(doc,"AllowedOverride")!=null) {
					AllowedOverride=Double.valueOf(Serializing.getXmlNodeValue(doc,"AllowedOverride"));
				}
				if(Serializing.getXmlNodeValue(doc,"Percentage")!=null) {
					Percentage=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Percentage"));
				}
				if(Serializing.getXmlNodeValue(doc,"PercentOverride")!=null) {
					PercentOverride=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PercentOverride"));
				}
				if(Serializing.getXmlNodeValue(doc,"CopayAmt")!=null) {
					CopayAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"CopayAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"NoBillIns")!=null) {
					NoBillIns=(Serializing.getXmlNodeValue(doc,"NoBillIns")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"PaidOtherIns")!=null) {
					PaidOtherIns=Double.valueOf(Serializing.getXmlNodeValue(doc,"PaidOtherIns"));
				}
				if(Serializing.getXmlNodeValue(doc,"BaseEst")!=null) {
					BaseEst=Double.valueOf(Serializing.getXmlNodeValue(doc,"BaseEst"));
				}
				if(Serializing.getXmlNodeValue(doc,"CopayOverride")!=null) {
					CopayOverride=Double.valueOf(Serializing.getXmlNodeValue(doc,"CopayOverride"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcDate")!=null) {
					ProcDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"ProcDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateEntry")!=null) {
					DateEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateEntry"));
				}
				if(Serializing.getXmlNodeValue(doc,"LineNumber")!=null) {
					LineNumber=Byte.valueOf(Serializing.getXmlNodeValue(doc,"LineNumber"));
				}
				if(Serializing.getXmlNodeValue(doc,"DedEst")!=null) {
					DedEst=Double.valueOf(Serializing.getXmlNodeValue(doc,"DedEst"));
				}
				if(Serializing.getXmlNodeValue(doc,"DedEstOverride")!=null) {
					DedEstOverride=Double.valueOf(Serializing.getXmlNodeValue(doc,"DedEstOverride"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsEstTotal")!=null) {
					InsEstTotal=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsEstTotal"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsEstTotalOverride")!=null) {
					InsEstTotalOverride=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsEstTotalOverride"));
				}
				if(Serializing.getXmlNodeValue(doc,"PaidOtherInsOverride")!=null) {
					PaidOtherInsOverride=Double.valueOf(Serializing.getXmlNodeValue(doc,"PaidOtherInsOverride"));
				}
				if(Serializing.getXmlNodeValue(doc,"EstimateNote")!=null) {
					EstimateNote=Serializing.getXmlNodeValue(doc,"EstimateNote");
				}
				if(Serializing.getXmlNodeValue(doc,"WriteOffEst")!=null) {
					WriteOffEst=Double.valueOf(Serializing.getXmlNodeValue(doc,"WriteOffEst"));
				}
				if(Serializing.getXmlNodeValue(doc,"WriteOffEstOverride")!=null) {
					WriteOffEstOverride=Double.valueOf(Serializing.getXmlNodeValue(doc,"WriteOffEstOverride"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsSubNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PaymentRow")!=null) {
					PaymentRow=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PaymentRow"));
				}
				if(Serializing.getXmlNodeValue(doc,"DoDelete")!=null) {
					DoDelete=(Serializing.getXmlNodeValue(doc,"DoDelete")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Claimproc Status.  The status must generally be the same as the claim, although it is sometimes not strictly enforced. */
		public enum ClaimProcStatus {
			/** 0: For claims that have been created or sent, but have not been received. */
			NotReceived,
			/** 1: For claims that have been received. */
			Received,
			/** 2: For preauthorizations. */
			Preauth,
			/** 3: The only place that this status is used is to make adjustments to benefits from the coverage window.  It is never attached to a claim. */
			Adjustment,
			/** 4:This differs from Received only slightly.  It's for additional payments on procedures already received.  Most fields are blank. */
			Supplemental,
			/** 5: CapClaim is used when you want to send a claim to a capitation insurance company.  These are similar to Supplemental in that there will always be a duplicate claimproc for a procedure. The first claimproc tracks the copay and writeoff, has a status of CapComplete, and is never attached to a claim. The second claimproc has status of CapClaim. */
			CapClaim,
			/** 6: Estimates have replaced the fields that were in the procedure table.  Once a procedure is complete, the claimprocstatus will still be Estimate.  An Estimate can be attached to a claim and status gets changed to NotReceived. */
			Estimate,
			/** 7: For capitation procedures that are complete.  This replaces the old procedurelog.CapCoPay field. This stores the copay and writeoff amounts.  The copay is only there for reference, while it is the writeoff that actually affects the balance. Never attached to a claim. If procedure is TP, then status will be CapEstimate.  Only set to CapComplete if procedure is Complete. */
			CapComplete,
			/** 8: For capitation procedures that are still estimates rather than complete.  When procedure is completed, this can be changed to CapComplete, but never to anything else. */
			CapEstimate
		}


}
