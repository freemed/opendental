using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class LetterMerge {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.LetterMerge lettermerge) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<LetterMerge>");
			sb.Append("<LetterMergeNum>").Append(lettermerge.LetterMergeNum).Append("</LetterMergeNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(lettermerge.Description)).Append("</Description>");
			sb.Append("<TemplateName>").Append(SerializeStringEscapes.EscapeForXml(lettermerge.TemplateName)).Append("</TemplateName>");
			sb.Append("<DataFileName>").Append(SerializeStringEscapes.EscapeForXml(lettermerge.DataFileName)).Append("</DataFileName>");
			sb.Append("<Category>").Append(lettermerge.Category).Append("</Category>");
			sb.Append("</LetterMerge>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.LetterMerge Deserialize(string xml) {
			OpenDentBusiness.LetterMerge lettermerge=new OpenDentBusiness.LetterMerge();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LetterMergeNum":
							lettermerge.LetterMergeNum=reader.ReadContentAsLong();
							break;
						case "Description":
							lettermerge.Description=reader.ReadContentAsString();
							break;
						case "TemplateName":
							lettermerge.TemplateName=reader.ReadContentAsString();
							break;
						case "DataFileName":
							lettermerge.DataFileName=reader.ReadContentAsString();
							break;
						case "Category":
							lettermerge.Category=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return lettermerge;
		}


	}
}