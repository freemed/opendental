using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Transaction {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Transaction transaction) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Transaction>");
			sb.Append("<TransactionNum>").Append(transaction.TransactionNum).Append("</TransactionNum>");
			sb.Append("<DateTimeEntry>").Append(transaction.DateTimeEntry.ToString("yyyyMMddHHmmss")).Append("</DateTimeEntry>");
			sb.Append("<UserNum>").Append(transaction.UserNum).Append("</UserNum>");
			sb.Append("<DepositNum>").Append(transaction.DepositNum).Append("</DepositNum>");
			sb.Append("<PayNum>").Append(transaction.PayNum).Append("</PayNum>");
			sb.Append("</Transaction>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Transaction Deserialize(string xml) {
			OpenDentBusiness.Transaction transaction=new OpenDentBusiness.Transaction();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TransactionNum":
							transaction.TransactionNum=reader.ReadContentAsLong();
							break;
						case "DateTimeEntry":
							transaction.DateTimeEntry=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "UserNum":
							transaction.UserNum=reader.ReadContentAsLong();
							break;
						case "DepositNum":
							transaction.DepositNum=reader.ReadContentAsLong();
							break;
						case "PayNum":
							transaction.PayNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return transaction;
		}


	}
}