using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class Schedule {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.Schedule schedule) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<Schedule>");
			sb.Append("<ScheduleNum>").Append(schedule.ScheduleNum).Append("</ScheduleNum>");
			sb.Append("<SchedDate>").Append(schedule.SchedDate.ToString("yyyyMMddHHmmss")).Append("</SchedDate>");
			sb.Append("<StartTime>").Append(schedule.StartTime.ToString()).Append("</StartTime>");
			sb.Append("<StopTime>").Append(schedule.StopTime.ToString()).Append("</StopTime>");
			sb.Append("<SchedType>").Append((int)schedule.SchedType).Append("</SchedType>");
			sb.Append("<ProvNum>").Append(schedule.ProvNum).Append("</ProvNum>");
			sb.Append("<BlockoutType>").Append(schedule.BlockoutType).Append("</BlockoutType>");
			sb.Append("<Note>").Append(SerializeStringEscapes.EscapeForXml(schedule.Note)).Append("</Note>");
			sb.Append("<Status>").Append((int)schedule.Status).Append("</Status>");
			sb.Append("<EmployeeNum>").Append(schedule.EmployeeNum).Append("</EmployeeNum>");
			sb.Append("<DateTStamp>").Append(schedule.DateTStamp.ToString("yyyyMMddHHmmss")).Append("</DateTStamp>");
			sb.Append("</Schedule>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.Schedule Deserialize(string xml) {
			OpenDentBusiness.Schedule schedule=new OpenDentBusiness.Schedule();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ScheduleNum":
							schedule.ScheduleNum=reader.ReadContentAsLong();
							break;
						case "SchedDate":
							schedule.SchedDate=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
						case "StartTime":
							schedule.StartTime=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "StopTime":
							schedule.StopTime=TimeSpan.Parse(reader.ReadContentAsString());
							break;
						case "SchedType":
							schedule.SchedType=(OpenDentBusiness.ScheduleType)reader.ReadContentAsInt();
							break;
						case "ProvNum":
							schedule.ProvNum=reader.ReadContentAsLong();
							break;
						case "BlockoutType":
							schedule.BlockoutType=reader.ReadContentAsLong();
							break;
						case "Note":
							schedule.Note=reader.ReadContentAsString();
							break;
						case "Status":
							schedule.Status=(OpenDentBusiness.SchedStatus)reader.ReadContentAsInt();
							break;
						case "EmployeeNum":
							schedule.EmployeeNum=reader.ReadContentAsLong();
							break;
						case "DateTStamp":
							schedule.DateTStamp=DateTime.ParseExact(reader.ReadContentAsString(),"yyyyMMddHHmmss",null);
							break;
					}
				}
			}
			return schedule;
		}


	}
}