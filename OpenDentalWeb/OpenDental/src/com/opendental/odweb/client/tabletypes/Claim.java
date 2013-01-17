package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;
import java.util.ArrayList;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/** Not a data column. */
		public ArrayList<ClaimAttach> Attachments;

		/** Deep copy of object. */
		public Claim deepCopy() {
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
			claim.Attachments=this.Attachments;
			return claim;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Claim>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateService>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateService)).append("</DateService>");
			sb.append("<DateSent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateSent)).append("</DateSent>");
			sb.append("<ClaimStatus>").append(Serializing.escapeForXml(ClaimStatus)).append("</ClaimStatus>");
			sb.append("<DateReceived>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateReceived)).append("</DateReceived>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<ProvTreat>").append(ProvTreat).append("</ProvTreat>");
			sb.append("<ClaimFee>").append(ClaimFee).append("</ClaimFee>");
			sb.append("<InsPayEst>").append(InsPayEst).append("</InsPayEst>");
			sb.append("<InsPayAmt>").append(InsPayAmt).append("</InsPayAmt>");
			sb.append("<DedApplied>").append(DedApplied).append("</DedApplied>");
			sb.append("<PreAuthString>").append(Serializing.escapeForXml(PreAuthString)).append("</PreAuthString>");
			sb.append("<IsProsthesis>").append(Serializing.escapeForXml(IsProsthesis)).append("</IsProsthesis>");
			sb.append("<PriorDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(PriorDate)).append("</PriorDate>");
			sb.append("<ReasonUnderPaid>").append(Serializing.escapeForXml(ReasonUnderPaid)).append("</ReasonUnderPaid>");
			sb.append("<ClaimNote>").append(Serializing.escapeForXml(ClaimNote)).append("</ClaimNote>");
			sb.append("<ClaimType>").append(Serializing.escapeForXml(ClaimType)).append("</ClaimType>");
			sb.append("<ProvBill>").append(ProvBill).append("</ProvBill>");
			sb.append("<ReferringProv>").append(ReferringProv).append("</ReferringProv>");
			sb.append("<RefNumString>").append(Serializing.escapeForXml(RefNumString)).append("</RefNumString>");
			sb.append("<PlaceService>").append(PlaceService.ordinal()).append("</PlaceService>");
			sb.append("<AccidentRelated>").append(Serializing.escapeForXml(AccidentRelated)).append("</AccidentRelated>");
			sb.append("<AccidentDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AccidentDate)).append("</AccidentDate>");
			sb.append("<AccidentST>").append(Serializing.escapeForXml(AccidentST)).append("</AccidentST>");
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
			sb.append("<AttachedFlags>").append(Serializing.escapeForXml(AttachedFlags)).append("</AttachedFlags>");
			sb.append("<AttachmentID>").append(Serializing.escapeForXml(AttachmentID)).append("</AttachmentID>");
			sb.append("<CanadianMaterialsForwarded>").append(Serializing.escapeForXml(CanadianMaterialsForwarded)).append("</CanadianMaterialsForwarded>");
			sb.append("<CanadianReferralProviderNum>").append(Serializing.escapeForXml(CanadianReferralProviderNum)).append("</CanadianReferralProviderNum>");
			sb.append("<CanadianReferralReason>").append(CanadianReferralReason).append("</CanadianReferralReason>");
			sb.append("<CanadianIsInitialLower>").append(Serializing.escapeForXml(CanadianIsInitialLower)).append("</CanadianIsInitialLower>");
			sb.append("<CanadianDateInitialLower>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CanadianDateInitialLower)).append("</CanadianDateInitialLower>");
			sb.append("<CanadianMandProsthMaterial>").append(CanadianMandProsthMaterial).append("</CanadianMandProsthMaterial>");
			sb.append("<CanadianIsInitialUpper>").append(Serializing.escapeForXml(CanadianIsInitialUpper)).append("</CanadianIsInitialUpper>");
			sb.append("<CanadianDateInitialUpper>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CanadianDateInitialUpper)).append("</CanadianDateInitialUpper>");
			sb.append("<CanadianMaxProsthMaterial>").append(CanadianMaxProsthMaterial).append("</CanadianMaxProsthMaterial>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("<InsSubNum2>").append(InsSubNum2).append("</InsSubNum2>");
			sb.append("<CanadaTransRefNum>").append(Serializing.escapeForXml(CanadaTransRefNum)).append("</CanadaTransRefNum>");
			sb.append("<CanadaEstTreatStartDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CanadaEstTreatStartDate)).append("</CanadaEstTreatStartDate>");
			sb.append("<CanadaInitialPayment>").append(CanadaInitialPayment).append("</CanadaInitialPayment>");
			sb.append("<CanadaPaymentMode>").append(CanadaPaymentMode).append("</CanadaPaymentMode>");
			sb.append("<CanadaTreatDuration>").append(CanadaTreatDuration).append("</CanadaTreatDuration>");
			sb.append("<CanadaNumAnticipatedPayments>").append(CanadaNumAnticipatedPayments).append("</CanadaNumAnticipatedPayments>");
			sb.append("<CanadaAnticipatedPayAmount>").append(CanadaAnticipatedPayAmount).append("</CanadaAnticipatedPayAmount>");
			sb.append("<PriorAuthorizationNumber>").append(Serializing.escapeForXml(PriorAuthorizationNumber)).append("</PriorAuthorizationNumber>");
			sb.append("<SpecialProgramCode>").append(SpecialProgramCode.ordinal()).append("</SpecialProgramCode>");
			sb.append("<UniformBillType>").append(Serializing.escapeForXml(UniformBillType)).append("</UniformBillType>");
			sb.append("<MedType>").append(MedType.ordinal()).append("</MedType>");
			sb.append("<AdmissionTypeCode>").append(Serializing.escapeForXml(AdmissionTypeCode)).append("</AdmissionTypeCode>");
			sb.append("<AdmissionSourceCode>").append(Serializing.escapeForXml(AdmissionSourceCode)).append("</AdmissionSourceCode>");
			sb.append("<PatientStatusCode>").append(Serializing.escapeForXml(PatientStatusCode)).append("</PatientStatusCode>");
			sb.append("<CustomTracking>").append(CustomTracking).append("</CustomTracking>");
			sb.append("<DateResent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateResent)).append("</DateResent>");
			sb.append("<CorrectionType>").append(CorrectionType.ordinal()).append("</CorrectionType>");
			sb.append("<ClaimIdentifier>").append(Serializing.escapeForXml(ClaimIdentifier)).append("</ClaimIdentifier>");
			sb.append("<OrigRefNum>").append(Serializing.escapeForXml(OrigRefNum)).append("</OrigRefNum>");
			sb.append("</Claim>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateService")!=null) {
					DateService=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateService"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateSent")!=null) {
					DateSent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateSent"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimStatus")!=null) {
					ClaimStatus=Serializing.getXmlNodeValue(doc,"ClaimStatus");
				}
				if(Serializing.getXmlNodeValue(doc,"DateReceived")!=null) {
					DateReceived=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateReceived"));
				}
				if(Serializing.getXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvTreat")!=null) {
					ProvTreat=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvTreat"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimFee")!=null) {
					ClaimFee=Double.valueOf(Serializing.getXmlNodeValue(doc,"ClaimFee"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsPayEst")!=null) {
					InsPayEst=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsPayEst"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsPayAmt")!=null) {
					InsPayAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"InsPayAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"DedApplied")!=null) {
					DedApplied=Double.valueOf(Serializing.getXmlNodeValue(doc,"DedApplied"));
				}
				if(Serializing.getXmlNodeValue(doc,"PreAuthString")!=null) {
					PreAuthString=Serializing.getXmlNodeValue(doc,"PreAuthString");
				}
				if(Serializing.getXmlNodeValue(doc,"IsProsthesis")!=null) {
					IsProsthesis=Serializing.getXmlNodeValue(doc,"IsProsthesis");
				}
				if(Serializing.getXmlNodeValue(doc,"PriorDate")!=null) {
					PriorDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"PriorDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"ReasonUnderPaid")!=null) {
					ReasonUnderPaid=Serializing.getXmlNodeValue(doc,"ReasonUnderPaid");
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimNote")!=null) {
					ClaimNote=Serializing.getXmlNodeValue(doc,"ClaimNote");
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimType")!=null) {
					ClaimType=Serializing.getXmlNodeValue(doc,"ClaimType");
				}
				if(Serializing.getXmlNodeValue(doc,"ProvBill")!=null) {
					ProvBill=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvBill"));
				}
				if(Serializing.getXmlNodeValue(doc,"ReferringProv")!=null) {
					ReferringProv=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReferringProv"));
				}
				if(Serializing.getXmlNodeValue(doc,"RefNumString")!=null) {
					RefNumString=Serializing.getXmlNodeValue(doc,"RefNumString");
				}
				if(Serializing.getXmlNodeValue(doc,"PlaceService")!=null) {
					PlaceService=PlaceOfService.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlaceService"))];
				}
				if(Serializing.getXmlNodeValue(doc,"AccidentRelated")!=null) {
					AccidentRelated=Serializing.getXmlNodeValue(doc,"AccidentRelated");
				}
				if(Serializing.getXmlNodeValue(doc,"AccidentDate")!=null) {
					AccidentDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"AccidentDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"AccidentST")!=null) {
					AccidentST=Serializing.getXmlNodeValue(doc,"AccidentST");
				}
				if(Serializing.getXmlNodeValue(doc,"EmployRelated")!=null) {
					EmployRelated=YN.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployRelated"))];
				}
				if(Serializing.getXmlNodeValue(doc,"IsOrtho")!=null) {
					IsOrtho=(Serializing.getXmlNodeValue(doc,"IsOrtho")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"OrthoRemainM")!=null) {
					OrthoRemainM=Byte.valueOf(Serializing.getXmlNodeValue(doc,"OrthoRemainM"));
				}
				if(Serializing.getXmlNodeValue(doc,"OrthoDate")!=null) {
					OrthoDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"OrthoDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatRelat")!=null) {
					PatRelat=Relat.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatRelat"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PlanNum2")!=null) {
					PlanNum2=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlanNum2"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatRelat2")!=null) {
					PatRelat2=Relat.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatRelat2"))];
				}
				if(Serializing.getXmlNodeValue(doc,"WriteOff")!=null) {
					WriteOff=Double.valueOf(Serializing.getXmlNodeValue(doc,"WriteOff"));
				}
				if(Serializing.getXmlNodeValue(doc,"Radiographs")!=null) {
					Radiographs=Byte.valueOf(Serializing.getXmlNodeValue(doc,"Radiographs"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimForm")!=null) {
					ClaimForm=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimForm"));
				}
				if(Serializing.getXmlNodeValue(doc,"AttachedImages")!=null) {
					AttachedImages=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AttachedImages"));
				}
				if(Serializing.getXmlNodeValue(doc,"AttachedModels")!=null) {
					AttachedModels=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AttachedModels"));
				}
				if(Serializing.getXmlNodeValue(doc,"AttachedFlags")!=null) {
					AttachedFlags=Serializing.getXmlNodeValue(doc,"AttachedFlags");
				}
				if(Serializing.getXmlNodeValue(doc,"AttachmentID")!=null) {
					AttachmentID=Serializing.getXmlNodeValue(doc,"AttachmentID");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianMaterialsForwarded")!=null) {
					CanadianMaterialsForwarded=Serializing.getXmlNodeValue(doc,"CanadianMaterialsForwarded");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianReferralProviderNum")!=null) {
					CanadianReferralProviderNum=Serializing.getXmlNodeValue(doc,"CanadianReferralProviderNum");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianReferralReason")!=null) {
					CanadianReferralReason=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadianReferralReason"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianIsInitialLower")!=null) {
					CanadianIsInitialLower=Serializing.getXmlNodeValue(doc,"CanadianIsInitialLower");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianDateInitialLower")!=null) {
					CanadianDateInitialLower=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"CanadianDateInitialLower"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianMandProsthMaterial")!=null) {
					CanadianMandProsthMaterial=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadianMandProsthMaterial"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianIsInitialUpper")!=null) {
					CanadianIsInitialUpper=Serializing.getXmlNodeValue(doc,"CanadianIsInitialUpper");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianDateInitialUpper")!=null) {
					CanadianDateInitialUpper=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"CanadianDateInitialUpper"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadianMaxProsthMaterial")!=null) {
					CanadianMaxProsthMaterial=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadianMaxProsthMaterial"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsSubNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsSubNum2")!=null) {
					InsSubNum2=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsSubNum2"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadaTransRefNum")!=null) {
					CanadaTransRefNum=Serializing.getXmlNodeValue(doc,"CanadaTransRefNum");
				}
				if(Serializing.getXmlNodeValue(doc,"CanadaEstTreatStartDate")!=null) {
					CanadaEstTreatStartDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"CanadaEstTreatStartDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadaInitialPayment")!=null) {
					CanadaInitialPayment=Double.valueOf(Serializing.getXmlNodeValue(doc,"CanadaInitialPayment"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadaPaymentMode")!=null) {
					CanadaPaymentMode=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadaPaymentMode"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadaTreatDuration")!=null) {
					CanadaTreatDuration=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadaTreatDuration"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadaNumAnticipatedPayments")!=null) {
					CanadaNumAnticipatedPayments=Byte.valueOf(Serializing.getXmlNodeValue(doc,"CanadaNumAnticipatedPayments"));
				}
				if(Serializing.getXmlNodeValue(doc,"CanadaAnticipatedPayAmount")!=null) {
					CanadaAnticipatedPayAmount=Double.valueOf(Serializing.getXmlNodeValue(doc,"CanadaAnticipatedPayAmount"));
				}
				if(Serializing.getXmlNodeValue(doc,"PriorAuthorizationNumber")!=null) {
					PriorAuthorizationNumber=Serializing.getXmlNodeValue(doc,"PriorAuthorizationNumber");
				}
				if(Serializing.getXmlNodeValue(doc,"SpecialProgramCode")!=null) {
					SpecialProgramCode=EnumClaimSpecialProgram.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"SpecialProgramCode"))];
				}
				if(Serializing.getXmlNodeValue(doc,"UniformBillType")!=null) {
					UniformBillType=Serializing.getXmlNodeValue(doc,"UniformBillType");
				}
				if(Serializing.getXmlNodeValue(doc,"MedType")!=null) {
					MedType=EnumClaimMedType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"AdmissionTypeCode")!=null) {
					AdmissionTypeCode=Serializing.getXmlNodeValue(doc,"AdmissionTypeCode");
				}
				if(Serializing.getXmlNodeValue(doc,"AdmissionSourceCode")!=null) {
					AdmissionSourceCode=Serializing.getXmlNodeValue(doc,"AdmissionSourceCode");
				}
				if(Serializing.getXmlNodeValue(doc,"PatientStatusCode")!=null) {
					PatientStatusCode=Serializing.getXmlNodeValue(doc,"PatientStatusCode");
				}
				if(Serializing.getXmlNodeValue(doc,"CustomTracking")!=null) {
					CustomTracking=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CustomTracking"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateResent")!=null) {
					DateResent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateResent"));
				}
				if(Serializing.getXmlNodeValue(doc,"CorrectionType")!=null) {
					CorrectionType=ClaimCorrectionType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"CorrectionType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimIdentifier")!=null) {
					ClaimIdentifier=Serializing.getXmlNodeValue(doc,"ClaimIdentifier");
				}
				if(Serializing.getXmlNodeValue(doc,"OrigRefNum")!=null) {
					OrigRefNum=Serializing.getXmlNodeValue(doc,"OrigRefNum");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Claim: "+e.getMessage());
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
