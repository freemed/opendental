using System;
using System.Text;

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
			//TODO: Make an example for the CRUD programmer.
			return null;
		}


	}
}