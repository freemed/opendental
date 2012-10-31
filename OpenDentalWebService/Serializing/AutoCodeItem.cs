using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AutoCodeItem {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AutoCodeItem autocodeitem) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AutoCodeItem>");
			sb.Append("<AutoCodeItemNum>").Append(autocodeitem.AutoCodeItemNum).Append("</AutoCodeItemNum>");
			sb.Append("<AutoCodeNum>").Append(autocodeitem.AutoCodeNum).Append("</AutoCodeNum>");
			sb.Append("<OldCode>").Append(SerializeStringEscapes.EscapeForXml(autocodeitem.OldCode)).Append("</OldCode>");
			sb.Append("<CodeNum>").Append(autocodeitem.CodeNum).Append("</CodeNum>");
			sb.Append("</AutoCodeItem>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AutoCodeItem Deserialize(string xml) {
			OpenDentBusiness.AutoCodeItem autocodeitem=new OpenDentBusiness.AutoCodeItem();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AutoCodeItemNum":
							autocodeitem.AutoCodeItemNum=reader.ReadContentAsLong();
							break;
						case "AutoCodeNum":
							autocodeitem.AutoCodeNum=reader.ReadContentAsLong();
							break;
						case "OldCode":
							autocodeitem.OldCode=reader.ReadContentAsString();
							break;
						case "CodeNum":
							autocodeitem.CodeNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return autocodeitem;
		}


	}
}