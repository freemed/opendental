package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class AutomationCondition {
		/** Primary key. */
		public int AutomationConditionNum;
		/** FK to automation.AutomationNum.  */
		public int AutomationNum;
		/** Enum:AutoCondField  */
		public AutoCondField CompareField;
		/** Enum:AutoCondComparison Not all comparisons are allowed with all data types. */
		public AutoCondComparison Comparison;
		/** . */
		public String CompareString;

		/** Deep copy of object. */
		public AutomationCondition deepCopy() {
			AutomationCondition automationcondition=new AutomationCondition();
			automationcondition.AutomationConditionNum=this.AutomationConditionNum;
			automationcondition.AutomationNum=this.AutomationNum;
			automationcondition.CompareField=this.CompareField;
			automationcondition.Comparison=this.Comparison;
			automationcondition.CompareString=this.CompareString;
			return automationcondition;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutomationCondition>");
			sb.append("<AutomationConditionNum>").append(AutomationConditionNum).append("</AutomationConditionNum>");
			sb.append("<AutomationNum>").append(AutomationNum).append("</AutomationNum>");
			sb.append("<CompareField>").append(CompareField.ordinal()).append("</CompareField>");
			sb.append("<Comparison>").append(Comparison.ordinal()).append("</Comparison>");
			sb.append("<CompareString>").append(Serializing.escapeForXml(CompareString)).append("</CompareString>");
			sb.append("</AutomationCondition>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AutomationConditionNum")!=null) {
					AutomationConditionNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutomationConditionNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AutomationNum")!=null) {
					AutomationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutomationNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CompareField")!=null) {
					CompareField=AutoCondField.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"CompareField"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Comparison")!=null) {
					Comparison=AutoCondComparison.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Comparison"))];
				}
				if(Serializing.getXmlNodeValue(doc,"CompareString")!=null) {
					CompareString=Serializing.getXmlNodeValue(doc,"CompareString");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum AutoCondField {
			/** Typically specify Equals the exact name/description of the sheet. */
			SheetNotCompletedTodayWithName,
			/** disease */
			Problem,
			/**  */
			Medication,
			/**  */
			Allergy,
			/** Example, 23 */
			Age,
			/** Allowed values are M or F, not case sensitive.  Enforce at entry time. */
			Gender,
			/**  */
			Labresult
		}

		/**  */
		public enum AutoCondComparison {
			/** Not sensitive to capitalization. */
			Equals,
			/**  */
			GreaterThan,
			/**  */
			LessThan,
			/** aka Like */
			Contains
		}


}
