using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AccountingAutoPay {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AccountingAutoPay accountingautopay) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AccountingAutoPay>");
			sb.Append("<AccountingAutoPayNum>").Append(accountingautopay.AccountingAutoPayNum).Append("</AccountingAutoPayNum>");
			sb.Append("<PayType>").Append(accountingautopay.PayType).Append("</PayType>");
			sb.Append("<PickList>").Append(SerializeStringEscapes.EscapeForXml(accountingautopay.PickList)).Append("</PickList>");
			sb.Append("</AccountingAutoPay>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AccountingAutoPay Deserialize(string xml) {
			OpenDentBusiness.AccountingAutoPay accountingautopay=new OpenDentBusiness.AccountingAutoPay();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AccountingAutoPayNum":
							accountingautopay.AccountingAutoPayNum=reader.ReadContentAsLong();
							break;
						case "PayType":
							accountingautopay.PayType=reader.ReadContentAsLong();
							break;
						case "PickList":
							accountingautopay.PickList=reader.ReadContentAsString();
							break;
					}
				}
			}
			return accountingautopay;
		}


	}
}