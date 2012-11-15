using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SigElementDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SigElementDef sigelementdef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SigElementDef>");
			sb.Append("<SigElementDefNum>").Append(sigelementdef.SigElementDefNum).Append("</SigElementDefNum>");
			sb.Append("<LightRow>").Append(sigelementdef.LightRow).Append("</LightRow>");
			sb.Append("<LightColor>").Append(sigelementdef.LightColor.ToArgb()).Append("</LightColor>");
			sb.Append("<SigElementType>").Append((int)sigelementdef.SigElementType).Append("</SigElementType>");
			sb.Append("<SigText>").Append(SerializeStringEscapes.EscapeForXml(sigelementdef.SigText)).Append("</SigText>");
			sb.Append("<Sound>").Append(SerializeStringEscapes.EscapeForXml(sigelementdef.Sound)).Append("</Sound>");
			sb.Append("<ItemOrder>").Append(sigelementdef.ItemOrder).Append("</ItemOrder>");
			sb.Append("</SigElementDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SigElementDef Deserialize(string xml) {
			OpenDentBusiness.SigElementDef sigelementdef=new OpenDentBusiness.SigElementDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SigElementDefNum":
							sigelementdef.SigElementDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LightRow":
							sigelementdef.LightRow=System.Convert.ToByte(reader.ReadContentAsString());
							break;
						case "LightColor":
							sigelementdef.LightColor=Color.FromArgb(System.Convert.ToInt32(reader.ReadContentAsString()));
							break;
						case "SigElementType":
							sigelementdef.SigElementType=(OpenDentBusiness.SignalElementType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "SigText":
							sigelementdef.SigText=reader.ReadContentAsString();
							break;
						case "Sound":
							sigelementdef.Sound=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							sigelementdef.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return sigelementdef;
		}


	}
}