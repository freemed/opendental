package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Schedule {
		/** Primary key. */
		public int ScheduleNum;
		/** Date for this timeblock. */
		public Date SchedDate;
		/** Start time for this timeblock. */
		public String StartTime;
		/** Stop time for this timeblock. */
		public String StopTime;
		/** Enum:ScheduleType 0=Practice,1=Provider,2=Blockout,3=Employee.  Practice is used as a way to indicate holidays and as a way to put a note in for the entire practice for one day.  But whenever type is Practice, times will be ignored. */
		public ScheduleType SchedType;
		/** FK to provider.ProvNum if a provider type. */
		public int ProvNum;
		/** FK to definition.DefNum if blockout.  eg. HighProduction, RCT Only, Emerg. */
		public int BlockoutType;
		/** This contains various types of text entered by the user. */
		public String Note;
		/** Enum:SchedStatus enumeration 0=Open,1=Closed,2=Holiday.  All blocks have a status of Open, but user doesn't see the status.  The "closed" status was previously used to override the defaults when the last timeblock was deleted.  But it's nearly phased out now.  Still used by blockouts.  Holidays are a special type of practice schedule item which do not have providers attached. */
		public SchedStatus Status;
		/** FK to employee.EmployeeNum. */
		public int EmployeeNum;
		/** Last datetime that this row was inserted or updated. */
		public Date DateTStamp;

		/** Deep copy of object. */
		public Schedule Copy() {
			Schedule schedule=new Schedule();
			schedule.ScheduleNum=this.ScheduleNum;
			schedule.SchedDate=this.SchedDate;
			schedule.StartTime=this.StartTime;
			schedule.StopTime=this.StopTime;
			schedule.SchedType=this.SchedType;
			schedule.ProvNum=this.ProvNum;
			schedule.BlockoutType=this.BlockoutType;
			schedule.Note=this.Note;
			schedule.Status=this.Status;
			schedule.EmployeeNum=this.EmployeeNum;
			schedule.DateTStamp=this.DateTStamp;
			return schedule;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Schedule>");
			sb.append("<ScheduleNum>").append(ScheduleNum).append("</ScheduleNum>");
			sb.append("<SchedDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</SchedDate>");
			sb.append("<StartTime>").append(Serializing.EscapeForXml(StartTime)).append("</StartTime>");
			sb.append("<StopTime>").append(Serializing.EscapeForXml(StopTime)).append("</StopTime>");
			sb.append("<SchedType>").append(SchedType.ordinal()).append("</SchedType>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<BlockoutType>").append(BlockoutType).append("</BlockoutType>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<Status>").append(Status.ordinal()).append("</Status>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTStamp>");
			sb.append("</Schedule>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ScheduleNum=Integer.valueOf(doc.getElementsByTagName("ScheduleNum").item(0).getFirstChild().getNodeValue());
				SchedDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("SchedDate").item(0).getFirstChild().getNodeValue());
				StartTime=doc.getElementsByTagName("StartTime").item(0).getFirstChild().getNodeValue();
				StopTime=doc.getElementsByTagName("StopTime").item(0).getFirstChild().getNodeValue();
				SchedType=ScheduleType.values()[Integer.valueOf(doc.getElementsByTagName("SchedType").item(0).getFirstChild().getNodeValue())];
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				BlockoutType=Integer.valueOf(doc.getElementsByTagName("BlockoutType").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				Status=SchedStatus.values()[Integer.valueOf(doc.getElementsByTagName("Status").item(0).getFirstChild().getNodeValue())];
				EmployeeNum=Integer.valueOf(doc.getElementsByTagName("EmployeeNum").item(0).getFirstChild().getNodeValue());
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** For schedule timeblocks. */
		public enum ScheduleType {
			/** 0 */
			Practice,
			/** 1 */
			Provider,
			/** 2 */
			Blockout,
			/** 3 */
			Employee
		}

		/** Schedule status.  Open=0,Closed=1,Holiday=2. */
		public enum SchedStatus {
			/** 0 */
			Open,
			/** 1 */
			Closed,
			/** 2 */
			Holiday
		}


}
