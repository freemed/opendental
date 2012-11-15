using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class MountDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.MountDef mountdef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<MountDef>");
			sb.Append("<MountDefNum>").Append(mountdef.MountDefNum).Append("</MountDefNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(mountdef.Description)).Append("</Description>");
			sb.Append("<ItemOrder>").Append(mountdef.ItemOrder).Append("</ItemOrder>");
			sb.Append("<IsRadiograph>").Append((mountdef.IsRadiograph)?1:0).Append("</IsRadiograph>");
			sb.Append("<Width>").Append(mountdef.Width).Append("</Width>");
			sb.Append("<Height>").Append(mountdef.Height).Append("</Height>");
			sb.Append("</MountDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.MountDef Deserialize(string xml) {
			OpenDentBusiness.MountDef mountdef=new OpenDentBusiness.MountDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "MountDefNum":
							mountdef.MountDefNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Description":
							mountdef.Description=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							mountdef.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "IsRadiograph":
							mountdef.IsRadiograph=reader.ReadContentAsString()!="0";
							break;
						case "Width":
							mountdef.Width=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Height":
							mountdef.Height=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return mountdef;
		}


	}
}