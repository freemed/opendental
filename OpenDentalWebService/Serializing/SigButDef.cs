using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SigButDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SigButDef sigbutdef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SigButDef>");
			sb.Append("<SigButDefNum>").Append(sigbutdef.SigButDefNum).Append("</SigButDefNum>");
			sb.Append("<ButtonText>").Append(SerializeStringEscapes.EscapeForXml(sigbutdef.ButtonText)).Append("</ButtonText>");
			sb.Append("<ButtonIndex>").Append(sigbutdef.ButtonIndex).Append("</ButtonIndex>");
			sb.Append("<SynchIcon>").Append(sigbutdef.SynchIcon).Append("</SynchIcon>");
			sb.Append("<ComputerName>").Append(SerializeStringEscapes.EscapeForXml(sigbutdef.ComputerName)).Append("</ComputerName>");
			sb.Append("</SigButDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SigButDef Deserialize(string xml) {
			OpenDentBusiness.SigButDef sigbutdef=new OpenDentBusiness.SigButDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SigButDefNum":
							sigbutdef.SigButDefNum=reader.ReadContentAsLong();
							break;
						case "ButtonText":
							sigbutdef.ButtonText=reader.ReadContentAsString();
							break;
						case "ButtonIndex":
							sigbutdef.ButtonIndex=reader.ReadContentAsInt();
							break;
						case "SynchIcon":
							sigbutdef.SynchIcon=(byte)reader.ReadContentAsInt();
							break;
						case "ComputerName":
							sigbutdef.ComputerName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return sigbutdef;
		}


	}
}