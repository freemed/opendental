package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"QuestionNum")!=null) {
					QuestionNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"QuestionNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"Answer")!=null) {
					Answer=Serializing.GetXmlNodeValue(doc,"Answer");
				}
				if(Serializing.GetXmlNodeValue(doc,"FormPatNum")!=null) {
					FormPatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FormPatNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}