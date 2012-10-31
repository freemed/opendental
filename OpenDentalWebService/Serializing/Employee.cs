using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Employee {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Employee employee) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Employee>");
			sb.Append("<EmployeeNum>").Append(employee.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<LName>").Append(SerializeStringEscapes.EscapeForXml(employee.LName)).Append("</LName>");
			sb.Append("<FName>").Append(SerializeStringEscapes.EscapeForXml(employee.FName)).Append("</FName>");
			sb.Append("<MiddleI>").Append(SerializeStringEscapes.EscapeForXml(employee.MiddleI)).Append("</MiddleI>");
			sb.Append("<IsHidden>").Append((employee.IsHidden)?1:0).Append("</IsHidden>");
			sb.Append("<ClockStatus>").Append(SerializeStringEscapes.EscapeForXml(employee.ClockStatus)).Append("</ClockStatus>");
			sb.Append("<PhoneExt>").Append(employee.PhoneExt).Append("</PhoneExt>");
			sb.Append("</Employee>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Employee Deserialize(string xml) {
			OpenDentBusiness.Employee employee=new OpenDentBusiness.Employee();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EmployeeNum":
							employee.EmployeeNum=reader.ReadContentAsLong();
							break;
						case "LName":
							employee.LName=reader.ReadContentAsString();
							break;
						case "FName":
							employee.FName=reader.ReadContentAsString();
							break;
						case "MiddleI":
							employee.MiddleI=reader.ReadContentAsString();
							break;
						case "IsHidden":
							employee.IsHidden=reader.ReadContentAsString()!="0";
							break;
						case "ClockStatus":
							employee.ClockStatus=reader.ReadContentAsString();
							break;
						case "PhoneExt":
							employee.PhoneExt=reader.ReadContentAsInt();
							break;
					}
				}
			}
			return employee;
		}


	}
}