using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Deposit {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Deposit deposit) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Deposit>");
			sb.Append("<DepositNum>").Append(deposit.DepositNum).Append("</DepositNum>");
			sb.Append("<DateDeposit>").Append(deposit.DateDeposit.ToString()).Append("</DateDeposit>");
			sb.Append("<BankAccountInfo>").Append(SerializeStringEscapes.EscapeForXml(deposit.BankAccountInfo)).Append("</BankAccountInfo>");
			sb.Append("<Amount>").Append(deposit.Amount).Append("</Amount>");
			sb.Append("<Memo>").Append(SerializeStringEscapes.EscapeForXml(deposit.Memo)).Append("</Memo>");
			sb.Append("</Deposit>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Deposit Deserialize(string xml) {
			OpenDentBusiness.Deposit deposit=new OpenDentBusiness.Deposit();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DepositNum":
							deposit.DepositNum=reader.ReadContentAsLong();
							break;
						case "DateDeposit":
							deposit.DateDeposit=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "BankAccountInfo":
							deposit.BankAccountInfo=reader.ReadContentAsString();
							break;
						case "Amount":
							deposit.Amount=reader.ReadContentAsDouble();
							break;
						case "Memo":
							deposit.Memo=reader.ReadContentAsString();
							break;
					}
				}
			}
			return deposit;
		}


	}
}