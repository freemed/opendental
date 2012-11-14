using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Claim {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Claim claim) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Claim>");
			sb.Append("<ClaimNum>").Append(claim.ClaimNum).Append("</ClaimNum>");
			sb.Append("<PatNum>").Append(claim.PatNum).Append("</PatNum>");
			sb.Append("<DateService>").Append(claim.DateService.ToString()).Append("</DateService>");
			sb.Append("<DateSent>").Append(claim.DateSent.ToString()).Append("</DateSent>");
			sb.Append("<ClaimStatus>").Append(SerializeStringEscapes.EscapeForXml(claim.ClaimStatus)).Append("</ClaimStatus>");
			sb.Append("<DateReceived>").Append(claim.DateReceived.ToString()).Append("</DateReceived>");
			sb.Append("<PlanNum>").Append(claim.PlanNum).Append("</PlanNum>");
			sb.Append("<ProvTreat>").Append(claim.ProvTreat).Append("</ProvTreat>");
			sb.Append("<ClaimFee>").Append(claim.ClaimFee).Append("</ClaimFee>");
			sb.Append("<InsPayEst>").Append(claim.InsPayEst).Append("</InsPayEst>");
			sb.Append("<InsPayAmt>").Append(claim.InsPayAmt).Append("</InsPayAmt>");
			sb.Append("<DedApplied>").Append(claim.DedApplied).Append("</DedApplied>");
			sb.Append("<PreAuthString>").Append(SerializeStringEscapes.EscapeForXml(claim.PreAuthString)).Append("</PreAuthString>");
			sb.Append("<IsProsthesis>").Append(SerializeStringEscapes.EscapeForXml(claim.IsProsthesis)).Append("</IsProsthesis>");
			sb.Append("<PriorDate>").Append(claim.PriorDate.ToString()).Append("</PriorDate>");
			sb.Append("<ReasonUnderPaid>").Append(SerializeStringEscapes.EscapeForXml(claim.ReasonUnderPaid)).Append("</ReasonUnderPaid>");
			sb.Append("<ClaimNote>").Append(SerializeStringEscapes.EscapeForXml(claim.ClaimNote)).Append("</ClaimNote>");
			sb.Append("<ClaimType>").Append(SerializeStringEscapes.EscapeForXml(claim.ClaimType)).Append("</ClaimType>");
			sb.Append("<ProvBill>").Append(claim.ProvBill).Append("</ProvBill>");
			sb.Append("<ReferringProv>").Append(claim.ReferringProv).Append("</ReferringProv>");
			sb.Append("<RefNumString>").Append(SerializeStringEscapes.EscapeForXml(claim.RefNumString)).Append("</RefNumString>");
			sb.Append("<PlaceService>").Append((int)claim.PlaceService).Append("</PlaceService>");
			sb.Append("<AccidentRelated>").Append(SerializeStringEscapes.EscapeForXml(claim.AccidentRelated)).Append("</AccidentRelated>");
			sb.Append("<AccidentDate>").Append(claim.AccidentDate.ToString()).Append("</AccidentDate>");
			sb.Append("<AccidentST>").Append(SerializeStringEscapes.EscapeForXml(claim.AccidentST)).Append("</AccidentST>");
			sb.Append("<EmployRelated>").Append((int)claim.EmployRelated).Append("</EmployRelated>");
			sb.Append("<IsOrtho>").Append((claim.IsOrtho)?1:0).Append("</IsOrtho>");
			sb.Append("<OrthoRemainM>").Append(claim.OrthoRemainM).Append("</OrthoRemainM>");
			sb.Append("<OrthoDate>").Append(claim.OrthoDate.ToString()).Append("</OrthoDate>");
			sb.Append("<PatRelat>").Append((int)claim.PatRelat).Append("</PatRelat>");
			sb.Append("<PlanNum2>").Append(claim.PlanNum2).Append("</PlanNum2>");
			sb.Append("<PatRelat2>").Append((int)claim.PatRelat2).Append("</PatRelat2>");
			sb.Append("<WriteOff>").Append(claim.WriteOff).Append("</WriteOff>");
			sb.Append("<Radiographs>").Append(claim.Radiographs).Append("</Radiographs>");
			sb.Append("<ClinicNum>").Append(claim.ClinicNum).Append("</ClinicNum>");
			sb.Append("<ClaimForm>").Append(claim.ClaimForm).Append("</ClaimForm>");
			sb.Append("<AttachedImages>").Append(claim.AttachedImages).Append("</AttachedImages>");
			sb.Append("<AttachedModels>").Append(claim.AttachedModels).Append("</AttachedModels>");
			sb.Append("<AttachedFlags>").Append(SerializeStringEscapes.EscapeForXml(claim.AttachedFlags)).Append("</AttachedFlags>");
			sb.Append("<AttachmentID>").Append(SerializeStringEscapes.EscapeForXml(claim.AttachmentID)).Append("</AttachmentID>");
			sb.Append("<CanadianMaterialsForwarded>").Append(SerializeStringEscapes.EscapeForXml(claim.CanadianMaterialsForwarded)).Append("</CanadianMaterialsForwarded>");
			sb.Append("<CanadianReferralProviderNum>").Append(SerializeStringEscapes.EscapeForXml(claim.CanadianReferralProviderNum)).Append("</CanadianReferralProviderNum>");
			sb.Append("<CanadianReferralReason>").Append(claim.CanadianReferralReason).Append("</CanadianReferralReason>");
			sb.Append("<CanadianIsInitialLower>").Append(SerializeStringEscapes.EscapeForXml(claim.CanadianIsInitialLower)).Append("</CanadianIsInitialLower>");
			sb.Append("<CanadianDateInitialLower>").Append(claim.CanadianDateInitialLower.ToString()).Append("</CanadianDateInitialLower>");
			sb.Append("<CanadianMandProsthMaterial>").Append(claim.CanadianMandProsthMaterial).Append("</CanadianMandProsthMaterial>");
			sb.Append("<CanadianIsInitialUpper>").Append(SerializeStringEscapes.EscapeForXml(claim.CanadianIsInitialUpper)).Append("</CanadianIsInitialUpper>");
			sb.Append("<CanadianDateInitialUpper>").Append(claim.CanadianDateInitialUpper.ToString()).Append("</CanadianDateInitialUpper>");
			sb.Append("<CanadianMaxProsthMaterial>").Append(claim.CanadianMaxProsthMaterial).Append("</CanadianMaxProsthMaterial>");
			sb.Append("<InsSubNum>").Append(claim.InsSubNum).Append("</InsSubNum>");
			sb.Append("<InsSubNum2>").Append(claim.InsSubNum2).Append("</InsSubNum2>");
			sb.Append("<CanadaTransRefNum>").Append(SerializeStringEscapes.EscapeForXml(claim.CanadaTransRefNum)).Append("</CanadaTransRefNum>");
			sb.Append("<CanadaEstTreatStartDate>").Append(claim.CanadaEstTreatStartDate.ToString()).Append("</CanadaEstTreatStartDate>");
			sb.Append("<CanadaInitialPayment>").Append(claim.CanadaInitialPayment).Append("</CanadaInitialPayment>");
			sb.Append("<CanadaPaymentMode>").Append(claim.CanadaPaymentMode).Append("</CanadaPaymentMode>");
			sb.Append("<CanadaTreatDuration>").Append(claim.CanadaTreatDuration).Append("</CanadaTreatDuration>");
			sb.Append("<CanadaNumAnticipatedPayments>").Append(claim.CanadaNumAnticipatedPayments).Append("</CanadaNumAnticipatedPayments>");
			sb.Append("<CanadaAnticipatedPayAmount>").Append(claim.CanadaAnticipatedPayAmount).Append("</CanadaAnticipatedPayAmount>");
			sb.Append("<PriorAuthorizationNumber>").Append(SerializeStringEscapes.EscapeForXml(claim.PriorAuthorizationNumber)).Append("</PriorAuthorizationNumber>");
			sb.Append("<SpecialProgramCode>").Append((int)claim.SpecialProgramCode).Append("</SpecialProgramCode>");
			sb.Append("<UniformBillType>").Append(SerializeStringEscapes.EscapeForXml(claim.UniformBillType)).Append("</UniformBillType>");
			sb.Append("<MedType>").Append((int)claim.MedType).Append("</MedType>");
			sb.Append("<AdmissionTypeCode>").Append(SerializeStringEscapes.EscapeForXml(claim.AdmissionTypeCode)).Append("</AdmissionTypeCode>");
			sb.Append("<AdmissionSourceCode>").Append(SerializeStringEscapes.EscapeForXml(claim.AdmissionSourceCode)).Append("</AdmissionSourceCode>");
			sb.Append("<PatientStatusCode>").Append(SerializeStringEscapes.EscapeForXml(claim.PatientStatusCode)).Append("</PatientStatusCode>");
			sb.Append("<CustomTracking>").Append(claim.CustomTracking).Append("</CustomTracking>");
			sb.Append("<DateResent>").Append(claim.DateResent.ToString()).Append("</DateResent>");
			sb.Append("<CorrectionType>").Append((int)claim.CorrectionType).Append("</CorrectionType>");
			sb.Append("<ClaimIdentifier>").Append(SerializeStringEscapes.EscapeForXml(claim.ClaimIdentifier)).Append("</ClaimIdentifier>");
			sb.Append("<OrigRefNum>").Append(SerializeStringEscapes.EscapeForXml(claim.OrigRefNum)).Append("</OrigRefNum>");
			sb.Append("</Claim>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Claim Deserialize(string xml) {
			OpenDentBusiness.Claim claim=new OpenDentBusiness.Claim();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimNum":
							claim.ClaimNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							claim.PatNum=reader.ReadContentAsLong();
							break;
						case "DateService":
							claim.DateService=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateSent":
							claim.DateSent=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ClaimStatus":
							claim.ClaimStatus=reader.ReadContentAsString();
							break;
						case "DateReceived":
							claim.DateReceived=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "PlanNum":
							claim.PlanNum=reader.ReadContentAsLong();
							break;
						case "ProvTreat":
							claim.ProvTreat=reader.ReadContentAsLong();
							break;
						case "ClaimFee":
							claim.ClaimFee=reader.ReadContentAsDouble();
							break;
						case "InsPayEst":
							claim.InsPayEst=reader.ReadContentAsDouble();
							break;
						case "InsPayAmt":
							claim.InsPayAmt=reader.ReadContentAsDouble();
							break;
						case "DedApplied":
							claim.DedApplied=reader.ReadContentAsDouble();
							break;
						case "PreAuthString":
							claim.PreAuthString=reader.ReadContentAsString();
							break;
						case "IsProsthesis":
							claim.IsProsthesis=reader.ReadContentAsString();
							break;
						case "PriorDate":
							claim.PriorDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ReasonUnderPaid":
							claim.ReasonUnderPaid=reader.ReadContentAsString();
							break;
						case "ClaimNote":
							claim.ClaimNote=reader.ReadContentAsString();
							break;
						case "ClaimType":
							claim.ClaimType=reader.ReadContentAsString();
							break;
						case "ProvBill":
							claim.ProvBill=reader.ReadContentAsLong();
							break;
						case "ReferringProv":
							claim.ReferringProv=reader.ReadContentAsLong();
							break;
						case "RefNumString":
							claim.RefNumString=reader.ReadContentAsString();
							break;
						case "PlaceService":
							claim.PlaceService=(OpenDentBusiness.PlaceOfService)reader.ReadContentAsInt();
							break;
						case "AccidentRelated":
							claim.AccidentRelated=reader.ReadContentAsString();
							break;
						case "AccidentDate":
							claim.AccidentDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "AccidentST":
							claim.AccidentST=reader.ReadContentAsString();
							break;
						case "EmployRelated":
							claim.EmployRelated=(OpenDentBusiness.YN)reader.ReadContentAsInt();
							break;
						case "IsOrtho":
							claim.IsOrtho=reader.ReadContentAsString()!="0";
							break;
						case "OrthoRemainM":
							claim.OrthoRemainM=(byte)reader.ReadContentAsInt();
							break;
						case "OrthoDate":
							claim.OrthoDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "PatRelat":
							claim.PatRelat=(OpenDentBusiness.Relat)reader.ReadContentAsInt();
							break;
						case "PlanNum2":
							claim.PlanNum2=reader.ReadContentAsLong();
							break;
						case "PatRelat2":
							claim.PatRelat2=(OpenDentBusiness.Relat)reader.ReadContentAsInt();
							break;
						case "WriteOff":
							claim.WriteOff=reader.ReadContentAsDouble();
							break;
						case "Radiographs":
							claim.Radiographs=(byte)reader.ReadContentAsInt();
							break;
						case "ClinicNum":
							claim.ClinicNum=reader.ReadContentAsLong();
							break;
						case "ClaimForm":
							claim.ClaimForm=reader.ReadContentAsLong();
							break;
						case "AttachedImages":
							claim.AttachedImages=reader.ReadContentAsInt();
							break;
						case "AttachedModels":
							claim.AttachedModels=reader.ReadContentAsInt();
							break;
						case "AttachedFlags":
							claim.AttachedFlags=reader.ReadContentAsString();
							break;
						case "AttachmentID":
							claim.AttachmentID=reader.ReadContentAsString();
							break;
						case "CanadianMaterialsForwarded":
							claim.CanadianMaterialsForwarded=reader.ReadContentAsString();
							break;
						case "CanadianReferralProviderNum":
							claim.CanadianReferralProviderNum=reader.ReadContentAsString();
							break;
						case "CanadianReferralReason":
							claim.CanadianReferralReason=(byte)reader.ReadContentAsInt();
							break;
						case "CanadianIsInitialLower":
							claim.CanadianIsInitialLower=reader.ReadContentAsString();
							break;
						case "CanadianDateInitialLower":
							claim.CanadianDateInitialLower=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "CanadianMandProsthMaterial":
							claim.CanadianMandProsthMaterial=(byte)reader.ReadContentAsInt();
							break;
						case "CanadianIsInitialUpper":
							claim.CanadianIsInitialUpper=reader.ReadContentAsString();
							break;
						case "CanadianDateInitialUpper":
							claim.CanadianDateInitialUpper=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "CanadianMaxProsthMaterial":
							claim.CanadianMaxProsthMaterial=(byte)reader.ReadContentAsInt();
							break;
						case "InsSubNum":
							claim.InsSubNum=reader.ReadContentAsLong();
							break;
						case "InsSubNum2":
							claim.InsSubNum2=reader.ReadContentAsLong();
							break;
						case "CanadaTransRefNum":
							claim.CanadaTransRefNum=reader.ReadContentAsString();
							break;
						case "CanadaEstTreatStartDate":
							claim.CanadaEstTreatStartDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "CanadaInitialPayment":
							claim.CanadaInitialPayment=reader.ReadContentAsDouble();
							break;
						case "CanadaPaymentMode":
							claim.CanadaPaymentMode=(byte)reader.ReadContentAsInt();
							break;
						case "CanadaTreatDuration":
							claim.CanadaTreatDuration=(byte)reader.ReadContentAsInt();
							break;
						case "CanadaNumAnticipatedPayments":
							claim.CanadaNumAnticipatedPayments=(byte)reader.ReadContentAsInt();
							break;
						case "CanadaAnticipatedPayAmount":
							claim.CanadaAnticipatedPayAmount=reader.ReadContentAsDouble();
							break;
						case "PriorAuthorizationNumber":
							claim.PriorAuthorizationNumber=reader.ReadContentAsString();
							break;
						case "SpecialProgramCode":
							claim.SpecialProgramCode=(OpenDentBusiness.EnumClaimSpecialProgram)reader.ReadContentAsInt();
							break;
						case "UniformBillType":
							claim.UniformBillType=reader.ReadContentAsString();
							break;
						case "MedType":
							claim.MedType=(OpenDentBusiness.EnumClaimMedType)reader.ReadContentAsInt();
							break;
						case "AdmissionTypeCode":
							claim.AdmissionTypeCode=reader.ReadContentAsString();
							break;
						case "AdmissionSourceCode":
							claim.AdmissionSourceCode=reader.ReadContentAsString();
							break;
						case "PatientStatusCode":
							claim.PatientStatusCode=reader.ReadContentAsString();
							break;
						case "CustomTracking":
							claim.CustomTracking=reader.ReadContentAsLong();
							break;
						case "DateResent":
							claim.DateResent=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "CorrectionType":
							claim.CorrectionType=(OpenDentBusiness.ClaimCorrectionType)reader.ReadContentAsInt();
							break;
						case "ClaimIdentifier":
							claim.ClaimIdentifier=reader.ReadContentAsString();
							break;
						case "OrigRefNum":
							claim.OrigRefNum=reader.ReadContentAsString();
							break;
					}
				}
			}
			return claim;
		}


	}
}