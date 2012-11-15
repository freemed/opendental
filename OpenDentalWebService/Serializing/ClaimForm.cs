using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClaimForm {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClaimForm claimform) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClaimForm>");
			sb.Append("<ClaimFormNum>").Append(claimform.ClaimFormNum).Append("</ClaimFormNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(claimform.Description)).Append("</Description>");
			sb.Append("<IsHidden>").Append((claimform.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<FontName>").Append(SerializeStringEscapes.EscapeForXml(claimform.FontName)).Append("</FontName>");
			sb.Append("<FontSize>").Append(claimform.FontSize).Append("</FontSize>");
			sb.Append("<UniqueID>").Append(SerializeStringEscapes.EscapeForXml(claimform.UniqueID)).Append("</UniqueID>");
			sb.Append("<PrintImages>").Append((claimform.PrintImages)?1:0).Append("</PrintImages>");
			sb.Append("<OffsetX>").Append(claimform.OffsetX).Append("</OffsetX>");
			sb.Append("<OffsetY>").Append(claimform.OffsetY).Append("</OffsetY>");
			sb.Append("</ClaimForm>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClaimForm Deserialize(string xml) {
			OpenDentBusiness.ClaimForm claimform=new OpenDentBusiness.ClaimForm();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimFormNum":
							claimform.ClaimFormNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							claimform.Description=reader.ReadContentAsString();
							break;
						case "IsHidden":
							claimform.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "FontName":
							claimform.FontName=reader.ReadContentAsString();
							break;
						case "FontSize":
							claimform.FontSize=System.Convert.ToSingle(reader.ReadContentAsString());
							break;
						case "UniqueID":
							claimform.UniqueID=reader.ReadContentAsString();
							break;
						case "PrintImages":
							claimform.PrintImages=reader.ReadContentAsString()!="0";
							break;
						case "OffsetX":
							claimform.OffsetX=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "OffsetY":
							claimform.OffsetY=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return claimform;
		}


	}
}