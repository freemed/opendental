using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Letter {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Letter letter) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Letter>");
			sb.Append("<LetterNum>").Append(letter.LetterNum).Append("</LetterNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(letter.Description)).Append("</Description>");
			sb.Append("<BodyText>").Append(SerializeStringEscapes.EscapeForXml(letter.BodyText)).Append("</BodyText>");
			sb.Append("</Letter>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Letter Deserialize(string xml) {
			OpenDentBusiness.Letter letter=new OpenDentBusiness.Letter();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "LetterNum":
							letter.LetterNum=reader.ReadContentAsLong();
							break;
						case "Description":
							letter.Description=reader.ReadContentAsString();
							break;
						case "BodyText":
							letter.BodyText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return letter;
		}


	}
}