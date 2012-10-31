using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClaimValCodeLog {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClaimValCodeLog claimvalcodelog) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClaimValCodeLog>");
			sb.Append("<ClaimValCodeLogNum>").Append(claimvalcodelog.ClaimValCodeLogNum).Append("</ClaimValCodeLogNum>");
			sb.Append("<ClaimNum>").Append(claimvalcodelog.ClaimNum).Append("</ClaimNum>");
			sb.Append("<ClaimField>").Append(SerializeStringEscapes.EscapeForXml(claimvalcodelog.ClaimField)).Append("</ClaimField>");
			sb.Append("<ValCode>").Append(SerializeStringEscapes.EscapeForXml(claimvalcodelog.ValCode)).Append("</ValCode>");
			sb.Append("<ValAmount>").Append(claimvalcodelog.ValAmount).Append("</ValAmount>");
			sb.Append("<Ordinal>").Append(claimvalcodelog.Ordinal).Append("</Ordinal>");
			sb.Append("</ClaimValCodeLog>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClaimValCodeLog Deserialize(string xml) {
			OpenDentBusiness.ClaimValCodeLog claimvalcodelog=new OpenDentBusiness.ClaimValCodeLog();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimValCodeLogNum":
							claimvalcodelog.ClaimValCodeLogNum=reader.ReadContentAsLong();
							break;
						case "ClaimNum":
							claimvalcodelog.ClaimNum=reader.ReadContentAsLong();
							break;
						case "ClaimField":
							claimvalcodelog.ClaimField=reader.ReadContentAsString();
							break;
						case "ValCode":
							claimvalcodelog.ValCode=reader.ReadContentAsString();
							break;
						case "ValAmount":
							claimvalcodelog.ValAmount=reader.ReadContentAsDouble();
							break;
						case "Ordinal":
							claimvalcodelog.Ordinal=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return claimvalcodelog;
		}


	}
}