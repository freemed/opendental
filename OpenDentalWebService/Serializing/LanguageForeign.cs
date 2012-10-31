using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class LanguageForeign {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.LanguageForeign languageforeign) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<LanguageForeign>");
			sb.Append("<LanguageForeignNum>").Append(languageforeign.LanguageForeignNum).Append("</LanguageForeignNum>");
			sb.Append("<ClassType>").Append(SerializeStringEscapes.EscapeForXml(languageforeign.ClassType)).Append("</ClassType>");
			sb.Append("<English>").Append(SerializeStringEscapes.EscapeForXml(languageforeign.English)).Append("</English>");
			sb.Append("<Culture>").Append(SerializeStringEscapes.EscapeForXml(languageforeign.Culture)).Append("</Culture>");
			sb.Append("<Translation>").Append(SerializeStringEscapes.EscapeForXml(languageforeign.Translation)).Append("</Translation>");
			sb.Append("<Comments>").Append(SerializeStringEscapes.EscapeForXml(languageforeign.Comments)).Append("</Comments>");
			sb.Append("</LanguageForeign>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.LanguageForeign Deserialize(string xml) {
			OpenDentBusiness.LanguageForeign languageforeign=new OpenDentBusiness.LanguageForeign();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LanguageForeignNum":
							languageforeign.LanguageForeignNum=reader.ReadContentAsLong();
							break;
						case "ClassType":
							languageforeign.ClassType=reader.ReadContentAsString();
							break;
						case "English":
							languageforeign.English=reader.ReadContentAsString();
							break;
						case "Culture":
							languageforeign.Culture=reader.ReadContentAsString();
							break;
						case "Translation":
							languageforeign.Translation=reader.ReadContentAsString();
							break;
						case "Comments":
							languageforeign.Comments=reader.ReadContentAsString();
							break;
					}
				}
			}
			return languageforeign;
		}


	}
}