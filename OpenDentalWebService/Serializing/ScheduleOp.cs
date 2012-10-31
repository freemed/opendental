using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class ScheduleOp {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.ScheduleOp scheduleop) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<ScheduleOp>");
			sb.Append("<ScheduleOpNum>").Append(scheduleop.ScheduleOpNum).Append("</ScheduleOpNum>");
			sb.Append("<ScheduleNum>").Append(scheduleop.ScheduleNum).Append("</ScheduleNum>");
			sb.Append("<OperatoryNum>").Append(scheduleop.OperatoryNum).Append("</OperatoryNum>");
			sb.Append("</ScheduleOp>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.ScheduleOp Deserialize(string xml) {
			OpenDentBusiness.ScheduleOp scheduleop=new OpenDentBusiness.ScheduleOp();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "ScheduleOpNum":
							scheduleop.ScheduleOpNum=reader.ReadContentAsLong();
							break;
						case "ScheduleNum":
							scheduleop.ScheduleNum=reader.ReadContentAsLong();
							break;
						case "OperatoryNum":
							scheduleop.OperatoryNum=reader.ReadContentAsLong();
							break;
					}
				}
			}
			return scheduleop;
		}


	}
}