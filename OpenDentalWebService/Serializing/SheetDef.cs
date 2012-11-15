using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SheetDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SheetDef sheetdef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SheetDef>");
			sb.Append("<SheetDefNum>").Append(sheetdef.SheetDefNum).Append("</SheetDefNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(sheetdef.Description)).Append("</Description>");
			sb.Append("<SheetType>").Append((int)sheetdef.SheetType).Append("</SheetType>");
			sb.Append("<FontSize>").Append(sheetdef.FontSize).Append("</FontSize>");
			sb.Append("<FontName>").Append(SerializeStringEscapes.EscapeForXml(sheetdef.FontName)).Append("</FontName>");
			sb.Append("<Width>").Append(sheetdef.Width).Append("</Width>");
			sb.Append("<Height>").Append(sheetdef.Height).Append("</Height>");
			sb.Append("<IsLandscape>").Append((sheetdef.IsLandscape)?1:0).Append("</IsLandscape>");
			sb.Append("</SheetDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SheetDef Deserialize(string xml) {
			OpenDentBusiness.SheetDef sheetdef=new OpenDentBusiness.SheetDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SheetDefNum":
							sheetdef.SheetDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							sheetdef.Description=reader.ReadContentAsString();
							break;
						case "SheetType":
							sheetdef.SheetType=(OpenDentBusiness.SheetTypeEnum)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "FontSize":
							sheetdef.FontSize=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "FontName":
							sheetdef.FontName=reader.ReadContentAsString();
							break;
						case "Width":
							sheetdef.Width=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Height":
							sheetdef.Height=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IsLandscape":
							sheetdef.IsLandscape=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return sheetdef;
		}


	}
}