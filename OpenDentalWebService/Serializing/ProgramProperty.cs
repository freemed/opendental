using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProgramProperty {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProgramProperty programproperty) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProgramProperty>");
			sb.Append("<ProgramPropertyNum>").Append(programproperty.ProgramPropertyNum).Append("</ProgramPropertyNum>");
			sb.Append("<ProgramNum>").Append(programproperty.ProgramNum).Append("</ProgramNum>");
			sb.Append("<PropertyDesc>").Append(SerializeStringEscapes.EscapeForXml(programproperty.PropertyDesc)).Append("</PropertyDesc>");
			sb.Append("<PropertyValue>").Append(SerializeStringEscapes.EscapeForXml(programproperty.PropertyValue)).Append("</PropertyValue>");
			sb.Append("<ComputerName>").Append(SerializeStringEscapes.EscapeForXml(programproperty.ComputerName)).Append("</ComputerName>");
			sb.Append("</ProgramProperty>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProgramProperty Deserialize(string xml) {
			OpenDentBusiness.ProgramProperty programproperty=new OpenDentBusiness.ProgramProperty();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProgramPropertyNum":
							programproperty.ProgramPropertyNum=reader.ReadContentAsLong();
							break;
						case "ProgramNum":
							programproperty.ProgramNum=reader.ReadContentAsLong();
							break;
						case "PropertyDesc":
							programproperty.PropertyDesc=reader.ReadContentAsString();
							break;
						case "PropertyValue":
							programproperty.PropertyValue=reader.ReadContentAsString();
							break;
						case "ComputerName":
							programproperty.ComputerName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return programproperty;
		}


	}
}