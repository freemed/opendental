using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TimeCardRule {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TimeCardRule timecardrule) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TimeCardRule>");
			sb.Append("<TimeCardRuleNum>").Append(timecardrule.TimeCardRuleNum).Append("</TimeCardRuleNum>");
			sb.Append("<EmployeeNum>").Append(timecardrule.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<OverHoursPerDay>").Append(timecardrule.OverHoursPerDay.ToString()).Append("</OverHoursPerDay>");
			sb.Append("<AfterTimeOfDay>").Append(timecardrule.AfterTimeOfDay.ToString()).Append("</AfterTimeOfDay>");
			sb.Append("<BeforeTimeOfDay>").Append(timecardrule.BeforeTimeOfDay.ToString()).Append("</BeforeTimeOfDay>");
			sb.Append("<AmtDiff>").Append(timecardrule.AmtDiff).Append("</AmtDiff>");
			sb.Append("</TimeCardRule>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TimeCardRule Deserialize(string xml) {
			OpenDentBusiness.TimeCardRule timecardrule=new OpenDentBusiness.TimeCardRule();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TimeCardRuleNum":
							timecardrule.TimeCardRuleNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EmployeeNum":
							timecardrule.EmployeeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "OverHoursPerDay":
							timecardrule.OverHoursPerDay=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "AfterTimeOfDay":
							timecardrule.AfterTimeOfDay=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "BeforeTimeOfDay":
							timecardrule.BeforeTimeOfDay=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "AmtDiff":
							timecardrule.AmtDiff=System.Convert.ToDouble(reader.ReadContentAsString());
							break;
					}
				}
			}
			return timecardrule;
		}


	}
}