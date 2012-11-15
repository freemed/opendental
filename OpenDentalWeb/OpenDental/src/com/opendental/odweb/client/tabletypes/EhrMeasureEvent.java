package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class EhrMeasureEvent {
		/** Primary key. */
		public int EhrMeasureEventNum;
		/** Date and time of measure event. */
		public Date DateTEvent;
		/** Enum: EhrMeasureEventType.  */
		public EhrMeasureEventType EventType;
		/** FK to patient.PatNum */
		public int PatNum;
		/** Only used for some types: EducationProvided, TobaccoCessation. */
		public String MoreInfo;

		/** Deep copy of object. */
		public EhrMeasureEvent Copy() {
			EhrMeasureEvent ehrmeasureevent=new EhrMeasureEvent();
			ehrmeasureevent.EhrMeasureEventNum=this.EhrMeasureEventNum;
			ehrmeasureevent.DateTEvent=this.DateTEvent;
			ehrmeasureevent.EventType=this.EventType;
			ehrmeasureevent.PatNum=this.PatNum;
			ehrmeasureevent.MoreInfo=this.MoreInfo;
			return ehrmeasureevent;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrMeasureEvent>");
			sb.append("<EhrMeasureEventNum>").append(EhrMeasureEventNum).append("</EhrMeasureEventNum>");
			sb.append("<DateTEvent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTEvent)).append("</DateTEvent>");
			sb.append("<EventType>").append(EventType.ordinal()).append("</EventType>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<MoreInfo>").append(Serializing.EscapeForXml(MoreInfo)).append("</MoreInfo>");
			sb.append("</EhrMeasureEvent>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EhrMeasureEventNum=Integer.valueOf(doc.getElementsByTagName("EhrMeasureEventNum").item(0).getFirstChild().getNodeValue());
				DateTEvent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTEvent").item(0).getFirstChild().getNodeValue());
				EventType=EhrMeasureEventType.values()[Integer.valueOf(doc.getElementsByTagName("EventType").item(0).getFirstChild().getNodeValue())];
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				MoreInfo=doc.getElementsByTagName("MoreInfo").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum EhrMeasureEventType {
			/** 0 */
			EducationProvided,
			/** 1 */
			OnlineAccessProvided,
			/** 2-not tracked yet. */
			ElectronicCopyRequested,
			/** 3 */
			ElectronicCopyProvidedToPt,
			/** 4, For one office visit. */
			ClinicalSummaryProvidedToPt,
			/** 5 */
			ReminderSent,
			/** 6 */
			MedicationReconcile,
			/** 7 */
			SummaryOfCareProvidedToDr,
			/** 8 */
			TobaccoUseAssessed,
			/** 9 */
			TobaccoCessation
		}


}
