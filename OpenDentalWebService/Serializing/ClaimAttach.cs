using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClaimAttach {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClaimAttach claimattach) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClaimAttach>");
			sb.Append("<ClaimAttachNum>").Append(claimattach.ClaimAttachNum).Append("</ClaimAttachNum>");
			sb.Append("<ClaimNum>").Append(claimattach.ClaimNum).Append("</ClaimNum>");
			sb.Append("<DisplayedFileName>").Append(SerializeStringEscapes.EscapeForXml(claimattach.DisplayedFileName)).Append("</DisplayedFileName>");
			sb.Append("<ActualFileName>").Append(SerializeStringEscapes.EscapeForXml(claimattach.ActualFileName)).Append("</ActualFileName>");
			sb.Append("</ClaimAttach>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClaimAttach Deserialize(string xml) {
			OpenDentBusiness.ClaimAttach claimattach=new OpenDentBusiness.ClaimAttach();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimAttachNum":
							claimattach.ClaimAttachNum=reader.ReadContentAsLong();
							break;
						case "ClaimNum":
							claimattach.ClaimNum=reader.ReadContentAsLong();
							break;
						case "DisplayedFileName":
							claimattach.DisplayedFileName=reader.ReadContentAsString();
							break;
						case "ActualFileName":
							claimattach.ActualFileName=reader.ReadContentAsString();
							break;
					}
				}
			}
			return claimattach;
		}


	}
}