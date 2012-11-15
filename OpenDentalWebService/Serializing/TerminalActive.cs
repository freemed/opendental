using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TerminalActive {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TerminalActive terminalactive) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TerminalActive>");
			sb.Append("<TerminalActiveNum>").Append(terminalactive.TerminalActiveNum).Append("</TerminalActiveNum>");
			sb.Append("<ComputerName>").Append(SerializeStringEscapes.EscapeForXml(terminalactive.ComputerName)).Append("</ComputerName>");
			sb.Append("<TerminalStatus>").Append((int)terminalactive.TerminalStatus).Append("</TerminalStatus>");
			sb.Append("<PatNum>").Append(terminalactive.PatNum).Append("</PatNum>");
			sb.Append("</TerminalActive>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TerminalActive Deserialize(string xml) {
			OpenDentBusiness.TerminalActive terminalactive=new OpenDentBusiness.TerminalActive();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TerminalActiveNum":
							terminalactive.TerminalActiveNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ComputerName":
							terminalactive.ComputerName=reader.ReadContentAsString();
							break;
						case "TerminalStatus":
							terminalactive.TerminalStatus=(OpenDentBusiness.TerminalStatusEnum)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "PatNum":
							terminalactive.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return terminalactive;
		}


	}
}