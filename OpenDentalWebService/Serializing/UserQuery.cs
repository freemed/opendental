using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class UserQuery {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.UserQuery userquery) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<UserQuery>");
			sb.Append("<QueryNum>").Append(userquery.QueryNum).Append("</QueryNum>");
			sb.Append("<Description>").Append(SerializeStringEscapes.EscapeForXml(userquery.Description)).Append("</Description>");
			sb.Append("<FileName>").Append(SerializeStringEscapes.EscapeForXml(userquery.FileName)).Append("</FileName>");
			sb.Append("<QueryText>").Append(SerializeStringEscapes.EscapeForXml(userquery.QueryText)).Append("</QueryText>");
			sb.Append("</UserQuery>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.UserQuery Deserialize(string xml) {
			OpenDentBusiness.UserQuery userquery=new OpenDentBusiness.UserQuery();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "QueryNum":
							userquery.QueryNum=reader.ReadContentAsLong();
							break;
						case "Description":
							userquery.Description=reader.ReadContentAsString();
							break;
						case "FileName":
							userquery.FileName=reader.ReadContentAsString();
							break;
						case "QueryText":
							userquery.QueryText=reader.ReadContentAsString();
							break;
					}
				}
			}
			return userquery;
		}


	}
}