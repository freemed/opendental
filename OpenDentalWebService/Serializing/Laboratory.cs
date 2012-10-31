using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Laboratory {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Laboratory laboratory) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Laboratory>");
			sb.Append("<LaboratoryNum>").Append(laboratory.LaboratoryNum).Append("</LaboratoryNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(laboratory.Description)).Append("</Description>");
			sb.Append("<Phone>").Append(SerializeStringEscapes.EscapeForXml(laboratory.Phone)).Append("</Phone>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(laboratory.Notes)).Append("</Notes>");
			sb.Append("<Slip>").Append(laboratory.Slip).Append("</Slip>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(laboratory.Address)).Append("</Address>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(laboratory.City)).Append("</City>");
			sb.Append("<State>").Append(SerializeStringEscapes.EscapeForXml(laboratory.State)).Append("</State>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(laboratory.Zip)).Append("</Zip>");
			sb.Append("<Email>").Append(SerializeStringEscapes.EscapeForXml(laboratory.Email)).Append("</Email>");
			sb.Append("<WirelessPhone>").Append(SerializeStringEscapes.EscapeForXml(laboratory.WirelessPhone)).Append("</WirelessPhone>");
			sb.Append("</Laboratory>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Laboratory Deserialize(string xml) {
			OpenDentBusiness.Laboratory laboratory=new OpenDentBusiness.Laboratory();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LaboratoryNum":
							laboratory.LaboratoryNum=reader.ReadContentAsLong();
							break;
						case "Description":
							laboratory.Description=reader.ReadContentAsString();
							break;
						case "Phone":
							laboratory.Phone=reader.ReadContentAsString();
							break;
						case "Notes":
							laboratory.Notes=reader.ReadContentAsString();
							break;
						case "Slip":
							laboratory.Slip=reader.ReadContentAsLong();
							break;
						case "Address":
							laboratory.Address=reader.ReadContentAsString();
							break;
						case "City":
							laboratory.City=reader.ReadContentAsString();
							break;
						case "State":
							laboratory.State=reader.ReadContentAsString();
							break;
						case "Zip":
							laboratory.Zip=reader.ReadContentAsString();
							break;
						case "Email":
							laboratory.Email=reader.ReadContentAsString();
							break;
						case "WirelessPhone":
							laboratory.WirelessPhone=reader.ReadContentAsString();
							break;
					}
				}
			}
			return laboratory;
		}


	}
}