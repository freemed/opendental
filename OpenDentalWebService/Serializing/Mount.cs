using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Mount {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Mount mount) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Mount>");
			sb.Append("<MountNum>").Append(mount.MountNum).Append("</MountNum>");
			sb.Append("<PatNum>").Append(mount.PatNum).Append("</PatNum>");
			sb.Append("<DocCategory>").Append(mount.DocCategory).Append("</DocCategory>");
			sb.Append("<DateCreated>").Append(mount.DateCreated.ToString("yyyyMMddHHmmss")).Append("</DateCreated>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(mount.Description)).Append("</Description>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(mount.Note)).Append("</Note>");
			sb.Append("<ImgType>").Append((int)mount.ImgType).Append("</ImgType>");
			sb.Append("<Width>").Append(mount.Width).Append("</Width>");
			sb.Append("<Height>").Append(mount.Height).Append("</Height>");
			sb.Append("</Mount>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Mount Deserialize(string xml) {
			OpenDentBusiness.Mount mount=new OpenDentBusiness.Mount();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "MountNum":
							mount.MountNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							mount.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DocCategory":
							mount.DocCategory=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "DateCreated":
							mount.DateCreated=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "Description":
							mount.Description=reader.ReadContentAsString();
							break;
						case "Note":
							mount.Note=reader.ReadContentAsString();
							break;
						case "ImgType":
							mount.ImgType=(OpenDentBusiness.ImageType)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Width":
							mount.Width=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Height":
							mount.Height=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return mount;
		}


	}
}