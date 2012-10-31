using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ProcButtonItem {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ProcButtonItem procbuttonitem) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ProcButtonItem>");
			sb.Append("<ProcButtonItemNum>").Append(procbuttonitem.ProcButtonItemNum).Append("</ProcButtonItemNum>");
			sb.Append("<ProcButtonNum>").Append(procbuttonitem.ProcButtonNum).Append("</ProcButtonNum>");
			sb.Append("<OldCode>").Append(SerializeStringEscapes.EscapeForXml(procbuttonitem.OldCode)).Append("</OldCode>");
			sb.Append("<AutoCodeNum>").Append(procbuttonitem.AutoCodeNum).Append("</AutoCodeNum>");
			sb.Append("<CodeNum>").Append(procbuttonitem.CodeNum).Append("</CodeNum>");
			sb.Append("</ProcButtonItem>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ProcButtonItem Deserialize(string xml) {
			OpenDentBusiness.ProcButtonItem procbuttonitem=new OpenDentBusiness.ProcButtonItem();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ProcButtonItemNum":
							procbuttonitem.ProcButtonItemNum=reader.ReadContentAsLong();
							break;
						case "ProcButtonNum":
							procbuttonitem.ProcButtonNum=reader.ReadContentAsLong();
							break;
						case "OldCode":
							procbuttonitem.OldCode=reader.ReadContentAsString();
							break;
						case "AutoCodeNum":
							procbuttonitem.AutoCodeNum=reader.ReadContentAsLong();
							break;
						case "CodeNum":
							procbuttonitem.CodeNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return procbuttonitem;
		}


	}
}