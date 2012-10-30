using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Account {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Account account) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Account>");
			sb.Append("<AccountNum>").Append(account.AccountNum).Append("</AccountNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(account.Description)).Append("</Description>");
			sb.Append("<AcctType>").Append((int)account.AcctType).Append("</AcctType>");
			sb.Append("<BankNumber>").Append(SerializeStringEscapes.EscapeForXml(account.BankNumber)).Append("</BankNumber>");
			sb.Append("<Inactive>").Append((account.Inactive)?1:0).Append("</Inactive>");
			sb.Append("<AccountColor>").Append(account.AccountColor.ToArgb()).Append("</AccountColor>");
			sb.Append("</Account>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Account Deserialize(string xml) {
			OpenDentBusiness.Account account=new OpenDentBusiness.Account();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AccountNum":
							account.AccountNum=reader.ReadContentAsLong();
							break;
						case "Description":
							account.Description=reader.ReadContentAsString();
							break;
						case "AcctType":
							account.AcctType=(OpenDentBusiness.AccountType)reader.ReadContentAsInt();
							break;
						case "BankNumber":
							account.BankNumber=reader.ReadContentAsString();
							break;
						case "Inactive":
							account.Inactive=reader.ReadContentAsString()!="0";
							break;
						case "AccountColor":
							account.AccountColor=Color.FromArgb(reader.ReadContentAsInt());
							break;
					}
				}
			}
			return account;
		}


	}
}