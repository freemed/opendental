package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class QuestionDef {
		/** Primary key. */
		public int QuestionDefNum;
		/** The question as presented to the patient. */
		public String Description;
		/** The order that the Questions will show. */
		public int ItemOrder;
		/** Enum:QuestionType */
		public QuestionType QuestType;

		/** Deep copy of object. */
		public QuestionDef Copy() {
			QuestionDef questiondef=new QuestionDef();
			questiondef.QuestionDefNum=this.QuestionDefNum;
			questiondef.Description=this.Description;
			questiondef.ItemOrder=this.ItemOrder;
			questiondef.QuestType=this.QuestType;
			return questiondef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<QuestionDef>");
			sb.append("<QuestionDefNum>").append(QuestionDefNum).append("</QuestionDefNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<QuestType>").append(QuestType.ordinal()).append("</QuestType>");
			sb.append("</QuestionDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				QuestionDefNum=Integer.valueOf(doc.getElementsByTagName("QuestionDefNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				QuestType=QuestionType.values()[Integer.valueOf(doc.getElementsByTagName("QuestType").item(0).getFirstChild().getNodeValue())];
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** 0=FreeformText, 1=YesNoUnknown. Allows for later adding other types, 3=picklist, 4, etc */
		public enum QuestionType {
			/** 0 */
			FreeformText,
			/** 1 */
			YesNoUnknown
		}


}
