using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClaimFormItem {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClaimFormItem claimformitem) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClaimFormItem>");
			sb.Append("<ClaimFormItemNum>").Append(claimformitem.ClaimFormItemNum).Append("</ClaimFormItemNum>");
			sb.Append("<ClaimFormNum>").Append(claimformitem.ClaimFormNum).Append("</ClaimFormNum>");
			sb.Append("<ImageFileName>").Append(SerializeStringEscapes.EscapeForXml(claimformitem.ImageFileName)).Append("</ImageFileName>");
			sb.Append("<FieldName>").Append(SerializeStringEscapes.EscapeForXml(claimformitem.FieldName)).Append("</FieldName>");
			sb.Append("<FormatString>").Append(SerializeStringEscapes.EscapeForXml(claimformitem.FormatString)).Append("</FormatString>");
			sb.Append("<XPos>").Append(claimformitem.XPos).Append("</XPos>");
			sb.Append("<YPos>").Append(claimformitem.YPos).Append("</YPos>");
			sb.Append("<Width>").Append(claimformitem.Width).Append("</Width>");
			sb.Append("<Height>").Append(claimformitem.Height).Append("</Height>");
			sb.Append("</ClaimFormItem>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClaimFormItem Deserialize(string xml) {
			OpenDentBusiness.ClaimFormItem claimformitem=new OpenDentBusiness.ClaimFormItem();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimFormItemNum":
							claimformitem.ClaimFormItemNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ClaimFormNum":
							claimformitem.ClaimFormNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ImageFileName":
							claimformitem.ImageFileName=reader.ReadContentAsString();
							break;
						case "FieldName":
							claimformitem.FieldName=reader.ReadContentAsString();
							break;
						case "FormatString":
							claimformitem.FormatString=reader.ReadContentAsString();
							break;
						case "XPos":
							claimformitem.XPos=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "YPos":
							claimformitem.YPos=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "Width":
							claimformitem.Width=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "Height":
							claimformitem.Height=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
					}
				}
			}
			return claimformitem;
		}


	}
}