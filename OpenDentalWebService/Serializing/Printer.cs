using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Printer {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Printer printer) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Printer>");
			sb.Append("<PrinterNum>").Append(printer.PrinterNum).Append("</PrinterNum>");
			sb.Append("<ComputerNum>").Append(printer.ComputerNum).Append("</ComputerNum>");
			sb.Append("<PrintSit>").Append((int)printer.PrintSit).Append("</PrintSit>");
			sb.Append("<PrinterName>").Append(SerializeStringEscapes.EscapeForXml(printer.PrinterName)).Append("</PrinterName>");
			sb.Append("<DisplayPrompt>").Append((printer.DisplayPrompt)?1:0).Append("</DisplayPrompt>");
			sb.Append("</Printer>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Printer Deserialize(string xml) {
			OpenDentBusiness.Printer printer=new OpenDentBusiness.Printer();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PrinterNum":
							printer.PrinterNum=reader.ReadContentAsLong();
							break;
						case "ComputerNum":
							printer.ComputerNum=reader.ReadContentAsLong();
							break;
						case "PrintSit":
							printer.PrintSit=(OpenDentBusiness.PrintSituation)reader.ReadContentAsInt();
							break;
						case "PrinterName":
							printer.PrinterName=reader.ReadContentAsString();
							break;
						case "DisplayPrompt":
							printer.DisplayPrompt=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return printer;
		}


	}
}