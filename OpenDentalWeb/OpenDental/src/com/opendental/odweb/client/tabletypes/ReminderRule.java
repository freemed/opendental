package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ReminderRuleNum")!=null) {
					ReminderRuleNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ReminderRuleNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ReminderCriterion")!=null) {
					ReminderCriterion=EhrCriterion.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ReminderCriterion"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"CriterionFK")!=null) {
					CriterionFK=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CriterionFK"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CriterionValue")!=null) {
					CriterionValue=Serializing.GetXmlNodeValue(doc,"CriterionValue");
				}
				if(Serializing.GetXmlNodeValue(doc,"Message")!=null) {
					Message=Serializing.GetXmlNodeValue(doc,"Message");
				}
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
