using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Language {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Language language) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Language>");
			sb.Append("<LanguageNum>").Append(language.LanguageNum).Append("</LanguageNum>");
			sb.Append("<EnglishComments>").Append(SerializeStringEscapes.EscapeForXml(language.EnglishComments)).Append("</EnglishComments>");
			sb.Append("<ClassType>").Append(SerializeStringEscapes.EscapeForXml(language.ClassType)).Append("</ClassType>");
			sb.Append("<English>").Append(SerializeStringEscapes.EscapeForXml(language.English)).Append("</English>");
			sb.Append("<IsObsolete>").Append((language.IsObsolete)?1:0).Append("</IsObsolete>");
			sb.Append("</Language>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Language Deserialize(string xml) {
			OpenDentBusiness.Language language=new OpenDentBusiness.Language();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LanguageNum":
							language.LanguageNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EnglishComments":
							language.EnglishComments=reader.ReadContentAsString();
							break;
						case "ClassType":
							language.ClassType=reader.ReadContentAsString();
							break;
						case "English":
							language.English=reader.ReadContentAsString();
							break;
						case "IsObsolete":
							language.IsObsolete=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return language;
		}


	}
}