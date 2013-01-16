using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class SecurityLog {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.SecurityLog securitylog) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<SecurityLog>");
			sb.Append("<SecurityLogNum>").Append(securitylog.SecurityLogNum).Append("</SecurityLogNum>");
			sb.Append("<PermType>").Append((int)securitylog.PermType).Append("</PermType>");
			sb.Append("<UserNum>").Append(securitylog.UserNum).Append("</UserNum>");
			sb.Append("<LogDateTime>").Append(securitylog.LogDateTime.ToString("yyyyMMddHHmmss")).Append("</LogDateTime>");
			sb.Append("<LogText>").Append(SerializeStringEscapes.EscapeForXml(securitylog.LogText)).Append("</LogText>");
			sb.Append("<PatNum>").Append(securitylog.PatNum).Append("</PatNum>");
			sb.Append("<CompName>").Append(SerializeStringEscapes.EscapeForXml(securitylog.CompName)).Append("</CompName>");
			sb.Append("<PatientName>").Append(SerializeStringEscapes.EscapeForXml(securitylog.PatientName)).Append("</PatientName>");
			sb.Append("<FKey>").Append(securitylog.FKey).Append("</FKey>");
			sb.Append("</SecurityLog>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.SecurityLog Deserialize(string xml) {
			OpenDentBusiness.SecurityLog securitylog=new OpenDentBusiness.SecurityLog();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "SecurityLogNum":
							securitylog.SecurityLogNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PermType":
							securitylog.PermType=(OpenDentBusiness.Permissions)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "UserNum":
							securitylog.UserNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "LogDateTime":
							securitylog.LogDateTime=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "LogText":
							securitylog.LogText=reader.ReadContentAsString();
							break;
						case "PatNum":
							securitylog.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "CompName":
							securitylog.CompName=reader.ReadContentAsString();
							break;
						case "PatientName":
							securitylog.PatientName=reader.ReadContentAsString();
							break;
						case "FKey":
							securitylog.FKey=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
					}
				}
			}
			return securitylog;
		}


	}
}