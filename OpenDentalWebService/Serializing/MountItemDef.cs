using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class MountItemDef {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.MountItemDef mountitemdef) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<MountItemDef>");
			sb.Append("<MountItemDefNum>").Append(mountitemdef.MountItemDefNum).Append("</MountItemDefNum>");
			sb.Append("<MountDefNum>").Append(mountitemdef.MountDefNum).Append("</MountDefNum>");
			sb.Append("<Xpos>").Append(mountitemdef.Xpos).Append("</Xpos>");
			sb.Append("<Ypos>").Append(mountitemdef.Ypos).Append("</Ypos>");
			sb.Append("<Width>").Append(mountitemdef.Width).Append("</Width>");
			sb.Append("<Height>").Append(mountitemdef.Height).Append("</Height>");
			sb.Append("</MountItemDef>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.MountItemDef Deserialize(string xml) {
			OpenDentBusiness.MountItemDef mountitemdef=new OpenDentBusiness.MountItemDef();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "MountItemDefNum":
							mountitemdef.MountItemDefNum=reader.ReadContentAsLong();
							break;
						case "MountDefNum":
							mountitemdef.MountDefNum=reader.ReadContentAsLong();
							break;
						case "Xpos":
							mountitemdef.Xpos=reader.ReadContentAsInt();
							break;
						case "Ypos":
							mountitemdef.Ypos=reader.ReadContentAsInt();
							break;
						case "Width":
							mountitemdef.Width=reader.ReadContentAsInt();
							break;
						case "Height":
							mountitemdef.Height=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return mountitemdef;
		}


	}
}