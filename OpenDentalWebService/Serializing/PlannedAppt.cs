using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class PlannedAppt {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.PlannedAppt plannedappt) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<PlannedAppt>");
			sb.Append("<PlannedApptNum>").Append(plannedappt.PlannedApptNum).Append("</PlannedApptNum>");
			sb.Append("<PatNum>").Append(plannedappt.PatNum).Append("</PatNum>");
			sb.Append("<AptNum>").Append(plannedappt.AptNum).Append("</AptNum>");
			sb.Append("<ItemOrder>").Append(plannedappt.ItemOrder).Append("</ItemOrder>");
			sb.Append("</PlannedAppt>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.PlannedAppt Deserialize(string xml) {
			OpenDentBusiness.PlannedAppt plannedappt=new OpenDentBusiness.PlannedAppt();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "PlannedApptNum":
							plannedappt.PlannedApptNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "PatNum":
							plannedappt.PatNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "AptNum":
							plannedappt.AptNum=System.Convert.ToInt64(reader.ReadContentAsString());
							break;
						case "ItemOrder":
							plannedappt.ItemOrder=System.Convert.ToInt32(reader.ReadContentAsString());
							break;
					}
				}
			}
			return plannedappt;
		}


	}
}