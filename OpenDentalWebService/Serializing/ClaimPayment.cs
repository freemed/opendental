using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClaimPayment {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClaimPayment claimpayment) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClaimPayment>");
			sb.Append("<ClaimPaymentNum>").Append(claimpayment.ClaimPaymentNum).Append("</ClaimPaymentNum>");
			sb.Append("<CheckDate>").Append(claimpayment.CheckDate.ToString()).Append("</CheckDate>");
			sb.Append("<CheckAmt>").Append(claimpayment.CheckAmt).Append("</CheckAmt>");
			sb.Append("<CheckNum>").Append(SerializeStringEscapes.EscapeForXml(claimpayment.CheckNum)).Append("</CheckNum>");
			sb.Append("<BankBranch>").Append(SerializeStringEscapes.EscapeForXml(claimpayment.BankBranch)).Append("</BankBranch>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(claimpayment.Note)).Append("</Note>");
			sb.Append("<ClinicNum>").Append(claimpayment.ClinicNum).Append("</ClinicNum>");
			sb.Append("<DepositNum>").Append(claimpayment.DepositNum).Append("</DepositNum>");
			sb.Append("<CarrierName>").Append(SerializeStringEscapes.EscapeForXml(claimpayment.CarrierName)).Append("</CarrierName>");
			sb.Append("<DateIssued>").Append(claimpayment.DateIssued.ToString()).Append("</DateIssued>");
			sb.Append("<IsPartial>").Append((claimpayment.IsPartial)?1:0).Append("</IsPartial>");
			sb.Append("</ClaimPayment>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClaimPayment Deserialize(string xml) {
			OpenDentBusiness.ClaimPayment claimpayment=new OpenDentBusiness.ClaimPayment();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimPaymentNum":
							claimpayment.ClaimPaymentNum=reader.ReadContentAsLong();
							break;
						case "CheckDate":
							claimpayment.CheckDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "CheckAmt":
							claimpayment.CheckAmt=reader.ReadContentAsDouble();
							break;
						case "CheckNum":
							claimpayment.CheckNum=reader.ReadContentAsString();
							break;
						case "BankBranch":
							claimpayment.BankBranch=reader.ReadContentAsString();
							break;
						case "Note":
							claimpayment.Note=reader.ReadContentAsString();
							break;
						case "ClinicNum":
							claimpayment.ClinicNum=reader.ReadContentAsLong();
							break;
						case "DepositNum":
							claimpayment.DepositNum=reader.ReadContentAsLong();
							break;
						case "CarrierName":
							claimpayment.CarrierName=reader.ReadContentAsString();
							break;
						case "DateIssued":
							claimpayment.DateIssued=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "IsPartial":
							claimpayment.IsPartial=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return claimpayment;
		}


	}
}