using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class AppointmentRule {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.AppointmentRule appointmentrule) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<AppointmentRule>");
			sb.Append("<AppointmentRuleNum>").Append(appointmentrule.AppointmentRuleNum).Append("</AppointmentRuleNum>");
			sb.Append("<RuleDesc>").Append(SerializeStringEscapes.EscapeForXml(appointmentrule.RuleDesc)).Append("</RuleDesc>");
			sb.Append("<CodeStart>").Append(SerializeStringEscapes.EscapeForXml(appointmentrule.CodeStart)).Append("</CodeStart>");
			sb.Append("<CodeEnd>").Append(SerializeStringEscapes.EscapeForXml(appointmentrule.CodeEnd)).Append("</CodeEnd>");
			sb.Append("<IsEnabled>").Append((appointmentrule.IsEnabled)?1:0).Append("</IsEnabled>");
			sb.Append("</AppointmentRule>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.AppointmentRule Deserialize(string xml) {
			OpenDentBusiness.AppointmentRule appointmentrule=new OpenDentBusiness.AppointmentRule();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "AppointmentRuleNum":
							appointmentrule.AppointmentRuleNum=reader.ReadContentAsLong();
							break;
						case "RuleDesc":
							appointmentrule.RuleDesc=reader.ReadContentAsString();
							break;
						case "CodeStart":
							appointmentrule.CodeStart=reader.ReadContentAsString();
							break;
						case "CodeEnd":
							appointmentrule.CodeEnd=reader.ReadContentAsString();
							break;
						case "IsEnabled":
							appointmentrule.IsEnabled=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return appointmentrule;
		}


	}
}