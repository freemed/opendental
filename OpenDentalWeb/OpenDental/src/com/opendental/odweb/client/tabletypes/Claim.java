package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Claim {
		/** Primary key */
		public int ClaimNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** Usually the same date as the procedures, but it can be changed if you wish. */
		public Date DateService;
		/** Usually the date it was created.  It might be sent a few days later if you don't send your e-claims every day. */
		public Date DateSent;
		/** Single char: U,H,W,P,S,or R.  U=Unsent, H=Hold until pri received, W=Waiting in queue, S=Sent, R=Received.  A(adj) is no longer used.  P(prob sent) is no longer used. */
		public String ClaimStatus;
		/** Date the claim was received. */
		public Date DateReceived;
		/** FK to insplan.PlanNum.  Every claim is attached to one plan. */
		public int PlanNum;
		/** FK to provider.ProvNum.  Treating provider for dental claims.  For institutional claims, this is called the attending provider. */
		public int ProvTreat;
		/** Total fee of claim. */
		public double ClaimFee;
		/** Amount insurance is estimated to pay on this claim. */
		public double InsPayEst;
		/** Amount insurance actually paid. */
		public double InsPayAmt;
		/** Deductible applied to this claim. */
		public double DedApplied;
		/** The predetermination of benefits number received from ins.  In X12, REF G3. */
		public String PreAuthString;
		/** Single char for No, Initial, or Replacement. */
		public String IsProsthesis;
		/** Date prior prosthesis was placed.  Note that this is only for paper claims.  E-claims have a date field on each individual procedure. */
		public Date PriorDate;
		/** Note for patient for why insurance didn't pay as expected. */
		public String ReasonUnderPaid;
		/** Note to be sent to insurance. Max 255 char.  E-claims also have notes on each procedure. */
		public String ClaimNote;
		/** "P"=primary, "S"=secondary, "PreAuth"=preauth, "Other"=other, "Cap"=capitation.  Not allowed to be blank. Might need to add "Med"=medical claim. */
		public String ClaimType;
		/** FK to provider.ProvNum.  Billing provider.  Assignment can be automated from the setup section. */
		public int ProvBill;
		/** FK to referral.ReferralNum. */
		public int ReferringProv;
		/** Referral number for this claim. */
		public String RefNumString;
		/** Enum:PlaceOfService . */
		public PlaceOfService PlaceService;
		/** blank or A=Auto, E=Employment, O=Other. */
		public String AccidentRelated;
		/** Date of accident, if applicable. */
		public Date AccidentDate;
		/** Accident state. */
		public String AccidentST;
		/** Enum:YN . */
		public YN EmployRelated;
		/** True if is ortho. */
		public boolean IsOrtho;
		/** Remaining months of ortho. Valid values are 1-36. */
		public byte OrthoRemainM;
		/** Date ortho appliance placed. */
		public Date OrthoDate;
		/** Enum:Relat  Relationship to subscriber.  The relationship is copied from InsPlan when the claim is created.  It might need to be changed in both places. */
		public Relat PatRelat;
		/** FK to insplan.PlanNum.  Other coverage plan number.  0 if none.  This provides the user with total control over what other coverage shows. This obviously limits the coverage on a single claim to two insurance companies. */
		public int PlanNum2;
		/** Enum:Relat  The relationship to the subscriber for other coverage on this claim. */
		public Relat PatRelat2;
		/** Sum of ClaimProc.Writeoff for this claim. */
		public double WriteOff;
		/** The number of x-rays enclosed. */
		public byte Radiographs;
		/** FK to clinic.ClinicNum.  0 if no clinic.  Since one claim cannot have procs from multiple clinics, the clinicNum is set when creating the claim and then cannot be changed.  The claim would have to be deleted and recreated.  Otherwise, if changing at the claim level, a feature would have to be added that synched all procs, claimprocs, and probably some other tables. */
		public int ClinicNum;
		/** FK to claimform.ClaimFormNum.  0 if not assigned to use the claimform for the insplan. */
		public int ClaimForm;
		/** The number of intraoral images attached.  Not the number of files attached.  This is the value that goes on the 2006 claimform. */
		public int AttachedImages;
		/** The number of models attached. */
		public int AttachedModels;
		/** A comma-delimited set of flag keywords.  Can have one or more of the following: EoB,Note,Perio,Misc.  Must also contain one of these: Mail or Elect. */
		public String AttachedFlags;
		/** Example: NEA#1234567.  If present, and if the claim note does not already start with this Id, then it will be prepended to the claim note for both e-claims and mail.  If using e-claims, this same ID will be used for all PWK segements. */
		public String AttachmentID;
		/** A08.  Any combination of E(email), C(correspondence), M(models), X(x-rays), and I(images).  So up to 5 char.  Gets converted to a single char A-Z for e-claims. */
		public String CanadianMaterialsForwarded;
		/** B05.  Optional. The 9-digit CDA number of the referring provider, or identifier of referring party up to 10 characters in length. */
		public String CanadianReferralProviderNum;
		/** B06.  A number 0(none) through 13. */
		public byte CanadianReferralReason;
		/** F18.  Y, N, or X(not a lower denture, crown, or bridge). */
		public String CanadianIsInitialLower;
		/** F19.  Mandatory if F18 is N. */
		public Date CanadianDateInitialLower;
		/** F21.  If crown, not required.  If denture or bridge, required if F18 is N.  Single digit number code, 0-6.  We added type 7, which is crown. */
		public byte CanadianMandProsthMaterial;
		/** F15.  Y, N, or X(not an upper denture, crown, or bridge). */
		public String CanadianIsInitialUpper;
		/** F04.  Mandatory if F15 is N. */
		public Date CanadianDateInitialUpper;
		/** F20.  If crown, not required.  If denture or bridge, required if F15 is N.  0 indicates empty response.  Single digit number code, 1-6.  We added type 7, which is crown. */
		public byte CanadianMaxProsthMaterial;
		/** FK to inssub.InsSubNum. */
		public int InsSubNum;
		/** FK to inssub.InsSubNum. */
		public int InsSubNum2;
		/** G01 assigned by carrier/network and returned in acks.  Used for claim reversal. */
		public String CanadaTransRefNum;
		/** F37 Used for predeterminations. */
		public Date CanadaEstTreatStartDate;
		/** F28 Used for predeterminations. */
		public double CanadaInitialPayment;
		/** F29 Used for predeterminations. */
		public byte CanadaPaymentMode;
		/** F30 Used for predeterminations. */
		public byte CanadaTreatDuration;
		/** F31 Used for predeterminations. */
		public byte CanadaNumAnticipatedPayments;
		/** F32 Used for predeterminations. */
		public double CanadaAnticipatedPayAmount;
		/** This is NOT the predetermination of benefits number.  In X12, this is REF G1. */
		public String PriorAuthorizationNumber;
		/** Enum:EnumClaimSpecialProgram  This is used to track EPSDT. */
		public EnumClaimSpecialProgram SpecialProgramCode;
		/** A three digit number used on 837I.  Aka Bill Code.  UBO4 4.  Examples: 321,823,131,652.  The third digit is claim frequency code.  If this is used, then our CorrectionType should be 0=original. */
		public String UniformBillType;
		/** Enum:EnumClaimMedType 0=Dental, 1=Medical, 2=Institutional */
		public EnumClaimMedType MedType;
		/** Used for inst claims. Single digit.  X12 2300 CL101.  UB04 14.  Should only be required for IP, but X12 clearly states required for all. */
		public String AdmissionTypeCode;
		/** Used for inst claims. Single char.  X12 2300 CL102.  UB04 15.  Should only be required for IP, but X12 clearly states required for all. */
		public String AdmissionSourceCode;
		/** Used for inst claims. Two digit.  X12 2300 CL103.  UB04 17.  Should only be required for IP, but X12 clearly states required for all. */
		public String PatientStatusCode;
		/** FK to definition.DefNum. Most users will leave this blank.  Some offices may set up tracking statuses such as 'review', 'hold', 'riskmanage', etc. */
		public int CustomTracking;
		/** Used for historical purposes only, not sent electronically. Automatically set when CorrectionType is not original and the claim is resent. */
		public Date DateResent;
		/** X12 CLM05-3. Usually set to original, but can be used to resubmit claims. */
		public ClaimCorrectionType CorrectionType;
		/** X12 CLM01. Unique identifier for the claim within the current database. Defaults to PatNum/ClaimNum, but can be edited by user. */
		public String ClaimIdentifier;
		/** X12 2300 REF (F8). Used when resending claims to refer to the original claim. The user must type this value in after reading it from the original claim response report. */
		public String OrigRefNum;

		/** Deep copy of object. */
		public Claim Copy() {
			Claim claim=new Claim();
			claim.ClaimNum=this.ClaimNum;
			claim.PatNum=this.PatNum;
			claim.DateService=this.DateService;
			claim.DateSent=this.DateSent;
			claim.ClaimStatus=this.ClaimStatus;
			claim.DateReceived=this.DateReceived;
			claim.PlanNum=this.PlanNum;
			claim.ProvTreat=this.ProvTreat;
			claim.ClaimFee=this.ClaimFee;
			claim.InsPayEst=this.InsPayEst;
			claim.InsPayAmt=this.InsPayAmt;
			claim.DedApplied=this.DedApplied;
			claim.PreAuthString=this.PreAuthString;
			claim.IsProsthesis=this.IsProsthesis;
			claim.PriorDate=this.PriorDate;
			claim.ReasonUnderPaid=this.ReasonUnderPaid;
			claim.ClaimNote=this.ClaimNote;
			claim.ClaimType=this.ClaimType;
			claim.ProvBill=this.ProvBill;
			claim.ReferringProv=this.ReferringProv;
			claim.RefNumString=this.RefNumString;
			claim.PlaceService=this.PlaceService;
			claim.AccidentRelated=this.AccidentRelated;
			claim.AccidentDate=this.AccidentDate;
			claim.AccidentST=this.AccidentST;
			claim.EmployRelated=this.EmployRelated;
			claim.IsOrtho=this.IsOrtho;
			claim.OrthoRemainM=this.OrthoRemainM;
			claim.OrthoDate=this.OrthoDate;
			claim.PatRelat=this.PatRelat;
			claim.PlanNum2=this.PlanNum2;
			claim.PatRelat2=this.PatRelat2;
			claim.WriteOff=this.WriteOff;
			claim.Radiographs=this.Radiographs;
			claim.ClinicNum=this.ClinicNum;
			claim.ClaimForm=this.ClaimForm;
			claim.AttachedImages=this.AttachedImages;
			claim.AttachedModels=this.AttachedModels;
			claim.AttachedFlags=this.AttachedFlags;
			claim.AttachmentID=this.AttachmentID;
			claim.CanadianMaterialsForwarded=this.CanadianMaterialsForwarded;
			claim.CanadianReferralProviderNum=this.CanadianReferralProviderNum;
			claim.CanadianReferralReason=this.CanadianReferralReason;
			claim.CanadianIsInitialLower=this.CanadianIsInitialLower;
			claim.CanadianDateInitialLower=this.CanadianDateInitialLower;
			claim.CanadianMandProsthMaterial=this.CanadianMandProsthMaterial;
			claim.CanadianIsInitialUpper=this.CanadianIsInitialUpper;
			claim.CanadianDateInitialUpper=this.CanadianDateInitialUpper;
			claim.CanadianMaxProsthMaterial=this.CanadianMaxProsthMaterial;
			claim.InsSubNum=this.InsSubNum;
			claim.InsSubNum2=this.InsSubNum2;
			claim.CanadaTransRefNum=this.CanadaTransRefNum;
			claim.CanadaEstTreatStartDate=this.CanadaEstTreatStartDate;
			claim.CanadaInitialPayment=this.CanadaInitialPayment;
			claim.CanadaPaymentMode=this.CanadaPaymentMode;
			claim.CanadaTreatDuration=this.CanadaTreatDuration;
			claim.CanadaNumAnticipatedPayments=this.CanadaNumAnticipatedPayments;
			claim.CanadaAnticipatedPayAmount=this.CanadaAnticipatedPayAmount;
			claim.PriorAuthorizationNumber=this.PriorAuthorizationNumber;
			claim.SpecialProgramCode=this.SpecialProgramCode;
			claim.UniformBillType=this.UniformBillType;
			claim.MedType=this.MedType;
			claim.AdmissionTypeCode=this.AdmissionTypeCode;
			claim.AdmissionSourceCode=this.AdmissionSourceCode;
			claim.PatientStatusCode=this.PatientStatusCode;
			claim.CustomTracking=this.CustomTracking;
			claim.DateResent=this.DateResent;
			claim.CorrectionType=this.CorrectionType;
			claim.ClaimIdentifier=this.ClaimIdentifier;
			claim.OrigRefNum=this.OrigRefNum;
			return claim;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Claim>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateService>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateService)).append("</DateService>");
			sb.append("<DateSent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateSent)).append("</DateSent>");
			sb.append("<ClaimStatus>").append(Serializing.EscapeForXml(ClaimStatus)).append("</ClaimStatus>");
			sb.append("<DateReceived>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateReceived)).append("</DateReceived>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<ProvTreat>").append(ProvTreat).append("</ProvTreat>");
			sb.append("<ClaimFee>").append(ClaimFee).append("</ClaimFee>");
			sb.append("<InsPayEst>").append(InsPayEst).append("</InsPayEst>");
			sb.append("<InsPayAmt>").append(InsPayAmt).append("</InsPayAmt>");
			sb.append("<DedApplied>").append(DedApplied).append("</DedApplied>");
			sb.append("<PreAuthString>").append(Serializing.EscapeForXml(PreAuthString)).append("</PreAuthString>");
			sb.append("<IsProsthesis>").append(Serializing.EscapeForXml(IsProsthesis)).append("</IsProsthesis>");
			sb.append("<PriorDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(PriorDate)).append("</PriorDate>");
			sb.append("<ReasonUnderPaid>").append(Serializing.EscapeForXml(ReasonUnderPaid)).append("</ReasonUnderPaid>");
			sb.append("<ClaimNote>").append(Serializing.EscapeForXml(ClaimNote)).append("</ClaimNote>");
			sb.append("<ClaimType>").append(Serializing.EscapeForXml(ClaimType)).append("</ClaimType>");
			sb.append("<ProvBill>").append(ProvBill).append("</ProvBill>");
			sb.append("<ReferringProv>").append(ReferringProv).append("</ReferringProv>");
			sb.append("<RefNumString>").append(Serializing.EscapeForXml(RefNumString)).append("</RefNumString>");
			sb.append("<PlaceService>").append(PlaceService.ordinal()).append("</PlaceService>");
			sb.append("<AccidentRelated>").append(Serializing.EscapeForXml(AccidentRelated)).append("</AccidentRelated>");
			sb.append("<AccidentDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AccidentDate)).append("</AccidentDate>");
			sb.append("<AccidentST>").append(Serializing.EscapeForXml(AccidentST)).append("</AccidentST>");
			sb.append("<EmployRelated>").append(EmployRelated.ordinal()).append("</EmployRelated>");
			sb.append("<IsOrtho>").append((IsOrtho)?1:0).append("</IsOrtho>");
			sb.append("<OrthoRemainM>").append(OrthoRemainM).append("</OrthoRemainM>");
			sb.append("<OrthoDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(OrthoDate)).append("</OrthoDate>");
			sb.append("<PatRelat>").append(PatRelat.ordinal()).append("</PatRelat>");
			sb.append("<PlanNum2>").append(PlanNum2).append("</PlanNum2>");
			sb.append("<PatRelat2>").append(PatRelat2.ordinal()).append("</PatRelat2>");
			sb.append("<WriteOff>").append(WriteOff).append("</WriteOff>");
			sb.append("<Radiographs>").append(Radiographs).append("</Radiographs>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<ClaimForm>").append(ClaimForm).append("</ClaimForm>");
			sb.append("<AttachedImages>").append(AttachedImages).append("</AttachedImages>");
			sb.append("<AttachedModels>").append(AttachedModels).append("</AttachedModels>");
			sb.append("<AttachedFlags>").append(Serializing.EscapeForXml(AttachedFlags)).append("</AttachedFlags>");
			sb.append("<AttachmentID>").append(Serializing.EscapeForXml(AttachmentID)).append("</AttachmentID>");
			sb.append("<CanadianMaterialsForwarded>").append(Serializing.EscapeForXml(CanadianMaterialsForwarded)).append("</CanadianMaterialsForwarded>");
			sb.append("<CanadianReferralProviderNum>").append(Serializing.EscapeForXml(CanadianReferralProviderNum)).append("</CanadianReferralProviderNum>");
			sb.append("<CanadianReferralReason>").append(CanadianReferralReason).append("</CanadianReferralReason>");
			sb.append("<CanadianIsInitialLower>").append(Serializing.EscapeForXml(CanadianIsInitialLower)).append("</CanadianIsInitialLower>");
			sb.append("<CanadianDateInitialLower>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CanadianDateInitialLower)).append("</CanadianDateInitialLower>");
			sb.append("<CanadianMandProsthMaterial>").append(CanadianMandProsthMaterial).append("</CanadianMandProsthMaterial>");
			sb.append("<CanadianIsInitialUpper>").append(Serializing.EscapeForXml(CanadianIsInitialUpper)).append("</CanadianIsInitialUpper>");
			sb.append("<CanadianDateInitialUpper>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CanadianDateInitialUpper)).append("</CanadianDateInitialUpper>");
			sb.append("<CanadianMaxProsthMaterial>").append(CanadianMaxProsthMaterial).append("</CanadianMaxProsthMaterial>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("<InsSubNum2>").append(InsSubNum2).append("</InsSubNum2>");
			sb.append("<CanadaTransRefNum>").append(Serializing.EscapeForXml(CanadaTransRefNum)).append("</CanadaTransRefNum>");
			sb.append("<CanadaEstTreatStartDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CanadaEstTreatStartDate)).append("</CanadaEstTreatStartDate>");
			sb.append("<CanadaInitialPayment>").append(CanadaInitialPayment).append("</CanadaInitialPayment>");
			sb.append("<CanadaPaymentMode>").append(CanadaPaymentMode).append("</CanadaPaymentMode>");
			sb.append("<CanadaTreatDuration>").append(CanadaTreatDuration).append("</CanadaTreatDuration>");
			sb.append("<CanadaNumAnticipatedPayments>").append(CanadaNumAnticipatedPayments).append("</CanadaNumAnticipatedPayments>");
			sb.append("<CanadaAnticipatedPayAmount>").append(CanadaAnticipatedPayAmount).append("</CanadaAnticipatedPayAmount>");
			sb.append("<PriorAuthorizationNumber>").append(Serializing.EscapeForXml(PriorAuthorizationNumber)).append("</PriorAuthorizationNumber>");
			sb.append("<SpecialProgramCode>").append(SpecialProgramCode.ordinal()).append("</SpecialProgramCode>");
			sb.append("<UniformBillType>").append(Serializing.EscapeForXml(UniformBillType)).append("</UniformBillType>");
			sb.append("<MedType>").append(MedType.ordinal()).append("</MedType>");
			sb.append("<AdmissionTypeCode>").append(Serializing.EscapeForXml(AdmissionTypeCode)).append("</AdmissionTypeCode>");
			sb.append("<AdmissionSourceCode>").append(Serializing.EscapeForXml(AdmissionSourceCode)).append("</AdmissionSourceCode>");
			sb.append("<PatientStatusCode>").append(Serializing.EscapeForXml(PatientStatusCode)).append("</PatientStatusCode>");
			sb.append("<CustomTracking>").append(CustomTracking).append("</CustomTracking>");
			sb.append("<DateResent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateResent)).append("</DateResent>");
			sb.append("<CorrectionType>").append(CorrectionType.ordinal()).append("</CorrectionType>");
			sb.append("<ClaimIdentifier>").append(Serializing.EscapeForXml(ClaimIdentifier)).append("</ClaimIdentifier>");
			sb.append("<OrigRefNum>").append(Serializing.EscapeForXml(OrigRefNum)).append("</OrigRefNum>");
			sb.append("</Claim>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateService")!=null) {
					DateService=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateService"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateSent")!=null) {
					DateSent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateSent"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimStatus")!=null) {
					ClaimStatus=Serializing.GetXmlNodeValue(doc,"ClaimStatus");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateReceived")!=null) {
					DateReceived=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateReceived"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvTreat")!=null) {
					ProvTreat=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvTreat"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimFee")!=null) {
					ClaimFee=Double.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimFee"));
				}
				if(Serializing.GetXmlNodeValue(doc,"InsPayEst")!=null) {
					InsPayEst=Double.valueOf(Serializing.GetXmlNodeValue(doc,"InsPayEst"));
				}
				if(Serializing.GetXmlNodeValue(doc,"InsPayAmt")!=null) {
					InsPayAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"InsPayAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DedApplied")!=null) {
					DedApplied=Double.valueOf(Serializing.GetXmlNodeValue(doc,"DedApplied"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PreAuthString")!=null) {
					PreAuthString=Serializing.GetXmlNodeValue(doc,"PreAuthString");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsProsthesis")!=null) {
					IsProsthesis=Serializing.GetXmlNodeValue(doc,"IsProsthesis");
				}
				if(Serializing.GetXmlNodeValue(doc,"PriorDate")!=null) {
					PriorDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"PriorDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ReasonUnderPaid")!=null) {
					ReasonUnderPaid=Serializing.GetXmlNodeValue(doc,"ReasonUnderPaid");
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimNote")!=null) {
					ClaimNote=Serializing.GetXmlNodeValue(doc,"ClaimNote");
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimType")!=null) {
					ClaimType=Serializing.GetXmlNodeValue(doc,"ClaimType");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvBill")!=null) {
					ProvBill=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvBill"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ReferringProv")!=null) {
					ReferringProv=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ReferringProv"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RefNumString")!=null) {
					RefNumString=Serializing.GetXmlNodeValue(doc,"RefNumString");
				}
				if(Serializing.GetXmlNodeValue(doc,"PlaceService")!=null) {
					PlaceService=PlaceOfService.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlaceService"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"AccidentRelated")!=null) {
					AccidentRelated=Serializing.GetXmlNodeValue(doc,"AccidentRelated");
				}
				if(Serializing.GetXmlNodeValue(doc,"AccidentDate")!=null) {
					AccidentDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"AccidentDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AccidentST")!=null) {
					AccidentST=Serializing.GetXmlNodeValue(doc,"AccidentST");
				}
				if(Serializing.GetXmlNodeValue(doc,"EmployRelated")!=null) {
					EmployRelated=YN.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployRelated"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"IsOrtho")!=null) {
					IsOrtho=(Serializing.GetXmlNodeValue(doc,"IsOrtho")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"OrthoRemainM")!=null) {
					OrthoRemainM=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"OrthoRemainM"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OrthoDate")!=null) {
					OrthoDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"OrthoDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatRelat")!=null) {
					PatRelat=Relat.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatRelat"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"PlanNum2")!=null) {
					PlanNum2=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PlanNum2"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatRelat2")!=null) {
					PatRelat2=Relat.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatRelat2"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"WriteOff")!=null) {
					WriteOff=Double.valueOf(Serializing.GetXmlNodeValue(doc,"WriteOff"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Radiographs")!=null) {
					Radiographs=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"Radiographs"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimForm")!=null) {
					ClaimForm=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimForm"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AttachedImages")!=null) {
					AttachedImages=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AttachedImages"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AttachedModels")!=null) {
					AttachedModels=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AttachedModels"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AttachedFlags")!=null) {
					AttachedFlags=Serializing.GetXmlNodeValue(doc,"AttachedFlags");
				}
				if(Serializing.GetXmlNodeValue(doc,"AttachmentID")!=null) {
					AttachmentID=Serializing.GetXmlNodeValue(doc,"AttachmentID");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianMaterialsForwarded")!=null) {
					CanadianMaterialsForwarded=Serializing.GetXmlNodeValue(doc,"CanadianMaterialsForwarded");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianReferralProviderNum")!=null) {
					CanadianReferralProviderNum=Serializing.GetXmlNodeValue(doc,"CanadianReferralProviderNum");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianReferralReason")!=null) {
					CanadianReferralReason=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CanadianReferralReason"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianIsInitialLower")!=null) {
					CanadianIsInitialLower=Serializing.GetXmlNodeValue(doc,"CanadianIsInitialLower");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianDateInitialLower")!=null) {
					CanadianDateInitialLower=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"CanadianDateInitialLower"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianMandProsthMaterial")!=null) {
					CanadianMandProsthMaterial=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CanadianMandProsthMaterial"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianIsInitialUpper")!=null) {
					CanadianIsInitialUpper=Serializing.GetXmlNodeValue(doc,"CanadianIsInitialUpper");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianDateInitialUpper")!=null) {
					CanadianDateInitialUpper=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"CanadianDateInitialUpper"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadianMaxProsthMaterial")!=null) {
					CanadianMaxProsthMaterial=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CanadianMaxProsthMaterial"));
				}
				if(Serializing.GetXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InsSubNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"InsSubNum2")!=null) {
					InsSubNum2=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"InsSubNum2"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadaTransRefNum")!=null) {
					CanadaTransRefNum=Serializing.GetXmlNodeValue(doc,"CanadaTransRefNum");
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadaEstTreatStartDate")!=null) {
					CanadaEstTreatStartDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"CanadaEstTreatStartDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadaInitialPayment")!=null) {
					CanadaInitialPayment=Double.valueOf(Serializing.GetXmlNodeValue(doc,"CanadaInitialPayment"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadaPaymentMode")!=null) {
					CanadaPaymentMode=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CanadaPaymentMode"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadaTreatDuration")!=null) {
					CanadaTreatDuration=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CanadaTreatDuration"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadaNumAnticipatedPayments")!=null) {
					CanadaNumAnticipatedPayments=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"CanadaNumAnticipatedPayments"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CanadaAnticipatedPayAmount")!=null) {
					CanadaAnticipatedPayAmount=Double.valueOf(Serializing.GetXmlNodeValue(doc,"CanadaAnticipatedPayAmount"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PriorAuthorizationNumber")!=null) {
					PriorAuthorizationNumber=Serializing.GetXmlNodeValue(doc,"PriorAuthorizationNumber");
				}
				if(Serializing.GetXmlNodeValue(doc,"SpecialProgramCode")!=null) {
					SpecialProgramCode=EnumClaimSpecialProgram.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SpecialProgramCode"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"UniformBillType")!=null) {
					UniformBillType=Serializing.GetXmlNodeValue(doc,"UniformBillType");
				}
				if(Serializing.GetXmlNodeValue(doc,"MedType")!=null) {
					MedType=EnumClaimMedType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MedType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"AdmissionTypeCode")!=null) {
					AdmissionTypeCode=Serializing.GetXmlNodeValue(doc,"AdmissionTypeCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"AdmissionSourceCode")!=null) {
					AdmissionSourceCode=Serializing.GetXmlNodeValue(doc,"AdmissionSourceCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"PatientStatusCode")!=null) {
					PatientStatusCode=Serializing.GetXmlNodeValue(doc,"PatientStatusCode");
				}
				if(Serializing.GetXmlNodeValue(doc,"CustomTracking")!=null) {
					CustomTracking=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CustomTracking"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateResent")!=null) {
					DateResent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateResent"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CorrectionType")!=null) {
					CorrectionType=ClaimCorrectionType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CorrectionType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ClaimIdentifier")!=null) {
					ClaimIdentifier=Serializing.GetXmlNodeValue(doc,"ClaimIdentifier");
				}
				if(Serializing.GetXmlNodeValue(doc,"OrigRefNum")!=null) {
					OrigRefNum=Serializing.GetXmlNodeValue(doc,"OrigRefNum");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum PlaceOfService {
			/** 0. CPT code 11 */
			Office,
			/** 1. CPT code 12 */
			PatientsHome,
			/** 2. CPT code 21 */
			InpatHospital,
			/** 3. CPT code 22 */
			OutpatHospital,
			/** 4. CPT code 31 */
			SkilledNursFac,
			/** 5. CPT code 33.  In X12, a similar code AdultLivCareFac 35 is mentioned. */
			CustodialCareFacility,
			/** 6. CPT code ?.  We use 11 for office. */
			OtherLocation,
			/** 7. CPT code 15 */
			MobileUnit,
			/** 8. CPT code 03 */
			School,
			/** 9. CPT code 26 */
			MilitaryTreatFac,
			/** 10. CPT code 50 */
			FederalHealthCenter,
			/** 11. CPT code 71 */
			PublicHealthClinic,
			/** 12. CPT code 72 */
			RuralHealthClinic,
			/** 13. CPT code 23 */
			EmergencyRoomHospital,
			/** 14. CPT code 24 */
			AmbulatorySurgicalCenter
		}

		/** Unknown,Yes, or No. */
		public enum YN {
			/** 0 */
			Unknown,
			/** 1 */
			Yes,
			/** 2 */
			No
		}

		/** Relationship to subscriber for insurance. */
		public enum Relat {
			/** 0 */
			Self,
			/** 1 */
			Spouse,
			/** 2 */
			Child,
			/** 3 */
			Employee,
			/** 4 */
			HandicapDep,
			/** 5 */
			SignifOther,
			/** 6 */
			InjuredPlaintiff,
			/** 7 */
			LifePartner,
			/** 8 */
			Dependent
		}

		/** 0=none, 1=EPSDT_1, 2=Handicapped_2, 3=SpecialFederal_3, (no 4), 5=Disability_5, 9=SecondOpinion_9 */
		public enum EnumClaimSpecialProgram {
			/**  */
			none,
			/**  */
			EPSDT_1,
			/**  */
			Handicapped_2,
			/**  */
			SpecialFederal_3,
			/**  */
			Disability_5,
			/**  */
			SecondOpinion_9
		}

		/**  */
		public enum EnumClaimMedType {
			/** 0 */
			Dental,
			/** 1 */
			Medical,
			/** 2 */
			Institutional
		}

		/**  */
		public enum ClaimCorrectionType {
			/** 0 - X12 1. Use for claims that are not ongoing. */
			Original,
			/** 1 - X12 7. Use to entirely replace an original claim. A claim reference number will be required. */
			Replacement,
			/** 2 - X12 8. Use to undo an original claim. A claim reference number will be required. */
			Void
		}


}
