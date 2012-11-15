using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class TimeAdjust {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.TimeAdjust timeadjust) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<TimeAdjust>");
			sb.Append("<TimeAdjustNum>").Append(timeadjust.TimeAdjustNum).Append("</TimeAdjustNum>");
			sb.Append("<EmployeeNum>").Append(timeadjust.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<TimeEntry>").Append(timeadjust.TimeEntry.ToString("yyyyMMddHHmmss")).Append("</TimeEntry>");
			sb.Append("<RegHours>").Append(timeadjust.RegHours.ToString()).Append("</RegHours>");
			sb.Append("<OTimeHours>").Append(timeadjust.OTimeHours.ToString()).Append("</OTimeHours>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(timeadjust.Note)).Append("</Note>");
			sb.Append("<IsAuto>").Append((timeadjust.IsAuto)?1:0).Append("</IsAuto>");
			sb.Append("</TimeAdjust>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.TimeAdjust Deserialize(string xml) {
			OpenDentBusiness.TimeAdjust timeadjust=new OpenDentBusiness.TimeAdjust();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "TimeAdjustNum":
							timeadjust.TimeAdjustNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "EmployeeNum":
							timeadjust.EmployeeNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "TimeEntry":
							timeadjust.TimeEntry=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "RegHours":
							timeadjust.RegHours=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "OTimeHours":
							timeadjust.OTimeHours=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "Note":
							timeadjust.Note=reader.ReadContentAsString();
							break;
						case "IsAuto":
							timeadjust.IsAuto=reader.ReadContentAsString()!="0";
							break;
					}
				}
			}
			return timeadjust;
		}


	}
}