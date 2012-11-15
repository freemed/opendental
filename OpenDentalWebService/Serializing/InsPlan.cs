using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class InsPlan {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.InsPlan insplan) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<InsPlan>");
			sb.Append("<PlanNum>").Append(insplan.PlanNum).Append("</PlanNum>");
			sb.Append("<GroupName>").Append(SerializeStringEscapes.EscapeForXml(insplan.GroupName)).Append("</GroupName>");
			sb.Append("<GroupNum>").Append(SerializeStringEscapes.EscapeForXml(insplan.GroupNum)).Append("</GroupNum>");
			sb.Append("<PlanNote>").Append(SerializeStringEscapes.EscapeForXml(insplan.PlanNote)).Append("</PlanNote>");
			sb.Append("<FeeSched>").Append(insplan.FeeSched).Append("</FeeSched>");
			sb.Append("<PlanType>").Append(SerializeStringEscapes.EscapeForXml(insplan.PlanType)).Append("</PlanType>");
			sb.Append("<ClaimFormNum>").Append(insplan.ClaimFormNum).Append("</ClaimFormNum>");
			sb.Append("<UseAltCode>").Append((insplan.UseAltCode)?1:0).Append("</UseAltCode>");
			sb.Append("<ClaimsUseUCR>").Append((insplan.ClaimsUseUCR)?1:0).Append("</ClaimsUseUCR>");
			sb.Append("<CopayFeeSched>").Append(insplan.CopayFeeSched).Append("</CopayFeeSched>");
			sb.Append("<EmployerNum>").Append(insplan.EmployerNum).Append("</EmployerNum>");
			sb.Append("<CarrierNum>").Append(insplan.CarrierNum).Append("</CarrierNum>");
			sb.Append("<AllowedFeeSched>").Append(insplan.AllowedFeeSched).Append("</AllowedFeeSched>");
			sb.Append("<TrojanID>").Append(SerializeStringEscapes.EscapeForXml(insplan.TrojanID)).Append("</TrojanID>");
			sb.Append("<DivisionNo>").Append(SerializeStringEscapes.EscapeForXml(insplan.DivisionNo)).Append("</DivisionNo>");
			sb.Append("<IsMedical>").Append((insplan.IsMedical)?1:0).Append("</IsMedical>");
			sb.Append("<FilingCode>").Append(insplan.FilingCode).Append("</FilingCode>");
			sb.Append("<DentaideCardSequence>").Append(insplan.DentaideCardSequence).Append("</DentaideCardSequence>");
			sb.Append("<ShowBaseUnits>").Append((insplan.ShowBaseUnits)?1:0).Append("</ShowBaseUnits>");
			sb.Append("<CodeSubstNone>").Append((insplan.CodeSubstNone)?1:0).Append("</CodeSubstNone>");
			sb.Append("<IsHidden>").Append((insplan.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<MonthRenew>").Append(insplan.MonthRenew).Append("</MonthRenew>");
			sb.Append("<FilingCodeSubtype>").Append(insplan.FilingCodeSubtype).Append("</FilingCodeSubtype>");
			sb.Append("<CanadianPlanFlag>").Append(SerializeStringEscapes.EscapeForXml(insplan.CanadianPlanFlag)).Append("</CanadianPlanFlag>");
			sb.Append("<CanadianDiagnosticCode>").Append(SerializeStringEscapes.EscapeForXml(insplan.CanadianDiagnosticCode)).Append("</CanadianDiagnosticCode>");
			sb.Append("<CanadianInstitutionCode>").Append(SerializeStringEscapes.EscapeForXml(insplan.CanadianInstitutionCode)).Append("</CanadianInstitutionCode>");
			sb.Append("<RxBIN>").Append(SerializeStringEscapes.EscapeForXml(insplan.RxBIN)).Append("</RxBIN>");
			sb.Append("<CobRule>").Append((int)insplan.CobRule).Append("</CobRule>");
			sb.Append("</InsPlan>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.InsPlan Deserialize(string xml) {
			OpenDentBusiness.InsPlan insplan=new OpenDentBusiness.InsPlan();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PlanNum":
							insplan.PlanNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "GroupName":
							insplan.GroupName=reader.ReadContentAsString();
							break;
						case "GroupNum":
							insplan.GroupNum=reader.ReadContentAsString();
							break;
						case "PlanNote":
							insplan.PlanNote=reader.ReadContentAsString();
							break;
						case "FeeSched":
							insplan.FeeSched=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PlanType":
							insplan.PlanType=reader.ReadContentAsString();
							break;
						case "ClaimFormNum":
							insplan.ClaimFormNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "UseAltCode":
							insplan.UseAltCode=reader.ReadContentAsString()!="0";
							break;
						case "ClaimsUseUCR":
							insplan.ClaimsUseUCR=reader.ReadContentAsString()!="0";
							break;
						case "CopayFeeSched":
							insplan.CopayFeeSched=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EmployerNum":
							insplan.EmployerNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CarrierNum":
							insplan.CarrierNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AllowedFeeSched":
							insplan.AllowedFeeSched=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TrojanID":
							insplan.TrojanID=reader.ReadContentAsString();
							break;
						case "DivisionNo":
							insplan.DivisionNo=reader.ReadContentAsString();
							break;
						case "IsMedical":
							insplan.IsMedical=reader.ReadContentAsString()!="0";
							break;
						case "FilingCode":
							insplan.FilingCode=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DentaideCardSequence":
							insplan.DentaideCardSequence=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "ShowBaseUnits":
							insplan.ShowBaseUnits=reader.ReadContentAsString()!="0";
							break;
						case "CodeSubstNone":
							insplan.CodeSubstNone=reader.ReadContentAsString()!="0";
							break;
						case "IsHidden":
							insplan.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "MonthRenew":
							insplan.MonthRenew=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "FilingCodeSubtype":
							insplan.FilingCodeSubtype=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CanadianPlanFlag":
							insplan.CanadianPlanFlag=reader.ReadContentAsString();
							break;
						case "CanadianDiagnosticCode":
							insplan.CanadianDiagnosticCode=reader.ReadContentAsString();
							break;
						case "CanadianInstitutionCode":
							insplan.CanadianInstitutionCode=reader.ReadContentAsString();
							break;
						case "RxBIN":
							insplan.RxBIN=reader.ReadContentAsString();
							break;
						case "CobRule":
							insplan.CobRule=(OpenDentBusiness.EnumCobRule)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return insplan;
		}


	}
}