using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class MountItem {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.MountItem mountitem) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<MountItem>");
			sb.Append("<MountItemNum>").Append(mountitem.MountItemNum).Append("</MountItemNum>");
			sb.Append("<MountNum>").Append(mountitem.MountNum).Append("</MountNum>");
			sb.Append("<Xpos>").Append(mountitem.Xpos).Append("</Xpos>");
			sb.Append("<Ypos>").Append(mountitem.Ypos).Append("</Ypos>");
			sb.Append("<OrdinalPos>").Append(mountitem.OrdinalPos).Append("</OrdinalPos>");
			sb.Append("<Width>").Append(mountitem.Width).Append("</Width>");
			sb.Append("<Height>").Append(mountitem.Height).Append("</Height>");
			sb.Append("</MountItem>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.MountItem Deserialize(string xml) {
			OpenDentBusiness.MountItem mountitem=new OpenDentBusiness.MountItem();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "MountItemNum":
							mountitem.MountItemNum=reader.ReadContentAsLong();
							break;
						case "MountNum":
							mountitem.MountNum=reader.ReadContentAsLong();
							break;
						case "Xpos":
							mountitem.Xpos=reader.ReadContentAsInt();
							break;
						case "Ypos":
							mountitem.Ypos=reader.ReadContentAsInt();
							break;
						case "OrdinalPos":
							mountitem.OrdinalPos=reader.ReadContentAsInt();
							break;
						case "Width":
							mountitem.Width=reader.ReadContentAsInt();
							break;
						case "Height":
							mountitem.Height=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return mountitem;
		}


	}
}