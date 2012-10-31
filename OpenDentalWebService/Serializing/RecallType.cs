using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class RecallType {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.RecallType recalltype) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<RecallType>");
			sb.Append("<RecallTypeNum>").Append(recalltype.RecallTypeNum).Append("</RecallTypeNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(recalltype.Description)).Append("</Description>");
			sb.Append("<DefaultInterval>").Append(recalltype.DefaultInterval).Append("</DefaultInterval>");
			sb.Append("<TimePattern>").Append(SerializeStringEscapes.EscapeForXml(recalltype.TimePattern)).Append("</TimePattern>");
			sb.Append("<Procedures>").Append(SerializeStringEscapes.EscapeForXml(recalltype.Procedures)).Append("</Procedures>");
			sb.Append("</RecallType>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.RecallType Deserialize(string xml) {
			OpenDentBusiness.RecallType recalltype=new OpenDentBusiness.RecallType();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "RecallTypeNum":
							recalltype.RecallTypeNum=reader.ReadContentAsLong();
							break;
						case "Description":
							recalltype.Description=reader.ReadContentAsString();
							break;
						case "DefaultInterval":
							recalltype.DefaultInterval=new OpenDentBusiness.Interval(reader.ReadContentAsInt());
							break;
						case "TimePattern":
							recalltype.TimePattern=reader.ReadContentAsString();
							break;
						case "Procedures":
							recalltype.Procedures=reader.ReadContentAsString();
							break;
					}
				}
			}
			return recalltype;
		}


	}
}