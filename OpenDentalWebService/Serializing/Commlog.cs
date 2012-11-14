using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Commlog {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Commlog commlog) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Commlog>");
			sb.Append("<CommlogNum>").Append(commlog.CommlogNum).Append("</CommlogNum>");
			sb.Append("<PatNum>").Append(commlog.PatNum).Append("</PatNum>");
			sb.Append("<CommDateTime>").Append(commlog.CommDateTime.ToString()).Append("</CommDateTime>");
			sb.Append("<CommType>").Append(commlog.CommType).Append("</CommType>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(commlog.Note)).Append("</Note>");
			sb.Append("<Mode_>").Append((int)commlog.Mode_).Append("</Mode_>");
			sb.Append("<SentOrReceived>").Append((int)commlog.SentOrReceived).Append("</SentOrReceived>");
			sb.Append("<UserNum>").Append(commlog.UserNum).Append("</UserNum>");
			sb.Append("<Signature>").Append(SerializeStringEscapes.EscapeForXml(commlog.Signature)).Append("</Signature>");
			sb.Append("<SigIsTopaz>").Append((commlog.SigIsTopaz)?1:0).Append("</SigIsTopaz>");
			sb.Append("<DateTStamp>").Append(commlog.DateTStamp.ToString()).Append("</DateTStamp>");
			sb.Append("<DateTimeEnd>").Append(commlog.DateTimeEnd.ToString()).Append("</DateTimeEnd>");
			sb.Append("</Commlog>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Commlog Deserialize(string xml) {
			OpenDentBusiness.Commlog commlog=new OpenDentBusiness.Commlog();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CommlogNum":
							commlog.CommlogNum=reader.ReadContentAsLong();
							break;
						case "PatNum":
							commlog.PatNum=reader.ReadContentAsLong();
							break;
						case "CommDateTime":
							commlog.CommDateTime=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "CommType":
							commlog.CommType=reader.ReadContentAsLong();
							break;
						case "Note":
							commlog.Note=reader.ReadContentAsString();
							break;
						case "Mode_":
							commlog.Mode_=(OpenDentBusiness.CommItemMode)reader.ReadContentAsInt();
							break;
						case "SentOrReceived":
							commlog.SentOrReceived=(OpenDentBusiness.CommSentOrReceived)reader.ReadContentAsInt();
							break;
						case "UserNum":
							commlog.UserNum=reader.ReadContentAsLong();
							break;
						case "Signature":
							commlog.Signature=reader.ReadContentAsString();
							break;
						case "SigIsTopaz":
							commlog.SigIsTopaz=reader.ReadContentAsString()!="0";
							break;
						case "DateTStamp":
							commlog.DateTStamp=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "DateTimeEnd":
							commlog.DateTimeEnd=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return commlog;
		}


	}
}