package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class EhrMeasure {
		/** Primary key. */
		public int EhrMeasureNum;
		/** Enum:EhrMeasureType */
		public EhrMeasureType MeasureType;
		/** 0-100, -1 indicates not entered yet. */
		public int Numerator;
		/** 0-100, -1 indicates not entered yet. */
		public int Denominator;

		/** Deep copy of object. */
		public EhrMeasure deepCopy() {
			EhrMeasure ehrmeasure=new EhrMeasure();
			ehrmeasure.EhrMeasureNum=this.EhrMeasureNum;
			ehrmeasure.MeasureType=this.MeasureType;
			ehrmeasure.Numerator=this.Numerator;
			ehrmeasure.Denominator=this.Denominator;
			return ehrmeasure;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrMeasure>");
			sb.append("<EhrMeasureNum>").append(EhrMeasureNum).append("</EhrMeasureNum>");
			sb.append("<MeasureType>").append(MeasureType.ordinal()).append("</MeasureType>");
			sb.append("<Numerator>").append(Numerator).append("</Numerator>");
			sb.append("<Denominator>").append(Denominator).append("</Denominator>");
			sb.append("</EhrMeasure>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EhrMeasureNum")!=null) {
					EhrMeasureNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EhrMeasureNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MeasureType")!=null) {
					MeasureType=EhrMeasureType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"MeasureType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Numerator")!=null) {
					Numerator=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Numerator"));
				}
				if(Serializing.getXmlNodeValue(doc,"Denominator")!=null) {
					Denominator=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Denominator"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum EhrMeasureType {
			/** 0 */
			ProblemList,
			/** 1 */
			MedicationList,
			/** 2 */
			AllergyList,
			/** 3 */
			Demographics,
			/** 4 */
			Education,
			/** 5 */
			TimelyAccess,
			/** 6 */
			ProvOrderEntry,
			/** 7 */
			Rx,
			/** 8 */
			VitalSigns,
			/** 9 */
			Smoking,
			/** 10 */
			Lab,
			/** 11 */
			ElectronicCopy,
			/** 12 */
			ClinicalSummaries,
			/** 13 */
			Reminders,
			/** 14 */
			MedReconcile,
			/** 15- Summary of care record for transition or referral. */
			SummaryOfCare
		}


}
