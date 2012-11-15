using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PayPlan {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PayPlan payplan) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PayPlan>");
			sb.Append("<PayPlanNum>").Append(payplan.PayPlanNum).Append("</PayPlanNum>");
			sb.Append("<PatNum>").Append(payplan.PatNum).Append("</PatNum>");
			sb.Append("<Guarantor>").Append(payplan.Guarantor).Append("</Guarantor>");
			sb.Append("<PayPlanDate>").Append(payplan.PayPlanDate.ToString("yyyyMMddHHmmss")).Append("</PayPlanDate>");
			sb.Append("<APR>").Append(payplan.APR).Append("</APR>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(payplan.Note)).Append("</Note>");
			sb.Append("<PlanNum>").Append(payplan.PlanNum).Append("</PlanNum>");
			sb.Append("<CompletedAmt>").Append(payplan.CompletedAmt).Append("</CompletedAmt>");
			sb.Append("<InsSubNum>").Append(payplan.InsSubNum).Append("</InsSubNum>");
			sb.Append("</PayPlan>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PayPlan Deserialize(string xml) {
			OpenDentBusiness.PayPlan payplan=new OpenDentBusiness.PayPlan();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PayPlanNum":
							payplan.PayPlanNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							payplan.PatNum=reader.ReadContentAsLong();
							break;
						case "Guarantor":
							payplan.Guarantor=reader.ReadContentAsLong();
							break;
						case "PayPlanDate":
							payplan.PayPlanDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "APR":
							payplan.APR=reader.ReadContentAsDouble();
							break;
						case "Note":
							payplan.Note=reader.ReadContentAsString();
							break;
						case "PlanNum":
							payplan.PlanNum=reader.ReadContentAsLong();
							break;
						case "CompletedAmt":
							payplan.CompletedAmt=reader.ReadContentAsDouble();
							break;
						case "InsSubNum":
							payplan.InsSubNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return payplan;
		}


	}
}