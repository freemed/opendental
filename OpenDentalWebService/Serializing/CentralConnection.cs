using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class CentralConnection {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.CentralConnection centralconnection) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<CentralConnection>");
			sb.Append("<CentralConnectionNum>").Append(centralconnection.CentralConnectionNum).Append("</CentralConnectionNum>");
			sb.Append("<ServerName>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.ServerName)).Append("</ServerName>");
			sb.Append("<DatabaseName>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.DatabaseName)).Append("</DatabaseName>");
			sb.Append("<MySqlUser>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.MySqlUser)).Append("</MySqlUser>");
			sb.Append("<MySqlPassword>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.MySqlPassword)).Append("</MySqlPassword>");
			sb.Append("<ServiceURI>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.ServiceURI)).Append("</ServiceURI>");
			sb.Append("<OdUser>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.OdUser)).Append("</OdUser>");
			sb.Append("<OdPassword>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.OdPassword)).Append("</OdPassword>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(centralconnection.Note)).Append("</Note>");
			sb.Append("<ItemOrder>").Append(centralconnection.ItemOrder).Append("</ItemOrder>");
			sb.Append("<WebServiceIsEcw>").Append((centralconnection.WebServiceIsEcw)?1:0).Append("</WebServiceIsEcw>");
			sb.Append("</CentralConnection>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.CentralConnection Deserialize(string xml) {
			OpenDentBusiness.CentralConnection centralconnection=new OpenDentBusiness.CentralConnection();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "CentralConnectionNum":
							centralconnection.CentralConnectionNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ServerName":
							centralconnection.ServerName=reader.ReadContentAsString();
							break;
						case "DatabaseName":
							centralconnection.DatabaseName=reader.ReadContentAsString();
							break;
						case "MySqlUser":
							centralconnection.MySqlUser=reader.ReadContentAsString();
							break;
						case "MySqlPassword":
							centralconnection.MySqlPassword=reader.ReadContentAsString();
							break;
						case "ServiceURI":
							centralconnection.ServiceURI=reader.ReadContentAsString();
							break;
						case "OdUser":
							centralconnection.OdUser=reader.ReadContentAsString();
							break;
						case "OdPassword":
							centralconnection.OdPassword=reader.ReadContentAsString();
							break;
						case "Note":
							centralconnection.Note=reader.ReadContentAsString();
							break;
						case "ItemOrder":
							centralconnection.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "WebServiceIsEcw":
							centralconnection.WebServiceIsEcw=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return centralconnection;
		}


	}
}