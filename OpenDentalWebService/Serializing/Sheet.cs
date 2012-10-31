using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Sheet {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Sheet sheet) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Sheet>");
			sb.Append("<SheetNum>").Append(sheet.SheetNum).Append("</SheetNum>");
			sb.Append("<SheetType>").Append((int)sheet.SheetType).Append("</SheetType>");
			sb.Append("<PatNum>").Append(sheet.PatNum).Append("</PatNum>");
			sb.Append("<DateTimeSheet>").Append(sheet.DateTimeSheet.ToLongDateString()).Append("</DateTimeSheet>");
			sb.Append("<FontSize>").Append(sheet.FontSize).Append("</FontSize>");
			sb.Append("<FontName>").Append(SerializeStringEscapes.EscapeForXml(sheet.FontName)).Append("</FontName>");
			sb.Append("<Width>").Append(sheet.Width).Append("</Width>");
			sb.Append("<Height>").Append(sheet.Height).Append("</Height>");
			sb.Append("<IsLandscape>").Append((sheet.IsLandscape)?1:0).Append("</IsLandscape>");
			sb.Append("<InternalNote>").Append(SerializeStringEscapes.EscapeForXml(sheet.InternalNote)).Append("</InternalNote>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(sheet.Description)).Append("</Description>");
			sb.Append("<ShowInTerminal>").Append(sheet.ShowInTerminal).Append("</ShowInTerminal>");
			sb.Append("<IsWebForm>").Append((sheet.IsWebForm)?1:0).Append("</IsWebForm>");
			sb.Append("</Sheet>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Sheet Deserialize(string xml) {
			OpenDentBusiness.Sheet sheet=new OpenDentBusiness.Sheet();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SheetNum":
							sheet.SheetNum=reader.ReadContentAsLong();
							break;
						case "SheetType":
							sheet.SheetType=(OpenDentBusiness.SheetTypeEnum)reader.ReadContentAsInt();
							break;
						case "PatNum":
							sheet.PatNum=reader.ReadContentAsLong();
							break;
						case "DateTimeSheet":
							sheet.DateTimeSheet=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "FontSize":
							sheet.FontSize=reader.ReadContentAsFloat();
							break;
						case "FontName":
							sheet.FontName=reader.ReadContentAsString();
							break;
						case "Width":
							sheet.Width=reader.ReadContentAsInt();
							break;
						case "Height":
							sheet.Height=reader.ReadContentAsInt();
							break;
						case "IsLandscape":
							sheet.IsLandscape=reader.ReadContentAsString()!="0";
							break;
						case "InternalNote":
							sheet.InternalNote=reader.ReadContentAsString();
							break;
						case "Description":
							sheet.Description=reader.ReadContentAsString();
							break;
						case "ShowInTerminal":
							sheet.ShowInTerminal=(byte)reader.ReadContentAsInt();
							break;
						case "IsWebForm":
							sheet.IsWebForm=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return sheet;
		}


	}
}