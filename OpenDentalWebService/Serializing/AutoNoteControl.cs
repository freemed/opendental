using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AutoNoteControl {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AutoNoteControl autonotecontrol) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AutoNoteControl>");
			sb.Append("<AutoNoteControlNum>").Append(autonotecontrol.AutoNoteControlNum).Append("</AutoNoteControlNum>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(autonotecontrol.Descript)).Append("</Descript>");
			sb.Append("<ControlType>").Append(SerializeStringEscapes.EscapeForXml(autonotecontrol.ControlType)).Append("</ControlType>");
			sb.Append("<ControlLabel>").Append(SerializeStringEscapes.EscapeForXml(autonotecontrol.ControlLabel)).Append("</ControlLabel>");
			sb.Append("<ControlOptions>").Append(SerializeStringEscapes.EscapeForXml(autonotecontrol.ControlOptions)).Append("</ControlOptions>");
			sb.Append("</AutoNoteControl>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AutoNoteControl Deserialize(string xml) {
			OpenDentBusiness.AutoNoteControl autonotecontrol=new OpenDentBusiness.AutoNoteControl();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AutoNoteControlNum":
							autonotecontrol.AutoNoteControlNum=reader.ReadContentAsLong();
							break;
						case "Descript":
							autonotecontrol.Descript=reader.ReadContentAsString();
							break;
						case "ControlType":
							autonotecontrol.ControlType=reader.ReadContentAsString();
							break;
						case "ControlLabel":
							autonotecontrol.ControlLabel=reader.ReadContentAsString();
							break;
						case "ControlOptions":
							autonotecontrol.ControlOptions=reader.ReadContentAsString();
							break;
					}
				}
			}
			return autonotecontrol;
		}


	}
}