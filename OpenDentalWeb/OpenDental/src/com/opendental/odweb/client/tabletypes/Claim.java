package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Claim {
		/** Primary key */
		public int ClaimNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** Usually the same date as the procedures, but it can be changed if you wish. */
		public String DateService;
		/** Usually the date it was created.  It might be sent a few days later if you don't send your e-claims every day. */
		public String DateSent;
		/** Single char: U,H,W,P,S,or R.  U=Unsent, H=Hold until pri received, W=Waiting in queue, S=Sent, R=Received.  A(adj) is no longer used.  P(prob sent) is no longer used. */
		public String ClaimStatus;
		/** Date the claim was received. */
		public String DateReceived;
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
		public String PriorDate;
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
		public String AccidentDate;
		/** Accident state. */
		public String AccidentST;
		/** Enum:YN . */
		public YN EmployRelated;
		/** True if is ortho. */
		public boolean IsOrtho;
		/** Remaining months of ortho. Valid values are 1-36. */
		public byte OrthoRemainM;
		/** Date ortho appliance placed. */
		public String OrthoDate;
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
		public String CanadianDateInitialLower;
		/** F21.  If crown, not required.  If denture or bridge, required if F18 is N.  Single digit number code, 0-6.  We added type 7, which is crown. */
		public byte CanadianMandProsthMaterial;
		/** F15.  Y, N, or X(not an upper denture, crown, or bridge). */
		public String CanadianIsInitialUpper;
		/** F04.  Mandatory if F15 is N. */
		public String CanadianDateInitialUpper;
		/** F20.  If crown, not required.  If denture or bridge, required if F15 is N.  0 indicates empty response.  Single digit number code, 1-6.  We added type 7, which is crown. */
		public byte CanadianMaxProsthMaterial;
		/** FK to inssub.InsSubNum. */
		public int InsSubNum;
		/** FK to inssub.InsSubNum. */
		public int InsSubNum2;
		/** G01 assigned by carrier/network and returned in acks.  Used for claim reversal. */
		public String CanadaTransRefNum;
		/** F37 Used for predeterminations. */
		public String CanadaEstTreatStartDate;
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
		public String DateResent;
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
			sb.append("<DateService>").append(Serializing.EscapeForXml(DateService)).append("</DateService>");
			sb.append("<DateSent>").append(Serializing.EscapeForXml(DateSent)).append("</DateSent>");
			sb.append("<ClaimStatus>").append(Serializing.EscapeForXml(ClaimStatus)).append("</ClaimStatus>");
			sb.append("<DateReceived>").append(Serializing.EscapeForXml(DateReceived)).append("</DateReceived>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<ProvTreat>").append(ProvTreat).append("</ProvTreat>");
			sb.append("<ClaimFee>").append(ClaimFee).append("</ClaimFee>");
			sb.append("<InsPayEst>").append(InsPayEst).append("</InsPayEst>");
			sb.append("<InsPayAmt>").append(InsPayAmt).append("</InsPayAmt>");
			sb.append("<DedApplied>").append(DedApplied).append("</DedApplied>");
			sb.append("<PreAuthString>").append(Serializing.EscapeForXml(PreAuthString)).append("</PreAuthString>");
			sb.append("<IsProsthesis>").append(Serializing.EscapeForXml(IsProsthesis)).append("</IsProsthesis>");
			sb.append("<PriorDate>").append(Serializing.EscapeForXml(PriorDate)).append("</PriorDate>");
			sb.append("<ReasonUnderPaid>").append(Serializing.EscapeForXml(ReasonUnderPaid)).append("</ReasonUnderPaid>");
			sb.append("<ClaimNote>").append(Serializing.EscapeForXml(ClaimNote)).append("</ClaimNote>");
			sb.append("<ClaimType>").append(Serializing.EscapeForXml(ClaimType)).append("</ClaimType>");
			sb.append("<ProvBill>").append(ProvBill).append("</ProvBill>");
			sb.append("<ReferringProv>").append(ReferringProv).append("</ReferringProv>");
			sb.append("<RefNumString>").append(Serializing.EscapeForXml(RefNumString)).append("</RefNumString>");
			sb.append("<PlaceService>").append(PlaceService.ordinal()).append("</PlaceService>");
			sb.append("<AccidentRelated>").append(Serializing.EscapeForXml(AccidentRelated)).append("</AccidentRelated>");
			sb.append("<AccidentDate>").append(Serializing.EscapeForXml(AccidentDate)).append("</AccidentDate>");
			sb.append("<AccidentST>").append(Serializing.EscapeForXml(AccidentST)).append("</AccidentST>");
			sb.append("<EmployRelated>").append(EmployRelated.ordinal()).append("</EmployRelated>");
			sb.append("<IsOrtho>").append((IsOrtho)?1:0).append("</IsOrtho>");
			sb.append("<OrthoRemainM>").append(OrthoRemainM).append("</OrthoRemainM>");
			sb.append("<OrthoDate>").append(Serializing.EscapeForXml(OrthoDate)).append("</OrthoDate>");
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
			sb.append("<CanadianDateInitialLower>").append(Serializing.EscapeForXml(CanadianDateInitialLower)).append("</CanadianDateInitialLower>");
			sb.append("<CanadianMandProsthMaterial>").append(CanadianMandProsthMaterial).append("</CanadianMandProsthMaterial>");
			sb.append("<CanadianIsInitialUpper>").append(Serializing.EscapeForXml(CanadianIsInitialUpper)).append("</CanadianIsInitialUpper>");
			sb.append("<CanadianDateInitialUpper>").append(Serializing.EscapeForXml(CanadianDateInitialUpper)).append("</CanadianDateInitialUpper>");
			sb.append("<CanadianMaxProsthMaterial>").append(CanadianMaxProsthMaterial).append("</CanadianMaxProsthMaterial>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("<InsSubNum2>").append(InsSubNum2).append("</InsSubNum2>");
			sb.append("<CanadaTransRefNum>").append(Serializing.EscapeForXml(CanadaTransRefNum)).append("</CanadaTransRefNum>");
			sb.append("<CanadaEstTreatStartDate>").append(Serializing.EscapeForXml(CanadaEstTreatStartDate)).append("</CanadaEstTreatStartDate>");
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
			sb.append("<DateResent>").append(Serializing.EscapeForXml(DateResent)).append("</DateResent>");
			sb.append("<CorrectionType>").append(CorrectionType.ordinal()).append("</CorrectionType>");
			sb.append("<ClaimIdentifier>").append(Serializing.EscapeForXml(ClaimIdentifier)).append("</ClaimIdentifier>");
			sb.append("<OrigRefNum>").append(Serializing.EscapeForXml(OrigRefNum)).append("</OrigRefNum>");
			sb.append("</Claim>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClaimNum=Integer.valueOf(doc.getElementsByTagName("ClaimNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateService=doc.getElementsByTagName("DateService").item(0).getFirstChild().getNodeValue();
				DateSent=doc.getElementsByTagName("DateSent").item(0).getFirstChild().getNodeValue();
				ClaimStatus=doc.getElementsByTagName("ClaimStatus").item(0).getFirstChild().getNodeValue();
				DateReceived=doc.getElementsByTagName("DateReceived").item(0).getFirstChild().getNodeValue();
				PlanNum=Integer.valueOf(doc.getElementsByTagName("PlanNum").item(0).getFirstChild().getNodeValue());
				ProvTreat=Integer.valueOf(doc.getElementsByTagName("ProvTreat").item(0).getFirstChild().getNodeValue());
				ClaimFee=Double.valueOf(doc.getElementsByTagName("ClaimFee").item(0).getFirstChild().getNodeValue());
				InsPayEst=Double.valueOf(doc.getElementsByTagName("InsPayEst").item(0).getFirstChild().getNodeValue());
				InsPayAmt=Double.valueOf(doc.getElementsByTagName("InsPayAmt").item(0).getFirstChild().getNodeValue());
				DedApplied=Double.valueOf(doc.getElementsByTagName("DedApplied").item(0).getFirstChild().getNodeValue());
				PreAuthString=doc.getElementsByTagName("PreAuthString").item(0).getFirstChild().getNodeValue();
				IsProsthesis=doc.getElementsByTagName("IsProsthesis").item(0).getFirstChild().getNodeValue();
				PriorDate=doc.getElementsByTagName("PriorDate").item(0).getFirstChild().getNodeValue();
				ReasonUnderPaid=doc.getElementsByTagName("ReasonUnderPaid").item(0).getFirstChild().getNodeValue();
				ClaimNote=doc.getElementsByTagName("ClaimNote").item(0).getFirstChild().getNodeValue();
				ClaimType=doc.getElementsByTagName("ClaimType").item(0).getFirstChild().getNodeValue();
				ProvBill=Integer.valueOf(doc.getElementsByTagName("ProvBill").item(0).getFirstChild().getNodeValue());
				ReferringProv=Integer.valueOf(doc.getElementsByTagName("ReferringProv").item(0).getFirstChild().getNodeValue());
				RefNumString=doc.getElementsByTagName("RefNumString").item(0).getFirstChild().getNodeValue();
				PlaceService=PlaceOfService.values()[Integer.valueOf(doc.getElementsByTagName("PlaceService").item(0).getFirstChild().getNodeValue())];
				AccidentRelated=doc.getElementsByTagName("AccidentRelated").item(0).getFirstChild().getNodeValue();
				AccidentDate=doc.getElementsByTagName("AccidentDate").item(0).getFirstChild().getNodeValue();
				AccidentST=doc.getElementsByTagName("AccidentST").item(0).getFirstChild().getNodeValue();
				EmployRelated=YN.values()[Integer.valueOf(doc.getElementsByTagName("EmployRelated").item(0).getFirstChild().getNodeValue())];
				IsOrtho=(doc.getElementsByTagName("IsOrtho").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				OrthoRemainM=Byte.valueOf(doc.getElementsByTagName("OrthoRemainM").item(0).getFirstChild().getNodeValue());
				OrthoDate=doc.getElementsByTagName("OrthoDate").item(0).getFirstChild().getNodeValue();
				PatRelat=Relat.values()[Integer.valueOf(doc.getElementsByTagName("PatRelat").item(0).getFirstChild().getNodeValue())];
				PlanNum2=Integer.valueOf(doc.getElementsByTagName("PlanNum2").item(0).getFirstChild().getNodeValue());
				PatRelat2=Relat.values()[Integer.valueOf(doc.getElementsByTagName("PatRelat2").item(0).getFirstChild().getNodeValue())];
				WriteOff=Double.valueOf(doc.getElementsByTagName("WriteOff").item(0).getFirstChild().getNodeValue());
				Radiographs=Byte.valueOf(doc.getElementsByTagName("Radiographs").item(0).getFirstChild().getNodeValue());
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
				ClaimForm=Integer.valueOf(doc.getElementsByTagName("ClaimForm").item(0).getFirstChild().getNodeValue());
				AttachedImages=Integer.valueOf(doc.getElementsByTagName("AttachedImages").item(0).getFirstChild().getNodeValue());
				AttachedModels=Integer.valueOf(doc.getElementsByTagName("AttachedModels").item(0).getFirstChild().getNodeValue());
				AttachedFlags=doc.getElementsByTagName("AttachedFlags").item(0).getFirstChild().getNodeValue();
				AttachmentID=doc.getElementsByTagName("AttachmentID").item(0).getFirstChild().getNodeValue();
				CanadianMaterialsForwarded=doc.getElementsByTagName("CanadianMaterialsForwarded").item(0).getFirstChild().getNodeValue();
				CanadianReferralProviderNum=doc.getElementsByTagName("CanadianReferralProviderNum").item(0).getFirstChild().getNodeValue();
				CanadianReferralReason=Byte.valueOf(doc.getElementsByTagName("CanadianReferralReason").item(0).getFirstChild().getNodeValue());
				CanadianIsInitialLower=doc.getElementsByTagName("CanadianIsInitialLower").item(0).getFirstChild().getNodeValue();
				CanadianDateInitialLower=doc.getElementsByTagName("CanadianDateInitialLower").item(0).getFirstChild().getNodeValue();
				CanadianMandProsthMaterial=Byte.valueOf(doc.getElementsByTagName("CanadianMandProsthMaterial").item(0).getFirstChild().getNodeValue());
				CanadianIsInitialUpper=doc.getElementsByTagName("CanadianIsInitialUpper").item(0).getFirstChild().getNodeValue();
				CanadianDateInitialUpper=doc.getElementsByTagName("CanadianDateInitialUpper").item(0).getFirstChild().getNodeValue();
				CanadianMaxProsthMaterial=Byte.valueOf(doc.getElementsByTagName("CanadianMaxProsthMaterial").item(0).getFirstChild().getNodeValue());
				InsSubNum=Integer.valueOf(doc.getElementsByTagName("InsSubNum").item(0).getFirstChild().getNodeValue());
				InsSubNum2=Integer.valueOf(doc.getElementsByTagName("InsSubNum2").item(0).getFirstChild().getNodeValue());
				CanadaTransRefNum=doc.getElementsByTagName("CanadaTransRefNum").item(0).getFirstChild().getNodeValue();
				CanadaEstTreatStartDate=doc.getElementsByTagName("CanadaEstTreatStartDate").item(0).getFirstChild().getNodeValue();
				CanadaInitialPayment=Double.valueOf(doc.getElementsByTagName("CanadaInitialPayment").item(0).getFirstChild().getNodeValue());
				CanadaPaymentMode=Byte.valueOf(doc.getElementsByTagName("CanadaPaymentMode").item(0).getFirstChild().getNodeValue());
				CanadaTreatDuration=Byte.valueOf(doc.getElementsByTagName("CanadaTreatDuration").item(0).getFirstChild().getNodeValue());
				CanadaNumAnticipatedPayments=Byte.valueOf(doc.getElementsByTagName("CanadaNumAnticipatedPayments").item(0).getFirstChild().getNodeValue());
				CanadaAnticipatedPayAmount=Double.valueOf(doc.getElementsByTagName("CanadaAnticipatedPayAmount").item(0).getFirstChild().getNodeValue());
				PriorAuthorizationNumber=doc.getElementsByTagName("PriorAuthorizationNumber").item(0).getFirstChild().getNodeValue();
				SpecialProgramCode=EnumClaimSpecialProgram.values()[Integer.valueOf(doc.getElementsByTagName("SpecialProgramCode").item(0).getFirstChild().getNodeValue())];
				UniformBillType=doc.getElementsByTagName("UniformBillType").item(0).getFirstChild().getNodeValue();
				MedType=EnumClaimMedType.values()[Integer.valueOf(doc.getElementsByTagName("MedType").item(0).getFirstChild().getNodeValue())];
				AdmissionTypeCode=doc.getElementsByTagName("AdmissionTypeCode").item(0).getFirstChild().getNodeValue();
				AdmissionSourceCode=doc.getElementsByTagName("AdmissionSourceCode").item(0).getFirstChild().getNodeValue();
				PatientStatusCode=doc.getElementsByTagName("PatientStatusCode").item(0).getFirstChild().getNodeValue();
				CustomTracking=Integer.valueOf(doc.getElementsByTagName("CustomTracking").item(0).getFirstChild().getNodeValue());
				DateResent=doc.getElementsByTagName("DateResent").item(0).getFirstChild().getNodeValue();
				CorrectionType=ClaimCorrectionType.values()[Integer.valueOf(doc.getElementsByTagName("CorrectionType").item(0).getFirstChild().getNodeValue())];
				ClaimIdentifier=doc.getElementsByTagName("ClaimIdentifier").item(0).getFirstChild().getNodeValue();
				OrigRefNum=doc.getElementsByTagName("OrigRefNum").item(0).getFirstChild().getNodeValue();
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
