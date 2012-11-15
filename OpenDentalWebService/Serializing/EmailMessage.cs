using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EmailMessage {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EmailMessage emailmessage) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EmailMessage>");
			sb.Append("<EmailMessageNum>").Append(emailmessage.EmailMessageNum).Append("</EmailMessageNum>");
			sb.Append("<PatNum>").Append(emailmessage.PatNum).Append("</PatNum>");
			sb.Append("<ToAddress>").Append(SerializeStringEscapes.EscapeForXml(emailmessage.ToAddress)).Append("</ToAddress>");
			sb.Append("<FromAddress>").Append(SerializeStringEscapes.EscapeForXml(emailmessage.FromAddress)).Append("</FromAddress>");
			sb.Append("<Subject>").Append(SerializeStringEscapes.EscapeForXml(emailmessage.Subject)).Append("</Subject>");
			sb.Append("<BodyText>").Append(SerializeStringEscapes.EscapeForXml(emailmessage.BodyText)).Append("</BodyText>");
			sb.Append("<MsgDateTime>").Append(emailmessage.MsgDateTime.ToString("yyyyMMddHHmmss")).Append("</MsgDateTime>");
			sb.Append("<SentOrReceived>").Append((int)emailmessage.SentOrReceived).Append("</SentOrReceived>");
			sb.Append("</EmailMessage>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EmailMessage Deserialize(string xml) {
			OpenDentBusiness.EmailMessage emailmessage=new OpenDentBusiness.EmailMessage();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EmailMessageNum":
							emailmessage.EmailMessageNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							emailmessage.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ToAddress":
							emailmessage.ToAddress=reader.ReadContentAsString();
							break;
						case "FromAddress":
							emailmessage.FromAddress=reader.ReadContentAsString();
							break;
						case "Subject":
							emailmessage.Subject=reader.ReadContentAsString();
							break;
						case "BodyText":
							emailmessage.BodyText=reader.ReadContentAsString();
							break;
						case "MsgDateTime":
							emailmessage.MsgDateTime=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "SentOrReceived":
							emailmessage.SentOrReceived=(OpenDentBusiness.CommSentOrReceived)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return emailmessage;
		}


	}
}