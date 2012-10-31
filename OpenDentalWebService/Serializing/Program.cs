using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Program {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Program program) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Program>");
			sb.Append("<ProgramNum>").Append(program.ProgramNum).Append("</ProgramNum>");
			sb.Append("<ProgName>").Append(SerializeStringEscapes.EscapeForXml(program.ProgName)).Append("</ProgName>");
			sb.Append("<ProgDesc>").Append(SerializeStringEscapes.EscapeForXml(program.ProgDesc)).Append("</ProgDesc>");
			sb.Append("<Enabled>").Append((program.Enabled)?1:0).Append("</Enabled>");
			sb.Append("<Path>").Append(SerializeStringEscapes.EscapeForXml(program.Path)).Append("</Path>");
			sb.Append("<CommandLine>").Append(SerializeStringEscapes.EscapeForXml(program.CommandLine)).Append("</CommandLine>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(program.Note)).Append("</Note>");
			sb.Append("<PluginDllName>").Append(SerializeStringEscapes.EscapeForXml(program.PluginDllName)).Append("</PluginDllName>");
			sb.Append("</Program>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Program Deserialize(string xml) {
			OpenDentBusiness.Program program=new OpenDentBusiness.Program();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProgramNum":
							program.ProgramNum=reader.ReadContentAsLong();
							break;
						case "ProgName":
							program.ProgName=reader.ReadContentAsString();
							break;
						case "ProgDesc":
							program.ProgDesc=reader.ReadContentAsString();
							break;
						case "Enabled":
							program.Enabled=reader.ReadContentAsString()!="0";
							break;
						case "Path":
							program.Path=reader.ReadContentAsString();
							break;
						case "CommandLine":
							program.CommandLine=reader.ReadContentAsString();
							break;
						case "Note":
							program.Note=reader.ReadContentAsString();
							break;
						case "PluginDllName":
							program.PluginDllName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return program;
		}


	}
}