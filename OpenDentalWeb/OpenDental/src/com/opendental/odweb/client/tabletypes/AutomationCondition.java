package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public AutomationCondition Copy() {
			AutomationCondition automationcondition=new AutomationCondition();
			automationcondition.AutomationConditionNum=this.AutomationConditionNum;
			automationcondition.AutomationNum=this.AutomationNum;
			automationcondition.CompareField=this.CompareField;
			automationcondition.Comparison=this.Comparison;
			automationcondition.CompareString=this.CompareString;
			return automationcondition;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AutomationCondition>");
			sb.append("<AutomationConditionNum>").append(AutomationConditionNum).append("</AutomationConditionNum>");
			sb.append("<AutomationNum>").append(AutomationNum).append("</AutomationNum>");
			sb.append("<CompareField>").append(CompareField.ordinal()).append("</CompareField>");
			sb.append("<Comparison>").append(Comparison.ordinal()).append("</Comparison>");
			sb.append("<CompareString>").append(Serializing.EscapeForXml(CompareString)).append("</CompareString>");
			sb.append("</AutomationCondition>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				AutomationConditionNum=Integer.valueOf(doc.getElementsByTagName("AutomationConditionNum").item(0).getFirstChild().getNodeValue());
				AutomationNum=Integer.valueOf(doc.getElementsByTagName("AutomationNum").item(0).getFirstChild().getNodeValue());
				CompareField=AutoCondField.values()[Integer.valueOf(doc.getElementsByTagName("CompareField").item(0).getFirstChild().getNodeValue())];
				Comparison=AutoCondComparison.values()[Integer.valueOf(doc.getElementsByTagName("Comparison").item(0).getFirstChild().getNodeValue())];
				CompareString=doc.getElementsByTagName("CompareString").item(0).getFirstChild().getNodeValue();
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
