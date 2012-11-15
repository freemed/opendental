using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SheetFieldDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SheetFieldDef sheetfielddef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SheetFieldDef>");
			sb.Append("<SheetFieldDefNum>").Append(sheetfielddef.SheetFieldDefNum).Append("</SheetFieldDefNum>");
			sb.Append("<SheetDefNum>").Append(sheetfielddef.SheetDefNum).Append("</SheetDefNum>");
			sb.Append("<FieldType>").Append((int)sheetfielddef.FieldType).Append("</FieldType>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(sheetfielddef.FieldName)).Append("</FieldName>");
			sb.Append("<FieldValue>").Append(SerializeStringEscapes.EscapeForXml(sheetfielddef.FieldValue)).Append("</FieldValue>");
			sb.Append("<FontSize>").Append(sheetfielddef.FontSize).Append("</FontSize>");
			sb.Append("<FontName>").Append(SerializeStringEscapes.EscapeForXml(sheetfielddef.FontName)).Append("</FontName>");
			sb.Append("<FontIsBold>").Append((sheetfielddef.FontIsBold)?1:0).Append("</FontIsBold>");
			sb.Append("<XPos>").Append(sheetfielddef.XPos).Append("</XPos>");
			sb.Append("<YPos>").Append(sheetfielddef.YPos).Append("</YPos>");
			sb.Append("<Width>").Append(sheetfielddef.Width).Append("</Width>");
			sb.Append("<Height>").Append(sheetfielddef.Height).Append("</Height>");
			sb.Append("<GrowthBehavior>").Append((int)sheetfielddef.GrowthBehavior).Append("</GrowthBehavior>");
			sb.Append("<RadioButtonValue>").Append(SerializeStringEscapes.EscapeForXml(sheetfielddef.RadioButtonValue)).Append("</RadioButtonValue>");
			sb.Append("<RadioButtonGroup>").Append(SerializeStringEscapes.EscapeForXml(sheetfielddef.RadioButtonGroup)).Append("</RadioButtonGroup>");
			sb.Append("<IsRequired>").Append((sheetfielddef.IsRequired)?1:0).Append("</IsRequired>");
			sb.Append("<TabOrder>").Append(sheetfielddef.TabOrder).Append("</TabOrder>");
			sb.Append("<ReportableName>").Append(SerializeStringEscapes.EscapeForXml(sheetfielddef.ReportableName)).Append("</ReportableName>");
			sb.Append("</SheetFieldDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SheetFieldDef Deserialize(string xml) {
			OpenDentBusiness.SheetFieldDef sheetfielddef=new OpenDentBusiness.SheetFieldDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SheetFieldDefNum":
							sheetfielddef.SheetFieldDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "SheetDefNum":
							sheetfielddef.SheetDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "FieldType":
							sheetfielddef.FieldType=(OpenDentBusiness.SheetFieldType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "FieldName":
							sheetfielddef.FieldName=reader.ReadContentAsString();
							break;
						case "FieldValue":
							sheetfielddef.FieldValue=reader.ReadContentAsString();
							break;
						case "FontSize":
							sheetfielddef.FontSize=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "FontName":
							sheetfielddef.FontName=reader.ReadContentAsString();
							break;
						case "FontIsBold":
							sheetfielddef.FontIsBold=reader.ReadContentAsString()!="0";
							break;
						case "XPos":
							sheetfielddef.XPos=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "YPos":
							sheetfielddef.YPos=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Width":
							sheetfielddef.Width=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Height":
							sheetfielddef.Height=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "GrowthBehavior":
							sheetfielddef.GrowthBehavior=(OpenDentBusiness.GrowthBehaviorEnum)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "RadioButtonValue":
							sheetfielddef.RadioButtonValue=reader.ReadContentAsString();
							break;
						case "RadioButtonGroup":
							sheetfielddef.RadioButtonGroup=reader.ReadContentAsString();
							break;
						case "IsRequired":
							sheetfielddef.IsRequired=reader.ReadContentAsString()!="0";
							break;
						case "TabOrder":
							sheetfielddef.TabOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "ReportableName":
							sheetfielddef.ReportableName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return sheetfielddef;
		}


	}
}