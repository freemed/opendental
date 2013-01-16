package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public EhrMeasureEvent deepCopy() {
			EhrMeasureEvent ehrmeasureevent=new EhrMeasureEvent();
			ehrmeasureevent.EhrMeasureEventNum=this.EhrMeasureEventNum;
			ehrmeasureevent.DateTEvent=this.DateTEvent;
			ehrmeasureevent.EventType=this.EventType;
			ehrmeasureevent.PatNum=this.PatNum;
			ehrmeasureevent.MoreInfo=this.MoreInfo;
			return ehrmeasureevent;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrMeasureEvent>");
			sb.append("<EhrMeasureEventNum>").append(EhrMeasureEventNum).append("</EhrMeasureEventNum>");
			sb.append("<DateTEvent>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTEvent)).append("</DateTEvent>");
			sb.append("<EventType>").append(EventType.ordinal()).append("</EventType>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<MoreInfo>").append(Serializing.escapeForXml(MoreInfo)).append("</MoreInfo>");
			sb.append("</EhrMeasureEvent>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EhrMeasureEventNum")!=null) {
					EhrMeasureEventNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EhrMeasureEventNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTEvent")!=null) {
					DateTEvent=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTEvent"));
				}
				if(Serializing.getXmlNodeValue(doc,"EventType")!=null) {
					EventType=EhrMeasureEventType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"EventType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MoreInfo")!=null) {
					MoreInfo=Serializing.getXmlNodeValue(doc,"MoreInfo");
				}
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
