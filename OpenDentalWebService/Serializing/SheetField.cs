using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SheetField {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SheetField sheetfield) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SheetField>");
			sb.Append("<SheetFieldNum>").Append(sheetfield.SheetFieldNum).Append("</SheetFieldNum>");
			sb.Append("<SheetNum>").Append(sheetfield.SheetNum).Append("</SheetNum>");
			sb.Append("<FieldType>").Append((int)sheetfield.FieldType).Append("</FieldType>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(sheetfield.FieldName)).Append("</FieldName>");
			sb.Append("<FieldValue>").Append(SerializeStringEscapes.EscapeForXml(sheetfield.FieldValue)).Append("</FieldValue>");
			sb.Append("<FontSize>").Append(sheetfield.FontSize).Append("</FontSize>");
			sb.Append("<FontName>").Append(SerializeStringEscapes.EscapeForXml(sheetfield.FontName)).Append("</FontName>");
			sb.Append("<FontIsBold>").Append((sheetfield.FontIsBold)?1:0).Append("</FontIsBold>");
			sb.Append("<XPos>").Append(sheetfield.XPos).Append("</XPos>");
			sb.Append("<YPos>").Append(sheetfield.YPos).Append("</YPos>");
			sb.Append("<Width>").Append(sheetfield.Width).Append("</Width>");
			sb.Append("<Height>").Append(sheetfield.Height).Append("</Height>");
			sb.Append("<GrowthBehavior>").Append((int)sheetfield.GrowthBehavior).Append("</GrowthBehavior>");
			sb.Append("<RadioButtonValue>").Append(SerializeStringEscapes.EscapeForXml(sheetfield.RadioButtonValue)).Append("</RadioButtonValue>");
			sb.Append("<RadioButtonGroup>").Append(SerializeStringEscapes.EscapeForXml(sheetfield.RadioButtonGroup)).Append("</RadioButtonGroup>");
			sb.Append("<IsRequired>").Append((sheetfield.IsRequired)?1:0).Append("</IsRequired>");
			sb.Append("<TabOrder>").Append(sheetfield.TabOrder).Append("</TabOrder>");
			sb.Append("<ReportableName>").Append(SerializeStringEscapes.EscapeForXml(sheetfield.ReportableName)).Append("</ReportableName>");
			sb.Append("</SheetField>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SheetField Deserialize(string xml) {
			OpenDentBusiness.SheetField sheetfield=new OpenDentBusiness.SheetField();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SheetFieldNum":
							sheetfield.SheetFieldNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SheetNum":
							sheetfield.SheetNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FieldType":
							sheetfield.FieldType=(OpenDentBusiness.SheetFieldType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "FieldName":
							sheetfield.FieldName=reader.ReadContentAsString();
							break;
						case "FieldValue":
							sheetfield.FieldValue=reader.ReadContentAsString();
							break;
						case "FontSize":
							sheetfield.FontSize=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "FontName":
							sheetfield.FontName=reader.ReadContentAsString();
							break;
						case "FontIsBold":
							sheetfield.FontIsBold=reader.ReadContentAsString()!="0";
							break;
						case "XPos":
							sheetfield.XPos=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "YPos":
							sheetfield.YPos=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Width":
							sheetfield.Width=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Height":
							sheetfield.Height=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "GrowthBehavior":
							sheetfield.GrowthBehavior=(OpenDentBusiness.GrowthBehaviorEnum)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "RadioButtonValue":
							sheetfield.RadioButtonValue=reader.ReadContentAsString();
							break;
						case "RadioButtonGroup":
							sheetfield.RadioButtonGroup=reader.ReadContentAsString();
							break;
						case "IsRequired":
							sheetfield.IsRequired=reader.ReadContentAsString()!="0";
							break;
						case "TabOrder":
							sheetfield.TabOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ReportableName":
							sheetfield.ReportableName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return sheetfield;
		}


	}
}