using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Site {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Site site) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Site>");
			sb.Append("<SiteNum>").Append(site.SiteNum).Append("</SiteNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(site.Description)).Append("</Description>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(site.Note)).Append("</Note>");
			sb.Append("</Site>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Site Deserialize(string xml) {
			OpenDentBusiness.Site site=new OpenDentBusiness.Site();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SiteNum":
							site.SiteNum=reader.ReadContentAsLong();
							break;
						case "Description":
							site.Description=reader.ReadContentAsString();
							break;
						case "Note":
							site.Note=reader.ReadContentAsString();
							break;
					}
				}
			}
			return site;
		}


	}
}