package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ReminderRule deepCopy() {
			ReminderRule reminderrule=new ReminderRule();
			reminderrule.ReminderRuleNum=this.ReminderRuleNum;
			reminderrule.ReminderCriterion=this.ReminderCriterion;
			reminderrule.CriterionFK=this.CriterionFK;
			reminderrule.CriterionValue=this.CriterionValue;
			reminderrule.Message=this.Message;
			return reminderrule;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ReminderRule>");
			sb.append("<ReminderRuleNum>").append(ReminderRuleNum).append("</ReminderRuleNum>");
			sb.append("<ReminderCriterion>").append(ReminderCriterion.ordinal()).append("</ReminderCriterion>");
			sb.append("<CriterionFK>").append(CriterionFK).append("</CriterionFK>");
			sb.append("<CriterionValue>").append(Serializing.escapeForXml(CriterionValue)).append("</CriterionValue>");
			sb.append("<Message>").append(Serializing.escapeForXml(Message)).append("</Message>");
			sb.append("</ReminderRule>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ReminderRuleNum")!=null) {
					ReminderRuleNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReminderRuleNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ReminderCriterion")!=null) {
					ReminderCriterion=EhrCriterion.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReminderCriterion"))];
				}
				if(Serializing.getXmlNodeValue(doc,"CriterionFK")!=null) {
					CriterionFK=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CriterionFK"));
				}
				if(Serializing.getXmlNodeValue(doc,"CriterionValue")!=null) {
					CriterionValue=Serializing.getXmlNodeValue(doc,"CriterionValue");
				}
				if(Serializing.getXmlNodeValue(doc,"Message")!=null) {
					Message=Serializing.getXmlNodeValue(doc,"Message");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ReminderRule: "+e.getMessage());
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
