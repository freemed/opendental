using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PaySplit {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PaySplit paysplit) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PaySplit>");
			sb.Append("<SplitNum>").Append(paysplit.SplitNum).Append("</SplitNum>");
			sb.Append("<SplitAmt>").Append(paysplit.SplitAmt).Append("</SplitAmt>");
			sb.Append("<PatNum>").Append(paysplit.PatNum).Append("</PatNum>");
			sb.Append("<ProcDate>").Append(paysplit.ProcDate.ToLongDateString()).Append("</ProcDate>");
			sb.Append("<PayNum>").Append(paysplit.PayNum).Append("</PayNum>");
			sb.Append("<IsDiscount>").Append((paysplit.IsDiscount)?1:0).Append("</IsDiscount>");
			sb.Append("<DiscountType>").Append(paysplit.DiscountType).Append("</DiscountType>");
			sb.Append("<ProvNum>").Append(paysplit.ProvNum).Append("</ProvNum>");
			sb.Append("<PayPlanNum>").Append(paysplit.PayPlanNum).Append("</PayPlanNum>");
			sb.Append("<DatePay>").Append(paysplit.DatePay.ToLongDateString()).Append("</DatePay>");
			sb.Append("<ProcNum>").Append(paysplit.ProcNum).Append("</ProcNum>");
			sb.Append("<DateEntry>").Append(paysplit.DateEntry.ToLongDateString()).Append("</DateEntry>");
			sb.Append("<UnearnedType>").Append(paysplit.UnearnedType).Append("</UnearnedType>");
			sb.Append("<ClinicNum>").Append(paysplit.ClinicNum).Append("</ClinicNum>");
			sb.Append("</PaySplit>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PaySplit Deserialize(string xml) {
			OpenDentBusiness.PaySplit paysplit=new OpenDentBusiness.PaySplit();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SplitNum":
							paysplit.SplitNum=reader.ReadContentAsLong();
							break;
						case "SplitAmt":
							paysplit.SplitAmt=reader.ReadContentAsDouble();
							break;
						case "PatNum":
							paysplit.PatNum=reader.ReadContentAsLong();
							break;
						case "ProcDate":
							paysplit.ProcDate=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "PayNum":
							paysplit.PayNum=reader.ReadContentAsLong();
							break;
						case "IsDiscount":
							paysplit.IsDiscount=reader.ReadContentAsString()!="0";
							break;
						case "DiscountType":
							paysplit.DiscountType=(byte)reader.ReadContentAsInt();
							break;
						case "ProvNum":
							paysplit.ProvNum=reader.ReadContentAsLong();
							break;
						case "PayPlanNum":
							paysplit.PayPlanNum=reader.ReadContentAsLong();
							break;
						case "DatePay":
							paysplit.DatePay=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "ProcNum":
							paysplit.ProcNum=reader.ReadContentAsLong();
							break;
						case "DateEntry":
							paysplit.DateEntry=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "UnearnedType":
							paysplit.UnearnedType=reader.ReadContentAsLong();
							break;
						case "ClinicNum":
							paysplit.ClinicNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return paysplit;
		}


	}
}