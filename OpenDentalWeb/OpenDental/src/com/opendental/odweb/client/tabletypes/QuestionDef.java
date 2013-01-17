package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public QuestionDef deepCopy() {
			QuestionDef questiondef=new QuestionDef();
			questiondef.QuestionDefNum=this.QuestionDefNum;
			questiondef.Description=this.Description;
			questiondef.ItemOrder=this.ItemOrder;
			questiondef.QuestType=this.QuestType;
			return questiondef;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<QuestionDef>");
			sb.append("<QuestionDefNum>").append(QuestionDefNum).append("</QuestionDefNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<QuestType>").append(QuestType.ordinal()).append("</QuestType>");
			sb.append("</QuestionDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"QuestionDefNum")!=null) {
					QuestionDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"QuestionDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"QuestType")!=null) {
					QuestType=QuestionType.valueOf(Serializing.getXmlNodeValue(doc,"QuestType"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing QuestionDef: "+e.getMessage());
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
