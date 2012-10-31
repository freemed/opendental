using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EmailAttach {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EmailAttach emailattach) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EmailAttach>");
			sb.Append("<EmailAttachNum>").Append(emailattach.EmailAttachNum).Append("</EmailAttachNum>");
			sb.Append("<EmailMessageNum>").Append(emailattach.EmailMessageNum).Append("</EmailMessageNum>");
			sb.Append("<DisplayedFileName>").Append(SerializeStringEscapes.EscapeForXml(emailattach.DisplayedFileName)).Append("</DisplayedFileName>");
			sb.Append("<ActualFileName>").Append(SerializeStringEscapes.EscapeForXml(emailattach.ActualFileName)).Append("</ActualFileName>");
			sb.Append("</EmailAttach>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EmailAttach Deserialize(string xml) {
			OpenDentBusiness.EmailAttach emailattach=new OpenDentBusiness.EmailAttach();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EmailAttachNum":
							emailattach.EmailAttachNum=reader.ReadContentAsLong();
							break;
						case "EmailMessageNum":
							emailattach.EmailMessageNum=reader.ReadContentAsLong();
							break;
						case "DisplayedFileName":
							emailattach.DisplayedFileName=reader.ReadContentAsString();
							break;
						case "ActualFileName":
							emailattach.ActualFileName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return emailattach;
		}


	}
}