package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
		public EhrMeasure Copy() {
			EhrMeasure ehrmeasure=new EhrMeasure();
			ehrmeasure.EhrMeasureNum=this.EhrMeasureNum;
			ehrmeasure.MeasureType=this.MeasureType;
			ehrmeasure.Numerator=this.Numerator;
			ehrmeasure.Denominator=this.Denominator;
			return ehrmeasure;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<EhrMeasure>");
			sb.append("<EhrMeasureNum>").append(EhrMeasureNum).append("</EhrMeasureNum>");
			sb.append("<MeasureType>").append(MeasureType.ordinal()).append("</MeasureType>");
			sb.append("<Numerator>").append(Numerator).append("</Numerator>");
			sb.append("<Denominator>").append(Denominator).append("</Denominator>");
			sb.append("</EhrMeasure>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EhrMeasureNum=Integer.valueOf(doc.getElementsByTagName("EhrMeasureNum").item(0).getFirstChild().getNodeValue());
				MeasureType=EhrMeasureType.values()[Integer.valueOf(doc.getElementsByTagName("MeasureType").item(0).getFirstChild().getNodeValue())];
				Numerator=Integer.valueOf(doc.getElementsByTagName("Numerator").item(0).getFirstChild().getNodeValue());
				Denominator=Integer.valueOf(doc.getElementsByTagName("Denominator").item(0).getFirstChild().getNodeValue());
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
