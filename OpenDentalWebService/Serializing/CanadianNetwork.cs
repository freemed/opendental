using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class CanadianNetwork {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.CanadianNetwork canadiannetwork) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<CanadianNetwork>");
			sb.Append("<CanadianNetworkNum>").Append(canadiannetwork.CanadianNetworkNum).Append("</CanadianNetworkNum>");
			sb.Append("<Abbrev>").Append(SerializeStringEscapes.EscapeForXml(canadiannetwork.Abbrev)).Append("</Abbrev>");
			sb.Append("<Descript>").Append(SerializeStringEscapes.EscapeForXml(canadiannetwork.Descript)).Append("</Descript>");
			sb.Append("<CanadianTransactionPrefix>").Append(SerializeStringEscapes.EscapeForXml(canadiannetwork.CanadianTransactionPrefix)).Append("</CanadianTransactionPrefix>");
			sb.Append("<CanadianIsRprHandler>").Append((canadiannetwork.CanadianIsRprHandler)?1:0).Append("</CanadianIsRprHandler>");
			sb.Append("</CanadianNetwork>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.CanadianNetwork Deserialize(string xml) {
			OpenDentBusiness.CanadianNetwork canadiannetwork=new OpenDentBusiness.CanadianNetwork();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CanadianNetworkNum":
							canadiannetwork.CanadianNetworkNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "Abbrev":
							canadiannetwork.Abbrev=reader.ReadContentAsString();
							break;
						case "Descript":
							canadiannetwork.Descript=reader.ReadContentAsString();
							break;
						case "CanadianTransactionPrefix":
							canadiannetwork.CanadianTransactionPrefix=reader.ReadContentAsString();
							break;
						case "CanadianIsRprHandler":
							canadiannetwork.CanadianIsRprHandler=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return canadiannetwork;
		}


	}
}