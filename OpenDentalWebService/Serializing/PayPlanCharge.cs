using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PayPlanCharge {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PayPlanCharge payplancharge) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PayPlanCharge>");
			sb.Append("<PayPlanChargeNum>").Append(payplancharge.PayPlanChargeNum).Append("</PayPlanChargeNum>");
			sb.Append("<PayPlanNum>").Append(payplancharge.PayPlanNum).Append("</PayPlanNum>");
			sb.Append("<Guarantor>").Append(payplancharge.Guarantor).Append("</Guarantor>");
			sb.Append("<PatNum>").Append(payplancharge.PatNum).Append("</PatNum>");
			sb.Append("<ChargeDate>").Append(payplancharge.ChargeDate.ToString("yyyyMMddHHmmss")).Append("</ChargeDate>");
			sb.Append("<Principal>").Append(payplancharge.Principal).Append("</Principal>");
			sb.Append("<Interest>").Append(payplancharge.Interest).Append("</Interest>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(payplancharge.Note)).Append("</Note>");
			sb.Append("<ProvNum>").Append(payplancharge.ProvNum).Append("</ProvNum>");
			sb.Append("<ClinicNum>").Append(payplancharge.ClinicNum).Append("</ClinicNum>");
			sb.Append("</PayPlanCharge>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PayPlanCharge Deserialize(string xml) {
			OpenDentBusiness.PayPlanCharge payplancharge=new OpenDentBusiness.PayPlanCharge();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PayPlanChargeNum":
							payplancharge.PayPlanChargeNum=reader.ReadContentAsLong();
							break;
						case "PayPlanNum":
							payplancharge.PayPlanNum=reader.ReadContentAsLong();
							break;
						case "Guarantor":
							payplancharge.Guarantor=reader.ReadContentAsLong();
							break;
						case "PatNum":
							payplancharge.PatNum=reader.ReadContentAsLong();
							break;
						case "ChargeDate":
							payplancharge.ChargeDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Principal":
							payplancharge.Principal=reader.ReadContentAsDouble();
							break;
						case "Interest":
							payplancharge.Interest=reader.ReadContentAsDouble();
							break;
						case "Note":
							payplancharge.Note=reader.ReadContentAsString();
							break;
						case "ProvNum":
							payplancharge.ProvNum=reader.ReadContentAsLong();
							break;
						case "ClinicNum":
							payplancharge.ClinicNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return payplancharge;
		}


	}
}