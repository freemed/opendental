using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ClaimCondCodeLog {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ClaimCondCodeLog claimcondcodelog) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ClaimCondCodeLog>");
			sb.Append("<ClaimCondCodeLogNum>").Append(claimcondcodelog.ClaimCondCodeLogNum).Append("</ClaimCondCodeLogNum>");
			sb.Append("<ClaimNum>").Append(claimcondcodelog.ClaimNum).Append("</ClaimNum>");
			sb.Append("<Code0>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code0)).Append("</Code0>");
			sb.Append("<Code1>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code1)).Append("</Code1>");
			sb.Append("<Code2>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code2)).Append("</Code2>");
			sb.Append("<Code3>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code3)).Append("</Code3>");
			sb.Append("<Code4>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code4)).Append("</Code4>");
			sb.Append("<Code5>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code5)).Append("</Code5>");
			sb.Append("<Code6>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code6)).Append("</Code6>");
			sb.Append("<Code7>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code7)).Append("</Code7>");
			sb.Append("<Code8>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code8)).Append("</Code8>");
			sb.Append("<Code9>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code9)).Append("</Code9>");
			sb.Append("<Code10>").Append(SerializeStringEscapes.EscapeForXml(claimcondcodelog.Code10)).Append("</Code10>");
			sb.Append("</ClaimCondCodeLog>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ClaimCondCodeLog Deserialize(string xml) {
			OpenDentBusiness.ClaimCondCodeLog claimcondcodelog=new OpenDentBusiness.ClaimCondCodeLog();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ClaimCondCodeLogNum":
							claimcondcodelog.ClaimCondCodeLogNum=reader.ReadContentAsLong();
							break;
						case "ClaimNum":
							claimcondcodelog.ClaimNum=reader.ReadContentAsLong();
							break;
						case "Code0":
							claimcondcodelog.Code0=reader.ReadContentAsString();
							break;
						case "Code1":
							claimcondcodelog.Code1=reader.ReadContentAsString();
							break;
						case "Code2":
							claimcondcodelog.Code2=reader.ReadContentAsString();
							break;
						case "Code3":
							claimcondcodelog.Code3=reader.ReadContentAsString();
							break;
						case "Code4":
							claimcondcodelog.Code4=reader.ReadContentAsString();
							break;
						case "Code5":
							claimcondcodelog.Code5=reader.ReadContentAsString();
							break;
						case "Code6":
							claimcondcodelog.Code6=reader.ReadContentAsString();
							break;
						case "Code7":
							claimcondcodelog.Code7=reader.ReadContentAsString();
							break;
						case "Code8":
							claimcondcodelog.Code8=reader.ReadContentAsString();
							break;
						case "Code9":
							claimcondcodelog.Code9=reader.ReadContentAsString();
							break;
						case "Code10":
							claimcondcodelog.Code10=reader.ReadContentAsString();
							break;
					}
				}
			}
			return claimcondcodelog;
		}


	}
}