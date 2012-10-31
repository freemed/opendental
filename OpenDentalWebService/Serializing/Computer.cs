using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Computer {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Computer computer) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Computer>");
			sb.Append("<ComputerNum>").Append(computer.ComputerNum).Append("</ComputerNum>");
			sb.Append("<CompName>").Append(SerializeStringEscapes.EscapeForXml(computer.CompName)).Append("</CompName>");
			sb.Append("<LastHeartBeat>").Append(computer.LastHeartBeat.ToLongDateString()).Append("</LastHeartBeat>");
			sb.Append("</Computer>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Computer Deserialize(string xml) {
			OpenDentBusiness.Computer computer=new OpenDentBusiness.Computer();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ComputerNum":
							computer.ComputerNum=reader.ReadContentAsLong();
							break;
						case "CompName":
							computer.CompName=reader.ReadContentAsString();
							break;
						case "LastHeartBeat":
							computer.LastHeartBeat=DateTime.Parse(reader.ReadContentAsString());
							break;
					}
				}
			}
			return computer;
		}


	}
}