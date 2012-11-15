using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DictCustom {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DictCustom dictcustom) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DictCustom>");
			sb.Append("<DictCustomNum>").Append(dictcustom.DictCustomNum).Append("</DictCustomNum>");
			sb.Append("<WordText>").Append(SerializeStringEscapes.EscapeForXml(dictcustom.WordText)).Append("</WordText>");
			sb.Append("</DictCustom>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DictCustom Deserialize(string xml) {
			OpenDentBusiness.DictCustom dictcustom=new OpenDentBusiness.DictCustom();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DictCustomNum":
							dictcustom.DictCustomNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "WordText":
							dictcustom.WordText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return dictcustom;
		}


	}
}