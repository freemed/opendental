using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Payment {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Payment payment) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Payment>");
			sb.Append("<PayNum>").Append(payment.PayNum).Append("</PayNum>");
			sb.Append("<PayType>").Append(payment.PayType).Append("</PayType>");
			sb.Append("<PayDate>").Append(payment.PayDate.ToString("yyyyMMddHHmmss")).Append("</PayDate>");
			sb.Append("<PayAmt>").Append(payment.PayAmt).Append("</PayAmt>");
			sb.Append("<CheckNum>").Append(SerializeStringEscapes.EscapeForXml(payment.CheckNum)).Append("</CheckNum>");
			sb.Append("<BankBranch>").Append(SerializeStringEscapes.EscapeForXml(payment.BankBranch)).Append("</BankBranch>");
			sb.Append("<PayNote>").Append(SerializeStringEscapes.EscapeForXml(payment.PayNote)).Append("</PayNote>");
			sb.Append("<IsSplit>").Append((payment.IsSplit)?1:0).Append("</IsSplit>");
			sb.Append("<PatNum>").Append(payment.PatNum).Append("</PatNum>");
			sb.Append("<ClinicNum>").Append(payment.ClinicNum).Append("</ClinicNum>");
			sb.Append("<DateEntry>").Append(payment.DateEntry.ToString("yyyyMMddHHmmss")).Append("</DateEntry>");
			sb.Append("<DepositNum>").Append(payment.DepositNum).Append("</DepositNum>");
			sb.Append("<Receipt>").Append(SerializeStringEscapes.EscapeForXml(payment.Receipt)).Append("</Receipt>");
			sb.Append("<IsRecurringCC>").Append((payment.IsRecurringCC)?1:0).Append("</IsRecurringCC>");
			sb.Append("</Payment>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Payment Deserialize(string xml) {
			OpenDentBusiness.Payment payment=new OpenDentBusiness.Payment();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PayNum":
							payment.PayNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PayType":
							payment.PayType=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PayDate":
							payment.PayDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "PayAmt":
							payment.PayAmt=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
						case "CheckNum":
							payment.CheckNum=reader.ReadContentAsString();
							break;
						case "BankBranch":
							payment.BankBranch=reader.ReadContentAsString();
							break;
						case "PayNote":
							payment.PayNote=reader.ReadContentAsString();
							break;
						case "IsSplit":
							payment.IsSplit=reader.ReadContentAsString()!="0";
							break;
						case "PatNum":
							payment.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ClinicNum":
							payment.ClinicNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateEntry":
							payment.DateEntry=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "DepositNum":
							payment.DepositNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Receipt":
							payment.Receipt=reader.ReadContentAsString();
							break;
						case "IsRecurringCC":
							payment.IsRecurringCC=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return payment;
		}


	}
}