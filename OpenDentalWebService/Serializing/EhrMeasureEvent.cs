using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;

namespace OpenDentalWebService {
	///<summary>This file is generated automatically by the crud, do not make any changes to this file because they will get overwritten.</summary>
	public class EhrMeasureEvent {

		///<summary></summary>
		public static string Serialize(OpenDentBusiness.EhrMeasureEvent ehrmeasureevent) {
			StringBuilder sb=new StringBuilder();
			sb.Append("<EhrMeasureEvent>");
			sb.Append("<EhrMeasureEventNum>").Append(ehrmeasureevent.EhrMeasureEventNum).Append("</EhrMeasureEventNum>");
			sb.Append("<DateTEvent>").Append(ehrmeasureevent.DateTEvent.ToString()).Append("</DateTEvent>");
			sb.Append("<EventType>").Append((int)ehrmeasureevent.EventType).Append("</EventType>");
			sb.Append("<PatNum>").Append(ehrmeasureevent.PatNum).Append("</PatNum>");
			sb.Append("<MoreInfo>").Append(SerializeStringEscapes.EscapeForXml(ehrmeasureevent.MoreInfo)).Append("</MoreInfo>");
			sb.Append("</EhrMeasureEvent>");
			return sb.ToString();
		}

		///<summary></summary>
		public static OpenDentBusiness.EhrMeasureEvent Deserialize(string xml) {
			OpenDentBusiness.EhrMeasureEvent ehrmeasureevent=new OpenDentBusiness.EhrMeasureEvent();
			using(XmlReader reader=XmlReader.Create(new StringReader(xml))) {
				reader.MoveToContent();
				while(reader.Read()) {
					//Only detect start elements.
					if(!reader.IsStartElement()) {
						continue;
					}
					switch(reader.Name) {
						case "EhrMeasureEventNum":
							ehrmeasureevent.EhrMeasureEventNum=reader.ReadContentAsLong();
							break;
						case "DateTEvent":
							ehrmeasureevent.DateTEvent=DateTime.Parse(reader.ReadContentAsString());
							break;
						case "EventType":
							ehrmeasureevent.EventType=(OpenDentBusiness.EhrMeasureEventType)reader.ReadContentAsInt();
							break;
						case "PatNum":
							ehrmeasureevent.PatNum=reader.ReadContentAsLong();
							break;
						case "MoreInfo":
							ehrmeasureevent.MoreInfo=reader.ReadContentAsString();
							break;
					}
				}
			}
			return ehrmeasureevent;
		}


	}
}