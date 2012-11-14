using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Procedure {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Procedure procedure) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Procedure>");
			sb.Append("<ProcNum>").Append(procedure.ProcNum).Append("</ProcNum>");
			sb.Append("<PatNum>").Append(procedure.PatNum).Append("</PatNum>");
			sb.Append("<AptNum>").Append(procedure.AptNum).Append("</AptNum>");
			sb.Append("<OldCode>").Append(SerializeStringEscapes.EscapeForXml(procedure.OldCode)).Append("</OldCode>");
			sb.Append("<ProcDate>").Append(procedure.ProcDate.ToString()).Append("</ProcDate>");
			sb.Append("<ProcFee>").Append(procedure.ProcFee).Append("</ProcFee>");
			sb.Append("<Surf>").Append(SerializeStringEscapes.EscapeForXml(procedure.Surf)).Append("</Surf>");
			sb.Append("<ToothNum>").Append(SerializeStringEscapes.EscapeForXml(procedure.ToothNum)).Append("</ToothNum>");
			sb.Append("<ToothRange>").Append(SerializeStringEscapes.EscapeForXml(procedure.ToothRange)).Append("</ToothRange>");
			sb.Append("<Priority>").Append(procedure.Priority).Append("</Priority>");
			sb.Append("<ProcStatus>").Append((int)procedure.ProcStatus).Append("</ProcStatus>");
			sb.Append("<ProvNum>").Append(procedure.ProvNum).Append("</ProvNum>");
			sb.Append("<Dx>").Append(procedure.Dx).Append("</Dx>");
			sb.Append("<PlannedAptNum>").Append(procedure.PlannedAptNum).Append("</PlannedAptNum>");
			sb.Append("<PlaceService>").Append((int)procedure.PlaceService).Append("</PlaceService>");
			sb.Append("<Prosthesis>").Append(SerializeStringEscapes.EscapeForXml(procedure.Prosthesis)).Append("</Prosthesis>");
			sb.Append("<DateOriginalProsth>").Append(procedure.DateOriginalProsth.ToString()).Append("</DateOriginalProsth>");
			sb.Append("<ClaimNote>").Append(SerializeStringEscapes.EscapeForXml(procedure.ClaimNote)).Append("</ClaimNote>");
			sb.Append("<DateEntryC>").Append(procedure.DateEntryC.ToString()).Append("</DateEntryC>");
			sb.Append("<ClinicNum>").Append(procedure.ClinicNum).Append("</ClinicNum>");
			sb.Append("<MedicalCode>").Append(SerializeStringEscapes.EscapeForXml(procedure.MedicalCode)).Append("</MedicalCode>");
			sb.Append("<DiagnosticCode>").Append(SerializeStringEscapes.EscapeForXml(procedure.DiagnosticCode)).Append("</DiagnosticCode>");
			sb.Append("<IsPrincDiag>").Append((procedure.IsPrincDiag)?1:0).Append("</IsPrincDiag>");
			sb.Append("<ProcNumLab>").Append(procedure.ProcNumLab).Append("</ProcNumLab>");
			sb.Append("<BillingTypeOne>").Append(procedure.BillingTypeOne).Append("</BillingTypeOne>");
			sb.Append("<BillingTypeTwo>").Append(procedure.BillingTypeTwo).Append("</BillingTypeTwo>");
			sb.Append("<CodeNum>").Append(procedure.CodeNum).Append("</CodeNum>");
			sb.Append("<CodeMod1>").Append(SerializeStringEscapes.EscapeForXml(procedure.CodeMod1)).Append("</CodeMod1>");
			sb.Append("<CodeMod2>").Append(SerializeStringEscapes.EscapeForXml(procedure.CodeMod2)).Append("</CodeMod2>");
			sb.Append("<CodeMod3>").Append(SerializeStringEscapes.EscapeForXml(procedure.CodeMod3)).Append("</CodeMod3>");
			sb.Append("<CodeMod4>").Append(SerializeStringEscapes.EscapeForXml(procedure.CodeMod4)).Append("</CodeMod4>");
			sb.Append("<RevCode>").Append(SerializeStringEscapes.EscapeForXml(procedure.RevCode)).Append("</RevCode>");
			sb.Append("<UnitQty>").Append(procedure.UnitQty).Append("</UnitQty>");
			sb.Append("<BaseUnits>").Append(procedure.BaseUnits).Append("</BaseUnits>");
			sb.Append("<StartTime>").Append(procedure.StartTime).Append("</StartTime>");
			sb.Append("<StopTime>").Append(procedure.StopTime).Append("</StopTime>");
			sb.Append("<DateTP>").Append(procedure.DateTP.ToString()).Append("</DateTP>");
			sb.Append("<SiteNum>").Append(procedure.SiteNum).Append("</SiteNum>");
			sb.Append("<HideGraphics>").Append((procedure.HideGraphics)?1:0).Append("</HideGraphics>");
			sb.Append("<CanadianTypeCodes>").Append(SerializeStringEscapes.EscapeForXml(procedure.CanadianTypeCodes)).Append("</CanadianTypeCodes>");
			sb.Append("<ProcTime>").Append(procedure.ProcTime.ToString()).Append("</ProcTime>");
			sb.Append("<ProcTimeEnd>").Append(procedure.ProcTimeEnd.ToString()).Append("</ProcTimeEnd>");
			sb.Append("<DateTStamp>").Append(procedure.DateTStamp.ToString()).Append("</DateTStamp>");
			sb.Append("<Prognosis>").Append(procedure.Prognosis).Append("</Prognosis>");
			sb.Append("<DrugUnit>").Append((int)procedure.DrugUnit).Append("</DrugUnit>");
			sb.Append("<DrugQty>").Append(procedure.DrugQty).Append("</DrugQty>");
			sb.Append("<UnitQtyType>").Append((int)procedure.UnitQtyType).Append("</UnitQtyType>");
			sb.Append("<StatementNum>").Append(procedure.StatementNum).Append("</StatementNum>");
			sb.Append("<IsLocked>").Append((procedure.IsLocked)?1:0).Append("</IsLocked>");
			sb.Append("</Procedure>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Procedure Deserialize(string xml) {
			OpenDentBusiness.Procedure procedure=new OpenDentBusiness.Procedure();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcNum":
							procedure.ProcNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							procedure.PatNum=reader.ReadContentAsLong();
							break;
						case "AptNum":
							procedure.AptNum=reader.ReadContentAsLong();
							break;
						case "OldCode":
							procedure.OldCode=reader.ReadContentAsString();
							break;
						case "ProcDate":
							procedure.ProcDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ProcFee":
							procedure.ProcFee=reader.ReadContentAsDouble();
							break;
						case "Surf":
							procedure.Surf=reader.ReadContentAsString();
							break;
						case "ToothNum":
							procedure.ToothNum=reader.ReadContentAsString();
							break;
						case "ToothRange":
							procedure.ToothRange=reader.ReadContentAsString();
							break;
						case "Priority":
							procedure.Priority=reader.ReadContentAsLong();
							break;
						case "ProcStatus":
							procedure.ProcStatus=(OpenDentBusiness.ProcStat)reader.ReadContentAsInt();
							break;
						case "ProvNum":
							procedure.ProvNum=reader.ReadContentAsLong();
							break;
						case "Dx":
							procedure.Dx=reader.ReadContentAsLong();
							break;
						case "PlannedAptNum":
							procedure.PlannedAptNum=reader.ReadContentAsLong();
							break;
						case "PlaceService":
							procedure.PlaceService=(OpenDentBusiness.PlaceOfService)reader.ReadContentAsInt();
							break;
						case "Prosthesis":
							procedure.Prosthesis=reader.ReadContentAsString();
							break;
						case "DateOriginalProsth":
							procedure.DateOriginalProsth=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ClaimNote":
							procedure.ClaimNote=reader.ReadContentAsString();
							break;
						case "DateEntryC":
							procedure.DateEntryC=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ClinicNum":
							procedure.ClinicNum=reader.ReadContentAsLong();
							break;
						case "MedicalCode":
							procedure.MedicalCode=reader.ReadContentAsString();
							break;
						case "DiagnosticCode":
							procedure.DiagnosticCode=reader.ReadContentAsString();
							break;
						case "IsPrincDiag":
							procedure.IsPrincDiag=reader.ReadContentAsString()!="0";
							break;
						case "ProcNumLab":
							procedure.ProcNumLab=reader.ReadContentAsLong();
							break;
						case "BillingTypeOne":
							procedure.BillingTypeOne=reader.ReadContentAsLong();
							break;
						case "BillingTypeTwo":
							procedure.BillingTypeTwo=reader.ReadContentAsLong();
							break;
						case "CodeNum":
							procedure.CodeNum=reader.ReadContentAsLong();
							break;
						case "CodeMod1":
							procedure.CodeMod1=reader.ReadContentAsString();
							break;
						case "CodeMod2":
							procedure.CodeMod2=reader.ReadContentAsString();
							break;
						case "CodeMod3":
							procedure.CodeMod3=reader.ReadContentAsString();
							break;
						case "CodeMod4":
							procedure.CodeMod4=reader.ReadContentAsString();
							break;
						case "RevCode":
							procedure.RevCode=reader.ReadContentAsString();
							break;
						case "UnitQty":
							procedure.UnitQty=reader.ReadContentAsInt();
							break;
						case "BaseUnits":
							procedure.BaseUnits=reader.ReadContentAsInt();
							break;
						case "StartTime":
							procedure.StartTime=reader.ReadContentAsInt();
							break;
						case "StopTime":
							procedure.StopTime=reader.ReadContentAsInt();
							break;
						case "DateTP":
							procedure.DateTP=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "SiteNum":
							procedure.SiteNum=reader.ReadContentAsLong();
							break;
						case "HideGraphics":
							procedure.HideGraphics=reader.ReadContentAsString()!="0";
							break;
						case "CanadianTypeCodes":
							procedure.CanadianTypeCodes=reader.ReadContentAsString();
							break;
						case "ProcTime":
							procedure.ProcTime=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "ProcTimeEnd":
							procedure.ProcTimeEnd=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "DateTStamp":
							procedure.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "Prognosis":
							procedure.Prognosis=reader.ReadContentAsLong();
							break;
						case "DrugUnit":
							procedure.DrugUnit=(OpenDentBusiness.EnumProcDrugUnit)reader.ReadContentAsInt();
							break;
						case "DrugQty":
							procedure.DrugQty=reader.ReadContentAsFloat();
							break;
						case "UnitQtyType":
							procedure.UnitQtyType=(OpenDentBusiness.ProcUnitQtyType)reader.ReadContentAsInt();
							break;
						case "StatementNum":
							procedure.StatementNum=reader.ReadContentAsLong();
							break;
						case "IsLocked":
							procedure.IsLocked=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return procedure;
		}


	}
}