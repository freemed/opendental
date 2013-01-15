using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EmailAddress {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EmailAddress emailaddress) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EmailAddress>");
			sb.Append("<EmailAddressNum>").Append(emailaddress.EmailAddressNum).Append("</EmailAddressNum>");
			sb.Append("<SMTPserver>").Append(SerializeStringEscapes.EscapeForXml(emailaddress.SMTPserver)).Append("</SMTPserver>");
			sb.Append("<EmailUsername>").Append(SerializeStringEscapes.EscapeForXml(emailaddress.EmailUsername)).Append("</EmailUsername>");
			sb.Append("<EmailPassword>").Append(SerializeStringEscapes.EscapeForXml(emailaddress.EmailPassword)).Append("</EmailPassword>");
			sb.Append("<ServerPort>").Append(emailaddress.ServerPort).Append("</ServerPort>");
			sb.Append("<UseSSL>").Append((emailaddress.UseSSL)?1:0).Append("</UseSSL>");
			sb.Append("<SenderAddress>").Append(SerializeStringEscapes.EscapeForXml(emailaddress.SenderAddress)).Append("</SenderAddress>");
			sb.Append("</EmailAddress>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EmailAddress Deserialize(string xml) {
			OpenDentBusiness.EmailAddress emailaddress=new OpenDentBusiness.EmailAddress();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EmailAddressNum":
							emailaddress.EmailAddressNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SMTPserver":
							emailaddress.SMTPserver=reader.ReadContentAsString();
							break;
						case "EmailUsername":
							emailaddress.EmailUsername=reader.ReadContentAsString();
							break;
						case "EmailPassword":
							emailaddress.EmailPassword=reader.ReadContentAsString();
							break;
						case "ServerPort":
							emailaddress.ServerPort=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "UseSSL":
							emailaddress.UseSSL=reader.ReadContentAsString()!="0";
							break;
						case "SenderAddress":
							emailaddress.SenderAddress=reader.ReadContentAsString();
							break;
					}
				}
			}
			return emailaddress;
		}


	}
}