package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Question {
		/** Primary key. */
		public int QuestionNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** The order that this question shows in the list. */
		public int ItemOrder;
		/** The original question. */
		public String Description;
		/** The answer to the question in text form. */
		public String Answer;
		/** FK to formpat.FormPatNum */
		public int FormPatNum;

		/** Deep copy of object. */
		public Question Copy() {
			Question question=new Question();
			question.QuestionNum=this.QuestionNum;
			question.PatNum=this.PatNum;
			question.ItemOrder=this.ItemOrder;
			question.Description=this.Description;
			question.Answer=this.Answer;
			question.FormPatNum=this.FormPatNum;
			return question;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Question>");
			sb.append("<QuestionNum>").append(QuestionNum).append("</QuestionNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<Answer>").append(Serializing.EscapeForXml(Answer)).append("</Answer>");
			sb.append("<FormPatNum>").append(FormPatNum).append("</FormPatNum>");
			sb.append("</Question>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				QuestionNum=Integer.valueOf(doc.getElementsByTagName("QuestionNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				Answer=doc.getElementsByTagName("Answer").item(0).getFirstChild().getNodeValue();
				FormPatNum=Integer.valueOf(doc.getElementsByTagName("FormPatNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
