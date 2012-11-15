using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class CreditCard {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.CreditCard creditcard) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<CreditCard>");
			sb.Append("<CreditCardNum>").Append(creditcard.CreditCardNum).Append("</CreditCardNum>");
			sb.Append("<PatNum>").Append(creditcard.PatNum).Append("</PatNum>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(creditcard.Address)).Append("</Address>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(creditcard.Zip)).Append("</Zip>");
			sb.Append("<XChargeToken>").Append(SerializeStringEscapes.EscapeForXml(creditcard.XChargeToken)).Append("</XChargeToken>");
			sb.Append("<CCNumberMasked>").Append(SerializeStringEscapes.EscapeForXml(creditcard.CCNumberMasked)).Append("</CCNumberMasked>");
			sb.Append("<CCExpiration>").Append(creditcard.CCExpiration.ToString("yyyyMMddHHmmss")).Append("</CCExpiration>");
			sb.Append("<ItemOrder>").Append(creditcard.ItemOrder).Append("</ItemOrder>");
			sb.Append("<ChargeAmt>").Append(creditcard.ChargeAmt).Append("</ChargeAmt>");
			sb.Append("<DateStart>").Append(creditcard.DateStart.ToString("yyyyMMddHHmmss")).Append("</DateStart>");
			sb.Append("<DateStop>").Append(creditcard.DateStop.ToString("yyyyMMddHHmmss")).Append("</DateStop>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(creditcard.Note)).Append("</Note>");
			sb.Append("<PayPlanNum>").Append(creditcard.PayPlanNum).Append("</PayPlanNum>");
			sb.Append("</CreditCard>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.CreditCard Deserialize(string xml) {
			OpenDentBusiness.CreditCard creditcard=new OpenDentBusiness.CreditCard();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CreditCardNum":
							creditcard.CreditCardNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							creditcard.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Address":
							creditcard.Address=reader.ReadContentAsString();
							break;
						case "Zip":
							creditcard.Zip=reader.ReadContentAsString();
							break;
						case "XChargeToken":
							creditcard.XChargeToken=reader.ReadContentAsString();
							break;
						case "CCNumberMasked":
							creditcard.CCNumberMasked=reader.ReadContentAsString();
							break;
						case "CCExpiration":
							creditcard.CCExpiration=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "ItemOrder":
							creditcard.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ChargeAmt":
							creditcard.ChargeAmt=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "DateStart":
							creditcard.DateStart=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DateStop":
							creditcard.DateStop=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Note":
							creditcard.Note=reader.ReadContentAsString();
							break;
						case "PayPlanNum":
							creditcard.PayPlanNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return creditcard;
		}


	}
}