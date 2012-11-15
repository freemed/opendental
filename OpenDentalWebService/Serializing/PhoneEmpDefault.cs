using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PhoneEmpDefault {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PhoneEmpDefault phoneempdefault) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PhoneEmpDefault>");
			sb.Append("<EmployeeNum>").Append(phoneempdefault.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<NoGraph>").Append((phoneempdefault.NoGraph)?1:0).Append("</NoGraph>");
			sb.Append("<NoColor>").Append((phoneempdefault.NoColor)?1:0).Append("</NoColor>");
			sb.Append("<RingGroups>").Append((int)phoneempdefault.RingGroups).Append("</RingGroups>");
			sb.Append("<EmpName>").Append(SerializeStringEscapes.EscapeForXml(phoneempdefault.EmpName)).Append("</EmpName>");
			sb.Append("<PhoneExt>").Append(phoneempdefault.PhoneExt).Append("</PhoneExt>");
			sb.Append("<StatusOverride>").Append((int)phoneempdefault.StatusOverride).Append("</StatusOverride>");
			sb.Append("<Notes>").Append(SerializeStringEscapes.EscapeForXml(phoneempdefault.Notes)).Append("</Notes>");
			sb.Append("<ComputerName>").Append(SerializeStringEscapes.EscapeForXml(phoneempdefault.ComputerName)).Append("</ComputerName>");
			sb.Append("<IsPrivateScreen>").Append((phoneempdefault.IsPrivateScreen)?1:0).Append("</IsPrivateScreen>");
			sb.Append("<IsTriageOperator>").Append((phoneempdefault.IsTriageOperator)?1:0).Append("</IsTriageOperator>");
			sb.Append("</PhoneEmpDefault>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PhoneEmpDefault Deserialize(string xml) {
			OpenDentBusiness.PhoneEmpDefault phoneempdefault=new OpenDentBusiness.PhoneEmpDefault();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EmployeeNum":
							phoneempdefault.EmployeeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "NoGraph":
							phoneempdefault.NoGraph=reader.ReadContentAsString()!="0";
							break;
						case "NoColor":
							phoneempdefault.NoColor=reader.ReadContentAsString()!="0";
							break;
						case "RingGroups":
							phoneempdefault.RingGroups=(OpenDentBusiness.AsteriskRingGroups)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "EmpName":
							phoneempdefault.EmpName=reader.ReadContentAsString();
							break;
						case "PhoneExt":
							phoneempdefault.PhoneExt=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "StatusOverride":
							phoneempdefault.StatusOverride=(OpenDentBusiness.PhoneEmpStatusOverride)System.Convert.ToInt32(reader.ReadContentAsString());
							break;
						case "Notes":
							phoneempdefault.Notes=reader.ReadContentAsString();
							break;
						case "ComputerName":
							phoneempdefault.ComputerName=reader.ReadContentAsString();
							break;
						case "IsPrivateScreen":
							phoneempdefault.IsPrivateScreen=reader.ReadContentAsString()!="0";
							break;
						case "IsTriageOperator":
							phoneempdefault.IsTriageOperator=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return phoneempdefault;
		}


	}
}