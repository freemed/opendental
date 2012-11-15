using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SupplyNeeded {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SupplyNeeded supplyneeded) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SupplyNeeded>");
			sb.Append("<SupplyNeededNum>").Append(supplyneeded.SupplyNeededNum).Append("</SupplyNeededNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(supplyneeded.Description)).Append("</Description>");
			sb.Append("<DateAdded>").Append(supplyneeded.DateAdded.ToString("yyyyMMddHHmmss")).Append("</DateAdded>");
			sb.Append("</SupplyNeeded>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SupplyNeeded Deserialize(string xml) {
			OpenDentBusiness.SupplyNeeded supplyneeded=new OpenDentBusiness.SupplyNeeded();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SupplyNeededNum":
							supplyneeded.SupplyNeededNum=reader.ReadContentAsLong();
							break;
						case "Description":
							supplyneeded.Description=reader.ReadContentAsString();
							break;
						case "DateAdded":
							supplyneeded.DateAdded=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return supplyneeded;
		}


	}
}