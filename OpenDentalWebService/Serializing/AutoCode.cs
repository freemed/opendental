using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AutoCode {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AutoCode autocode) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AutoCode>");
			sb.Append("<AutoCodeNum>").Append(autocode.AutoCodeNum).Append("</AutoCodeNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(autocode.Description)).Append("</Description>");
			sb.Append("<IsHidden>").Append((autocode.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<LessIntrusive>").Append((autocode.LessIntrusive)?1:0).Append("</LessIntrusive>");
			sb.Append("</AutoCode>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AutoCode Deserialize(string xml) {
			OpenDentBusiness.AutoCode autocode=new OpenDentBusiness.AutoCode();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AutoCodeNum":
							autocode.AutoCodeNum=reader.ReadContentAsLong();
							break;
						case "Description":
							autocode.Description=reader.ReadContentAsString();
							break;
						case "IsHidden":
							autocode.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "LessIntrusive":
							autocode.LessIntrusive=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return autocode;
		}


	}
}