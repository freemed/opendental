using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Formulary {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Formulary formulary) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Formulary>");
			sb.Append("<FormularyNum>").Append(formulary.FormularyNum).Append("</FormularyNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(formulary.Description)).Append("</Description>");
			sb.Append("</Formulary>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Formulary Deserialize(string xml) {
			OpenDentBusiness.Formulary formulary=new OpenDentBusiness.Formulary();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "FormularyNum":
							formulary.FormularyNum=reader.ReadContentAsLong();
							break;
						case "Description":
							formulary.Description=reader.ReadContentAsString();
							break;
					}
				}
			}
			return formulary;
		}


	}
}