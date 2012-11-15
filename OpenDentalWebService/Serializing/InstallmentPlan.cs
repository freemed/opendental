using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class InstallmentPlan {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.InstallmentPlan installmentplan) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<InstallmentPlan>");
			sb.Append("<InstallmentPlanNum>").Append(installmentplan.InstallmentPlanNum).Append("</InstallmentPlanNum>");
			sb.Append("<PatNum>").Append(installmentplan.PatNum).Append("</PatNum>");
			sb.Append("<DateAgreement>").Append(installmentplan.DateAgreement.ToString("yyyyMMddHHmmss")).Append("</DateAgreement>");
			sb.Append("<DateFirstPayment>").Append(installmentplan.DateFirstPayment.ToString("yyyyMMddHHmmss")).Append("</DateFirstPayment>");
			sb.Append("<MonthlyPayment>").Append(installmentplan.MonthlyPayment).Append("</MonthlyPayment>");
			sb.Append("<APR>").Append(installmentplan.APR).Append("</APR>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(installmentplan.Note)).Append("</Note>");
			sb.Append("</InstallmentPlan>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.InstallmentPlan Deserialize(string xml) {
			OpenDentBusiness.InstallmentPlan installmentplan=new OpenDentBusiness.InstallmentPlan();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "InstallmentPlanNum":
							installmentplan.InstallmentPlanNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							installmentplan.PatNum=reader.ReadContentAsLong();
							break;
						case "DateAgreement":
							installmentplan.DateAgreement=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateFirstPayment":
							installmentplan.DateFirstPayment=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "MonthlyPayment":
							installmentplan.MonthlyPayment=reader.ReadContentAsDouble();
							break;
						case "APR":
							installmentplan.APR=reader.ReadContentAsFloat();
							break;
						case "Note":
							installmentplan.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return installmentplan;
		}


	}
}