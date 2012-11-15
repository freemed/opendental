using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class HL7DefMessage {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.HL7DefMessage hl7defmessage) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<HL7DefMessage>");
			sb.Append("<HL7DefMessageNum>").Append(hl7defmessage.HL7DefMessageNum).Append("</HL7DefMessageNum>");
			sb.Append("<HL7DefNum>").Append(hl7defmessage.HL7DefNum).Append("</HL7DefNum>");
			sb.Append("<MessageType>").Append((int)hl7defmessage.MessageType).Append("</MessageType>");
			sb.Append("<EventType>").Append((int)hl7defmessage.EventType).Append("</EventType>");
			sb.Append("<InOrOut>").Append((int)hl7defmessage.InOrOut).Append("</InOrOut>");
			sb.Append("<ItemOrder>").Append(hl7defmessage.ItemOrder).Append("</ItemOrder>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(hl7defmessage.Note)).Append("</Note>");
			sb.Append("</HL7DefMessage>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.HL7DefMessage Deserialize(string xml) {
			OpenDentBusiness.HL7DefMessage hl7defmessage=new OpenDentBusiness.HL7DefMessage();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "HL7DefMessageNum":
							hl7defmessage.HL7DefMessageNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "HL7DefNum":
							hl7defmessage.HL7DefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "MessageType":
							hl7defmessage.MessageType=(OpenDentBusiness.MessageTypeHL7)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "EventType":
							hl7defmessage.EventType=(OpenDentBusiness.EventTypeHL7)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "InOrOut":
							hl7defmessage.InOrOut=(OpenDentBusiness.InOutHL7)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							hl7defmessage.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Note":
							hl7defmessage.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return hl7defmessage;
		}


	}
}