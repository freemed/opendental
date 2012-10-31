using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AutoNote {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AutoNote autonote) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AutoNote>");
			sb.Append("<AutoNoteNum>").Append(autonote.AutoNoteNum).Append("</AutoNoteNum>");
			sb.Append("<AutoNoteName>").Append(SerializeStringEscapes.EscapeForXml(autonote.AutoNoteName)).Append("</AutoNoteName>");
			sb.Append("<MainText>").Append(SerializeStringEscapes.EscapeForXml(autonote.MainText)).Append("</MainText>");
			sb.Append("</AutoNote>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AutoNote Deserialize(string xml) {
			OpenDentBusiness.AutoNote autonote=new OpenDentBusiness.AutoNote();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AutoNoteNum":
							autonote.AutoNoteNum=reader.ReadContentAsLong();
							break;
						case "AutoNoteName":
							autonote.AutoNoteName=reader.ReadContentAsString();
							break;
						case "MainText":
							autonote.MainText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return autonote;
		}


	}
}