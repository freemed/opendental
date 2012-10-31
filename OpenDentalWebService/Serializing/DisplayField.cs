using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class DisplayField {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.DisplayField displayfield) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<DisplayField>");
			sb.Append("<DisplayFieldNum>").Append(displayfield.DisplayFieldNum).Append("</DisplayFieldNum>");
			sb.Append("<InternalName>").Append(SerializeStringEscapes.EscapeForXml(displayfield.InternalName)).Append("</InternalName>");
			sb.Append("<ItemOrder>").Append(displayfield.ItemOrder).Append("</ItemOrder>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(displayfield.Description)).Append("</Description>");
			sb.Append("<ColumnWidth>").Append(displayfield.ColumnWidth).Append("</ColumnWidth>");
			sb.Append("<Category>").Append((int)displayfield.Category).Append("</Category>");
			sb.Append("<ChartViewNum>").Append(displayfield.ChartViewNum).Append("</ChartViewNum>");
			sb.Append("</DisplayField>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.DisplayField Deserialize(string xml) {
			OpenDentBusiness.DisplayField displayfield=new OpenDentBusiness.DisplayField();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "DisplayFieldNum":
							displayfield.DisplayFieldNum=reader.ReadContentAsLong();
							break;
						case "InternalName":
							displayfield.InternalName=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							displayfield.ItemOrder=reader.ReadContentAsInt();
							break;
						case "Description":
							displayfield.Description=reader.ReadContentAsString();
							break;
						case "ColumnWidth":
							displayfield.ColumnWidth=reader.ReadContentAsInt();
							break;
						case "Category":
							displayfield.Category=(OpenDentBusiness.DisplayFieldCategory)reader.ReadContentAsInt();
							break;
						case "ChartViewNum":
							displayfield.ChartViewNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return displayfield;
		}


	}
}