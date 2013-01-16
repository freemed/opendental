package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class EhrMeasure {
		/** Primary key. */
		public int EhrMeasureNum;
		/** Enum:EhrMeasureType */
		public EhrMeasureType MeasureType;
		/** 0-100, -1 indicates not entered yet. */
		public int Numerator;
		/** 0-100, -1 indicates not entered yet. */
		public int Denominator;
		/** Not a database column. */
		public String Objective;
		/** Not a database column. */
		public String Measure;
		/** Not a database column.  More than this percent for meaningful use. */
		public int PercentThreshold;
		/** Not a database column.  An explanation of which patients qualify for enumerator. */
		public String NumeratorExplain;
		/** Not a database column.  An explanation of which patients qualify for denominator. */
		public String DenominatorExplain;

		/** Deep copy of object. */
		public EhrMeasure deepCopy() {
			EhrMeasure ehrmeasure=new EhrMeasure();
			ehrmeasure.EhrMeasureNum=this.EhrMeasureNum;
			ehrmeasure.MeasureType=this.MeasureType;
			ehrmeasure.Numerator=this.Numerator;
			ehrmeasure.Denominator=this.Denominator;
			ehrmeasure.Objective=this.Objective;
			ehrmeasure.Measure=this.Measure;
			ehrmeasure.PercentThreshold=this.PercentThreshold;
			ehrmeasure.NumeratorExplain=this.NumeratorExplain;
			ehrmeasure.DenominatorExplain=this.DenominatorExplain;
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
			sb.append("<Objective>").append(Serializing.escapeForXml(Objective)).append("</Objective>");
			sb.append("<Measure>").append(Serializing.escapeForXml(Measure)).append("</Measure>");
			sb.append("<PercentThreshold>").append(PercentThreshold).append("</PercentThreshold>");
			sb.append("<NumeratorExplain>").append(Serializing.escapeForXml(NumeratorExplain)).append("</NumeratorExplain>");
			sb.append("<DenominatorExplain>").append(Serializing.escapeForXml(DenominatorExplain)).append("</DenominatorExplain>");
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
				if(Serializing.getXmlNodeValue(doc,"Objective")!=null) {
					Objective=Serializing.getXmlNodeValue(doc,"Objective");
				}
				if(Serializing.getXmlNodeValue(doc,"Measure")!=null) {
					Measure=Serializing.getXmlNodeValue(doc,"Measure");
				}
				if(Serializing.getXmlNodeValue(doc,"PercentThreshold")!=null) {
					PercentThreshold=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PercentThreshold"));
				}
				if(Serializing.getXmlNodeValue(doc,"NumeratorExplain")!=null) {
					NumeratorExplain=Serializing.getXmlNodeValue(doc,"NumeratorExplain");
				}
				if(Serializing.getXmlNodeValue(doc,"DenominatorExplain")!=null) {
					DenominatorExplain=Serializing.getXmlNodeValue(doc,"DenominatorExplain");
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
