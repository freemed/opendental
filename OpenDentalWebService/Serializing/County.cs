using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class County {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.County county) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<County>");
			sb.Append("<CountyNum>").Append(county.CountyNum).Append("</CountyNum>");
			sb.Append("<CountyName>").Append(SerializeStringEscapes.EscapeForXml(county.CountyName)).Append("</CountyName>");
			sb.Append("<CountyCode>").Append(SerializeStringEscapes.EscapeForXml(county.CountyCode)).Append("</CountyCode>");
			sb.Append("</County>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.County Deserialize(string xml) {
			OpenDentBusiness.County county=new OpenDentBusiness.County();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CountyNum":
							county.CountyNum=reader.ReadContentAsLong();
							break;
						case "CountyName":
							county.CountyName=reader.ReadContentAsString();
							break;
						case "CountyCode":
							county.CountyCode=reader.ReadContentAsString();
							break;
					}
				}
			}
			return county;
		}


	}
}