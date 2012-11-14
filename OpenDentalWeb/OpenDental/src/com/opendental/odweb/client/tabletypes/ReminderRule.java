package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ReminderRule {
		/** Primary key. */
		public int ReminderRuleNum;
		/** Enum:EhrCriterion Problem,Medication,Allergy,Age,Gender,LabResult. */
		public EhrCriterion ReminderCriterion;
		/** Foreign key to disease.DiseaseDefNum, medicationpat.MedicationNum, or allergy.AllergyDefNum. Will be 0 if Age, Gender, or LabResult are the trigger. */
		public int CriterionFK;
		/** Only used if Age, Gender, or LabResult are the trigger. Examples: "<25"(must include < or >), "Male"/"Female", "INR" (the simple description of the lab test) */
		public String CriterionValue;
		/** Text that will show as the reminder. */
		public String Message;

		/** Deep copy of object. */
		public ReminderRule Copy() {
			ReminderRule reminderrule=new ReminderRule();
			reminderrule.ReminderRuleNum=this.ReminderRuleNum;
			reminderrule.ReminderCriterion=this.ReminderCriterion;
			reminderrule.CriterionFK=this.CriterionFK;
			reminderrule.CriterionValue=this.CriterionValue;
			reminderrule.Message=this.Message;
			return reminderrule;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReminderRule>");
			sb.append("<ReminderRuleNum>").append(ReminderRuleNum).append("</ReminderRuleNum>");
			sb.append("<ReminderCriterion>").append(ReminderCriterion.ordinal()).append("</ReminderCriterion>");
			sb.append("<CriterionFK>").append(CriterionFK).append("</CriterionFK>");
			sb.append("<CriterionValue>").append(Serializing.EscapeForXml(CriterionValue)).append("</CriterionValue>");
			sb.append("<Message>").append(Serializing.EscapeForXml(Message)).append("</Message>");
			sb.append("</ReminderRule>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ReminderRuleNum=Integer.valueOf(doc.getElementsByTagName("ReminderRuleNum").item(0).getFirstChild().getNodeValue());
				ReminderCriterion=EhrCriterion.values()[Integer.valueOf(doc.getElementsByTagName("ReminderCriterion").item(0).getFirstChild().getNodeValue())];
				CriterionFK=Integer.valueOf(doc.getElementsByTagName("CriterionFK").item(0).getFirstChild().getNodeValue());
				CriterionValue=doc.getElementsByTagName("CriterionValue").item(0).getFirstChild().getNodeValue();
				Message=doc.getElementsByTagName("Message").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** EhrCriterion: Problem,Medication,Allergy,Age,Gender,LabResult */
		public enum EhrCriterion {
			/** 0-Problem (diseaseDef) */
			Problem,
			/** 1-Medication */
			Medication,
			/** 2-AllergyDef */
			Allergy,
			/** 3-Age */
			Age,
			/** 4-Gender */
			Gender,
			/** 5-LabResult */
			LabResult,
			/** 6-ICD9 */
			ICD9
		}


}
