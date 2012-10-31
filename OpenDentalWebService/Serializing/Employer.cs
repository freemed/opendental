using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Employer {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Employer employer) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Employer>");
			sb.Append("<EmployerNum>").Append(employer.EmployerNum).Append("</EmployerNum>");
			sb.Append("<EmpName>").Append(SerializeStringEscapes.EscapeForXml(employer.EmpName)).Append("</EmpName>");
			sb.Append("<Address>").Append(SerializeStringEscapes.EscapeForXml(employer.Address)).Append("</Address>");
			sb.Append("<Address2>").Append(SerializeStringEscapes.EscapeForXml(employer.Address2)).Append("</Address2>");
			sb.Append("<City>").Append(SerializeStringEscapes.EscapeForXml(employer.City)).Append("</City>");
			sb.Append("<State>").Append(SerializeStringEscapes.EscapeForXml(employer.State)).Append("</State>");
			sb.Append("<Zip>").Append(SerializeStringEscapes.EscapeForXml(employer.Zip)).Append("</Zip>");
			sb.Append("<Phone>").Append(SerializeStringEscapes.EscapeForXml(employer.Phone)).Append("</Phone>");
			sb.Append("</Employer>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Employer Deserialize(string xml) {
			OpenDentBusiness.Employer employer=new OpenDentBusiness.Employer();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EmployerNum":
							employer.EmployerNum=reader.ReadContentAsLong();
							break;
						case "EmpName":
							employer.EmpName=reader.ReadContentAsString();
							break;
						case "Address":
							employer.Address=reader.ReadContentAsString();
							break;
						case "Address2":
							employer.Address2=reader.ReadContentAsString();
							break;
						case "City":
							employer.City=reader.ReadContentAsString();
							break;
						case "State":
							employer.State=reader.ReadContentAsString();
							break;
						case "Zip":
							employer.Zip=reader.ReadContentAsString();
							break;
						case "Phone":
							employer.Phone=reader.ReadContentAsString();
							break;
					}
				}
			}
			return employer;
		}


	}
}