using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class HL7Msg {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.HL7Msg hl7msg) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<HL7Msg>");
			sb.Append("<HL7MsgNum>").Append(hl7msg.HL7MsgNum).Append("</HL7MsgNum>");
			sb.Append("<HL7Status>").Append((int)hl7msg.HL7Status).Append("</HL7Status>");
			sb.Append("<MsgText>").Append(SerializeStringEscapes.EscapeForXml(hl7msg.MsgText)).Append("</MsgText>");
			sb.Append("<AptNum>").Append(hl7msg.AptNum).Append("</AptNum>");
			sb.Append("<DateTStamp>").Append(hl7msg.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("<PatNum>").Append(hl7msg.PatNum).Append("</PatNum>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(hl7msg.Note)).Append("</Note>");
			sb.Append("</HL7Msg>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.HL7Msg Deserialize(string xml) {
			OpenDentBusiness.HL7Msg hl7msg=new OpenDentBusiness.HL7Msg();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "HL7MsgNum":
							hl7msg.HL7MsgNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "HL7Status":
							hl7msg.HL7Status=(OpenDentBusiness.HL7MessageStatus)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "MsgText":
							hl7msg.MsgText=reader.ReadContentAsString();
							break;
						case "AptNum":
							hl7msg.AptNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateTStamp":
							hl7msg.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "PatNum":
							hl7msg.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Note":
							hl7msg.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return hl7msg;
		}


	}
}