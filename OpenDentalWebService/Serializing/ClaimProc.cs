using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClaimProc {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClaimProc claimproc) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClaimProc>");
			sb.Append("<ClaimProcNum>").Append(claimproc.ClaimProcNum).Append("</ClaimProcNum>");
			sb.Append("<ProcNum>").Append(claimproc.ProcNum).Append("</ProcNum>");
			sb.Append("<ClaimNum>").Append(claimproc.ClaimNum).Append("</ClaimNum>");
			sb.Append("<PatNum>").Append(claimproc.PatNum).Append("</PatNum>");
			sb.Append("<ProvNum>").Append(claimproc.ProvNum).Append("</ProvNum>");
			sb.Append("<FeeBilled>").Append(claimproc.FeeBilled).Append("</FeeBilled>");
			sb.Append("<InsPayEst>").Append(claimproc.InsPayEst).Append("</InsPayEst>");
			sb.Append("<DedApplied>").Append(claimproc.DedApplied).Append("</DedApplied>");
			sb.Append("<Status>").Append((int)claimproc.Status).Append("</Status>");
			sb.Append("<InsPayAmt>").Append(claimproc.InsPayAmt).Append("</InsPayAmt>");
			sb.Append("<Remarks>").Append(SerializeStringEscapes.EscapeForXml(claimproc.Remarks)).Append("</Remarks>");
			sb.Append("<ClaimPaymentNum>").Append(claimproc.ClaimPaymentNum).Append("</ClaimPaymentNum>");
			sb.Append("<PlanNum>").Append(claimproc.PlanNum).Append("</PlanNum>");
			sb.Append("<DateCP>").Append(claimproc.DateCP.ToLongDateString()).Append("</DateCP>");
			sb.Append("<WriteOff>").Append(claimproc.WriteOff).Append("</WriteOff>");
			sb.Append("<CodeSent>").Append(SerializeStringEscapes.EscapeForXml(claimproc.CodeSent)).Append("</CodeSent>");
			sb.Append("<AllowedOverride>").Append(claimproc.AllowedOverride).Append("</AllowedOverride>");
			sb.Append("<Percentage>").Append(claimproc.Percentage).Append("</Percentage>");
			sb.Append("<PercentOverride>").Append(claimproc.PercentOverride).Append("</PercentOverride>");
			sb.Append("<CopayAmt>").Append(claimproc.CopayAmt).Append("</CopayAmt>");
			sb.Append("<NoBillIns>").Append((claimproc.NoBillIns)?1:0).Append("</NoBillIns>");
			sb.Append("<PaidOtherIns>").Append(claimproc.PaidOtherIns).Append("</PaidOtherIns>");
			sb.Append("<BaseEst>").Append(claimproc.BaseEst).Append("</BaseEst>");
			sb.Append("<CopayOverride>").Append(claimproc.CopayOverride).Append("</CopayOverride>");
			sb.Append("<ProcDate>").Append(claimproc.ProcDate.ToLongDateString()).Append("</ProcDate>");
			sb.Append("<DateEntry>").Append(claimproc.DateEntry.ToLongDateString()).Append("</DateEntry>");
			sb.Append("<LineNumber>").Append(claimproc.LineNumber).Append("</LineNumber>");
			sb.Append("<DedEst>").Append(claimproc.DedEst).Append("</DedEst>");
			sb.Append("<DedEstOverride>").Append(claimproc.DedEstOverride).Append("</DedEstOverride>");
			sb.Append("<InsEstTotal>").Append(claimproc.InsEstTotal).Append("</InsEstTotal>");
			sb.Append("<InsEstTotalOverride>").Append(claimproc.InsEstTotalOverride).Append("</InsEstTotalOverride>");
			sb.Append("<PaidOtherInsOverride>").Append(claimproc.PaidOtherInsOverride).Append("</PaidOtherInsOverride>");
			sb.Append("<EstimateNote>").Append(SerializeStringEscapes.EscapeForXml(claimproc.EstimateNote)).Append("</EstimateNote>");
			sb.Append("<WriteOffEst>").Append(claimproc.WriteOffEst).Append("</WriteOffEst>");
			sb.Append("<WriteOffEstOverride>").Append(claimproc.WriteOffEstOverride).Append("</WriteOffEstOverride>");
			sb.Append("<ClinicNum>").Append(claimproc.ClinicNum).Append("</ClinicNum>");
			sb.Append("<InsSubNum>").Append(claimproc.InsSubNum).Append("</InsSubNum>");
			sb.Append("<PaymentRow>").Append(claimproc.PaymentRow).Append("</PaymentRow>");
			sb.Append("</ClaimProc>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClaimProc Deserialize(string xml) {
			OpenDentBusiness.ClaimProc claimproc=new OpenDentBusiness.ClaimProc();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimProcNum":
							claimproc.ClaimProcNum=reader.ReadContentAsLong();
							break;
						case "ProcNum":
							claimproc.ProcNum=reader.ReadContentAsLong();
							break;
						case "ClaimNum":
							claimproc.ClaimNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							claimproc.PatNum=reader.ReadContentAsLong();
							break;
						case "ProvNum":
							claimproc.ProvNum=reader.ReadContentAsLong();
							break;
						case "FeeBilled":
							claimproc.FeeBilled=reader.ReadContentAsDouble();
							break;
						case "InsPayEst":
							claimproc.InsPayEst=reader.ReadContentAsDouble();
							break;
						case "DedApplied":
							claimproc.DedApplied=reader.ReadContentAsDouble();
							break;
						case "Status":
							claimproc.Status=(OpenDentBusiness.ClaimProcStatus)reader.ReadContentAsInt();
							break;
						case "InsPayAmt":
							claimproc.InsPayAmt=reader.ReadContentAsDouble();
							break;
						case "Remarks":
							claimproc.Remarks=reader.ReadContentAsString();
							break;
						case "ClaimPaymentNum":
							claimproc.ClaimPaymentNum=reader.ReadContentAsLong();
							break;
						case "PlanNum":
							claimproc.PlanNum=reader.ReadContentAsLong();
							break;
						case "DateCP":
							claimproc.DateCP=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "WriteOff":
							claimproc.WriteOff=reader.ReadContentAsDouble();
							break;
						case "CodeSent":
							claimproc.CodeSent=reader.ReadContentAsString();
							break;
						case "AllowedOverride":
							claimproc.AllowedOverride=reader.ReadContentAsDouble();
							break;
						case "Percentage":
							claimproc.Percentage=reader.ReadContentAsInt();
							break;
						case "PercentOverride":
							claimproc.PercentOverride=reader.ReadContentAsInt();
							break;
						case "CopayAmt":
							claimproc.CopayAmt=reader.ReadContentAsDouble();
							break;
						case "NoBillIns":
							claimproc.NoBillIns=reader.ReadContentAsString()!="0";
							break;
						case "PaidOtherIns":
							claimproc.PaidOtherIns=reader.ReadContentAsDouble();
							break;
						case "BaseEst":
							claimproc.BaseEst=reader.ReadContentAsDouble();
							break;
						case "CopayOverride":
							claimproc.CopayOverride=reader.ReadContentAsDouble();
							break;
						case "ProcDate":
							claimproc.ProcDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateEntry":
							claimproc.DateEntry=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "LineNumber":
							claimproc.LineNumber=(byte)reader.ReadContentAsInt();
							break;
						case "DedEst":
							claimproc.DedEst=reader.ReadContentAsDouble();
							break;
						case "DedEstOverride":
							claimproc.DedEstOverride=reader.ReadContentAsDouble();
							break;
						case "InsEstTotal":
							claimproc.InsEstTotal=reader.ReadContentAsDouble();
							break;
						case "InsEstTotalOverride":
							claimproc.InsEstTotalOverride=reader.ReadContentAsDouble();
							break;
						case "PaidOtherInsOverride":
							claimproc.PaidOtherInsOverride=reader.ReadContentAsDouble();
							break;
						case "EstimateNote":
							claimproc.EstimateNote=reader.ReadContentAsString();
							break;
						case "WriteOffEst":
							claimproc.WriteOffEst=reader.ReadContentAsDouble();
							break;
						case "WriteOffEstOverride":
							claimproc.WriteOffEstOverride=reader.ReadContentAsDouble();
							break;
						case "ClinicNum":
							claimproc.ClinicNum=reader.ReadContentAsLong();
							break;
						case "InsSubNum":
							claimproc.InsSubNum=reader.ReadContentAsLong();
							break;
						case "PaymentRow":
							claimproc.PaymentRow=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return claimproc;
		}


	}
}