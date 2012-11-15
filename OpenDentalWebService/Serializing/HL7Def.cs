using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class HL7Def {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.HL7Def hl7def) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<HL7Def>");
			sb.Append("<HL7DefNum>").Append(hl7def.HL7DefNum).Append("</HL7DefNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(hl7def.Description)).Append("</Description>");
			sb.Append("<ModeTx>").Append((int)hl7def.ModeTx).Append("</ModeTx>");
			sb.Append("<IncomingFolder>").Append(SerializeStringEscapes.EscapeForXml(hl7def.IncomingFolder)).Append("</IncomingFolder>");
			sb.Append("<OutgoingFolder>").Append(SerializeStringEscapes.EscapeForXml(hl7def.OutgoingFolder)).Append("</OutgoingFolder>");
			sb.Append("<IncomingPort>").Append(SerializeStringEscapes.EscapeForXml(hl7def.IncomingPort)).Append("</IncomingPort>");
			sb.Append("<OutgoingIpPort>").Append(SerializeStringEscapes.EscapeForXml(hl7def.OutgoingIpPort)).Append("</OutgoingIpPort>");
			sb.Append("<FieldSeparator>").Append(SerializeStringEscapes.EscapeForXml(hl7def.FieldSeparator)).Append("</FieldSeparator>");
			sb.Append("<ComponentSeparator>").Append(SerializeStringEscapes.EscapeForXml(hl7def.ComponentSeparator)).Append("</ComponentSeparator>");
			sb.Append("<SubcomponentSeparator>").Append(SerializeStringEscapes.EscapeForXml(hl7def.SubcomponentSeparator)).Append("</SubcomponentSeparator>");
			sb.Append("<RepetitionSeparator>").Append(SerializeStringEscapes.EscapeForXml(hl7def.RepetitionSeparator)).Append("</RepetitionSeparator>");
			sb.Append("<EscapeCharacter>").Append(SerializeStringEscapes.EscapeForXml(hl7def.EscapeCharacter)).Append("</EscapeCharacter>");
			sb.Append("<IsInternal>").Append((hl7def.IsInternal)?1:0).Append("</IsInternal>");
			sb.Append("<InternalType>").Append(SerializeStringEscapes.EscapeForXml(hl7def.InternalType)).Append("</InternalType>");
			sb.Append("<InternalTypeVersion>").Append(SerializeStringEscapes.EscapeForXml(hl7def.InternalTypeVersion)).Append("</InternalTypeVersion>");
			sb.Append("<IsEnabled>").Append((hl7def.IsEnabled)?1:0).Append("</IsEnabled>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(hl7def.Note)).Append("</Note>");
			sb.Append("<HL7Server>").Append(SerializeStringEscapes.EscapeForXml(hl7def.HL7Server)).Append("</HL7Server>");
			sb.Append("<HL7ServiceName>").Append(SerializeStringEscapes.EscapeForXml(hl7def.HL7ServiceName)).Append("</HL7ServiceName>");
			sb.Append("<ShowDemographics>").Append((int)hl7def.ShowDemographics).Append("</ShowDemographics>");
			sb.Append("<ShowAppts>").Append((hl7def.ShowAppts)?1:0).Append("</ShowAppts>");
			sb.Append("<ShowAccount>").Append((hl7def.ShowAccount)?1:0).Append("</ShowAccount>");
			sb.Append("</HL7Def>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.HL7Def Deserialize(string xml) {
			OpenDentBusiness.HL7Def hl7def=new OpenDentBusiness.HL7Def();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "HL7DefNum":
							hl7def.HL7DefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							hl7def.Description=reader.ReadContentAsString();
							break;
						case "ModeTx":
							hl7def.ModeTx=(OpenDentBusiness.ModeTxHL7)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IncomingFolder":
							hl7def.IncomingFolder=reader.ReadContentAsString();
							break;
						case "OutgoingFolder":
							hl7def.OutgoingFolder=reader.ReadContentAsString();
							break;
						case "IncomingPort":
							hl7def.IncomingPort=reader.ReadContentAsString();
							break;
						case "OutgoingIpPort":
							hl7def.OutgoingIpPort=reader.ReadContentAsString();
							break;
						case "FieldSeparator":
							hl7def.FieldSeparator=reader.ReadContentAsString();
							break;
						case "ComponentSeparator":
							hl7def.ComponentSeparator=reader.ReadContentAsString();
							break;
						case "SubcomponentSeparator":
							hl7def.SubcomponentSeparator=reader.ReadContentAsString();
							break;
						case "RepetitionSeparator":
							hl7def.RepetitionSeparator=reader.ReadContentAsString();
							break;
						case "EscapeCharacter":
							hl7def.EscapeCharacter=reader.ReadContentAsString();
							break;
						case "IsInternal":
							hl7def.IsInternal=reader.ReadContentAsString()!="0";
							break;
						case "InternalType":
							hl7def.InternalType=reader.ReadContentAsString();
							break;
						case "InternalTypeVersion":
							hl7def.InternalTypeVersion=reader.ReadContentAsString();
							break;
						case "IsEnabled":
							hl7def.IsEnabled=reader.ReadContentAsString()!="0";
							break;
						case "Note":
							hl7def.Note=reader.ReadContentAsString();
							break;
						case "HL7Server":
							hl7def.HL7Server=reader.ReadContentAsString();
							break;
						case "HL7ServiceName":
							hl7def.HL7ServiceName=reader.ReadContentAsString();
							break;
						case "ShowDemographics":
							hl7def.ShowDemographics=(OpenDentBusiness.HL7ShowDemographics)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ShowAppts":
							hl7def.ShowAppts=reader.ReadContentAsString()!="0";
							break;
						case "ShowAccount":
							hl7def.ShowAccount=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return hl7def;
		}


	}
}